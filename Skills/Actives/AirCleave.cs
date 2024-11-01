using EntityStates;
using Panthera;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.GUI;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.OldSkills;
using Panthera.Skills;
using Panthera.Skills.Actives;
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

namespace Panthera.Skills.Actives
{
    class AirCleave : MachineScript
    {

        public int comboNumber = 1;
        public float damageMultiplier = PantheraConfig.AirCleave_attackDamageMultiplier;
        public FireProjectileInfo projectileInfo = new FireProjectileInfo();
        public bool projectilesFired = false;
        public float startTime;
        public float baseDuration = PantheraConfig.AirCleave_attackDuration;
        public float attackTime = PantheraConfig.AirCleave_attackTime;
        public bool hasFired = false;
        public bool isFireAirCleave = false;

        public AirCleave()
        {
            base.icon = PantheraAssets.AirCleaveSkill;
            base.name = PantheraTokens.Get("skill_AirCleaveName");
            base.baseCooldown = PantheraConfig.AirCleave_cooldown;
            base.desc1 = string.Format(PantheraTokens.Get("skill_AirCleaveDesc"), PantheraConfig.AirCleave_attackDamageMultiplier * 100) + string.Format(PantheraTokens.Get("skill_AirCleaveFuryDesc"), PantheraConfig.AirCleave_furyAdded);
            base.desc2 = null;
            base.skillID = PantheraConfig.AirCleave_SkillID;
            base.priority = PantheraConfig.AirCleave_priority;
            base.interruptPower = PantheraConfig.AirCleave_interruptPower;
        }

        public override bool CanBeUsed(PantheraObj ptraObj)
        {
            //if (ptraObj.skillLocator.getStock(PantheraConfig.AirCleave_SkillID) <= 0) return false;
            return true;
        }

        public override void Start()
        {

            // Check if Fire Air Cleave //
            if (base.pantheraObj.furyMode == true && base.pantheraObj.GetAbilityLevel(PantheraConfig.HeatWave_AbilityID) > 0)
                this.isFireAirCleave = true;

            // Get the Combo Number //
            this.comboNumber = base.pantheraObj.AttackNumber;

            // Get the Sharpened Fangs multiplicator //
            float sharpenedFangsMult = 1;
            if (base.getAbilityLevel(PantheraConfig.SharpenedFangs_AbilityID) == 1)
                sharpenedFangsMult += PantheraConfig.SharpenedFangs_damagePercent1;
            else if (base.getAbilityLevel(PantheraConfig.SharpenedFangs_AbilityID) == 2)
                sharpenedFangsMult += PantheraConfig.SharpenedFangs_damagePercent2;
            else if (base.getAbilityLevel(PantheraConfig.SharpenedFangs_AbilityID) == 3)
                sharpenedFangsMult += PantheraConfig.SharpenedFangs_damagePercent3;

            // Create the projectile info //
            GameObject projectile = PantheraAssets.AirCleaveLeftProjectile;
            if (this.comboNumber == 1)
            {
                projectile = PantheraAssets.AirCleaveLeftProjectile;
                if (this.isFireAirCleave) projectile = PantheraAssets.FireAirCleaveLeftProjectile;
            }
            else if (this.comboNumber == 2)
            {
                projectile = PantheraAssets.AirCleaveRightProjectile;
                if (this.isFireAirCleave) projectile = PantheraAssets.FireAirCleaveRightProjectile;
            }
            float damage = base.characterBody.damage * this.damageMultiplier * sharpenedFangsMult;
            float projScale = base.pantheraObj.modelScale * PantheraConfig.AirCleave_projScale;
            projectile.transform.localScale = new Vector3(projScale, projScale, projScale);
            projectile.GetComponent<ProjectileController>().ghostPrefab.transform.localScale = new Vector3(projScale, projScale, projScale);
            this.projectileInfo.projectilePrefab = projectile;
            this.projectileInfo.damageTypeOverride = DamageType.Generic;
            this.projectileInfo.damageColorIndex = DamageColorIndex.Default;
            this.projectileInfo.crit = RollCrit();
            this.projectileInfo.force = PantheraConfig.AirCleave_projectileForce;
            this.projectileInfo.damage = damage;
            this.projectileInfo.speedOverride = PantheraConfig.AirCleave_projectileSpeed;
            this.projectileInfo.useSpeedOverride = true;
            this.projectileInfo.owner = base.gameObject;
            this.projectileInfo.position = base.GetAimRay().origin;
            this.projectileInfo.rotation = Util.QuaternionSafeLookRotation(base.GetAimRay().direction);

            // Set the character to forward //
            base.characterDirection.forward = base.GetAimRay().direction;

            // Save the time //
            //base.skillLocator.startCooldown(PantheraConfig.AirCleave_SkillID);
            this.startTime = Time.time;

            // Set the Fake Skill Cooldown //
            //base.skillLocator.secondary.DeductStock(1);

            // Set the attack //
            this.baseDuration = this.baseDuration / base.attackSpeedStat;
            this.attackTime = this.attackTime / base.attackSpeedStat;
            base.StartAimMode(PantheraConfig.AirCleave_minimumAimTime + this.baseDuration, false);
            base.characterBody.outOfCombatStopwatch = 0f;
            base.pantheraObj.pantheraMotor.isSprinting = false;

            // Play the Animation //            
            if (this.comboNumber == 1)
            {
                Sound.playSound(Sound.AirCleave1, base.gameObject);
                if (this.isFireAirCleave == true) Sound.playSound(Sound.FireRip1, gameObject);
                PlayAnimation("LeftRip", 0.2f);
            }
            // Combo 2 //
            else
            {
                Sound.playSound(Sound.AirCleave2, base.gameObject);
                if (this.isFireAirCleave == true) Sound.playSound(Sound.FireRip1, gameObject);
                PlayAnimation("RightRip", 0.2f);
            }

        }

        public override void Update()
        {

        }

        public override void FixedUpdate()
        {


            // Stop if the duration is reached //
            float skillDuration = Time.time - this.startTime;
            if (skillDuration >= this.baseDuration && this.hasFired == true)
            {
                EndScript();
                return;
            }


            // Check if the attack has already fired //
            if (this.hasFired == false && skillDuration >= this.attackTime)
            {

                // Set as Fired //
                this.hasFired = true;

                // Fire projectiles //
                ProjectileManager.instance.FireProjectile(this.projectileInfo);

            }

        }

        public override void Stop()
        {

        }

    }
}
