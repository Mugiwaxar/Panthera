using EntityStates;
using HarmonyLib;
using Panthera;
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

        public static PantheraSkill SkillDef;

        public Vector3 dashDirection;
        public float moveSpeed;
        public bool firstUse = true;
        public float lastFired = 0;
        public float lastSoundPlayed = 0;
        public float lastFuryConsumed = 0;
        public int previousPlayerLayer;
        public OverlapAttack attack;
        public GameObject effect;

        public ClawsStorm()
        {

        }

        public static void Create()
        {
            // Create the Skill //
            PantheraSkill skill = new PantheraSkill();
            skill.skillID = PantheraConfig.ClawsStorm_SkillID;
            skill.name = Tokens.ClawStormSkillName;
            skill.desc = Tokens.ClawStormSkillDesc;
            skill.icon = Assets.ClawStorm;
            skill.iconPrefab = ConfigPanel.ActiveSkillPrefab;
            skill.type = PantheraSkill.SkillType.active;
            skill.associatedSkill = typeof(ClawsStorm);
            skill.priority = PantheraConfig.ClawsStorm_priority;
            skill.interruptPower = PantheraConfig.ClawsStorm_interruptPower;
            skill.cooldown = PantheraConfig.ClawsStorm_cooldown;
            skill.requiredFury = PantheraConfig.ClawsStorm_requiredFury;

            // Save this Skill //
            SkillDef = skill;
            PantheraSkill.SkillDefsList.Add(skill.skillID, skill);
        }

        public override PantheraSkill getSkillDef()
        {
            return SkillDef;
        }

        public override bool CanBeUsed(PantheraObj ptraObj)
        {
            if (ptraObj.characterBody.fury < SkillDef.requiredFury) return false;
            if (Time.time - PantheraSkill.GetCooldownTime(SkillDef.skillID) < SkillDef.cooldown) return false;
            return true;
        }

        public override void Start()
        {

            // Create the attack //
            attack = Functions.CreateOverlapAttack(gameObject, PantheraConfig.ClawsStorm_damageMultiplier, PantheraConfig.ClawsStorm_hitboxName);

            // Set in combat //
            characterBody.outOfCombatStopwatch = 0f;

            // Set the camera to ClawsStorm //
            CamHelper.applyAimType(CamHelper.AimType.ClawsStorm);

            // Start the effect //
            effect = Functions.SpawnEffect(
                    gameObject,
                    Assets.ClawsStormFX,
                    modelTransform.position,
                    1,
                    modelTransform.gameObject
                    );

            // Change the Player layer //
            previousPlayerLayer = gameObject.layer;
            gameObject.layer = LayerIndex.fakeActor.intVal;
            characterMotor.Motor.RebuildCollidableLayers();

            // Do the dash //
            Dash();

            // Set god mod //
            //characterBody.healthComponent.godMode = true;

        }

        public override void Update()
        {

        }

        public override void FixedUpdate()
        {

            if (this.characterBody.fury < PantheraConfig.ClawsStorm_continuousConsumedFury || base.wasInterrupted == true || base.inputBank.isSkillPressed(SkillDef.skillID) == false)
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

            // Play the Sound //
            float lastSoundTime = Time.time - this.lastSoundPlayed;
            if (lastSoundTime >= PantheraConfig.ClawsStorm_playSoundTime)
            {
                Sound.playSound(Sound.ClawsStorm, gameObject);
                this.lastSoundPlayed = Time.time;
            }

            // Fire the attack //
            float lastFiredTime = Time.time - this.lastFired;
            if (lastFiredTime >= PantheraConfig.ClawsStorm_firedDelay)
            {
                attack.Fire();
                attack = Functions.CreateOverlapAttack(gameObject, PantheraConfig.ClawsStorm_damageMultiplier, PantheraConfig.ClawsStorm_hitboxName);
                lastFired = Time.time;
            }

            // Grab the Enemies //
            Collider[] colliders = Physics.OverlapSphere(base.characterBody.corePosition + (base.characterDirection.forward*PantheraConfig.ClawsStorm_grabDistanceMultiplier), PantheraConfig.ClawsStorm_grabScanRadius, LayerIndex.entityPrecise.mask.value);
            foreach (Collider collider in colliders)
            {
                HurtBox hurtbox = collider.GetComponent<HurtBox>();
                TeamComponent tc = hurtbox.healthComponent?.body?.teamComponent;
                if (tc != null && tc.teamIndex == TeamIndex.Monster)
                {
                    GameObject obj = tc.body.gameObject;
                    if (obj.GetComponent<HoldTarget>() == null)
                    {
                        HoldTarget comp = obj.AddComponent<HoldTarget>();
                        float relativeDistance = Vector3.Distance(obj.transform.position, base.characterBody.corePosition);
                        comp.skillScript = this;
                        comp.ptraObj = base.pantheraObj;
                        comp.relativeDistance = relativeDistance;
                    }
                }
            }

            // Do the Dash //
            Dash();

        }

        public override void Stop()
        {

            // Restore the camera //
            CamHelper.applyAimType(CamHelper.AimType.Standard);

            // Restore the Player layer //
            gameObject.layer = previousPlayerLayer;
            characterMotor.Motor.RebuildCollidableLayers();

            // Disable god mode //
            //characterBody.healthComponent.godMode = false;

            // Stop the effect //
            UnityEngine.Object.DestroyImmediate(effect);

        }

        public void Dash()
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
            moveSpeed = Math.Min(PantheraConfig.ClawsStorm_maxMoveSpeed, moveSpeedStat);
            moveSpeed = Math.Max(PantheraConfig.ClawsStorm_minMoveSpeed, moveSpeed);
            pantheraObj.pantheraMotor.startSprint = true;
            characterBody.characterMotor.velocity = dashDirection * moveSpeed * PantheraConfig.ClawsStorm_dashSpeedMultiplicator;
        }

        public void CreateOverlapAttack()
        {
            // Get the HitBox //
            HitBoxGroup hitBoxGroup = null;
            Transform modelTransform = base.modelTransform;
            if (modelTransform)
            {
                hitBoxGroup = Array.Find(modelTransform.GetComponents<HitBoxGroup>(), (element) => element.groupName == PantheraConfig.ClawsStorm_hitboxName);
            }

            // Create the Attack //
            attack = new OverlapAttack();
            attack.damageType = PantheraConfig.ClawsStorm_damageType;
            attack.attacker = gameObject;
            attack.inflictor = gameObject;
            attack.teamIndex = GetTeam();
            attack.damage = PantheraConfig.ClawsStorm_damageMultiplier * damageStat;
            attack.procCoefficient = PantheraConfig.ClawsStorm_procCoefficient;
            //this.attack.hitEffectPrefab = this.hitEffectPrefab;
            attack.forceVector = PantheraConfig.ClawsStorm_bonusForce;
            attack.pushAwayForce = PantheraConfig.ClawsStorm_pushForce;
            attack.hitBoxGroup = hitBoxGroup;
            attack.isCrit = RollCrit();
            //this.attack.impactSound = this.impactSound;

        }

    }

}