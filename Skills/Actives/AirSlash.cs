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
    class AirSlash : MachineScript
    {

        public int comboNumber = 1;
        public float damageMultiplier = PantheraConfig.AirSlash_atkDamageMultiplier;
        public bool projectilesFired = false;
        public float startTime;
        public float baseDuration = PantheraConfig.AirSlash_attackDuration;
        public float attackTime = PantheraConfig.AirSlash_attackTime;
        public bool hasFired = false;

        public AirSlash()
        {
            base.icon = PantheraAssets.AirSlashSkill;
            base.name = PantheraTokens.Get("ability_AirSlashName");
            base.baseCooldown = PantheraConfig.AirSlash_cooldown;
            base.showCooldown = true;
            base.desc1 = String.Format(Utils.PantheraTokens.Get("ability_AirSlashDesc"), PantheraConfig.AirSlash_atkDamageMultiplier * 100) + String.Format(Utils.PantheraTokens.Get("Ability_AirSlashFuryDesc"), PantheraConfig.AirSlash_furyAdded);
            base.desc2 = null;
            base.skillID = PantheraConfig.AirSlash_SkillID;
            base.requiredAbilityID = PantheraConfig.AirSlash_AbilityID;
            base.priority = PantheraConfig.AirSlash_priority;
            base.interruptPower = PantheraConfig.AirSlash_interruptPower;
            base.comboMaxTime = PantheraConfig.AirSlash_comboMaxTime;
        }

        public override bool CanBeUsed(PantheraObj ptraObj)
        {
            if (ptraObj.skillLocator.getStock(PantheraConfig.AirSlash_SkillID) <= 0) return false;
            return true;
        }

        public override void Start()
        {

            // Get the Combo Number //
            this.comboNumber = base.pantheraObj.attackNumber;

            // Set the character to forward //
            base.characterDirection.forward = GetAimRay().direction;

            // Unhide the Crosshair //
            base.pantheraObj.crosshairComp.unHideCrosshair();

            // Save the time //
            this.startTime = Time.time;

            // Set the attack //
            this.baseDuration = this.baseDuration / base.attackSpeedStat;
            this.attackTime = this.attackTime / base.attackSpeedStat;
            StartAimMode(PantheraConfig.AirSlash_minimumAimTime + this.baseDuration, false);
            base.pantheraObj.pantheraMotor.isSprinting = false;
        }

        public override void Update()
        {

        }

        public override void FixedUpdate()
        {

            // Get the Skill duration //
            float skillDuration = Time.time - this.startTime;

            // Check if the attack has already fired //
            if (this.hasFired == false && skillDuration >= this.attackTime)
            {

                // Set as Fired //
                this.hasFired = true;

                // Fire the Projectile //
                this.Fire();

            }

            // Stop if the duration is reached //
            if (skillDuration >= this.baseDuration && this.hasFired == true)
            {
                EndScript();
                return;
            }

        }

        public void Fire()
        {

            // Get the Combo //
            string animation = "LeftSlash";
            GameObject FX = PantheraAssets.AirSlashStartLeftFX;
            if (this.comboNumber == 2)
            {
                animation = "RightSlash";
                FX = PantheraAssets.AirSlashStartRightFX;
            }

            // Play the Sound //
            Sound.playSound(Sound.AirSlash, base.gameObject);

            // Spawn the Effect //
            FXManager.SpawnEffect(base.gameObject, FX, base.GetAimRay().origin, base.pantheraObj.modelScale, base.characterBody.gameObject, Util.QuaternionSafeLookRotation(base.GetAimRay().direction));

            // Play the Animation //
            base.PlayAnimation(animation);

            // Create the projectile info //
            GameObject projectile = PantheraAssets.AirSlashProjectile.InstantiateClone("AirSlashLoop(ProjectileClone)");
            float damage = base.characterBody.damage * this.damageMultiplier;
            Vector3 scale = projectile.transform.localScale * PantheraConfig.AirSlash_projScale * base.pantheraObj.modelScale;
            projectile.transform.localScale = scale;
            projectile.GetComponent<ProjectileController>().ghostPrefab.transform.localScale = scale;
            FireProjectileInfo projectileInfo = new FireProjectileInfo();
            projectileInfo.projectilePrefab = projectile;
            projectileInfo.damageTypeOverride = DamageType.Generic;
            projectileInfo.damageColorIndex = DamageColorIndex.Default;
            projectileInfo.crit = RollCrit();
            projectileInfo.force = PantheraConfig.AirSlash_projectileForce;
            projectileInfo.damage = damage;
            projectileInfo.speedOverride = PantheraConfig.AirSlash_projectileSpeed;
            projectileInfo.useSpeedOverride = true;
            projectileInfo.owner = base.gameObject;
            projectileInfo.position = base.GetAimRay().origin;
            projectileInfo.rotation = Util.QuaternionSafeLookRotation(base.GetAimRay().direction);

            // Fire projectiles //
            ProjectileManager.instance.FireProjectile(projectileInfo);

        }

        public override void Stop()
        {
            // Hide the Crosshair //
            base.pantheraObj.crosshairComp.delayHideCrosshair(PantheraConfig.AirSlash_hideCrosshairTime);
            // Start the Cooldown //
            base.skillLocator.startCooldown(PantheraConfig.AirSlash_SkillID);
        }

    }
}
