using Panthera;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.GUI;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.OldSkills;
using Panthera.Skills.Actives;
using Panthera.Utils;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Panthera.Skills.Actives
{
    public class ArcaneAnchor : MachineScript
    {
        public float startTime;

        public ArcaneAnchor()
        {
            base.icon = Assets.ArcaneAnchorSkill;
            base.name = PantheraTokens.Get("ability_ArcaneAnchorName");
            base.baseCooldown = PantheraConfig.ArcaneAnchor_cooldown;
            base.removeStealth = false;
            base.desc1 = String.Format(Utils.PantheraTokens.Get("ability_ArcaneAnchorDesc")); String.Format(Utils.PantheraTokens.Get("ability_ArcaneAnchorDesc"));
            base.desc2 = null;
            base.showCooldown = true;
            //base.machineToUse = 2;
            base.skillID = PantheraConfig.ArcaneAnchor_SkillID;
            base.requiredAbilityID = PantheraConfig.ArcaneAnchor_AbilityID;
            base.priority = PantheraConfig.ArcaneAnchor_priority;
            base.interruptPower = PantheraConfig.ArcaneAnchor_interruptPower;
        }

        public override bool CanBeUsed(PantheraObj ptraObj)
        {
            if (ptraObj.skillLocator.getStock(PantheraConfig.ArcaneAnchor_SkillID) <= 0) return false;
            if (ptraObj.frontShieldObj.active == false) return false;
            if (ptraObj.frontShieldDeployed == true) return false;
            if (ptraObj.characterBody.frontShield <= 0) return false;
            return true;
        }

        public override void Start()
        {

            // Save the time //
            this.startTime = Time.time;

            // Start the Cooldown //
            base.skillLocator.startCooldown(PantheraConfig.ArcaneAnchor_SkillID);

            // Set the Cooldown of Front Shield //
            base.skillLocator.startCooldown(PantheraConfig.FrontShield_SkillID, PantheraConfig.ArcaneAnchor_frontShieldCooldown);

            // Set the Front Shield to deployed //
            Passives.FrontShield.DeployShield(base.pantheraObj, true);

            // Play the sound //
            Sound.playSound(Sound.ArcaneAnchor, base.gameObject);

        }

        public override void Update()
        {

        }

        public override void FixedUpdate()
        {


            // Stop if the duration is reached //
            float skillDuration = Time.time - this.startTime;
            if (skillDuration >= PantheraConfig.ArcaneAnchor_skillDuration)
            {
                base.EndScript();
                return;
            }

        }

        public override void Stop()
        {

        }

    }
}
