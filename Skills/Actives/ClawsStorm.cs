using EntityStates;
using EntityStates.AffixEarthHealer;
using HarmonyLib;
using Panthera;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.GUI;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.OldSkills;
using Panthera.Skills.Actives;
using Panthera.Skills.Passives;
using Panthera.Utils;
using R2API;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using RoR2.Skills;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using static RoR2.CameraTargetParams;
using static UnityEngine.ParticleSystem;

namespace Panthera.Skills.Actives
{
    class ClawsStorm : MachineScript
    {

        public float fireRate;
        public float damageMultiplier = PantheraConfig.clawsStorm_damageMultiplier;
        public Vector3 dashDirection;
        public bool firstUse = true;
        public float lastFired = 0;
        public float lastFuryConsumed = 0;
        public int origPlayerLayer;
        public int effectID;
        public List<GameObject> enemiesCompList = new List<GameObject>();

        public ClawsStorm()
        {
            base.icon = PantheraAssets.ClawStormSkill;
            base.name = PantheraTokens.Get("ability_ClawsStormName");
            base.baseCooldown = PantheraConfig.ClawsStorm_cooldown;
            base.desc1 = String.Format(Utils.PantheraTokens.Get("ability_ClawsStormDesc"), PantheraConfig.clawsStorm_damageMultiplier * 100, PantheraConfig.ClawsStorm_firedDelay, PantheraConfig.ClawsStorm_continuousConsumedFury);
            base.desc2 = null;
            base.skillID = PantheraConfig.ClawsStorm_SkillID;
            base.priority = PantheraConfig.ClawsStorm_priority;
            base.requiredAbilityID = PantheraConfig.ClawsStorm_AbilityID;
            base.interruptPower = PantheraConfig.ClawsStorm_interruptPower;
            base.activated = false;
        }

        public override bool CanBeUsed(PantheraObj ptraObj)
        {
            if (ptraObj.characterBody.fury < PantheraConfig.ClawsStorm_requiredFury) return false;
            if (ptraObj.skillLocator.getStock(PantheraConfig.ClawsStorm_SkillID) <= 0) return false;
            return true;
        }

        public override void Start()
        {

            // Set in combat //
            base.characterBody.outOfCombatStopwatch = 0f;

            // Set the camera to ClawsStorm //
            CamHelper.ApplyCameraType(CamHelper.AimType.ClawsStorm, pantheraObj);

            // Get the Effect //
            GameObject effectColor = PantheraAssets.ClawsStormWhiteFX;
            if (base.pantheraObj.PantheraSkinIndex == 1)
                effectColor = PantheraAssets.ClawsStormWhiteFX;
            else if (base.pantheraObj.PantheraSkinIndex == 2)
                effectColor = PantheraAssets.ClawsStormOrangeFX;
            else if (base.pantheraObj.PantheraSkinIndex == 3)
                effectColor = PantheraAssets.ClawsStormOrangeFX;
            else if (base.pantheraObj.PantheraSkinIndex == 4)
                effectColor = PantheraAssets.ClawsStormRedFX;

            // Start the effect //
            this.effectID = FXManager.SpawnEffect(base.gameObject, effectColor, base.modelTransform.position, 1, base.characterBody.gameObject, new Quaternion(), true);

            // Change the Player layer //
            //this.origPlayerLayer = gameObject.layer;
            //base.gameObject.layer = LayerIndex.fakeActor.intVal;
            //base.characterMotor.Motor.RebuildCollidableLayers();

            // Hide the Model //
            base.pantheraObj.characterModel.invisibilityCount += 1;

            // Get the Damages //
            int tornadoLevel = base.pantheraObj.profileComponent.getAbilityLevel(PantheraConfig.Tornado_AbilityID);
            if (tornadoLevel == 1)
                this.damageMultiplier *= 1 + PantheraConfig.Tornado_damagePercent1;
            else if (tornadoLevel == 2)
                this.damageMultiplier *= 1 + PantheraConfig.Tornado_damagePercent2;
            else if (tornadoLevel == 3)
                this.damageMultiplier *= 1 + PantheraConfig.Tornado_damagePercent3;

            // Do the dash //
            dash();

            // Set god mod //
            //characterBody.healthComponent.godMode = true;

            // Set as activated //
            base.pantheraObj.clawsStormActivated = true;
            new ServerSetClawsStormMessage(base.gameObject, true, base.pantheraObj.characterModel.invisibilityCount).Send(NetworkDestination.Server);

            // Clear the List //
            this.enemiesCompList.Clear();

        }

        public override void Update()
        {

        }

        public override void FixedUpdate()
        {

            if (base.characterBody.fury < PantheraConfig.ClawsStorm_continuousConsumedFury || this.wasInterrupted == true || base.inputBank.keysPressed.HasFlag(KeysBinder.KeysEnum.Skill2) == false)
            {
                base.EndScript();
            }

            // Consume Fury //
            //float lastFuryConsumedTime = Time.time - lastFuryConsumed;
            //if (lastFuryConsumedTime >= PantheraConfig.ClawsStorm_continuousConsumeTime)
            //{
            //    characterBody.fury -= PantheraConfig.ClawsStorm_continuousConsumedFury;
            //    lastFuryConsumed = Time.time;
            //}

            // Fire the attack //
            this.fireRate = PantheraConfig.ClawsStorm_firedDelay / base.characterBody.attackSpeed;
            float lastFiredTime = Time.time - this.lastFired;
            if (lastFiredTime >= this.fireRate)
            {

                // Play the Sound //
                Sound.playSound(Sound.ClawsStorm, gameObject);

                // Consume the Fury //
                base.characterBody.fury -= PantheraConfig.ClawsStorm_continuousConsumedFury;

                // Do the Attack //
                List<HurtBox> hitResults = new List<HurtBox>();
                float damage = characterBody.damage * this.damageMultiplier;
                bool isCrit = RollCrit();
                OverlapAttack attack = Functions.CreateOverlapAttack(base.gameObject, damage, isCrit, PantheraConfig.ClawsStorm_procCoefficient, PantheraConfig.ClawsStorm_hitboxName);
                attack.Fire(hitResults);
                this.lastFired = Time.time;

                // Calcule Healing Storm //
                //float healMult = CharacterAbilities.clawsStorm_healMultiplier;
                //float heal = damage;
                //if (isCrit) heal *= 2;
                //heal *= healMult;

                // Itineratre the Enemies List //
                //foreach (HurtBox hurtbox in hitResults)
                //{
                //    TeamComponent tc = hurtbox.healthComponent?.body?.teamComponent;
                //    if (tc != null && tc.teamIndex == TeamIndex.Monster)
                //    {
                //        GameObject obj = tc.body.gameObject;
                //        // Grab the Enemies //
                //        if (obj.GetComponent<HoldTarget>() == null)
                //        {
                //            enemiesCompList.Add(obj);
                //            HoldTarget comp = obj.AddComponent<HoldTarget>();
                //            float relativeDistance = Vector3.Distance(obj.transform.position, characterBody.corePosition);
                //            comp.skillScript = this;
                //            comp.ptraObj = pantheraObj;
                //            comp.relativeDistance = relativeDistance;
                //        }
                //        // Apply Healing Storm //
                //        //if (heal > 0)
                //        //    new ServerHeal(characterBody.gameObject, Math.Max(1, heal)).Send(NetworkDestination.Server);
                //    }
                //}

            }

            // Do the Dash //
            dash();

        }

        public override void Stop()
        {

            // Remove all Enemies HoldTarget Component //
            //foreach (GameObject obj in enemiesCompList)
            //{
            //    if (obj != null && obj.GetComponent<HoldTarget>() != null)
            //        obj.GetComponent<HoldTarget>().SetToDestroy();
            //}

            // Restore the camera //
            CamHelper.ApplyCameraType(CamHelper.AimType.Standard, pantheraObj);

            // Restore the Player layer //
            //gameObject.layer = origPlayerLayer;
            //characterMotor.Motor.RebuildCollidableLayers();

            // Show the Model //
            base.pantheraObj.characterModel.invisibilityCount -= 1;

            // Disable god mode //
            //characterBody.healthComponent.godMode = false;

            // Stop the effect //
            FXManager.DestroyEffect(this.effectID, 1);

            // Set the Cooldown //
            skillLocator.startCooldown(PantheraConfig.ClawsStorm_SkillID);

            // Set as deactivated //
            new ServerSetClawsStormMessage(base.gameObject, false, base.pantheraObj.characterModel.invisibilityCount).Send(NetworkDestination.Server);

        }

        public void dash()
        {
            // Get the character direction //
            if (this.firstUse == true && base.characterBody.aimTimer > 0 && base.inputBank && base.characterDirection)
            {
                this.firstUse = false;
                base.characterBody.SetAimTimer(0);
                this.dashDirection = (base.inputBank.moveVector == Vector3.zero ? base.characterDirection.forward : base.inputBank.moveVector).normalized;
                base.characterDirection.forward = this.dashDirection;
            }
            // Set the velocity //
            this.dashDirection = base.characterDirection.forward;
            if (base.characterMotor.isGrounded == false)
            {
                this.dashDirection.y = base.GetAimRay().direction.y;
                base.characterDirection.forward = base.GetAimRay().direction;
            }
            float moveSpeed = base.moveSpeedStat * PantheraConfig.ClawsStorm_dashSpeedMultiplicator;
            moveSpeed = Math.Min(PantheraConfig.ClawsStorm_maxMoveSpeed, moveSpeed);
            moveSpeed = Math.Max(PantheraConfig.ClawsStorm_minMoveSpeed, moveSpeed);
            base.pantheraObj.pantheraMotor.isSprinting = true;
            base.characterBody.characterMotor.velocity = this.dashDirection * moveSpeed;
        }

    }

}