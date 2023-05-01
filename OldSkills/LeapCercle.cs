using EntityStates;
using Panthera;
using Panthera.Components;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.OldSkills;
using Panthera.Skills;
using R2API;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.ParticleSystem;

namespace Panthera.OldSkills
{
    //class LeapCercle : MachineScript
    //{

    //    public Vector3 leapCerclePosition;
    //    public Vector3 leapDirection;
    //    public float previousAirControl;
    //    public float speed;
    //    public float startTime;

    //    public LeapCercle()
    //    {
    //        //priority = PantheraConfig.LeapCercle_priority;
    //        //interruptPower = PantheraConfig.LeapCercle_interruptPower;
    //    }

    //    public override bool CanBeUsed(PantheraObj ptraObj)
    //    {
    //        return true;
    //    }

    //    public override void Start()
    //    {
    //        // Start the timer //
    //        startTime = Time.time;

    //        // Set the Air control //
    //        previousAirControl = characterMotor.airControl;
    //        characterMotor.airControl = PantheraConfig.LeapCercle_airControl;

    //        // Get where to leap //
    //        leapCerclePosition = pantheraObj.actualLeapCerle.position;
    //        Vector3 direction = leapCerclePosition;

    //        // Set the jump used to zero //
    //        characterMotor.jumpCount = 0;

    //        // Calculate the Direction //

    //        pantheraObj.pantheraMotor.startSprint = true;
    //        speed = PantheraConfig.LeapCercle_moveSpeed;
    //        direction.y = Mathf.Max(direction.y, speed);
    //        Vector3 a = direction.normalized * PantheraConfig.LeapCercle_moveSpeed;
    //        Vector3 b = Vector3.up * PantheraConfig.LeapCercle_moveSpeed;
    //        Vector3 b2 = new Vector3(direction.x, 0f, direction.z).normalized * PantheraConfig.LeapCercle_upwardVelocity;
    //        characterMotor.Motor.ForceUnground();
    //        characterMotor.velocity = a + b + b2;
    //        Vector3 characterDirection = leapDirection - characterBody.corePosition;
    //        base.characterDirection.forward = characterDirection.normalized;

    //        // Save the direction //
    //        leapDirection = direction;

    //        // Play the sound // 
    //        //Utils.Sound.playSound(Utils.Sound.LeapCercle, gameObject);

    //        // Enable the Trail //
    //        pantheraFX.SetLeapTrailFX(true);

    //    }

    //    public override void Update()
    //    {

    //    }

    //    public override void FixedUpdate()
    //    {
    //        float totalDuration = Time.time - startTime;

    //        // Stop if the character hit the ground //
    //        if (totalDuration >= PantheraConfig.Leap_minimumDuration && characterMotor.Motor.GroundingStatus.IsStableOnGround)
    //        {
    //            EndScript();
    //            return;
    //        }

    //        // Stop if the leap is too long //
    //        if (totalDuration >= PantheraConfig.LeapCercle_maximumLeapDuration)
    //        {
    //            EndScript();
    //            return;
    //        }

    //        // Stop if the cercle is reached //
    //        if (Vector3.Distance(rigidbody.GetComponent<CapsuleCollider>().ClosestPoint(leapCerclePosition), leapCerclePosition) < PantheraConfig.LeapCercle_stopDistance)
    //        {
    //            EndScript();
    //            return;
    //        }

    //        // Slow down //
    //        if (Vector3.Distance(rigidbody.GetComponent<CapsuleCollider>().ClosestPoint(leapCerclePosition), leapCerclePosition) < PantheraConfig.LeapCercle_slowDownDistance)
    //        {
    //            speed = PantheraConfig.LeapCercle_slowDownSpeed;
    //        }

    //        // Continue the leap //
    //        Vector3 characterDirection = leapDirection - characterBody.footPosition;
    //        base.characterDirection.forward = characterDirection.normalized;
    //        Vector3 charPos = characterBody.footPosition;
    //        Vector3 cerclePos = leapCerclePosition;
    //        Vector3 relativePos = cerclePos - charPos;
    //        //characterMotor.velocity = relativePos.normalized * PantheraConfig.Leap_aimVelocity * speed * PantheraConfig.Leap_forwardVelocity;

    //    }

    //    public override void Stop()
    //    {

    //        // Make the character run after the jump //
    //        pantheraObj.pantheraMotor.startSprint = true;

    //        // Add the regeneration //
    //        new ServerAddBuff(gameObject, (int)PantheraConfig.regenBuffDef.buffIndex, PantheraConfig.LeapCercle_regenDuration).Send(NetworkDestination.Server);

    //        // Set the previous air control //
    //        characterMotor.airControl = previousAirControl;

    //        // Stop the velocity //
    //        characterMotor.velocity = Vector3.zero;

    //        // Disable the Trail //
    //        pantheraFX.SetLeapTrailFX(false);

    //        // Destroy the leap cercle //
    //        if (pantheraObj.actualLeapCerle != null)
    //        {
    //            UnityEngine.Object.Destroy(pantheraObj.actualLeapCerle.gameObject, PantheraConfig.LeapCerle_delayBeforeDestroyed);
    //            pantheraObj.actualLeapCerle.destroying = true;
    //            pantheraObj.actualLeapCerle = null;
    //            if (NetworkServer.active == false) new ClientDestroyLeapCercleFX(gameObject).Send(NetworkDestination.Clients);
    //        }

    //        // Remove the leap cercle buff //
    //        new ServerSetBuffCount(gameObject, (int)Buff.leapCercle.buffIndex, 0).Send(NetworkDestination.Server);

    //    }

    //}
}
