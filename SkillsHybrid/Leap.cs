using EntityStates;
using KinematicCharacterController;
using Panthera;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.GUI;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.Skills;
using Panthera.SkillsHybrid;
using Panthera.Utils;
using R2API;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using RoR2.Audio;
using RoR2.Skills;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using static RoR2.CharacterMotor;
using static UnityEngine.ParticleSystem;

namespace Panthera.SkillsHybrid
{
    class Leap : MachineScript
    {

        public Vector3 leapDirection;
        public Vector3 lastDirection;
        public Collider playerCollider;
        public float maximumDuration = PantheraConfig.Leap_duration;
        public float moveSpeed;
        public float previousAirControl;

        public bool targetFound = false;
        public Vector3 targetPosition = Vector3.zero;
        public CharacterBody targetBody;
        public Collider targetCollider;
        public Rigidbody targetRigidBody;
        public bool targetHit = false;

        public Vector3 originalVelocity;
        public float startingTime;

        public Leap()
        {

        }

        public static void Create()
        {
            // Create the Skill //
            PantheraSkill skill = new PantheraSkill();
            skill.skillID = PantheraConfig.Leap_SkillID;
            skill.name = "LEAP_SKILL_NAME";
            skill.desc = "LEAP_SKILL_DESC";
            skill.icon = Assets.Leap;
            skill.iconPrefab = Assets.HybridSkillPrefab;
            skill.type = PantheraSkill.SkillType.hybrid;
            skill.associatedSkill = typeof(Leap);
            skill.priority = PantheraConfig.Leap_priority;
            skill.interruptPower = PantheraConfig.Leap_interruptPower;
            skill.cooldown = PantheraConfig.Leap_cooldown1;

            // Save this Skill //
            PantheraSkill.SkillDefsList.Add(skill.skillID, skill);
        }

        public override PantheraSkill getSkillDef()
        {
            return base.pantheraObj.activePreset.getSkillByID(PantheraConfig.Leap_SkillID);
        }

        public override bool CanBeUsed(PantheraObj ptraObj)
        {
            base.pantheraObj = ptraObj;
            if (ptraObj.characterBody.stamina < PantheraConfig.Leap_requiredStamina) return false;
            if (ptraObj.skillLocator.getCooldownInSecond(this.getSkillDef().skillID) > 0) return false;
            return true;
        }

        public override void Start()
        {

            // Save the time //
            this.startingTime = Time.time;

            // Remove the Stamina //
            base.characterBody.stamina -= PantheraConfig.Leap_requiredStamina;

            // Set the Air control //
            this.previousAirControl = base.characterMotor.airControl;
            base.characterMotor.airControl = PantheraConfig.Leap_airControl;

            // Set the Jump count to zero //
            base.characterMotor.jumpCount = 0;

            // Save the collider //
            this.playerCollider = GetComponent<Collider>();

            // Set the move speed //
            this.moveSpeed = Math.Min(PantheraConfig.Leap_maxMoveSpeed, base.moveSpeedStat * PantheraConfig.Leap_speedMultiplier);
            float minimumY = PantheraConfig.Leap_minimumY;

            // Get where to leap //
            Vector3 direction = base.GetAimRay().direction;
            HuntressTracker tracker = base.characterBody.GetComponent<HuntressTracker>();
            HurtBox target = tracker.GetTrackingTarget();

            // If there is a target //
            if (target != null)
            {

                // Setup the target //
                this.targetFound = true;
                this.targetPosition = target.transform.position;
                this.targetBody = target.healthComponent?.GetComponent<CharacterBody>();
                this.targetRigidBody = this.targetBody.GetComponent<Rigidbody>();
                this.targetCollider = this.targetBody.GetComponent<Collider>();

                // Set the air control to 0 //
                this.characterMotor.airControl = 0;

                // Get the direction //
                minimumY = PantheraConfig.Leap_minimumYTarget;
                Vector3 charPos = base.characterBody.corePosition;
                Vector3 targetPos = this.targetCollider != null ? this.targetCollider.ClosestPoint(charPos) : this.targetPosition;
                Vector3 relativePos = targetPos - charPos;
                direction = relativePos.normalized;

            }

            // Make the Character Sprint //
            base.pantheraObj.pantheraMotor.startSprint = true;

            // Calculate the Velocity //
            direction.y *= PantheraConfig.Leap_aimRayYMultiplier;
            direction.y = Mathf.Max(direction.y, minimumY);
            Vector3 upVelocity = new Vector3(0, direction.y, 0) * PantheraConfig.Leap_upwardVelocity;
            Vector3 forwardVelocity = new Vector3(direction.x, 0, direction.z);
            Vector3 totalVelocity = upVelocity + forwardVelocity * this.moveSpeed;
            base.characterMotor.Motor.ForceUnground();
            base.characterMotor.velocity = totalVelocity;
            this.originalVelocity = totalVelocity;

            // Set the move vector //
            base.characterDirection.forward = direction;
            this.leapDirection = direction;

            // Play the sound // 
            Sound.playSound(Sound.Leap, base.gameObject);

            // Enable the Trail //
            base.pantheraFX.SetLeapTrailFX(true);

            // Apply the Cooldown //
            if (this.targetFound == true)
            {

                int leapLevel = base.pantheraObj.activePreset.getAbilityLevel(PantheraConfig.LeapAbilityID);

                if (leapLevel == 0)
                    base.skillLocator.setCooldownTime(this.getSkillDef().skillID, Time.time);
                else if (leapLevel == 1)
                    base.skillLocator.setCooldownTime(this.getSkillDef().skillID, Time.time - PantheraConfig.Leap_targetReduction1);
                else if (leapLevel == 2)
                    base.skillLocator.setCooldownTime(this.getSkillDef().skillID, Time.time - PantheraConfig.Leap_targetReduction2);
                else if (leapLevel == 3)
                    base.skillLocator.setCooldownTime(this.getSkillDef().skillID, Time.time - PantheraConfig.Leap_targetReduction3);
            }
            else
            {
                base.skillLocator.startCooldown(this.getSkillDef().skillID);
            }

            // Spawn the Leap cercle //
            //if (base.pantheraObj.actualLeapCerle != null)
            //{
            //	GameObject.Destroy(base.pantheraObj.actualLeapCerle.gameObject, PantheraConfig.LeapCerle_delayBeforeDestroyed);
            //	base.pantheraObj.actualLeapCerle.destroying = true;
            //	base.pantheraObj.actualLeapCerle = null;
            //}
            //Vector3 leapCerclePosition = base.characterBody.footPosition;
            //leapCerclePosition += Vector3.up;
            //GameObject effect = Utils.Functions.SpawnEffect(base.gameObject, Assets.LeapCercleFX, leapCerclePosition, PantheraConfig.Model_generalScale, null, Util.QuaternionSafeLookRotation(player.transform.localRotation.eulerAngles), false);
            //this.pantheraObj.actualLeapCerle = effect.GetComponent<LeapCercleComponent>();
            //this.pantheraObj.actualLeapCerle.ptraObj = this.pantheraObj;
            //if(NetworkServer.active == false) new ClientCreateLeapCercleFX(base.gameObject, leapCerclePosition).Send(NetworkDestination.Clients);

        }

        public override void Update()
        {

        }

        public override void FixedUpdate()
        {

            // Get the duration //
            float totalDuration = Time.time - this.startingTime;

            // Stop if the leap is too long //
            if (totalDuration >= this.maximumDuration)
            {
                base.machine.EndScript();
                return;
            }

            // If there is a target //
            if (this.targetFound == true && this.targetHit == false)
            {
                base.characterDirection.forward = this.leapDirection;
                Vector3 charPos = characterBody.corePosition;
                Vector3 targetPos = targetCollider != null ? this.targetCollider.ClosestPoint(charPos) : this.targetPosition;
                Vector3 relativePos = targetPos - charPos;
                this.lastDirection = relativePos.normalized;
                base.characterMotor.velocity = relativePos.normalized * this.moveSpeed * PantheraConfig.Leap_targetSpeedMultiplier;
            }
            else
            {
                base.characterMotor.velocity = this.originalVelocity;
                this.originalVelocity = new Vector3(this.originalVelocity.x, this.originalVelocity.y - PantheraConfig.Leap_naturalDown, this.originalVelocity.z);
            }

            // Stop if the character hit the ground //
            if (totalDuration >= PantheraConfig.Leap_minimumDuration && base.characterMotor.Motor.GroundingStatus.IsStableOnGround)
            {
                base.machine.EndScript();
                return;
            }

            // Check if the Target can be hit //
            if (this.targetHit == false && this.targetFound == true)
            {
                Collider[] colliders = Physics.OverlapSphere(base.characterBody.corePosition + base.characterDirection.forward * PantheraConfig.Leap_leapStopDistance, PantheraConfig.Leap_leapScanRadius, LayerIndex.entityPrecise.mask.value);
                foreach (Collider collider in colliders)
                {
                    HurtBox hurtbox = collider.GetComponent<HurtBox>();
                    if (hurtbox != null && hurtbox.healthComponent != null && this.targetBody != null &&
                        this.targetBody.healthComponent != null && hurtbox.healthComponent == this.targetBody.healthComponent)
                    {
                        this.OnTargetHit();
                        Passives.Stealth.DidDamageUnstealth(base.pantheraObj);
                        return;
                    }
                }
            }

            // Smash the target to the ground //
            if (this.targetHit == true && this.targetFound == true)
            {
                base.characterMotor.velocity = base.characterDirection.forward * this.moveSpeed;
                base.characterMotor.velocity.y = 0 - PantheraConfig.Leap_downwardMultiplier * this.moveSpeed;
            }

        }

        public void OnTargetHit()
        {

            // Return if the Target has no Body //
            if (this.targetBody == null || this.targetRigidBody == null)
            {
                base.machine.EndScript();
                return;
            }

            // A Target was hit, set targetHit to true //
            this.targetHit = true;

            // Stop if the Target is on the ground //
            CharacterMotor motor = (CharacterMotor)this.targetRigidBody.GetComponent(typeof(CharacterMotor));
            if (motor != null && motor.isGrounded == true)
            {
                base.machine.EndScript();
                return;
            }

            // Stop if the target is not flying //
            if (this.targetBody.isFlying == false)
            {
                base.machine.EndScript();
                return;
            }

            // Set the character air control //
            base.characterMotor.airControl = PantheraConfig.Leap_airControlTarget;

            // Attach the component //
            if (this.targetBody.GetComponent<HoldTarget>() == null)
            {
                float relativeDistance = PantheraConfig.Leap_leapStopDistance + Vector3.Distance(this.targetCollider.ClosestPoint(base.characterBody.corePosition), this.targetBody.corePosition);
                HoldTarget comp = this.targetBody.gameObject.AddComponent<HoldTarget>();
                comp.skillScript = this;
                comp.ptraObj = pantheraObj;
                comp.relativeDistance = relativeDistance;
            }

        }

        public override void Stop()
        {

            // Detach the Component //
            if (this.targetBody != null && this.targetBody.GetComponent<HoldTarget>() != null)
                this.targetBody.GetComponent<HoldTarget>().SetToDestroy();

            // Set the previous air control //
            base.characterMotor.airControl = this.previousAirControl;

            // Stop the velocity //
            if (this.targetHit == true)
            {
                base.characterMotor.velocity = Vector3.zero;
            }

            // Disable the Trail //
            base.pantheraFX.SetLeapTrailFX(false);

            // Make the character run after the jump //
            base.pantheraObj.pantheraMotor.startSprint = true;

        }

    }

}