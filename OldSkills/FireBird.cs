using Panthera;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.OldSkills;
using Panthera.Skills.Passives;
using Panthera.Utils;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using RoR2.Projectile;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.OldSkills
{
    public class FireBird : MachineScript
    {

        //public FireProjectileInfo projectileInfo;
        //public float startTime;
        //public bool hasFired = false;

        //public FireBird()
        //{

        //}

        //public static void Create()
        //{
        //    // Create the Skill //
        //    PantheraSkill skill = new PantheraSkill();
        //    skill.skillID = PantheraConfig.FireBird_SkillID;
        //    skill.name = "FIRE_BIRD_SKILL_NAME";
        //    skill.desc = "FIRE_BIRD_SKILL_DESC";
        //    skill.icon = PantheraAssets.FireBird;
        //    skill.iconPrefab = PantheraAssets.ActiveSkillPrefab;
        //    skill.type = PantheraSkill.SkillType.active;
        //    skill.associatedSkill = typeof(FireBird);
        //    skill.priority = PantheraConfig.FireBird_priority;
        //    skill.interruptPower = PantheraConfig.FireBird_interruptPower;
        //    skill.cooldown = PantheraConfig.FireBird_Cooldown;
        //    skill.requiredFury = PantheraConfig.FireBird_furyRequired;

        //    // Save this Skill //
        //    PantheraSkill.SkillDefsList.Add(skill.skillID, skill);
        //}

        ////public override PantheraSkill getSkillDef()
        ////{
        ////    return base.pantheraObj.activePreset.getSkillByID(PantheraConfig.FireBird_SkillID);
        ////}

        //public override bool CanBeUsed(PantheraObj ptraObj)
        //{
        //    pantheraObj = ptraObj;
        //    if (ptraObj.characterBody.fury < getSkillDef().requiredFury) return false;
        //    if (ptraObj.skillLocator.getCooldown(getSkillDef().skillID) > 0) return false;
        //    return true;
        //}

        //public override void Start()
        //{

        //    // Set the Cooldown //
        //    skillLocator.startCooldown(getSkillDef().skillID);

        //    // Remove the Power //
        //    characterBody.fury -= getSkillDef().requiredFury;

        //    // Save the time //
        //    startTime = Time.time;

        //    // Unstealth //
        //    //Skills.Passives.Stealth.DidDamageUnstealth(pantheraObj);

        //    // Set in combat //
        //    characterBody.outOfCombatStopwatch = 0f;

        //    // Stop the Sprint //
        //    pantheraObj.pantheraMotor.isSprinting = false;

        //    // Set the character to forward //
        //    characterDirection.forward = GetAimRay().direction;

        //    // Create the projectile info //
        //    projectileInfo.projectilePrefab = PantheraAssets.FireBirdProjectile;
        //    projectileInfo.damageTypeOverride = DamageType.Generic;
        //    projectileInfo.damageColorIndex = DamageColorIndex.Default;
        //    projectileInfo.crit = RollCrit();
        //    projectileInfo.force = PantheraConfig.AirCleave_projectileForce;
        //    projectileInfo.damage = PantheraConfig.FireBird_damageMultiplier * damageStat;
        //    projectileInfo.speedOverride = PantheraConfig.FireBird_projectileSpeed;
        //    projectileInfo.useSpeedOverride = true;
        //    projectileInfo.owner = gameObject;
        //    projectileInfo.position = characterBody.corePosition + characterDirection.forward * pantheraObj.modelScale;
        //    projectileInfo.rotation = Util.QuaternionSafeLookRotation(GetAimRay().direction);

        //}

        //public override void Update()
        //{

        //}

        //public override void FixedUpdate()
        //{

        //    // Stop if the duration is reached //
        //    float skillDuration = Time.time - startTime;
        //    if (skillDuration >= PantheraConfig.FireBird_duration)
        //    {
        //        EndScript();
        //        return;
        //    }

        //    // Check if the attack has already fired //
        //    if (hasFired == false)
        //    {

        //        // Fire projectiles //
        //        ProjectileManager.instance.FireProjectile(projectileInfo);
        //        hasFired = true;

        //        // Play the Sound //
        //        Sound.playSound(Sound.FireBird, gameObject);

        //        // Play the Animation //
        //        PlayAnimation("Roar");

        //    }

        //}

        //public override void Stop()
        //{

        //}

    }
}
