using EntityStates;
using Panthera;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.OldSkills;
using Panthera.Skills;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using static RoR2.DotController;

namespace Panthera.OldSkills
{
    class Mangle : MachineScript
    {

        public CharacterBody target;
        public bool done = false;
        public float startTime;

        public Mangle()
        {
            //priority = PantheraConfig.Mangle_priority;
            //interruptPower = PantheraConfig.Mangle_interruptPower;
        }

        public override bool CanBeUsed(PantheraObj ptraObj)
        {
            return true;
        }

        public override void Start()
        {
            // Set the start time //
            startTime = Time.time;
        }

        public override void Update()
        {

        }

        public override void FixedUpdate()
        {
            // Get the total duration //
            float totalDuration = Time.time - startTime;

            // Stop if the duration is reached //
            if (totalDuration >= PantheraConfig.Mangle_atkBaseDuration)
            {
                machine.EndScript();
                return;
            }

            // Check the target //
            if (target == null || target.healthComponent.alive == false)
            {
                machine.EndScript();
                return;
            }

            // Do the attack //
            if (done == false)
            {

                done = true;


                // Calcule the duration the bleeding //
                int stack = characterBody.GetBuffCount(Buff.mangleBuff);
                float duration = PantheraConfig.Mangle_bleedDuration + stack;

                // Tell the server to apply the dot //
                new ServerInflictDot(
                    characterBody.gameObject,
                    target.gameObject,
                    PantheraConfig.bleedDotIndex,
                    duration,
                    PantheraConfig.Mangle_bleedBaseDamage
                    ).Send(NetworkDestination.Server);

                // Spawn the effect //
                //Utils.Functions.SpawnEffect(gameObject, Assets.MangleFX, target.corePosition, PantheraConfig.Model_generalScale, null, Util.QuaternionSafeLookRotation(characterDirection.forward));

                // Clear the buffs //
                new ServerSetBuffCount(gameObject, (int)Buff.mangleBuff.buffIndex, 0);

            }

        }

        public override void Stop()
        {

        }

    }
}
