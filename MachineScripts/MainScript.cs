using EntityStates;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.MachineScripts
{
    class MainScript : MachineScript
    {

        CharacterAnimParamAvailability characterAnimParamAvailability;
        public BodyAnimatorSmoothingParameters.SmoothingParameters smoothingParameters;
        public CharacterAnimatorWalkParamCalculator animatorWalkParamCalculator;
        public CameraTargetParams.AimRequest aimRequest;

        public bool wasGrounded;
        public Vector3 previousPosition;
        public Vector3 estimatedVelocity;
        public float lastYSpeed;
        public int emoteRequest = -1;

        public override void Start()
        {

            // Set up the animator //
            this.characterAnimParamAvailability = CharacterAnimParamAvailability.FromAnimator(this.modelAnimator);
            this.smoothingParameters = BodyAnimatorSmoothingParameters.defaultParameters;

            // Setup the characterBody
            if (base.characterBody)
            {
                this.attackSpeedStat = base.characterBody.attackSpeed;
                this.damageStat = base.characterBody.damage;
                this.critStat = base.characterBody.crit;
                this.moveSpeedStat = base.characterBody.moveSpeed;
            }

            // Start the animation //
            int layerIndex = this.modelAnimator.GetLayerIndex("Body");
            if (this.characterAnimParamAvailability.isGrounded)
            {
                this.wasGrounded = this.characterMotor.isGrounded;
                this.modelAnimator.SetBool(AnimationParameters.isGrounded, this.wasGrounded);
            }
            if (this.characterMotor.isGrounded)
            {
                this.modelAnimator.CrossFadeInFixedTime("Idle", 0.1f, layerIndex);
            }
            else
            {
                this.modelAnimator.CrossFadeInFixedTime("AscendDescend", 0.1f, layerIndex);
            }
            this.modelAnimator.Update(0f);

            // Set the previous position //
            this.previousPosition = base.transform.position;

            // Enable the Aim Animator //
            this.aimAnimator.enabled = true;

        }

        public override void Update()
        {

            // Update animation //
            Vector3 position = base.transform.position;
            this.estimatedVelocity = (position - this.previousPosition) / Time.deltaTime;
            this.previousPosition = position;
            this.UpdateAnimationParameters();

        }

        public override void FixedUpdate()
        {

            // Create the ground impact //
            float num = this.estimatedVelocity.y - this.lastYSpeed;
            if (base.characterMotor.isGrounded && !this.wasGrounded)
            {
                int layerIndex = this.modelAnimator.GetLayerIndex("Impact");
                if (layerIndex >= 0)
                {
                    this.modelAnimator.SetLayerWeight(layerIndex, Mathf.Clamp01(Mathf.Max(new float[]
                    {
                        0.3f,
                        num / 5f,
                        this.modelAnimator.GetLayerWeight(layerIndex)
                    })));
                    this.modelAnimator.PlayInFixedTime("LightImpact", layerIndex, 0f);
                }
            }
            this.wasGrounded = base.characterMotor.isGrounded;
            this.lastYSpeed = this.estimatedVelocity.y;

            // Get all inputs //
            GatherInputs();

            // Do the base movements //
            HandleMovements();

        }

        public override void Stop()
        {

            // Disable the Aim Animator //
            this.aimAnimator.enabled = false;

            // Stop the velocity //
            if (base.isAuthority) base.characterMotor.moveDirection = Vector3.zero;

            // Stop the character animation //
            if (this.characterAnimParamAvailability.isMoving) this.modelAnimator.SetBool(AnimationParameters.isMoving, false);
            if (this.characterAnimParamAvailability.turnAngle) this.modelAnimator.SetFloat(AnimationParameters.turnAngle, 0f);

        }

        public void UpdateAnimationParameters()
        {
            Vector3 vector = base.inputBank ? base.inputBank.moveVector : Vector3.zero;
            bool value = vector != Vector3.zero && base.characterBody.moveSpeed > Mathf.Epsilon;
            this.animatorWalkParamCalculator.Update(vector, base.characterDirection ? base.characterDirection.animatorForward : base.transform.forward, this.smoothingParameters, Time.fixedDeltaTime);
            if (this.characterAnimParamAvailability.walkSpeed)
            {
                Utils.Functions.SetAnimatorFloat(this.gameObject, "walkSpeed", base.characterBody.moveSpeed);
            }
            if (this.characterAnimParamAvailability.isGrounded)
            {
                Utils.Functions.SetAnimatorBoolean(this.gameObject, "isGrounded", base.characterMotor.isGrounded);
            }
            if (this.characterAnimParamAvailability.isMoving)
            {
                Utils.Functions.SetAnimatorBoolean(this.gameObject, "isMoving", value);
            }
            if (this.characterAnimParamAvailability.turnAngle)
            {
                Utils.Functions.SetAnimatorFloat(this.gameObject, "turnAngle", this.animatorWalkParamCalculator.remainingTurnAngle, this.smoothingParameters.turnAngleSmoothDamp, Time.fixedDeltaTime);
            }
            if (this.characterAnimParamAvailability.isSprinting)
            {
                Utils.Functions.SetAnimatorBoolean(this.gameObject, "isSprinting", base.characterBody.isSprinting);
            }
            if (this.characterAnimParamAvailability.forwardSpeed)
            {
                Utils.Functions.SetAnimatorFloat(this.gameObject, "forwardSpeed", this.animatorWalkParamCalculator.animatorWalkSpeed.x, this.smoothingParameters.forwardSpeedSmoothDamp, Time.deltaTime);
            }
            if (this.characterAnimParamAvailability.rightSpeed)
            {
                this.modelAnimator.SetFloat(AnimationParameters.rightSpeed, this.animatorWalkParamCalculator.animatorWalkSpeed.y, this.smoothingParameters.rightSpeedSmoothDamp, Time.deltaTime);
                Utils.Functions.SetAnimatorFloat(this.gameObject, "rightSpeed", this.animatorWalkParamCalculator.animatorWalkSpeed.y, this.smoothingParameters.rightSpeedSmoothDamp, Time.deltaTime);
            }
            if (this.characterAnimParamAvailability.upSpeed)
            {
                this.modelAnimator.SetFloat(AnimationParameters.upSpeed, this.estimatedVelocity.y, 0.1f, Time.deltaTime);
                Utils.Functions.SetAnimatorFloat(this.gameObject, "upSpeed", this.estimatedVelocity.y, 0.1f, Time.deltaTime);
            }
        }

        public void GatherInputs()
        {
            this.emoteRequest = base.inputBank.emoteRequest;
            base.inputBank.emoteRequest = -1;
        }

        public void HandleMovements()
        {

            

            // ---------- ?????????????????????????????? ----------------------- //
            // Set the camera //
            //if (this.startSprint)
            //{
            //    if (base.cameraTargetParams.currentAimMode == CameraTargetParams.AimType.Standard)
            //    {
            //        this.aimRequest = base.cameraTargetParams.RequestAimType(CameraTargetParams.AimType.Sprinting);
            //    }
            //}
            //else if (base.characterMotor.superSprinting == true)
            //{
            //    this.aimRequest = base.cameraTargetParams.RequestAimType(CameraTargetParams.AimType.Aura);
            //}
            //else
            //{
            //    CameraTargetParams.AimRequest aimRequest = this.aimRequest;
            //    if (aimRequest != null)
            //    {
            //        aimRequest.Dispose();
            //    }
            //}
            // ---------- ?????????????????????????????? ----------------------- //

        }


    }
}
