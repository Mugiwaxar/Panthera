using RoR2;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.MachineScripts;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Panthera.NetworkMessages;
using R2API.Networking.Interfaces;
using R2API.Networking;

namespace Panthera.Skills.Actives
{
    class FeralBite : MachineScript
    {

        public HealthComponent targetHC;
        public float startTime;

        public FeralBite()
        {
            base.icon = PantheraAssets.FeralBiteSkill;
            base.name = PantheraTokens.Get("skill_FeralBiteName");
            base.baseCooldown = PantheraConfig.FeralBite_cooldown;
            base.desc1 = string.Format(PantheraTokens.Get("skill_FeralBiteDesc"), PantheraConfig.FeralBite_damagesMultiplier * 100, PantheraConfig.FeralBite_healMultiplier * 100) + string.Format(PantheraTokens.Get("skill_FeralBiteFuryDesc"), PantheraConfig.FeralBite_furyAdded);
            base.skillID = PantheraConfig.FeralBite_SkillID;
            base.priority = PantheraConfig.FeralBite_priority;
            base.interruptPower = PantheraConfig.FeralBite_interruptPower;
        }

        public override bool CanBeUsed(PantheraObj ptraObj)
        {
            if (ptraObj.skillLocator.getStock(PantheraConfig.FeralBite_SkillID) <= 0) return false;
            return true;
        }

        public override void Start()
        {

            // Check the Target //
            if(this.targetHC == null || this.targetHC.alive == false)
                base.EndScript();

            // Save the time //
            this.startTime = Time.time;

            // Save the last Feral Bite use //
            this.pantheraObj.lastFeralBiteUse = Time.time;

            // Set in combat //
            base.characterBody.outOfCombatStopwatch = 0f;

            // Start the Aim Mode //
            base.StartAimMode(PantheraConfig.FeralBite_skillDuration, false);

            // Unstealth //
            Skills.Passives.Stealth.DidDamageUnstealth(pantheraObj);

            // Set the Cooldown //
            base.skillLocator.startCooldown(PantheraConfig.FeralBite_SkillID, base.baseCooldown);

            // Spawn the effect //
            FXManager.SpawnEffect(gameObject, PantheraAssets.FeralBiteFX, characterBody.corePosition, pantheraObj.actualModelScale, null, Util.QuaternionSafeLookRotation(characterDirection.forward));

            // Play the Animation //
            Utils.Animation.PlayAnimation(pantheraObj, "Bite");

            // Play the Sound //
            Sound.playSound(Utils.Sound.FeralBite, base.gameObject);

            // Calculate the damages //
            float damage = base.damageStat * PantheraConfig.FeralBite_damagesMultiplier;

            // Check if critic //
            bool isCrit = Util.CheckRoll(base.critStat, base.characterBody.master);

            // Tell the server to inflict damage //
            new ServerInflictDamage(gameObject, this.targetHC.gameObject, this.targetHC.transform.position, damage, isCrit, PantheraConfig.FeralBite_procCoefficient).Send(NetworkDestination.Server);

            // Add Fury //
            if (base.pantheraObj.profileComponent.getAbilityLevel(PantheraConfig.Fury_AbilityID) > 0)
                base.characterBody.fury += isCrit == true ? PantheraConfig.FeralBite_furyAdded * 2 : PantheraConfig.FeralBite_furyAdded;

        }

        public override void FixedUpdate()
        {
            // Get the total duration //
            float totalDuration = Time.time - startTime;

            // Stop if the time is up //
            if (totalDuration > PantheraConfig.FeralBite_skillDuration)
            {
                EndScript();
            }

        }

        public override void Stop()
        {

        }

    }
}
