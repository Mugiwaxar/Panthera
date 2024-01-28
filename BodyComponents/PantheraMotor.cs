using EntityStates;
using KinematicCharacterController;
using Panthera;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.ParticleSystem;

namespace Panthera.BodyComponents
{
    [RequireComponent(typeof(CharacterBody))]
    public class PantheraMotor : CharacterMotor
    {

        public PantheraObj pantheraObj;
        public CharacterBody characterBody;
        public PantheraKinematicMotor kinematicPantheraMotor;
        public BodyAnimatorSmoothingParameters.SmoothingParameters smoothingParameters;

        public float lastSpringTime = 0;
        public bool doJump;
        public bool isSprinting;
        public bool wasDashing;

        public void DoInit()
        {
            this.pantheraObj = GetComponent<PantheraObj>();
            this.characterBody = GetComponent<CharacterBody>();
            this.kinematicPantheraMotor = GetComponent<PantheraKinematicMotor>();
            base.Motor = GetComponent<PantheraKinematicMotor>();
            this.smoothingParameters = BodyAnimatorSmoothingParameters.defaultParameters;
        }

        public new void Awake()
        {
            if (base.Motor == null) base.Motor = GetComponent<PantheraKinematicMotor>();
            base.networkIdentity = GetComponent<NetworkIdentity>();
            base.body = GetComponent<PantheraBody>();
            base.capsuleCollider = GetComponent<CapsuleCollider>();
            base.previousPosition = transform.position;
            base.Motor.Rigidbody.mass = mass;
            base.Motor.MaxStableSlopeAngle = 70f;
            base.Motor.MaxStableDenivelationAngle = 55f;
            base.Motor.RebuildCollidableLayers();
            if (generateParametersOnAwake)
            {
                base.GenerateParameters();
            }
            base.useGravity = base.gravityParameters.CheckShouldUseGravity();
            base.isFlying = base.flightParameters.CheckShouldUseFlight();
        }

        public new void FixedUpdate()
        {

            // Return if Panthera is Sleeping //
            if (this.pantheraObj?.getPassiveScript()?.isSleeping == true)
                return;

            // Do the jump //
            ProcessJump();

            // Get the move vector //
            Vector3 moveVector = this.pantheraObj.pantheraInputBank.moveVector;

            // Get the Aim direction //
            Vector3 aimDirection = this.pantheraObj.pantheraInputBank.aimDirection;

            // Set the move direction //
            base.moveDirection = moveVector;

            // Set the move vector //
            base.characterDirection.moveVector = this.characterBody.shouldAim ? aimDirection : moveVector;

            // Stop the sprint if the character is too slow //
            if (moveVector.magnitude <= 0.5f)
            {
                this.isSprinting = false;
            }

            // Set the sprint //
            this.characterBody.isSprinting = this.isSprinting;

            float fixedDeltaTime = Time.fixedDeltaTime;
            if (fixedDeltaTime == 0f)
            {
                return;
            }

            Vector3 position = base.transform.position;
            if ((base.previousPosition - position).sqrMagnitude < 0.00062500004f * fixedDeltaTime)
            {
                base.restStopwatch += fixedDeltaTime;
            }
            else
            {
                base.restStopwatch = 0f;
            }

            base.previousPosition = position;
            if (base.netIsGrounded)
            {
                base.lastGroundedTime = Run.FixedTimeStamp.now;
            }

            // Check if the Character is Stealthed //
            if (this.pantheraObj != null && this.pantheraObj.stealthed == true)
            {
                //// Check the Silent Predator Ability and stop the Sprint //
                //if (pantheraObj.activePreset.getAbilityLevel(PantheraConfig.SilentPredatorAbilityID) < 3)
                //    characterBody.isSprinting = false;
            }

            // Update the Dash speed //
            if (this.pantheraObj.dashing == true && this.wasDashing == false)
            {
                characterBody.RecalculateStats();
                wasDashing = true;
            }
            else if (this.pantheraObj.dashing == false && this.wasDashing == true)
            {
                this.characterBody.RecalculateStats();
                this.wasDashing = false;
            }

        }

        public virtual void ProcessJump()
        {

            bool featherJump = false;
            bool JumpBoost = false;

            // Check if the character can jump //
            if (this.doJump && base.jumpCount < this.characterBody.maxJumpCount)
            {

                // Check if Wax Quail Item must trigger //
                int itemCount = this.characterBody.inventory.GetItemCount(RoR2Content.Items.JumpBoost);
                float horizontalBonus = 1f;
                float verticalBonus = 1f;
                if (base.jumpCount >= 1)
                {
                    featherJump = true;
                    horizontalBonus = 1.5f;
                    verticalBonus = 1.5f;
                }
                else if (itemCount > 0f && this.characterBody.isSprinting)
                {
                    float num = characterBody.acceleration * airControl;
                    if (this.characterBody.moveSpeed > 0f && num > 0f)
                    {
                        JumpBoost = true;
                        float num2 = Mathf.Sqrt(10f * itemCount / num);
                        float num3 = characterBody.moveSpeed / num;
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
                if (featherJump)
                {
                    EffectManager.SpawnEffect(Resources.Load<GameObject>("Prefabs/Effects/FeatherEffect"), new EffectData
                    {
                        origin = characterBody.footPosition
                    }, true);
                }
                else if (jumpCount > 0)
                {
                    EffectManager.SpawnEffect(Resources.Load<GameObject>("Prefabs/Effects/ImpactEffects/CharacterLandImpact"), new EffectData
                    {
                        origin = characterBody.footPosition,
                        scale = characterBody.radius
                    }, true);
                }

                // Spawn the jump boost effect //
                if (JumpBoost)
                {
                    EffectManager.SpawnEffect(Resources.Load<GameObject>("Prefabs/Effects/BoostJumpEffect"), new EffectData
                    {
                        origin = characterBody.footPosition,
                        rotation = Util.QuaternionSafeLookRotation(velocity)
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
