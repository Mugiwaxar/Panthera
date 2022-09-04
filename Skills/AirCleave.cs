using EntityStates;
using Panthera.Components;
using Panthera.GUI;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.Utils;
using R2API;
using R2API.Networking;
using R2API.Networking.Interfaces;
using Rewired;
using RoR2;
using RoR2.Audio;
using RoR2.Projectile;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Panthera.Skills
{
    class AirCleave : MachineScript
    {

        //public static float aimVelocity = 0.3f;
        //public static float maxRushDistance = 2;
        //public static float rushDistanceStop = 1;

        public static GameObject projectilePrefab1 = Assets.LeftRipProjectile;
        public static GameObject projectilePrefab2 = Assets.RightRipProjectile;
        public static int comboNumber = 1;
        public static PantheraSkill SkillDef;

        public OverlapAttack attack;
        public float damageMultiplier;
        public string swingSoundString = "";
        public string hitSoundString = "";
        public string muzzleString = "SwingCenter";
        public CharacterBody target;
        public FireProjectileInfo projectileInfo;
        public bool projectilesFired = false;
        public float startTime;
        public float baseDuration;
        public bool hasFired = false;

        public AirCleave()
        {

        }

        public static void Create()
        {
            // Create the Skill //
            PantheraSkill skill = new PantheraSkill();
            skill.skillID = PantheraConfig.AirCleave_SkillID;
            skill.name = Tokens.AirCleaveSkillName;
            skill.desc = Tokens.AirCleaveSkillDesc;
            skill.icon = Assets.AirCleave;
            skill.iconPrefab = ConfigPanel.ActiveSkillPrefab;
            skill.type = PantheraSkill.SkillType.active;
            skill.associatedSkill = typeof(AirCleave);
            skill.priority = PantheraConfig.AirCleave_priority;
            skill.interruptPower = PantheraConfig.AirCleave_interruptPower;
            skill.cooldown = PantheraConfig.AirCleave_cooldown;
            skill.requiredEnergy = PantheraConfig.AirCleave_requiredEnergy;

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
            if (ptraObj.characterBody.energy < SkillDef.requiredEnergy) return false;
            if (Time.time - PantheraSkill.GetCooldownTime(SkillDef.skillID) < SkillDef.cooldown) return false;
            return true;
        }

        public override void Start()
        {

            // Create the projectile info //
            GameObject projectile = null;
            float damage = 0;
            if (comboNumber == 1)
            {
                projectile = AirCleave.projectilePrefab1;
                damage = PantheraConfig.AirCleave_atk1DamageMultiplier * base.damageStat;
            }
            else if (comboNumber == 2)
            {
                projectile = AirCleave.projectilePrefab2;
                damage = PantheraConfig.AirCleave_atk2DamageMultiplier * base.damageStat;
            }
            this.projectileInfo.projectilePrefab = projectile;
            this.projectileInfo.damageTypeOverride = DamageType.Generic;
            this.projectileInfo.damageColorIndex = DamageColorIndex.Default;
            this.projectileInfo.crit = base.RollCrit();
            this.projectileInfo.force = PantheraConfig.AirCleave_projectileForce;
            this.projectileInfo.damage = damage;
            this.projectileInfo.speedOverride = PantheraConfig.AirCleave_projectileSpeed;
            this.projectileInfo.useSpeedOverride = true;
            this.projectileInfo.owner = base.gameObject;
            this.projectileInfo.position = base.characterBody.corePosition + (Vector3.up * PantheraConfig.Model_generalScale);
            this.projectileInfo.rotation = Util.QuaternionSafeLookRotation(base.GetAimRay().direction);

            // Set the character to forward //
            base.characterDirection.forward = base.GetAimRay().direction;

            // Save the time //
            PantheraSkill.SetCooldownTime(SkillDef.skillID, Time.time);
            this.startTime = Time.time;

            // Get the duration //
            if (comboNumber == 1) this.baseDuration = PantheraConfig.AirCleave_atk1BaseDuration;
            else if (comboNumber == 2) this.baseDuration = PantheraConfig.AirCleave_atk2BaseDuration;

            // Set the attack //
            this.baseDuration = this.baseDuration / this.attackSpeedStat;
            base.StartAimMode(PantheraConfig.AirCleave_minimumAimTime + this.baseDuration, false);
            base.characterBody.outOfCombatStopwatch = 0f;
            base.pantheraObj.pantheraMotor.startSprint = false;

            // Remove the Energy //
            this.characterBody.energy -= SkillDef.requiredEnergy;

        }

        public override void Update()
        {
            
        }

        public override void FixedUpdate()
        {


            // Stop if the duration is reached //
            float skillDuration = Time.time - this.startTime;
            if (skillDuration >= this.baseDuration)
            {
                base.EndScript();
                return;
            }


            // Check if the attack has already fired //
            if (this.hasFired == false)
            {

                this.hasFired = true;

                // Combo 1 //
                if (comboNumber == 1)
                {
                    Sound.playSound(Utils.Sound.AirCleave1, base.gameObject);
                    base.PlayAnimation("Gesture", "LeftRip");
                }
                // Combo 2 //
                else if (comboNumber == 2)
                {
                    Sound.playSound(Utils.Sound.AirCleave2, base.gameObject);
                    base.PlayAnimation("Gesture", "RightRip");
                }

                // Fire projectiles //
                ProjectileManager.instance.FireProjectile(this.projectileInfo);
                base.skillLocator.secondary.stock -= 1;

            }

        }

        public override void Stop()
        {
            // Change the combo number //
            if (comboNumber == 1)
                comboNumber = 2;
            else if (comboNumber == 2)
                comboNumber = 1;
        }

    }
}
