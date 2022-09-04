using Panthera.Components;
using Panthera.Utils;
using R2API;
using RoR2;
using RoR2.Audio;
using RoR2.Projectile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Panthera
{
    public static class Assets
    {

        private const string csProjName = "Panthera";
        private const string assetbundleName = "PantheraBundle";

        internal static List<EffectDef> effectDefs = new List<EffectDef>();
        internal static List<GameObject> projectilePrefabs = new List<GameObject>();

        public static AssetBundle MainAssetBundle = null;
        public static Material NormalMaterial;
        public static Material StealMaterial;

        public static Texture Portrait;

        public static Sprite PassiveSkill;
        public static Sprite Rip;
        public static Sprite RipMenu;
        public static Sprite AirCleave;
        public static Sprite AirCleaveMenu;
        public static Sprite Leap;
        public static Sprite LeapMenu;
        public static Sprite MightyRoar;
        public static Sprite MightyRoarMenu;
        public static Sprite ClawStorm;
        public static Sprite FrontShield;
        public static Sprite Prowl;
        public static Sprite ProwlActive;
        public static Sprite FuriousBite;

        public static Sprite DestructionAbility;
        public static Sprite GuardianAbility;
        public static Sprite RuseAbility;

        public static Sprite MangleBuff;
        public static Sprite RaySlashBuff;
        public static Sprite ShieldBuff;
        public static Sprite NineLivesBuff;
        public static Sprite LeapCercleBuff;
        public static Sprite StealBuff;

        public static GameObject LeftRipFX;
        public static GameObject RightRipFX;
        public static GameObject FrontRipFX;
        public static GameObject RightRipAtkFX;
        public static GameObject LeftRipAtkFX;
        public static GameObject LeftRipProjectileFX;
        public static GameObject RightRipProjectileFX;
        public static GameObject ClawsStormFX;
        public static GameObject BiteFX;
        //public static GameObject MangleFX;
        public static GameObject MightyRoarFX;
        public static GameObject RaySlashFX;
        public static GameObject RaySlashChargeFX;
        //public static GameObject NineLivesFX;
        public static GameObject LeapCercleFX;
        public static GameObject ShieldFX;
        public static GameObject LeapTrailFX;
        public static GameObject SuperSprintFX;

        public static GameObject LeftRipProjectile;
        public static GameObject RightRipProjectile;
        public static GameObject RaySlashProjectile;

        public static GameObject BlockEffectPrefab;

        // Loading Assets //
        public static void PopulateAssets()
        {

            // Load Asset Bundle //
            MainAssetBundle = AssetBundle.LoadFromMemory(Properties.Resources.PantheraBundle);

            // Load materials //
            Assets.NormalMaterial = Assets.MainAssetBundle.LoadAsset<Material>("7020102");
            Assets.StealMaterial = Assets.MainAssetBundle.LoadAsset<Material>("7020102_Alpha");

            // Load portrait //
            Portrait = MainAssetBundle.LoadAsset<Texture>("Portrait");

            // Load skills sprites //
            PassiveSkill = MainAssetBundle.LoadAsset<Sprite>("FelineSkillsIconMenu");
            Rip = MainAssetBundle.LoadAsset<Sprite>("RipIcon");
            RipMenu = MainAssetBundle.LoadAsset<Sprite>("RipIconMenu");
            AirCleave = MainAssetBundle.LoadAsset<Sprite>("AirCleaveIcon");
            AirCleaveMenu = MainAssetBundle.LoadAsset<Sprite>("AirCleaveIconMenu");
            Leap = MainAssetBundle.LoadAsset<Sprite>("LeapIcon");
            LeapMenu = MainAssetBundle.LoadAsset<Sprite>("LeapIconMenu");
            MightyRoar = MainAssetBundle.LoadAsset<Sprite>("MightyRoarIcon");
            MightyRoarMenu = MainAssetBundle.LoadAsset<Sprite>("MightyRoarIconMenu");
            ClawStorm = MainAssetBundle.LoadAsset<Sprite>("ClawStormIcon");
            FrontShield = MainAssetBundle.LoadAsset<Sprite>("FrontShieldIcon");
            Prowl = MainAssetBundle.LoadAsset<Sprite>("ProwlIcon");
            ProwlActive = MainAssetBundle.LoadAsset<Sprite>("ProwlActiveIcon");
            FuriousBite = MainAssetBundle.LoadAsset<Sprite>("FuriousBiteIcon");

            DestructionAbility = MainAssetBundle.LoadAsset<Sprite>("DestructionAbility");
            GuardianAbility = MainAssetBundle.LoadAsset<Sprite>("GuardianAbility");
            RuseAbility = MainAssetBundle.LoadAsset<Sprite>("RuseAbility");

            MangleBuff = MainAssetBundle.LoadAsset<Sprite>("MangleBuff");
            RaySlashBuff = MainAssetBundle.LoadAsset<Sprite>("RaySlashBuff");
            ShieldBuff = MainAssetBundle.LoadAsset<Sprite>("ShieldBuff");
            NineLivesBuff = MainAssetBundle.LoadAsset<Sprite>("NineLivesBuff");
            LeapCercleBuff = MainAssetBundle.LoadAsset<Sprite>("LeapCercleBuff");
            StealBuff = MainAssetBundle.LoadAsset<Sprite>("ProwlIcon");

            // Load FX prefabs //
            LeftRipFX = Assets.MainAssetBundle.LoadAsset<GameObject>("ClawAttackLeft");
            RightRipFX = Assets.MainAssetBundle.LoadAsset<GameObject>("ClawAttackRight");
            FrontRipFX = Assets.MainAssetBundle.LoadAsset<GameObject>("ClawAttackFront");
            RightRipAtkFX = Assets.MainAssetBundle.LoadAsset<GameObject>("RightRipAttack");
            LeftRipAtkFX = Assets.MainAssetBundle.LoadAsset<GameObject>("LeftRipAttack");
            LeftRipProjectileFX = Assets.MainAssetBundle.LoadAsset<GameObject>("ClawProjectileLeft");
            RightRipProjectileFX = Assets.MainAssetBundle.LoadAsset<GameObject>("ClawProjectileRight");
            ClawsStormFX = Assets.MainAssetBundle.LoadAsset<GameObject>("ClawsStorm");
            BiteFX = Assets.MainAssetBundle.LoadAsset<GameObject>("BiteAttack");
            //MangleFX = Assets.MainAssetBundle.LoadAsset<GameObject>("MangleFX");
            MightyRoarFX = Assets.MainAssetBundle.LoadAsset<GameObject>("MightyRoar");
            RaySlashFX = Assets.MainAssetBundle.LoadAsset<GameObject>("RaySlash");
            RaySlashChargeFX = Assets.MainAssetBundle.LoadAsset<GameObject>("RaySlashCharge");
            //NineLivesFX = Assets.MainAssetBundle.LoadAsset<GameObject>("NineLiveFX");
            LeapCercleFX = Assets.MainAssetBundle.LoadAsset<GameObject>("LeapCercle");
            ShieldFX = Assets.MainAssetBundle.LoadAsset<GameObject>("ShieldFX");
            LeapTrailFX = Assets.MainAssetBundle.LoadAsset<GameObject>("LeapTrailFX");
            SuperSprintFX = Assets.MainAssetBundle.LoadAsset<GameObject>("SuperSprintFX");

            // Set up Block Effect Prefab //
            BlockEffectPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/BearProc").InstantiateClone("PantheraBlockEffect", true);
            BlockEffectPrefab.GetComponent<EffectComponent>().soundName = Sound.ShieldAbsorb;
            LoadEffect(BlockEffectPrefab, 5, Sound.ShieldAbsorb);

            // Add effect definitions //
            LoadEffect(LeftRipFX, 3);
            LoadEffect(RightRipFX, 3);
            LoadEffect(FrontRipFX, 3);
            LoadEffect(RightRipAtkFX, 3);
            LoadEffect(LeftRipAtkFX, 3);
            LoadEffect(ClawsStormFX, 300);
            LoadEffect(BiteFX, 2, Sound.FuriousBite);
            //LoadEffect(MangleFX, 2, Sound.Mangle);
            LoadEffect(MightyRoarFX, 3, Sound.MightyRoar);
            LoadEffect(RaySlashChargeFX);
            //LoadEffect(NineLivesFX, 3, Sound.NineLives);
            LoadEffect(LeapCercleFX, PantheraConfig.LeapCercle_leapCercleDuration);
            LoadEffect(ShieldFX);
            LoadEffect(LeapTrailFX);
            LoadEffect(SuperSprintFX);

            // Add the FXStop to the Leap cercle FX //
            LeapCercleFX.AddComponent<LeapCercleComponent>();

            // Create projectiles //
            LeftRipProjectile = Assets.CreateProjectile(LeftRipProjectileFX, "leftRipProjectile");
            RightRipProjectile = Assets.CreateProjectile(RightRipProjectileFX, "rightRipProjectile");
            RaySlashProjectile = Assets.CreateProjectile(RaySlashFX, "raySlashProjectile");

            // Set up projectiles //
            LeftRipProjectile.GetComponent<ProjectileSimple>().lifetime = 5;
            LeftRipProjectile.GetComponent<ProjectileSimple>().desiredForwardSpeed = 150;
            LeftRipProjectile.GetComponent<ProjectileDamage>().enabled = false;

            RightRipProjectile.GetComponent<ProjectileSimple>().lifetime = 5;
            RightRipProjectile.GetComponent<ProjectileSimple>().desiredForwardSpeed = 150;
            RightRipProjectile.GetComponent<ProjectileDamage>().enabled = false;

            //LoopSoundDef soundDef = ScriptableObject.CreateInstance<LoopSoundDef>();
            //soundDef.startSoundName = Utils.Sound.RaySlashLoopStart;
            //soundDef.stopSoundName = Utils.Sound.RaySlashLoopStop;
            //RaySlashProjectile.GetComponent<ProjectileSimple>().lifetime = 20;
            //RaySlashProjectile.GetComponent<ProjectileSimple>().desiredForwardSpeed = 100;
            //RaySlashProjectile.GetComponent<ProjectileDamage>().enabled = false;
            //RaySlashProjectile.GetComponent<ProjectileController>().flightSoundLoop = soundDef;


        }

        public static GameObject CreateProjectile(GameObject FXPrefab, string name)
        {

            // Create the projectile //
            GameObject projectile = PrefabAPI.InstantiateClone(Resources.Load<GameObject>("Prefabs/Projectiles/FMJ"), name, true);

            // Get the child locator //
            ChildLocator childLocator = FXPrefab.GetComponent<ChildLocator>();
            Transform newHitboxTransform = childLocator.FindChild("HitBox");

            // Set the hitbox //
            HitBox oldHitbox = projectile.GetComponentInChildren<HitBox>();
            HitBox newHitBox = newHitboxTransform.gameObject.AddComponent<HitBox>();
            oldHitbox.transform.localPosition = newHitBox.transform.localPosition;
            oldHitbox.transform.localRotation = newHitBox.transform.localRotation;
            oldHitbox.transform.localScale = newHitBox.transform.localScale;

            // Setup projectile controler //
            ProjectileController controler = projectile.GetComponent<ProjectileController>();

            // Setup the FX //
            GameObject projectileGhost = FXPrefab.InstantiateClone(name + "Ghost", false);
            projectileGhost.AddComponent<ProjectileGhostController>();
            projectileGhost.AddComponent<VFXAttributes>();
            controler.ghostPrefab = projectileGhost;

            // Save the projectile //
            Assets.projectilePrefabs.Add(projectile);

            return projectile;

        }

        private static GameObject LoadEffect(GameObject prefab, float duration = 0, string soundName = null)
        {

            if (duration > 0) prefab.AddComponent<DestroyOnTimer>().duration = duration;
            if(prefab.GetComponent<NetworkIdentity>() == null) prefab.AddComponent<NetworkIdentity>();
            var vfxAttributes = prefab.GetComponent<VFXAttributes>() ? prefab.GetComponent<VFXAttributes>() : prefab.AddComponent<VFXAttributes>();
            vfxAttributes.vfxPriority = VFXAttributes.VFXPriority.Always;
            var effect = prefab.GetComponent<EffectComponent>() ? prefab.GetComponent <EffectComponent>() : prefab.AddComponent<EffectComponent>();
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
        }

    }

}
