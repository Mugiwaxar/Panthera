using Panthera.MachineScripts;

namespace Panthera.OldSkills
{
    public class SaveMyFriend : MachineScript
    {

        //public float startingTime;
        //public float moveSpeed;
        //public float previousAirControl;
        //public CharacterBody targetBody;
        //public Collider targetCollider;
        //public Vector3 startingVelocity;
        //public Vector3 originalVelocity;
        //public Vector3 leapDirection;
        //public Vector3 lastDirection;
        //public bool reachedTarget;

        //public static void Create()
        //{
        //    // Create the Skill //
        //    PantheraSkill skill = new PantheraSkill();
        //    skill.skillID = PantheraConfig.SaveMyFriend_SkillID;
        //    skill.name = "SAVE_MY_FRIEND_ABILITY_NAME";
        //    skill.desc = "SAVE_MY_FRIEND_ABILITY_DESC";
        //    skill.icon = PantheraAssets.SaveMyFriendAbility;
        //    skill.iconPrefab = PantheraAssets.HybridSkillPrefab;
        //    skill.type = PantheraSkill.SkillType.hybrid;
        //    skill.associatedSkill = typeof(SaveMyFriend);
        //    skill.priority = PantheraConfig.SaveMyFriend_priority;
        //    skill.interruptPower = PantheraConfig.SaveMyFriend_interruptPower;
        //    skill.cooldown = PantheraConfig.SaveMyFriend_cooldown;

        //    // Save this Skill //
        //    PantheraSkill.SkillDefsList.Add(skill.skillID, skill);
        //}

        ////public override PantheraSkill getSkillDef()
        ////{
        ////    return base.pantheraObj.activePreset.getSkillByID(PantheraConfig.SaveMyFriend_SkillID);
        ////}

        //public override bool CanBeUsed(PantheraObj ptraObj)
        //{
        //    pantheraObj = ptraObj;
        //    if (ptraObj.skillLocator.getCooldown(getSkillDef().skillID) > 0) return false;
        //    return true;
        //}

        //public override void Start()
        //{

        //    // Save the time //
        //    startingTime = Time.time;

        //    // Save the Stating Velocity //
        //    startingVelocity = characterMotor.velocity;

        //    // Set the Air control //
        //    previousAirControl = characterMotor.airControl;
        //    characterMotor.airControl = PantheraConfig.Leap_airControl;

        //    // Set the move speed //
        //    moveSpeed = Math.Min(PantheraConfig.Leap_maxMoveSpeed, moveSpeedStat * PantheraConfig.Leap_speedMultiplier);
        //    float minimumY = PantheraConfig.Leap_minimumY;

        //    // Get where to leap //
        //    Vector3 direction = GetAimRay().direction;
        //    HuntressTracker tracker = characterBody.GetComponent<HuntressTracker>();
        //    HurtBox target = tracker.GetTrackingTarget();

        //    // Stop if no Target //
        //    if (target == null || target.healthComponent == null || target.healthComponent.GetComponent<CharacterBody>() == null)
        //    {
        //        machine.EndScript();
        //        return;
        //    }

        //    // Check the Team //
        //    if (target.teamIndex != TeamIndex.Player)
        //    {
        //        machine.EndScript();
        //        return;
        //    }

        //    // Setup the target //
        //    targetBody = target.healthComponent.GetComponent<CharacterBody>();
        //    targetCollider = targetBody.GetComponent<Collider>();

        //    // Set the air control to 0 //
        //    characterMotor.airControl = 0;

        //    // Get the direction //
        //    minimumY = PantheraConfig.Leap_minimumYTarget;
        //    Vector3 charPos = characterBody.corePosition;
        //    Vector3 targetPos = targetCollider.ClosestPoint(charPos);
        //    Vector3 relativePos = targetPos - charPos;
        //    direction = relativePos.normalized;

        //    // Make the Character Sprint //
        //    pantheraObj.pantheraMotor.isSprinting = true;

        //    // Calculate the Velocity //
        //    direction.y *= PantheraConfig.Leap_aimRayYMultiplier;
        //    direction.y = Mathf.Max(direction.y, minimumY);
        //    Vector3 upVelocity = new Vector3(0, direction.y, 0) * PantheraConfig.Leap_upwardVelocity;
        //    Vector3 forwardVelocity = new Vector3(direction.x, 0, direction.z);
        //    Vector3 totalVelocity = upVelocity + forwardVelocity * moveSpeed;
        //    characterMotor.Motor.ForceUnground();
        //    characterMotor.velocity = totalVelocity;
        //    originalVelocity = totalVelocity;

        //    // Set the move vector //
        //    characterDirection.forward = direction;
        //    leapDirection = direction;

        //    // Play the sound // 
        //    Sound.playSound(Sound.Leap, gameObject);

        //    // Enable the Trail //
        //    pantheraFX.setLeapTrailFX(true);

        //    // Set the Cooldown //
        //    skillLocator.startCooldown(getSkillDef().skillID);

        //}

        //public override void Update()
        //{

        //}

        //public override void FixedUpdate()
        //{

        //    // Get the duration //
        //    float totalDuration = Time.time - startingTime;

        //    // Stop if the leap is too long //
        //    if (totalDuration >= PantheraConfig.SaveMyFriend_skillMaxDuration)
        //    {
        //        machine.EndScript();
        //        return;
        //    }

        //    // Stop if the Target is dead or not exist anymore //
        //    if (targetBody != null && targetBody.healthComponent.alive == false)
        //    {
        //        machine.EndScript();
        //        return;
        //    }
        //    else
        //    {
        //        characterDirection.forward = leapDirection;
        //        Vector3 charPos = characterBody.corePosition;
        //        Vector3 targetPos = targetCollider.ClosestPoint(charPos);
        //        Vector3 relativePos = targetPos - charPos;
        //        lastDirection = relativePos.normalized;
        //        characterMotor.velocity = relativePos.normalized * moveSpeed * PantheraConfig.Leap_targetSpeedMultiplier;
        //    }

        //    // Check if the Target is reached //
        //    if (reachedTarget == false)
        //    {
        //        Collider[] colliders = Physics.OverlapSphere(pantheraObj.findModelChild("Mouth").position, PantheraConfig.Leap_leapScanRadius, LayerIndex.entityPrecise.mask.value);
        //        foreach (Collider collider in colliders)
        //        {
        //            HurtBox hurtbox = collider.GetComponent<HurtBox>();
        //            if (hurtbox != null && hurtbox.healthComponent != null && targetBody != null &&
        //                targetBody.healthComponent != null && hurtbox.healthComponent == targetBody.healthComponent)
        //            {
        //                // Attach the component //
        //                GameObject obj = targetBody.gameObject;
        //                if (obj.GetComponent<HoldTarget>() == null)
        //                {
        //                    Collider playerCollider = gameObject.GetComponent<Collider>();
        //                    float relativeDistance = Vector3.Distance(collider.ClosestPoint(pantheraObj.findModelChild("Mouth").position), targetBody.corePosition);
        //                    HoldTarget comp = obj.AddComponent<HoldTarget>();
        //                    comp.skillScript = this;
        //                    comp.ptraObj = pantheraObj;
        //                    comp.relativeDistance = relativeDistance;
        //                    comp.isAlly = true;
        //                    reachedTarget = true;
        //                    pantheraObj.StartCoroutine(DestroyComp(obj));
        //                }
        //                machine.EndScript();
        //                return;
        //            }
        //        }
        //    }

        //}

        //public override void Stop()
        //{

        //    // Set the previous air control //
        //    characterMotor.airControl = previousAirControl;

        //    // Disable the Trail //
        //    pantheraFX.setLeapTrailFX(false);

        //    // Make the character run after the jump //
        //    pantheraObj.pantheraMotor.isSprinting = true;

        //}

        //public static IEnumerator DestroyComp(GameObject obj)
        //{
        //    yield return new WaitForSeconds(PantheraConfig.SaveMyFriend_compMaxDuration);
        //    if (obj != null && obj.GetComponent<HoldTarget>())
        //        obj.GetComponent<HoldTarget>().SetToDestroy();
        //}

    }

}
