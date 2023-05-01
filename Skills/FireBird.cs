using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.Utils;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using RoR2.Projectile;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Skills
{
    internal class FireBird : MachineScript
    {

        public FireProjectileInfo projectileInfo;
        public float startTime;
        public bool hasFired = false;

        public FireBird()
        {

        }

        public static void Create()
        {
            // Create the Skill //
            PantheraSkill skill = new PantheraSkill();
            skill.skillID = PantheraConfig.FireBird_SkillID;
            skill.name = "FIRE_BIRD_SKILL_NAME";
            skill.desc = "FIRE_BIRD_SKILL_DESC";
            skill.icon = Assets.FireBird;
            skill.iconPrefab = Assets.ActiveSkillPrefab;
            skill.type = PantheraSkill.SkillType.active;
            skill.associatedSkill = typeof(FireBird);
            skill.priority = PantheraConfig.FireBird_priority;
            skill.interruptPower = PantheraConfig.FireBird_interruptPower;
            skill.cooldown = PantheraConfig.FireBird_Cooldown;
            skill.requiredFury = PantheraConfig.FireBird_furyRequired;

            // Save this Skill //
            PantheraSkill.SkillDefsList.Add(skill.skillID, skill);
        }

        public override PantheraSkill getSkillDef()
        {
            return base.pantheraObj.activePreset.getSkillByID(PantheraConfig.FireBird_SkillID);
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

            // Set the Cooldown //
            base.skillLocator.startCooldown(this.getSkillDef().skillID);

            // Remove the Power //
            base.characterBody.fury -= this.getSkillDef().requiredFury;

            // Save the time //
            this.startTime = Time.time;

            // Unstealth //
            Passives.Stealth.DidDamageUnstealth(base.pantheraObj);

            // Set in combat //
            characterBody.outOfCombatStopwatch = 0f;

            // Stop the Sprint //
            base.pantheraObj.pantheraMotor.startSprint = false;

            // Set the character to forward //
            base.characterDirection.forward = base.GetAimRay().direction;

            // Create the projectile info //
            this.projectileInfo.projectilePrefab = Base.Assets.FireBirdProjectile;
            this.projectileInfo.damageTypeOverride = DamageType.Generic;
            this.projectileInfo.damageColorIndex = DamageColorIndex.Default;
            this.projectileInfo.crit = base.RollCrit();
            this.projectileInfo.force = PantheraConfig.AirCleave_projectileForce;
            this.projectileInfo.damage = PantheraConfig.FireBird_damageMultiplier * base.damageStat;
            this.projectileInfo.speedOverride = PantheraConfig.FireBird_projectileSpeed;
            this.projectileInfo.useSpeedOverride = true;
            this.projectileInfo.owner = base.gameObject;
            this.projectileInfo.position = base.characterBody.corePosition + (base.characterDirection.forward * base.pantheraObj.modelScale);
            this.projectileInfo.rotation = Util.QuaternionSafeLookRotation(base.GetAimRay().direction);

        }

        public override void Update()
        {

        }

        public override void FixedUpdate()
        {

            // Stop if the duration is reached //
            float skillDuration = Time.time - this.startTime;
            if (skillDuration >= PantheraConfig.FireBird_duration)
            {
                base.EndScript();
                return;
            }

            // Check if the attack has already fired //
            if (this.hasFired == false)
            {

                // Fire projectiles //
                ProjectileManager.instance.FireProjectile(this.projectileInfo);
                this.hasFired = true;

                // Play the Sound //
                Sound.playSound(Utils.Sound.FireBird, base.gameObject);

                // Play the Animation //
                base.PlayAnimation("Roar");

            }

        }

        public override void Stop()
        {

        }

    }
}
