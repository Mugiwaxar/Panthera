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
using Panthera.Skills.Passives;
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

namespace Panthera.OldSkills
{
    class ChargedAirSlashSave : MachineScript
    {

        //public int comboNumber = 1;
        //public float damageMultiplier = PantheraConfig.AirSlash_minAtkDamageMultiplier;
        //public bool projectilesFired = false;
        //public float startTime;
        //public float baseDuration = PantheraConfig.AirSlash_attackDuration;
        //public float attackTime = PantheraConfig.AirSlash_attackTime;
        //public float chargePercent;
        //public int effectID;
        //public GameObject chargeEffect;
        //public bool hasFired = false;

        //public AirSlashSave()
        //{
        //    icon = PantheraAssets.AirSlashSkill;
        //    name = PantheraTokens.Get("ability_AirSlashName");
        //    baseCooldown = PantheraConfig.AirSlash_cooldown;
        //    showCooldown = true;
        //    desc1 = string.Format(PantheraTokens.Get("ability_AirSlashDesc"), PantheraConfig.AirSlash_minAtkDamageMultiplier * 100) + string.Format(PantheraTokens.Get("Ability_AirSlashFuryDesc"), PantheraConfig.AirSlash_furyAdded);
        //    desc2 = null;
        //    skillID = PantheraConfig.AirSlash_SkillID;
        //    requiredAbilityID = PantheraConfig.AirSlash_AbilityID;
        //    priority = PantheraConfig.AirSlash_priority;
        //    interruptPower = PantheraConfig.AirSlash_interruptPower;
        //}

        //public override bool CanBeUsed(PantheraObj ptraObj)
        //{
        //    if (ptraObj.skillLocator.getStock(PantheraConfig.AirSlash_SkillID) <= 0) return false;
        //    return true;
        //}

        //public override void Start()
        //{

        //    // Get the Combo Number //
        //    comboNumber = pantheraObj.attackNumber;

        //    // Set the character to forward //
        //    characterDirection.forward = GetAimRay().direction;

        //    // Unhide the Crosshair //
        //    pantheraObj.crosshairComp.unHideCrosshair();

        //    // Save the time //
        //    startTime = Time.time;

        //    // Play the cast Sound //
        //    Sound.playSound(Sound.AirSlashCastLoop, gameObject);

        //    // Set the attack //
        //    baseDuration = baseDuration / attackSpeedStat;
        //    attackTime = attackTime / attackSpeedStat;
        //    StartAimMode(PantheraConfig.AirSlash_minimumAimTime + baseDuration, false);
        //    characterBody.outOfCombatStopwatch = 0f;
        //    pantheraObj.pantheraMotor.isSprinting = false;

        //    // Get the starting scale //
        //    float startingScale = pantheraObj.modelScale * PantheraConfig.AirSlash_minProjScale;

        //    // Spawn the Effect //
        //    effectID = FXManager.SpawnEffect(gameObject, PantheraAssets.AirSlashCastLoopFX, characterBody.corePosition, 1, characterBody.gameObject, modelTransform.rotation, true);
        //    chargeEffect = FXManager.GetEffect(effectID);
        //    chargeEffect.transform.localScale = new Vector3(startingScale, startingScale, startingScale);

        //}

        //public override void Update()
        //{

        //}

        //public override void FixedUpdate()
        //{

        //    // Get the Skill duration //
        //    float skillDuration = Time.time - startTime;

        //    // Calculate the charge percent //
        //    //float chargeTime = Math.Min(PantheraConfig.AirSlash_maxChargeTime, skillDuration);
        //    chargePercent = Mathf.Min(1, Mathf.Log10(1 + skillDuration / 1.5f));

        //    // Start the Aim Mode //
        //    StartAimMode(PantheraConfig.AirSlash_aimModeTime, false);

        //    // Check if the Effect still exist //
        //    if (chargeEffect != null)
        //    {
        //        // Change the FX scale //
        //        float scale = (PantheraConfig.AirSlash_maxProjScale - PantheraConfig.AirSlash_minProjScale) * chargePercent + PantheraConfig.AirSlash_minProjScale;
        //        chargeEffect.transform.localScale = new Vector3(scale, scale, scale);
        //        // Change the FX Rotation //
        //        chargeEffect.transform.rotation = Util.QuaternionSafeLookRotation(base.GetAimRay().direction);
        //    }

        //    // Set the Crosshair //
        //    pantheraObj.crosshairComp.loadingCrosshairLevel = chargePercent;

        //    // Set the Sound //
        //    AkSoundEngine.SetRTPCValue("AirSlashCastLoopPitch", chargePercent * 100, gameObject);

        //    // Check if the Button is still pressed //
        //    if (inputBank.keysPressedList.Contains(KeysBinder.KeysEnum.Skill2))
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        // Check if the attack has already fired //
        //        if (hasFired == false && skillDuration >= attackTime)
        //        {

        //            // Set as Fired //
        //            hasFired = true;

        //            // Fire the Projectile //
        //            Fire();

        //        }
        //    }

        //    // Stop if the duration is reached //
        //    if (skillDuration >= baseDuration && hasFired == true)
        //    {
        //        EndScript();
        //        return;
        //    }

        //}

        //public void Fire()
        //{

        //    // Save the Effect values //
        //    Vector3 position = chargeEffect.transform.position;
        //    Vector3 scale = chargeEffect.transform.localScale;
        //    Quaternion rotation = chargeEffect.transform.rotation;

        //    // Destroy the Effect //
        //    FXManager.DestroyEffect(effectID);

        //    // Get the Combo //
        //    string animation = "LeftSlash";
        //    GameObject FX = PantheraAssets.AirSlashStartLeftFX;
        //    if (comboNumber == 2)
        //    {
        //        animation = "RightSlash";
        //        FX = PantheraAssets.AirSlashStartRightFX;
        //    }

        //    // Play the Sound //
        //    Sound.playSound(Sound.AirSlash, gameObject);

        //    // Spawn the Effect //
        //    FXManager.SpawnEffect(gameObject, FX, characterBody.corePosition, pantheraObj.modelScale, characterBody.gameObject, rotation, true);

        //    // Play the Animation //
        //    PlayAnimation(animation);

        //    // Calcule the Damage Multiplier //
        //    damageMultiplier = (PantheraConfig.AirSlash_maxAtkDamageMultiplier - PantheraConfig.AirSlash_minAtkDamageMultiplier) * chargePercent + PantheraConfig.AirSlash_minAtkDamageMultiplier;

        //    // Calcule the Speed Multiplier //
        //    float projectilSpeed = (PantheraConfig.AirSlash_maxProjectileSpeed - PantheraConfig.AirSlash_minProjectileSpeed) * chargePercent + PantheraConfig.AirSlash_minProjectileSpeed;

        //    // Create the projectile info //
        //    GameObject projectile = PantheraAssets.AirSlashProjectile.InstantiateClone("AirSlashLoop(ProjectileClone)");
        //    float damage = characterBody.damage * damageMultiplier;
        //    projectile.transform.localScale = scale;
        //    projectile.GetComponent<ProjectileController>().ghostPrefab.transform.localScale = scale;
        //    FireProjectileInfo projectileInfo = new FireProjectileInfo();
        //    projectileInfo.projectilePrefab = projectile;
        //    projectileInfo.damageTypeOverride = DamageType.Generic;
        //    projectileInfo.damageColorIndex = DamageColorIndex.Default;
        //    projectileInfo.crit = RollCrit();
        //    projectileInfo.force = PantheraConfig.AirSlash_projectileForce;
        //    projectileInfo.damage = damage;
        //    projectileInfo.speedOverride = projectilSpeed;
        //    projectileInfo.useSpeedOverride = true;
        //    projectileInfo.owner = gameObject;
        //    projectileInfo.position = position;
        //    projectileInfo.rotation = rotation;

        //    // Fire projectiles //
        //    ProjectileManager.instance.FireProjectile(projectileInfo);

        //}

        //public override void Stop()
        //{
        //    // Hide the Crosshair and set it to zero //
        //    pantheraObj.crosshairComp.delayHideCrosshair(PantheraConfig.AirSlash_hideCrosshairTime);
        //    pantheraObj.crosshairComp.loadingCrosshairLevel = 0;
        //    // Start the Cooldown //
        //    skillLocator.startCooldown(PantheraConfig.AirSlash_SkillID);
        //    // Stop the cast Sound //
        //    Sound.playSound(Sound.AirSlashCastLoopStop, gameObject);
        //    // Check if the Effect still exist and destroy it //
        //    if (chargeEffect != null)
        //        FXManager.DestroyEffect(effectID);
        //}

    }
}
