using Panthera;
using Panthera.Abilities.Actives;
using Panthera.Base;
using Panthera.Components;
using Panthera.Components.Projectiles;
using Panthera.OldSkills;
using Panthera.Orbs;
using Panthera.Utils;
using R2API;
using RoR2;
using RoR2.Audio;
using RoR2.Orbs;
using RoR2.Projectile;
using RoR2.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using static Rewired.UI.ControlMapper.ControlMapper;

namespace Panthera.Base
{
    public static class PantheraAssets
    {

        #region Project Parameters
        private const string csProjName = "Panthera";
        private const string assetbundleName = "PantheraBundle";
        #endregion

        #region Content Pack
        public static List<EffectDef> effectDefs = new List<EffectDef>();
        public static List<GameObject> projectilePrefabs = new List<GameObject>();
        #endregion

        #region Asset Bundle
        public static AssetBundle MainAssetBundle;
        #endregion

        #region Character Prefabs
        public static GameObject MainPrefab;
        public static GameObject DisplayPrefab;
        #endregion

        #region Objects Prefabs
        public static GameObject FrontShieldObj;
        public static GameObject FrostedAirObj;
        public static GameObject DodgeEffectPrefab;
        public static GameObject BlockEffectPrefab;
        public static GameObject AbsorbEffectPrefab;
        public static GameObject OutOfCombatEffectPrefab;
        public static GameObject ReducedEffectPrefab;
        #endregion

        #region GUI Prefabs
        public static GameObject PantheraCanvas;
        public static GameObject ConfigButtonPrefab;
        public static GameObject ConfigPanelPrefab;
        public static GameObject SkillTreeWindowPrefab;
        public static GameObject PrimarySkillPrefab;
        public static GameObject ActiveSkillPrefab;
        public static GameObject PassiveSkillPrefab;
        public static GameObject HybridSkillPrefab;
        public static GameObject KeyBindWindowPrefab;
        public static GameObject ResetKeyBindWindowPrefab;
        public static GameObject ActivePresetWindowPrefab;
        public static GameObject ResetAttributesWindowPrefab;
        public static GameObject ResetSkillsTreeWindowPrefab;
        public static GameObject SimpleTooltipPrefab;
        public static GameObject SkillsTooltipPrefab;
        public static GameObject AbilitiesTooltipPrefab;
        public static GameObject BuffsTooltipPrefab;
        public static GameObject ComboBaseTemplate;
        public static GameObject ComboSkillTemplate;
        public static GameObject ComboLineTemplate;
        public static GameObject ComboButtonTemplate;
        #endregion

        #region HUD Prefabs
        public static GameObject AbilitiesIcons;
        public static GameObject SpellsIcons;
        public static GameObject FuryBar;
        public static Sprite LevelUpIcon;
        public static GameObject HUDComboBaseTemplate;
        public static GameObject HUDComboSkillTemplate;
        public static GameObject HUDComboLineTemplate;
        public static GameObject HUDComboButtonTemplate;
        public static GameObject HUDCooldownFrame;
        public static GameObject HUDCooldownSkillTemplate;
        public static GameObject HUDShieldBar;
        public static GameObject HUDBlockBar;
        public static GameObject CrosshairPrefab;
        #endregion

        #region Debug
        public static GameObject debugInfo;
        public static GameObject debugInfoText;
        #endregion

        #region Materials
        public static Material Body1Mat1;
        public static Material Body1Mat2;
        public static Material Body2Mat1;
        public static Material Body2Mat2;
        public static Material Body3Mat1;
        public static Material Body3Mat2;
        public static Material Body4Mat1;
        public static Material Body4Mat2;
        public static Material Body4Mat3;
        public static Material Body4Mat4;
        public static Material Body4Mat5;
        public static Material Body4Mat6;
        public static Material Body4Mat7;
        public static Material Body4Mat8;
        public static Material Body4Mat9;
        public static Material Body4Mat10;
        public static Material Body4Mat11;
        public static Material Body4Mat12;
        public static Material Body4Mat13;
        public static Material XRayMat;
        public static GameObject PantheraPostProcess;
        #endregion

        #region Bodies
        public static Mesh BodyMesh1;
        public static Mesh BodyMesh2;
        public static Mesh BodyMesh3;
        public static Mesh BodyMesh4;
        #endregion

        #region Portraits
        public static Texture DefaultPortrait;
        public static Sprite Portrait1;
        public static Sprite Portrait2;
        public static Sprite Portrait3;
        public static Sprite Portrait4;
        public static Sprite OverviewPortrait1;
        public static Sprite OverviewPortrait2;
        public static Sprite OverviewPortrait3;
        public static Sprite OverviewPortrait4;
        #endregion

        #region Skills Icones
        public static Sprite RipSkill;
        public static Sprite RipSkillMenu;
        public static Sprite AirCleaveSkill;
        public static Sprite AirCleaveSkillMenu;
        public static Sprite LeapSkill;
        public static Sprite LeapSkillMenu;
        public static Sprite MightyRoarSkill;
        public static Sprite MightyRoarSkillMenu;
        public static Sprite FurySkill;
        public static Sprite FurySkillDisabled;
        public static Sprite GuardianSkill;
        public static Sprite GuardianSkillDisabled;
        public static Sprite SlashSkill;
        public static Sprite SlashSkillMenu;
        public static Sprite ProwlSkill;
        public static Sprite ProwlSkillDisabled;
        public static Sprite DetectionSkill;
        public static Sprite DetectionSkillDisabled;
        public static Sprite AmbitionSkill;
        public static Sprite AirSlashSkill;
        public static Sprite FrontShieldSkill;
        public static Sprite FrontShieldSkillMenu;
        public static Sprite ClawStormSkill;
        public static Sprite ClawStormSkillMenu;
        public static Sprite GhostRipSkill;
        public static Sprite GhostRipSkillMenu;
        public static Sprite ShieldBashSkill;
        public static Sprite GoldenRipSkill;
        public static Sprite GoldenRipSkillMenu;
        public static Sprite ArcaneAnchorSkill;
        public static Sprite ArcaneAnchorSkillMenu;
        public static Sprite ConvergenceHookSkill;
        public static Sprite PortalSurgeSkill;
        #endregion

        #region Abilities Icones
        public static Sprite FelineSkillsAbility;
        public static Sprite FelineSkillsAbilityMenu;
        public static Sprite SharpenedFangsAbility;
        public static Sprite EnchantedFurAbility;
        public static Sprite WindWalkerAbility;
        public static Sprite PredatorAbility;
        public static Sprite SwiftMovesAbility;
        public static Sprite ImprovedShieldAbility;
        public static Sprite TornadoAbility;
        public static Sprite MassiveHookAbility;
        public static Sprite SixthSenseAbility;
        public static Sprite RelentlessStalkerAbility;
        public static Sprite RoarOfResilienceAbility;
        public static Sprite ClawsSharpeningAbility;
        public static Sprite GoldenStartAbility;
        public static Sprite StealthStrikeAbility;
        public static Sprite CryoLeapAbility;
        public static Sprite ShadowStalkerAbility;
        public static Sprite WardensVitalityAbility;
        public static Sprite SavageRevitalizationAbility;
        public static Sprite InnateProtectionAbility;
        public static Sprite FurrifyAbility;
        public static Sprite ConcentrationAbility;
        public static Sprite SuperiorFlairAbility;
        public static Sprite UntamedSpiritAbility;
        public static Sprite GodPowerAbility;
        public static Sprite EternalFuryAbility;
        public static Sprite InnerRageAbility;
        public static Sprite InfernalSwipeAbility;
        public static Sprite HeatWaveAbility;
        public static Sprite KineticResorptionAbility;
        public static Sprite ExtendedProtectionAbility;
        #endregion

        #region Controller Button Icones
        public static Sprite AButtonIcon;
        public static Sprite BButtonIcon;
        public static Sprite DownArrowButtonIcon;
        public static Sprite InfoButtonIcon;
        public static Sprite LBButtonIcon;
        public static Sprite LButtonIcon;
        public static Sprite LDownButtonIcon;
        public static Sprite LeftArrowButtonIcon;
        public static Sprite LLeftButtonIcon;
        public static Sprite LRightButtonIcon;
        public static Sprite LTButtonIcon;
        public static Sprite LUpButtonIcon;
        public static Sprite RBButtonIcon;
        public static Sprite RButtonIcon;
        public static Sprite RDownButtonIcon;
        public static Sprite RightArrowButtonIcon;
        public static Sprite RLeftButtonIcon;
        public static Sprite RRightButtonIcon;
        public static Sprite RTButtonIcon;
        public static Sprite RUpButtonIcon;
        public static Sprite UpArrowButtonIcon;
        public static Sprite XButtonIcon;
        public static Sprite YButtonIcon;
        #endregion

        #region Buffs Icones
        public static Sprite CupidityBuff;
        public static Sprite TenacityBuff;
        public static Sprite RazorsBuff;
        public static Sprite BleedOutBuff;
        public static Sprite MortalMirageBuff;
        public static Sprite EclipseBuff;
        public static Sprite FrozenPawsBuff;
        public static Sprite RegenerationBuff;
        public static Sprite ResilienceBuff;
        public static Sprite EnrageBuff;
        public static Sprite IgnitionBuff;
        #endregion

        #region Character FX
        public static GameObject DeadFX;
        public static GameObject LevelUPFX;
        public static GameObject RegenerationFX;
        #endregion

        #region Skills FX
        public static GameObject LeftRipFX;
        public static GameObject RightRipFX;
        public static GameObject AirCleaveLeftFX;
        public static GameObject AirCleaveRightFX;
        public static GameObject LeapTrailFX;
        public static GameObject MightyRoarFX;
        public static GameObject FuryOnFX;
        public static GameObject FuryAuraFX;
        public static GameObject GuardianOnFX;
        public static GameObject GuardianAuraFX;
        public static GameObject SlashFX;
        public static GameObject AmbitionOnFX;
        public static GameObject AmbitionAuraFX;
        public static GameObject AirSlashStartLeftFX;
        public static GameObject AirSlashStartRightFX;
        public static GameObject AirSlashProjectileLoopFX;
        public static GameObject GhostLeftRipFX;
        public static GameObject GhostRightRipFX;
        public static GameObject ShieldBashFX;
        public static GameObject GoldenLeftRipFX;
        public static GameObject GoldenRightRipFX;
        public static GameObject ConvergenceHookFX;
        public static GameObject ClawsStormWhiteFX;
        public static GameObject ClawsStormOrangeFX;
        public static GameObject ClawsStormRedFX;
        public static GameObject PortalChargingExplosionFX;
        public static GameObject PortalCharingEndFX;
        public static GameObject PortalOverChargeFX;
        public static GameObject PortalPlayerChargingFX;
        public static GameObject PortalChargingFX;
        public static GameObject FireRipLeftFX;
        public static GameObject FireRipRightFX;
        public static GameObject FireAirCleaveLeftFX;
        public static GameObject FireAirCleaveRightFX;
        #endregion

        #region Orbs FX
        public static GameObject ShieldOrbFX;
        #endregion

        #region HitFX
        public static GameObject RipHitFX;
        public static GameObject AirCleaveHitFX;
        public static GameObject FrontShieldHitFX;
        #endregion

        #region Projectiles
        public static GameObject AirCleaveLeftProjectile;
        public static GameObject AirCleaveRightProjectile;
        public static GameObject AirSlashProjectile;
        public static GameObject FireAirCleaveLeftProjectile;
        public static GameObject FireAirCleaveRightProjectile;
        #endregion


        #region PantheraAssets Loading
        public static void PopulateAssets()
        {

            #region Asset Bundle
            MainAssetBundle = AssetBundle.LoadFromMemory(Properties.Resources.PantheraBundle);
            #endregion

            #region Models
            MainPrefab = UnityEngine.Object.Instantiate(MainAssetBundle.LoadAsset<GameObject>("PantheraModel"));
            DisplayPrefab = UnityEngine.Object.Instantiate(MainAssetBundle.LoadAsset<GameObject>("PantheraModelDisplay"));
            #endregion

            #region Objects
            FrontShieldObj = MainAssetBundle.LoadAsset<GameObject>("FrontShieldObj").InstantiateClone("FrontShieldObj");
            FrostedAirObj = MainAssetBundle.LoadAsset<GameObject>("FrostedAirObj").InstantiateClone("FrostedAirObj");
            #endregion

            #region GUI Items
            PantheraCanvas = MainAssetBundle.LoadAsset<GameObject>("PantheraCanva");
            ConfigButtonPrefab = MainAssetBundle.LoadAsset<GameObject>("ConfigButton");
            ConfigPanelPrefab = MainAssetBundle.LoadAsset<GameObject>("PantheraPanel");
            SkillTreeWindowPrefab = MainAssetBundle.LoadAsset<GameObject>("SkillsTreeWindow");
            PrimarySkillPrefab = MainAssetBundle.LoadAsset<GameObject>("SkillIconPrimary");
            ActiveSkillPrefab = MainAssetBundle.LoadAsset<GameObject>("SkillIconActive");
            PassiveSkillPrefab = MainAssetBundle.LoadAsset<GameObject>("SkillIconPassive");
            HybridSkillPrefab = MainAssetBundle.LoadAsset<GameObject>("SkillIconHybrid");
            KeyBindWindowPrefab = MainAssetBundle.LoadAsset<GameObject>("KeyBindWindow");
            ResetKeyBindWindowPrefab = MainAssetBundle.LoadAsset<GameObject>("ResetKeysBindWindow");
            ActivePresetWindowPrefab = MainAssetBundle.LoadAsset<GameObject>("ActivatePresetWindow");
            ResetAttributesWindowPrefab = MainAssetBundle.LoadAsset<GameObject>("ResetAttributesWindow");
            ResetSkillsTreeWindowPrefab = MainAssetBundle.LoadAsset<GameObject>("ResetSkillsTreeWindow");
            SimpleTooltipPrefab = MainAssetBundle.LoadAsset<GameObject>("SimpleTooltip");
            SkillsTooltipPrefab = MainAssetBundle.LoadAsset<GameObject>("SkillsTooltip");
            AbilitiesTooltipPrefab = MainAssetBundle.LoadAsset<GameObject>("AbilitiesTooltip");
            BuffsTooltipPrefab = MainAssetBundle.LoadAsset<GameObject>("BuffTooltip");
            ComboBaseTemplate = MainAssetBundle.LoadAsset<GameObject>("ComboBaseTemplate");
            ComboSkillTemplate = MainAssetBundle.LoadAsset<GameObject>("ComboSkillTemplate");
            ComboLineTemplate = MainAssetBundle.LoadAsset<GameObject>("ComboLineTemplate");
            ComboButtonTemplate = MainAssetBundle.LoadAsset<GameObject>("ComboButtonTemplate");
            #endregion

            #region HUD Items
            AbilitiesIcons = MainAssetBundle.LoadAsset<GameObject>("HUDAbilities");
            SpellsIcons = MainAssetBundle.LoadAsset<GameObject>("HUDSpells");
            FuryBar = MainAssetBundle.LoadAsset<GameObject>("HUDFuryBar");
            debugInfo = MainAssetBundle.LoadAsset<GameObject>("DebugInfoHUD");
            debugInfoText = MainAssetBundle.LoadAsset<GameObject>("DebugInfoTextHUD");
            HUDComboBaseTemplate = MainAssetBundle.LoadAsset<GameObject>("HUDComboBaseTemplate");
            HUDComboSkillTemplate = MainAssetBundle.LoadAsset<GameObject>("HUDComboSkillTemplate");
            HUDComboLineTemplate = MainAssetBundle.LoadAsset<GameObject>("HUDComboLineTemplate");
            HUDComboButtonTemplate = MainAssetBundle.LoadAsset<GameObject>("HUDComboButtonTemplate");
            HUDCooldownFrame = MainAssetBundle.LoadAsset<GameObject>("HUDCooldownFrame");
            HUDCooldownSkillTemplate = MainAssetBundle.LoadAsset<GameObject>("HUDCooldownSkillTemplate");
            HUDShieldBar = MainAssetBundle.LoadAsset<GameObject>("HUDShieldBar");
            HUDBlockBar = MainAssetBundle.LoadAsset<GameObject>("HUDBlockBar");
            CrosshairPrefab = MainAssetBundle.LoadAsset<GameObject>("CrosshairPrefab");
            #endregion

            #region Materials
            Body1Mat1 = MainAssetBundle.LoadAsset<Material>("Cat1Mat1");
            Body1Mat2 = MainAssetBundle.LoadAsset<Material>("Cat1Mat2");
            Body2Mat1 = MainAssetBundle.LoadAsset<Material>("Cat2Mat1");
            Body2Mat2 = MainAssetBundle.LoadAsset<Material>("Cat2Mat2");
            Body3Mat1 = MainAssetBundle.LoadAsset<Material>("Cat3Mat1");
            Body3Mat2 = MainAssetBundle.LoadAsset<Material>("Cat3Mat2");
            Body4Mat1 = MainAssetBundle.LoadAsset<Material>("hair2_d");
            Body4Mat2 = MainAssetBundle.LoadAsset<Material>("face");
            Body4Mat3 = MainAssetBundle.LoadAsset<Material>("hair_d");
            Body4Mat4 = MainAssetBundle.LoadAsset<Material>("jewelry");
            Body4Mat5 = MainAssetBundle.LoadAsset<Material>("jewelry2");
            Body4Mat6 = MainAssetBundle.LoadAsset<Material>("whiskers_d");
            Body4Mat7 = MainAssetBundle.LoadAsset<Material>("whiskers");
            Body4Mat8 = MainAssetBundle.LoadAsset<Material>("body");
            Body4Mat9 = MainAssetBundle.LoadAsset<Material>("Claws");
            Body4Mat10 = MainAssetBundle.LoadAsset<Material>("mouth");
            Body4Mat11 = MainAssetBundle.LoadAsset<Material>("eye");
            Body4Mat12 = MainAssetBundle.LoadAsset<Material>("hair2");
            Body4Mat13 = MainAssetBundle.LoadAsset<Material>("Body_2");
            XRayMat = MainAssetBundle.LoadAsset<Material>("XRayMaterial");
            PantheraPostProcess = MainAssetBundle.LoadAsset<GameObject>("PantheraPostProcess");
            #endregion

            #region Bodies
            BodyMesh1 = MainAssetBundle.LoadAsset<Mesh>("PantheraBody1");
            BodyMesh2 = MainAssetBundle.LoadAsset<Mesh>("PantheraBody2");
            BodyMesh3 = MainAssetBundle.LoadAsset<Mesh>("PantheraBody3");
            BodyMesh4 = MainAssetBundle.LoadAsset<Mesh>("PantheraBody4");
            #endregion

            #region Portraits
            DefaultPortrait = MainAssetBundle.LoadAsset<Texture>("Portrait1");
            Portrait1 = MainAssetBundle.LoadAsset<Sprite>("Portrait1");
            Portrait2 = MainAssetBundle.LoadAsset<Sprite>("Portrait2");
            Portrait3 = MainAssetBundle.LoadAsset<Sprite>("Portrait3");
            Portrait4 = MainAssetBundle.LoadAsset<Sprite>("Portrait4");
            OverviewPortrait1 = MainAssetBundle.LoadAsset<Sprite>("PantheraOverviewPortraitWhite");
            OverviewPortrait2 = MainAssetBundle.LoadAsset<Sprite>("PantheraOverviewPortraitOrange");
            OverviewPortrait3 = MainAssetBundle.LoadAsset<Sprite>("PantheraOverviewPortraitPrimalStalker");
            OverviewPortrait4 = MainAssetBundle.LoadAsset<Sprite>("PantheraOverviewPortraitRed");
            #endregion

            #region HUD Icones
            LevelUpIcon = MainAssetBundle.LoadAsset<Sprite>("LevelUpIcon");
            #endregion

            #region Skills Icones
            RipSkill = MainAssetBundle.LoadAsset<Sprite>("RipIcon");
            RipSkillMenu = MainAssetBundle.LoadAsset<Sprite>("RipIconMenu");
            AirCleaveSkill = MainAssetBundle.LoadAsset<Sprite>("AirCleaveIcon");
            AirCleaveSkillMenu = MainAssetBundle.LoadAsset<Sprite>("AirCleaveIconMenu");
            LeapSkill = MainAssetBundle.LoadAsset<Sprite>("LeapIcon");
            LeapSkillMenu = MainAssetBundle.LoadAsset<Sprite>("LeapIconMenu");
            MightyRoarSkill = MainAssetBundle.LoadAsset<Sprite>("MightyRoarIcon");
            MightyRoarSkillMenu = MainAssetBundle.LoadAsset<Sprite>("MightyRoarIconMenu");
            FurySkill = MainAssetBundle.LoadAsset<Sprite>("FuryIcon");
            FurySkillDisabled = MainAssetBundle.LoadAsset<Sprite>("FuryIconDisabled");
            GuardianSkill = MainAssetBundle.LoadAsset<Sprite>("GuardianIcon");
            GuardianSkillDisabled = MainAssetBundle.LoadAsset<Sprite>("GuardianIconDisabled");
            SlashSkill = MainAssetBundle.LoadAsset<Sprite>("SlashIcon");
            SlashSkillMenu = MainAssetBundle.LoadAsset<Sprite>("SlashIconMenu");
            ProwlSkill = MainAssetBundle.LoadAsset<Sprite>("ProwlIcon");
            ProwlSkillDisabled = MainAssetBundle.LoadAsset<Sprite>("ProwlIconDisabled");
            DetectionSkill = MainAssetBundle.LoadAsset<Sprite>("DetectionIcon");
            DetectionSkillDisabled = MainAssetBundle.LoadAsset<Sprite>("DetectionIconDisabled");
            AmbitionSkill = MainAssetBundle.LoadAsset<Sprite>("AmbitionIcon");
            AirSlashSkill = MainAssetBundle.LoadAsset<Sprite>("AirSlashIcon");
            FrontShieldSkill = MainAssetBundle.LoadAsset<Sprite>("FrontShieldIcon");
            FrontShieldSkillMenu = MainAssetBundle.LoadAsset<Sprite>("FrontShieldIconMenu");
            ClawStormSkill = MainAssetBundle.LoadAsset<Sprite>("ClawsStormIcon");
            ClawStormSkillMenu = MainAssetBundle.LoadAsset<Sprite>("ClawsStormIconMenu");
            GhostRipSkill = MainAssetBundle.LoadAsset<Sprite>("GhostRipIcon");
            GhostRipSkillMenu = MainAssetBundle.LoadAsset<Sprite>("GhostRipIconMenu");
            ShieldBashSkill = MainAssetBundle.LoadAsset<Sprite>("ShieldBashIcon");
            GoldenRipSkill = MainAssetBundle.LoadAsset<Sprite>("GoldenRipIcon");
            GoldenRipSkillMenu = MainAssetBundle.LoadAsset<Sprite>("GoldenRipIconMenu");
            ArcaneAnchorSkill = MainAssetBundle.LoadAsset<Sprite>("ArcaneAnchorIcon");
            ArcaneAnchorSkillMenu = MainAssetBundle.LoadAsset<Sprite>("ArcaneAnchorIconMenu");
            ConvergenceHookSkill = MainAssetBundle.LoadAsset<Sprite>("ConvergenceHookIcon");            
            PortalSurgeSkill = MainAssetBundle.LoadAsset<Sprite>("PortalSurgeIcon");            
            #endregion

            #region Abilities Icones
            FelineSkillsAbility = MainAssetBundle.LoadAsset<Sprite>("FelineSkillsIcon");
            FelineSkillsAbilityMenu = MainAssetBundle.LoadAsset<Sprite>("FelineSkillsIconMenu");
            SharpenedFangsAbility  = MainAssetBundle.LoadAsset<Sprite>("SharpenedFangsIcon");
            EnchantedFurAbility = MainAssetBundle.LoadAsset<Sprite>("EnchantedFurIcon");
            WindWalkerAbility = MainAssetBundle.LoadAsset<Sprite>("WindWalkerIcon");
            PredatorAbility = MainAssetBundle.LoadAsset<Sprite>("PredatorIcon");
            SwiftMovesAbility = MainAssetBundle.LoadAsset<Sprite>("SwiftMovesIcon");
            ImprovedShieldAbility = MainAssetBundle.LoadAsset<Sprite>("ImprovedShieldIcon");
            TornadoAbility = MainAssetBundle.LoadAsset<Sprite>("TornadoIcon");
            MassiveHookAbility = MainAssetBundle.LoadAsset<Sprite>("MassiveHookIcon");
            SixthSenseAbility = MainAssetBundle.LoadAsset<Sprite>("SixthSenseIcon");
            RelentlessStalkerAbility = MainAssetBundle.LoadAsset<Sprite>("RelentlessStalkerIcon");
            RoarOfResilienceAbility = MainAssetBundle.LoadAsset<Sprite>("RoarOfResilienceIcon");
            ClawsSharpeningAbility = MainAssetBundle.LoadAsset<Sprite>("ClawsSharpeningIcon");
            GoldenStartAbility = MainAssetBundle.LoadAsset<Sprite>("GoldenStartIcon");
            StealthStrikeAbility = MainAssetBundle.LoadAsset<Sprite>("StealthStrikeIcon");
            CryoLeapAbility = MainAssetBundle.LoadAsset<Sprite>("CryoLeapIcon");
            ShadowStalkerAbility = MainAssetBundle.LoadAsset<Sprite>("ShadowStalkerIcon");
            WardensVitalityAbility = MainAssetBundle.LoadAsset<Sprite>("WardensVitalityIcon");
            SavageRevitalizationAbility = MainAssetBundle.LoadAsset<Sprite>("SavageRevitalizationIcone");
            InnateProtectionAbility = MainAssetBundle.LoadAsset<Sprite>("InnateProtectionIcon");
            FurrifyAbility = MainAssetBundle.LoadAsset<Sprite>("FurrifyIcon");
            ConcentrationAbility = MainAssetBundle.LoadAsset<Sprite>("ConcentrationIcon");
            SuperiorFlairAbility = MainAssetBundle.LoadAsset<Sprite>("SuperiorFlairIcon");
            UntamedSpiritAbility = MainAssetBundle.LoadAsset<Sprite>("UntamedSpiritIcon");
            GodPowerAbility = MainAssetBundle.LoadAsset<Sprite>("GodPowerIcon");
            EternalFuryAbility = MainAssetBundle.LoadAsset<Sprite>("EternalFuryIcon");
            InnerRageAbility = MainAssetBundle.LoadAsset<Sprite>("InnerRageIcon");
            InfernalSwipeAbility = MainAssetBundle.LoadAsset<Sprite>("InfernalSwipeIcon");
            HeatWaveAbility = MainAssetBundle.LoadAsset<Sprite>("HeatWaveIcon");
            KineticResorptionAbility = MainAssetBundle.LoadAsset<Sprite>("KineticResorptionIcon");
            ExtendedProtectionAbility = MainAssetBundle.LoadAsset<Sprite>("ExtendedProtectionIcon");
            #endregion

            #region Controller Buttons Icones
            AButtonIcon = MainAssetBundle.LoadAsset<Sprite>("AButtonIcon");
            BButtonIcon = MainAssetBundle.LoadAsset<Sprite>("BButtonIcon");
            DownArrowButtonIcon = MainAssetBundle.LoadAsset<Sprite>("DownArrowButtonIcon");
            InfoButtonIcon = MainAssetBundle.LoadAsset<Sprite>("InfoButtonIcon");
            LBButtonIcon = MainAssetBundle.LoadAsset<Sprite>("LBButtonIcon");
            LButtonIcon = MainAssetBundle.LoadAsset<Sprite>("LButtonIcon");
            LDownButtonIcon = MainAssetBundle.LoadAsset<Sprite>("LDownButtonIcon");
            LeftArrowButtonIcon = MainAssetBundle.LoadAsset<Sprite>("LeftArrowButtonIcon");
            LLeftButtonIcon = MainAssetBundle.LoadAsset<Sprite>("LLeftButtonIcon");
            LRightButtonIcon = MainAssetBundle.LoadAsset<Sprite>("LRightButtonIcon");
            LTButtonIcon = MainAssetBundle.LoadAsset<Sprite>("LTButtonIcon");
            LUpButtonIcon = MainAssetBundle.LoadAsset<Sprite>("LUpButtonIcon");
            RBButtonIcon = MainAssetBundle.LoadAsset<Sprite>("RBButtonIcon");
            RButtonIcon = MainAssetBundle.LoadAsset<Sprite>("RButtonIcon");
            RDownButtonIcon = MainAssetBundle.LoadAsset<Sprite>("RDownButtonIcon");
            RightArrowButtonIcon = MainAssetBundle.LoadAsset<Sprite>("RightArrowButtonIcon");
            RLeftButtonIcon = MainAssetBundle.LoadAsset<Sprite>("RLeftButtonIcon");
            RRightButtonIcon = MainAssetBundle.LoadAsset<Sprite>("RRightButtonIcon");
            RTButtonIcon = MainAssetBundle.LoadAsset<Sprite>("RTButtonIcon");
            RUpButtonIcon = MainAssetBundle.LoadAsset<Sprite>("RUpButtonIcon");
            UpArrowButtonIcon = MainAssetBundle.LoadAsset<Sprite>("UpArrowButtonIcon");
            XButtonIcon = MainAssetBundle.LoadAsset<Sprite>("XButtonIcon");
            YButtonIcon = MainAssetBundle.LoadAsset<Sprite>("YButtonIcon");
            #endregion

            #region Buffs Icones
            CupidityBuff = MainAssetBundle.LoadAsset<Sprite>("CupidityBuff");
            TenacityBuff = MainAssetBundle.LoadAsset<Sprite>("TenacityBuff");
            RazorsBuff = MainAssetBundle.LoadAsset<Sprite>("RazorsBuff");
            BleedOutBuff = MainAssetBundle.LoadAsset<Sprite>("BleedOutBuff");
            MortalMirageBuff = MainAssetBundle.LoadAsset<Sprite>("MortalMirageBuff");
            EclipseBuff = MainAssetBundle.LoadAsset<Sprite>("EclipseBuff");
            FrozenPawsBuff = MainAssetBundle.LoadAsset<Sprite>("FrozenPawsBuff");
            RegenerationBuff = MainAssetBundle.LoadAsset<Sprite>("RegenerationBuff");
            ResilienceBuff = MainAssetBundle.LoadAsset<Sprite>("ResilienceBuff");
            EnrageBuff = MainAssetBundle.LoadAsset<Sprite>("EnrageBuff");
            IgnitionBuff = MainAssetBundle.LoadAsset<Sprite>("IgnitionBuff");
            #endregion

            #region Character FX
            DeadFX = MainAssetBundle.LoadAsset<GameObject>("DeadFX");
            LevelUPFX = MainAssetBundle.LoadAsset<GameObject>("LevelUPFX");
            RegenerationFX = MainAssetBundle.LoadAsset<GameObject>("RegenerationFX");
            #endregion

            #region Skills FX
            LeftRipFX = MainAssetBundle.LoadAsset<GameObject>("RipLeftFX");
            RightRipFX = MainAssetBundle.LoadAsset<GameObject>("RipRightFX");
            AirCleaveLeftFX = MainAssetBundle.LoadAsset<GameObject>("AirCleaveLeftFX");
            AirCleaveRightFX = MainAssetBundle.LoadAsset<GameObject>("AirCleaveRightFX");
            LeapTrailFX = MainAssetBundle.LoadAsset<GameObject>("LeapTrailFX");
            MightyRoarFX = MainAssetBundle.LoadAsset<GameObject>("MightyRoarFX");
            FuryOnFX = MainAssetBundle.LoadAsset<GameObject>("FuryOnFX");
            FuryAuraFX = MainAssetBundle.LoadAsset<GameObject>("FuryAuraFX");
            GuardianOnFX = MainAssetBundle.LoadAsset<GameObject>("GuardianOnFX");
            GuardianAuraFX = MainAssetBundle.LoadAsset<GameObject>("GuardianAuraFX");
            SlashFX = MainAssetBundle.LoadAsset<GameObject>("SlashFX");
            AmbitionOnFX = MainAssetBundle.LoadAsset<GameObject>("AmbitionOnFX");
            AmbitionAuraFX = MainAssetBundle.LoadAsset<GameObject>("AmbitionAuraFX");
            AirSlashStartLeftFX = MainAssetBundle.LoadAsset<GameObject>("AirSlashStartLeftFX");
            AirSlashStartRightFX = MainAssetBundle.LoadAsset<GameObject>("AirSlashStartRightFX");
            AirSlashProjectileLoopFX = MainAssetBundle.LoadAsset<GameObject>("AirSlashLoopFX");
            GhostLeftRipFX = MainAssetBundle.LoadAsset<GameObject>("GhostRipLeftFX");
            GhostRightRipFX = MainAssetBundle.LoadAsset<GameObject>("GhostRipRightFX");
            ShieldBashFX = MainAssetBundle.LoadAsset<GameObject>("ShieldBashFX");
            GoldenLeftRipFX = MainAssetBundle.LoadAsset<GameObject>("GoldenRipLeftFX");
            GoldenRightRipFX = MainAssetBundle.LoadAsset<GameObject>("GoldenRipRightFX");
            ConvergenceHookFX = MainAssetBundle.LoadAsset<GameObject>("ConvergenceHookFX");
            ClawsStormWhiteFX = MainAssetBundle.LoadAsset<GameObject>("ClawsStormWhiteFX");
            ClawsStormOrangeFX = MainAssetBundle.LoadAsset<GameObject>("ClawsStormOrangeFX");
            PortalChargingExplosionFX = MainAssetBundle.LoadAsset<GameObject>("PortalChargingExplosionFX");
            PortalCharingEndFX = MainAssetBundle.LoadAsset<GameObject>("PortalCharingEndFX");
            PortalOverChargeFX = MainAssetBundle.LoadAsset<GameObject>("PortalOverChargeFX");
            PortalPlayerChargingFX = MainAssetBundle.LoadAsset<GameObject>("PortalPlayerChargingFX");
            PortalChargingFX = MainAssetBundle.LoadAsset<GameObject>("ProtalChargingFX");
            ClawsStormRedFX = MainAssetBundle.LoadAsset<GameObject>("ClawsStormRedFX");
            FireRipLeftFX = MainAssetBundle.LoadAsset<GameObject>("FireRipLeftFX");
            FireRipRightFX = MainAssetBundle.LoadAsset<GameObject>("FireRipRightFX");
            FireAirCleaveLeftFX = MainAssetBundle.LoadAsset<GameObject>("FireAirCleaveLeftFX");
            FireAirCleaveRightFX = MainAssetBundle.LoadAsset<GameObject>("FireAirCleaveRightFX");
            #endregion

            #region Orbs FX
            ShieldOrbFX = MainAssetBundle.LoadAsset<GameObject>("ShieldOrbFX");
            SetAsOrb(ShieldOrbFX);
            #endregion

            #region HitFX
            RipHitFX = MainAssetBundle.LoadAsset<GameObject>("RipHitFX");
            AirCleaveHitFX = MainAssetBundle.LoadAsset<GameObject>("AirCleaveHitFX");
            FrontShieldHitFX = MainAssetBundle.LoadAsset<GameObject>("FrontShieldHitFX");
            #endregion

            #region Setup Front Shield Prafab
            FrontShieldObj.SetActive(false);
            TeamComponent frontShieldTC = FrontShieldObj.AddComponent<TeamComponent>();
            FrontShieldHealthComponent frontShieldHC = FrontShieldObj.AddComponent<FrontShieldHealthComponent>();
            HurtBox frontShieldHurtBox = FrontShieldObj.AddComponent<HurtBox>();
            FrontShieldObj.AddComponent<FrontShieldComponent>();
            frontShieldTC.teamIndex = TeamIndex.None;
            frontShieldHC.health = 9999999;
            frontShieldHurtBox.healthComponent = frontShieldHC;
            #endregion

            #region Setup Frosted Air Prafab
            FrostedAirObj.SetActive(false);
            FrostedAirObj.AddComponent<FrostedAirComponent>();
            FrostedAirObj.AddComponent<NetworkIdentity>();
            #endregion

            #region Set up Dodge Effect Prefab
            DodgeEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/BearProc").InstantiateClone("PantheraDodgeEffect", false);
            DodgeEffectPrefab.GetComponent<EffectComponent>().soundName = Sound.Dodge;
            GameObject.DestroyImmediate(DodgeEffectPrefab.transform.Find("TextCamScaler").Find("TextRiser").Find("TextMeshPro").GetComponent<LanguageTextMeshController>());
            DodgeEffectPrefab.transform.Find("TextCamScaler").Find("TextRiser").Find("TextMeshPro").GetComponent<TextMeshPro>().text = Utils.PantheraTokens.Get("base_DodgeText");
            LoadEffect(DodgeEffectPrefab, 5);
            #endregion

            #region Set up Block Effect Prefab
            BlockEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/BearProc").InstantiateClone("PantheraBlockEffect", false);
            BlockEffectPrefab.GetComponent<EffectComponent>().soundName = Sound.Block;
            GameObject.DestroyImmediate(BlockEffectPrefab.transform.Find("TextCamScaler").Find("TextRiser").Find("TextMeshPro").GetComponent<LanguageTextMeshController>());
            BlockEffectPrefab.transform.Find("TextCamScaler").Find("TextRiser").Find("TextMeshPro").GetComponent<TextMeshPro>().text = Utils.PantheraTokens.Get("base_BlockText");
            LoadEffect(BlockEffectPrefab, 5);
            #endregion

            #region Set up Absorb Effect Prefab
            AbsorbEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/BearProc").InstantiateClone("PantheraAbsorbEffect", false);
            AbsorbEffectPrefab.GetComponent<EffectComponent>().soundName = Sound.ShieldAbsorb;
            GameObject.DestroyImmediate(AbsorbEffectPrefab.transform.Find("TextCamScaler").Find("TextRiser").Find("TextMeshPro").GetComponent<LanguageTextMeshController>());
            AbsorbEffectPrefab.transform.Find("TextCamScaler").Find("TextRiser").Find("TextMeshPro").GetComponent<TextMeshPro>().text = Utils.PantheraTokens.Get("base_AbsorbText");
            LoadEffect(AbsorbEffectPrefab, 5);
            #endregion

            #region Set Out Of Combat Effect Prefab
            OutOfCombatEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/BearProc").InstantiateClone("PantheraOutOfCombatEffect", false);
            OutOfCombatEffectPrefab.GetComponent<EffectComponent>().soundName = null;
            GameObject.DestroyImmediate(OutOfCombatEffectPrefab.transform.Find("TextCamScaler").Find("TextRiser").Find("TextMeshPro").GetComponent<LanguageTextMeshController>());
            OutOfCombatEffectPrefab.transform.Find("TextCamScaler").Find("TextRiser").Find("TextMeshPro").GetComponent<TextMeshPro>().text = Utils.PantheraTokens.Get("base_OutOfCombatText");
            LoadEffect(OutOfCombatEffectPrefab, 7);
            #endregion

            #region Reduced Effect Prefab
            ReducedEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/BearProc").InstantiateClone("PantheraOutOfCombatEffect", false);
            ReducedEffectPrefab.GetComponent<EffectComponent>().soundName = Sound.Reduced;
            GameObject.DestroyImmediate(ReducedEffectPrefab.transform.Find("TextCamScaler").Find("TextRiser").Find("TextMeshPro").GetComponent<LanguageTextMeshController>());
            ReducedEffectPrefab.transform.Find("TextCamScaler").Find("TextRiser").Find("TextMeshPro").GetComponent<TextMeshPro>().text = Utils.PantheraTokens.Get("base_ReducedText");
            LoadEffect(ReducedEffectPrefab, 7);
            #endregion

            #region Load Effects
            LoadEffect(LeftRipFX, 2);
            LoadEffect(RightRipFX, 2);
            LoadEffect(RipHitFX, 2);
            LoadEffect(AirCleaveHitFX, 0.5f);
            LoadEffect(LeapTrailFX);
            LoadEffect(MightyRoarFX, 3);
            LoadEffect(FuryOnFX, 5);
            LoadEffect(FuryAuraFX);
            LoadEffect(GuardianOnFX, 5);
            LoadEffect(GuardianAuraFX);
            LoadEffect(SlashFX, 1);
            LoadEffect(AmbitionOnFX, 5);
            LoadEffect(AmbitionAuraFX);
            LoadEffect(AirSlashStartLeftFX, 2);
            LoadEffect(AirSlashStartRightFX, 2);
            LoadEffect(FrontShieldHitFX, 0.5f);
            LoadEffect(GhostLeftRipFX, 2);
            LoadEffect(GhostRightRipFX, 2);
            LoadEffect(ShieldBashFX, 4f);
            LoadEffect(GoldenLeftRipFX, 2);
            LoadEffect(GoldenRightRipFX, 2);
            LoadEffect(ConvergenceHookFX, 3);
            LoadEffect(ClawsStormWhiteFX);
            LoadEffect(ClawsStormOrangeFX);
            LoadEffect(ClawsStormRedFX);
            LoadEffect(PortalChargingExplosionFX, 5);
            LoadEffect(PortalCharingEndFX, 5);
            LoadEffect(PortalOverChargeFX);
            LoadEffect(PortalPlayerChargingFX, 15);
            LoadEffect(PortalChargingFX, 15);
            LoadEffect(DeadFX);
            LoadEffect(LevelUPFX);
            LoadEffect(RegenerationFX);
            LoadEffect(FireRipLeftFX, 3);
            LoadEffect(FireRipRightFX, 3);
        #endregion

            #region Create Projectiles
            // ---- Air Cleave Left ---- //
            AirCleaveLeftProjectile = CreateProjectile(AirCleaveLeftFX, PantheraConfig.AirCleave_leftProjectileName, 10, 50);
            AirCleaveProjectile airCleaveLeftComp = AirCleaveLeftProjectile.AddComponent<AirCleaveProjectile>();
            airCleaveLeftComp.SetupProjectile(airCleaveLeftComp, PantheraConfig.AirCleave_leftProjectileName, Utils.Sound.AirCleaveHit, AirCleaveHitFX, true, false);

            // ---- Air Cleave Right ---- //
            AirCleaveRightProjectile = CreateProjectile(AirCleaveRightFX, PantheraConfig.AirCleave_rightProjectileName, 10, 50);
            AirCleaveProjectile airCleaveRightComp = AirCleaveRightProjectile.AddComponent<AirCleaveProjectile>();
            airCleaveRightComp.SetupProjectile(airCleaveRightComp, PantheraConfig.AirCleave_rightProjectileName, Utils.Sound.AirCleaveHit, AirCleaveHitFX, true, false);

            // ---- Air Slash ---- //
            AirSlashProjectile = CreateProjectile(AirSlashProjectileLoopFX, PantheraConfig.AirSlash_projectileName, 10, 50);
            AirSlashProjectile airSlashComp = AirSlashProjectile.AddComponent<AirSlashProjectile>();
            airSlashComp.SetupProjectile(airSlashComp, PantheraConfig.AirSlash_projectileName, Utils.Sound.AirCleaveHit, AirCleaveHitFX, false, false);

            // ---- Fire Air Cleave Left ---- //
            FireAirCleaveLeftProjectile = CreateProjectile(FireAirCleaveLeftFX, PantheraConfig.AirCleave_leftFireProjectileName, 10, 50);
            AirCleaveProjectile fireAirCleaveLeftComp = FireAirCleaveLeftProjectile.AddComponent<AirCleaveProjectile>();
            fireAirCleaveLeftComp.SetupProjectile(fireAirCleaveLeftComp, PantheraConfig.AirCleave_leftFireProjectileName, Utils.Sound.AirCleaveHit, AirCleaveHitFX, true, false);

            // ---- Fire Air Cleave Right ---- //
            FireAirCleaveRightProjectile = CreateProjectile(FireAirCleaveRightFX, PantheraConfig.AirCleave_rightFireProjectileName, 10, 50);
            AirCleaveProjectile fireAirCleaveRightComp = FireAirCleaveRightProjectile.AddComponent<AirCleaveProjectile>();
            fireAirCleaveRightComp.SetupProjectile(fireAirCleaveRightComp, PantheraConfig.AirCleave_rightFireProjectileName, Utils.Sound.AirCleaveHit, AirCleaveHitFX, true, false);
            #endregion


        }
        #endregion


        #region Create Projectiles
        public static GameObject CreateProjectile(GameObject FXPrefab, string name, float lifetime = 10, float desiredForwardSpeed = 100)
        {

            // Create the projectile //
            GameObject projectile = Resources.Load<GameObject>("Prefabs/Projectiles/FMJ").InstantiateClone(name, false);

            // Setup projectile controler //
            ProjectileController controler = projectile.GetComponent<ProjectileController>();
            controler.canImpactOnTrigger = true;

            // Setup the FX //
            GameObject projectileGhost = FXPrefab.InstantiateClone(name + "Ghost", false);
            ProjectileGhostController ghostController = projectileGhost.AddComponent<ProjectileGhostController>();
            projectileGhost.AddComponent<VFXAttributes>();
            controler.ghostPrefab = projectileGhost;
            ghostController.inheritScaleFromProjectile = true;

            // Remove the Overlap Attack //
            GameObject.Destroy(projectile.GetComponent<ProjectileOverlapAttack>());

            // Setup the Projectile Simple //
            projectile.GetComponent<ProjectileSimple>().lifetime = lifetime;
            projectile.GetComponent<ProjectileSimple>().desiredForwardSpeed = desiredForwardSpeed;

            // Setup the Collider //
            GameObject.DestroyImmediate(projectile.GetComponent<Collider>());
            BoxCollider boxCollider = projectile.AddComponent<BoxCollider>();
            boxCollider.center = FXPrefab.GetComponent<BoxCollider>().center;
            boxCollider.size = FXPrefab.GetComponent<BoxCollider>().size;
            boxCollider.isTrigger = true;

            // Save the projectile //
            projectilePrefabs.Add(projectile);
            Utils.Prefabs.AddPrefab(projectile);
            projectile.RegisterNetworkPrefab();

            return projectile;

        }
        #endregion

        #region Set as Orb
        public static void SetAsOrb(GameObject origOrb)
        {

            // Get the Heal Orb //
            GameObject ho = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/OrbEffects/HealthOrbEffect");

            // Set the Orb Effect //
            OrbEffect orbEffect = origOrb.AddComponent<OrbEffect>();
            orbEffect.startVelocity1 = new Vector3(-5, 16, -5);
            orbEffect.startVelocity2 = new Vector3(5, 8, 5);
            orbEffect.movementCurve = ho.GetComponent<OrbEffect>().movementCurve;

            // Add the Rigid Body //
            origOrb.AddComponent<Rigidbody>();

            // Add the Nework Identity Component //
            origOrb.AddComponent<NetworkIdentity>();

            // Set the VFXAttributes //
            VFXAttributes vfxAttributes = origOrb.GetComponent<VFXAttributes>() ? origOrb.GetComponent<VFXAttributes>() : origOrb.AddComponent<VFXAttributes>();
            vfxAttributes.vfxPriority = VFXAttributes.VFXPriority.Always;

            // Set the EffectComponent //
            EffectComponent effect = origOrb.GetComponent<EffectComponent>() ? origOrb.GetComponent<EffectComponent>() : origOrb.AddComponent<EffectComponent>();
            effect.applyScale = true;
            effect.effectIndex = EffectIndex.Invalid;
            effect.parentToReferencedTransform = false;
            effect.positionAtReferencedTransform = false;

            // Register the Effect //
            AddEffect(origOrb);

        }
        #endregion

        #region Load Effects
        private static GameObject LoadEffect(GameObject prefab, float duration = 0)
        {

            if (duration > 0)
            {
                DestroyOnTimer timer = prefab.GetComponent<DestroyOnTimer>() ? prefab.GetComponent<DestroyOnTimer>() : prefab.AddComponent<DestroyOnTimer>();
                timer.duration = duration;
            }
            if (prefab.GetComponent<NetworkIdentity>() == null) prefab.AddComponent<NetworkIdentity>().localPlayerAuthority = true;
            VFXAttributes vfxAttributes = prefab.GetComponent<VFXAttributes>() ? prefab.GetComponent<VFXAttributes>() : prefab.AddComponent<VFXAttributes>();
            vfxAttributes.vfxPriority = VFXAttributes.VFXPriority.Always;
            EffectComponent effect = prefab.GetComponent<EffectComponent>() ? prefab.GetComponent<EffectComponent>() : prefab.AddComponent<EffectComponent>();
            effect.applyScale = false;
            effect.effectIndex = EffectIndex.Invalid;
            effect.parentToReferencedTransform = true;
            effect.positionAtReferencedTransform = true;

            AddEffect(prefab);

            return prefab;
        }

        private static void AddEffect(GameObject effectPrefab)
        {

            EffectDef newEffectDef = new EffectDef();
            newEffectDef.prefab = effectPrefab;
            newEffectDef.prefabEffectComponent = effectPrefab.GetComponent<EffectComponent>();
            newEffectDef.prefabName = effectPrefab.name;
            newEffectDef.prefabVfxAttributes = effectPrefab.GetComponent<VFXAttributes>();

            effectDefs.Add(newEffectDef);
            effectPrefab.RegisterNetworkPrefab();
            Utils.Prefabs.AddPrefab(effectPrefab);

        }
        #endregion


    }

}
