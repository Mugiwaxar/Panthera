using EntityStates;
using KinematicCharacterController;
using Panthera;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.GUI;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.OldSkills;
using Panthera.Skills;
using Panthera.Skills.Actives;
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

namespace Panthera.Skills.Actives
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
            base.icon = PantheraAssets.LeapSkill;
            base.name = PantheraTokens.Get("skill_LeapName");
            base.baseCooldown = PantheraConfig.Leap_cooldown;
            base.desc1 = PantheraTokens.Get("skill_LeapDesc");
            base.desc2 = null;
            base.removeStealth = false;
            base.skillID = PantheraConfig.Leap_SkillID;
            base.priority = PantheraConfig.Leap_priority;
            base.interruptPower = PantheraConfig.Leap_interruptPower;
        }

        public override bool CanBeUsed(PantheraObj ptraObj)
        {
            //if (ptraObj.characterBody.stamina < PantheraConfig.Leap_requiredStamina) return false;
            if (ptraObj.skillLocator.getStock(PantheraConfig.Leap_SkillID) <= 0) return false;
            return true;
        }

        public override void Start()
        {

            // Save the time //
            this.startingTime = Time.time;

            // Remove the Stamina //
            //base.characterBody.stamina -= PantheraConfig.Leap_requiredStamina;

            // Set the Air control //
            this.previousAirControl = base.characterMotor.airControl;
            base.characterMotor.airControl = PantheraConfig.Leap_airControl;

            // Set the Jump count to zero //
            base.characterMotor.jumpCount = 0;

            // Remove the Frozen Paws Buff //
            if (base.characterBody.HasBuff(Base.Buff.FrozenPawsBuff) == true)
                new ServerClearTimedBuffs(base.gameObject, Base.Buff.FrozenPawsBuff.index).Send(NetworkDestination.Server);

            // Save the collider //
            this.playerCollider = GetComponent<Collider>();

            // Set the move speed //
            this.moveSpeed = Math.Min(PantheraConfig.Leap_maxMoveSpeed, moveSpeedStat * PantheraConfig.Leap_speedMultiplier);
            float minimumY = PantheraConfig.Leap_minimumY;

            // Get where to leap //
            Vector3 direction = GetAimRay().direction;
            HuntressTracker tracker = GetComponent<HuntressTracker>();
            HurtBox target = tracker.GetTrackingTarget();

            // If there is a target //
            if (target != null)
            {

                // Setup the target //
                this.targetFound = true;
                this.targetPosition = target.transform.position;
                this.targetBody = target.healthComponent?.GetComponent<CharacterBody>();
                this.targetRigidBody = this. targetBody.GetComponent<Rigidbody>();
                this.targetCollider = this.targetBody.GetComponent<Collider>();

                // Set the air control to 0 //
                base.characterMotor.airControl = 0;

                // Get the direction //
                minimumY = PantheraConfig.Leap_minimumYTarget;
                Vector3 charPos = base.characterBody.corePosition;
                Vector3 targetPos = this.targetCollider != null ? this.targetCollider.ClosestPoint(charPos) : this.targetPosition;
                Vector3 relativePos = targetPos - charPos;
                direction = relativePos.normalized;

            }

            // Make the Character Sprint //
            base.pantheraObj.pantheraMotor.isSprinting = true;

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
            base.pantheraFX.setLeapTrailFX(true);

            // Apply the Cooldown //
            float cooldown = this.baseCooldown;
            if (this.targetFound == true && base.pantheraObj.getAbilityLevel(PantheraConfig.RelentlessStalker_AbilityID) > 0)
                cooldown -= PantheraConfig.RelentlessStalker_CooldownReduction;
            base.skillLocator.startCooldown(PantheraConfig.Leap_SkillID, cooldown);

            // Set the Fake Skill Cooldown //
            //skillLocator.utility.DeductStock(1);

            // Launch the Fake Skill //
            base.characterBody.OnSkillActivated(base.skillLocator.utility);

            //if (this.targetFound == true)
            //{

            //    int leapLevel = CharacterAbilities.getAbilityLevel(PantheraConfig.LeapAbilityID);

            //    if (leapLevel == 0)
            //        skillLocator.setCooldownTime(getSkillDef().skillID, Time.time);
            //    else if (leapLevel == 1)
            //        skillLocator.setCooldownTime(getSkillDef().skillID, Time.time - PantheraConfig.Leap_targetReduction1);
            //    else if (leapLevel == 2)
            //        skillLocator.setCooldownTime(getSkillDef().skillID, Time.time - PantheraConfig.Leap_targetReduction2);
            //    else if (leapLevel == 3)
            //        skillLocator.setCooldownTime(getSkillDef().skillID, Time.time - PantheraConfig.Leap_targetReduction3);
            //}
            //else
            //{
            //    skillLocator.startCooldown(getSkillDef().skillID);
            //}

            // Spawn the Leap cercle //
            //if (base.pantheraObj.actualLeapCerle != null)
            //{
            //	GameObject.Destroy(base.pantheraObj.actualLeapCerle.gameObject, PantheraConfig.LeapCerle_delayBeforeDestroyed);
            //	base.pantheraObj.actualLeapCerle.destroying = true;
            //	base.pantheraObj.actualLeapCerle = null;
            //}
            //Vector3 leapCerclePosition = base.characterBody.footPosition;
            //leapCerclePosition += Vector3.up;
            //GameObject effect = Utils.Functions.SpawnEffect(base.gameObject, PantheraAssets.LeapCercleFX, leapCerclePosition, PantheraConfig.Model_generalScale, null, Util.QuaternionSafeLookRotation(player.transform.localRotation.eulerAngles), false);
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
                Vector3 charPos = base.characterBody.corePosition;
                Vector3 targetPos = this.targetCollider != null ? this.targetCollider.ClosestPoint(charPos) : this.targetPosition;
                Vector3 relativePos = targetPos - charPos;
                this.lastDirection = relativePos.normalized;
                base.characterMotor.velocity = relativePos.normalized * this.moveSpeed * PantheraConfig.Leap_targetSpeedMultiplier;
            }
            else if (this.targetFound == false)
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
            if (this.targetFound == true && this.targetHit == false)
            {
                Collider[] colliders = Physics.OverlapSphere(base.characterBody.corePosition + base.characterDirection.forward * PantheraConfig.Leap_leapStopDistance, PantheraConfig.Leap_leapScanRadius, LayerIndex.entityPrecise.mask.value);
                foreach (Collider collider in colliders)
                {
                    HurtBox hurtbox = collider.GetComponent<HurtBox>();
                    if (hurtbox != null && hurtbox.healthComponent != null && this.targetBody != null &&
                        this.targetBody.healthComponent != null && hurtbox.healthComponent == this.targetBody.healthComponent)
                    {
                        this.OnTargetHit();
                        //OldPassives.Stealth.DidDamageUnstealth(pantheraObj);
                        return;
                    }
                }
            }

            // Stop the Leap if the Velocity is Down //
            if (this.targetFound == false && base.characterMotor.velocity.y < 0)
            {
                base.machine.EndScript();
                return;
            }

            // Smash the target to the ground //
            //if (targetHit == true && targetFound == true)
            //{
            //    characterMotor.velocity = characterDirection.forward * moveSpeed;
            //    characterMotor.velocity.y = 0 - PantheraConfig.Leap_downwardMultiplier * moveSpeed;
            //}

        }

        public void OnTargetHit()
        {

            // Apply the Cryo-Leap Buff //
            int cryoLeapLevel = base.getAbilityLevel(PantheraConfig.CryoLeap_AbilityID);
            if (cryoLeapLevel > 0)
            {
                float frozenPawsDuration = 0;
                if (cryoLeapLevel == 1)
                    frozenPawsDuration = PantheraConfig.CryoLeap_duration1;
                else if (cryoLeapLevel == 2)
                    frozenPawsDuration = PantheraConfig.CryoLeap_duration2;
                else if (cryoLeapLevel == 3)
                    frozenPawsDuration = PantheraConfig.CryoLeap_duration3;
                new ServerAddBuff(base.gameObject, base.gameObject, Buff.FrozenPawsBuff, 1, frozenPawsDuration).Send(NetworkDestination.Server);
            }

            // Set Target as hit //
            this.targetHit = true;

            // End the Script //
            base.machine.EndScript();

            // Return if the Target has no Body //
            //if (this.targetBody == null || this.targetRigidBody == null)
            //{
            //    base.machine.EndScript();
            //    return;
            //}

            // A Target was hit, set targetHit to true //
            //this.targetHit = true;

            //// Stop if the Target is on the ground //
            //CharacterMotor motor = (CharacterMotor)this.targetRigidBody.GetComponent(typeof(CharacterMotor));
            //if (motor != null && motor.isGrounded == true)
            //{
            //    base.machine.EndScript();
            //    return;
            //}

            //// Stop if the target is not flying //
            //if (this.targetBody.isFlying == false)
            //{
            //    base.machine.EndScript();
            //    return;
            //}

            //// Set the character air control //
            //base.characterMotor.airControl = PantheraConfig.Leap_airControlTarget;

            //// Attach the component //
            //if (targetBody.GetComponent<HoldTarget>() == null)
            //{
            //    float relativeDistance = PantheraConfig.Leap_leapStopDistance + Vector3.Distance(targetCollider.ClosestPoint(characterBody.corePosition), targetBody.corePosition);
            //    HoldTarget comp = targetBody.gameObject.AddComponent<HoldTarget>();
            //    comp.skillScript = this;
            //    comp.ptraObj = pantheraObj;
            //    comp.relativeDistance = relativeDistance;
            //}

        }

        public override void Stop()
        {

            // Detach the Component //
            //if (targetBody != null && targetBody.GetComponent<HoldTarget>() != null)
            //    targetBody.GetComponent<HoldTarget>().SetToDestroy();

            // Set the previous air control //
            characterMotor.airControl = previousAirControl;

            // Stop the velocity //
            if (this.targetHit == true)
                base.characterMotor.velocity = Vector3.zero;

            // Disable the Trail //
            base.pantheraFX.setLeapTrailFX(false);

            // Make the character run after the jump //
            base.pantheraObj.pantheraMotor.isSprinting = true;

        }

    }

}