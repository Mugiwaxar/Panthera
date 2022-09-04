using Panthera.MachineScripts;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static RoR2.DotController;

namespace Panthera
{
    class PantheraConfig
    {

        #region Color
        public static Color ConfigButtonColor = new Color32(255, 255, 225, 255);
        public static Color ConfigButtonHoveredColor = new Color32(255, 0, 0, 255);
        public static Color ExitButtonHoveredColor = new Color32(255, 0, 0, 255);
        public static Color TabButtonHoveredColor = new Color32(255, 0, 0, 255);
        public static Color SkillTreeTabButtonHoveredColor = new Color32(150, 0, 0, 255);
        public static Color SkillTreeAbilityHoveredColor = new Color32(0, 255, 0, 255);
        public static Color AbilityButtonHoveredColor = new Color32(255, 255, 255, 255);
        public static Color SkillButtonHoveredColor = new Color32(255, 0, 0, 255);
        public static Color KeysBindButtonDefaultColor = new Color32(247, 255, 135, 255);
        public static Color KeysBindButtonConflictColor = new Color32(255, 0, 0, 255);
        public static Color KeysBindButtonHoveredColor = new Color32(255, 0, 0, 255);
        public static Color KeysBindWindowButtonHoveredColor = new Color32(255, 0, 0, 255);
        public static Color PresetFrameNormalColor = new Color32(197, 104, 42, 255);
        public static Color PresetImageNormalColor = new Color32(255, 255, 255, 255);
        public static Color PresetButtonHoveredColor = new Color32(255, 0, 0, 255);
        public static Color PresetButtonSelectedColor = new Color32(0, 255, 91, 255);
        public static Color OverviewButtonsHoveredColor = new Color32(255, 0, 0, 255);
        public static Color PresetActivateButtonNormalColor = new Color32(255, 255, 255, 255);
        public static Color PresetActivateButtonDisabledColor = new Color32(255, 255, 255, 75);
        public static Color PresetResetButtonNormalColor = new Color32(255, 255, 255, 255);
        public static Color PresetResetButtonDisabledColor = new Color32(255, 255, 255, 75);
        #endregion
        #region Model
        public static float Model_generalScale = 1.5f;
        #endregion
        #region Stats
        public static readonly float Default_MaxHealth = 200;
        public static readonly float Default_MaxHealthLevel = 30;
        public static readonly float Default_HealthRegen = 2f;
        public static readonly float Default_HealthRegenLevel = 0.15f;
        public static readonly float Default_Energy = 100;
        public static readonly float Default_EnergyRegen = 10;
        public static readonly float Default_MaxFury = 100;
        public static readonly float Default_MaxPower = 100;
        public static readonly float Default_MaxComboPoint = 5;
        public static readonly float Default_MaxShield = 100;
        public static readonly float Default_MoveSpeed = 9;
        public static readonly float Default_MoveSpeedLevel = 0.03f;
        public static readonly float Default_Damage = 15;
        public static readonly float Default_DamageLevel = 2;
        public static readonly float Default_AttackSpeed = 1f;
        public static readonly float Default_AttackSpeedLevel = 0.03f;
        public static readonly float Default_Critic = 15;
        public static readonly float Default_CriticLevel = 0.5f;
        public static readonly float Default_Defense = 20;
        public static readonly float Default_DefenseLevel = 0.5f;
        public static readonly float Default_jumpCount = 1;
        #endregion
        #region SkillsIDs
        public static int Rip_SkillID = 1;
        public static int AirCleave_SkillID = 2;
        public static int Leap_SkillID = 3;
        public static int MightyRoar_SkillID = 4;
        public static int ClawsStorm_SkillID = 5;
        public static int FrontShield_SkillID = 6;
        public static int Prowl_SkillID = 7;
        public static int FuriousBite_SkillID = 8;
        #endregion
        #region AbilityID
        public static int DestructionAbilityID = 1;
        public static int GuardianAbilityID = 2;
        public static int RuseAbilityID = 3;
        #endregion
        #region Buffs and Dots
        public static BuffDef cloakBuff = RoR2Content.Buffs.Cloak;
        public static BuffDef weakDebuffDef = RoR2Content.Buffs.Weak;
        public static BuffDef regenBuffDef = RoR2Content.Buffs.CrocoRegen;
        public static BuffDef hiddenInvincibility = RoR2Content.Buffs.HiddenInvincibility;
        public static DotIndex bleedDotIndex = DotController.DotIndex.Bleed;
        public static DotIndex superBleedDotIndex = DotController.DotIndex.SuperBleed;
        #endregion
        #region Keys
        public static int JumpKey = 4;
        public static int InteractKey = 5;
        public static int EquipmentKey = 6;
        public static int SprintKey = 18;
        public static int PingKey = 28;
        public static int SwitchBarKey = 130;
        public static int Skill1Key = 7;
        public static int Skill2Key = 8;
        public static int Skill3Key = 9;
        public static int Skill4Key = 10;    
        public static int Skill5Key = 135;    
        public static int Skill6Key = 136;    
        public static int Skill7Key = 137;    
        public static int Skill8Key = 138;    
        public static int Skill9Key = 139;    
        public static int Skill10Key = 1310;    
        #endregion
        #region Tracker
        public static float Tracker_maxDistance = 100;
        public static float Tracker_maxAngle = 20;
        #endregion
        #region Motor
        public static float normalMaxAngle = 65;
        public static float superSprintMaxAngle = 90;
        #endregion

        #region Fury
        public static int Fury_pointsGenerated = 1;
        public static int Fury_pointsGenerationCooldown = 5;
        public static int Fury_damageTimeBeforStopGenerate = 3;
        #endregion

        #region Rip
        public static string Rip_hitboxName = "Rip";
        public static float Rip_cooldown = 0.3f;
        public static int Rip_requiredEnergy = 0;
        public static DamageType Rip_damageType = DamageType.Generic;
        public static float Rip_procCoefficient = 1f;
        public static float Rip_pushForce = 300f;
        public static Vector3 Rip_bonusForce = Vector3.zero;
        public static float Rip_atk1DamageMultiplier = 1f;
        public static float Rip_atk2DamageMultiplier = 1f;
        public static float Rip_atk3DamageMultiplier = 2f;
        public static float Rip_atk1BaseDuration = 0.5f;
        public static float Rip_atk2BaseDuration = 0.5f;
        public static float Rip_atk3BaseDuration = 0.8f;
        public static float Rip_distanceAttackActivation = 7;
        public static float Rip_weekDebuffDuration = 10;
        public static float Rip_minimumAimTime = 1f;
        public static float Rip_weakDuration = 5;
        public static string RightRip_hitboxName = "RightRip";
        public static float RightRip_atkDamageMultiplier = 1.3f;
        public static float RightRip_atkBaseDuration = 0.6f;
        public static float RightRip_rightVelocityMultiplier = 17;
        public static float RightRip_rightVelocityTime = 0.25f;
        public static string LeftRip_hitboxName = "LeftRip";
        public static float LeftRip_atkDamageMultiplier = 1.3f;
        public static float LeftRip_atkBaseDuration = 0.6f;
        public static float LeftRip_leftVelocityMultiplier = 17;
        public static float LeftRip_leftVelocityTime = 0.25f;
        public static string FrontRip_hitboxName = "FrontRip";
        public static float FrontRip_atkDamageMultiplier = 1.5f;
        public static float FrontRip_atkBaseDuration = 0.8f;
        public static float FrontRip_forwardVelocityMultiplier = 22;
        public static float FrontRip_forwardVelocityTime = 0.30f;
        #endregion
        #region AirCleave
        public static float AirCleave_cooldown = 0.5f;
        public static int AirCleave_requiredEnergy = 30;
        public static float AirCleave_atk1BaseDuration = 1f;
        public static float AirCleave_atk2BaseDuration = 1f;
        public static float AirCleave_atk1DamageMultiplier = 1.5f;
        public static float AirCleave_atk2DamageMultiplier = 1.5f;
        public static float AirCleave_projectileSpeed = 65;
        public static float AirCleave_projectileForce = 5;
        public static float AirCleave_minimumAimTime = 1f;
        #endregion
        #region Leap
        public static int Leap_coolDown = 10;
        public static int Leap_requiredEnergy = 0;
        public static float Leap_coolDownReduction = 2;
        public static float Leap_airControl = 0.10f;
        public static float Leap_airControlTarget = 0.50f;
        public static float Leap_minimumY = 0.35f;
        public static float Leap_minimumYTarget = 0.40f;
        public static float Leap_aimVelocity = 4;
        public static float Leap_maxMoveSpeed = 13;
        public static float Leap_forwardVelocity = 4;
        public static float Leap_upwardVelocity = 6;
        public static float Leap_minimumDuration = 0.7f;
        public static float Leap_leapStopDistance = 1.5f;
        public static float Leap_destroyComponentDelay = 0.5f;
        #endregion
        #region Mighty Roar
        public static int MightyRoar_energyRequired = 0;
        public static float MightyRoar_cooldown = 15;
        public static float MightyRoar_distance = 20;
        public static float MightyRoar_stunDuration = 3;
        public static float MightyRoar_duration = 0.5f;
        #endregion
        #region ClawsStorm
        public static string ClawsStorm_hitboxName = "ClawStorm";
        public static float ClawsStorm_cooldown = 0.3f;
        public static int ClawsStorm_requiredFury = 5;
        public static int ClawsStorm_continuousConsumedFury = 1;
        public static float ClawsStorm_continuousConsumeTime = 0.5f;
        public static float ClawsStorm_playSoundTime = 0.1f;
        public static float ClawsStorm_destroyFXDelay = 0.5f;
        public static DamageType ClawsStorm_damageType = DamageType.Generic;
        public static float ClawsStorm_procCoefficient = 1f;
        public static float ClawsStorm_pushForce = 300f;
        public static Vector3 ClawsStorm_bonusForce = Vector3.zero;
        public static float ClawsStorm_damageMultiplier = 0.7f;
        public static float ClawsStorm_baseDuration = 0.50f;
        public static float ClawsStorm_dashSpeedMultiplicator = 4f;
        public static float ClawsStorm_maxMoveSpeed = 10;
        public static float ClawsStorm_minMoveSpeed = 3;
        public static float ClawsStorm_firedDelay = 0.25f;
        public static float ClawsStorm_grabScanRadius = 2.5f;
        public static float ClawsStorm_grabDistanceMultiplier = 1f;
        #endregion
        #region Shield
        public static float FrontShield_cooldown = 0.1f;
        public static float FrontShield_rechargeDelayAfterDamage = 3;
        public static float FrontShield_rechargeDelayAfterDestroyed = 5;
        public static float FrontShield_maxShieldHealthPercent = 0.25f;
        public static float FrontShield_rechargeRatePercent = 0.1f;
        public static float FrontShield_decreaseMultiplier = 0.8f;
        #endregion
        #region Prowl
        public static float Prowl_coolDown = 30;
        public static float Prowl_moveSpeedReduction = 5;
        public static float Prowl_skillDuration = 1;
        #endregion
        #region Furious Bite
        public static DamageType FuriousBite_atkDamageType = DamageType.Generic;
        public static float FuriousBite_cooldown = 8f;
        public static int FuriousBite_energyRequired = 40;
        public static int FuriousBite_maxComboPointUsed = 5;
        public static float FuriousBite_atkDamageMultiplier = 2.5f;
        public static float FuriousBite_ComboPointMultiplier = 0.5f;
        public static float FuriousBite_atkBaseDuration = 0.5f;
        public static float FuriousBite_maxDistanceToActivate = 3;
        #endregion

        #region Destruction
        public static float Destruction_addedDamage = 3;
        public static float Destruction_addedDamageLevel = 0.2f;
        public static float Destruction_addedAttackSpeed = 0.2f;
        public static float Destruction_addedAttackSpeedLevel = 0.01f;
        #endregion
        #region Guardian
        public static float Guardian_addedHealth = 100;
        public static float Guardian_addedHealthLevel = 10;
        public static float Guardian_addedHealthRegen = 1f;
        public static float Guardian_addedHealthRegenLevel = 0.5f;
        public static float Guardian_addedDefense = 15;
        public static float Guardian_addedDefenseLevel = 0.2f;
        #endregion

        #region Steal
        public static float stealDelay = 5;
        #endregion
        #region Shield
        public static float Shield_duration = 3;
        public static float Shield_maxStack = 10;
        public static float Shield_damageCoefficientPerStack = 0.1f;
        public static float Shield_damageAbsoptionShieldMultiplier = 0.01f;
        #endregion
        #region Super Sprint
        public static float superSprintDelay = 3;
        public static float superSprintSpeedAdded = 8;
        #endregion
        #region Health
        public static bool canBeExecuted = false;
        public static float healthPercentAfterNineLivesActivated = 0.1f;
        #endregion
        #region Priorities
        public static ScriptPriority Rip_priority = ScriptPriority.MinimumPriority;
        public static ScriptPriority Rip_interruptPower = ScriptPriority.MinimumPriority;

        public static ScriptPriority AirCleave_priority = ScriptPriority.MinimumPriority;
        public static ScriptPriority AirCleave_interruptPower = ScriptPriority.MinimumPriority;

        public static ScriptPriority Leap_priority = ScriptPriority.SmallPriority;
        public static ScriptPriority Leap_interruptPower = ScriptPriority.VerySmallPriority;

        public static ScriptPriority MightyRoar_priority = ScriptPriority.VeryHightPriority;
        public static ScriptPriority MightyRoar_interruptPower = ScriptPriority.HightPriority;

        public static ScriptPriority ClawsStorm_priority = ScriptPriority.SmallPriority;
        public static ScriptPriority ClawsStorm_interruptPower = ScriptPriority.SmallPriority;

        public static ScriptPriority FrontShield_priority = ScriptPriority.MinimumPriority;
        public static ScriptPriority FrontShield_interruptPower = ScriptPriority.AveragePriority;

        public static ScriptPriority Prowl_priority = ScriptPriority.MaximumPriority;
        public static ScriptPriority Prowl_interruptPower = ScriptPriority.ExtraPriority;

        public static ScriptPriority FuriousBite_priority = ScriptPriority.AveragePriority;
        public static ScriptPriority FuriousBite_interruptPower = ScriptPriority.AveragePriority;



        public static ScriptPriority Mangle_priority = ScriptPriority.VeryHightPriority;
        public static ScriptPriority Mangle_interruptPower = ScriptPriority.AveragePriority;

        public static ScriptPriority LeapCercle_priority = ScriptPriority.MaximumPriority;
        public static ScriptPriority LeapCercle_interruptPower = ScriptPriority.ExtraPriority;

        public static ScriptPriority RaySlash_priority = ScriptPriority.ExtraHightPriority;
        public static ScriptPriority RaySlash_interruptPower = ScriptPriority.ExtraHightPriority;
        #endregion
        #region Passives
        public static int Passive_maxMangleStack = 30;
        public static float Passive_bleedDamage = 0.10f;
        public static float Passive_bleedDuration = 10;
        public static float Passive_maxLifeStealDistance = 8;
        public static float Passive_lifeStealMultiplier = 0.25f;
        public static float Passive_stealDamageMultiplier = 3;
        public static float Passive_stealStunDuration = 2;
        public static float Passive_nineLivesCooldown = 300;
        public static float Passive_barriereMultiplier = 0.5f;
        public static float Passive_leapBuffRemoveTime = 1;
        public static float Passive_maxPreyHoldingTime = 5;
        #endregion
        #region Mangle
        public static float Mangle_atkBaseDuration = 0.7f;
        public static float Mangle_bleedDuration = 10;
        public static float Mangle_bleedBaseDamage = 1f;
        public static float Mangle_maxDistanceToActivate = 4;
        #endregion
        #region Leap Cercle
        public static float LeapCercle_leapCercleDuration = 15;
        public static float LeapCercle_jumpBackDelay = 1;
        public static float LeapCercle_airControl = 0.15f;
        public static float LeapCercle_maximumLeapDuration = 3;
        public static float LeapCercle_moveSpeed = 20;
        public static float LeapCercle_slowDownSpeed = 8;
        public static float LeapCercle_upwardVelocity = 10;
        public static float LeapCercle_minimumY = 0.15f;
        public static float LeapCercle_slowDownDistance = 5;
        public static float LeapCercle_stopDistance = 2f;
        public static float LeapCercle_regenDuration = 0.5f;
        public static float LeapCerle_delayBeforeDestroyed = 1;
        #endregion
        #region Keep The Prey
        public static float KeepThePrey_destroyComponentDelay = 0.5f;
        #endregion
        #region Ray Slash
        public static float RaySlash_projectileForce = 0;
        public static float RaySlash_damageMultiplier = 50;
        public static float RaySlash_projectileSpeed = 30;
        public static float RaySlash_initialWaiting = 0.2f;
        public static float RaySlash_chargeMinWaiting = 1;
        public static float RaySlash_startWaiting = 1.2f;
        public static float RaySlash_endWaiting = 2.2f;
        public static float RaySlash_effectDuration = 1;
        public static int RaySlash_maxRaySlashBuff = 100;
        public static int RaySlash_buffRequiredToFire = 100;
        public static float RaySlash_timeNeededToRemoveAStock = 1;
        public static float RaySlash_damageAbsoptionMultiplier = 0.03f;
        #endregion

        #region Item Change
        public static ItemIndex ItemChange_shieldIndex = (ItemIndex)127;
        public static ItemIndex ItemChange_steakIndex = (ItemIndex)65;
        public static ItemIndex ItemChange_magazineIndex = (ItemIndex)147;
        public static ItemIndex ItemChange_alienHeadIndex = (ItemIndex)2;
        public static ItemIndex ItemChange_bandolierIndex = (ItemIndex)9;
        public static ItemIndex ItemChange_shurikenIndex = (ItemIndex)132;
        public static ItemIndex ItemChange_squidIndex = (ItemIndex)160;
        public static ItemIndex ItemChange_brainstalksIndex = (ItemIndex)93;
        public static ItemIndex ItemChange_hardlightAfterburnerIndex = (ItemIndex)177;
        public static ItemIndex ItemChange_heresyEssenceIndex = (ItemIndex)102;
        public static ItemIndex ItemChange_heresyHooksIndex = (ItemIndex)101;
        public static ItemIndex ItemChange_heresyStridesIndex = (ItemIndex)105;
        public static ItemIndex ItemChange_heresyVisionsIndex = (ItemIndex)100;
        public static ItemIndex ItemChange_brittleCrownIndex = (ItemIndex)72;
        public static ItemIndex ItemChange_lightFluxPauldronIndex = (ItemIndex)75;
        public static ItemIndex ItemChange_purityIndex = (ItemIndex)98;
        public static ItemIndex ItemChange_lysateCellIndex = (ItemIndex)52;
        public static float ItemChange_magazinePercentCooldownReduction = 0.95f;
        public static float ItemChange_alienHeadPercentCooldownReduction = 0.85f;
        public static float ItemChange_hardlightAfterburnerPercentCooldownReduction = 0.75f;
        public static float ItemChange_lightFluxPauldronPercentCooldownReduction = 0.75f;
        public static float ItemChange_purityPercentCooldownReduction = 0.80f;
        public static float ItemChange_lysateCellCooldownReduction = 0.85f;
        public static float ItemChange_noCooldownTimeRemoved = 0.02f;
        #endregion

        #region Others
        public static int ResetPresetCost = 5;
        public static int ActivatePresetCost = 1;
        public static int ExperienceDivider = 80;
        public static int PointsRequiredForMultipleSkillsTree = 30;
        #endregion


        public static void readDefs()
        {
            cloakBuff = RoR2Content.Buffs.Cloak;
            weakDebuffDef = RoR2Content.Buffs.Weak;
            regenBuffDef = RoR2Content.Buffs.CrocoRegen;
            hiddenInvincibility = RoR2Content.Buffs.HiddenInvincibility;
            bleedDotIndex = DotController.DotIndex.Bleed;
            superBleedDotIndex = DotController.DotIndex.SuperBleed;
        }

    }
}
