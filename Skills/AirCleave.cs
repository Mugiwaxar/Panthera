using EntityStates;
using Panthera.Base;
using Panthera.BodyComponents;
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
        public bool isFireAirCleave = false;

        public AirCleave()
        {

        }

        public static void Create()
        {
            // Create the Skill //
            PantheraSkill skill = new PantheraSkill();
            skill.skillID = PantheraConfig.AirCleave_SkillID;
            skill.name = "AIR_CLEAVE_SKILL_NAME";
            skill.desc = "AIR_CLEAVE_SKILL_DESC";
            skill.icon = Assets.AirCleave;
            skill.iconPrefab = Assets.ActiveSkillPrefab;
            skill.type = PantheraSkill.SkillType.active;
            skill.associatedSkill = typeof(AirCleave);
            skill.priority = PantheraConfig.AirCleave_priority;
            skill.interruptPower = PantheraConfig.AirCleave_interruptPower;
            skill.cooldown = PantheraConfig.AirCleave_cooldown;
            skill.requiredEnergy = PantheraConfig.AirCleave_requiredEnergy;

            // Save this Skill //
            PantheraSkill.SkillDefsList.Add(skill.skillID, skill);
        }

        public override PantheraSkill getSkillDef()
        {
            return base.pantheraObj.activePreset.getSkillByID(PantheraConfig.AirCleave_SkillID);
        }

        public override bool CanBeUsed(PantheraObj ptraObj)
        {
            base.pantheraObj = ptraObj;
            if (ptraObj.characterBody.energy < this.getSkillDef().requiredEnergy) return false;
            if (ptraObj.skillLocator.getCooldownInSecond(this.getSkillDef().skillID) > 0) return false;
            return true;
        }

        public override void Start()
        {

            // Get the The Rip-per buff count //
            int ripperBuffCount = base.characterBody.GetBuffCount(Base.Buff.TheRipperBuff);

            // Check if Fire Air Cleave //
            if (base.pantheraObj.activePreset.getAbilityLevel(PantheraConfig.BurningSpiritAbilityID) > 0 && ripperBuffCount >= PantheraConfig.BurningSpirit_ripperStackNeeded)
            {
                this.isFireAirCleave = true;
            }

            // Create the projectile info //
            GameObject projectile = null;
            float damage = 0;
            if (base.pantheraObj.aircleaveComboNumber == 1)
            {
                projectile = Assets.AirCleaveLeftProjectile;
                if (this.isFireAirCleave) projectile = Assets.FireAirCleaveLeftProjectile;
                damage = PantheraConfig.AirCleave_atk1DamageMultiplier * base.damageStat;
            }
            else if (base.pantheraObj.aircleaveComboNumber == 2)
            {
                projectile = Assets.AirCleaveRightProjectile;
                if (this.isFireAirCleave) projectile = Assets.FireAirCleaveRightProjectile;
                damage = PantheraConfig.AirCleave_atk2DamageMultiplier * base.damageStat;
            }

            float projScale = base.pantheraObj.modelScale * PantheraConfig.AirCleave_projScale;
            projectile.transform.localScale = new Vector3(projScale, projScale, projScale);
            projectile.GetComponent<ProjectileController>().ghostPrefab.transform.localScale = new Vector3(projScale, projScale, projScale);
            this.projectileInfo.projectilePrefab = projectile;
            this.projectileInfo.damageTypeOverride = DamageType.Generic;
            this.projectileInfo.damageColorIndex = DamageColorIndex.Default;
            this.projectileInfo.crit = base.RollCrit();
            this.projectileInfo.force = PantheraConfig.AirCleave_projectileForce;
            this.projectileInfo.damage = damage;
            this.projectileInfo.speedOverride = PantheraConfig.AirCleave_projectileSpeed;
            this.projectileInfo.useSpeedOverride = true;
            this.projectileInfo.owner = base.gameObject;
            this.projectileInfo.position = base.characterBody.corePosition + (base.characterDirection.forward * base.pantheraObj.modelScale);
            this.projectileInfo.rotation = Util.QuaternionSafeLookRotation(base.GetAimRay().direction);

            // Set the character to forward //
            base.characterDirection.forward = base.GetAimRay().direction;

            // Save the time //
            base.skillLocator.startCooldown(this.getSkillDef().skillID);
            this.startTime = Time.time;

            // Unstealth //
            Passives.Stealth.DidDamageUnstealth(base.pantheraObj);

            // Get the duration //
            if (base.pantheraObj.aircleaveComboNumber == 1) this.baseDuration = PantheraConfig.AirCleave_atk1BaseDuration;
            else if (base.pantheraObj.aircleaveComboNumber == 2) this.baseDuration = PantheraConfig.AirCleave_atk2BaseDuration;

            // Set the attack //
            this.baseDuration = this.baseDuration / this.attackSpeedStat;
            base.StartAimMode(PantheraConfig.AirCleave_minimumAimTime + this.baseDuration, false);
            base.characterBody.outOfCombatStopwatch = 0f;
            base.pantheraObj.pantheraMotor.startSprint = false;

            // Remove the Energy //
            this.characterBody.energy -= this.getSkillDef().requiredEnergy;

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

                // Set as Fired //
                this.hasFired = true;

                // Combo 1 //
                if (base.pantheraObj.aircleaveComboNumber == 1)
                {
                    Sound.playSound(Utils.Sound.AirCleave1, base.gameObject);
                    if (this.isFireAirCleave == true) Sound.playSound(Utils.Sound.FireRip1, base.gameObject);
                    base.PlayAnimation("LeftRip", 0.2f);
                }
                // Combo 2 //
                else if (base.pantheraObj.aircleaveComboNumber == 2)
                {
                    Sound.playSound(Utils.Sound.AirCleave2, base.gameObject);
                    if (this.isFireAirCleave == true) Sound.playSound(Utils.Sound.FireRip1, base.gameObject);
                    base.PlayAnimation("RightRip", 0.2f);
                }

                // Fire projectiles //
                ProjectileManager.instance.FireProjectile(this.projectileInfo);

            }

        }

        public override void Stop()
        {
            // Change the combo number //
            if (base.pantheraObj.aircleaveComboNumber == 1)
                base.pantheraObj.aircleaveComboNumber = 2;
            else if (base.pantheraObj.aircleaveComboNumber == 2)
                base.pantheraObj.aircleaveComboNumber = 1;
        }

    }
}
