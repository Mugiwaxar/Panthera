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

        #region Keys
        public static readonly int Keys_OpenPantheraPanelActionCode = 129;
        public static readonly KeyCode Keys_DefaultOpenPantheraPanelKey = KeyCode.P;
        #endregion
        #region Color
        public static readonly Color32 ConfigButtonColor = new Color32(255, 255, 225, 255);
        public static readonly Color32 ConfigButtonHoveredColor = new Color32(255, 0, 0, 255);
        public static readonly Color32 ExitButtonHoveredColor = new Color32(255, 0, 0, 255);
        public static readonly Color32 TabButtonHoveredColor = new Color32(255, 0, 0, 255);
        public static readonly Color32 SkillTreeTabButtonHoveredColor = new Color32(150, 0, 0, 255);
        public static readonly Color32 SkillTreeAbilityHoveredColor = new Color32(0, 255, 0, 255);
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
        public static readonly Color32 PresetActivateButtonNormalColor = new Color32(255, 255, 255, 255);
        public static readonly Color32 PresetActivateButtonDisabledColor = new Color32(255, 255, 255, 75);
        public static readonly Color32 PresetResetButtonNormalColor = new Color32(255, 255, 255, 255);
        public static readonly Color32 PresetResetButtonDisabledColor = new Color32(255, 255, 255, 75);
        public static readonly Color32 DetectionDefaultColor = new Color32(0, 0, 255, 255);
        public static readonly Color32 DetectionCharacterDefaultColor = new Color32(0, 255, 0, 255);
        public static readonly Color32 DetectionCanAffortChestColor = new Color32(255, 224, 116, 255);
        #endregion
        #region Model
        public static readonly float Model_defaultModelScale = 1.5f;
        public static readonly string Model_PrefabName = "Panthera";
        public static readonly float Model_fadeStartDistance = 4;
        public static readonly float Model_fadeEndDistance = 7;
        #endregion
        #region Camera Parameters
        public static readonly Vector3 defaultCamPosition = new Vector3(0f, 1, -15);
        public static readonly Vector3 defaultGuardianCamPosition = new Vector3(0, 2, -20);
        public static readonly Vector3 Death_cameraPos = new Vector3(0f, 5, -50);
        public static readonly Vector3 ClawsStorm_cameraPos = new Vector3(0, 3, -40);
        #endregion
        #region Stats
        public static readonly float Default_MaxHealth = 180;
        public static readonly float Default_MaxHealthLevel = 25;
        public static readonly float Default_HealthRegen = 2f;
        public static readonly float Default_HealthRegenLevel = 0.15f;
        public static readonly float Default_Energy = 100;
        public static readonly float Default_EnergyRegen = 10;
        public static readonly float Default_DefaultStamina = 150;
        public static readonly float Default_StaminaRegen = 25;
        public static readonly float Default_MaxFury = 250;
        public static readonly float Default_MaxPower = 100;
        public static readonly float Default_MaxComboPoint = 5;
        public static readonly float Default_MaxShield = 100;
        public static readonly float Default_MoveSpeed = 8;
        public static readonly float Default_MoveSpeedLevel = 0.05f;
        public static readonly float Default_Damage = 15;
        public static readonly float Default_DamageLevel = 2;
        public static readonly float Default_AttackSpeed = 1f;
        public static readonly float Default_AttackSpeedLevel = 0.03f;
        public static readonly float Default_Critic = 10;
        public static readonly float Default_CriticLevel = 0.5f;
        public static readonly float Default_Defense = 18;
        public static readonly float Default_DefenseLevel = 0.4f;
        public static readonly float Default_jumpCount = 1;
        #endregion
        #region SkillsIDs
        public static readonly int Rip_SkillID = 1;
        public static readonly int AirCleave_SkillID = 2;
        public static readonly int Leap_SkillID = 3;
        public static readonly int MightyRoar_SkillID = 4;
        public static readonly int ClawsStorm_SkillID = 5;
        public static readonly int FrontShield_SkillID = 6;
        public static readonly int Prowl_SkillID = 7;
        public static readonly int FuriousBite_SkillID = 8;
        public static readonly int Dash_SkillID = 9;
        public static readonly int ShieldBash_SkillID = 10;
        public static readonly int ZoneHeal_SkillID = 11;
        public static readonly int WindWalker_SkillID = 12;
        public static readonly int TheRipper_SkillID = 13;
        public static readonly int PowerfullJaws_SkillID = 14;
        public static readonly int SaveMyFriend_SkillID = 15;
        public static readonly int ShieldOfPower_SkillID = 16;
        public static readonly int BurningSpirit_SkillID = 17;
        public static readonly int PassivePower_SkillID = 18;
        public static readonly int Slash_SkillID = 19;
        public static readonly int FireBird_SkillID = 20;
        public static readonly int Revive_SkillID = 21;
        public static readonly int Detection_SkillID = 22;
        public static readonly int Regeneration_SkillID = 23;
        #endregion
        #region AbilityIDs
        public static readonly int DestructionAbilityID = 1;
        public static readonly int GuardianAbilityID = 2;
        public static readonly int RuseAbilityID = 3;
        public static readonly int LeapAbilityID = 4;
        public static readonly int ImprovedClawsStormAbilityID = 5;
        public static readonly int HealingStormAbilityID = 6;
        public static readonly int TornadoAbilityID = 7;
        public static readonly int SharpenedFrangsAbilityID = 8;
        public static readonly int PowerfullJawsAbilityID = 9;
        public static readonly int PredatorsDrinkAbilityID = 10;
        public static readonly int ShadowsMasterAbilityID = 11;
        public static readonly int SilentPredatorAbilityID = 12;
        public static readonly int PrimalStalkerAbilityID = 13;
        public static readonly int ShieldFocusAbilityID = 14;
        public static readonly int ResidualEnergyAbilityID = 15;
        public static readonly int DefensiveHasteAbilityID = 16;
        public static readonly int DashAbilityID = 17;
        public static readonly int ShieldBashAbilityID = 18;
        public static readonly int KineticAbsorbtionAbilityID = 19;
        public static readonly int ZoneHealAbilityID = 20;
        public static readonly int WindWalkerAbilityID = 21;
        public static readonly int PerspicacityAbilityID = 22;
        public static readonly int HealingCleaveAbilityID = 23;
        public static readonly int TheRipperAbilityID = 24;
        public static readonly int InstinctiveResistanceAbilityID = 25;
        public static readonly int BloodyRageAbilityID = 26;
        public static readonly int GodOfReapersAbilityID = 27;
        public static readonly int SaveMyFriendAbilityID = 28;
        public static readonly int ShieldOfPowerAbilityID = 29;
        public static readonly int GhostRipAbilityID = 30;
        public static readonly int ProwlAbilityID = 31;
        public static readonly int MightyRoarAbilityID = 32;
        public static readonly int EchoAbilityID = 33;
        public static readonly int PiercingWavesAbilityID = 34;
        public static readonly int BurningSpiritAbilityID = 35;
        public static readonly int HellCatAbilityID = 36;
        public static readonly int SlashAbilityID = 37;
        public static readonly int CircularSawAbilityID = 38;
        public static readonly int IgnitionAbilityID = 39;
        public static readonly int SoulsShelterAbilityID = 40;
        public static readonly int TheSlashPerAbilityID = 41;
        public static readonly int HighTemperatureAbilityID = 42;
        public static readonly int SacredFlamesAbilityID = 43;
        public static readonly int FireBirdAbilityID = 44;
        public static readonly int AngryBirdAbilityID = 45;
        public static readonly int PassivePowerAbilityID = 46;
        public static readonly int ReviveAbilityID = 47;
        public static readonly int DetectionAbilityID = 48;
        public static readonly int PrescienceAbilityID = 49;
        public static readonly int ConcentrationAbilityID = 50;
        public static readonly int PrecisionAbilityID = 51;
        public static readonly int DeterminationAbilityID = 52;
        public static readonly int RegenerationAbilityID = 53;
        public static readonly int StrongBarrierAbilityID = 54;
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
        #region Keys
        public static readonly int JumpKey = 4;
        public static readonly int InteractKey = 5;
        public static readonly int EquipmentKey = 6;
        public static readonly int SprintKey = 18;
        public static readonly int PingKey = 28;
        public static readonly int SwitchBarKey = 130;
        public static readonly int Skill1Key = 7;
        public static readonly int Skill2Key = 8;
        public static readonly int Skill3Key = 9;
        public static readonly int Skill4Key = 10;
        public static readonly int Skill5Key = 135;
        public static readonly int Skill6Key = 136;
        public static readonly int Skill7Key = 137;
        public static readonly int Skill8Key = 138;
        public static readonly int Skill9Key = 139;
        public static readonly int Skill10Key = 1310;
        #endregion
        #region Tracker
        public static readonly float Tracker_maxDistance = 100;
        public static readonly float Tracker_maxAngle = 20;
        #endregion
        #region Motor
        public static readonly float normalMaxAngle = 65;
        public static readonly float superSprintMaxAngle = 90;
        public static readonly float unsleepDuration = 2.2f;
        #endregion
        #region Priorities
        public static readonly ScriptPriority Rip_priority = ScriptPriority.MinimumPriority;
        public static readonly ScriptPriority Rip_interruptPower = ScriptPriority.MinimumPriority;

        public static readonly ScriptPriority AirCleave_priority = ScriptPriority.MinimumPriority;
        public static readonly ScriptPriority AirCleave_interruptPower = ScriptPriority.MinimumPriority;

        public static readonly ScriptPriority Leap_priority = ScriptPriority.SmallPriority;
        public static readonly ScriptPriority Leap_interruptPower = ScriptPriority.VerySmallPriority;

        public static readonly ScriptPriority MightyRoar_priority = ScriptPriority.AveragePriority;
        public static readonly ScriptPriority MightyRoar_interruptPower = ScriptPriority.HightPriority;

        public static readonly ScriptPriority ClawsStorm_priority = ScriptPriority.SmallPriority;
        public static readonly ScriptPriority ClawsStorm_interruptPower = ScriptPriority.SmallPriority;

        public static readonly ScriptPriority FrontShield_priority = ScriptPriority.MinimumPriority;
        public static readonly ScriptPriority FrontShield_interruptPower = ScriptPriority.AveragePriority;

        public static readonly ScriptPriority Prowl_priority = ScriptPriority.MaximumPriority;
        public static readonly ScriptPriority Prowl_interruptPower = ScriptPriority.ExtraPriority;

        public static readonly ScriptPriority FuriousBite_priority = ScriptPriority.AveragePriority;
        public static readonly ScriptPriority FuriousBite_interruptPower = ScriptPriority.AveragePriority;

        public static readonly ScriptPriority Dash_priority = ScriptPriority.MinimumPriority;
        public static readonly ScriptPriority Dash_interruptPower = ScriptPriority.MinimumPriority;

        public static readonly ScriptPriority ShieldBash_priority = ScriptPriority.VeryHightPriority;
        public static readonly ScriptPriority ShieldBash_interruptPower = ScriptPriority.ExtraPriority;

        public static readonly ScriptPriority ZoneHeal_priority = ScriptPriority.MinimumPriority;
        public static readonly ScriptPriority ZoneHeal_interruptPower = ScriptPriority.AveragePriority;

        public static readonly ScriptPriority SaveMyFriend_priority = ScriptPriority.ExtraPriority;
        public static readonly ScriptPriority SaveMyFriend_interruptPower = ScriptPriority.ExtraPriority;

        public static readonly ScriptPriority Slash_priority = ScriptPriority.VerySmallPriority;
        public static readonly ScriptPriority Slash_interruptPower = ScriptPriority.VerySmallPriority;

        public static readonly ScriptPriority FireBird_priority = ScriptPriority.ExtraPriority;
        public static readonly ScriptPriority FireBird_interruptPower = ScriptPriority.AveragePriority;

        public static readonly ScriptPriority Revive_priority = ScriptPriority.NoPriority;
        public static readonly ScriptPriority Revive_interruptPower = ScriptPriority.NoPriority;

        public static readonly ScriptPriority Detection_priority = ScriptPriority.MaximumPriority;
        public static readonly ScriptPriority Detection_interruptPower = ScriptPriority.NoPriority;

        //public static readonly ScriptPriority Mangle_priority = ScriptPriority.VeryHightPriority;
        //public static readonly ScriptPriority Mangle_interruptPower = ScriptPriority.AveragePriority;

        //public static readonly ScriptPriority LeapCercle_priority = ScriptPriority.MaximumPriority;
        //public static readonly ScriptPriority LeapCercle_interruptPower = ScriptPriority.ExtraPriority;

        //public static readonly ScriptPriority RaySlash_priority = ScriptPriority.ExtraHightPriority;
        //public static readonly ScriptPriority RaySlash_interruptPower = ScriptPriority.ExtraHightPriority;
        #endregion
        #region Item Change
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
        public static readonly float ItemChange_steakHealthAdded = 50;
        public static readonly float ItemChange_magazinePercentCooldownReduction = 0.95f;
        public static readonly float ItemChange_alienHeadPercentCooldownReduction = 0.85f;
        public static readonly float ItemChange_hardlightAfterburnerPercentCooldownReduction = 0.75f;
        public static readonly float ItemChange_lightFluxPauldronPercentCooldownReduction = 0.75f;
        public static readonly float ItemChange_purityPercentCooldownReduction = 0.80f;
        public static readonly float ItemChange_lysateCellCooldownReduction = 0.85f;
        public static readonly float ItemChange_noCooldownTimeRemoved = 0.02f;
        #endregion
        #region Preset
        public static readonly int ResetPresetCost = 5;
        public static readonly int ActivatePresetCost = 1;
        #endregion
        #region Experience
        public static readonly float ExperienceDivider = 20;
        public static readonly int LevelsRequiredForMultipleSkillsTree = 40;
        #endregion
        #region Death
        public static readonly float Death_effectStartTime = 3;
        public static readonly float Death_minimumTimeBeforeDestroying = 6;
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

        #region Rip
        public static readonly string Rip_hitboxName = "Rip";
        public static readonly float Rip_cooldown = 0.3f;
        public static readonly int Rip_requiredEnergy = 0;
        public static readonly DamageType Rip_damageType = DamageType.Generic;
        public static readonly float Rip_procCoefficient = 1f;
        public static readonly float Rip_pushForce = 300f;
        public static readonly Vector3 Rip_bonusForce = Vector3.zero;
        public static readonly float Rip_atk1DamageMultiplier = 2f;
        public static readonly float Rip_atk2DamageMultiplier = 2f;
        public static readonly float Rip_atk3DamageMultiplier = 4f;
        public static readonly float Rip_atk1BaseDuration = 0.5f;
        public static readonly float Rip_atk2BaseDuration = 0.5f;
        public static readonly float Rip_atk3BaseDuration = 0.8f;
        public static readonly float Rip_distanceAttackActivation = 7;
        public static readonly float Rip_weekDebuffDuration = 10;
        public static readonly float Rip_minimumAimTime = 1f;
        public static readonly float Rip_weakDuration = 5;
        public static readonly string RightRip_hitboxName = "RightRip";
        public static readonly float RightRip_atkDamageMultiplier = 1.3f;
        public static readonly float RightRip_atkBaseDuration = 0.6f;
        public static readonly float RightRip_rightVelocityMultiplier = 17;
        public static readonly float RightRip_rightVelocityTime = 0.25f;
        public static readonly string LeftRip_hitboxName = "LeftRip";
        public static readonly float LeftRip_atkDamageMultiplier = 1.3f;
        public static readonly float LeftRip_atkBaseDuration = 0.6f;
        public static readonly float LeftRip_leftVelocityMultiplier = 17;
        public static readonly float LeftRip_leftVelocityTime = 0.25f;
        public static readonly string FrontRip_hitboxName = "FrontRip";
        public static readonly float FrontRip_atkDamageMultiplier = 1.5f;
        public static readonly float FrontRip_atkBaseDuration = 0.8f;
        public static readonly float FrontRip_forwardVelocityMultiplier = 22;
        public static readonly float FrontRip_forwardVelocityTime = 0.30f;
        #endregion
        #region AirCleave
        public static readonly string AirCleave_leftProjectileName = "AirCleaveLeftProjectile";
        public static readonly string AirCleave_rightProjectileName = "AirCleaveRightProjectile";
        public static readonly string FireAirCleave_leftProjectileName = "FireAirCleaveLeftProjectile";
        public static readonly string FireAirCleave_rightProjectileName = "FireAirCleaveRightProjectile";
        public static readonly float AirCleave_projScale = 0.5f;
        public static readonly float AirCleave_cooldown = 0.3f;
        public static readonly int AirCleave_requiredEnergy = 10;
        public static readonly float AirCleave_atk1BaseDuration = 0.5f;
        public static readonly float AirCleave_atk2BaseDuration = 0.5f;
        public static readonly float AirCleave_atk1DamageMultiplier = 1.5f;
        public static readonly float AirCleave_atk2DamageMultiplier = 1.5f;
        public static readonly float AirCleave_projectileSpeed = 130;
        public static readonly float AirCleave_projectileForce = 15;
        public static readonly float AirCleave_minimumAimTime = 1f;
        #endregion
        #region Leap
        public static readonly int Leap_unlockLevel = 7;
        public static readonly int Leap_maxLevel = 3;
        public static readonly int Leap_requiredStamina = 30;
        public static readonly float Leap_cooldown1 = 13;
        public static readonly float Leap_cooldown2 = 10;
        public static readonly float Leap_cooldown3 = 7;
        public static readonly float Leap_targetReduction1 = 1;
        public static readonly float Leap_targetReduction2 = 2;
        public static readonly float Leap_targetReduction3 = 3;
        public static readonly float Leap_airControl = 0.10f;
        public static readonly float Leap_airControlTarget = 0.50f;
        public static readonly float Leap_aimRayYMultiplier = 15;
        public static readonly float Leap_minimumY = 5f;
        public static readonly float Leap_minimumYTarget = 10f;
        public static readonly float Leap_speedMultiplier = 3.5f;
        public static readonly float Leap_targetSpeedMultiplier = 4;
        public static readonly float Leap_maxMoveSpeed = 100;
        public static readonly float Leap_upwardVelocity = 2.7f;
        public static readonly float Leap_downwardMultiplier = 2.2f;
        public static readonly float Leap_minimumDuration = 0.7f;
        public static readonly float Leap_duration = 3;
        public static readonly float Leap_naturalDown = 0.5f;
        public static readonly float Leap_leapStopDistance = 2f;
        public static readonly float Leap_leapScanRadius = 2.5f;
        public static readonly float Leap_destroyComponentDelay = 0.5f;
        #endregion
        #region Mighty Roar
        public static readonly int MightyRoar_unlockLevel = 15;
        public static readonly int MightyRoar_maxLevel = 1;
        public static readonly int MightyRoar_energyRequired = 40;
        public static readonly float MightyRoar_cooldown = 45;
        public static readonly float MightyRoar_radius = 15;
        public static readonly float MightyRoar_stunDuration = 2f;
        public static readonly float MightyRoar_duration = 0.5f;
        #endregion
        #region ClawsStorm
        public static readonly string ClawsStorm_hitboxName = "ClawStorm";
        public static readonly float ClawsStorm_cooldown = 5;
        public static readonly int ClawsStorm_requiredFury = 3;
        public static readonly int ClawsStorm_continuousConsumedFury = 1;
        public static readonly float ClawsStorm_continuousConsumeTime = 1;
        public static readonly float ClawsStorm_destroyFXDelay = 0.5f;
        public static readonly DamageType ClawsStorm_damageType = DamageType.Generic;
        public static readonly float ClawsStorm_procCoefficient = 1f;
        public static readonly float ClawsStorm_pushForce = 1f;
        public static readonly Vector3 ClawsStorm_bonusForce = Vector3.zero;
        public static readonly float ClawsStorm_damageMultiplier = 0.50f;
        public static readonly float ClawsStorm_baseDuration = 0.50f;
        public static readonly float ClawsStorm_dashSpeedMultiplicator = 18f;
        public static readonly float ClawsStorm_maxMoveSpeed = 38;
        public static readonly float ClawsStorm_minMoveSpeed = 17;
        public static readonly float ClawsStorm_firedDelay = 0.16f;
        #endregion
        #region Front Shield
        public static readonly float FrontShield_cooldown = 0.1f;
        public static readonly float FrontShield_rechargeDelayAfterDamage = 3;
        public static readonly float FrontShield_rechargeDelayAfterDestroyed = 5;
        public static readonly float FrontShield_maxShieldHealthPercent = 0.15f;
        public static readonly float FrontShield_rechargeRatePercent = 0.1f;
        public static readonly float FrontShield_rechargeRatetime = 0.10f;
        public static readonly float FrontShield_damageDecreaseMultiplier = 1;
        #endregion
        #region Prow
        public static readonly int Prowl_unlockLevel = 7;
        public static readonly int Prowl_maxLevel = 1;
        public static readonly float Prowl_coolDown = 30;
        public static readonly float Prowl_moveSpeedReduction = 5;
        public static readonly float Prowl_skillDuration = 0.3f;
        #endregion
        #region Furious Bite
        public static readonly float FuriousBite_cooldown = 8f;
        public static readonly int FuriousBite_energyRequired = 40;
        public static readonly int FuriousBite_maxComboPointUsed = 5;
        public static readonly float FuriousBite_atkDamageMultiplier = 2.5f;
        public static readonly float FuriousBite_ComboPointMultiplier = 0.5f;
        public static readonly float FuriousBite_atkBaseDuration = 0.5f;
        public static readonly float FuriousBite_detectionRadius = 4;
        #endregion
        #region Dash
        public static readonly int Dash_unlockLevel = 5;
        public static readonly int Dash_maxLevel = 5;
        public static readonly float Dash_skillDuration = 0.3f;
        public static readonly float Dash_cooldown = 0.3f;
        public static readonly float Dash_speedMultiplier1 = 2.2f;
        public static readonly float Dash_speedMultiplier2 = 2.4f;
        public static readonly float Dash_speedMultiplier3 = 2.6f;
        public static readonly float Dash_speedMultiplier4 = 2.8f;
        public static readonly float Dash_speedMultiplier5 = 3;
        public static readonly float Dash_staminaConsumed1 = 26;
        public static readonly float Dash_staminaConsumed2 = 22;
        public static readonly float Dash_staminaConsumed3 = 18;
        public static readonly float Dash_staminaConsumed4 = 14;
        public static readonly float Dash_staminaConsumed5 = 10;
        #endregion
        #region Shield Bash
        public static readonly int ShieldBash_maxLevel = 3;
        public static readonly int ShieldBash_unlockLevel = 8;
        public static readonly int ShieldBash_requiredEnergy = 30;
        public static readonly float ShieldBash_grabScanRadius = 8;
        public static readonly float ShieldBash_grabDistanceMultiplier = 2.5f;
        public static readonly float ShieldBash_moveSpeedMultiplier = 100;
        public static readonly float ShieldBash_cooldown = 4;
        public static readonly float ShieldBash_skillDuration = 0.17f;
        public static readonly float ShieldBash_pushMinMultiplier = 1f;
        public static readonly float ShieldBash_pushMaxMultiplier = 5f;
        public static readonly float ShieldBash_upMinMultiplier = 1f;
        public static readonly float ShieldBash_upMaxMultiplier = 3f;
        public static readonly float ShieldBash_damage1 = 3;
        public static readonly float ShieldBash_damage2 = 5;
        public static readonly float ShieldBash_damage3 = 7;
        public static readonly float ShieldBash_stunDuration1 = 3;
        public static readonly float ShieldBash_stunDuration2 = 4;
        public static readonly float ShieldBash_stunDuration3 = 5;
        #endregion
        #region Zone Heal
        public static readonly int ZoneHeal_maxLevel = 3;
        public static readonly int ZoneHeal_unlockLevel = 10;
        public static readonly float ZoneHeal_cooldown1 = 60;
        public static readonly float ZoneHeal_cooldown2 = 45;
        public static readonly float ZoneHeal_cooldown3 = 30;
        public static readonly int ZoneHeal_RequiredPower = 10;
        public static readonly float ZoneHeal_fxScale = 1.5f;
        public static readonly float ZoneHeal_skillDuration = 0.3f;
        public static readonly float ZoneHeal_healDuration = 10f;
        public static readonly float ZoneHeal_healRate = 1;
        public static readonly float ZoneHeal_percentHeal1 = 0.05f;
        public static readonly float ZoneHeal_percentHeal2 = 0.075f;
        public static readonly float ZoneHeal_percentHeal3 = 0.1f;
        public static readonly float ZoneHeal_radius = 6.5f;
        #endregion
        #region Slash
        public static readonly int Slash_maxLevel = 1;
        public static readonly int Slash_unlockLevel = 5;
        public static readonly int Slash_energyRequired = 50;
        public static readonly int Slash_Cooldown = 5;
        public static readonly float Slash_duration = 0.8f;
        public static readonly float Slash_radius = 7;
        public static readonly float Slash_damageMultiplier = 5;
        #endregion
        #region Fire Bird
        public static readonly string FireBird_projectileName = "FireBirdProjectile";
        public static readonly float FireBird_projectileSpeed = 35;
        public static readonly int FireBird_maxLevel = 1;
        public static readonly int FireBird_unlockLevel = 18;
        public static readonly int FireBird_furyRequired = 80;
        public static readonly int FireBird_Cooldown = 30;
        public static readonly float FireBird_duration = 1;
        public static readonly float FireBird_damageMultiplier = 35;
        public static readonly float FireBird_burnDamageMultiplier = 20;
        public static readonly float FireBird_burnDuration = 10;
        #endregion
        #region Revive
        public static readonly int Revive_maxLevel = 1;
        public static readonly int Revive_unlockLevel = 15;
        public static readonly int Revive_powerRequired = 50;
        public static readonly int Revive_cooldown = 60;
        public static readonly int Revive_failCooldown = 3;
        public static readonly float Revive_duration = 8;
        public static readonly float Revive_CheckTargetDuration = 2;
        public static readonly float Revive_targetForwardMultiplier = 5;
        #endregion
        #region Detection
        public static readonly int Detection_maxLevel = 1;
        public static readonly int Detection_unlockLevel = 12;
        public static readonly int Detection_coolDown = 1;
        public static readonly float Detection_scanRate = 0.5f;
        public static readonly float Detection_skillMinDuration = 0.3f;
        public static readonly float Detection_skillMaxDuration = 2.5f;
        public static readonly int Detection_maxScanPerFrame = 100;
        public static readonly float Detection_staminaConsumed = 20;
        public static readonly int DetectionLayerIndex = 31;
        #endregion

        #region Destruction
        public static readonly int Fury_pointsGenerated = 1;
        public static readonly int Fury_pointsGenerationCooldown = 3;
        public static readonly float Fury_startingFury = 50;
        public static readonly int Fury_damageTimeBeforStopGenerate = 3;
        public static readonly float Destruction_addedDamage = 3;
        public static readonly float Destruction_addedDamageLevel = 0.2f;
        public static readonly float Destruction_addedAttackSpeed = 0.2f;
        public static readonly float Destruction_addedAttackSpeedLevel = 0.01f;
        #endregion
        #region Guardian
        public static readonly float Guardian_modelScale = 2;
        public static readonly float Guardian_addedHealth = 100;
        public static readonly float Guardian_addedHealthLevel = 10;
        public static readonly float Guardian_addedHealthRegen = 1f;
        public static readonly float Guardian_addedHealthRegenLevel = 0.5f;
        public static readonly float Guardian_addedDefense = 15;
        public static readonly float Guardian_addedDefenseLevel = 0.2f;
        #endregion
        #region Ruse
        public static readonly float Ruse_addedCrit = 10;
        public static readonly float Ruse_addedCritLevel = 0.3f;
        #endregion
        #region Improved Claws Storm
        public static readonly int ImprovedClawsStorm_maxLevel = 5;
        public static readonly float ImprovedClawsStorm_cooldown1 = 4;
        public static readonly float ImprovedClawsStorm_cooldown2 = 3;
        public static readonly float ImprovedClawsStorm_cooldown3 = 2;
        public static readonly float ImprovedClawsStorm_cooldown4 = 1;
        public static readonly float ImprovedClawsStorm_cooldown5 = 0;
        public static readonly float  ImprovedClawsStorm_DamageIncrease1 = 0.60f;
        public static readonly float  ImprovedClawsStorm_DamageIncrease2 = 0.70f;
        public static readonly float  ImprovedClawsStorm_DamageIncrease3 = 0.8f;
        public static readonly float ImprovedClawsStorm_DamageIncrease4 = 0.9f;
        public static readonly float  ImprovedClawsStorm_DamageIncrease5 = 1;
        #endregion
        #region Healing Storm
        public static readonly int HealingStorm_unlockLevel = 3;
        public static readonly int HealingStorm_maxLevel = 5;
        public static readonly float HealingStrom_percent1 = 0.05f;
        public static readonly float HealingStrom_percent2 = 0.10f;
        public static readonly float HealingStrom_percent3 = 0.15f;
        public static readonly float HealingStrom_percent4 = 0.20f;
        public static readonly float HealingStrom_percent5 = 0.25f;
        #endregion
        #region Tornado
        public static readonly int Tornado_unlockLevel = 5;
        public static readonly int Tornado_maxLevel = 3;
        public static readonly float Tornado_speed1 = 0.14f;
        public static readonly float Tornado_speed2 = 0.12f;
        public static readonly float Tornado_speed3 = 0.10f;
        #endregion
        #region Sharpened Fangs
        public static readonly int SharpenedFangs_maxLevel = 5;
        public static readonly int SharpenedFangs_energyCost1 = 36;
        public static readonly int SharpenedFangs_energyCost2 = 32;
        public static readonly int SharpenedFangs_energyCost3 = 28;
        public static readonly int SharpenedFangs_energyCost4 = 24;
        public static readonly int SharpenedFangs_energyCost5 = 20;
        public static readonly float SharpenedFangs_baseDamageMultiplier1 = 3f;
        public static readonly float SharpenedFangs_baseDamageMultiplier2 = 3.5f;
        public static readonly float SharpenedFangs_baseDamageMultiplier3 = 4;
        public static readonly float SharpenedFangs_baseDamageMultiplier4 = 4.5f;
        public static readonly float SharpenedFangs_baseDamageMultiplier5 = 5;
        public static readonly float SharpenedFangs_comboDamageMultiplier1 = 0.7f;
        public static readonly float SharpenedFangs_comboDamageMultiplier2 = 0.9f;
        public static readonly float SharpenedFangs_comboDamageMultiplier3 = 1.1f;
        public static readonly float SharpenedFangs_comboDamageMultiplier4 = 1.3f;
        public static readonly float SharpenedFangs_comboDamageMultiplier5 = 1.5f;
        #endregion
        #region Powerfull Jaws
        public static readonly int PowerfullJaws_maxLevel = 1;
        public static readonly int PowerfullJaws_RequiredLevel = 10;
        public static readonly int PowerfullJaws_cooldown = 10;
        #endregion
        #region Predator's Drink
        public static readonly int PredatorsDrink_maxLevel = 5;
        public static readonly int PredatorsDrink_unlockLevel = 3;
        public static readonly float PredatorsDrink_basePercent = 0.05f;
        public static readonly float PredatorsDrink_percent1 = 0.01f;
        public static readonly float PredatorsDrink_percent2 = 0.02f;
        public static readonly float PredatorsDrink_percent3 = 0.03f;
        public static readonly float PredatorsDrink_percent4 = 0.04f;
        public static readonly float PredatorsDrink_percent5 = 0.05f;
        #endregion
        #region Shadow's Master
        public static readonly int ShadowsMaster_maxLevel = 3;
        public static readonly int ShadowsMaster_unlockLevel = 8;
        public static readonly float ShadowsMaster_cooldown1 = 25;
        public static readonly float ShadowsMaster_cooldown2 = 20;
        public static readonly float ShadowsMaster_cooldown3 = 15;
        public static readonly float ShadowsMaster_cooldownReduction1 = 7;
        public static readonly float ShadowsMaster_cooldownReduction2 = 5;
        public static readonly float ShadowsMaster_cooldownReduction3 = 3;
        #endregion
        #region Silent Predator
        public static readonly int SilentPredator_maxLevel = 3;
        public static readonly int SilentPredator_unlockLevel = 10;
        public static readonly float SilentPredator_reduction1 = 0.33f;
        public static readonly float SilentPredator_reduction2 = 0.66f;
        public static readonly float SilentPredator_reduction3 = 1;
        #endregion
        #region Primal Stalker
        public static readonly int PrimalStalker_maxLevel = 3;
        public static readonly int PrimalStalker_unlockLevel = 15;
        public static readonly float PrimalStalker_fixedTime1 = 1;
        public static readonly float PrimalStalker_fixedTime2 = 2;
        public static readonly float PrimalStalker_fixedTime3 = 3;
        #endregion
        #region Shield Focus
        public static readonly int ShieldFocus_maxLevel = 9;
        public static readonly float ShieldFocus_healthPercent1 = 0.20f;
        public static readonly float ShieldFocus_healthPercent2 = 0.25f;
        public static readonly float ShieldFocus_healthPercent3 = 0.30f;
        public static readonly float ShieldFocus_healthPercent4 = 0.35f;
        public static readonly float ShieldFocus_healthPercent5 = 0.40f;
        public static readonly float ShieldFocus_healthPercent6 = 0.45f;
        public static readonly float ShieldFocus_healthPercent7 = 0.50f;
        public static readonly float ShieldFocus_healthPercent8 = 0.55f;
        public static readonly float ShieldFocus_healthPercent9 = 0.60f;
        public static readonly float ShieldFocus_damagePercent1 = 0.95f;
        public static readonly float ShieldFocus_damagePercent2 = 0.90f;
        public static readonly float ShieldFocus_damagePercent3 = 0.85f;
        public static readonly float ShieldFocus_damagePercent4 = 0.80f;
        public static readonly float ShieldFocus_damagePercent5 = 0.75f;
        public static readonly float ShieldFocus_damagePercent6 = 0.70f;
        public static readonly float ShieldFocus_damagePercent7 = 0.65f;
        public static readonly float ShieldFocus_damagePercent8 = 0.60f;
        public static readonly float ShieldFocus_damagePercent9 = 0.55f;
        #endregion
        #region Residual Energy
        public static readonly int ResidualEnergy_maxLevel = 5;
        public static readonly int ResidualEnergy_unlockLevel = 5;
        public static readonly float ResidualEnergy_percent1 = 0.5f;
        public static readonly float ResidualEnergy_percent2 = 0.6f;
        public static readonly float ResidualEnergy_percent3 = 0.7f;
        public static readonly float ResidualEnergy_percent4 = 0.8f;
        public static readonly float ResidualEnergy_percent5 = 0.9f;
        public static readonly float ResidualEnergy_decay = 0.25f;
        #endregion
        #region Defensive Haste
        public static readonly int DefensiveHaste_maxLevel = 4;
        public static readonly int DefensiveHaste_unlockLevel = 4;
        public static readonly float DefensiveHaste_damageDelay1 = 2.5f;
        public static readonly float DefensiveHaste_damageDelay2 = 2;
        public static readonly float DefensiveHaste_damageDelay3 = 1.5f;
        public static readonly float DefensiveHaste_damageDelay4 = 1;
        public static readonly float DefensiveHaste_destroyedDelay1 = 4.5f;
        public static readonly float DefensiveHaste_destroyedDelay2 = 4;
        public static readonly float DefensiveHaste_destroyedDelay3 = 3.5f;
        public static readonly float DefensiveHaste_destroyedDelay4 = 3;
        #endregion
        #region Kinetic Absorbtion
        public static readonly int KineticAbsorbtion_maxLevel = 3;
        public static readonly int KineticAbsorbtion_unlockLevel = 10;
        public static readonly float KineticAbsorbtion_percent1 = 0.02f;
        public static readonly float KineticAbsorbtion_percent2 = 0.04f;
        public static readonly float KineticAbsorbtion_percent3 = 0.06f;
        #endregion
        #region WindWalker
        public static readonly int WindWalker_unlockLevel = 3;
        public static readonly int WindWalker_maxLevel = 1;
        #endregion
        #region Perspicacity
        public static readonly int Perspicacity_maxLevel = 3;
        public static readonly int Perspicacity_unlockLevel = 5;
        public static readonly float Perspicacity_percent1 = 0.1f;
        public static readonly float Perspicacity_percent2 = 0.2f;
        public static readonly float Perspicacity_percent3 = 0.3f;
        #endregion
        #region Healing Cleave
        public static readonly int HealingCleave_maxLevel = 3;
        public static readonly int HealingCleave_unlockLevel = 8;
        public static readonly float HealingCleave_percent1 = 0.033f;
        public static readonly float HealingCleave_percent2 = 0.066f;
        public static readonly float HealingCleave_percent3 = 0.100f;
        #endregion
        #region The Rip-per
        public static readonly int TheRipper_maxLevel = 1;
        public static readonly int TheRipper_unlockLevel = 5;
        public static readonly int TheRipper_maxStack = 10;
        public static readonly float TheRipper_damageIncreasePercent = 0.03f;
        public static readonly float TheRipper_speedIncreasePercent = 0.03f;
        public static readonly float TheRipper_buffDuration = 5;
        #endregion
        #region Instinctive Resistance
        public static readonly int InstinctiveResistance_unlockLevel = 7;
        public static readonly int InstinctiveResistance_maxLevel = 6;
        public static readonly float InstinctiveResistance_percent1 = 0.01f;
        public static readonly float InstinctiveResistance_percent2 = 0.015f;
        public static readonly float InstinctiveResistance_percent3 = 0.020f;
        public static readonly float InstinctiveResistance_percent4 = 0.025f;
        public static readonly float InstinctiveResistance_percent5 = 0.030f;
        public static readonly float InstinctiveResistance_percent6 = 0.035f;
        #endregion
        #region Bloody Rage
        public static readonly int BloodyRage_unlockLevel = 10;
        public static readonly int BloodyRage_maxLevel = 5;
        public static readonly float BloodyRage_Percent1 = 0.01f;
        public static readonly float BloodyRage_Percent2 = 0.015f;
        public static readonly float BloodyRage_Percent3 = 0.02f;
        public static readonly float BloodyRage_Percent4 = 0.025f;
        public static readonly float BloodyRage_Percent5 = 0.03f;
        #endregion
        #region God Of Reapers
        public static readonly int GodOfReapers_unlockLevel = 15;
        public static readonly int GodOfReapers_maxLevel = 5;
        public static readonly int GodOfReapers_maxBuff1 = 11;
        public static readonly int GodOfReapers_maxBuff2 = 12;
        public static readonly int GodOfReapers_maxBuff3 = 13;
        public static readonly int GodOfReapers_maxBuff4 = 14;
        public static readonly int GodOfReapers_maxBuff5 = 15;
        #endregion
        #region Save My Friend!
        public static readonly int SaveMyFriend_maxLevel = 1;
        public static readonly int SaveMyFriend_unlockLevel = 10;
        public static readonly int SaveMyFriend_cooldown = 60;
        public static readonly float SaveMyFriend_skillMaxDuration = 2.5f;
        public static readonly float SaveMyFriend_compMaxDuration = 5;
        public static readonly float SaveMyFriend_healInterval = 1;
        public static readonly float SaveMyFriend_healPercent = 0.1f;
        #endregion
        #region Shield Of Power
        public static readonly int ShieldOfPower_maxLevel = 1;
        public static readonly int ShieldOfPower_unlockLevel = 10;
        #endregion
        #region Ghost Rip
        public static readonly int GhostRip_maxLevel = 5;
        public static readonly int GhostRip_unlockLevel = 12;
        public static readonly float GhostRip_critPercent1 = 15;
        public static readonly float GhostRip_critPercent2 = 30;
        public static readonly float GhostRip_critPercent3 = 45;
        public static readonly float GhostRip_critPercent4 = 60;
        public static readonly float GhostRip_critPercent5 = 75;
        public static readonly float GhostRip_stunDuration1 = 2;
        public static readonly float GhostRip_stunDuration2 = 2.5f;
        public static readonly float GhostRip_stunDuration3 = 3;
        public static readonly float GhostRip_stunDuration4 = 3.5f;
        public static readonly float GhostRip_stunDuration5 = 4;
        #endregion
        #region Echo
        public static readonly int Echo_maxLevel = 3;
        public static readonly int Echo_unlockLevel = 16;
        public static readonly float Echo_cooldown1 = 40;
        public static readonly float Echo_cooldown2 = 35;
        public static readonly float Echo_cooldown3 = 30;
        public static readonly float Echo_radius1 = 30;
        public static readonly float Echo_radius2 = 45;
        public static readonly float Echo_radius3 = 60;
        public static readonly float Echo_stunDuration1 = 3;
        public static readonly float Echo_stunDuration2 = 4;
        public static readonly float Echo_stunDuration3 = 5;
        #endregion
        #region Piercing Waves
        public static readonly int PiercingWaves_maxLevel = 5;
        public static readonly int PiercingWaves_unlockLevel = 20;
        public static readonly float PiercingWaves_bleedDuration1 = 3;
        public static readonly float PiercingWaves_bleedDuration2 = 6;
        public static readonly float PiercingWaves_bleedDuration3 = 9;
        public static readonly float PiercingWaves_bleedDuration4 = 12;
        public static readonly float PiercingWaves_bleedDuration5 = 15;
        public static readonly float PiercingWaves_bleedDamage1 = 0.5f;
        public static readonly float PiercingWaves_bleedDamage2 = 1;
        public static readonly float PiercingWaves_bleedDamage3 = 1.5f;
        public static readonly float PiercingWaves_bleedDamage4 = 2;
        public static readonly float PiercingWaves_bleedDamage5 = 2.5f;
        #endregion
        #region Burning Spirit
        public static readonly int BurningSpirit_maxLevel = 1;
        public static readonly int BurningSpirit_unlockLevel = 7;
        public static readonly float BurningSpirit_ripperStackNeeded = 8;
        public static readonly float BurningSpirit_burnDuration = 3;
        public static readonly float BurningSpirit_burnDamage = 1;
        #endregion
        #region Hell Cat
        public static readonly int HellCat_maxLevel = 1;
        public static readonly int HellCat_unlockLevel = 8;
        #endregion
        #region Circular Saw
        public static readonly int CircualSaw_maxLevel = 3;
        public static readonly int CircualSaw_unlockLevel = 6;
        public static readonly float CircualSaw_damage1 = 6;
        public static readonly float CircualSaw_damage2 = 7;
        public static readonly float CircualSaw_damage3 = 8;
        public static readonly int CircualSaw_energy1 = 45;
        public static readonly int CircualSaw_energy2 = 40;
        public static readonly int CircualSaw_energy3 = 35;
        #endregion
        #region Ignition
        public static readonly int Ignition_maxLevel = 5;
        public static readonly int Ignition_unlockLevel = 10;
        public static readonly float Ignition_radius = 15;
        public static readonly float Ignition_burnDamage1 = 1;
        public static readonly float Ignition_burnDamage2 = 2;
        public static readonly float Ignition_burnDamage3 = 3;
        public static readonly float Ignition_burnDamage4 = 4;
        public static readonly float Ignition_burnDamage5 = 5;
        public static readonly float Ignition_burnDuration1 = 6;
        public static readonly float Ignition_burnDuration2 = 7;
        public static readonly float Ignition_burnDuration3 = 8;
        public static readonly float Ignition_burnDuration4 = 9;
        public static readonly float Ignition_burnDuration5 = 10;
        #endregion
        #region Souls Shelter
        public static readonly int SoulsShelter_maxLevel = 1;
        public static readonly int SoulsShelter_unlockLevel = 8;
        #endregion
        #region The Slash-per
        public static readonly int TheSlashPer_maxLevel = 1;
        public static readonly int TheSlashPer_unlockLevel = 15;
        #endregion
        #region High Temperature
        public static readonly int HighTemperature_maxLevel = 4;
        public static readonly int HighTemperature_unlockLevel = 11;
        public static readonly float HighTemperature_radius1 = 20;
        public static readonly float HighTemperature_radius2 = 25;
        public static readonly float HighTemperature_radius3 = 30;
        public static readonly float HighTemperature_radius4 = 35;
        #endregion
        #region Sacred Flames
        public static readonly int SacredFlames_maxLevel = 3;
        public static readonly int SacredFlames_unlockLevel = 15;
        public static readonly float SacredFlames_percent1 = 0.10f;
        public static readonly float SacredFlames_percent2 = 0.20f;
        public static readonly float SacredFlames_percent3 = 0.30f;
        #endregion
        #region Angry Bird
        public static readonly int AngryBird_maxLevel = 1;
        public static readonly int AngryBird_unlockLevel = 20;
        #endregion
        #region Passive Power
        public static readonly int PassivePower_maxLevel = 1;
        public static readonly int PassivePower_unlockLevel = 5;
        public static readonly float PassivePower_cooldown = 5;
        #endregion
        #region Prescience
        public static readonly int Prescience_maxLevel = 1;
        public static readonly int Prescience_unlockLevel = 15;
        #endregion
        #region Concentration
        public static readonly int Concentration_maxLevel = 1;
        public static readonly int Concentration_unlockLevel = 17;
        #endregion
        #region Precision
        public static readonly int Precision_maxLevel = 3;
        public static readonly int Precision_unlockLevel = 20;
        public static readonly float Precision_percent1 = 0.05f;
        public static readonly float Precision_percent2 = 0.10f;
        public static readonly float Precision_percent3 = 0.15f;
        #endregion
        #region Determination
        public static readonly int Determination_maxLevel = 3;
        public static readonly int Determination_unlockLevel = 25;
        public static readonly float Determination_comsumed1 = 15;
        public static readonly float Determination_comsumed2 = 10;
        public static readonly float Determination_comsumed3 = 5;
        #endregion
        #region Regeneration
        public static readonly int Regeneration_maxLevel = 3;
        public static readonly int Regeneration_unlockLevel = 1;
        public static readonly float Regeneration_cooldown = 5;
        public static readonly float Regeneration_percent1 = 0.03f;
        public static readonly float Regeneration_percent2 = 0.05f;
        public static readonly float Regeneration_percent3 = 0.07f;
        #endregion
        #region Strong Barrier
        public static readonly int StrongBarrier_maxLevel = 3;
        public static readonly int StrongBarrier_unlockLevel = 6;
        public static readonly float StrongBarrier_percent1 = 0.3f;
        public static readonly float StrongBarrier_percent2 = 0.2f;
        public static readonly float StrongBarrier_percent3 = 0.1f;
        #endregion

        public static void readDefs()
        {

            CloakBuffDef = RoR2Content.Buffs.Cloak;
            WeakDebuffDef = RoR2Content.Buffs.Weak;
            RegenBuffDef = RoR2Content.Buffs.CrocoRegen;
            InvincibilityBuffDef = RoR2Content.Buffs.Immune;
            HiddenInvincibilityBuffDef = RoR2Content.Buffs.HiddenInvincibility;

            BleedDotIndex = DotController.DotIndex.Bleed;
            SuperBleedDotIndex = DotController.DotIndex.SuperBleed;
            BurnDotIndex = DotController.DotIndex.Burn;
            SuperBurnDotIndex = DotController.DotIndex.StrongerBurn;


            ItemChange_magazineIndex = RoR2Content.Items.SecondarySkillMagazine;
            ItemChange_alienHeadIndex = RoR2Content.Items.AlienHead;
            ItemChange_bandolierIndex = RoR2Content.Items.Bandolier;
            ItemChange_shurikenIndex = DLC1Content.Items.PrimarySkillShuriken;
            ItemChange_squidIndex = RoR2Content.Items.Squid;
            ItemChange_brainstalksIndex = RoR2Content.Items.KillEliteFrenzy;
            ItemChange_hardlightAfterburnerIndex = RoR2Content.Items.UtilitySkillMagazine;
            ItemChange_heresyEssenceIndex = RoR2Content.Items.LunarSpecialReplacement;
            ItemChange_heresyHooksIndex = RoR2Content.Items.LunarSecondaryReplacement;
            ItemChange_heresyStridesIndex = RoR2Content.Items.LunarUtilityReplacement;
            ItemChange_heresyVisionsIndex = RoR2Content.Items.LunarPrimaryReplacement;
            ItemChange_brittleCrownIndex = RoR2Content.Items.GoldOnHit;
            ItemChange_lightFluxPauldronIndex = DLC1Content.Items.HalfAttackSpeedHalfCooldowns;
            ItemChange_purityIndex = RoR2Content.Items.LunarBadLuck;
            ItemChange_lysateCellIndex = DLC1Content.Items.EquipmentMagazineVoid;
            ItemChange_transcendanceIndex = RoR2Content.Items.ShieldOnly;

        }

    }
}
