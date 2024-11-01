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
using Panthera.Passives;
using Panthera.OldSkills;
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
using Panthera.Skills.Actives;
using Panthera.Skills.Passives;
using UnityEngine.TextCore;
using System.Linq;
using static UnityEngine.ParticleSystem;

namespace Panthera.BodyComponents
{
    public class PantheraObj : NetworkBehaviour
    {
        public Player InputPlayer => Panthera.InputPlayer;
        public CharacterAbilities CharacterAbilities => Panthera.PantheraCharacter.CharacterAbilities;
        public CharacterSkills CharacterSkills => Panthera.PantheraCharacter.CharacterSkills;
        public CharacterCombos CharacterCombos => Panthera.PantheraCharacter.CharacterCombos;

        public Dictionary<int, bool> activatedComboList = [];
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
        public PantheraMaster _pantheraMaster;
        public PantheraMaster PantheraMaster
        {
            get
            {
                if (!this._pantheraMaster)
                {
                    var master = this.characterBody ? this.characterBody.masterObject : null;
                    if (master && !master.TryGetComponent<PantheraMaster>(out _pantheraMaster))
                    {
                        _pantheraMaster = master.AddComponent<PantheraMaster>();
                    }
                }
                return this._pantheraMaster;
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

        public int PantheraSkinIndex => characterBody != null ? (int)characterBody.skinIndex + 1 : 1;
        public int ActualPantheraSkinIndex;
        public float modelScale = PantheraConfig.Model_defaultModelScale;
        public float changeModelScale = PantheraConfig.Model_defaultModelScale;

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
        public Coroutine UnstealthCoroutine
        {
            get => _unstealthCoroutine;
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
        public int AttackNumber
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

        public void DoDamageSelf(float damage) => new ServerInflictDamage(base.gameObject, base.gameObject, base.transform.position, damage).Send(NetworkDestination.Server);

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

            // Set the Main Renderer //
            characterModel.mainSkinnedMeshRenderer = this.modelTransform.GetComponentInChildren<SkinnedMeshRenderer>();

            // Get the Original Layer Index //
            this.origLayerIndex = this.characterModel.mainSkinnedMeshRenderer.gameObject.layer;

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
            CharacterBody.onBodyStartGlobal += OnEntitySpawned;

            // Get all spawned Entities //
            foreach (CharacterMaster master in CharacterMaster.readOnlyInstancesList)
            {
                if (master.GetBody() != null)
                    this.OnEntitySpawned(master.GetBody());
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
            foreach (KeyValuePair<int, PantheraCombo> pair in CharacterCombos.CombosList)
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
            this.defaultCamPos = new Vector3(camPos.x, camPos.y, camPos.z * modelScale);
            Utils.CamHelper.ApplyAimType(Utils.CamHelper.AimType.Standard, this);

            // Set Camera Fade distance //
            CameraRigController rigCtrl = Camera.main.transform.parent.GetComponent<CameraRigController>();
            if (rigCtrl != null)
            {
                rigCtrl.fadeStartDistance = PantheraConfig.Model_fadeStartDistance;
                rigCtrl.fadeEndDistance = PantheraConfig.Model_fadeEndDistance;
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
            int eternalFuryLvl = this.GetAbilityLevel(PantheraConfig.EternalFury_AbilityID);
            if (eternalFuryLvl == 1)
                minFury = this.characterBody.maxFury * PantheraConfig.EternalFury_startPercent1;
            else if (eternalFuryLvl == 2)
                minFury = this.characterBody.maxFury * PantheraConfig.EternalFury_startPercent2;
            else if (eternalFuryLvl == 3)
                minFury = this.characterBody.maxFury * PantheraConfig.EternalFury_startPercent3;

            // Set Fury back //
            this.characterBody.fury = Math.Max(this.PantheraMaster.savedFury, minFury);

            // Set Cooldown Back //
            this.skillLocator.rechargeSkillList = this.PantheraMaster.savedCooldownList;

            // Create the Recharge Stock List //
            if (this.skillLocator.rechargeSkillList == null)
                this.skillLocator.createRechargeSkillsList();

            // Apply all Stats //
            this.ApplyStats();

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
            if (this.HasAuthority())
            {
                // Enable or disable the Front Shield //
                if (this.GetSkillMachine1SciptType() == typeof(Skills.Actives.FrontShield) || this.GetSkillMachine1SciptType() == typeof(Skills.Actives.ShieldBash) || this.frontShieldDeployed == true)
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
                if(this.frontShieldObj.active == true)
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
                else if (PantheraSkinIndex == 4)
                {
                    Skin.ChangedToMainModel4(this.childLocator);
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

            // Check the Model Scale //
            if (this.HasAuthority() == true && this.modelScale != this.changeModelScale)
            {
                if (this.modelScale > this.changeModelScale)
                    this.modelScale -= 0.01f;
                else if (this.modelScale < this.changeModelScale)
                    this.modelScale += 0.01f;
                if (Math.Abs(this.modelScale - this.changeModelScale) < 0.02f)
                {
                    this.modelScale = this.changeModelScale;
                    Vector3 camPos = PantheraConfig.defaultCamPosition;
                    this.defaultCamPos = new Vector3(camPos.x, camPos.y, camPos.z * modelScale);
                    Utils.CamHelper.ApplyAimType(Utils.CamHelper.AimType.Standard, this);
                }
                this.transform.localScale = new Vector3(this.modelScale, this.modelScale, this.modelScale);
                this.modelTransform.localScale = new Vector3(this.modelScale, this.modelScale, this.modelScale);
                new ServerChangePantheraScale(base.gameObject, this.modelScale).Send(NetworkDestination.Server);
            }

            // Update the Front Shield Scale //
            float scale = this.frontShieldScale;
            if (this.frontShieldDeployed == true && scale < PantheraConfig.ArcaneAnchor_deployedScale)
                scale += PantheraConfig.ArcaneAnchor_deployedScaleSpeed;
            else if (this.frontShieldDeployed == false)
                scale = PantheraConfig.FrontShield_defaultScale * this.modelScale;
            if (scale != this.frontShieldScale)
            {
                this.frontShieldScale = scale;
                this.frontShieldObj.transform.localScale = new Vector3(scale, scale, scale);
            }

            // Create a Debug Line //
            //LineRenderer lineComp = base.gameObject.GetComponent<LineRenderer>();
            //if (lineComp == null)
            //{
            //    lineComp = base.gameObject.AddComponent<LineRenderer>();
            //    lineComp.widthMultiplier = 0.2f;
            //    lineComp.positionCount = 2;
            //}

            //lineComp.SetPosition(0, this.pantheraInputBank.aimOrigin);
            //lineComp.SetPosition(1, this.pantheraInputBank.aimOrigin + (this.pantheraInputBank.aimDirection * 1000));

        }

        public void OnDestroy()
        {
            if (this.HasAuthority() == true)
            {
                // Check the Master //
                if(this.PantheraMaster != null)
                {
                    // Save the Fury //
                    this.PantheraMaster.savedFury = this.characterBody.fury;
                    // Save the Cooldown //
                    this.PantheraMaster.savedCooldownList = this.skillLocator.rechargeSkillList;
                }
                // Set all Skills Icones Back //
                this.skillLocator.primary.skillDef.icon = PantheraAssets.RipSkillMenu;
                this.skillLocator.secondary.skillDef.icon = PantheraAssets.SlashSkillMenu;
            }
            // Destroy the Front Shield //
            GameObject.Destroy(this.frontShieldObj);
            // Stop all Events //
            CharacterBody.onBodyStartGlobal -= OnEntitySpawned;
        }

        public void ApplyStats() => characterBody.RecalculateStats();

        public bool HasAuthority() => this.networkID.hasAuthority || (NetworkServer.active && networkID.clientAuthorityOwner == null);

        public void OnEntitySpawned(CharacterBody body)
        {
            // Check if Monster //
            if (body.teamComponent != null && body.teamComponent.teamIndex != TeamIndex.Monster) return;
            // Add the Predator Component //
            if (body.gameObject.GetComponent<PredatorComponent>() == null)
                body.gameObject.AddComponent<PredatorComponent>();
        }

        public int GetAbilityLevel(int abilityID) => this.profileComponent.GetAbilityLevel(abilityID);

        public bool IsMastery(int abilityID) => this.profileComponent.isMastery(abilityID);

        public bool IsSkillUnlocked(int skillId) => this.profileComponent.IsSkillUnlocked(skillId);

        public bool IsComboUnlocked(int comboID) => this.profileComponent.isComboUnlocked(comboID);

        public Transform FindModelChild(string childName) => childLocator.FindChild(childName);

        public MainScript GetMainScript() => (MainScript)GetComponent<PantheraMainMachine>().GetCurrentScript();

        public BigCatPassive GetPassiveScript() => (BigCatPassive)GetComponent<PantheraPassiveMachine>().GetCurrentScript();

        public MachineScript GetSkillMachine1Scipt() => skillsMachine1.GetCurrentScript();

        public MachineScript GetSkillMachine2Scipt() => skillsMachine2.GetCurrentScript();

        public Type GetSkillMachine1SciptType() => skillsMachine1.GetCurrentScript()?.GetType();

        public Type GetSkillMachine2SciptType() => skillsMachine2.GetCurrentScript()?.GetType();

        public void StopAllScripts()
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

        public static void ReadDefs()
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
}
