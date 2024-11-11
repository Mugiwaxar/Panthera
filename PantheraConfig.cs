using Panthera.Components;
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

        // -- Base -- //
        #region Save Parameter Name
        public static readonly string SP_CharacterXP = "CharacterXP";
        public static readonly string SP_CharacterMastery = "CharacterMastery";
        public static readonly string SP_Endurance = "CharacterEndurance";
        public static readonly string SP_Force = "CharacterForce";
        public static readonly string SP_Agility = "CharacterAgility";
        public static readonly string SP_Swiftness = "CharacterSwiftness";
        public static readonly string SP_Dexterity = "CharacterDexterity";
        public static readonly string SP_Spirit = "CharacterSpirit";
        #endregion
        #region Keys
        public static readonly string InteractKeyName = "Interact";
        public static readonly string EquipmentKeyName = "Activate Equipment";
        public static readonly string SprintKeyName = "Sprint";
        public static readonly string InfoKeyName = "Info";
        public static readonly string PingKeyName = "Ping";
        public static readonly string ForwardKeyName = "Move Forward";
        public static readonly string BackwardKeyName = "Move Backward";
        public static readonly string LeftKeyName = "Move Left";
        public static readonly string RightKeyName = "Move Right";
        public static readonly string JumpKeyName = "Jump";
        public static readonly string Skill1KeyName = "Primary Skill";
        public static readonly string Skill2KeyName = "Secondary Skill";
        public static readonly string Skill3KeyName = "Utility Skill";
        public static readonly string Skill4KeyName = "Special Skill";
        public static readonly int LeftRightKey = 0;
        public static readonly int ForwardBackwardKey = 1;
        public static readonly int JumpKey = 4;
        public static readonly int InteractKey = 5;
        public static readonly int EquipmentKey = 6;
        public static readonly int SprintKey = 18;
        public static readonly int InfoKey = 19;
        public static readonly int PingKey = 28;
        public static readonly int Skill1Key = 7;
        public static readonly int Skill2Key = 8;
        public static readonly int Skill3Key = 9;
        public static readonly int Skill4Key = 10;
        public static readonly int Keys_OpenPantheraPanelActionCode = 1300;
        public static readonly int Keys_Ability1ActionCode = 1301;
        public static readonly int Keys_Ability2ActionCode = 1302;
        public static readonly int Keys_Ability3ActionCode = 1303;
        public static readonly int Keys_Ability4ActionCode = 1304;
        public static readonly int Keys_SpellsModeActionCode = 1310;
        public static readonly string Keys_OpenPantheraPanelActionName = "OpenPantheraPanel";
        public static readonly string Keys_OpenPantheraPanelActionDesc = "Open Panthera Panel";
        public static readonly KeyCode Keys_OpenPantheraPanelDefaultKey = KeyCode.P;
        public static readonly string Keys_Ability1ActionName = "Ability1";
        public static readonly string Keys_Ability1ActionDesc = "Ability 1";
        public static readonly KeyCode Keys_Ability1DefaultKey = KeyCode.Alpha1;
        public static readonly int Keys_Ability1DefaultJoystickIdentifierID = 16;
        public static readonly string Keys_Ability2ActionName = "Ability2";
        public static readonly string Keys_Ability2ActionDesc = "Ability 2";
        public static readonly KeyCode Keys_Ability2DefaultKey = KeyCode.Alpha2;
        public static readonly int Keys_Ability2DefaultJoystickIdentifierID = 17;
        public static readonly string Keys_Ability3ActionName = "Ability3";
        public static readonly string Keys_Ability3ActionDesc = "Ability 3";
        public static readonly KeyCode Keys_Ability3DefaultKey = KeyCode.Alpha3;
        public static readonly int Keys_Ability3DefaultJoystickIdentifierID = 18;
        public static readonly string Keys_Ability4ActionName = "Ability4";
        public static readonly string Keys_Ability4ActionDesc = "Ability 4";
        public static readonly KeyCode Keys_Ability4DefaultKey = KeyCode.Alpha4;
        public static readonly int Keys_Ability4DefaultJoystickIdentifierID = 19;
        public static readonly string Keys_SpellsModeActionName = "Spells";
        public static readonly string Keys_SpellsModeActionDesc = "Spell Mode";
        public static readonly KeyCode Keys_SpellsModeDefaultKey = KeyCode.T;
        public static readonly int Keys_SpellModeDefaultJoystickIdentifierID = 7;
        #endregion
        #region Priorities
        public static readonly ScriptPriority Rip_priority = ScriptPriority.AveragePriority;
        public static readonly ScriptPriority Rip_interruptPower = ScriptPriority.MinimumPriority;

        public static readonly ScriptPriority AirCleave_priority = ScriptPriority.AveragePriority;
        public static readonly ScriptPriority AirCleave_interruptPower = ScriptPriority.MinimumPriority;

        public static readonly ScriptPriority Leap_priority = ScriptPriority.SmallPriority;
        public static readonly ScriptPriority Leap_interruptPower = ScriptPriority.VerySmallPriority;

        public static readonly ScriptPriority MightyRoar_priority = ScriptPriority.AveragePriority;
        public static readonly ScriptPriority MightyRoar_interruptPower = ScriptPriority.HightPriority;

        public static readonly ScriptPriority Fury_priority = ScriptPriority.AveragePriority;
        public static readonly ScriptPriority Fury_interruptPower = ScriptPriority.AveragePriority;

        public static readonly ScriptPriority Guardian_priority = ScriptPriority.AveragePriority;
        public static readonly ScriptPriority Guardian_interruptPower = ScriptPriority.AveragePriority;

        public static readonly ScriptPriority Slash_priority = ScriptPriority.VerySmallPriority;
        public static readonly ScriptPriority Slash_interruptPower = ScriptPriority.VerySmallPriority;

        public static readonly ScriptPriority Detection_priority = ScriptPriority.AveragePriority;
        public static readonly ScriptPriority Detection_interruptPower = ScriptPriority.AveragePriority;

        public static readonly ScriptPriority Prowl_priority = ScriptPriority.AveragePriority;
        public static readonly ScriptPriority Prowl_interruptPower = ScriptPriority.AveragePriority;

        public static readonly ScriptPriority Ambition_priority = ScriptPriority.AveragePriority;
        public static readonly ScriptPriority Ambition_interruptPower = ScriptPriority.AveragePriority;

        public static readonly ScriptPriority AirSlash_priority = ScriptPriority.ExtraPriority;
        public static readonly ScriptPriority AirSlash_interruptPower = ScriptPriority.HightPriority;

        public static readonly ScriptPriority FrontShield_priority = ScriptPriority.MinimumPriority;
        public static readonly ScriptPriority FrontShield_interruptPower = ScriptPriority.ExtraPriority;

        public static readonly ScriptPriority ClawsStorm_priority = ScriptPriority.SmallPriority;
        public static readonly ScriptPriority ClawsStorm_interruptPower = ScriptPriority.SmallPriority;

        public static readonly ScriptPriority ShieldBash_priority = ScriptPriority.VeryHightPriority;
        public static readonly ScriptPriority ShieldBash_interruptPower = ScriptPriority.ExtraPriority;

        public static readonly ScriptPriority ArcaneAnchor_priority = ScriptPriority.ExtraHightPriority;
        public static readonly ScriptPriority ArcaneAnchor_interruptPower = ScriptPriority.VerySmallPriority;

        public static readonly ScriptPriority ConvergenceHook_priority = ScriptPriority.ExtraPriority;
        public static readonly ScriptPriority ConvergenceHook_interruptPower = ScriptPriority.MinimumPriority;

        public static readonly ScriptPriority PortalSurge_priority = ScriptPriority.MinimumPriority;
        public static readonly ScriptPriority PortalSurge_interruptPower = ScriptPriority.MinimumPriority;



        public static readonly ScriptPriority FuriousBite_priority = ScriptPriority.AveragePriority;
        public static readonly ScriptPriority FuriousBite_interruptPower = ScriptPriority.AveragePriority;

        public static readonly ScriptPriority Dash_priority = ScriptPriority.MinimumPriority;
        public static readonly ScriptPriority Dash_interruptPower = ScriptPriority.MinimumPriority;

        public static readonly ScriptPriority ZoneHeal_priority = ScriptPriority.MinimumPriority;
        public static readonly ScriptPriority ZoneHeal_interruptPower = ScriptPriority.AveragePriority;

        public static readonly ScriptPriority SaveMyFriend_priority = ScriptPriority.ExtraPriority;
        public static readonly ScriptPriority SaveMyFriend_interruptPower = ScriptPriority.ExtraPriority;

        public static readonly ScriptPriority FireBird_priority = ScriptPriority.ExtraPriority;
        public static readonly ScriptPriority FireBird_interruptPower = ScriptPriority.AveragePriority;

        public static readonly ScriptPriority Revive_priority = ScriptPriority.NoPriority;
        public static readonly ScriptPriority Revive_interruptPower = ScriptPriority.NoPriority;

        //public static readonly ScriptPriority Mangle_priority = ScriptPriority.VeryHightPriority;
        //public static readonly ScriptPriority Mangle_interruptPower = ScriptPriority.AveragePriority;

        //public static readonly ScriptPriority LeapCercle_priority = ScriptPriority.MaximumPriority;
        //public static readonly ScriptPriority LeapCercle_interruptPower = ScriptPriority.ExtraPriority;

        //public static readonly ScriptPriority RaySlash_priority = ScriptPriority.ExtraHightPriority;
        //public static readonly ScriptPriority RaySlash_interruptPower = ScriptPriority.ExtraHightPriority;
        #endregion
        #region Stats
        public static readonly int ResetAttributesCost = 2;
        public static readonly int ResetSkillsTreeCost = 3;
        public static readonly int MaxPantheraLevel = 1000;
        public static readonly float Default_MaxHealth = 200;
        public static readonly float Default_MaxHealthLevel = 40;
        public static readonly float Default_HealthRegen = 2.5f;
        public static readonly float Default_HealthRegenLevel = 0.4f;
        public static readonly float Default_MaxFury = 100;
        public static readonly float Default_MaxFuryLevel = 15;
        public static readonly float Default_MaxFrontShield = 100;
        public static readonly float Default_MoveSpeed = 9;
        public static readonly float Default_MoveSpeedLevel = 0.025f;
        public static readonly float Default_Damage = 25;
        public static readonly float Default_DamageLevel = 2.5f;
        public static readonly float Default_AttackSpeed = 1;
        public static readonly float Default_AttackSpeedLevel = 0.001f;
        public static readonly float Default_Critic = 8;
        public static readonly float Default_CriticLevel = 0.075f;
        public static readonly float Default_Dodge = 5;
        public static readonly float Default_DodgeLevel = 0.025f;
        public static readonly float Default_Defense = 15;
        public static readonly float Default_DefenseLevel = 0.075f;
        public static readonly float Default_Mastery = 3;
        public static readonly float Default_MasteryLevel = 0.01f;
        public static readonly float Default_jumpCount = 1;
        public static readonly float Default_maxBlock = 10;
        public static readonly float Default_blockImmunDuration = 0.3f;
        #endregion
        #region Color
        public static readonly Color32 ConfigButtonColor = new Color32(255, 255, 225, 255);
        public static readonly Color32 ConfigButtonHoveredColor = new Color32(255, 0, 0, 255);
        public static readonly Color32 ScaleWindowButtonHoveredColor = new Color32(255, 0, 0, 255);
        public static readonly Color32 ExitButtonHoveredColor = new Color32(255, 0, 0, 255);
        public static readonly Color32 TabButtonHoveredColor = new Color32(255, 0, 0, 255);
        public static readonly Color32 SkillTreeTabButtonHoveredColor = new Color32(150, 0, 0, 255);
        public static readonly Color32 AbilityButtonHoveredColor = new Color32(255, 255, 255, 255);
        public static readonly Color32 SkillButtonHoveredColor = new Color32(255, 0, 0, 255);
        public static readonly Color32 KeysBindButtonDefaultColor = new Color32(247, 255, 135, 255);
        public static readonly Color32 KeysBindButtonConflictColor = new Color32(255, 0, 0, 255);
        public static readonly Color32 KeysBindButtonHoveredColor = new Color32(255, 0, 0, 255);
        public static readonly Color32 KeysBindWindowButtonHoveredColor = new Color32(255, 0, 0, 255);
        public static readonly Color32 PresetFrameNormalColor = new Color32(197, 104, 42, 255);
        public static readonly Color32 PresetImageNormalColor = new Color32(255, 255, 255, 255);
        public static readonly Color32 PresetButtonHoveredColor = new Color32(255, 0, 0, 255);
        public static readonly Color32 PresetButtonSelectedColor = new Color32(0, 255, 91, 255);
        public static readonly Color32 OverviewButtonsHoveredColor = new Color32(255, 0, 0, 255);
        public static readonly Color32 OverviewAttributeButtonsEnabledColor = new Color32(255, 255, 255, 255);
        public static readonly Color32 OverviewAttributeButtonsDisabledColor = new Color32(85, 85, 85, 255);
        public static readonly Color32 PresetActivateButtonNormalColor = new Color32(255, 255, 255, 255);
        public static readonly Color32 PresetActivateButtonDisabledColor = new Color32(255, 255, 255, 75);
        public static readonly Color32 PresetResetButtonNormalColor = new Color32(255, 255, 255, 255);
        public static readonly Color32 PresetResetButtonDisabledColor = new Color32(255, 255, 255, 75);
        public static readonly Color32 DetectionDefaultColor = new Color32(0, 0, 255, 255);
        public static readonly Color32 DetectionCharacterDefaultColor = new Color32(0, 255, 0, 255);
        public static readonly Color32 DetectionCanAffortChestColor = new Color32(255, 224, 116, 255);
        public static readonly Color32 PantheraHUDLevelBarColor = new Color32(255, 125, 0, 255);
        public static readonly Color32 SkillsNormalSkillColor = new Color32(255, 255, 255, 255);
        public static readonly Color32 SkillsLockedSkillColor = new Color32(120, 50, 50, 150);
        public static readonly Color32 SkillsNormalFrameColor = new Color32(255, 255, 255, 255);
        public static readonly Color32 SkillsMasteryFrameColor = new Color32(159, 0, 219, 255);
        public static readonly Color32 SkillsTreeUnlockedAbilityColor = new Color32(255, 255, 255, 255);
        public static readonly Color32 SkillsTreeLockedAbilityColor = new Color32(100, 100, 100, 150);
        public static readonly Color32 SkillsTreeAbilityHoveredColor = new Color32(0, 255, 0, 255);
        public static readonly Color32 SkillsTreeLineUnlockedColor = new Color32(255, 255, 255, 255);
        public static readonly Color32 SkillsTreeLinelockedColor = new Color32(68, 68, 68, 255);
        public static readonly Color32 ComboNormalColor = new Color32(255, 255, 255, 255);
        public static readonly Color32 ComboLockedColor = new Color32(97, 0, 0, 255);
        public static readonly Color32 ComboCooldownNormalIconColor = new Color32(0, 170, 0, 255);
        public static readonly Color32 ComboCooldownLoadingIconColor = new Color32(200, 0, 0, 255);
        public static readonly Color32 DetectionCDFillNormalColor = new Color32(0, 224, 218, 200);
        public static readonly Color32 DetectionCDFillRechargeColor = new Color32(255, 27, 0, 200);
        public static readonly Color32 FrontShieldNormalColor = new Color32(0, 93, 248, 200);
        public static readonly Color32 FrontShieldCriticColor = new Color32(237, 0, 0, 200);
        public static readonly Color32 BlockBarColor = new Color32(255, 255, 255, 230);
        #endregion
        #region Model
        public static readonly float Model_defaultModelScale = 1.3f;
        public static readonly string Model_PrefabName = "Panthera";
        public static readonly float Model_fadeStartDistance = 5;
        public static readonly float Model_fadeEndDistance = 10;
        #endregion
        #region Camera Parameters
        public static readonly float defaultFOV = 60;
        public static readonly Vector3 defaultCamPosition = new Vector3(0, 1, -13);
        public static readonly Vector3 Death_cameraPos = new Vector3(0f, 5, -50);
        public static readonly Vector3 ClawsStorm_cameraPos = new Vector3(0, 3, -40);
        #endregion
        #region Buffs and Dots
        public static BuffDef CloakBuffDef;
        public static BuffDef WeakDebuffDef;
        public static BuffDef RegenBuffDef;
        public static BuffDef InvincibilityBuffDef;
        public static BuffDef HiddenInvincibilityBuffDef;
        public static DotIndex BleedDotIndex;
        public static DotIndex SuperBleedDotIndex;
        public static DotIndex BurnDotIndex;
        public static DotIndex SuperBurnDotIndex;
        #endregion
        #region Tracker
        public static readonly float Tracker_maxDistance = 100;
        public static readonly float Tracker_maxAngle = 20;
        #endregion
        #region Motor
        public static readonly float normalMaxAngle = 65;
        public static readonly float unsleepDuration = 2.2f;
        #endregion
        #region Item Change
        public static ItemDef ItemChange_steak;
        public static ItemDef ItemChange_magazineIndex;
        public static ItemDef ItemChange_alienHeadIndex;
        public static ItemDef ItemChange_bandolierIndex;
        public static ItemDef ItemChange_shurikenIndex;
        public static ItemDef ItemChange_squidIndex;
        public static ItemDef ItemChange_brainstalksIndex;
        public static ItemDef ItemChange_hardlightAfterburnerIndex;
        public static ItemDef ItemChange_heresyEssenceIndex;
        public static ItemDef ItemChange_heresyHooksIndex;
        public static ItemDef ItemChange_heresyStridesIndex;
        public static ItemDef ItemChange_heresyVisionsIndex;
        public static ItemDef ItemChange_brittleCrownIndex;
        public static ItemDef ItemChange_lightFluxPauldronIndex;
        public static ItemDef ItemChange_purityIndex;
        public static ItemDef ItemChange_lysateCellIndex;
        public static ItemDef ItemChange_transcendanceIndex;
        public static readonly float ItemChange_magazinePercentCooldownReduction = 0.95f;
        public static readonly float ItemChange_alienHeadPercentCooldownReduction = 0.85f;
        public static readonly float ItemChange_hardlightAfterburnerPercentCooldownReduction = 0.66f;
        public static readonly float ItemChange_lightFluxPauldronPercentCooldownReduction = 0.75f;
        public static readonly float ItemChange_purityPercentCooldownReduction = 0.80f;
        public static readonly float ItemChange_lysateCellCooldownReduction = 0.66f;
        public static readonly float ItemChange_noCooldownTimeRemoved = 0.02f;
        #endregion
        #region Death
        public static readonly float Death_effectStartTime = 3;
        public static readonly float Death_minimumTimeBeforeDestroying = 6;
        #endregion
        #region Orbs

        public static readonly string ShieldOrb_Name = "ShieldOrb";
        public static readonly float ShieldOrb_Duration = 1.5f;

        #endregion

        // -- Skills -- //
        #region Skills ID
        public static readonly int Rip_SkillID = 1;
        public static readonly int AirCleave_SkillID = 2;
        public static readonly int Leap_SkillID = 3;
        public static readonly int MightyRoar_SkillID = 4;
        public static readonly int Fury_SkillID = 5;
        public static readonly int Guardian_SkillID = 6;
        public static readonly int Slash_SkillID = 7;
        public static readonly int Prowl_SkillID = 8;
        public static readonly int Detection_SkillID = 9;
        public static readonly int Ambition_SkillID = 10;
        public static readonly int AirSlash_SkillID = 11;
        public static readonly int FrontShield_SkillID = 12;
        public static readonly int ClawsStorm_SkillID = 13;
        public static readonly int ShieldBash_SkillID = 14;
        public static readonly int ArcaneAnchor_SkillID = 15;
        public static readonly int ConvergenceHook_SkillID = 16;
        public static readonly int PortalSurge_SkillID = 17;
        #endregion

        #region Portal Surge
        public static readonly int PortalSurge_lunarCost = 15;
        public static readonly int PortalSurge_requiredIngameLevel = 30;
        public static readonly float PortalSurge_cooldown = 900;
        public static readonly float PortalSurge_failCoolDown = 5;
        public static readonly float PortalSurge_duration = 9.6f;
        public static readonly float PortalSurge_endEffectTime = 7.9f;
        public static readonly float PortalSurge_FailTime = 1.5f;
        public static readonly float PortalSurge_detectionRadius = 1;
        public static readonly float PortalSurge_damage = 1000;
        public static readonly float PortalSurge_teleporterChargeTime = 600;
        public static readonly float PortalSurge_teleporterRadius = 500;
        public static readonly float PortalSurge_addCreditsTime = 0.3f;
        public static readonly float PortalSurge_initialCredit = 800;
        public static readonly string PortalSurge_megaBossAddedName = " PtraMegaBoss";
        public static readonly float PortalSurge_bossSizeMultiplier = 2;
        public static readonly float PortalSurge_bossStatsMultiplier = 3;
        #endregion
        #region Rip
        public static readonly string Rip_hitboxName = "Rip";
        public static readonly float Rip_cooldown = 0.3f;
        public static readonly DamageType Rip_damageType = DamageType.Generic;
        public static readonly float Rip_minimumAimTime = 1.5f;
        public static readonly float Rip_procCoefficient = 1f;
        public static readonly float Rip_dashForce = 10;
        public static readonly float Rip_pushForce = 5;
        public static readonly Vector3 Rip_bonusForce = Vector3.zero;
        public static readonly float Rip_atkDamageMultiplier = 1.5f;
        public static readonly float Rip_atkDuration = 0.6f;
        public static readonly float Rip_attackTime = 0.20f;
        public static readonly int Rip_furyAdded = 3;
        public static readonly float Rip_enemiesForwardScanDistance = 1.7f;
        public static readonly float Rip_enemiesScanRadius = 6f;
        public static readonly float Rip_hideCrosshairTime = 3f;
        #endregion
        #region Air Cleaves
        public static readonly string AirCleave_leftProjectileName = "AirCleaveLeftProjectile";
        public static readonly string AirCleave_rightProjectileName = "AirCleaveRightProjectile";
        public static readonly string AirCleave_leftFireProjectileName = "FireAirCleaveLeftProjectile";
        public static readonly string AirCleave_rightFireProjectileName = "FireAirCleaveRightProjectile";
        public static readonly float AirCleave_cooldown = 0.3f;
        public static readonly float AirCleave_attackDuration = 0.8f;
        public static readonly float AirCleave_attackTime = 0.25f;
        public static readonly float AirCleave_attackDamageMultiplier = 1;
        public static readonly float AirCleave_procCoefficient = 0.8f;
        public static readonly float AirCleave_projectileSpeed = 130;
        public static readonly float AirCleave_projectileForce = 15;
        public static readonly float AirCleave_pushForce = 10;
        public static readonly float AirCleave_minimumAimTime = 1f;
        public static readonly float AirCleave_furyAdded = 5;
        public static readonly float AirCleave_projScale = 0.5f;
        #endregion
        #region Leap
        public static readonly float Leap_cooldown = 1; // ----------------------------------------- //
        public static readonly float Leap_airControl = 0.10f;
        public static readonly float Leap_maxMoveSpeed = 100;
        public static readonly float Leap_speedMultiplier = 3.5f;
        public static readonly float Leap_minimumY = 5f;
        public static readonly float Leap_minimumYTarget = 10f;
        public static readonly float Leap_aimRayYMultiplier = 15;
        public static readonly float Leap_upwardVelocity = 2.7f;
        public static readonly float Leap_targetSpeedMultiplier = 4;
        public static readonly float Leap_naturalDown = 0.5f;
        public static readonly float Leap_minimumDuration = 0.7f;
        public static readonly float Leap_duration = 3;
        public static readonly float Leap_leapStopDistance = 3f;
        public static readonly float Leap_leapScanRadius = 5f;
        public static readonly float Leap_Fov = 100f;
        #endregion
        #region Myghty Roar
        public static readonly float MightyRoar_cooldown = 20;
        public static readonly float MightyRoar_radius = 15;
        public static readonly float MightyRoar_stunDuration = 3f;
        public static readonly float MightyRoar_duration = 0.5f;
        #endregion
        #region fury
        public static readonly float Fury_cooldown = 30;
        public static readonly float Fury_duration = 0.25f;
        public static readonly float Fury_furyPointsDecreaseTime = 0.05f;
        public static readonly float Fury_increasedAttackSpeed = 0.50f;
        public static readonly float Fury_increasedMoveSpeed = 0.50f;
        #endregion
        #region Guardian
        public static readonly float Guardian_cooldown = 8;
        public static readonly float Guardian_duration = 0.25f;
        public static readonly float Guardian_increasedArmor = 0.75f;
        public static readonly float Guardian_increasedHealthRegen = 1.0f;
        public static readonly float Guardian_increasedMoveSpeedCoef = 1.15f;
        public static readonly float Guardian_barrierDecayRatePercent = 0.25f;
        public static readonly float Guardian_increasedSize = 1.5f;
        public static readonly float Guardian_masteryHealPercent = 0.15f;
        #endregion
        #region Slash
        public static readonly int Slash_cooldown = 5;
        public static readonly float Slash_duration = 0.8f;
        public static readonly float Slash_radius = 8;
        public static readonly float Slash_damageMultiplier = 2;
        public static readonly float Slash_knockbackPowerMin = 5;
        public static readonly float Slash_knockbackPowerMax = 15;
        public static readonly float Slash_procCoefficient = 0.7f;
        public static readonly float Slash_BleedDuration = 7;
        public static readonly int Slash_furyAdded = 2;
        #endregion
        #region Prowl
        public static readonly float Prowl_coolDown = 8;
        public static readonly float Prowl_unStealDelay = 0.1f;
        public static readonly float Prowl_moveSpeedMultiplier = 0.60f;
        public static readonly float Prowl_skillDuration = 0.3f;
        #endregion
        #region Detection
        public static readonly float Detection_cooldown = 2f;
        public static readonly float Detection_maxTime = 8f;
        public static readonly float Detection_scanRate = 0.5f;
        public static readonly float Detection_skillMinDuration = 0.3f;
        public static readonly float Detection_skillMaxDuration = 2.5f;
        public static readonly int Detection_maxScanPerFrame = 100;
        public static readonly int Detection_layerIndex = 31;
        #endregion
        #region Ambition
        public static readonly float Ambition_buffDuration = 30;
        public static readonly float Ambition_cooldown = 300;
        public static readonly float Ambition_duration = 0.5f;
        #endregion
        #region Air Slash
        public static readonly string AirSlash_projectileName = "AirSlashProjectile";
        public static readonly float AirSlash_cooldown = 7;
        public static readonly float AirSlash_comboMaxTime = 5;
        public static readonly float AirSlash_atkDamageMultiplier = 2f;
        public static readonly float AirSlash_procCoefficient = 1.5f;
        public static readonly float AirSlash_furyAdded = 2;
        public static readonly float AirSlash_attackDuration = 0.5f;
        public static readonly float AirSlash_attackTime = 0.25f;
        public static readonly float AirSlash_projScale = 0.50f;
        public static readonly float AirSlash_projectileSpeed = 50;
        public static readonly float AirSlash_projectileForce = 15;
        public static readonly float AirSlash_minimumAimTime = 0.8f;
        public static readonly float AirSlash_hideCrosshairTime = 1.5f;
        #endregion
        #region Front Shield
        public static readonly string FrontShield_worldHitboxName = "FrontShieldWorldHitBox";
        public static readonly float FrontShield_cooldown = 0.1f;
        public static readonly float FrontShield_skillDuration = 0.50f;
        public static readonly float FrontShield_defaultScale = 0.7f;
        public static readonly float FrontShield_maxShieldHealthPercent = 0.33f;
        public static readonly float FrontShield_rechargeDelayAfterDamage = 3;
        public static readonly float FrontShield_rechargeDelayAfterDestroyed = 5;
        public static readonly float FrontShield_rechargeRatePercent = 0.1f;
        public static readonly float FrontShield_rechargeRatetime = 0.10f;
        public static readonly float FrontShield_moveSpeedMultiplier = 0.5f;
        public static readonly float FrontShield_addedforwardMultiplier = 1.1f;
        public static readonly float FrontShield_addedUpPosition = 0.5f;
        public static readonly float FrontShield_criticColorPercent = 0.30f;
        #endregion
        #region Claws Storm
        public static readonly string ClawsStorm_hitboxName = "ClawStorm";
        public static readonly float ClawsStorm_cooldown = 1;
        public static readonly int ClawsStorm_requiredFury = 3;
        public static readonly int ClawsStorm_continuousConsumedFury = 1;
        public static readonly float ClawsStorm_continuousConsumeTime = 1;
        public static readonly float ClawsStorm_destroyFXDelay = 0.5f;
        public static readonly DamageType ClawsStorm_damageType = DamageType.Generic;
        public static readonly float ClawsStorm_procCoefficient = 0.2f;
        public static readonly float ClawsStorm_pushForce = 1f;
        public static readonly Vector3 ClawsStorm_bonusForce = Vector3.zero;
        public static readonly float ClawsStorm_damageMultiplier = 0.50f;
        public static readonly float ClawsStorm_baseDuration = 0.50f;
        public static readonly float ClawsStorm_dashSpeedMultiplicator = 18f;
        public static readonly float ClawsStorm_maxMoveSpeed = 38;
        public static readonly float ClawsStorm_minMoveSpeed = 17;
        public static readonly float clawsStorm_damageMultiplier = 0.30f;
        public static readonly float ClawsStorm_firedDelay = 0.14f;
        #endregion
        #region Shield Bash
        public static readonly float ShieldBash_grabScanRadius = 8;
        public static readonly float ShieldBash_grabDistanceMultiplier = 2.5f;
        public static readonly float ShieldBash_moveSpeedMultiplier = 70;
        public static readonly float ShieldBash_cooldown = 4;
        public static readonly float ShieldBash_skillDuration = 0.50f;
        public static readonly float ShieldBash_effectDuration = 0.33f;
        public static readonly float ShieldBash_pushMinMultiplier = 1f;
        public static readonly float ShieldBash_pushMaxMultiplier = 10f;
        public static readonly float ShieldBash_upMinMultiplier = 1f;
        public static readonly float ShieldBash_upMaxMultiplier = 5f;
        public static readonly float ShieldBash_damageMultiplier = 3.5f;
        public static readonly float ShieldBash_procCoefficient = 1.5f;
        public static readonly float ShieldBash_stunDuration = 2;
        public static readonly float ShieldBash_magnetudeDecreasePercent = 0.9f;
        #endregion
        #region Arcane Anchor
        public static readonly float ArcaneAnchor_cooldown = 3;
        public static readonly float ArcaneAnchor_frontShieldCooldown = 2;
        public static readonly float ArcaneAnchor_deployedScale = 4;
        public static readonly float ArcaneAnchor_deployedScaleSpeed = 0.05f;
        public static readonly float ArcaneAnchor_skillDuration = 0.3f;
        public static readonly float ArcaneAnchor_MaxDistance = 75f;
        #endregion
        #region Convergence Hook
        public static readonly float ConvergenceHook_cooldown = 5;
        public static readonly float ConvergenceHook_skillDuration = 0.5f;
        public static readonly float ConvergenceHook_stunDuration = 2f;
        public static readonly float ConvergenceHook_compDuration = 5f;
        public static readonly float ConvergenceHook_hookSpeed = 2.5f;
        public static readonly float ConvergenceHook_StopDistance = 1f;
        #endregion

        // -- Abilities -- //
        #region Abilities ID
        public static readonly int UntamedSpirit_AbilityID = -1;
        public static readonly int GodPower_AbilityID = -2;
        public static readonly int PortalSurge_AbilityID = -3;
        public static readonly int FelineSkills_AbilityID = 1;
        public static readonly int SharpenedFangs_AbilityID = 2;
        public static readonly int Fury_AbilityID = 3;
        public static readonly int EnchantedFur_AbilityID = 4;
        public static readonly int Guardian_AbilityID = 5;
        public static readonly int WindWalker_AbilityID = 6;
        public static readonly int Predator_AbilityID = 7;
        public static readonly int Prowl_AbilityID = 8;
        public static readonly int Detection_AbilityID = 9;
        public static readonly int Ambition_AbilityID = 10;
        public static readonly int AirSlash_AbilityID = 11;
        public static readonly int FrontShield_AbilityID = 12;
        public static readonly int ClawsStorm_AbilityID = 13;
        public static readonly int SwiftMoves_AbilityID = 14;
        public static readonly int GhostRip_AbilityID = 15;
        public static readonly int ImprovedShield_AbilityID = 16;
        public static readonly int ShieldBash_AbilityID = 17;
        public static readonly int GoldenRip_AbilityID = 18;
        public static readonly int Tornado_AbilityID = 19;
        public static readonly int ArcaneAnchor_AbilityID = 20;
        public static readonly int ConvergenceHook_AbilityID = 21;
        public static readonly int MassiveHook_AbilityID = 22;
        public static readonly int SixthSense_AbilityID = 23;
        public static readonly int RelentlessStalker_AbilityID = 24;
        public static readonly int RoarOfResilience_AbilityID = 25;
        public static readonly int ClawsSharpening_AbilityID = 26;
        public static readonly int GoldenStart_AbilityID = 27;
        public static readonly int StealthStrike_AbilityID = 28;
        public static readonly int CryoLeap_AbilityID = 29;
        public static readonly int ShadowStalker_AbilityID = 30;
        public static readonly int WardensVitality_AbilityID = 31;
        public static readonly int SavageRevitalization_AbilityID = 32;
        public static readonly int InnateProtection_AbilityID = 33;
        public static readonly int Furrify_AbilityID = 34;
        public static readonly int Concentration_AbilityID = 35;
        public static readonly int SuperiorFlair_AbilityID = 36;
        public static readonly int EternalFury_AbilityID = 37;
        public static readonly int InnerRage_AbilityID = 38;
        public static readonly int InfernalSwipe_AbilityID = 39;
        public static readonly int HeatWave_AbilityID = 40;
        public static readonly int KineticResorption_AbilityID = 41;
        public static readonly int ExtendedProtection_AbilityID = 42;
        #endregion

        #region Untamed Spirit
        public static readonly int UntamedSpitit_unlockLevel = 1;
        #endregion
        #region God Power
        public static readonly int GodPower_unlockLevel = 15;
        #endregion
        #region Portal Surge
        public static readonly int PortalSurge_unlockLevel = 30;
        #endregion
        #region Sharpened Fangs
        public static readonly int SharpenedFangs_maxLevel = 3;
        public static readonly float SharpenedFangs_damagePercent1 = 0.2f;
        public static readonly float SharpenedFangs_damagePercent2 = 0.4f;
        public static readonly float SharpenedFangs_damagePercent3 = 0.6f;
        #endregion
        #region Fury
        public static readonly int Fury_maxLevel = 1;
        #endregion
        #region Enchanted Fur
        public static readonly int EnchantedFur_maxLevel = 3;
        public static readonly float EnchantedFur_percent1 = 0.05f;
        public static readonly float EnchantedFur_percent2 = 0.10f;
        public static readonly float EnchantedFur_percent3 = 0.15f;
        #endregion
        #region Guardian
        public static readonly int Guardian_maxLevel = 1;
        #endregion
        #region Wind Walker
        public static readonly int WindWalker_maxLevel = 1;
        #endregion
        #region Predator
        public static readonly int Predator_maxLevel = 1;
        public static readonly float Predator_steakHealthAdded = 25;
        #endregion
        #region Prowl
        public static readonly float Prowl_outOfCombatTime = 5;
        public static readonly int Prowl_maxLevel = 1;
        #endregion
        #region Detection
        public static readonly int Detection_maxLevel = 1;
        #endregion
        #region Ambition
        public static readonly int Ambition_maxLevel = 1;
        #endregion
        #region Air Slash
        public static readonly int AirSlash_maxLevel = 1;
        #endregion
        #region Front Shield
        public static readonly int FrontShield_maxLevel = 1;
        #endregion
        #region Claws Storm
        public static readonly int ClawsStorm_maxLevel = 1;
        #endregion
        #region Swift Moves
        public static readonly int SwiftMoves_maxLevel = 3;
        public static readonly float SwiftMoves_percent1 = 0.25f;
        public static readonly float SwiftMoves_percent2 = 0.50f;
        public static readonly float SwiftMoves_percent3 = 0.75f;
        #endregion
        #region Ghost Rip
        public static readonly int GhostRip_maxLevel = 1;
        public static readonly float GhostRip_cooldown = 0.3f;
        public static readonly float GhostRip_damageMultiplier = 1.5f;
        public static readonly float GhostRip_critMultiplier = 2f;
        public static readonly float GhostRip_stunDuration = 3.5f;
        public static readonly int GhostRip_furyAdded = 4;
        #endregion
        #region Improved Shield
        public static readonly int ImprovedShield_maxLevel = 3;
        public static readonly float ImprovedShield_addedPercent1 = 0.01f;
        public static readonly float ImprovedShield_addedPercent2 = 0.02f;
        public static readonly float ImprovedShield_addedPercent3 = 0.03f;
        public static readonly float ImprovedShield_RemovedRechargeDelay1 = 0.5f;
        public static readonly float ImprovedShield_RemovedRechargeDelay2 = 1f;
        public static readonly float ImprovedShield_RemovedRechargeDelay3 = 1.5f;
        #endregion
        #region Shield Bash
        public static readonly int ShieldBash_maxLevel = 1;
        #endregion
        #region Golden Rip
        public static readonly int GoldenRip_maxLevel = 3;
        public static readonly int GoldenRip_addedCoin1 = 1;
        public static readonly int GoldenRip_addedCoin2 = 2;
        public static readonly int GoldenRip_addedCoin3 = 3;
        public static readonly float GoldenRip_cooldown = 0.3f;
        public static readonly float GoldenRip_DamageMultiplier = 1.5f;
        public static readonly int GoldenRip_furyAdded = 2;
        #endregion
        #region Tornado
        public static readonly int Tornado_maxLevel = 3;
        public static readonly float Tornado_damagePercent1 = 0.33f;
        public static readonly float Tornado_damagePercent2 = 0.66f;
        public static readonly float Tornado_damagePercent3 = 1;
        public static readonly float Tornado_resistPercent1 = 0.25f;
        public static readonly float Tornado_resistPercent2 = 0.50f;
        public static readonly float Tornado_resistPercent3 = 0.75f;
        #endregion
        #region Arcane Anchor
        public static readonly int ArcaneAnchor_maxLevel = 1;
        #endregion
        #region Convergence Hook
        public static readonly int ConvergenceHook_maxLevel = 1;
        #endregion
        #region Massive Hook
        public static readonly int MassiveHook_maxLevel = 1;
        #endregion
        #region Sixth Sense
        public static readonly int SixthSense_maxLevel = 1;
        #endregion
        #region Relentless Stalker
        public static readonly int RelentlessStalker_maxLevel = 1;
        public static readonly int RelentlessStalker_CooldownReduction = 5;
        #endregion
        #region Roar Of Resilience
        public static readonly int RoarOfResilience_maxLevel = 1;
        #endregion
        #region Claws Sharpening
        public static readonly int ClawsSharpening_maxLevel = 1;
        #endregion
        #region Golden Start
        public static readonly int GoldenStart_maxLevel = 1;
        public static readonly int GoldenStart_addedGold = 25;
        #endregion
        #region Stealth Strike
        public static readonly int StealthStrike_maxLevel = 1;
        #endregion
        #region Cryo-Leap
        public static readonly int CryoLeap_maxLevel = 3;
        public static readonly float CryoLeap_duration1 = 1;
        public static readonly float CryoLeap_duration2 = 2;
        public static readonly float CryoLeap_duration3 = 3;
        #endregion
        #region Shadow Stalker
        public static readonly int ShadowStalker_maxLevel = 2;
        public static readonly float ShadowStalker_duration1 = 1;
        public static readonly float ShadowStalker_duration2 = 2;
        #endregion
        #region Warden's Vitality
        public static readonly int WardensVitality_maxLevel = 3;
        public static readonly float WardensVitality_maxHealthPercent1 = 1.1f;
        public static readonly float WardensVitality_maxHealthPercent2 = 1.2f;
        public static readonly float WardensVitality_maxHealthPercent3 = 1.3f;
        public static readonly int WardensVitality_BlockAdded1 = 1;
        public static readonly int WardensVitality_BlockAdded2 = 2;
        public static readonly int WardensVitality_BlockAdded3 = 3;
        #endregion
        #region Savage Revitalization
        public static readonly int SavageRevitalization_maxLevel = 1;
        public static readonly int SavageRevitalization_addedStack = 5;
        public static readonly float SavageRevitalization_buffTime = 5;
        public static readonly float SavageRevitalization_MasteryBuffTime = 10;
        #endregion
        #region Innate Protection
        public static readonly int InnateProtection_maxLevel = 2;
        public static readonly float InnateProtection_percent1 = 0.5f;
        public static readonly float InnateProtection_percent2 = 0.25f;
        #endregion
        #region Furrify
        public static readonly int Furrify_maxLevel = 1;
        public static readonly float Furrify_percent = 0.1f;
        #endregion
        #region Concentration
        public static readonly int Concentration_maxLevel = 1;
        #endregion
        #region Superior Flair
        public static readonly int SuperiorFlair_maxLevel = 5;
        public static readonly float SuperiorFlair_percent1 = 12;
        public static readonly float SuperiorFlair_percent2 = 16;
        public static readonly float SuperiorFlair_percent3 = 20;
        public static readonly float SuperiorFlair_percent4 = 24;
        public static readonly float SuperiorFlair_percent5 = 28;
        #endregion
        #region Eternal Fury
        public static readonly int EternalFury_maxLevel = 3;
        public static readonly float EternalFury_reductionPercent1 = 0.33f;
        public static readonly float EternalFury_reductionPercent2 = 0.66f;
        public static readonly float EternalFury_reductionPercent3 = 1.00f;
        public static readonly float EternalFury_startPercent1 = 0.20f;
        public static readonly float EternalFury_startPercent2 = 0.40f;
        public static readonly float EternalFury_startPercent3 = 0.60f;
        #endregion
        #region Inner Rage
        public static readonly int InnerRage_maxLevel = 4;
        public static readonly float InnerRage_addedFuryPercent1 = 0.03f;
        public static readonly float InnerRage_addedFuryPercent2 = 0.6f;
        public static readonly float InnerRage_addedFuryPercent3 = 0.9f;
        public static readonly float InnerRage_addedFuryPercent4 = 0.12f;
        public static readonly float InnerRage_enrageTime = 3;
        public static readonly float InnerRage_enrageTimeFuryMode = 5;
        #endregion
        #region Infernal Swipe
        public static readonly int InfernalSwipe_maxLevel = 2;
        public static readonly float InfernalSwipe_damagePercent1 = 0.25f;
        public static readonly float InfernalSwipe_damagePercent2 = 0.50f;
        public static readonly float InfernalSwipe_ingnitionChance1 = 0.15f;
        public static readonly float InfernalSwipe_ingnitionChance2 = 0.30f;
        public static readonly float InfernalSwipe_procCoefficient = 1.5f;
        #endregion
        #region Heat Wave
        public static readonly int HeatWave_maxLevel = 1;
        #endregion
        #region Kinetic Resorption
        public static readonly int KineticResorption_maxLevel = 3;
        public static readonly float KineticResorption_regenPercent1 = 0.10f;
        public static readonly float KineticResorption_regenPercent2 = 0.20f;
        public static readonly float KineticResorption_regenPercent3 = 0.30f;
        public static readonly float KineticResorption_orbsCreationDelay = 1;
        #endregion
        #region Extended Protection
        public static readonly int ExtendedProtection_maxLevel = 4;
        public static readonly float ExtendedProtection_percent1 = 0.1f;
        public static readonly float ExtendedProtection_percent2 = 0.2f;
        public static readonly float ExtendedProtection_percent3 = 0.3f;
        public static readonly float ExtendedProtection_percent4 = 0.4f;
        #endregion

        // -- Combos -- //
        #region Combos ID
        public static readonly int Rip_CombosID = 1;
        public static readonly int Slash_CombosID = 2;
        public static readonly int Leap_CombosID = 3;
        public static readonly int MightyRoar_CombosID = 4;
        public static readonly int Fury_CombosID = 5;
        public static readonly int Guardian_CombosID = 6;
        public static readonly int Prowl_CombosID = 7;
        public static readonly int Detection_CombosID = 8;
        public static readonly int AirSlash_CombosID = 9;
        public static readonly int FrontShield_CombosID = 10;
        public static readonly int ClawsStorm_CombosID = 11;
        public static readonly int ShieldBash_CombosID = 12;
        public static readonly int ArcaneAnchor_CombosID = 13;
        public static readonly int ConvergenceHook_CombosID = 14;
        public static readonly int MassiveHook_CombosID = 15;
        public static readonly int Ambition_CombosID = 16;
        public static readonly int PortalSurge_CombosID = 17;
        #endregion
        #region Combos Config
        public static float Combos_maxTime = 3;
        public static float Combos_failTimeFrame = 2.5f;
        #endregion

        // Buffs //
        #region Eclipse
        public static readonly float Eclipse_duration = 10;
        #endregion
        #region Cupidity
        public static readonly int Cupidity_maxStacks = 50;
        public static readonly float Cupidity_goldMultiplier = 0.05f;
        public static readonly float Cupidity_decreaseTime = 1;
        #endregion
        #region Tenacity
        public static readonly int Tenacity_maxStacks = 15;
        public static readonly float Tenacity_duration = 5;
        public static readonly float Tenacity_blockAdded = 0.02f;
        #endregion
        #region Razors
        public static readonly int Razors_maxStacks = 10;
        #endregion
        #region Mortal Mirage
        public static readonly float MortalMirage_duration = 4;
        #endregion
        #region Bleed Out
        public static readonly float BleedOut_duration = 5;
        public static readonly float BleedOut_damage = 0.1f;
        public static readonly float BleedOut_damageTime = 0.3f;
        #endregion
        #region Frozen Paws
        public static readonly float FrozenPaws_duration = 1;
        public static readonly float FrozenPaws_minGroundDistance = 5;
        #endregion
        #region Regeneration
        public static readonly float Regeneration_duration = 10;
        public static readonly int Regeneration_maxStack = 15;
        public static readonly float Regeneration_percentHeal = 0.01f;
        public static readonly float Regeneration_time = 1;
        #endregion
        #region Resilience
        public static readonly float Resilience_duration = 10;
        public static readonly int Resilience_maxStack = 15;
        public static readonly float Resilience_percentArmor = 0.05f;
        #endregion
        #region Enrage
        public static readonly float Enrage_duration = 5;
        public static readonly int Enrage_maxStack = 20;
        public static readonly float Enrage_furyPercent = 0.01f;
        #endregion
        #region Ignition
        public static readonly float Ignition_duration = 10;
        public static readonly float Ignition_damage = 0.5f;
        public static readonly float Ignition_damageTime = 1;
        #endregion

    }
}
