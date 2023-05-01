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
using Panthera.Skills;
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

namespace Panthera.Skills
{
    class ClawsStorm : MachineScript
    {

        public float fireRate;
        public Vector3 dashDirection;
        public bool firstUse = true;
        public float lastFired = 0;
        public float lastFuryConsumed = 0;
        public int origPlayerLayer;
        public int effect;
        public List<GameObject> enemiesCompList = new List<GameObject>();

        public ClawsStorm()
        {

        }

        public static void Create()
        {
            // Create the Skill //
            PantheraSkill skill = new PantheraSkill();
            skill.skillID = PantheraConfig.ClawsStorm_SkillID;
            skill.name = "CLAWSSTORM_SKILL_NAME";
            skill.desc = "CLAWSSTORM_SKILL_DESC";
            skill.icon = Assets.ClawStorm;
            skill.iconPrefab = Assets.ActiveSkillPrefab;
            skill.type = PantheraSkill.SkillType.active;
            skill.associatedSkill = typeof(ClawsStorm);
            skill.priority = PantheraConfig.ClawsStorm_priority;
            skill.interruptPower = PantheraConfig.ClawsStorm_interruptPower;
            skill.cooldown = PantheraConfig.ClawsStorm_cooldown;
            skill.requiredFury = PantheraConfig.ClawsStorm_requiredFury;

            // Save this Skill //
            PantheraSkill.SkillDefsList.Add(skill.skillID, skill);
        }

        public override PantheraSkill getSkillDef()
        {
            return base.pantheraObj.activePreset.getSkillByID(PantheraConfig.ClawsStorm_SkillID);
        }

        public override bool CanBeUsed(PantheraObj ptraObj)
        {
            base.pantheraObj = ptraObj;
            if (ptraObj.characterBody.fury < this.getSkillDef().requiredFury) return false;
            if (ptraObj.skillLocator.getCooldownInSecond(this.getSkillDef().skillID) > 0) return false;
            return true;
        }

        public override void Start()
        {

            // Set in combat //
            base.characterBody.outOfCombatStopwatch = 0f;

            // Unstealth //
            Passives.Stealth.DidDamageUnstealth(base.pantheraObj);

            // Set the camera to ClawsStorm //
            CamHelper.ApplyAimType(CamHelper.AimType.ClawsStorm, base.pantheraObj);

            // Get the Effect //
            GameObject effectColor = Assets.ClawsStormWhiteFX;
            if (base.pantheraObj.PantheraSkinIndex == 1)
                effectColor = Assets.ClawsStormWhiteFX;
            else if (base.pantheraObj.PantheraSkinIndex == 2)
                effectColor = Assets.ClawsStormOrangeFX;
            else if (base.pantheraObj.PantheraSkinIndex == 3)
                effectColor = Assets.ClawsStormOrangeFX;

            // Start the effect //
            this.effect = Utils.FXManager.SpawnEffect(base.gameObject, effectColor, base.modelTransform.position, 1, base.characterBody.gameObject, new Quaternion(), true);

            // Change the Player layer //
            //this.origPlayerLayer = gameObject.layer;
            //base.gameObject.layer = LayerIndex.fakeActor.intVal;
            //base.characterMotor.Motor.RebuildCollidableLayers();

            // Hide the Model //
            base.pantheraObj.characterModel.invisibilityCount += 1;

            // Do the dash //
            this.dash();

            // Set god mod //
            //characterBody.healthComponent.godMode = true;

        }

        public override void Update()
        {

        }

        public override void FixedUpdate()
        {

            if (this.characterBody.fury < PantheraConfig.ClawsStorm_continuousConsumedFury || base.wasInterrupted == true || base.inputBank.isSkillPressed(this.getSkillDef().skillID) == false)
            {
                this.EndScript();
            }

            // Consume Fury //
            float lastFuryConsumedTime = Time.time - this.lastFuryConsumed;
            if (lastFuryConsumedTime >= PantheraConfig.ClawsStorm_continuousConsumeTime)
            {
                this.characterBody.fury -= PantheraConfig.ClawsStorm_continuousConsumedFury;
                this.lastFuryConsumed = Time.time;
            }

            // Fire the attack //
            this.fireRate = base.pantheraObj.activePreset.clawsStorm_firedDelay / base.characterBody.attackSpeed;
            float lastFiredTime = Time.time - this.lastFired;
            if (lastFiredTime >= fireRate)
            {

                // Play the Sound //
                Sound.playSound(Sound.ClawsStorm, gameObject);

                // Do the Attack //
                List<HurtBox> hitResults = new List<HurtBox>();
                float damage = base.characterBody.damage * base.pantheraObj.activePreset.clawsStorm_damageMultiplier;
                bool isCrit = base.RollCrit();
                OverlapAttack attack = Functions.CreateOverlapAttack(gameObject, damage, isCrit, PantheraConfig.ClawsStorm_hitboxName);
                attack.Fire(hitResults);
                lastFired = Time.time;

                // Consume the Fury //
                //this.characterBody.fury -= PantheraConfig.ClawsStorm_continuousConsumedFury;

                // Calcule Healing Storm //
                float healMult = base.pantheraObj.activePreset.clawsStorm_healMultiplier;
                float heal = damage;
                if (isCrit) heal *= 2;
                heal *= healMult;

                // Itineratre the Enemies List //
                foreach (HurtBox hurtbox in hitResults)
                {
                    TeamComponent tc = hurtbox.healthComponent?.body?.teamComponent;
                    if (tc != null && tc.teamIndex == TeamIndex.Monster)
                    {
                        GameObject obj = tc.body.gameObject;
                        // Grab the Enemies //
                        if (obj.GetComponent<HoldTarget>() == null)
                        {
                            this.enemiesCompList.Add(obj);
                            HoldTarget comp = obj.AddComponent<HoldTarget>();
                            float relativeDistance = Vector3.Distance(obj.transform.position, base.characterBody.corePosition);
                            comp.skillScript = this;
                            comp.ptraObj = base.pantheraObj;
                            comp.relativeDistance = relativeDistance;
                        }
                        // Apply Healing Storm //
                        if (heal > 0)
                            new ServerHeal(base.characterBody.gameObject, Math.Max(1, heal)).Send(NetworkDestination.Server);
                    }
                }

            }

            // Do the Dash //
            dash();

        }

        public override void Stop()
        {

            // Remove all Enemies HoldTarget Component //
            foreach (GameObject obj in this.enemiesCompList)
            {
                if (obj != null && obj.GetComponent<HoldTarget>() != null)
                    obj.GetComponent<HoldTarget>().SetToDestroy();
            }

            // Restore the camera //
            CamHelper.ApplyAimType(CamHelper.AimType.Standard, base.pantheraObj);

            // Restore the Player layer //
            //gameObject.layer = origPlayerLayer;
            //characterMotor.Motor.RebuildCollidableLayers();

            // Show the Model //
            base.pantheraObj.characterModel.invisibilityCount -= 1;

            // Disable god mode //
            //characterBody.healthComponent.godMode = false;

            // Stop the effect //
            if (RoR2Application.isInMultiPlayer == true) 
                new ClientDestroyEffect(this.effect).Send(NetworkDestination.Clients);
            UnityEngine.Object.Destroy(Utils.FXManager.GetFX(this.effect));

            // Set the Cooldown //
            base.skillLocator.startCooldown(this.getSkillDef().skillID);

        }

        public void dash()
        {
            // Get the character direction //
            if (firstUse == true && characterBody.aimTimer > 0 && inputBank && characterDirection)
            {
                firstUse = false;
                characterBody.SetAimTimer(0);
                dashDirection = (inputBank.moveVector == Vector3.zero ? characterDirection.forward : inputBank.moveVector).normalized;
                characterDirection.forward = dashDirection;
            }
            // Set the velocity //
            dashDirection = characterDirection.forward;
            if (characterMotor.isGrounded == false)
            {
                dashDirection.y = GetAimRay().direction.y;
                characterDirection.forward = GetAimRay().direction;
            }
            float moveSpeed = base.moveSpeedStat * PantheraConfig.ClawsStorm_dashSpeedMultiplicator;
            moveSpeed = Math.Min(PantheraConfig.ClawsStorm_maxMoveSpeed, moveSpeed);
            moveSpeed = Math.Max(PantheraConfig.ClawsStorm_minMoveSpeed, moveSpeed);
            pantheraObj.pantheraMotor.startSprint = true;
            characterBody.characterMotor.velocity = dashDirection * moveSpeed;
        }

    }

}