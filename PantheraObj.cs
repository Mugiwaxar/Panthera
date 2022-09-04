using EntityStates;
using KinematicCharacterController;
using Panthera.Components;
using Panthera.GUI;
using Panthera.Machines;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.Skills;
using R2API;
using R2API.Networking;
using R2API.Networking.Interfaces;
using Rewired;
using RoR2;
using RoR2.Audio;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Panthera
{
    class PantheraObj : MonoBehaviour
    {

        public static PantheraObj Instance;

        public GameObject player;
        public Player networkUser;
        public NetworkIdentity networkID;
        public PantheraBody characterBody;
        public SkillLocator skillLocator;
        public ModelLocator modelLocator;        
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

        public PantheraMachine mainMachine;
        public PantheraMachine passiveMachine;
        public PantheraMachine skillsMachine;
        public PantheraMachine networkMachine;

        public PantheraHUD pantheraHUD;

        public GameObject frontShield;

        public bool authorityStart;
        public LeapCercleComponent actualLeapCerle;
        public CharacterBody holdedPrey;
        public bool onRaySlashCharge = false;

        public void Awake()
        {

            // Get values //
            PantheraObj.Instance = this;
            this.player = this.gameObject;
            this.networkID = player.GetComponent<NetworkIdentity>();
            this.characterBody = player.GetComponent<PantheraBody>();
            this.skillLocator = player.GetComponent<SkillLocator>();
            this.modelLocator = player.GetComponent<ModelLocator>();
            this.modelTransform = this.modelLocator.modelTransform;
            this.modelAnimator = this.modelLocator.modelTransform.GetComponent<Animator>();
            this.childLocator = this.modelLocator.modelTransform.GetComponent<ChildLocator>();
            this.pantheraMotor = base.GetComponent<PantheraMotor>();
            this.kinematicPantheraMotor = base.GetComponent<PantheraKinematicMotor>();
            this.direction = player.GetComponent<CharacterDirection>();
            this.healthComponent = base.GetComponent<PantheraHealthComponent>();
            this.pantheraInputBank = base.GetComponent<PantheraInputBank>();
            this.mainMachine = base.GetComponent<PantheraMainMachine>();
            this.passiveMachine = base.GetComponent<PantheraPassiveMachine>();
            this.skillsMachine = base.GetComponent<PantheraSkillsMachine>();
            this.networkMachine = base.GetComponent<PantheraNetworkMachine>();
            this.pantheraFX = base.GetComponent<PantheraFX>();
            this.activePreset = Preset.ActivePreset;
            this.pantheraHUD = PantheraHUD.Instance;

            // Init machines //
            this.mainMachine.Init();
            this.passiveMachine.Init();
            this.skillsMachine.Init();
            this.networkMachine.Init();

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

            // Create server hooks //
            if (NetworkServer.active == true) On.RoR2.HealthComponent.TakeDamage += this.healthComponent.OnTakeDamage;

            // Create server + client hooks //
            On.RoR2.CharacterBody.GetVisibilityLevel_TeamIndex += this.characterBody.GetVisibilityLevel;
            On.KinematicCharacterController.KinematicCharacterSystem.Simulate_float += PantheraKinematicSystem.Simulate;
            On.KinematicCharacterController.KinematicCharacterSystem.Simulate_float_KinematicCharacterMotorArray_int_PhysicsMoverArray_int += PantheraKinematicSystem.Simulate;
            On.RoR2.UI.GameEndReportPanelController.SetPlayerInfo += Character.GameEndRepport;

        }

        public void Update()
        {

            // Start for Panthera only //
            if (this.authorityStart == false && HasAuthority() == true)
            {

                // Ask for the InputPlayer //
                this.networkUser = this.characterBody.master.playerCharacterMasterController.networkUser.inputPlayer;
                this.pantheraInputBank.networkUser = this.networkUser;

                // Set start to true //
                this.authorityStart = true;

                // Apply all Stats //
                this.applyStats();

                // Send Preset to the Server //
                if (NetworkServer.active == false) new ServerSyncPreset(this.gameObject, this.activePreset.unlockedAbilitiesList).Send(NetworkDestination.Server);

                // Start the machines //
                this.mainMachine.enabled = true;
                this.passiveMachine.enabled = true;
                this.skillsMachine.enabled = true;

                // Start the input bank //
                this.pantheraInputBank.enabled = true;

                // Disable the network machine //
                if (NetworkServer.active == false)
                    this.networkMachine.enabled = false;

                // Create hooks //
                //On.KinematicCharacterController.KinematicCharacterMotor.IsStableOnNormal += this.kinematicPantheraMotor.IsStableOnNormal;
                //On.KinematicCharacterController.KinematicCharacterMotor.UpdatePhase1 += this.kinematicPantheraMotor.UpdatePhase1;
                //On.KinematicCharacterController.KinematicCharacterMotor.UpdatePhase2 += this.kinematicPantheraMotor.UpdatePhase2;
                //On.RoR2.GenericSkill.RunRecharge += PantheraSkill.OnRunRecharge;

                // Read the defs //
                PantheraConfig.readDefs();

            }

        }

        public void OnDestroy()
        {

            // Stop server hooks //
            if (NetworkServer.active == true) On.RoR2.HealthComponent.TakeDamage -= this.healthComponent.OnTakeDamage;

            // Stop server + client hooks //
            On.RoR2.CharacterBody.GetVisibilityLevel_TeamIndex -= this.characterBody.GetVisibilityLevel;
            On.KinematicCharacterController.KinematicCharacterSystem.Simulate_float -= PantheraKinematicSystem.Simulate;
            On.KinematicCharacterController.KinematicCharacterSystem.Simulate_float_KinematicCharacterMotorArray_int_PhysicsMoverArray_int -= PantheraKinematicSystem.Simulate;
            On.RoR2.UI.GameEndReportPanelController.SetPlayerInfo -= Character.GameEndRepport;

            // Stop Panthera hooks //
            if (this.authorityStart == true)
            {
                //On.KinematicCharacterController.KinematicCharacterMotor.IsStableOnNormal -= this.kinematicPantheraMotor.IsStableOnNormal;
                //On.KinematicCharacterController.KinematicCharacterMotor.UpdatePhase1 -= this.kinematicPantheraMotor.UpdatePhase1;
                //On.KinematicCharacterController.KinematicCharacterMotor.UpdatePhase2 -= this.kinematicPantheraMotor.UpdatePhase2;
                //On.RoR2.GenericSkill.RunRecharge -= PantheraSkill.OnRunRecharge;
            }

        }

        public void applyStats()
        {
            this.characterBody.baseMaxHealth = this.activePreset.maxHealth;
            this.characterBody.levelMaxHealth = this.activePreset.maxHealthLevel;
            this.characterBody.baseRegen = this.activePreset.healthRegen;
            this.characterBody.levelRegen = this.activePreset.healthRegenLevel;
            this.characterBody.baseMoveSpeed = this.activePreset.moveSpeed;
            this.characterBody.levelMoveSpeed = this.activePreset.moveSpeedLevel;
            this.characterBody.baseDamage = this.activePreset.damage;
            this.characterBody.levelDamage = this.activePreset.damageLevel;
            this.characterBody.baseAttackSpeed = this.activePreset.attackSpeed;
            this.characterBody.levelAttackSpeed = this.activePreset.attackSpeedLevel;
            this.characterBody.baseCrit = this.activePreset.critic;
            this.characterBody.levelCrit = this.activePreset.criticLevel;
            this.characterBody.baseArmor = this.activePreset.defense;
            this.characterBody.levelArmor = this.activePreset.defenseLevel;
            this.characterBody.baseJumpCount = (int)this.activePreset.jumpCount;
            this.characterBody.RecalculateStats();
            this.healthComponent.health = this.characterBody.baseMaxHealth;
        }

        public bool HasAuthority()
        {
            if (this.networkID.hasAuthority == true) return true;
            if (NetworkServer.active == true && this.networkID.clientAuthorityOwner == null) return true;
            return false;
        }

        public Transform FindModelChild(string childName)
        {
            return this.childLocator.FindChild(childName);
        }

        public MainScript GetMainScript()
        {
            return (MainScript)base.GetComponent<PantheraMainMachine>().GetCurrentScript();
        }

        public BigCatPassive GetPassiveScript()
        {
            return (BigCatPassive)base.GetComponent<PantheraPassiveMachine>().GetCurrentScript();
        }

        public Type GetActualSkillType()
        {
            if (this.skillsMachine.GetCurrentScript() == null) return null;
            return this.skillsMachine.GetCurrentScript().GetType();
        }

    }

}
