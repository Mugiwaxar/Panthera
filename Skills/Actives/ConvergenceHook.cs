using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.Utils;
using R2API.Networking;
using R2API.Networking.Interfaces;
using System;
using UnityEngine;

namespace Panthera.Skills.Actives
{
    class ConvergenceHook : MachineScript
    {

        public float startTime;
        public float baseDuration = PantheraConfig.ConvergenceHook_skillDuration;

        public ConvergenceHook()
        {
            base.icon = PantheraAssets.ConvergenceHookSkill;
            base.name = PantheraTokens.Get("ability_ConvergenceHookName");
            base.baseCooldown = PantheraConfig.ConvergenceHook_cooldown;
            base.showCooldown = true;
            base.desc1 = String.Format(Utils.PantheraTokens.Get("ability_ConvergenceHookDesc"));
            base.desc2 = null;
            base.skillID = PantheraConfig.ConvergenceHook_SkillID;
            base.requiredAbilityID = PantheraConfig.ConvergenceHook_AbilityID;
            base.priority = PantheraConfig.ConvergenceHook_priority;
            base.interruptPower = PantheraConfig.ConvergenceHook_interruptPower;
        }

        public override bool CanBeUsed(PantheraObj ptraObj)
        {
            if (ptraObj.skillLocator.GetStock(PantheraConfig.ConvergenceHook_SkillID) <= 0) return false;
            return true;
        }

        public override void Start()
        {

            // Save the time //
            this.startTime = Time.time;

            // Start the Cooldown //
            base.skillLocator.StartCooldown(PantheraConfig.ConvergenceHook_SkillID);

            // Play the Sound //
            Sound.playSound(Sound.ConvergenceHook, gameObject);

            // Play the Animation //
            PlayAnimation("Dodge1", 0.2f);

            // Calculate the skill duration //
            this.baseDuration = this.baseDuration / base.attackSpeedStat;

            // Spawn the Effect //
            FXManager.SpawnEffect(base.pantheraObj.gameObject, PantheraAssets.ConvergenceHookFX, base.modelTransform.position, base.pantheraObj.modelScale, null, base.modelTransform.rotation, false);

            // Send the Message to active the Component //
            new ServerActivateConvergenceHookComp(this.gameObject).Send(NetworkDestination.Server);

        }

        public override void Update()
        {

        }

        public override void FixedUpdate()
        {

            // Stop if the duration is reached //
            float skillDuration = Time.time - this.startTime;
            if (skillDuration >= this.baseDuration)
            {
                EndScript();
                return;
            }

        }

        public override void Stop()
        {

        }

    }
}
