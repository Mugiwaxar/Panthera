using EntityStates;
using KinematicCharacterController;
using MonoMod.RuntimeDetour.HookGen;
using Panthera;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.Machines;
using Panthera.MachineScripts;
using Panthera.Utils;
using R2API;
using RoR2;
using RoR2.Networking;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using static R2API.LoadoutAPI;
using static RoR2.CharacterSelectSurvivorPreviewDisplayController;

namespace Panthera.Base
{
    class Prefab
    {

        public static List<GameObject> masterPrefabs = new List<GameObject>();
        public static List<GameObject> bodyPrefabs = new List<GameObject>();
        public static List<SurvivorDef> SurvivorDefinitions = new List<SurvivorDef>();
        public static List<SkillFamily> skillFamilies = new List<SkillFamily>();
        public static List<SkillDef> skillDefs = new List<SkillDef>();
        public static List<Type> entityStates = new List<Type>();
        public static GameObject CharacterPrefab;
        public static GameObject CharacterDisplayPrefab;

        public static readonly Color CharacterColor = new Color(1, 1, 1);
        public static DamageAPI.ModdedDamageType BarrierDamageType;

        public static void RegisterCharacter()
        {

            // Create the Prefabs //
            Prefab.CharacterPrefab = Prefab.CreateCharacterPrefab(PantheraAssets.MainPrefab, PantheraConfig.Model_PrefabName);
            Prefab.CharacterDisplayPrefab = Prefab.CreateDisplayPrefab(PantheraAssets.DisplayPrefab, PantheraConfig.Model_PrefabName, Prefab.CharacterPrefab);
            Prefab.RegisterSkills(Prefab.CharacterPrefab);

            // Create the survivor def //
            SurvivorDef survivorDef = ScriptableObject.CreateInstance<SurvivorDef>();

            survivorDef.bodyPrefab = Prefab.CharacterPrefab;
            survivorDef.displayPrefab = Prefab.CharacterDisplayPrefab;
            survivorDef.primaryColor = CharacterColor;
            survivorDef.displayNameToken = PantheraTokens.Get("PANTHERA_NAME");
            survivorDef.descriptionToken = PantheraTokens.Get("PANTHERA_DESC");
            survivorDef.desiredSortPosition = 999;
            //survivorDef.unlockableDef = unlockableDef;

            // Register the survivor //
            SurvivorDefinitions.Add(survivorDef);

            // Register Damage Type //
            //BarrierDamageType = DamageAPI.ReserveDamageType();

        }

        public static GameObject CreateCharacterPrefab(GameObject prefab, string name)
        {
            #region Prefab
            GameObject characterPrefab = new GameObject(name);
            GameObject disabledGameObject = new GameObject(name + "DisabledObj");
            disabledGameObject.SetActive(false);
            characterPrefab.transform.parent = disabledGameObject.transform;
            UnityEngine.Object.DontDestroyOnLoad(disabledGameObject);
            #endregion
            #region NetworkIdentity
            characterPrefab.AddComponent<NetworkIdentity>().localPlayerAuthority = true;
            #endregion
            #region PantheraObj
            PantheraObj pantheraObj = characterPrefab.AddComponent<PantheraObj>();
            #endregion
            #region Loading Model
            GameObject model = prefab;
            //model.AddComponent<NetworkIdentity>().localPlayerAuthority = true;
            #endregion
            #region ChildLocator
            ChildLocator childLocator = model.GetComponent<ChildLocator>();
            #endregion
            #region Transform
            GameObject gameObject = new GameObject("ModelBase");
            gameObject.transform.parent = characterPrefab.transform;
            gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
            gameObject.transform.localRotation = Quaternion.identity;
            gameObject.transform.localScale = new Vector3(1, 1, 1);

            GameObject gameObject2 = new GameObject("CameraPivot");
            gameObject2.transform.parent = gameObject.transform;
            gameObject2.transform.localPosition = new Vector3(0f, 0f, 0f);
            gameObject2.transform.localRotation = Quaternion.identity;
            gameObject2.transform.localScale = Vector3.one;

            GameObject gameObject3 = new GameObject("AimOrigin");
            gameObject3.transform.parent = gameObject.transform;
            gameObject3.transform.localPosition = new Vector3(0f, 1.3f, 0.877f);
            gameObject3.transform.localRotation = Quaternion.identity;
            gameObject3.transform.localScale = Vector3.one;

            Transform transform = model.transform;
            transform.parent = gameObject.transform;
            transform.localScale = new Vector3(PantheraConfig.Model_defaultModelScale, PantheraConfig.Model_defaultModelScale, PantheraConfig.Model_defaultModelScale);
            #endregion
            #region SkillLocator
            PantheraSkillLocator skillLocator = characterPrefab.AddComponent<PantheraSkillLocator>();
            skillLocator.ptraObj = pantheraObj;
            #endregion
            #region CharacterDirection
            CharacterDirection characterDirection = characterPrefab.AddComponent<CharacterDirection>();
            characterDirection.moveVector = Vector3.zero;
            characterDirection.targetTransform = gameObject.transform;
            characterDirection.overrideAnimatorForwardTransform = null;
            characterDirection.rootMotionAccumulator = null;
            characterDirection.modelAnimator = model.GetComponentInChildren<Animator>();
            characterDirection.driveFromRootRotation = false;
            characterDirection.turnSpeed = 720f;
            #endregion
            #region Panthera Body
            PantheraBody bodyComponent = characterPrefab.AddComponent<PantheraBody>();
            bodyComponent.bodyIndex = (BodyIndex)(-1);
            bodyComponent.name = "PantheraBody";
            bodyComponent.baseNameToken = PantheraTokens.Get("PANTHERA_NAME");
            bodyComponent.subtitleNameToken = PantheraTokens.Get("PANTHERA_SUBTITLE");
            //bodyComponent.masterObject = Resources.Load<GameObject>("Prefabs/CharacterBodies/CommandoBody");
            bodyComponent.bodyFlags = CharacterBody.BodyFlags.IgnoreFallDamage;
            bodyComponent.bodyColor = Prefab.CharacterColor;
            bodyComponent.rootMotionInMainState = false;
            bodyComponent.mainRootSpeed = 0;
            bodyComponent.baseMaxHealth = PantheraConfig.Default_MaxHealth;
            bodyComponent.levelMaxHealth = PantheraConfig.Default_MaxHealthLevel;
            bodyComponent.baseRegen = PantheraConfig.Default_HealthRegen;
            bodyComponent.levelRegen = PantheraConfig.Default_HealthRegenLevel;
            bodyComponent.baseMaxShield = 0;
            bodyComponent.levelMaxShield = 0;
            bodyComponent.baseMoveSpeed = PantheraConfig.Default_MoveSpeed;
            bodyComponent.levelMoveSpeed = PantheraConfig.Default_MoveSpeedLevel;
            bodyComponent.baseAcceleration = 80;
            bodyComponent.baseJumpPower = 18;
            bodyComponent.levelJumpPower = 0;
            bodyComponent.baseDamage = PantheraConfig.Default_Damage;
            bodyComponent.levelDamage = PantheraConfig.Default_DamageLevel;
            bodyComponent.baseAttackSpeed = PantheraConfig.Default_AttackSpeed;
            bodyComponent.levelAttackSpeed = PantheraConfig.Default_AttackSpeedLevel;
            bodyComponent.baseCrit = PantheraConfig.Default_Critic;
            bodyComponent.levelCrit = PantheraConfig.Default_CriticLevel;
            bodyComponent.baseArmor = PantheraConfig.Default_Defense;
            bodyComponent.levelArmor = PantheraConfig.Default_DefenseLevel;
            bodyComponent.baseJumpCount = (int)PantheraConfig.Default_jumpCount;
            bodyComponent.sprintingSpeedMultiplier = 1.4f;
            bodyComponent.wasLucky = false;
            bodyComponent.hideCrosshair = true;
            bodyComponent._defaultCrosshairPrefab = null;
            bodyComponent.aimOriginTransform = gameObject3.transform;
            bodyComponent.hullClassification = HullClassification.Human;
            bodyComponent.portraitIcon = PantheraAssets.DefaultPortrait.texture;
            bodyComponent.isChampion = false;
            bodyComponent.currentVehicle = null;
            bodyComponent.skinIndex = 0U;
            bodyComponent.preferredPodPrefab = null;
            #endregion
            #region Panthera Motor
            PantheraMotor characterMotor = characterPrefab.AddComponent<PantheraMotor>();
            characterMotor.walkSpeedPenaltyCoefficient = 1f;
            characterMotor.characterDirection = characterDirection;
            characterMotor.muteWalkMotion = false;
            characterMotor.mass = 430f;
            characterMotor.airControl = 0.50f;
            characterMotor.disableAirControlUntilCollision = false;
            characterMotor.generateParametersOnAwake = true;
            characterMotor.useGravity = true;
            characterMotor.isFlying = false;
            #endregion
            #region Panthera input bank
            PantheraInputBank inputBankTest = characterPrefab.AddComponent<PantheraInputBank>();
            inputBankTest.moveVector = Vector3.zero;
            inputBankTest.enabled = false;
            #endregion
            #region CameraTargetParams
            CameraTargetParams cameraTargetParams = characterPrefab.AddComponent<CameraTargetParams>();
            cameraTargetParams.cameraParams = Resources.Load<GameObject>("Prefabs/CharacterBodies/CommandoBody").GetComponent<CameraTargetParams>().cameraParams;
            cameraTargetParams.cameraPivotTransform = null;
            cameraTargetParams.RequestAimType(CameraTargetParams.AimType.Standard);
            cameraTargetParams.recoil = Vector2.zero;
            cameraTargetParams.dontRaycastToPivot = false;
            #endregion
            #region ModelLocator
            ModelLocator modelLocator = characterPrefab.AddComponent<ModelLocator>();
            modelLocator.modelTransform = transform;
            modelLocator.modelBaseTransform = gameObject.transform;
            modelLocator.dontReleaseModelOnDeath = false;
            modelLocator.autoUpdateModelTransform = true;
            modelLocator.dontDetatchFromParent = false;
            modelLocator.noCorpse = false;
            modelLocator.normalizeToFloor = true;
            modelLocator.preserveModel = false;
            #endregion
            #region CharacterModel
            CharacterModel characterModel = model.AddComponent<CharacterModel>();
            characterModel.body = bodyComponent;
            characterModel.baseRendererInfos = new CharacterModel.RendererInfo[]
            {
                new CharacterModel.RendererInfo
                {
                    defaultMaterial = PantheraAssets.skin1Mat,
                    renderer = model.GetComponentInChildren<SkinnedMeshRenderer>(),
                    defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                    ignoreOverlays = false
                }
            };
            characterModel.autoPopulateLightInfos = true;
            characterModel.invisibilityCount = 0;
            characterModel.temporaryOverlays = new List<TemporaryOverlayInstance>();
            characterModel.mainSkinnedMeshRenderer = model.GetComponentInChildren<SkinnedMeshRenderer>();
            #endregion
            #region TeamComponent
            TeamComponent teamComponent = characterPrefab.GetComponent<TeamComponent>();
            teamComponent.hideAllyCardDisplay = false;
            teamComponent.teamIndex = TeamIndex.None;
            #endregion
            #region Panthera Health Component
            PantheraHealthComponent healthComponent = characterPrefab.AddComponent<PantheraHealthComponent>();
            healthComponent.health = 90f;
            healthComponent.shield = 0f;
            healthComponent.barrier = 0f;
            healthComponent.magnetiCharge = 0f;
            healthComponent.body = null;
            healthComponent.dontShowHealthbar = false;
            healthComponent.globalDeathEventChanceCoefficient = 1f;
            #endregion
            #region Interactor
            PantheraInteractor interactor = characterPrefab.AddComponent<PantheraInteractor>();
            interactor.ptraObj = pantheraObj;
            interactor.maxInteractionDistance = 3f;
            #endregion
            #region InteractionDriver
            characterPrefab.AddComponent<InteractionDriver>().highlightInteractor = true;
            #endregion
            #region CharacterNetworkTransform
            characterPrefab.AddComponent<CharacterNetworkTransform>();
            #endregion
            #region SfxLocator
            SfxLocator sfxLocator = characterPrefab.AddComponent<SfxLocator>();
            sfxLocator.deathSound = "Play_ui_player_death";
            sfxLocator.barkSound = "";
            sfxLocator.openSound = "";
            sfxLocator.landingSound = "Play_char_land";
            sfxLocator.fallDamageSound = "Play_char_land_fall_damage";
            sfxLocator.aliveLoopStart = "";
            sfxLocator.aliveLoopStop = "";
            #endregion
            #region Rigidbody
            Rigidbody rigidbody = characterPrefab.AddComponent<Rigidbody>();
            rigidbody.mass = 100f;
            rigidbody.drag = 0f;
            rigidbody.angularDrag = 0f;
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;
            rigidbody.interpolation = RigidbodyInterpolation.None;
            rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
            rigidbody.constraints = RigidbodyConstraints.None;
            #endregion
            #region CapsuleCollider
            CapsuleCollider capsuleCollider = characterPrefab.AddComponent<CapsuleCollider>();
            capsuleCollider.isTrigger = false;
            capsuleCollider.material = null;
            capsuleCollider.direction = 0;
            capsuleCollider.radius = PantheraConfig.Model_defaultCapsuleRadius;
            capsuleCollider.height = PantheraConfig.Model_defaultCapsuleHeight;
            capsuleCollider.center = Vector3.zero;
            #endregion
            #region Kinematic Panthera Motor
            PantheraKinematicMotor kinematicCharacterMotor = characterPrefab.AddComponent<PantheraKinematicMotor>();
            kinematicCharacterMotor.CharacterController = characterMotor;
            kinematicCharacterMotor.Capsule = capsuleCollider;
            kinematicCharacterMotor.CapsuleRadius = capsuleCollider.radius;
            kinematicCharacterMotor.CapsuleHeight = capsuleCollider.height;
            kinematicCharacterMotor.CapsuleYOffset = 0;
            kinematicCharacterMotor._attachedRigidbody = rigidbody;
            kinematicCharacterMotor.playerCharacter = true;
            //kinematicCharacterMotor.DetectDiscreteCollisions = false;
            kinematicCharacterMotor.GroundDetectionExtraDistance = 0f;
            kinematicCharacterMotor.MaxStepHeight = 0.2f;
            kinematicCharacterMotor.MinRequiredStepDepth = 0.1f;
            kinematicCharacterMotor.MaxStableSlopeAngle = 45f;
            kinematicCharacterMotor.MaxStableDistanceFromLedge = 0.5f;
            //kinematicCharacterMotor.PreventSnappingOnLedges = false;
            kinematicCharacterMotor.MaxStableDenivelationAngle = 55f;
            kinematicCharacterMotor.RigidbodyInteractionType = RigidbodyInteractionType.None;
            kinematicCharacterMotor.PreserveAttachedRigidbodyMomentum = true;
            kinematicCharacterMotor.HasPlanarConstraint = false;
            kinematicCharacterMotor.PlanarConstraintAxis = Vector3.up;
            kinematicCharacterMotor.StepHandling = StepHandlingMethod.None;
            //kinematicCharacterMotor.LedgeHandling = true;
            kinematicCharacterMotor.InteractiveRigidbodyHandling = true;
            //kinematicCharacterMotor.SafeMovement = false;
            #endregion
            #region HurtBoxGroup
            HurtBoxGroup hurtBoxGroup = model.AddComponent<HurtBoxGroup>();
            HurtBox mainHurtbox = childLocator.FindChild("MainHurtbox").gameObject.AddComponent<HurtBox>();
            mainHurtbox.transform.localPosition = new Vector3(0f, 0f, -0.3f);
            mainHurtbox.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            mainHurtbox.gameObject.layer = LayerIndex.entityPrecise.intVal;
            mainHurtbox.healthComponent = healthComponent;
            mainHurtbox.isBullseye = true;
            mainHurtbox.damageModifier = HurtBox.DamageModifier.Normal;
            mainHurtbox.hurtBoxGroup = hurtBoxGroup;
            mainHurtbox.indexInGroup = 0;
            //HurtBox shieldHurtbox = childLocator.FindChild("ShieldHurtbox").gameObject.AddComponent<HurtBox>();
            //shieldHurtbox.gameObject.layer = LayerIndex.entityPrecise.intVal;
            //shieldHurtbox.healthComponent = healthComponent;
            //shieldHurtbox.isBullseye = false;
            //shieldHurtbox.damageModifier = HurtBox.DamageModifier.Barrier;
            //shieldHurtbox.hurtBoxGroup = hurtBoxGroup;
            //shieldHurtbox.indexInGroup = 1;
            hurtBoxGroup.hurtBoxes = new HurtBox[]
            {
                mainHurtbox
            };
            hurtBoxGroup.mainHurtBox = mainHurtbox;
            hurtBoxGroup.bullseyeCount = 1;
            #endregion
            #region FootstepHandler
            FootstepHandler footstepHandler = model.AddComponent<FootstepHandler>();
            footstepHandler.baseFootstepString = "Play_player_footstep";
            footstepHandler.sprintFootstepOverrideString = "";
            footstepHandler.enableFootstepDust = true;
            footstepHandler.footstepDustPrefab = Resources.Load<GameObject>("Prefabs/GenericFootstepDust");
            #endregion
            #region RagdollController
            RagdollController ragdollController = model.AddComponent<RagdollController>();
            ragdollController.bones = null;
            ragdollController.componentsToDisableOnRagdoll = null;
            #endregion
            #region AimAnimator
            AimAnimator aimAnimator = model.AddComponent<AimAnimator>();
            aimAnimator.inputBank = inputBankTest;
            aimAnimator.directionComponent = characterDirection;
            aimAnimator.pitchRangeMax = 55f;
            aimAnimator.pitchRangeMin = -50f;
            aimAnimator.yawRangeMin = -44f;
            aimAnimator.yawRangeMax = 44f;
            aimAnimator.pitchGiveupRange = 30f;
            aimAnimator.yawGiveupRange = 10f;
            aimAnimator.giveupDuration = 8f;
            #endregion
            #region EquipmentSlot
            characterPrefab.AddComponent<EquipmentSlot>();
            #endregion
            #region Main Panthera Machine
            PantheraMainMachine mainStateMachine = characterPrefab.AddComponent<PantheraMainMachine>();
            mainStateMachine.name = "Main Machine";
            mainStateMachine.enabled = false;
            #endregion
            #region Passive Panthera Machine
            PantheraPassiveMachine pantheraPassiveMachine = characterPrefab.AddComponent<PantheraPassiveMachine>();
            pantheraPassiveMachine.name = "Passive Machine";
            pantheraPassiveMachine.enabled = false;
            #endregion
            #region Skill Machine 1
            PantheraSkillsMachine skillMachine1 = characterPrefab.AddComponent<PantheraSkillsMachine>();
            skillMachine1.name = "Skill Machine 1";
            skillMachine1.enabled = false;
            #endregion
            #region Skill Machine 2
            PantheraSkillsMachine skillMachine2 = characterPrefab.AddComponent<PantheraSkillsMachine>();
            skillMachine2.name = "Skill Machine 2";
            skillMachine2.enabled = false;
            #endregion
            #region Panthera Network Machine
            PantheraNetworkMachine networkMachine = characterPrefab.AddComponent<PantheraNetworkMachine>();
            networkMachine.name = "Network Machine";
            networkMachine.enabled = false;
            #endregion
            #region Panthera Death Behavion
            PantheraDeathBehavior characterDeathBehavior = characterPrefab.AddComponent<PantheraDeathBehavior>();
            characterDeathBehavior.ptraObj = pantheraObj;
            characterDeathBehavior.deathMachine = skillMachine1;
            characterDeathBehavior.deathState = new SerializableEntityStateType(typeof(GenericCharacterDeath));
            #endregion
            #region SetStateOnHurt  < -------- TO SEE
            SetStateOnHurt stateOnHurt = characterPrefab.AddComponent<SetStateOnHurt>();
            stateOnHurt.canBeStunned = false;
            stateOnHurt.canBeHitStunned = false;
            stateOnHurt.canBeFrozen = false;
            //stateOnHurt.targetStateMachine = bodyMachine;
            Array.Resize(ref stateOnHurt.idleStateMachine, 1);
            //stateOnHurt.idleStateMachine[0] = weaponMachine;
            stateOnHurt.hurtState = new SerializableEntityStateType(typeof(Idle));
            #endregion
            #region Panthera FX
            PantheraFX pantheraFX = characterPrefab.AddComponent<PantheraFX>();
            #endregion
            #region Tracker
            var tracker = characterPrefab.AddComponent<HuntressTracker>();
            tracker.maxTrackingDistance = PantheraConfig.Tracker_maxDistance;
            tracker.maxTrackingAngle = PantheraConfig.Tracker_maxAngle;
            tracker.enabled = true;
            #endregion
            #region Hitboxes
            Functions.CreateHitbox(model, childLocator.FindChild("RipHitBox"), PantheraConfig.Rip_hitboxName);
            Functions.CreateHitbox(model, childLocator.FindChild("ClawsStormHitBox"), PantheraConfig.ClawsStorm_hitboxName);
            #endregion
            #region ItemChange
            PantheraItemChange itemChange = characterPrefab.AddComponent<PantheraItemChange>();
            itemChange.ptraObj = pantheraObj;
            #endregion
            #region Combo Component
            PantheraComboComponent comboComponent = characterPrefab.AddComponent<PantheraComboComponent>();
            comboComponent.ptraObj = pantheraObj;
            #endregion
            #region Register
            bodyPrefabs.Add(characterPrefab);
            characterPrefab.RegisterNetworkPrefab();
            return characterPrefab;
            #endregion
        }

        public static GameObject CreateDisplayPrefab(GameObject prefab, string name, GameObject mainPrefab)
        {

            // Create the display prefab //
            GameObject DisplayCharacterPrefab = Resources.Load<GameObject>("Prefabs/CharacterBodies/CommandoBody").InstantiateClone(name + "Display");
            UnityEngine.Object.DestroyImmediate(DisplayCharacterPrefab.transform.Find("ModelBase").gameObject);
            UnityEngine.Object.DestroyImmediate(DisplayCharacterPrefab.transform.Find("CameraPivot").gameObject);
            UnityEngine.Object.DestroyImmediate(DisplayCharacterPrefab.transform.Find("AimOrigin").gameObject);

            // Get the display model //
            GameObject model = prefab;

            // Set up the display model //
            GameObject modelBase = new GameObject("ModelBase");
            modelBase.transform.parent = DisplayCharacterPrefab.transform;
            modelBase.transform.position = new Vector3(0f, 0f, 0f);
            modelBase.transform.localPosition = new Vector3(0f, 0.7f, 0f);
            modelBase.transform.localRotation = Quaternion.identity;
            modelBase.transform.localScale = new Vector3(1, 1, 1);

            GameObject cameraPivot = new GameObject("CameraPivot");
            cameraPivot.transform.parent = modelBase.transform;
            cameraPivot.transform.localPosition = new Vector3(0f, 1.6f, 0f);
            cameraPivot.transform.localRotation = Quaternion.identity;
            cameraPivot.transform.localScale = Vector3.one;

            GameObject aimOrigin = new GameObject("AimOrigin");
            aimOrigin.transform.parent = modelBase.transform;
            aimOrigin.transform.localPosition = new Vector3(0f, 2.2f, 0f);
            aimOrigin.transform.localRotation = Quaternion.identity;
            aimOrigin.transform.localScale = Vector3.one;
            DisplayCharacterPrefab.GetComponent<CharacterBody>().aimOriginTransform = aimOrigin.transform;

            model.transform.parent = modelBase.transform;
            model.transform.localPosition = Vector3.zero;
            model.transform.localRotation = Quaternion.identity;

            // Create the Model Renderer //
            CharacterModel characterModel = model.AddComponent<CharacterModel>();
            characterModel.baseRendererInfos = DisplayCharacterPrefab.GetComponentInChildren<CharacterModel>().baseRendererInfos;

            // Add the Sound Component //
            model.gameObject.AddComponent<DisplayModelComponent>();

            // Return the Display Prefab //
            return model;

        }

        public static void CreateDoppelganger()
        {

            GameObject newMaster = PrefabAPI.InstantiateClone(Resources.Load<GameObject>("Prefabs/CharacterMasters/CommandoMonsterMaster"), "PantheraMonster", true);
            newMaster.GetComponent<CharacterMaster>().bodyPrefab = Prefab.CharacterPrefab;

            masterPrefabs.Add(newMaster);
        }

        public static void RegisterSkills(GameObject prefab)
        {

            // Get the skill locator //
            SkillLocator skillLocator = prefab.GetComponent<SkillLocator>();

            UnityEngine.Object.DestroyImmediate(skillLocator.primary);
            UnityEngine.Object.DestroyImmediate(skillLocator.secondary);
            UnityEngine.Object.DestroyImmediate(skillLocator.utility);
            UnityEngine.Object.DestroyImmediate(skillLocator.special);

            // Add skill family 1 //
            skillLocator.primary = prefab.AddComponent<GenericSkill>();
            skillLocator.primary.skillName = PantheraTokens.Get("skill_RipName");
            SkillFamily primaryFamily = ScriptableObject.CreateInstance<SkillFamily>();
            (primaryFamily as ScriptableObject).name = prefab.name + "PrimaryFamily";
            primaryFamily.variants = new SkillFamily.Variant[0];
            skillLocator.primary._skillFamily = primaryFamily;
            skillFamilies.Add(primaryFamily);

            // Add skill family 2 //
            skillLocator.secondary = prefab.AddComponent<GenericSkill>();
            skillLocator.secondary.skillName = PantheraTokens.Get("skill_SlashName");
            SkillFamily secondaryFamily = ScriptableObject.CreateInstance<SkillFamily>();
            (secondaryFamily as ScriptableObject).name = prefab.name + "SecondaryFamily";
            secondaryFamily.variants = new SkillFamily.Variant[0];
            skillLocator.secondary._skillFamily = secondaryFamily;
            skillFamilies.Add(secondaryFamily);

            // Add skill family 3 //
            skillLocator.utility = prefab.AddComponent<GenericSkill>();
            skillLocator.utility.skillName = PantheraTokens.Get("skill_LeapName");
            SkillFamily utilityFamily = ScriptableObject.CreateInstance<SkillFamily>();
            (utilityFamily as ScriptableObject).name = prefab.name + "UtilityFamily";
            utilityFamily.variants = new SkillFamily.Variant[0];
            skillLocator.utility._skillFamily = utilityFamily;
            skillFamilies.Add(utilityFamily);

            // Add skill family 4 //
            skillLocator.special = prefab.AddComponent<GenericSkill>();
            skillLocator.special.skillName = PantheraTokens.Get("skill_MightyRoarName");
            SkillFamily specialFamily = ScriptableObject.CreateInstance<SkillFamily>();
            (specialFamily as ScriptableObject).name = prefab.name + "SpecialFamily";
            specialFamily.variants = new SkillFamily.Variant[0];
            skillLocator.special._skillFamily = specialFamily;
            skillFamilies.Add(specialFamily);

            // Create all skills //
            RegisterFakePassive(prefab);
            //Skills.PantheraSpawn.register();
            RegisterFakeSkill1(primaryFamily);
            RegisterFakeSkill2(secondaryFamily);
            RegisterFakeSkill3(utilityFamily);
            RegisterFakeSkill4(specialFamily);

        }

        public static void RegisterFakePassive(GameObject prefab)
        {

            SkillLocator skillLocator = prefab.GetComponent<SkillLocator>();

            skillLocator.passiveSkill.enabled = true;
            skillLocator.passiveSkill.skillNameToken = PantheraTokens.Get("skill_passiveName");
            skillLocator.passiveSkill.skillDescriptionToken = PantheraTokens.Get("skill_passiveDesc");
            skillLocator.passiveSkill.icon = PantheraAssets.FelineSkillsAbilityMenu;


        }

        public static void RegisterFakeSkill1(SkillFamily family)
        {

            // Build the Skill //
            SkillDef skill1Def = ScriptableObject.CreateInstance<SkillDef>();
            skill1Def.skillNameToken = PantheraTokens.Get("skill_RipName");
            skill1Def.skillDescriptionToken = String.Format(PantheraTokens.Get("skill_RipDesc"), PantheraConfig.Rip_atkDamageMultiplier * 100);
            skill1Def.skillName = PantheraTokens.Get("skill_RipName");
            skill1Def.keywordTokens = new string[] { };
            skill1Def.icon = PantheraAssets.RipSkillMenu;
            skill1Def.baseMaxStock = 1;
            skill1Def.baseRechargeInterval = PantheraConfig.Rip_cooldown;
            skill1Def.beginSkillCooldownOnSkillEnd = false;
            skill1Def.fullRestockOnAssign = false;
            skill1Def.rechargeStock = 1;
            skill1Def.requiredStock = 1;
            skill1Def.stockToConsume = 1;

            // Save the skill //
            Array.Resize(ref family.variants, 1);
            family.variants[0] = new SkillFamily.Variant
            {
                skillDef = skill1Def,
                viewableNode = new ViewablesCatalog.Node(skill1Def.skillNameToken, false, null)
            };

        }

        public static void RegisterFakeSkill2(SkillFamily family)
        {

            // Build the Skill //
            SkillDef skill2Def = ScriptableObject.CreateInstance<SkillDef>();
            skill2Def.skillNameToken = PantheraTokens.Get("skill_SlashName");
            skill2Def.skillDescriptionToken = string.Format(PantheraTokens.Get("skill_SlashDesc"), PantheraConfig.Slash_damageMultiplier * 100);
            skill2Def.skillName = PantheraTokens.Get("skill_SlashName");
            skill2Def.keywordTokens = new string[] { };
            skill2Def.icon = PantheraAssets.SlashSkillMenu;
            skill2Def.baseMaxStock = 1;
            skill2Def.baseRechargeInterval = PantheraConfig.Slash_cooldown;
            skill2Def.beginSkillCooldownOnSkillEnd = false;
            skill2Def.fullRestockOnAssign = false;
            skill2Def.rechargeStock = 1;
            skill2Def.requiredStock = 1;
            skill2Def.stockToConsume = 1;

            // Save the skill //
            Array.Resize(ref family.variants, 1);
            family.variants[0] = new SkillFamily.Variant
            {
                skillDef = skill2Def,
                viewableNode = new ViewablesCatalog.Node(skill2Def.skillNameToken, false, null)
            };

            // Register the Skill inside the Content Pack //
            //Prefab.skillDefs.Add(skill2Def);
            //Prefab.entityStates.Add(typeof(AirCleave));
        }

        public static void RegisterFakeSkill3(SkillFamily family)
        {

            // Build the Skill //
            SkillDef skill3Def = ScriptableObject.CreateInstance<SkillDef>();
            skill3Def.skillNameToken = PantheraTokens.Get("skill_LeapName");
            skill3Def.skillDescriptionToken = PantheraTokens.Get("skill_LeapDesc");
            skill3Def.skillName = PantheraTokens.Get("skill_LeapName");
            skill3Def.keywordTokens = new string[] { };
            skill3Def.icon = PantheraAssets.LeapSkillMenu;
            skill3Def.baseMaxStock = 1;
            skill3Def.baseRechargeInterval = PantheraConfig.Leap_cooldown;
            skill3Def.beginSkillCooldownOnSkillEnd = false;
            skill3Def.fullRestockOnAssign = false;
            skill3Def.rechargeStock = 1;
            skill3Def.requiredStock = 1;
            skill3Def.stockToConsume = 1;

            // Save the skill //
            Array.Resize(ref family.variants, 1);
            family.variants[0] = new SkillFamily.Variant
            {
                skillDef = skill3Def,
                viewableNode = new ViewablesCatalog.Node(skill3Def.skillNameToken, false, null)
            };

        }

        public static void RegisterFakeSkill4(SkillFamily family)
        {

            // Build the Skill //
            SkillDef skill4Def = ScriptableObject.CreateInstance<SkillDef>();
            skill4Def.skillNameToken = PantheraTokens.Get("skill_MightyRoarName");
            skill4Def.skillDescriptionToken = String.Format(PantheraTokens.Get("skill_MightyRoarDesc"), PantheraConfig.MightyRoar_radius, PantheraConfig.MightyRoar_stunDuration, PantheraConfig.MightyRoar_damage * 100, PantheraConfig.MightyRoar_damageCount);
            skill4Def.skillName = PantheraTokens.Get("skill_MightyRoarName");
            skill4Def.keywordTokens = new string[] { };
            skill4Def.icon = PantheraAssets.MightyRoarSkillMenu;
            skill4Def.baseMaxStock = 1;
            skill4Def.baseRechargeInterval = PantheraConfig.MightyRoar_cooldown;
            skill4Def.beginSkillCooldownOnSkillEnd = false;
            skill4Def.fullRestockOnAssign = false;
            skill4Def.rechargeStock = 1;
            skill4Def.requiredStock = 1;
            skill4Def.stockToConsume = 1;

            // Save the skill //
            Array.Resize(ref family.variants, 1);
            family.variants[0] = new SkillFamily.Variant
            {
                skillDef = skill4Def,
                viewableNode = new ViewablesCatalog.Node(skill4Def.skillNameToken, false, null)
            };
        }

    }
}
