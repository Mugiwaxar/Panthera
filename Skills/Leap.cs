using EntityStates;
using KinematicCharacterController;
using Panthera.Components;
using Panthera.GUI;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
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

namespace Panthera.Skills
{
	class Leap : MachineScript
	{

		public static PantheraSkill SkillDef;

        public Vector3 leapDirection;
		public Vector3 lastDirection;
		public Collider playerCollider;
		public float maximumDuration = 1f;
		public float moveSpeed;
        public float previousAirControl;

		public bool targetFound = false;
		public Vector3 targetPosition = Vector3.zero;
        public CharacterBody targetBody;
        public Collider targetCollider;
        public Rigidbody targetRigidBody;
        public bool targetHit = false;
		public bool wasElite = false;

		public float reachDistance;
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
			skill.name = Tokens.LeapSkillName;
			skill.desc = Tokens.LeapSkillDesc;
			skill.icon = Assets.Leap;
			skill.iconPrefab = ConfigPanel.ActiveSkillPrefab;
			skill.type = PantheraSkill.SkillType.active;
			skill.associatedSkill = typeof(Leap);
            skill.priority = PantheraConfig.Leap_priority;
            skill.interruptPower = PantheraConfig.Leap_interruptPower;
            skill.cooldown = PantheraConfig.Leap_coolDown;
			skill.requiredEnergy = PantheraConfig.Leap_requiredEnergy;

			// Save this Skill //
			SkillDef = skill;
			PantheraSkill.SkillDefsList.Add(skill.skillID, skill);
		}

        public override PantheraSkill getSkillDef()
        {
            return SkillDef;
        }

        public override bool CanBeUsed(PantheraObj ptraObj)
        {
			if (ptraObj.characterBody.energy < SkillDef.requiredEnergy) return false;
			if (Time.time - PantheraSkill.GetCooldownTime(SkillDef.skillID) < SkillDef.cooldown) return false;
			return true;
		}

		public override void Start()
		{

			// Save the time //
			PantheraSkill.SetCooldownTime(SkillDef.skillID, Time.time);
			this.startingTime = Time.time;

			// Set the Air control //
			this.previousAirControl = base.characterMotor.airControl;
			base.characterMotor.airControl = PantheraConfig.Leap_airControl;

            // Set the jump count to zero //
            //base.characterMotor.jumpCount = 0;

			// Save the collider //
			this.playerCollider = base.GetComponent<Collider>();

			// Set the move speed //
			this.moveSpeed = Math.Min(PantheraConfig.Leap_maxMoveSpeed, this.moveSpeedStat * PantheraConfig.Leap_speedMultiplier);
			float minimumY = PantheraConfig.Leap_minimumY;

            // Get where to leap //
            Vector3 direction = base.GetAimRay().direction;
            HuntressTracker tracker = base.characterBody.GetComponent<HuntressTracker>();
            HurtBox target = tracker.GetTrackingTarget();

            // If there is a target //
            if (target != null && this.pantheraObj.holdedPrey == null)
			{

				// Setup the target //
				this.targetFound = true;
				this.targetPosition = target.transform.position;
				this.targetBody = target.healthComponent?.GetComponent<CharacterBody>();
				this.targetRigidBody = this.targetBody.GetComponent<Rigidbody>();
				this.targetCollider = this.targetBody.GetComponent<Collider>();

				// Check the Target Body //
				if (this.targetBody != null)
				{
                    // Save elite state //
                    this.wasElite = this.targetBody.isElite;

                    // Set elite to false //
                    new ServerSetElite(this.targetBody.gameObject, false).Send(NetworkDestination.Server);
                }

				// Set a larger duration //
				this.maximumDuration = 5;

				// Set the air control to 0 //
				base.characterMotor.airControl = 0;

				// Get the direction //
				minimumY = PantheraConfig.Leap_minimumYTarget;
				Vector3 charPos = base.characterBody.corePosition;
				Vector3 targetPos = this.targetCollider == null ? this.targetCollider.ClosestPoint(charPos) : this.targetPosition;
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
			Vector3 totalVelocity = upVelocity + forwardVelocity  * this.moveSpeed;
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

			// Stop if the character hit the ground //
			if (totalDuration >= PantheraConfig.Leap_minimumDuration && base.characterMotor.Motor.GroundingStatus.IsStableOnGround)
			{
				finishAttack();
				return;
			}

			// Stop if the leap is too long //
			if (totalDuration >= this.maximumDuration)
			{
				finishAttack();
				return;
			}

			// Check if the target was hit //
			//if (this.targetHit == false && this.targetFound == true)
			//{
   //             Vector3 targetPos = this.targetCollider == null ? this.targetCollider.ClosestPoint(base.characterBody.corePosition) : this.targetPosition;
   //             if (Vector3.Distance(base.characterBody.corePosition, targetPos) <= PantheraConfig.Leap_leapStopDistance)
			//	{
   //                 OnTargetHit();
   //                 return;
   //             }
			//}

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
						OnTargetHit();
						return;
					}
				}
			}

			// Smash the target to the ground //
			if (this.targetHit == true && this.targetFound == true)
			{
				base.characterMotor.velocity = this.characterDirection.forward * this.moveSpeed;
				base.characterMotor.velocity.y = 0 - PantheraConfig.Leap_downwardMultiplier * this.moveSpeed;
			}

		}

		public void OnTargetHit()
		{

            // Return if the Target has no Body //
            if (this.targetBody == null || this.targetRigidBody == null)
            {
                finishAttack();
                return;
            }

            // Save the distance //
            this.reachDistance = PantheraConfig.Leap_leapStopDistance + Vector3.Distance(this.targetCollider.ClosestPoint(base.characterBody.corePosition), this.targetBody.corePosition);

			// A Target was hit, set targetHit to true //
			this.targetHit = true;

			// Stop if the Target is on the ground //
			CharacterMotor motor = (CharacterMotor)this.targetRigidBody.GetComponent(typeof(CharacterMotor));
			if (motor != null && motor.isGrounded == true)
			{
				finishAttack();
				return;
			}

			// Stop if the target is not flying //
			if (this.targetBody.isFlying == false)
			{
				finishAttack();
				return;
			}

			// Set the character air control //
			this.characterMotor.airControl = PantheraConfig.Leap_airControlTarget;

			// Attach the component //
			GameObject obj = this.targetBody.gameObject;
            if (obj.GetComponent<HoldTarget>() == null)
            {
                HoldTarget comp = obj.AddComponent<HoldTarget>();
                float relativeDistance = reachDistance;
                comp.skillScript = this;
                comp.ptraObj = base.pantheraObj;
                comp.relativeDistance = relativeDistance;
            }

        }


		public void finishAttack()
		{

			//// Start Furious Bite //
			//if (base.inputBank.IsKeyPressed(PantheraConfig.Skill1Key) && this.target != null && this.targetRigidBody != null && Utils.Functions.getCollideDistance(base.rigidbody, target.rigidbody) < PantheraConfig.FuriousBite_maxDistanceToActivate)
			//{
   //             this.machine.SetScript(new FuriousBite { target = this.target, reachDistance = this.reachDistance });
   //             return;
   //         }

   //         // Start Mangle //
   //         if (base.isAuthority == true && base.inputBank.IsKeyPressed(PantheraConfig.Skill2Key) && this.target != null && this.targetRigidBody != null && Utils.Functions.getCollideDistance(base.rigidbody, target.rigidbody) < PantheraConfig.Mangle_maxDistanceToActivate)
   //         {
   //             this.machine.SetScript(new Mangle { target = this.target });
   //             return;
   //         }

			// Stop the script //
            this.machine.EndScript();

		}

		public override void Stop()
		{

			// Set the previous air control //
			base.characterMotor.airControl = this.previousAirControl;

			// Stop the velocity and set elite back //
			if (this.targetBody != null)
			{
				base.characterMotor.velocity = Vector3.zero;
				new ServerSetElite(this.targetBody.gameObject, this.wasElite).Send(NetworkDestination.Server);
			}

			// Disable the Trail //
			base.pantheraFX.SetLeapTrailFX(false);

			// Make the character run after the jump //
			base.pantheraObj.pantheraMotor.startSprint = true;

		}

	}

}