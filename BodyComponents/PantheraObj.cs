using EntityStates;
using KinematicCharacterController;
using LeTai.Asset.TranslucentImage;
using Panthera;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.GUI;
using Panthera.Machines;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.Skills;
using Panthera.SkillsHybrid;
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
using System.Text;
using ThreeEyedGames;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering.PostProcessing;
using static RoR2.CameraTargetParams;

namespace Panthera.BodyComponents
{
    public class PantheraObj : NetworkBehaviour
    {

        public GameObject player;
        public Player rewirePlayer;
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
        public Preset activePreset;
        public PantheraDeathBehavior deathBehavior;
        public CameraTargetParams pantheraCamParam;
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
        public int ActualPantheraSkinIndex;
        public float modelScale = PantheraConfig.Model_defaultModelScale;

        public PantheraHUD pantheraHUD;

        public CameraParamsOverrideHandle lastCamHandle;
        public Camera pantheraCam;
        public Vector3 defaultCamPos
        {
            get
            {
                if (this.activePreset != null && this.activePreset.getAbilityLevel(PantheraConfig.GuardianAbilityID) > 0)
                    return PantheraConfig.defaultGuardianCamPosition;
                return PantheraConfig.defaultCamPosition;
            }
        }
        public GameObject origPostProcess;
        public GameObject pantheraPostProcess;
        public int OrigLayerIndex;

        public GameObject frontShield;
        public bool interactPressed;

        public bool stealthed = false;
        public float prowlActivationTime = 0;
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
        public int aircleaveComboNumber = 1;
        public int slashComboNumber = 1;
        public bool detectionActivated = false;

        public void Awake()
        {

            // Get values //
            this.player = gameObject;
            this.networkID = player.GetComponent<NetworkIdentity>();
            this.characterBody = player.GetComponent<PantheraBody>();
            this.skillLocator = player.GetComponent<PantheraSkillLocator>();
            this.modelLocator = player.GetComponent<ModelLocator>();
            this.modelTransform = modelLocator.modelTransform;
            this.characterModel = modelTransform.GetComponent<CharacterModel>();
            this.modelAnimator = modelLocator.modelTransform.GetComponent<Animator>();
            this.childLocator = modelLocator.modelTransform.GetComponent<ChildLocator>();
            this.pantheraMotor = GetComponent<PantheraMotor>();
            this.kinematicPantheraMotor = GetComponent<PantheraKinematicMotor>();
            this.direction = player.GetComponent<CharacterDirection>();
            this.healthComponent = GetComponent<PantheraHealthComponent>();
            this.pantheraInputBank = GetComponent<PantheraInputBank>();
            this.mainMachine = GetComponent<PantheraMainMachine>();
            this.passiveMachine = GetComponent<PantheraPassiveMachine>();
            this.skillsMachine1 = GetComponent<PantheraSkillsMachine>();
            this.skillsMachine2 = GetComponents<PantheraSkillsMachine>()[1];
            this.networkMachine = GetComponent<PantheraNetworkMachine>();
            this.pantheraFX = GetComponent<PantheraFX>();
            this.activePreset = Preset.ActivePreset;
            this.pantheraHUD = PantheraHUD.Instance;
            this.deathBehavior = GetComponent<PantheraDeathBehavior>();
            this.pantheraCamParam = GetComponent<CameraTargetParams>();

            // Set the Main Renderer //
            characterModel.mainSkinnedMeshRenderer = modelTransform.GetComponentInChildren<SkinnedMeshRenderer>();

            // Get the Original Layer Index //
            this.OrigLayerIndex = characterModel.mainSkinnedMeshRenderer.gameObject.layer;

            // Init body //
            this.characterBody.DoInit();

            // Init motors //
            this.pantheraMotor.DoInit();
            this.kinematicPantheraMotor.DoInit();

            // Init Heal Component //
            this.healthComponent.DoInit();

            // Init input bank //
            this.pantheraInputBank.DoInit();

            // Init FX //
            this.pantheraFX.Init();

            // Find the Front Shield //
            this.frontShield = this.childLocator.FindChild("ShieldHurtbox").gameObject;
            this.frontShield.SetActive(false);

        }

        public void Start()
        {

            // Ask for the InputPlayer //
            this.rewirePlayer = this.characterBody.master.playerCharacterMasterController.networkUser.inputPlayer;
            this.pantheraInputBank.rewirePlayer = this.rewirePlayer;

            // Start the Network Machine //
            this.networkMachine.enabled = true;

        }

        public override void OnStartAuthority()
        {

            // Call the upper Fonction //
            base.OnStartAuthority();

            // Set Panthera Object to the Active Preset //
            if (this.activePreset != null)
                this.activePreset.ptraObj = this;

            // Send Preset to the Server //
            if (NetworkServer.active == false) new ServerSyncPreset(gameObject, this.activePreset.unlockedAbilitiesList).Send(NetworkDestination.Server);

            // Set Default Camera Parameters //
            Utils.CamHelper.ApplyAimType(Utils.CamHelper.AimType.Standard, this);

            // Set Camera Fade distance //
            CameraRigController rigCtrl = Camera.main.transform.parent.GetComponent<CameraRigController>();
            if (rigCtrl != null)
            {
                rigCtrl.fadeStartDistance = PantheraConfig.Model_fadeStartDistance;
                rigCtrl.fadeEndDistance = PantheraConfig.Model_fadeEndDistance;
                if (this.activePreset.getAbilityLevel(PantheraConfig.GuardianAbilityID) > 0)
                {
                    rigCtrl.fadeStartDistance = PantheraConfig.Model_fadeStartDistance + 1;
                    rigCtrl.fadeEndDistance = PantheraConfig.Model_fadeEndDistance + 1;
                }
            }

            // Start the machines //
            this.mainMachine.enabled = true;
            this.passiveMachine.enabled = true;
            this.skillsMachine1.enabled = true;
            this.skillsMachine2.enabled = true;

            // Start the input bank //
            this.pantheraInputBank.enabled = true;

            // Set Fury back //
            this.characterBody.fury = this.pantheraMaster.savedFury;

            // Set Power back //
            this.characterBody.power = this.pantheraMaster.savedPower;

            // Set Cooldown Back //
            this.skillLocator.cooldownList = this.pantheraMaster.savedCooldownList;

            // Apply all Stats //
            this.applyStats();

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
            this.pantheraPostProcess = GameObject.Instantiate(Base.Assets.PantheraPostProcess);
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
                if (this.getSkillMachine1SciptType() == typeof(FrontShield) || this.getSkillMachine1SciptType() == typeof(ShieldBash))
                {
                    if (frontShield.active == false)
                        Passives.FrontShield.EnableFrontShield(this);
                }
                else
                {
                    if (this.frontShield.active == true)
                        Passives.FrontShield.DisableFrontShield(this);
                }
            }
        }

        public void FixedUpdate()
        {
            // Set the Model //
            if (this.ActualPantheraSkinIndex != this.PantheraSkinIndex)
            {
                if (this.PantheraSkinIndex == 1)
                {
                    Skin.ChangedToMainModel1(this.childLocator);
                    this.ActualPantheraSkinIndex = this.PantheraSkinIndex;
                }
                else if (PantheraSkinIndex == 2)
                {
                    Skin.ChangedToMainModel2(this.childLocator);
                    this.ActualPantheraSkinIndex = this.PantheraSkinIndex;
                }
                else if (PantheraSkinIndex == 3)
                {
                    Skin.ChangedToMainModel3(this.childLocator);
                    this.ActualPantheraSkinIndex = this.PantheraSkinIndex;
                }
            }
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
        }

        public void OnDestroy()
        {
            if (this.hasAuthority() == true)
            {
                // Save the Fury //
                this.pantheraMaster.savedFury = this.characterBody.fury;
                // Save the Power //
                this.pantheraMaster.savedPower = this.characterBody.power;
                // Save the Cooldown //
                this.pantheraMaster.savedCooldownList = this.skillLocator.cooldownList;
            }
        }

        public void applyStats()
        {
            characterBody.baseMaxHealth = activePreset.maxHealth;
            characterBody.levelMaxHealth = activePreset.maxHealthLevel;
            characterBody.baseRegen = activePreset.healthRegen;
            characterBody.levelRegen = activePreset.healthRegenLevel;
            characterBody.baseMoveSpeed = activePreset.moveSpeed;
            characterBody.levelMoveSpeed = activePreset.moveSpeedLevel;
            characterBody.baseDamage = activePreset.damage;
            characterBody.levelDamage = activePreset.damageLevel;
            characterBody.baseAttackSpeed = activePreset.attackSpeed;
            characterBody.levelAttackSpeed = activePreset.attackSpeedLevel;
            characterBody.baseCrit = activePreset.critic;
            characterBody.levelCrit = activePreset.criticLevel;
            characterBody.baseArmor = activePreset.defense;
            characterBody.levelArmor = activePreset.defenseLevel;
            characterBody.baseJumpCount = (int)activePreset.jumpCount;

            characterBody.RecalculateStats();

            healthComponent.health = characterBody.maxHealth;
            characterBody.shield = activePreset.maxShield;
        }

        public bool hasAuthority()
        {
            if (this.networkID.hasAuthority == true) return true;
            if (NetworkServer.active == true && networkID.clientAuthorityOwner == null) return true;
            return false;
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

    }

}
