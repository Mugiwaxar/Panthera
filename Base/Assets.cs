using Panthera;
using Panthera.Base;
using Panthera.Components;
using Panthera.OldSkills;
using Panthera.Utils;
using R2API;
using RoR2;
using RoR2.Audio;
using RoR2.Projectile;
using RoR2.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Panthera.Base
{
    public static class Assets
    {

        private const string csProjName = "Panthera";
        private const string assetbundleName = "PantheraBundle";

        internal static List<EffectDef> effectDefs = new List<EffectDef>();
        internal static List<GameObject> projectilePrefabs = new List<GameObject>();

        public static AssetBundle MainAssetBundle;

        public static GameObject MainPrefab;
        public static GameObject DisplayPrefab;

        public static GameObject ConfigButtonPrefab;
        public static GameObject ConfigPanelPrefab;
        public static GameObject PrimarySkillPrefab;
        public static GameObject ActiveSkillPrefab;
        public static GameObject PassiveSkillPrefab;
        public static GameObject HybridSkillPrefab;
        public static GameObject KeyBindWindowPrefab;
        public static GameObject ActivePresetWindowPrefab;
        public static GameObject ResetPresetWindowPrefab;

        public static GameObject PantheraHUDPrefab;
        public static GameObject CooldownFramePrefab;

        public static Material Body1Mat1;
        public static Material Body1Mat2;
        public static Material Body2Mat1;
        public static Material Body2Mat2;
        public static Material Body3Mat1;
        public static Material Body3Mat2;
        public static Mesh BodyMesh1;
        public static Mesh BodyMesh2;
        public static Mesh BodyMesh3;

        public static Material XRayMat;
        public static GameObject PantheraPostProcess;

        public static Texture DefaultPortrait;

        public static Sprite Portrait1;
        public static Sprite Portrait2;
        public static Sprite Portrait3;
        public static Sprite OverviewPortrait1;
        public static Sprite OverviewPortrait2;
        public static Sprite OverviewPortrait3;

        public static Sprite PassiveSkill;
        public static Sprite Rip;
        public static Sprite RipMenu;
        public static Sprite AirCleave;
        public static Sprite AirCleaveMenu;
        public static Sprite Leap;
        public static Sprite MightyRoar;
        public static Sprite ClawStorm;
        public static Sprite FrontShield;
        public static Sprite Prowl;
        public static Sprite ProwlActive;
        public static Sprite FuriousBite;
        public static Sprite Dash;
        public static Sprite ShieldBash;
        public static Sprite ZoneHeal;
        public static Sprite Slash;
        public static Sprite FireBird;
        public static Sprite Revive;
        public static Sprite Detection;
        public static Sprite DetectionActive;

        public static Sprite DestructionAbility;
        public static Sprite GuardianAbility;
        public static Sprite RuseAbility;
        public static Sprite HealingStormAbility;
        public static Sprite TornadoAbility;
        public static Sprite PowerfullJawsAbility;
        public static Sprite PredatorsDrinkAbility;
        public static Sprite ShadowMasterAbility;
        public static Sprite SilentPredatorAbility;
        public static Sprite PrimalStalkerAbility;
        public static Sprite ResidualEnergyAbility;
        public static Sprite DefensiveHasteAbility;
        public static Sprite KineticAbsorbtionAbility;
        public static Sprite WindWalkerAbility;
        public static Sprite PerspicacityAbility;
        public static Sprite HealingCleaveAbility;
        public static Sprite TheRipperAbility;
        public static Sprite InstinctiveResistanceAbility;
        public static Sprite BloodyRageAbility;
        public static Sprite GodOfReapersAbility;
        public static Sprite SaveMyFriendAbility;
        public static Sprite ShieldOfPowerAbility;
        public static Sprite GhostRipAbility;
        public static Sprite EchoAbility;
        public static Sprite PiercingWavesAbility;
        public static Sprite BurningSpiritAbility;
        public static Sprite HellCatAbility;
        public static Sprite CircularSawAbility;
        public static Sprite IgnitionAbility;
        public static Sprite SoulsShelterAbility;
        public static Sprite TheSlashPerAbility;
        public static Sprite HighTemperatureAbility;
        public static Sprite SacredFlamesAbility;
        public static Sprite AngryBirdAbility;
        public static Sprite PassivePowerAbility;
        public static Sprite PrescienceAbility;
        public static Sprite ConcentrationAbility;
        public static Sprite PrecisionAbility;
        public static Sprite DeterminationAbility;
        public static Sprite RegenerationAbility;
        public static Sprite StrongBarrierAbility;

        //public static Sprite MangleBuff;
        //public static Sprite RaySlashBuff;
        //public static Sprite ShieldBuff;
        //public static Sprite NineLivesBuff;
        //public static Sprite LeapCercleBuff;
        public static Sprite StealthBuff;

        public static GameObject LeftRipFX;
        public static GameObject RightRipFX;
        public static GameObject FrontRipFX;
        public static GameObject RipHitFX;
        public static GameObject LeftFireRipFX;
        public static GameObject RightFireRipFX;
        public static GameObject FrontFireRipFX;
        public static GameObject AirCleaveLeftFX;
        public static GameObject AirCleaveRightFX;
        public static GameObject FireAirCleaveLeftFX;
        public static GameObject FireAirCleaveRightFX;
        public static GameObject AirCleaveHitFX;
        public static GameObject ClawsStormWhiteFX;
        public static GameObject ClawsStormOrangeFX;
        public static GameObject MightyRoarFX;
        public static GameObject BiteFX;
        public static GameObject LeapTrailFX;
        public static GameObject DashFX;
        public static GameObject ZoneHealFX;
        public static GameObject FlashHealFX;
        public static GameObject LoopHealFX;
        public static GameObject FrontShieldHitFX;
        public static GameObject SlashFX;
        public static GameObject FireSlashFX;
        public static GameObject FireBirdFX;
        public static GameObject ReviveFX;
        public static GameObject DeadFX;
        //public static GameObject RightRipAtkFX;
        //public static GameObject LeftRipAtkFX;
        //public static GameObject MangleFX;
        //public static GameObject RaySlashFX;
        //public static GameObject RaySlashChargeFX;
        //public static GameObject NineLivesFX;
        //public static GameObject LeapCercleFX;
        //public static GameObject ShieldFX;

        public static GameObject AirCleaveLeftProjectile;
        public static GameObject AirCleaveRightProjectile;
        public static GameObject FireAirCleaveLeftProjectile;
        public static GameObject FireAirCleaveRightProjectile;
        public static GameObject FireBirdProjectile;
        //public static GameObject RaySlashProjectile;

        public static GameObject BlockEffectPrefab;

        // Loading Assets //
        public static void PopulateAssets()
        {

            // Load Asset Bundle //
            MainAssetBundle = AssetBundle.LoadFromMemory(Properties.Resources.PantheraBundle);

            // Load Models //
            MainPrefab = UnityEngine.Object.Instantiate(MainAssetBundle.LoadAsset<GameObject>("PantheraModel"));
            DisplayPrefab = UnityEngine.Object.Instantiate(MainAssetBundle.LoadAsset<GameObject>("PantheraModelDisplay"));

            // Load GUI Items //
            ConfigButtonPrefab = MainAssetBundle.LoadAsset<GameObject>("ConfigButton");
            ConfigPanelPrefab = MainAssetBundle.LoadAsset<GameObject>("PantheraPanel");
            PrimarySkillPrefab = MainAssetBundle.LoadAsset<GameObject>("SkillIconPrimary");
            ActiveSkillPrefab = MainAssetBundle.LoadAsset<GameObject>("SkillIconActive");
            PassiveSkillPrefab = MainAssetBundle.LoadAsset<GameObject>("SkillIconPassive");
            HybridSkillPrefab = MainAssetBundle.LoadAsset<GameObject>("SkillIconHybrid");
            KeyBindWindowPrefab = MainAssetBundle.LoadAsset<GameObject>("KeyBindWindow");
            ActivePresetWindowPrefab = MainAssetBundle.LoadAsset<GameObject>("ActivatePresetWindow");
            ResetPresetWindowPrefab = MainAssetBundle.LoadAsset<GameObject>("ResetPresetWindow");

            // Load HUD Items //
            PantheraHUDPrefab = MainAssetBundle.LoadAsset<GameObject>("PantheraHUD");
            CooldownFramePrefab = MainAssetBundle.LoadAsset<GameObject>("CooldownFrame");

            // Load Materials //
            Body1Mat1 = MainAssetBundle.LoadAsset<Material>("Cat1Mat1");
            Body1Mat2 = MainAssetBundle.LoadAsset<Material>("Cat1Mat2");
            Body2Mat1 = MainAssetBundle.LoadAsset<Material>("Cat2Mat1");
            Body2Mat2 = MainAssetBundle.LoadAsset<Material>("Cat2Mat2");
            Body3Mat1 = MainAssetBundle.LoadAsset<Material>("Cat3Mat1");
            Body3Mat2 = MainAssetBundle.LoadAsset<Material>("Cat3Mat2");
            BodyMesh1 = MainAssetBundle.LoadAsset<Mesh>("PantheraBody1");
            BodyMesh2 = MainAssetBundle.LoadAsset<Mesh>("PantheraBody2");
            BodyMesh3 = MainAssetBundle.LoadAsset<Mesh>("PantheraBody3");
            XRayMat = MainAssetBundle.LoadAsset<Material>("XRayMaterial");
            PantheraPostProcess = MainAssetBundle.LoadAsset<GameObject>("PantheraPostProcess");

            // Load Portrait //
            DefaultPortrait = MainAssetBundle.LoadAsset<Texture>("Portrait1");
            Portrait1 = MainAssetBundle.LoadAsset<Sprite>("Portrait1");
            Portrait2 = MainAssetBundle.LoadAsset<Sprite>("Portrait2");
            Portrait3 = MainAssetBundle.LoadAsset<Sprite>("Portrait3");
            OverviewPortrait1 = MainAssetBundle.LoadAsset<Sprite>("PantheraOverviewPortraitWhite");
            OverviewPortrait2 = MainAssetBundle.LoadAsset<Sprite>("PantheraOverviewPortraitOrange");
            OverviewPortrait3 = MainAssetBundle.LoadAsset<Sprite>("PantheraOverviewPortraitPrimalStalker");

            // Load Skills Sprites //
            PassiveSkill = MainAssetBundle.LoadAsset<Sprite>("FelineSkillsIconMenu");
            Rip = MainAssetBundle.LoadAsset<Sprite>("RipIcon");
            RipMenu = MainAssetBundle.LoadAsset<Sprite>("RipIconMenu");
            AirCleave = MainAssetBundle.LoadAsset<Sprite>("AirCleaveIcon");
            AirCleaveMenu = MainAssetBundle.LoadAsset<Sprite>("AirCleaveIconMenu");
            Leap = MainAssetBundle.LoadAsset<Sprite>("LeapIcon");
            MightyRoar = MainAssetBundle.LoadAsset<Sprite>("MightyRoarIcon");
            ClawStorm = MainAssetBundle.LoadAsset<Sprite>("ClawsStormIcon");
            FrontShield = MainAssetBundle.LoadAsset<Sprite>("FrontShieldIcon");
            Prowl = MainAssetBundle.LoadAsset<Sprite>("ProwlIcon");
            ProwlActive = MainAssetBundle.LoadAsset<Sprite>("ProwlActiveIcon");
            FuriousBite = MainAssetBundle.LoadAsset<Sprite>("FuriousBiteIcon");
            Dash = MainAssetBundle.LoadAsset<Sprite>("DashIcon");
            ShieldBash = MainAssetBundle.LoadAsset<Sprite>("ShieldBashIcon");
            ZoneHeal = MainAssetBundle.LoadAsset<Sprite>("ZoneHealIcon");
            Slash = MainAssetBundle.LoadAsset<Sprite>("SlashIcon");
            FireBird = MainAssetBundle.LoadAsset<Sprite>("FireBirdIcon");
            Revive = MainAssetBundle.LoadAsset<Sprite>("ReviveIcon");
            Detection = MainAssetBundle.LoadAsset<Sprite>("DetectionIcon");
            DetectionActive = MainAssetBundle.LoadAsset<Sprite>("DetectionActiveIcon");

            // Load Ability Sprites //
            DestructionAbility = MainAssetBundle.LoadAsset<Sprite>("DestructionAbility");
            GuardianAbility = MainAssetBundle.LoadAsset<Sprite>("GuardianAbility");
            RuseAbility = MainAssetBundle.LoadAsset<Sprite>("RuseAbility");
            HealingStormAbility = MainAssetBundle.LoadAsset<Sprite>("HealingStormAbility");
            TornadoAbility = MainAssetBundle.LoadAsset<Sprite>("TornadoAbility");
            PowerfullJawsAbility = MainAssetBundle.LoadAsset<Sprite>("PowerfullJawsAbility");
            PredatorsDrinkAbility = MainAssetBundle.LoadAsset<Sprite>("PredatorsDrinkAbility");
            ShadowMasterAbility = MainAssetBundle.LoadAsset<Sprite>("ShadowMasterAbility");
            SilentPredatorAbility = MainAssetBundle.LoadAsset<Sprite>("SilentPredatorAbility");
            PrimalStalkerAbility = MainAssetBundle.LoadAsset<Sprite>("PrimalStalkerAbility");
            ResidualEnergyAbility = MainAssetBundle.LoadAsset<Sprite>("ResidualEnergyAbility");
            DefensiveHasteAbility = MainAssetBundle.LoadAsset<Sprite>("DefensiveHasteAbility");
            KineticAbsorbtionAbility = MainAssetBundle.LoadAsset<Sprite>("KineticAbsorptionAbility");
            WindWalkerAbility = MainAssetBundle.LoadAsset<Sprite>("WindWalkerAbility");
            PerspicacityAbility = MainAssetBundle.LoadAsset<Sprite>("PerspicacityAbility");
            HealingCleaveAbility = MainAssetBundle.LoadAsset<Sprite>("HealingCleaveAbility");
            TheRipperAbility = MainAssetBundle.LoadAsset<Sprite>("TheRipperAbility");
            InstinctiveResistanceAbility = MainAssetBundle.LoadAsset<Sprite>("InstinctiveResistanceAbility");
            BloodyRageAbility = MainAssetBundle.LoadAsset<Sprite>("BloodyRageAbility");
            GodOfReapersAbility = MainAssetBundle.LoadAsset<Sprite>("GodOfReapersAbility");
            SaveMyFriendAbility = MainAssetBundle.LoadAsset<Sprite>("SaveMyFriendAbility");
            ShieldOfPowerAbility = MainAssetBundle.LoadAsset<Sprite>("ShieldOfPowerAbility");
            GhostRipAbility = MainAssetBundle.LoadAsset<Sprite>("GhostRipAbility");
            EchoAbility = MainAssetBundle.LoadAsset<Sprite>("EchoAbility");
            PiercingWavesAbility = MainAssetBundle.LoadAsset<Sprite>("PiercingWavesAbility");
            BurningSpiritAbility = MainAssetBundle.LoadAsset<Sprite>("BurningSpiritAbility");
            HellCatAbility = MainAssetBundle.LoadAsset<Sprite>("HellCatAbility");
            CircularSawAbility = MainAssetBundle.LoadAsset<Sprite>("CircularSawAbility");
            IgnitionAbility = MainAssetBundle.LoadAsset<Sprite>("IgnitionAbility");
            SoulsShelterAbility = MainAssetBundle.LoadAsset<Sprite>("SoulsShelterAbility");
            TheSlashPerAbility = MainAssetBundle.LoadAsset<Sprite>("TheSlashPerAbility");
            HighTemperatureAbility = MainAssetBundle.LoadAsset<Sprite>("HighTemperatureAbility");
            SacredFlamesAbility = MainAssetBundle.LoadAsset<Sprite>("SacredFlamesAbility");
            AngryBirdAbility = MainAssetBundle.LoadAsset<Sprite>("AngryBirdAbility");
            PassivePowerAbility = MainAssetBundle.LoadAsset<Sprite>("PassivePowerAbility");
            PrescienceAbility = MainAssetBundle.LoadAsset<Sprite>("PrescienceAbility");
            ConcentrationAbility = MainAssetBundle.LoadAsset<Sprite>("ConcentrationAbility");
            PrecisionAbility = MainAssetBundle.LoadAsset<Sprite>("PrecisionAbility");
            DeterminationAbility = MainAssetBundle.LoadAsset<Sprite>("DeterminationAbility");
            RegenerationAbility = MainAssetBundle.LoadAsset<Sprite>("RegenerationAbility");
            StrongBarrierAbility = MainAssetBundle.LoadAsset<Sprite>("StrongBarrierAbility");

            // Load Buff Sprites //
            //MangleBuff = MainAssetBundle.LoadAsset<Sprite>("MangleBuff");
            //RaySlashBuff = MainAssetBundle.LoadAsset<Sprite>("RaySlashBuff");
            //ShieldBuff = MainAssetBundle.LoadAsset<Sprite>("ShieldBuff");
            //NineLivesBuff = MainAssetBundle.LoadAsset<Sprite>("NineLivesBuff");
            //LeapCercleBuff = MainAssetBundle.LoadAsset<Sprite>("LeapCercleBuff");
            StealthBuff = MainAssetBundle.LoadAsset<Sprite>("ProwlIcon");

            // Load FX Prefabs //
            LeftRipFX = MainAssetBundle.LoadAsset<GameObject>("ClawAttackLeft");
            RightRipFX = MainAssetBundle.LoadAsset<GameObject>("ClawAttackRight");
            FrontRipFX = MainAssetBundle.LoadAsset<GameObject>("ClawAttackFront");
            RipHitFX = MainAssetBundle.LoadAsset<GameObject>("RipHitFX");
            LeftFireRipFX = MainAssetBundle.LoadAsset<GameObject>("FireClawAttackLeft");
            RightFireRipFX = MainAssetBundle.LoadAsset<GameObject>("FireClawAttackRight");
            FrontFireRipFX = MainAssetBundle.LoadAsset<GameObject>("FireClawAttackFront");
            AirCleaveLeftFX = MainAssetBundle.LoadAsset<GameObject>("AirCleaveLeftFX");
            AirCleaveRightFX = MainAssetBundle.LoadAsset<GameObject>("AirCleaveRightFX");
            FireAirCleaveLeftFX = MainAssetBundle.LoadAsset<GameObject>("FireAirCleaveLeftFX");
            FireAirCleaveRightFX = MainAssetBundle.LoadAsset<GameObject>("FireAirCleaveRightFX");
            AirCleaveHitFX = MainAssetBundle.LoadAsset<GameObject>("AirCleaveHitFX");
            ClawsStormWhiteFX = MainAssetBundle.LoadAsset<GameObject>("ClawsStormWhite");
            ClawsStormOrangeFX = MainAssetBundle.LoadAsset<GameObject>("ClawsStormOrange");
            BiteFX = MainAssetBundle.LoadAsset<GameObject>("BiteAttack");
            MightyRoarFX = MainAssetBundle.LoadAsset<GameObject>("MightyRoar");
            LeapTrailFX = MainAssetBundle.LoadAsset<GameObject>("LeapTrailFX");
            DashFX = MainAssetBundle.LoadAsset<GameObject>("DashFX");
            ZoneHealFX = MainAssetBundle.LoadAsset<GameObject>("ZoneHealFX");
            FlashHealFX = MainAssetBundle.LoadAsset<GameObject>("FlashHeal");
            LoopHealFX = MainAssetBundle.LoadAsset<GameObject>("LoopHeal");
            FrontShieldHitFX = MainAssetBundle.LoadAsset<GameObject>("FrontShieldHitFX");
            SlashFX = MainAssetBundle.LoadAsset<GameObject>("Slash");
            FireSlashFX = MainAssetBundle.LoadAsset<GameObject>("FireSlash");
            FireBirdFX = MainAssetBundle.LoadAsset<GameObject>("FireBirdFX");
            ReviveFX = MainAssetBundle.LoadAsset<GameObject>("ReviveFX");
            DeadFX = MainAssetBundle.LoadAsset<GameObject>("DeadFX");
            //RightRipAtkFX = Assets.MainAssetBundle.LoadAsset<GameObject>("RightRipAttack");
            //LeftRipAtkFX = Assets.MainAssetBundle.LoadAsset<GameObject>("LeftRipAttack");
            //MangleFX = Assets.MainAssetBundle.LoadAsset<GameObject>("MangleFX");
            //RaySlashFX = Assets.MainAssetBundle.LoadAsset<GameObject>("RaySlash");
            //RaySlashChargeFX = Assets.MainAssetBundle.LoadAsset<GameObject>("RaySlashCharge");
            //NineLivesFX = Assets.MainAssetBundle.LoadAsset<GameObject>("NineLiveFX");
            //LeapCercleFX = Assets.MainAssetBundle.LoadAsset<GameObject>("LeapCercle");
            //ShieldFX = Assets.MainAssetBundle.LoadAsset<GameObject>("ShieldFX");

            // Set up Block Effect Prefab //
            BlockEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/BearProc").InstantiateClone("PantheraBlockEffect", true);
            BlockEffectPrefab.GetComponent<EffectComponent>().soundName = Sound.ShieldAbsorb;
            LoadEffect(BlockEffectPrefab, 5, Sound.ShieldAbsorb);

            // Add Effect definitions //
            LoadEffect(LeftRipFX, 2);
            LoadEffect(RightRipFX, 2);
            LoadEffect(FrontRipFX, 2);
            LoadEffect(RipHitFX, 0.5f);
            LoadEffect(LeftFireRipFX, 2);
            LoadEffect(RightFireRipFX, 2);
            LoadEffect(FrontFireRipFX, 2);
            LoadEffect(AirCleaveHitFX, 0.5f);
            LoadEffect(ClawsStormWhiteFX, 300);
            LoadEffect(ClawsStormOrangeFX, 300);
            LoadEffect(BiteFX, 2, Sound.FuriousBite);
            LoadEffect(MightyRoarFX, 3, Sound.MightyRoar);
            LoadEffect(LeapTrailFX);
            LoadEffect(DashFX);
            LoadEffect(ZoneHealFX, 15);
            LoadEffect(FlashHealFX, 2);
            LoadEffect(LoopHealFX, 100);
            LoadEffect(FrontShieldHitFX, 0.5f);
            LoadEffect(SlashFX, 1);
            LoadEffect(FireSlashFX, 2.5f);
            LoadEffect(ReviveFX, 10);
            LoadEffect(DeadFX, 8);
            //LoadEffect(RightRipAtkFX, 3);
            //LoadEffect(LeftRipAtkFX, 3);
            //LoadEffect(NineLivesFX, 3, Sound.NineLives);
            //LoadEffect(MangleFX, 2, Sound.Mangle);
            //LoadEffect(RaySlashChargeFX);
            //LoadEffect(LeapCercleFX, PantheraConfig.LeapCercle_leapCercleDuration);
            //LoadEffect(ShieldFX);

            // Add the FXStop to the Leap cercle FX //
            //LeapCercleFX.AddComponent<LeapCercleComponent>();

            // Create projectiles //
            AirCleaveLeftProjectile = CreateProjectile(AirCleaveLeftFX, PantheraConfig.AirCleave_leftProjectileName, Utils.Sound.AirCleaveHit, AirCleaveHitFX);
            AirCleaveRightProjectile = CreateProjectile(AirCleaveRightFX, PantheraConfig.AirCleave_rightProjectileName, Utils.Sound.AirCleaveHit, AirCleaveHitFX);
            FireAirCleaveLeftProjectile = CreateProjectile(FireAirCleaveLeftFX, PantheraConfig.FireAirCleave_leftProjectileName, Utils.Sound.AirCleaveHit, AirCleaveHitFX);
            FireAirCleaveRightProjectile = CreateProjectile(FireAirCleaveRightFX, PantheraConfig.FireAirCleave_rightProjectileName, Utils.Sound.AirCleaveHit, AirCleaveHitFX);
            FireBirdProjectile = CreateProjectile(FireBirdFX, PantheraConfig.FireBird_projectileName, Utils.Sound.AirCleaveHit, AirCleaveHitFX, false);
            //RaySlashProjectile = Assets.CreateProjectile(RaySlashFX, "raySlashProjectile");

            // Set up projectiles //
            AirCleaveLeftProjectile.GetComponent<ProjectileSimple>().lifetime = 5;
            AirCleaveLeftProjectile.GetComponent<ProjectileSimple>().desiredForwardSpeed = 50;
            GameObject.DestroyImmediate(AirCleaveLeftProjectile.GetComponent<Collider>());
            BoxCollider airCleaveLeftProjectileCollider = AirCleaveLeftProjectile.AddComponent<BoxCollider>();
            airCleaveLeftProjectileCollider.center = AirCleaveLeftFX.GetComponent<BoxCollider>().center;
            airCleaveLeftProjectileCollider.size = AirCleaveLeftFX.GetComponent<BoxCollider>().size;
            airCleaveLeftProjectileCollider.isTrigger = true;

            AirCleaveRightProjectile.GetComponent<ProjectileSimple>().lifetime = 5;
            AirCleaveRightProjectile.GetComponent<ProjectileSimple>().desiredForwardSpeed = 50;
            GameObject.DestroyImmediate(AirCleaveRightProjectile.GetComponent<Collider>());
            BoxCollider airCleaveRightProjectileCollider = AirCleaveRightProjectile.AddComponent<BoxCollider>();
            airCleaveRightProjectileCollider.center = AirCleaveRightFX.GetComponent<BoxCollider>().center;
            airCleaveRightProjectileCollider.size = AirCleaveRightFX.GetComponent<BoxCollider>().size;
            airCleaveRightProjectileCollider.isTrigger = true;

            FireAirCleaveLeftProjectile.GetComponent<ProjectileSimple>().lifetime = 5;
            FireAirCleaveLeftProjectile.GetComponent<ProjectileSimple>().desiredForwardSpeed = 50;
            GameObject.DestroyImmediate(FireAirCleaveLeftProjectile.GetComponent<Collider>());
            BoxCollider fireAirCleaveLeftProjectileCollider = FireAirCleaveLeftProjectile.AddComponent<BoxCollider>();
            fireAirCleaveLeftProjectileCollider.center = AirCleaveLeftFX.GetComponent<BoxCollider>().center;
            fireAirCleaveLeftProjectileCollider.size = AirCleaveLeftFX.GetComponent<BoxCollider>().size;
            fireAirCleaveLeftProjectileCollider.isTrigger = true;

            FireAirCleaveRightProjectile.GetComponent<ProjectileSimple>().lifetime = 5;
            FireAirCleaveRightProjectile.GetComponent<ProjectileSimple>().desiredForwardSpeed = 50;
            GameObject.DestroyImmediate(FireAirCleaveRightProjectile.GetComponent<Collider>());
            BoxCollider fireAirCleaveRightProjectileCollider = FireAirCleaveRightProjectile.AddComponent<BoxCollider>();
            fireAirCleaveRightProjectileCollider.center = AirCleaveRightFX.GetComponent<BoxCollider>().center;
            fireAirCleaveRightProjectileCollider.size = AirCleaveRightFX.GetComponent<BoxCollider>().size;
            fireAirCleaveRightProjectileCollider.isTrigger = true;

            FireBirdProjectile.GetComponent<ProjectileSimple>().lifetime = 20;
            FireBirdProjectile.GetComponent<ProjectileSimple>().desiredForwardSpeed = 50;
            GameObject.DestroyImmediate(FireBirdProjectile.GetComponent<Collider>());
            BoxCollider FireBirdProjectileCollider = FireBirdProjectile.AddComponent<BoxCollider>();
            FireBirdProjectileCollider.center = FireBirdFX.GetComponent<BoxCollider>().center;
            FireBirdProjectileCollider.size = FireBirdFX.GetComponent<BoxCollider>().size;
            FireBirdProjectileCollider.isTrigger = true;

            LoopSoundDef fireBirdSoundLoop = ScriptableObject.CreateInstance<LoopSoundDef>();
            fireBirdSoundLoop.startSoundName = Utils.Sound.FireBirdLoopStart;
            fireBirdSoundLoop.stopSoundName = Utils.Sound.FireBirdLoopStop;
            FireBirdProjectile.GetComponent<ProjectileController>().flightSoundLoop = fireBirdSoundLoop;


        }

        public static GameObject CreateProjectile(GameObject FXPrefab, string name, string impactSound = "", GameObject impactEffect = null, bool destroyOnHit = true, bool destroyOnWorld = false)
        {

            // Create the projectile //
            GameObject projectile = Resources.Load<GameObject>("Prefabs/Projectiles/FMJ").InstantiateClone(name, true);

            // Setup projectile controler //
            ProjectileController controler = projectile.GetComponent<ProjectileController>();

            // Change the Projectile Type //
            PantheraProjectileComponent ptraProjComp = projectile.AddComponent<PantheraProjectileComponent>();
            ptraProjComp.projectileName = name;
            ptraProjComp.impactSound = impactSound;
            ptraProjComp.impactEffect = impactEffect;
            ptraProjComp.destroyOnHit = destroyOnHit;
            ptraProjComp.destroyOnWorld = destroyOnWorld;
            controler.canImpactOnTrigger = true;
                
            // Setup the FX //
            GameObject projectileGhost = FXPrefab.InstantiateClone(name + "Ghost", false);
            projectileGhost.AddComponent<ProjectileGhostController>();
            projectileGhost.AddComponent<VFXAttributes>();
            controler.ghostPrefab = projectileGhost;

            // Disable the Overlap Attack //
            projectile.GetComponent<ProjectileOverlapAttack>().enabled = false;            

            // Save the projectile //
            projectilePrefabs.Add(projectile);
            Prefabs.AddPrefab(projectile);
            Prefab.networkedObjectPrefabs.Add(projectile);
            projectile.RegisterNetworkPrefab();

            return projectile;

        }

        private static GameObject LoadEffect(GameObject prefab, float duration = 0, string soundName = null)
        {

            if (duration > 0) prefab.AddComponent<DestroyOnTimer>().duration = duration;
            if (prefab.GetComponent<NetworkIdentity>() == null) prefab.AddComponent<NetworkIdentity>().localPlayerAuthority = true;
            var vfxAttributes = prefab.GetComponent<VFXAttributes>() ? prefab.GetComponent<VFXAttributes>() : prefab.AddComponent<VFXAttributes>();
            vfxAttributes.vfxPriority = VFXAttributes.VFXPriority.Always;
            var effect = prefab.GetComponent<EffectComponent>() ? prefab.GetComponent<EffectComponent>() : prefab.AddComponent<EffectComponent>();
            effect.applyScale = false;
            effect.effectIndex = EffectIndex.Invalid;
            effect.parentToReferencedTransform = true;
            effect.positionAtReferencedTransform = true;
            effect.soundName = soundName;

            AddEffect(prefab, soundName);

            return prefab;
        }

        private static void AddEffect(GameObject effectPrefab, string soundName = null)
        {
            EffectDef newEffectDef = new EffectDef();
            newEffectDef.prefab = effectPrefab;
            newEffectDef.prefabEffectComponent = effectPrefab.GetComponent<EffectComponent>();
            newEffectDef.prefabName = effectPrefab.name;
            newEffectDef.prefabVfxAttributes = effectPrefab.GetComponent<VFXAttributes>();
            newEffectDef.spawnSoundEventName = soundName;

            effectDefs.Add(newEffectDef);
            Prefab.networkedObjectPrefabs.Add(effectPrefab);
            Prefabs.AddPrefab(effectPrefab);
            effectPrefab.RegisterNetworkPrefab();

        }

    }

}
