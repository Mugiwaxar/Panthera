namespace Panthera.OldSkills
{
    //class RaySlash : MachineScript
    //{

    //    public static GameObject projectile = PantheraAssets.RaySlashProjectile;

    //    public FireProjectileInfo projectileInfo;
    //    public float startTime;
    //    public float chargeTime;
    //    public float releaseTime;
    //    public float lastStockRemove;
    //    public bool chargeTimeSet = false;
    //    public bool releaseTimeSet = false;
    //    public bool firstStart = true;
    //    public bool soundPlayed = false;
    //    public bool hasFired = false;
    //    public bool fire = false;
    //    public GameObject effect;

    //    public RaySlash()
    //    {
    //        //priority = PantheraConfig.RaySlash_priority;
    //        //interruptPower = PantheraConfig.RaySlash_interruptPower;
    //    }

    //    public override bool CanBeUsed(PantheraObj ptraObj)
    //    {
    //        return true;
    //    }

    //    public override void Start()
    //    {

    //        // Set the start time //
    //        startTime = Time.time;

    //        // Start the effect //
    //        effect = Utils.Functions.SpawnEffect(
    //                base.gameObject,
    //                PantheraAssets.RaySlashChargeFX,
    //                this.modelTransform.position,
    //                1,
    //                this.modelTransform.gameObject
    //                );

    //        // Create the Projectile //
    //        projectileInfo.projectilePrefab = projectile;
    //        projectileInfo.damageTypeOverride = DamageType.Generic;
    //        projectileInfo.damageColorIndex = DamageColorIndex.Default;
    //        projectileInfo.crit = RollCrit();
    //        projectileInfo.force = PantheraConfig.RaySlash_projectileForce;
    //        projectileInfo.damage = PantheraConfig.RaySlash_damageMultiplier * damageStat;
    //        projectileInfo.speedOverride = PantheraConfig.RaySlash_projectileSpeed;
    //        projectileInfo.useSpeedOverride = true;
    //        projectileInfo.owner = gameObject;
    //        projectileInfo.position = characterBody.corePosition;
    //        projectileInfo.rotation = Util.QuaternionSafeLookRotation(GetAimRay().direction);

    //        // Prepare the animator //
    //        Animator animator = modelAnimator;
    //        animator.SetBool("RaySlashFire", false);
    //        animator.SetBool("RaySlashStopWaiting", false);

    //    }

    //    public override void Update()
    //    {

    //    }

    //    public override void FixedUpdate()
    //    {
    //        float duration = Time.time - startTime;
    //        float lastStockRemoved = Time.time - lastStockRemove;

    //        // Wait before continuing //
    //        if (duration <= PantheraConfig.RaySlash_initialWaiting)
    //        {
    //            return;
    //        }

    //        // Slow the character //
    //        characterMotor.velocity = Vector3.zero;

    //        // Play the animation //
    //        if (firstStart == true)
    //        {
    //            firstStart = false;
    //            PlayAnimation("Gesture", "RaySlashPreparing");

    //            // Set the charge to on //
    //            pantheraObj.onRaySlashCharge = true;
    //            if (NetworkServer.active == false) new ServerSetRaySlashCharge(gameObject, true).Send(NetworkDestination.Server);

    //        }

    //        // Play the sound //
    //        if (soundPlayed == false)
    //        {
    //            //Utils.Sound.playSound(Utils.Sound.RaySlashChargeStart, gameObject);
    //            soundPlayed = true;
    //        }

    //        // Set the skill to charge //
    //        if (chargeTimeSet == false)
    //        {

    //            chargeTimeSet = true;
    //            chargeTime = Time.time;

    //        }

    //        // Remove one stock //
    //        if (lastStockRemoved > PantheraConfig.RaySlash_timeNeededToRemoveAStock && releaseTimeSet == false)
    //        {
    //            lastStockRemove = Time.time;
    //            skillLocator.special.stock -= 1;
    //        }

    //        // Check if the key is still down //
    //        if (inputBank.IsKeyPressed(PantheraConfig.Skill4Key) && skillLocator.special.stock > 0)
    //        {
    //            return;
    //        }

    //        // Wait the charge before continuing //
    //        if (duration <= PantheraConfig.RaySlash_chargeMinWaiting)
    //        {
    //            return;
    //        }

    //        // Set the skill to release //
    //        if (releaseTimeSet == false)
    //        {
    //            releaseTimeSet = true;
    //            releaseTime = Time.time;

    //            // Set the charge to off //
    //            pantheraObj.onRaySlashCharge = false;
    //            if (NetworkServer.active == false) new ServerSetRaySlashCharge(gameObject, false).Send(NetworkDestination.Server);

    //            // Set the next animation //
    //            if (inputBank.IsKeyPressed(PantheraConfig.Skill1Key) && characterBody.GetBuffCount(Buff.raySlashBuff) >= PantheraConfig.RaySlash_buffRequiredToFire)
    //            {
    //                modelAnimator.SetBool("RaySlashFire", true);
    //                fire = true;
    //            }
    //            else
    //            {
    //                modelAnimator.SetBool("RaySlashStopWaiting", true);
    //                EndScript();
    //                return;
    //            }

    //        }

    //        // Wait before firing //
    //        if (duration <= PantheraConfig.RaySlash_startWaiting + (releaseTime - chargeTime))
    //        {
    //            return;
    //        }

    //        // Fire the projectile //
    //        if (hasFired == false && fire == true)
    //        {
    //            hasFired = true;
    //            new ServerSetBuffCount(gameObject, (int)Buff.raySlashBuff.buffIndex, 0).Send(NetworkDestination.Server);
    //            Vector3 direction = new Vector3(characterDirection.forward.x, GetAimRay().direction.y, characterDirection.forward.z);
    //            projectileInfo.rotation = Util.QuaternionSafeLookRotation(direction);
    //            ProjectileManager.instance.FireProjectile(projectileInfo);
    //            //Utils.Sound.playSound(Utils.Sound.RaySlashStart, gameObject);
    //            //Utils.Sound.playSound(Utils.Sound.RaySlashRoar, gameObject);
    //        }

    //        // Wait before the end //
    //        if (duration <= PantheraConfig.RaySlash_endWaiting + (releaseTime - chargeTime))
    //        {
    //            return;
    //        }

    //        EndScript();

    //    }

    //    public override void Stop()
    //    {

    //        // Stop the sound //
    //        //Utils.Sound.playSound(Utils.Sound.RaySlashChargeStop, gameObject);

    //        // Stop the effect //
    //        UnityEngine.Object.DestroyImmediate(effect);

    //        // Set the charge to off //
    //        pantheraObj.onRaySlashCharge = false;
    //        if (NetworkServer.active == false) new ServerSetRaySlashCharge(gameObject, false).Send(NetworkDestination.Server);

    //    }

    //}
}