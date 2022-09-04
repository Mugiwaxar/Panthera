using EntityStates;
using KinematicCharacterController;
using Panthera.Passives;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.ParticleSystem;

namespace Panthera.Components
{
    [RequireComponent(typeof(CharacterBody))]
    class PantheraMotor : CharacterMotor
    {

        public PantheraObj pantheraObj;
        public CharacterBody characterBody;
        public PantheraKinematicMotor kinematicPantheraMotor;
        public BodyAnimatorSmoothingParameters.SmoothingParameters smoothingParameters;

        public bool sprinting = false;
        public bool superSprinting = false;
        public float lastSpringTime = 0;
        public bool doJump;
        public bool startSprint;

        public void DoInit()
        {
            this.pantheraObj = base.GetComponent<PantheraObj>();
            this.characterBody = base.GetComponent<CharacterBody>();
            this.kinematicPantheraMotor = base.GetComponent<PantheraKinematicMotor>();
            this.Motor = base.GetComponent<PantheraKinematicMotor>();
            this.smoothingParameters = BodyAnimatorSmoothingParameters.defaultParameters;
        }

        public new void Awake()
        {
            if (this.Motor == null) this.Motor = base.GetComponent<PantheraKinematicMotor>();
            networkIdentity = GetComponent<NetworkIdentity>();
            body = GetComponent<PantheraBody>();
            capsuleCollider = GetComponent<CapsuleCollider>();
            previousPosition = base.transform.position;
            base.Motor.Rigidbody.mass = mass;
            base.Motor.MaxStableSlopeAngle = 70f;
            base.Motor.MaxStableDenivelationAngle = 55f;
            base.Motor.RebuildCollidableLayers();
            if (generateParametersOnAwake)
            {
                GenerateParameters();
            }
            useGravity = gravityParameters.CheckShouldUseGravity();
            isFlying = flightParameters.CheckShouldUseFlight();
        }

        public new void FixedUpdate()
        {

            // Do the jump //
            this.ProcessJump();

            // Get the move vector //
            Vector3 moveVector = this.pantheraObj.pantheraInputBank.moveVector;

            // Get the Aim direction //
            Vector3 aimDirection = this.pantheraObj.pantheraInputBank.aimDirection;

            // Set the move direction //
            base.moveDirection = moveVector;

            // Set the move vector //
            base.characterDirection.moveVector = (this.characterBody.shouldAim ? aimDirection : moveVector);

            // Stop the sprint if the character is too slow //
            if (moveVector.magnitude <= 0.5f)
            {
                this.startSprint = false;
            }

            // Set the sprint //
            this.characterBody.isSprinting = this.startSprint;

            // Make the character super-sprint //
            //if (this.characterBody.isSprinting == true)
            //{
            //    if (this.sprinting == false)
            //    {
            //        this.lastSpringTime = Time.time;
            //        this.sprinting = true;
            //    }
            //    float sprintTime = Time.time - this.lastSpringTime;
            //    if (sprintTime >= PantheraConfig.superSprintDelay)
            //    {
            //        if (pantheraObj.HasAuthority() == true) SuperSprint.StartSuperSprint(this);
            //    }
            //}
            //else
            //{
            //    if (pantheraObj.HasAuthority() == true) SuperSprint.StopSuperSpring(this);
            //    this.sprinting = false;
            //}

            float fixedDeltaTime = Time.fixedDeltaTime;
            if (fixedDeltaTime == 0f)
            {
                return;
            }

            Vector3 position = base.transform.position;
            if ((this.previousPosition - position).sqrMagnitude < 0.00062500004f * fixedDeltaTime)
            {
                this.restStopwatch += fixedDeltaTime;
            }
            else
            {
                this.restStopwatch = 0f;
            }

            this.previousPosition = position;
            if (this.netIsGrounded)
            {
                this.lastGroundedTime = Run.FixedTimeStamp.now;
            }

            // Check if the Character is Stealed //
            if (this.pantheraObj.GetPassiveScript() != null && this.pantheraObj.GetPassiveScript().stealed == true)
            {
                // Stop the Sprint //
                this.characterBody.isSprinting = false;
            }

        }

        public virtual void ProcessJump()
        {

            bool flag = false;
            bool flag2 = false;

            // Check if the character can jump //
            if (this.doJump && base.jumpCount < this.characterBody.maxJumpCount)
            {

                // Check if Wax Quail Item must trigger //
                int itemCount = this.characterBody.inventory.GetItemCount(RoR2Content.Items.JumpBoost);
                float horizontalBonus = 1f;
                float verticalBonus = 1f;
                if (base.jumpCount >= this.characterBody.baseJumpCount)
                {
                    flag = true;
                    horizontalBonus = 1.5f;
                    verticalBonus = 1.5f;
                }
                else if ((float)itemCount > 0f && this.characterBody.isSprinting)
                {
                    float num = this.characterBody.acceleration * base.airControl;
                    if (this.characterBody.moveSpeed > 0f && num > 0f)
                    {
                        flag2 = true;
                        float num2 = Mathf.Sqrt(10f * (float)itemCount / num);
                        float num3 = this.characterBody.moveSpeed / num;
                        horizontalBonus = (num2 + num3) / num3;
                    }
                }

                // Apply the jump velocity to the character //
                GenericCharacterMain.ApplyJumpVelocity(this, this.characterBody, horizontalBonus, verticalBonus, false);

                // Create the jump animation //
                int layerIndex = this.pantheraObj.modelAnimator.GetLayerIndex("Body");
                if (layerIndex >= 0)
                {
                    if (base.jumpCount == 0 || this.characterBody.baseJumpCount == 1)
                    {
                        this.pantheraObj.modelAnimator.CrossFadeInFixedTime("Jump", this.smoothingParameters.intoJumpTransitionTime, layerIndex);
                    }
                    else
                    {
                        this.pantheraObj.modelAnimator.CrossFadeInFixedTime("BonusJump", this.smoothingParameters.intoJumpTransitionTime, layerIndex);
                    }
                }

                // Spawn the jump effect //
                if (flag)
                {
                    EffectManager.SpawnEffect(Resources.Load<GameObject>("Prefabs/Effects/FeatherEffect"), new EffectData
                    {
                        origin = this.characterBody.footPosition
                    }, true);
                }
                else if (base.jumpCount > 0)
                {
                    EffectManager.SpawnEffect(Resources.Load<GameObject>("Prefabs/Effects/ImpactEffects/CharacterLandImpact"), new EffectData
                    {
                        origin = this.characterBody.footPosition,
                        scale = this.characterBody.radius
                    }, true);
                }

                // Spawn the jump boost effect //
                if (flag2)
                {
                    EffectManager.SpawnEffect(Resources.Load<GameObject>("Prefabs/Effects/BoostJumpEffect"), new EffectData
                    {
                        origin = this.characterBody.footPosition,
                        rotation = Util.QuaternionSafeLookRotation(base.velocity)
                    }, true);
                }

                // Increase the jump count //
                base.jumpCount++;

            }

            // Turn doJump to false //
            this.doJump = false;

        }

    }
}
