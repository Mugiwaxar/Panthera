using EntityStates;
using KinematicCharacterController;
using LeTai.Asset.TranslucentImage;
using Panthera;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Combos;
using Panthera.Components;
using Panthera.GUI;
using Panthera.Machines;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.OldSkills;
using Panthera.Passives;
using Panthera.Skills.Actives;
using Panthera.Skills.Passives;
using R2API;
using R2API.Networking;
using R2API.Networking.Interfaces;
using Rewired;
using RoR2;
using RoR2.Audio;
using RoR2.PostProcess;
using RoR2.PostProcessing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThreeEyedGames;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.TextCore;
using static RoR2.CameraTargetParams;
using static UnityEngine.ParticleSystem;

namespace Panthera.BodyComponents
{
    public class PantheraObj : NetworkBehaviour
    {

        public Player InputPlayer
        {
            get
            {
                return Panthera.InputPlayer;
            }
        }
        public CharacterAbilities characterAbilities
        {
            get
            {
                return Panthera.PantheraCharacter.characterAbilities;
            }
        }
        public CharacterSkills characterSkills
        {
            get
            {
                return Panthera.PantheraCharacter.characterSkills;
            }
        }
        public CharacterCombos characterCombos
        {
            get
            {
                return Panthera.PantheraCharacter.characterCombos;
            }
        }

        public Dictionary<int, bool> activatedComboList = new Dictionary<int, bool>();
        public NetworkIdentity networkID;
        public PantheraBody characterBody;
        public PantheraSkillLocator skillLocator;
        public ModelLocator modelLocator;
        public CharacterModel characterModel;
        public Transform modelTransform;
        public Animator modelAnimator;
        public ChildLocator childLocator;
        public PantheraMotor pantheraMotor;
        public PantheraKinematicMotor kinematicPantheraMotor;
        public CharacterDirection direction;
        public PantheraHealthComponent healthComponent;
        public PantheraInputBank pantheraInputBank;
        public PantheraFX pantheraFX;
        public PantheraDeathBehavior deathBehavior;
        public CameraTargetParams pantheraCamParam;
        public CameraRigController cameraRigController;
        public PantheraMaster pantheraMaster
        {
            get
            {
                PantheraMaster master = this.characterBody?.master?.GetComponent<PantheraMaster>();
                if (master == null && this.characterBody != null && this.characterBody.master != null)
                    master = this.characterBody.master.gameObject.AddComponent<PantheraMaster>();
                return master;
            }
        }
        public PantheraComboComponent comboComponent;
        public ProfileComponent profileComponent;
        public CrosshairComp crosshairComp;

        public PantheraMachine mainMachine;
        public PantheraMachine passiveMachine;
        public PantheraMachine skillsMachine1;
        public PantheraMachine skillsMachine2;
        public PantheraMachine networkMachine;

        public int PantheraSkinIndex
        {
            get
            {
                if (characterBody != null)
                    return (int)characterBody.skinIndex + 1;
                return 1;
            }
        }
        public float actualModelScale = PantheraConfig.Model_defaultModelScale;
        public float desiredModelScale = PantheraConfig.Model_defaultModelScale;

        public CameraParamsOverrideHandle lastCamHandle;
        public Camera pantheraCam;
        public Vector3 defaultCamPos = PantheraConfig.defaultCamPosition;

        public GameObject origPostProcess;
        public GameObject pantheraPostProcess;
        public int origLayerIndex;

        public bool stealthed = false;
        public bool furyMode = false;
        public bool detectionMode = false;
        public bool guardianMode = false;
        public bool ambitionMode = false;
        public float ambitionTimer = 0;
        public bool clawsStormActivated = false;
        public bool portalSurgeActivated = false;
        public float furyDecreaseTime = PantheraConfig.Fury_furyPointsDecreaseTime;

        public GameObject frontShieldObj;
        public GameObject FrostedAirObj;
        public bool frontShieldDeployed = false;
        public float frontShieldScale = PantheraConfig.FrontShield_defaultScale;

        public List<ConvergenceHookComp> convergenceCompsList = new List<ConvergenceHookComp>();

        public bool interactPressed;
        public bool jumpPressed;
        public bool sprintPressed;

        public Coroutine _unstealthCoroutine;
        public Coroutine unstealthCoroutine
        {
            get
            {
                return _unstealthCoroutine;
            }
            set
            {
                if (_unstealthCoroutine != null)
                {
                    StopCoroutine(_unstealthCoroutine);
                    _unstealthCoroutine = null;
                }
                _unstealthCoroutine = value;
            }
        }
        public bool dashing = false;
        public int _attackNumber = 1;
        public int attackNumber
        {
            get
            {
                if (this._attackNumber == 1)
                {
                    this._attackNumber = 2;
                    return 1;
                }
                else
                {
                    this._attackNumber = 1;
                    return 2;
                }
            }
        }

        public void doDamageSelf(float damage)
        {
            new ServerInflictDamage(base.gameObject, base.gameObject, base.transform.position, damage).Send(NetworkDestination.Server);
        }

        public void Awake()
        {

            // Get values //
            this.networkID = base.gameObject.GetComponent<NetworkIdentity>();
            this.characterBody = base.gameObject.GetComponent<PantheraBody>();
            this.skillLocator = base.gameObject.GetComponent<PantheraSkillLocator>();
            this.modelLocator = base.gameObject.GetComponent<ModelLocator>();
            this.modelTransform = this.modelLocator.modelTransform;
            this.characterModel = this.modelTransform.GetComponent<CharacterModel>();
            this.modelAnimator = this.modelLocator.modelTransform.GetComponent<Animator>();
            this.childLocator = this.modelLocator.modelTransform.GetComponent<ChildLocator>();
            this.pantheraMotor = base.gameObject.GetComponent<PantheraMotor>();
            this.kinematicPantheraMotor = base.gameObject.GetComponent<PantheraKinematicMotor>();
            this.direction = base.gameObject.GetComponent<CharacterDirection>();
            this.healthComponent = base.gameObject.GetComponent<PantheraHealthComponent>();
            this.pantheraInputBank = base.gameObject.GetComponent<PantheraInputBank>();
            this.mainMachine = base.gameObject.GetComponent<PantheraMainMachine>();
            this.passiveMachine = base.gameObject.GetComponent<PantheraPassiveMachine>();
            this.skillsMachine1 = base.gameObject.GetComponent<PantheraSkillsMachine>();
            this.skillsMachine2 = base.gameObject.GetComponents<PantheraSkillsMachine>()[1];
            this.networkMachine = base.gameObject.GetComponent<PantheraNetworkMachine>();
            this.pantheraFX = base.gameObject.GetComponent<PantheraFX>();
            this.comboComponent = base.gameObject.GetComponent<PantheraComboComponent>();
            this.deathBehavior = base.gameObject.GetComponent<PantheraDeathBehavior>();
            this.pantheraCamParam = base.gameObject.GetComponent<CameraTargetParams>();

            // Visual debug component //
            //this.gameObject.AddComponent<CapsuleColliderVisualizerWithHeight>();

            // Set the Main Renderer //
            //characterModel.mainSkinnedMeshRenderer = this.modelTransform.GetComponentInChildren<SkinnedMeshRenderer>();

            // Get the Original Layer Index //
            this.origLayerIndex = this.modelTransform.GetComponentInChildren<SkinnedMeshRenderer>().gameObject.layer;

            // Init Body //
            this.characterBody.DoInit();

            // Init motors //
            this.pantheraMotor.DoInit();
            this.kinematicPantheraMotor.DoInit();

            // Init Heal Component //
            this.healthComponent.DoInit();

            // Init input bank //
            this.pantheraInputBank.DoInit();

            // Init FX //
            this.pantheraFX.DoInit();

            // Create the Front Shield //
            this.frontShieldObj = GameObject.Instantiate<GameObject>(PantheraAssets.FrontShieldObj);
            this.frontShieldObj.SetActive(true);
            this.frontShieldObj.GetComponent<FrontShieldComponent>().ptraObj = this;
            this.frontShieldObj.GetComponent<FrontShieldHealthComponent>().ptraObj = this;
            this.frontShieldObj.GetComponent<TeamComponent>().teamIndex = TeamIndex.Player;
            this.frontShieldObj.GetComponent<HurtBox>().teamIndex = TeamIndex.Player;
            this.frontShieldObj.GetComponent<CharacterBody>().doNotReassignToTeamBasedCollisionLayer = true;
            this.frontShieldObj.layer = LayerIndex.entityPrecise.intVal;
            this.frontShieldObj.transform.FindChild(PantheraConfig.FrontShield_worldHitboxName).gameObject.layer = LayerIndex.world.intVal;

            // Create the Frozen Air //
            this.FrostedAirObj = GameObject.Instantiate<GameObject>(PantheraAssets.FrostedAirObj);
            this.FrostedAirObj.SetActive(true);
            this.FrostedAirObj.GetComponent<FrostedAirComponent>().ptraObj = this;

            // Start all Events //
            CharacterBody.onBodyStartGlobal += onEntitySpawned;

            // Get all spawned Entities //
            foreach (CharacterMaster master in CharacterMaster.readOnlyInstancesList)
            {
                if (master.GetBody() != null)
                    this.onEntitySpawned(master.GetBody());
            }

            // Set the RNG //
            Panthera.ptraRNG = new Xoroshiro128Plus(Run.instance.seed);

        }

        public void Start()
        {

            // Start the Network Machine //
            this.networkMachine.enabled = true;

        }

        public override void OnStartAuthority()
        {

            // Call the upper Fonction //
            base.OnStartAuthority();

            // Set the Character Panthera Object //
            Panthera.PantheraCharacter.pantheraObj = this;

            // Start the HUD //
            Panthera.PantheraHUD.StartHUD(this);

            // Create the Activated Combots List //
            foreach (KeyValuePair<int, PantheraCombo> pair in characterCombos.CombosList)
            {
                this.activatedComboList.Add(pair.Key, pair.Value.activated);
            }

            // Set Panthera Object to the Active Preset //
            //if (this.activePreset != null)
            //    this.activePreset.ptraObj = this;

            // Send Preset to the Server //
            //if (NetworkServer.active == false) new ServerSyncPreset(gameObject, this.activePreset.unlockedAbilitiesList).Send(NetworkDestination.Server);

            // Synchronize the Profile //
            this.profileComponent = base.gameObject.AddComponent<ProfileComponent>();
            this.profileComponent.syncProfile();

            // Set Default Camera Parameters //
            Vector3 camPos = PantheraConfig.defaultCamPosition;
            this.defaultCamPos = new Vector3(camPos.x, camPos.y, camPos.z * actualModelScale);
            Utils.CamHelper.ApplyCameraType(Utils.CamHelper.AimType.Standard, this);

            // Set Camera Fade distance //
            this.cameraRigController = this.characterBody.master?.playerCharacterMasterController?.networkUser.cameraRigController;
            if (cameraRigController != null)
            {
                cameraRigController.fadeStartDistance = PantheraConfig.Model_fadeStartDistance;
                cameraRigController.fadeEndDistance = PantheraConfig.Model_fadeEndDistance;
            }

            // Start the machines //
            this.mainMachine.enabled = true;
            this.passiveMachine.enabled = true;
            this.skillsMachine1.enabled = true;
            this.skillsMachine2.enabled = true;

            // Start the input bank //
            this.pantheraInputBank.enabled = true;

            // Get the minimum Fury //
            float minFury = 0;
            int eternalFuryLvl = this.getAbilityLevel(PantheraConfig.EternalFury_AbilityID);
            if (eternalFuryLvl == 1)
                minFury = this.characterBody.maxFury * PantheraConfig.EternalFury_startPercent1;
            else if (eternalFuryLvl == 2)
                minFury = this.characterBody.maxFury * PantheraConfig.EternalFury_startPercent2;
            else if (eternalFuryLvl == 3)
                minFury = this.characterBody.maxFury * PantheraConfig.EternalFury_startPercent3;

            // Set Fury back //
            this.characterBody.fury = Math.Max(this.pantheraMaster.savedFury, minFury);

            // Set Cooldown Back //
            this.skillLocator.rechargeSkillList = this.pantheraMaster.savedCooldownList;

            // Create the Recharge Stock List //
            if (this.skillLocator.rechargeSkillList == null)
                this.skillLocator.createRechargeSkillsList();

            // Apply all Stats //
            this.characterBody.RecalculateStats();

            // Create the Panthera Camera //
            GameObject camObj = GameObject.Instantiate(Camera.main.gameObject);
            camObj.SetActive(false);
            camObj.transform.parent = Camera.main.transform.parent;
            camObj.name = "PantheraCam";
            this.pantheraCam = camObj.GetComponent<Camera>();
            this.pantheraCam.cullingMask = 1 << 31;
            this.pantheraCam.clearFlags = CameraClearFlags.Nothing;
            GameObject.DestroyImmediate(this.pantheraCam.transform.Find("GlobalPostProcessVolume").gameObject);
            GameObject.DestroyImmediate(this.pantheraCam.transform.Find("SprintParticles").gameObject);
            GameObject.DestroyImmediate(this.pantheraCam.GetComponent<DecaliciousRenderer>());
            GameObject.DestroyImmediate(this.pantheraCam.GetComponent<PostProcessLayer>());
            GameObject.DestroyImmediate(this.pantheraCam.GetComponent<FlareLayer>());
            GameObject.DestroyImmediate(this.pantheraCam.GetComponent<AkAudioListener>());
            GameObject.DestroyImmediate(this.pantheraCam.GetComponent<BlurOptimized>());
            GameObject.DestroyImmediate(this.pantheraCam.GetComponent<Rigidbody>());
            GameObject.DestroyImmediate(this.pantheraCam.GetComponent<AkGameObj>());
            GameObject.DestroyImmediate(this.pantheraCam.GetComponent<OutlineHighlight>());
            GameObject.DestroyImmediate(this.pantheraCam.GetComponent<SceneCamera>());
            GameObject.DestroyImmediate(this.pantheraCam.GetComponent<SobelCommandBuffer>());
            //GameObject.DestroyImmediate(this.pantheraCam.GetComponent<CameraResolutionScaler>());
            GameObject.DestroyImmediate(this.pantheraCam.GetComponent<TranslucentImageSource>());
            GameObject.DestroyImmediate(this.pantheraCam.GetComponent<VisionLimitEffect>());

            // Instantiate the Post Process //
            this.pantheraPostProcess = GameObject.Instantiate(Base.PantheraAssets.PantheraPostProcess);
            this.pantheraPostProcess.SetActive(false);
            this.pantheraPostProcess.transform.SetParent(Camera.main.transform);
            this.pantheraPostProcess.layer = LayerIndex.postProcess.intVal;

            // Get the Original Post Process //
            this.origPostProcess = Camera.main.transform.Find("GlobalPostProcessVolume").gameObject;

        }

        public void Update()
        {
            if (this.hasAuthority())
            {
                // Enable or disable the Front Shield //
                if (this.getSkillMachine1SciptType() == typeof(Skills.Actives.FrontShield) || this.getSkillMachine1SciptType() == typeof(Skills.Actives.ShieldBash) || this.frontShieldDeployed == true)
                {
                    if (this.frontShieldObj.active == false)
                        Skills.Passives.FrontShield.EnableFrontShield(this);
                }
                else
                {
                    if (this.frontShieldObj.active == true)
                        Skills.Passives.FrontShield.DisableFrontShield(this);
                }
                // Disable the Shield Deployement if to far away //
                if (this.frontShieldObj.active == true)
                {
                    if (Vector3.Distance(this.transform.position, this.frontShieldObj.transform.position) > PantheraConfig.ArcaneAnchor_MaxDistance)
                    {
                        Skills.Passives.FrontShield.DeployShield(this, false);
                    }
                }
            }

        }

        public void FixedUpdate()
        {
            //// Set the Model //
            //if (this.ActualPantheraSkinIndex != this.PantheraSkinIndex)
            //{
            //    if (this.PantheraSkinIndex == 1)
            //    {
            //        Skin.ChangedToMainModel1(this.childLocator);
            //        this.ActualPantheraSkinIndex = this.PantheraSkinIndex;
            //    }
            //    else if (PantheraSkinIndex == 2)
            //    {
            //        Skin.ChangedToMainModel2(this.childLocator);
            //        this.ActualPantheraSkinIndex = this.PantheraSkinIndex;
            //    }
            //    else if (PantheraSkinIndex == 3)
            //    {
            //        Skin.ChangedToMainModel3(this.childLocator);
            //        this.ActualPantheraSkinIndex = this.PantheraSkinIndex;
            //    }
            //    else if (PantheraSkinIndex == 4)
            //    {
            //        Skin.ChangedToMainModel4(this.childLocator);
            //        this.ActualPantheraSkinIndex = this.PantheraSkinIndex;
            //    }
            //}

            // Update the Panthera Camera //
            if (this.pantheraCam != null && this.pantheraCam.gameObject.active == true)
            {
                this.pantheraCam.fieldOfView = Camera.main.fieldOfView;
                this.pantheraCam.rect = Camera.main.rect;
                this.pantheraCam.transform.position = Camera.main.transform.position;
                this.pantheraCam.transform.localPosition = Camera.main.transform.localPosition;
                this.pantheraCam.transform.rotation = Camera.main.transform.rotation;
                this.pantheraCam.transform.localRotation = Camera.main.transform.localRotation;
                this.pantheraCam.transform.localScale = Camera.main.transform.localScale;
            }

            // Check the Model Scale //
            if (this.hasAuthority() == true && this.actualModelScale != this.desiredModelScale)
            {
                if (this.actualModelScale > this.desiredModelScale)
                    this.actualModelScale -= 0.01f;
                else if (this.actualModelScale < this.desiredModelScale)
                    this.actualModelScale += 0.01f;
                if (Math.Abs(this.actualModelScale - this.desiredModelScale) < 0.02f)
                {
                    this.actualModelScale = this.desiredModelScale;
                }
                this.transform.localScale = new Vector3(this.actualModelScale, this.actualModelScale, this.actualModelScale);
                this.modelTransform.localScale = new Vector3(this.actualModelScale, this.actualModelScale, this.actualModelScale);
                new ServerChangePantheraScale(base.gameObject, this.actualModelScale).Send(NetworkDestination.Server);
                Vector3 camPos = PantheraConfig.defaultCamPosition;
                this.defaultCamPos = new Vector3(camPos.x, camPos.y, camPos.z * actualModelScale);
                Utils.CamHelper.ApplyCameraType(Utils.CamHelper.AimType.Standard, this, 2);
                this.kinematicPantheraMotor.SetCapsuleDimensions(PantheraConfig.Model_defaultCapsuleRadius * this.actualModelScale, PantheraConfig.Model_defaultCapsuleHeight * this.actualModelScale, 0);
            }

            // Update the Front Shield Scale //
            float scale = this.frontShieldScale;
            if (this.frontShieldDeployed == true && scale < PantheraConfig.ArcaneAnchor_deployedScale)
                scale += PantheraConfig.ArcaneAnchor_deployedScaleSpeed;
            else if (this.frontShieldDeployed == false)
                scale = PantheraConfig.FrontShield_defaultScale * this.actualModelScale;
            if (scale != this.frontShieldScale)
            {
                this.frontShieldScale = scale;
                this.frontShieldObj.transform.localScale = new Vector3(scale, scale, scale);
            }

        }

        public void OnDestroy()
        {
            if (this.hasAuthority() == true)
            {
                // Check the Master //
                if (this.pantheraMaster != null)
                {
                    // Save the Fury //
                    this.pantheraMaster.savedFury = this.characterBody.fury;
                    // Save the Cooldown //
                    this.pantheraMaster.savedCooldownList = this.skillLocator.rechargeSkillList;
                }
                // Set all Skills Icones Back //
                this.skillLocator.primary.skillDef.icon = PantheraAssets.RipSkillMenu;
                this.skillLocator.secondary.skillDef.icon = PantheraAssets.SlashSkillMenu;
            }
            // Destroy the Front Shield //
            GameObject.Destroy(this.frontShieldObj);
            // Stop all Events //
            CharacterBody.onBodyStartGlobal -= onEntitySpawned;
        }

        public bool hasAuthority()
        {
            if (this.networkID.hasAuthority == true) return true;
            if (NetworkServer.active == true && networkID.clientAuthorityOwner == null) return true;
            return false;
        }

        public void onEntitySpawned(CharacterBody body)
        {
            // Check if Monster //
            if (body.teamComponent != null && body.teamComponent.teamIndex != TeamIndex.Monster) return;
            // Add the Predator Component //
            if (body.gameObject.GetComponent<PredatorComponent>() == null)
                body.gameObject.AddComponent<PredatorComponent>();
        }

        public int getAbilityLevel(int abilityID)
        {
            return this.profileComponent.getAbilityLevel(abilityID);
        }

        public bool isMastery(int abilityID)
        {
            return this.profileComponent.isMastery(abilityID);
        }

        public bool isSkillUnlocked(int skillId)
        {
            return this.profileComponent.isSkillUnlocked(skillId);
        }

        public bool isComboUnlocked(int comboID)
        {
            return this.profileComponent.isComboUnlocked(comboID);
        }

        public Transform findModelChild(string childName)
        {
            return childLocator.FindChild(childName);
        }

        public MainScript getMainScript()
        {
            return (MainScript)GetComponent<PantheraMainMachine>().GetCurrentScript();
        }

        public BigCatPassive getPassiveScript()
        {
            return (BigCatPassive)GetComponent<PantheraPassiveMachine>().GetCurrentScript();
        }

        public MachineScript getSkillMachine1Scipt()
        {
            if (skillsMachine1.GetCurrentScript() == null) return null;
            return skillsMachine1.GetCurrentScript();
        }

        public MachineScript getSkillMachine2Scipt()
        {
            if (skillsMachine2.GetCurrentScript() == null) return null;
            return skillsMachine2.GetCurrentScript();
        }

        public Type getSkillMachine1SciptType()
        {
            if (skillsMachine1.GetCurrentScript() == null) return null;
            return skillsMachine1.GetCurrentScript().GetType();
        }

        public Type getSkillMachine2SciptType()
        {
            if (skillsMachine2.GetCurrentScript() == null) return null;
            return skillsMachine2.GetCurrentScript().GetType();
        }

        public void stopAllScripts()
        {
            this.mainMachine.EndScript();
            this.mainMachine.nextScript = null;
            this.passiveMachine.EndScript();
            this.passiveMachine.nextScript = null;
            this.skillsMachine1.EndScript();
            this.skillsMachine1.nextScript = null;
            this.skillsMachine2.EndScript();
            this.skillsMachine2.nextScript = null;
            this.networkMachine.EndScript();
            this.networkMachine.nextScript = null;
        }

        public static void readDefs()
        {

            PantheraConfig.CloakBuffDef = RoR2Content.Buffs.Cloak;
            PantheraConfig.WeakDebuffDef = RoR2Content.Buffs.Weak;
            PantheraConfig.RegenBuffDef = RoR2Content.Buffs.CrocoRegen;
            PantheraConfig.InvincibilityBuffDef = RoR2Content.Buffs.Immune;
            PantheraConfig.HiddenInvincibilityBuffDef = RoR2Content.Buffs.HiddenInvincibility;

            PantheraConfig.BleedDotIndex = DotController.DotIndex.Bleed;
            PantheraConfig.SuperBleedDotIndex = DotController.DotIndex.SuperBleed;
            PantheraConfig.BurnDotIndex = DotController.DotIndex.Burn;
            PantheraConfig.SuperBurnDotIndex = DotController.DotIndex.StrongerBurn;


            PantheraConfig.ItemChange_steak = RoR2Content.Items.FlatHealth;
            PantheraConfig.ItemChange_magazineIndex = RoR2Content.Items.SecondarySkillMagazine;
            PantheraConfig.ItemChange_alienHeadIndex = RoR2Content.Items.AlienHead;
            PantheraConfig.ItemChange_bandolierIndex = RoR2Content.Items.Bandolier;
            PantheraConfig.ItemChange_shurikenIndex = DLC1Content.Items.PrimarySkillShuriken;
            PantheraConfig.ItemChange_squidIndex = RoR2Content.Items.Squid;
            PantheraConfig.ItemChange_brainstalksIndex = RoR2Content.Items.KillEliteFrenzy;
            PantheraConfig.ItemChange_hardlightAfterburnerIndex = RoR2Content.Items.UtilitySkillMagazine;
            PantheraConfig.ItemChange_heresyEssenceIndex = RoR2Content.Items.LunarSpecialReplacement;
            PantheraConfig.ItemChange_heresyHooksIndex = RoR2Content.Items.LunarSecondaryReplacement;
            PantheraConfig.ItemChange_heresyStridesIndex = RoR2Content.Items.LunarUtilityReplacement;
            PantheraConfig.ItemChange_heresyVisionsIndex = RoR2Content.Items.LunarPrimaryReplacement;
            PantheraConfig.ItemChange_brittleCrownIndex = RoR2Content.Items.GoldOnHit;
            PantheraConfig.ItemChange_lightFluxPauldronIndex = DLC1Content.Items.HalfAttackSpeedHalfCooldowns;
            PantheraConfig.ItemChange_purityIndex = RoR2Content.Items.LunarBadLuck;
            PantheraConfig.ItemChange_lysateCellIndex = DLC1Content.Items.EquipmentMagazineVoid;
            PantheraConfig.ItemChange_transcendanceIndex = RoR2Content.Items.ShieldOnly;

        }

    }


    public class CapsuleColliderVisualizerWithHeight : MonoBehaviour
    {

        CharacterBody body;
        Transform model;

        public Color hitboxColor = new Color(1f, 0f, 0f, 0.3f); // Couleur des hitbox (semi-transparente)

        private Collider[] hitboxes = new Collider[1];
        private GameObject[] visualizers;

        private GameObject visualHeightPlane;
        private GameObject visualHeightPlaneFoot;

        void Start()
        {
            this.body = transform.GetComponent<CharacterBody>();
            this.model = transform.GetComponent<PantheraObj>().modelTransform;
            CapsuleCollider capsule = GetComponent<CapsuleCollider>();

            // Récupérer tous les colliders des enfants
            //hitboxes = model.GetComponentsInChildren<Collider>();
            hitboxes[0] = model.GetComponent<HitBoxGroup>().hitBoxes[0].gameObject.GetComponent<Collider>();
            visualizers = new GameObject[hitboxes.Length];
            CreateVisualizers();


            //// Crée un plan pour visualiser la hauteur
            //visualHeightPlane = GameObject.CreatePrimitive(PrimitiveType.Quad);
            //Destroy(visualHeightPlane.GetComponent<Collider>()); // Supprime le collider du plan
            //visualHeightPlane.transform.SetParent(transform);
            //visualHeightPlane.transform.localScale = new Vector3(
            //    7,
            //    1, // Le plan est toujours plat sur l'axe Z
            //    1
            //);

            //visualHeightPlaneFoot = GameObject.CreatePrimitive(PrimitiveType.Quad);
            //Destroy(visualHeightPlaneFoot.GetComponent<Collider>()); // Supprime le collider du plan
            //visualHeightPlaneFoot.transform.SetParent(transform);
            //visualHeightPlaneFoot.transform.localScale = new Vector3(
            //    7,
            //    1, // Le plan est toujours plat sur l'axe Z
            //    1
            //);

            //UpdateVisualHeightPlane();

        }

        private void CreateVisualizers()
        {
            for (int i = 0; i < hitboxes.Length; i++)
            {
                Collider hitbox = hitboxes[i];
                GameObject visualizer = null;

                // Créer une primitive correspondant au type de collider
                if (hitbox is BoxCollider)
                    visualizer = GameObject.CreatePrimitive(PrimitiveType.Cube);
                else if (hitbox is SphereCollider)
                    visualizer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                else if (hitbox is CapsuleCollider)
                    visualizer = GameObject.CreatePrimitive(PrimitiveType.Capsule);

                if (visualizer != null)
                {
                    //visualizer.transform.SetParent(hitbox.transform, false);
                    visualizer.GetComponent<Renderer>().material.color = hitboxColor;
                    visualizer.GetComponent<Collider>().enabled = false; // Désactiver la collision sur les visualisateurs
                    visualizers[i] = visualizer;
                }
            }
        }

        void Update()
        {
            UpdateVisualizers();
            //UpdateVisualHeightPlane();
        }

        private void UpdateVisualizers()
        {
            for (int i = 0; i < hitboxes.Length; i++)
            {
                Collider hitbox = hitboxes[i];
                GameObject visualizer = visualizers[i];

                if (hitbox is BoxCollider box)
                {
                    visualizer.transform.position = box.transform.position;
                    visualizer.transform.localScale = box.size * box.transform.lossyScale.y;
                    visualizer.transform.rotation = box.transform.rotation;
                }
                else if (hitbox is SphereCollider sphere)
                {
                    visualizer.transform.localPosition = sphere.center;
                    visualizer.transform.localScale = Vector3.one * sphere.radius * 2f;
                }
                else if (hitbox is CapsuleCollider capsule)
                {
                    visualizer.transform.localPosition = capsule.center;
                    visualizer.transform.localScale = new Vector3(capsule.radius * 2f, capsule.height / 2f, capsule.radius * 2f);
                }
            }
        }

        private void UpdateVisualHeightPlane()
        {

            // Ajuste la position du plan à la hauteur du CapsuleCollider
//            visualHeightPlane.transform.position = new Vector3(
//                transform.position.x,
//                transform.position.y,
//                transform.position.z
//            );
//            visualHeightPlane.transform.rotation = model.rotation * Quaternion.Euler(90, 0, 0);

//            visualHeightPlaneFoot.transform.position = new Vector3(
//            body.footPosition.x,
//            body.footPosition.y,
//            body.footPosition.z
//);
//            visualHeightPlaneFoot.transform.rotation = model.rotation * Quaternion.Euler(90, 0, 0);
        }


        void OnDestroy()
        {

            foreach (var visualizer in visualizers)
            {
                if (visualizer != null)
                    Destroy(visualizer);
            }

            if (visualHeightPlane != null) Destroy(visualHeightPlane);

        }
    }

}
