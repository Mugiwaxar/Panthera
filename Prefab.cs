using System.Collections.Generic;
using R2API;
using EntityStates;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;
using KinematicCharacterController;
using RoR2.Skills;
using System;
using RoR2.Networking;
using Panthera.Components;
using Panthera.Machines;
using Panthera.Skills;
using Panthera.MachineScripts;
using Panthera.Utils;

namespace Panthera
{
    class Prefab
    {
        internal static List<GameObject> bodyPrefabs = new List<GameObject>();
        internal static List<SkillFamily> skillFamilies = new List<SkillFamily>();
        internal static List<SkillDef> skillDefs = new List<SkillDef>();
        internal static List<Type> entityStates = new List<Type>();
        public static GameObject characterPrefab;

        public static void CreatePrefab()
        {
            #region Prefab
            // Create the prefab //
            characterPrefab = new GameObject("PantheraPrefab");
            GameObject disabledGameObject = new GameObject("PantheraMain");
            disabledGameObject.SetActive(false);
            characterPrefab.transform.parent = disabledGameObject.transform;
            GameObject.DontDestroyOnLoad(disabledGameObject);
            #endregion
            #region PantheraObj
            // Add the PantheraObj component //
            PantheraObj pantheraObj = characterPrefab.AddComponent<PantheraObj>();
            #endregion
            #region NetworkIdentity
            characterPrefab.AddComponent<NetworkIdentity>().localPlayerAuthority = true;
            #endregion
            #region Loading Model
            // Loading the Panthera Model //
            GameObject model = GameObject.Instantiate(Assets.MainAssetBundle.LoadAsset<GameObject>("Panthera"));
            model.AddComponent<NetworkIdentity>().localPlayerAuthority = true;
            #endregion
            #region ChildLocator
            ChildLocator childLocator = model.GetComponent<ChildLocator>();
            #endregion
            #region Transform
            // Set Transform //
            GameObject gameObject = new GameObject("ModelBase");
            gameObject.transform.parent = characterPrefab.transform;
            gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
            gameObject.transform.localRotation = Quaternion.identity;
            gameObject.transform.localScale = new Vector3(1, 1, 1);

            GameObject gameObject2 = new GameObject("CameraPivot");
            gameObject2.transform.parent = gameObject.transform;
            gameObject2.transform.localPosition = new Vector3(0f, 1.6f, 0f);
            gameObject2.transform.localRotation = Quaternion.identity;
            gameObject2.transform.localScale = Vector3.one;

            GameObject gameObject3 = new GameObject("AimOrigin");
            gameObject3.transform.parent = gameObject.transform;
            gameObject3.transform.localPosition = new Vector3(0f, 1.4f, 0f);
            gameObject3.transform.localRotation = Quaternion.identity;
            gameObject3.transform.localScale = Vector3.one;

            Transform transform = model.transform;
            transform.parent = gameObject.transform;
            transform.localScale = new Vector3(PantheraConfig.Model_generalScale, PantheraConfig.Model_generalScale, PantheraConfig.Model_generalScale);
            #endregion
            #region SkillLocator
            SkillLocator skillLoctor = characterPrefab.AddComponent<SkillLocator>();
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
            #region CharacterBody
            PantheraBody bodyComponent = characterPrefab.AddComponent<PantheraBody>();
            bodyComponent.bodyIndex = (BodyIndex)(-1);
            bodyComponent.name = "PantheraBody";
            bodyComponent.baseNameToken = "PantheraName";
            bodyComponent.subtitleNameToken = "PantheraSubtitle";
            //bodyComponent.masterObject = Resources.Load<GameObject>("Prefabs/CharacterBodies/CommandoBody");
            bodyComponent.bodyFlags = CharacterBody.BodyFlags.IgnoreFallDamage;
            bodyComponent.bodyColor = Character.CharacterColor;
            bodyComponent.rootMotionInMainState = false;
            bodyComponent.mainRootSpeed = 0;
            bodyComponent.baseMaxHealth = 180;
            bodyComponent.levelMaxHealth = 30;
            bodyComponent.baseRegen = 1.5f;
            bodyComponent.levelRegen = 0.15f;
            bodyComponent.baseMaxShield = 0;
            bodyComponent.levelMaxShield = 0;
            bodyComponent.baseMoveSpeed = 9;
            bodyComponent.levelMoveSpeed = 0.03f;
            bodyComponent.baseAcceleration = 80;
            bodyComponent.baseJumpPower = 15;
            bodyComponent.levelJumpPower = 0;
            bodyComponent.baseDamage = 15;
            bodyComponent.levelDamage = 2;
            bodyComponent.baseAttackSpeed = 1f;
            bodyComponent.levelAttackSpeed = 0.03f;
            bodyComponent.baseCrit = 15;
            bodyComponent.levelCrit = 0.5f;
            bodyComponent.baseArmor = 15;
            bodyComponent.levelArmor = 0.5f;
            bodyComponent.baseJumpCount = 1;
            bodyComponent.sprintingSpeedMultiplier = 1.5f;
            bodyComponent.wasLucky = false;
            bodyComponent.hideCrosshair = true;
            bodyComponent.aimOriginTransform = gameObject3.transform;
            bodyComponent.hullClassification = HullClassification.Human;
            bodyComponent.portraitIcon = Assets.Portrait;
            bodyComponent.isChampion = false;
            bodyComponent.currentVehicle = null;
            bodyComponent.skinIndex = 0U;
            bodyComponent.preferredPodPrefab = null;
            #endregion
            #region CharacterMotor
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
            //Material mat1 = Assets.mainAssetBundle.LoadAsset<Material>("7020102");
            //Material mat2 = Assets.mainAssetBundle.LoadAsset<Material>("effect_7020102_L");
            //Material mat3 = Assets.mainAssetBundle.LoadAsset<Material>("7020102_Alpha");

            CharacterModel characterModel = model.AddComponent<CharacterModel>();
            characterModel.body = bodyComponent;
            characterModel.baseRendererInfos = new CharacterModel.RendererInfo[]
            {
                //new CharacterModel.RendererInfo
                //{
                //    defaultMaterial = mat1,
                //    renderer = model.GetComponentInChildren<SkinnedMeshRenderer>(),
                //    defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                //    ignoreOverlays = true
                //},
                //new CharacterModel.RendererInfo
                //{
                //    defaultMaterial = mat2,
                //    renderer = model.GetComponentInChildren<SkinnedMeshRenderer>(),
                //    defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                //    ignoreOverlays = true
                //},
                //new CharacterModel.RendererInfo
                //{
                //    defaultMaterial = mat3,
                //    renderer = model.GetComponentInChildren<SkinnedMeshRenderer>(),
                //    defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                //    ignoreOverlays = true
                //}
            };
            characterModel.autoPopulateLightInfos = true;
            characterModel.invisibilityCount = 0;
            characterModel.temporaryOverlays = new List<TemporaryOverlay>();
            characterModel.mainSkinnedMeshRenderer = model.GetComponentInChildren<SkinnedMeshRenderer>();
            #endregion
            #region TeamComponent
            TeamComponent teamComponent = characterPrefab.GetComponent<TeamComponent>();
            teamComponent.hideAllyCardDisplay = false;
            teamComponent.teamIndex = TeamIndex.None;
            #endregion
            #region Panthera heal component
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
            characterPrefab.AddComponent<Interactor>().maxInteractionDistance = 3f;
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
            capsuleCollider.radius = 0.4f;
            capsuleCollider.height = 2;
            capsuleCollider.center = Vector3.zero;
            #endregion
            #region KinematicCharacterMotor
            PantheraKinematicMotor kinematicCharacterMotor = characterPrefab.AddComponent<PantheraKinematicMotor>();
            kinematicCharacterMotor.CharacterController = characterMotor;
            kinematicCharacterMotor.Capsule = capsuleCollider;
            kinematicCharacterMotor.CapsuleRadius = capsuleCollider.radius;
            kinematicCharacterMotor.CapsuleHeight = capsuleCollider.height;
            kinematicCharacterMotor.Rigidbody = rigidbody;
            kinematicCharacterMotor.DetectDiscreteCollisions = false;
            kinematicCharacterMotor.GroundDetectionExtraDistance = 0f;
            kinematicCharacterMotor.MaxStepHeight = 0.2f;
            kinematicCharacterMotor.MinRequiredStepDepth = 0.1f;
            kinematicCharacterMotor.MaxStableSlopeAngle = 45f;
            kinematicCharacterMotor.MaxStableDistanceFromLedge = 0.5f;
            kinematicCharacterMotor.PreventSnappingOnLedges = false;
            kinematicCharacterMotor.MaxStableDenivelationAngle = 55f;
            kinematicCharacterMotor.RigidbodyInteractionType = RigidbodyInteractionType.None;
            kinematicCharacterMotor.PreserveAttachedRigidbodyMomentum = true;
            kinematicCharacterMotor.HasPlanarConstraint = false;
            kinematicCharacterMotor.PlanarConstraintAxis = Vector3.up;
            kinematicCharacterMotor.StepHandling = StepHandlingMethod.None;
            kinematicCharacterMotor.LedgeHandling = true;
            kinematicCharacterMotor.InteractiveRigidbodyHandling = true;
            kinematicCharacterMotor.SafeMovement = false;
            #endregion
            #region HurtBoxGroup
            HurtBoxGroup hurtBoxGroup = model.AddComponent<HurtBoxGroup>();
            HurtBox mainHurtbox = childLocator.FindChild("MainHurtbox").gameObject.AddComponent<HurtBox>();
            mainHurtbox.gameObject.layer = LayerIndex.entityPrecise.intVal;
            mainHurtbox.healthComponent = healthComponent;
            mainHurtbox.isBullseye = true;
            mainHurtbox.damageModifier = HurtBox.DamageModifier.Normal;
            mainHurtbox.hurtBoxGroup = hurtBoxGroup;
            mainHurtbox.indexInGroup = 0;
            HurtBox shieldHurtbox = childLocator.FindChild("ShieldHurtbox").gameObject.AddComponent<HurtBox>();
            shieldHurtbox.gameObject.layer = LayerIndex.entityPrecise.intVal;
            shieldHurtbox.healthComponent = healthComponent;
            shieldHurtbox.isBullseye = false;
            shieldHurtbox.damageModifier = HurtBox.DamageModifier.Barrier;
            shieldHurtbox.hurtBoxGroup = hurtBoxGroup;
            shieldHurtbox.indexInGroup = 1;
            hurtBoxGroup.hurtBoxes = new HurtBox[]
            {
                mainHurtbox,
                shieldHurtbox
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
            mainStateMachine.mainScript = typeof(MainScript).FullName;
            mainStateMachine.name = "Main Machine";
            mainStateMachine.enabled = false;
            #endregion
            #region Passive Panthera Machine
            PantheraPassiveMachine pantheraPassiveMachine = characterPrefab.AddComponent<PantheraPassiveMachine>();
            pantheraPassiveMachine.mainScript = typeof(BigCatPassive).FullName;
            pantheraPassiveMachine.name = "Passive Machine";
            pantheraPassiveMachine.enabled = false;
            #endregion
            #region Skill Machine
            PantheraSkillsMachine skillMachine = characterPrefab.AddComponent<PantheraSkillsMachine>();
            skillMachine.name = "Skill Machine";
            skillMachine.enabled = false;
            #endregion
            #region PantheraNetworkMachine
            PantheraNetworkMachine networkMachine = characterPrefab.AddComponent<PantheraNetworkMachine>();
            networkMachine.name = "Network Machine";
            networkMachine.mainScript = typeof(NetworkScript).FullName;
            #endregion
            #region CharacterDeathBehavion  < -------- TO SEE
            CharacterDeathBehavior characterDeathBehavior = characterPrefab.AddComponent<CharacterDeathBehavior>();
            //characterDeathBehavior.deathStateMachine = bodyMachine;
            characterDeathBehavior.deathState = new SerializableEntityStateType(typeof(GenericCharacterDeath));
            #endregion
            #region SetStateOnHurt  < -------- TO SEE
            SetStateOnHurt stateOnHurt = characterPrefab.AddComponent<SetStateOnHurt>();
            stateOnHurt.canBeStunned = false;
            stateOnHurt.canBeHitStunned = false;
            stateOnHurt.canBeFrozen = true;
            //stateOnHurt.targetStateMachine = bodyMachine;
            Array.Resize<EntityStateMachine>(ref stateOnHurt.idleStateMachine, 1);
            //stateOnHurt.idleStateMachine[0] = weaponMachine;
            stateOnHurt.hurtState = new SerializableEntityStateType(typeof(EntityStates.Idle));
            #endregion
            #region PantheraFX
            PantheraFX pantheraFX = characterPrefab.AddComponent<PantheraFX>();
            #endregion
            #region Tracker
            var tracker = characterPrefab.AddComponent<HuntressTracker>();
            tracker.maxTrackingDistance = PantheraConfig.Tracker_maxDistance;
            tracker.maxTrackingAngle = PantheraConfig.Tracker_maxAngle;
            tracker.enabled = true;
            #endregion
            #region Hitboxes
            // Add the Hitboxes //
            Utils.Functions.CreateHitbox(model, childLocator.FindChild("RipHitBox"), "Rip");
            Utils.Functions.CreateHitbox(model, childLocator.FindChild("FrontRipHitBox"), "FrontRip");
            Utils.Functions.CreateHitbox(model, childLocator.FindChild("RightRipHitBox"), "RightRip");
            Utils.Functions.CreateHitbox(model, childLocator.FindChild("LeftRipHitBox"), "LeftRip");
            Utils.Functions.CreateHitbox(model, childLocator.FindChild("ClawsStormHitbox"), "ClawStorm");
            #endregion
            #region ItemChange
            ItemChange itemChange = characterPrefab.AddComponent<ItemChange>();
            itemChange.ptraObj = pantheraObj;
            #endregion
            #region Register
            // Register the character //
            bodyPrefabs.Add(characterPrefab);
            PrefabAPI.RegisterNetworkPrefab(characterPrefab);
            #endregion
        }


        public static void RegisterSkills()
        {
            // Get the prefab //
            GameObject prefab = Prefab.characterPrefab;

            // Get the skill locator //
            SkillLocator skillLocator = prefab.GetComponent<SkillLocator>();

            Panthera.DestroyImmediate(skillLocator.primary);
            Panthera.DestroyImmediate(skillLocator.secondary);
            Panthera.DestroyImmediate(skillLocator.utility);
            Panthera.DestroyImmediate(skillLocator.special);

            // Add skill family 1 //
            skillLocator.primary = prefab.AddComponent<GenericSkill>();
            skillLocator.primary.skillName = Tokens.RipSkillName;
            SkillFamily primaryFamily = ScriptableObject.CreateInstance<SkillFamily>();
            (primaryFamily as ScriptableObject).name = prefab.name + "PrimaryFamily";
            primaryFamily.variants = new SkillFamily.Variant[0];
            skillLocator.primary._skillFamily = primaryFamily;
            skillFamilies.Add(primaryFamily);

            //// Add skill family 2 //
            skillLocator.secondary = prefab.AddComponent<GenericSkill>();
            skillLocator.secondary.skillName = Tokens.AirCleaveSkillName;
            SkillFamily secondaryFamily = ScriptableObject.CreateInstance<SkillFamily>();
            (secondaryFamily as ScriptableObject).name = prefab.name + "SecondaryFamily";
            secondaryFamily.variants = new SkillFamily.Variant[0];
            skillLocator.secondary._skillFamily = secondaryFamily;
            skillFamilies.Add(secondaryFamily);

            // Add skill family 3 //
            skillLocator.utility = prefab.AddComponent<GenericSkill>();
            skillLocator.utility.skillName = Tokens.LeapSkillName;
            SkillFamily utilityFamily = ScriptableObject.CreateInstance<SkillFamily>();
            (utilityFamily as ScriptableObject).name = prefab.name + "UtilityFamily";
            utilityFamily.variants = new SkillFamily.Variant[0];
            skillLocator.utility._skillFamily = utilityFamily;
            skillFamilies.Add(utilityFamily);

            // Add skill family 4 //
            skillLocator.special = prefab.AddComponent<GenericSkill>();
            skillLocator.special.skillName = Tokens.MightyRoarSkillName;
            SkillFamily specialFamily = ScriptableObject.CreateInstance<SkillFamily>();
            (specialFamily as ScriptableObject).name = prefab.name + "SpecialFamily";
            specialFamily.variants = new SkillFamily.Variant[0];
            skillLocator.special._skillFamily = specialFamily;
            skillFamilies.Add(specialFamily);

            // Create all skills //
            RegisterFakePassive();
            //Skills.PantheraSpawn.register();
            RegisterFakeSkill1(primaryFamily);
            RegisterFakeSkill2(secondaryFamily);
            RegisterFakeSkill3(utilityFamily);
            RegisterFakeSkill4(specialFamily);

        }

        public static void RegisterFakePassive()
        {


            SkillLocator skillLocator = Prefab.characterPrefab.GetComponent<SkillLocator>();

            skillLocator.passiveSkill.enabled = true;
            skillLocator.passiveSkill.skillNameToken = "PANTHERA_PASSIVE_NAME";
            skillLocator.passiveSkill.skillDescriptionToken = "PANTHERA_PASSIVE_DESCRIPTION";
            skillLocator.passiveSkill.icon = Assets.PassiveSkill;
            skillLocator.passiveSkill.keywordToken = "KEYWORD_PTRPASSIVE";

            // Register the Skill inside the Content Pack //
            //Prefab.entityStates.Add(typeof(BigCatPassive));

        }

        public static void RegisterFakeSkill1(SkillFamily family)
        {

            // Build the Skill //
            SkillDef skill1Def = ScriptableObject.CreateInstance<SkillDef>();
            skill1Def.skillNameToken = "PANTHERA_RIP_NAME";
            skill1Def.skillDescriptionToken = "PANTHERA_RIP_DESCRIPTION";
            skill1Def.skillName = "PANTHERA_RIP_NAME";
            skill1Def.keywordTokens = new string[] { "KEYWORD_COMBO", "KEYWORD_STUNNINGPRESENCE", "KEYWORD_POWEREDCLAWS" };
            skill1Def.icon = Assets.RipMenu;
            skill1Def.baseMaxStock = 0;
            skill1Def.baseRechargeInterval = 1.5f;
            skill1Def.beginSkillCooldownOnSkillEnd = false;
            skill1Def.fullRestockOnAssign = false;
            skill1Def.rechargeStock = 0;
            skill1Def.requiredStock = 0;
            skill1Def.stockToConsume = 0;
            //skill1Def.activationState = new SerializableEntityStateType(typeof(Rip));
            //skill1Def.interruptPriority = InterruptPriority.Any;
            //skill1Def.isCombatSkill = true;
            //skill1Def.mustKeyPress = false;
            //skill1Def.cancelSprintingOnActivation = false;
            //skill1Def.canceledFromSprinting = false;
            //skill1Def.activationStateMachineName = "Weapon";

            // Save the skill //
            Array.Resize(ref family.variants, 1);
            family.variants[0] = new SkillFamily.Variant
            {
                skillDef = skill1Def,
                viewableNode = new ViewablesCatalog.Node(skill1Def.skillNameToken, false, null)
            };

            // Register the Skill inside the Content Pack //
            //Prefab.skillDefs.Add(skill1Def);
            //Prefab.entityStates.Add(typeof(Rip));
        }

        public static void RegisterFakeSkill2(SkillFamily family)
        {

            // Build the Skill //
            SkillDef skill2Def = ScriptableObject.CreateInstance<SkillDef>();
            skill2Def.skillNameToken = "PANTHERA_AIRCLEAVE_NAME";
            skill2Def.skillDescriptionToken = "PANTHERA_AIRCLEAVE_DESCRIPTION";
            skill2Def.skillName = "PANTHERA_AIRCLEAVE_NAME";
            skill2Def.keywordTokens = new string[] { "KEYWORD_COMBO", "KEYWORD_STUNNINGPRESENCE", "KEYWORD_POWEREDCLAWS" };
            skill2Def.icon = Assets.AirCleaveMenu;
            skill2Def.baseMaxStock = 1;
            skill2Def.baseRechargeInterval = 1.5f;
            skill2Def.beginSkillCooldownOnSkillEnd = false;
            skill2Def.fullRestockOnAssign = false;
            skill2Def.rechargeStock = 0;
            skill2Def.requiredStock = 0;
            skill2Def.stockToConsume = 0;
            //skill2Def.activationState = new SerializableEntityStateType(typeof(Rip));
            //skill2Def.interruptPriority = InterruptPriority.Any;
            //skill2Def.isCombatSkill = true;
            //skill2Def.mustKeyPress = false;
            //skill2Def.cancelSprintingOnActivation = false;
            //skill2Def.canceledFromSprinting = false;
            //skill2Def.activationStateMachineName = "Weapon";

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
            skill3Def.skillNameToken = "PANTHERA_LEAP_NAME";
            skill3Def.skillDescriptionToken = "PANTHERA_LEAP_DESCRIPTION";
            skill3Def.skillName = "PANTHERA_LEAP_NAME";
            skill3Def.keywordTokens = new string[] { "KEYWORD_FASTREFLEX", "KEYWORD_LEAPCERCLE", "KEYWORD_FURIOUSBITE", "KEYWORD_MANGLE" };
            skill3Def.icon = Assets.LeapMenu;
            skill3Def.baseMaxStock = 2;
            skill3Def.baseRechargeInterval = 5f;
            skill3Def.beginSkillCooldownOnSkillEnd = false;
            skill3Def.fullRestockOnAssign = false;
            skill3Def.rechargeStock = 1;
            skill3Def.requiredStock = 1;
            skill3Def.stockToConsume = 1;
            //skill3Def.activationState = new SerializableEntityStateType(typeof(Leap));
            //skill3Def.activationStateMachineName = "Weapon";
            //skill3Def.canceledFromSprinting = false;
            //skill3Def.interruptPriority = InterruptPriority.Skill;
            //skill3Def.isCombatSkill = true;
            //skill3Def.mustKeyPress = false;
            //skill3Def.cancelSprintingOnActivation = false;



            // Save the skill //
            Array.Resize(ref family.variants, 1);
            family.variants[0] = new SkillFamily.Variant
            {
                skillDef = skill3Def,
                viewableNode = new ViewablesCatalog.Node(skill3Def.skillNameToken, false, null)
            };

            // Register the Skill inside the Content Pack //
            //Prefab.skillDefs.Add(skill3Def);
            //Prefab.entityStates.Add(typeof(Leap));

        }

        public static void RegisterFakeSkill4(SkillFamily family)
        {

            // Build the Skill //
            SkillDef skill4Def = ScriptableObject.CreateInstance<SkillDef>();
            skill4Def.skillNameToken = "PANTHERA_MIGHTY_ROAR_NAME";
            skill4Def.skillDescriptionToken = "PANTHERA_MIGHTY_ROAR_DESCRIPTION";
            skill4Def.skillName = "PANTHERA_MIGHTY_ROAR_NAME";
            skill4Def.keywordTokens = new string[] { "KEYWORD_COMBO", "KEYWORD_STUNNINGPRESENCE", "KEYWORD_POWEREDCLAWS" };
            skill4Def.icon = Assets.MightyRoarMenu;
            skill4Def.baseMaxStock = 0;
            skill4Def.baseRechargeInterval = 1.5f;
            skill4Def.beginSkillCooldownOnSkillEnd = false;
            skill4Def.fullRestockOnAssign = false;
            skill4Def.rechargeStock = 0;
            skill4Def.requiredStock = 0;
            skill4Def.stockToConsume = 0;
            //skill4Def.activationState = new SerializableEntityStateType(typeof(Rip));
            //skill4Def.interruptPriority = InterruptPriority.Any;
            //skill4Def.isCombatSkill = true;
            //skill4Def.mustKeyPress = false;
            //skill4Def.cancelSprintingOnActivation = false;
            //skill4Def.canceledFromSprinting = false;
            //skill4Def.activationStateMachineName = "Weapon";

            // Save the skill //
            Array.Resize(ref family.variants, 1);
            family.variants[0] = new SkillFamily.Variant
            {
                skillDef = skill4Def,
                viewableNode = new ViewablesCatalog.Node(skill4Def.skillNameToken, false, null)
            };

            // Register the Skill inside the Content Pack //
            //Prefab.skillDefs.Add(skill4Def);
            //Prefab.entityStates.Add(typeof(MightyRoar));
        }

    }
}
