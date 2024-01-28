using Panthera;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.OldSkills;
using Panthera.Utils;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.OldSkills
{
    //class FrontRip : MachineScript
    //{

    //    public OverlapAttack attack;
    //    public float startTime;
    //    public bool hasFired = false;
    //    public Vector3 moveVector;
    //    public Vector3 previousVelocity;
    //    public bool grounded;

    //    public FrontRip()
    //    {
    //        //priority = PantheraConfig.Rip_priority;
    //        //interruptPower = PantheraConfig.Rip_interruptPower;
    //    }

    //    public override bool CanBeUsed(PantheraObj ptraObj)
    //    {
    //        return true;
    //    }

    //    public override void Start()
    //    {

    //        // Create the attack //
    //        attack = Functions.CreateOverlapAttack(gameObject, PantheraConfig.FrontRip_atkDamageMultiplier, PantheraConfig.FrontRip_hitboxName);

    //        // Set the character to forward //
    //        characterDirection.forward = GetAimRay().direction;

    //        // Save the time //
    //        startTime = Time.time;

    //        // Set the attack //
    //        StartAimMode(PantheraConfig.Rip_minimumAimTime + PantheraConfig.FrontRip_atkBaseDuration, false);
    //        characterBody.outOfCombatStopwatch = 0f;
    //        pantheraObj.pantheraMotor.startSprint = false;

    //        // Move the character //
    //        moveVector = Quaternion.Euler(0f, characterDirection.yaw, 0f) * Vector3.forward;
    //        moveVector *= PantheraConfig.FrontRip_forwardVelocityMultiplier;
    //        moveVector += characterMotor.velocity;
    //        previousVelocity = characterMotor.velocity;

    //        if (characterMotor.isGrounded == true) grounded = true;

    //    }

    //    public override void Update()
    //    {

    //    }

    //    public override void FixedUpdate()
    //    {

    //        // Stop if the duration is reached //
    //        float skillDuration = Time.time - startTime;
    //        if (skillDuration >= PantheraConfig.FrontRip_atkBaseDuration)
    //        {
    //            EndScript();
    //            return;
    //        }

    //        // Move the character //
    //        if (skillDuration < PantheraConfig.FrontRip_forwardVelocityTime && grounded == true)
    //        {
    //            characterMotor.velocity = moveVector;
    //        }

    //        // Check if the attack has already fired //
    //        if (hasFired == false)
    //        {

    //            // Fire the attack //
    //            List<HurtBox> enemiesHit = new List<HurtBox>();
    //            hasFired = true;
    //            attack.Fire(enemiesHit);
    //            Sound.playSound(Sound.Rip2, gameObject);
    //            PlayAnimation("Gesture", "FrontRipAtk");
    //            Utils.FXManager.SpawnEffect(gameObject, Assets.FrontRipFX, characterBody.corePosition, 1, modelTransform.gameObject, Util.QuaternionSafeLookRotation(characterDirection.forward));

    //            // Apply Weak //
    //            if (enemiesHit != null && enemiesHit.Count > 0)
    //            {
    //                foreach (HurtBox enemy in enemiesHit)
    //                {
    //                    new ServerApplyWeak(enemy.healthComponent.gameObject, PantheraConfig.Rip_weakDuration).Send(NetworkDestination.Server);
    //                }
    //            }

    //        }

    //    }

    //    public override void Stop()
    //    {
    //        if (grounded == true) characterMotor.velocity = previousVelocity;
    //    }


    //}
}
