using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.MachineScripts;
using Panthera.Utils;
using RoR2;
using RoR2.Skills;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.SkillsHybrid
{
    internal class SaveMyFriend : MachineScript
    {

        public float startingTime;
        public float moveSpeed;
        public float previousAirControl;
        public CharacterBody targetBody;
        public Collider targetCollider;
        public Vector3 startingVelocity;
        public Vector3 originalVelocity;
        public Vector3 leapDirection;
        public Vector3 lastDirection;
        public bool reachedTarget;

        public static void Create()
        {
            // Create the Skill //
            PantheraSkill skill = new PantheraSkill();
            skill.skillID = PantheraConfig.SaveMyFriend_SkillID;
            skill.name = "SAVE_MY_FRIEND_ABILITY_NAME";
            skill.desc = "SAVE_MY_FRIEND_ABILITY_DESC";
            skill.icon = Assets.SaveMyFriendAbility;
            skill.iconPrefab = Assets.HybridSkillPrefab;
            skill.type = PantheraSkill.SkillType.hybrid;
            skill.associatedSkill = typeof(SaveMyFriend);
            skill.priority = PantheraConfig.SaveMyFriend_priority;
            skill.interruptPower = PantheraConfig.SaveMyFriend_interruptPower;
            skill.cooldown = PantheraConfig.SaveMyFriend_cooldown;

            // Save this Skill //
            PantheraSkill.SkillDefsList.Add(skill.skillID, skill);
        }

        public override PantheraSkill getSkillDef()
        {
            return base.pantheraObj.activePreset.getSkillByID(PantheraConfig.SaveMyFriend_SkillID);
        }

        public override bool CanBeUsed(PantheraObj ptraObj)
        {
            base.pantheraObj = ptraObj;
            if (ptraObj.skillLocator.getCooldownInSecond(this.getSkillDef().skillID) > 0) return false;
            return true;
        }

        public override void Start()
        {

            // Save the time //
            this.startingTime = Time.time;

            // Save the Stating Velocity //
            this.startingVelocity = base.characterMotor.velocity;

            // Set the Air control //
            this.previousAirControl = base.characterMotor.airControl;
            base.characterMotor.airControl = PantheraConfig.Leap_airControl;

            // Set the move speed //
            this.moveSpeed = Math.Min(PantheraConfig.Leap_maxMoveSpeed, base.moveSpeedStat * PantheraConfig.Leap_speedMultiplier);
            float minimumY = PantheraConfig.Leap_minimumY;

            // Get where to leap //
            Vector3 direction = base.GetAimRay().direction;
            HuntressTracker tracker = base.characterBody.GetComponent<HuntressTracker>();
            HurtBox target = tracker.GetTrackingTarget();

            // Stop if no Target //
            if (target == null || target.healthComponent == null || target.healthComponent.GetComponent<CharacterBody>() == null)
            {
                base.machine.EndScript();
                return;
            }

            // Check the Team //
            if (target.teamIndex != TeamIndex.Player)
            {
                base.machine.EndScript();
                return;
            }

            // Setup the target //
            this.targetBody = target.healthComponent.GetComponent<CharacterBody>();
            this.targetCollider = this.targetBody.GetComponent<Collider>();

            // Set the air control to 0 //
            this.characterMotor.airControl = 0;

            // Get the direction //
            minimumY = PantheraConfig.Leap_minimumYTarget;
            Vector3 charPos = base.characterBody.corePosition;
            Vector3 targetPos = this.targetCollider.ClosestPoint(charPos);
            Vector3 relativePos = targetPos - charPos;
            direction = relativePos.normalized;

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

            // Set the Cooldown //
            base.skillLocator.startCooldown(this.getSkillDef().skillID);

        }

        public override void Update()
        {

        }

        public override void FixedUpdate()
        {

            // Get the duration //
            float totalDuration = Time.time - this.startingTime;

            // Stop if the leap is too long //
            if (totalDuration >= PantheraConfig.SaveMyFriend_skillMaxDuration)
            {
                base.machine.EndScript();
                return;
            }

            // Stop if the Target is dead or not exist anymore //
            if (this.targetBody != null && this.targetBody.healthComponent.alive == false)
            {
                base.machine.EndScript();
                return;
            }
            else
            {
                base.characterDirection.forward = this.leapDirection;
                Vector3 charPos = characterBody.corePosition;
                Vector3 targetPos = this.targetCollider.ClosestPoint(charPos);
                Vector3 relativePos = targetPos - charPos;
                this.lastDirection = relativePos.normalized;
                base.characterMotor.velocity = relativePos.normalized * this.moveSpeed * PantheraConfig.Leap_targetSpeedMultiplier;
            }

            // Check if the Target is reached //
            if (this.reachedTarget == false)
            {
                Collider[] colliders = Physics.OverlapSphere(base.pantheraObj.findModelChild("Mouth").position, PantheraConfig.Leap_leapScanRadius, LayerIndex.entityPrecise.mask.value);
                foreach (Collider collider in colliders)
                {
                    HurtBox hurtbox = collider.GetComponent<HurtBox>();
                    if (hurtbox != null && hurtbox.healthComponent != null && this.targetBody != null &&
                        this.targetBody.healthComponent != null && hurtbox.healthComponent == this.targetBody.healthComponent)
                    {
                        // Attach the component //
                        GameObject obj = this.targetBody.gameObject;
                        if (obj.GetComponent<HoldTarget>() == null)
                        {
                            Collider playerCollider = base.gameObject.GetComponent<Collider>();
                            float relativeDistance = Vector3.Distance(collider.ClosestPoint(base.pantheraObj.findModelChild("Mouth").position), this.targetBody.corePosition);
                            HoldTarget comp = obj.AddComponent<HoldTarget>();
                            comp.skillScript = this;
                            comp.ptraObj = pantheraObj;
                            comp.relativeDistance = relativeDistance;
                            comp.isAlly = true;
                            this.reachedTarget = true;
                            base.pantheraObj.StartCoroutine(DestroyComp(obj));
                        }
                        base.machine.EndScript();
                        return;
                    }
                }
            }

        }

        public override void Stop()
        {

            // Set the previous air control //
            base.characterMotor.airControl = this.previousAirControl;

            // Disable the Trail //
            base.pantheraFX.SetLeapTrailFX(false);

            // Make the character run after the jump //
            base.pantheraObj.pantheraMotor.startSprint = true;

        }

        public static IEnumerator DestroyComp(GameObject obj)
        {
            yield return new WaitForSeconds(PantheraConfig.SaveMyFriend_compMaxDuration);
            if (obj != null && obj.GetComponent<HoldTarget>())
                obj.GetComponent<HoldTarget>().SetToDestroy();
        }

    }

}
