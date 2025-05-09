﻿using EntityStates;
using EntityStates.Jellyfish;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.Machines;
using Panthera.MachineScripts;
using Panthera.Skills;
using Panthera.Utils;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static UnityEngine.ParticleSystem.PlaybackState;

namespace Panthera.MachineScripts
{
    public class MachineScript : ICloneable
    {

        public PantheraMachineState stateType;

        public GameObject gameObject;
        public PantheraMachine machine;
        public Transform transform;
        public PantheraObj pantheraObj;
        public PantheraBody characterBody;
        public CharacterDirection characterDirection;
        public CameraTargetParams cameraTargetParams;
        public SfxLocator sfxLocator;
        public PantheraInputBank inputBank;
        public PantheraSkillLocator skillLocator;
        public HealthComponent healthComponent;
        public Rigidbody rigidbody;
        public PantheraMotor characterMotor;
        public PantheraKinematicMotor kinematicCharacterMotor;
        public ModelLocator modelLocator;
        public Transform modelTransform;
        public Animator modelAnimator;
        public AimAnimator aimAnimator;
        public BigCatPassive bcp;
        public PantheraFX pantheraFX;
        public PantheraMaster masterObj;

        public float attackSpeedStat
        {
            get
            {
                if (this.characterBody != null)
                    return this.characterBody.attackSpeed;
                return 0;
            }
            set
            {
                if (this.characterBody != null)
                    this.characterBody.attackSpeed = value;
            }
        }
        public float damageStat
        {
            get
            {
                if (this.characterBody != null)
                    return this.characterBody.damage;
                return 0;
            }
            set
            {
                if (this.characterBody != null)
                    this.characterBody.damage = value;
            }
        }
        public float critStat
        {
            get
            {
                if (this.characterBody != null)
                    return this.characterBody.crit;
                return 0;
            }
            set
            {
                if (this.characterBody != null)
                    this.characterBody.crit = value;
            }
        }
        public float moveSpeedStat
        {
            get
            {
                if (this.characterBody != null)
                    return this.characterBody.moveSpeed;
                return 0;
            }
            set
            {
                if (this.characterBody != null)
                    this.characterBody.moveSpeed = value;
            }
        }
        public bool wasInterrupted = false;

        public Sprite icon;
        public string name;
        public float baseCooldown = 1;
        public int stock = 1;
        public int maxStock = 1;
        public string desc1;
        public string desc2;
        public int skillID = -1;
        public int requiredAbilityID = 1;
        public int machineToUse = 1;
        public bool showCooldown = false;
        public bool removeStealth = true;
        public float comboMaxTime = PantheraConfig.Combos_maxTime;
        public ScriptPriority priority = ScriptPriority.NoPriority;
        public ScriptPriority interruptPower = ScriptPriority.NoPriority;
        public bool activated = true;

        public void SetUpScript(GameObject player, PantheraMachine machine)
        {

            this.stateType = PantheraMachineState.HaveToStart;

            // Setup pointers //
            this.machine = machine;
            this.gameObject = player;
            this.transform = player.transform;
            this.pantheraObj = player.GetComponent<PantheraObj>();
            this.characterBody = player.GetComponent<PantheraBody>();
            this.characterDirection = player.GetComponent<CharacterDirection>();
            this.cameraTargetParams = player.GetComponent<CameraTargetParams>();
            this.sfxLocator = player.GetComponent<SfxLocator>();
            this.inputBank = player.GetComponent<PantheraInputBank>();
            this.skillLocator = player.GetComponent<PantheraSkillLocator>();
            this.healthComponent = player.GetComponent<HealthComponent>();
            this.rigidbody = player.GetComponent<Rigidbody>();
            this.characterMotor = player.GetComponent<PantheraMotor>();
            this.kinematicCharacterMotor = player.GetComponent<PantheraKinematicMotor>();
            this.modelLocator = player.GetComponent<ModelLocator>();
            this.modelTransform = this.modelLocator.modelTransform;
            this.modelAnimator = this.modelLocator.modelTransform.GetComponent<Animator>();
            this.aimAnimator = this.modelTransform.GetComponent<AimAnimator>();
            this.pantheraFX = player.GetComponent<PantheraFX>();
            this.masterObj = this.pantheraObj.pantheraMaster;
            this.bcp = this.pantheraObj.getPassiveScript();

        }

        public virtual PantheraSkill getSkillDef()
        {
            return null;
        }

        public virtual bool CanBeUsed(PantheraObj ptraObj)
        {
            return true;
        }

        public virtual void Start()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void FixedUpdate()
        {

        }

        public virtual void Stop()
        {

        }

        public void EndScript()
        {
            this.machine.EndScript();
        }
        public bool CheckScript(MachineScript script)
        {
            if (this.machine.CheckScript(script) == true)
                return true;
            else
                return false;
        }
        public bool TryScript(MachineScript script)
        {
            if (this.machine.TryScript(script) == true)
                return true;
            else
                return false;
        }
        public bool SetScript(MachineScript script)
        {
            if (this.machine.SetScript(script) == true)
                return true;
            else
                return false;
        }

        public T GetComponent<T>()
        {
            return this.gameObject.GetComponent<T>();
        }
        public bool isAuthority
        {
            get
            {
                return this.pantheraObj.hasAuthority();
            }
        }
        public Ray GetAimRay()
        {
            if (this.inputBank)
            {
                return new Ray(this.inputBank.aimOrigin, this.inputBank.aimDirection);
            }
            return new Ray(this.transform.position, this.transform.forward);
        }
        public void StartAimMode(float duration = 2f, bool snap = false)
        {
            Ray aimRay = this.GetAimRay();

            if (this.characterDirection && aimRay.direction != Vector3.zero)
            {
                if (snap)
                {
                    this.characterDirection.forward = aimRay.direction;
                }
                else
                {
                    this.characterDirection.moveVector = aimRay.direction;
                }
            }
            if (this.characterBody)
            {
                this.characterBody.SetAimTimer(duration);
            }
            if (this.modelLocator)
            {
                Transform modelTransform = this.modelLocator.modelTransform;
                if (modelTransform)
                {
                    AimAnimator component = modelTransform.GetComponent<AimAnimator>();
                    if (component && snap)
                    {
                        component.AimImmediate();
                    }
                }
            }
        }
        public bool RollCrit()
        {
            return Util.CheckRoll(this.critStat, this.characterBody.master);
        }
        public void PlayAnimation(string animationStateName, float crossFadeTime = 0)
        {
            Utils.Animation.PlayAnimation(this.pantheraObj, animationStateName, crossFadeTime);
        }
        public TeamIndex GetTeam()
        {
            return TeamComponent.GetObjectTeam(this.gameObject);
        }
        public void AddRecoil(float verticalMin, float verticalMax, float horizontalMin, float horizontalMax)
        {
            cameraTargetParams.AddRecoil(verticalMin, verticalMax, horizontalMin, horizontalMax);
        }
        public void RandomHorizontalRecoil(float min, float max)
        {
            int rand = UnityEngine.Random.Range(0, 2);
            if (rand == 0)
                this.AddRecoil(0, 0, min, max);
            else
                this.AddRecoil(0, 0, 1 - min, 1 - max);
        }
        public void RandomVerticalRecoil(float min, float max)
        {
            int rand = UnityEngine.Random.Range(0, 2);
            if (rand == 0)
                this.AddRecoil(min, max, 0, 0);
            else
                this.AddRecoil(1 - min, 1 - max, 0, 0);
        }
        public int getAbilityLevel(int ID)
        {
            return this.pantheraObj.profileComponent.getAbilityLevel(ID);
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public void Dash(float power)
        {
            PhysForceInfo physForceInfo = PhysForceInfo.Create();
            physForceInfo.force = this.characterDirection.forward * power;
            physForceInfo.ignoreGroundStick = true;
            physForceInfo.disableAirControlUntilCollision = true;
            physForceInfo.massIsOne = true;
            characterMotor.ApplyForceImpulse(physForceInfo);
        }

    }

    public enum ScriptPriority : int
    {
        NoPriority = 0,
        MinimumPriority = 1,
        VerySmallPriority = 2,
        SmallPriority = 3,
        AveragePriority = 4,
        HightPriority = 5,
        VeryHightPriority = 6,
        ExtraPriority = 7,
        ExtraHightPriority = 8,
        MaximumPriority = 9,
        UnstoppablePriority = 10
    }

}
