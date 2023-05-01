using EntityStates;
using KinematicCharacterController;
using Panthera;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.Passives;
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

        public bool sprinting = false;
        public float lastSpringTime = 0;
        public bool doJump;
        public bool startSprint;
        public bool wasDashing;

        public void DoInit()
        {
            pantheraObj = GetComponent<PantheraObj>();
            characterBody = GetComponent<CharacterBody>();
            kinematicPantheraMotor = GetComponent<PantheraKinematicMotor>();
            Motor = GetComponent<PantheraKinematicMotor>();
            smoothingParameters = BodyAnimatorSmoothingParameters.defaultParameters;
        }

        public new void Awake()
        {
            if (Motor == null) Motor = GetComponent<PantheraKinematicMotor>();
            networkIdentity = GetComponent<NetworkIdentity>();
            body = GetComponent<PantheraBody>();
            capsuleCollider = GetComponent<CapsuleCollider>();
            previousPosition = transform.position;
            Motor.Rigidbody.mass = mass;
            Motor.MaxStableSlopeAngle = 70f;
            Motor.MaxStableDenivelationAngle = 55f;
            Motor.RebuildCollidableLayers();
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
            ProcessJump();

            // Get the move vector //
            Vector3 moveVector = pantheraObj.pantheraInputBank.moveVector;

            // Get the Aim direction //
            Vector3 aimDirection = pantheraObj.pantheraInputBank.aimDirection;

            // Set the move direction //
            moveDirection = moveVector;

            // Set the move vector //
            characterDirection.moveVector = characterBody.shouldAim ? aimDirection : moveVector;

            // Stop the sprint if the character is too slow //
            if (moveVector.magnitude <= 0.5f)
            {
                startSprint = false;
            }

            // Set the sprint //
            characterBody.isSprinting = startSprint;

            float fixedDeltaTime = Time.fixedDeltaTime;
            if (fixedDeltaTime == 0f)
            {
                return;
            }

            Vector3 position = transform.position;
            if ((previousPosition - position).sqrMagnitude < 0.00062500004f * fixedDeltaTime)
            {
                restStopwatch += fixedDeltaTime;
            }
            else
            {
                restStopwatch = 0f;
            }

            previousPosition = position;
            if (netIsGrounded)
            {
                lastGroundedTime = Run.FixedTimeStamp.now;
            }

            // Check if the Character is Stealthed //
            if (pantheraObj != null && pantheraObj.stealthed == true)
            {
                // Check the Silent Predator Ability and stop the Sprint //
                if (pantheraObj.activePreset.getAbilityLevel(PantheraConfig.SilentPredatorAbilityID) < 3)
                    characterBody.isSprinting = false;
            }

            // Update the Dash speed //
            if (pantheraObj.dashing == true && wasDashing == false)
            {
                characterBody.RecalculateStats();
                wasDashing = true;
            }
            else if (pantheraObj.dashing == false && wasDashing == true)
            {
                characterBody.RecalculateStats();
                wasDashing = false;
            }

        }

        public virtual void ProcessJump()
        {

            bool featherJump = false;
            bool JumpBoost = false;

            // Check if the character can jump //
            if (doJump && jumpCount < characterBody.maxJumpCount)
            {

                // Check if Wax Quail Item must trigger //
                int itemCount = characterBody.inventory.GetItemCount(RoR2Content.Items.JumpBoost);
                float horizontalBonus = 1f;
                float verticalBonus = 1f;
                if (jumpCount >= 1)
                {
                    featherJump = true;
                    horizontalBonus = 1.5f;
                    verticalBonus = 1.5f;
                }
                else if (itemCount > 0f && characterBody.isSprinting)
                {
                    float num = characterBody.acceleration * airControl;
                    if (characterBody.moveSpeed > 0f && num > 0f)
                    {
                        JumpBoost = true;
                        float num2 = Mathf.Sqrt(10f * itemCount / num);
                        float num3 = characterBody.moveSpeed / num;
                        horizontalBonus = (num2 + num3) / num3;
                    }
                }

                // Apply the jump velocity to the character //
                GenericCharacterMain.ApplyJumpVelocity(this, characterBody, horizontalBonus, verticalBonus, false);

                // Create the jump animation //
                int layerIndex = pantheraObj.modelAnimator.GetLayerIndex("Body");
                if (layerIndex >= 0)
                {
                    if (jumpCount == 0 || characterBody.baseJumpCount == 1)
                    {
                        pantheraObj.modelAnimator.CrossFadeInFixedTime("Jump", smoothingParameters.intoJumpTransitionTime, layerIndex);
                    }
                    else
                    {
                        pantheraObj.modelAnimator.CrossFadeInFixedTime("BonusJump", smoothingParameters.intoJumpTransitionTime, layerIndex);
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
                jumpCount++;

            }

            // Turn doJump to false //
            doJump = false;

        }

    }
}
