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
    public class FrontShield : MachineScript
    {

        public FrontShield()
        {
            base.icon = PantheraAssets.FrontShieldSkill;
            base.name = PantheraTokens.Get("ability_FrontShieldName");
            base.baseCooldown = PantheraConfig.FrontShield_cooldown;
            base.removeStealth = false;
            base.desc1 = String.Format(Utils.PantheraTokens.Get("ability_FrontShieldDesc"), PantheraConfig.FrontShield_maxShieldHealthPercent * 100, PantheraConfig.FrontShield_rechargeDelayAfterDamage, PantheraConfig.FrontShield_rechargeDelayAfterDestroyed);
            base.desc2 = null;
            base.skillID = PantheraConfig.FrontShield_SkillID;
            base.requiredAbilityID = PantheraConfig.FrontShield_AbilityID;
            base.priority = PantheraConfig.FrontShield_priority;
            base.interruptPower = PantheraConfig.FrontShield_interruptPower;
            base.activated = false;
        }

        public override bool CanBeUsed(PantheraObj ptraObj)
        {
            if (ptraObj.skillLocator.getStock(PantheraConfig.FrontShield_SkillID) <= 0) return false;
            if (ptraObj.characterBody.frontShield <= 0) return false;
            return true;
        }

        public override void Start()
        {

            // Set the Cooldown //
            base.skillLocator.startCooldown(PantheraConfig.FrontShield_SkillID);

            // Set the character to forward //
            base.characterDirection.forward = GetAimRay().direction;

            // Start the Aim mode //
            base.StartAimMode(1, false);

            // Retrive the Front Shield if deployed //
            if (base.pantheraObj.frontShieldDeployed == true)
                Passives.FrontShield.DeployShield(base.pantheraObj, false);

        }

        public override void Update()
        {

        }

        public override void FixedUpdate()
        {

            // Check if the Shield must stop //
            if (base.characterBody.frontShield <= 0 || base.wasInterrupted == true || base.inputBank.keysPressed.HasFlag(KeysBinder.KeysEnum.Skill2) == false)
            {
                base.EndScript();
                return;
            }

            // Restart the Aim mode //
            base.StartAimMode(1, false);

        }

        public override void Stop()
        {

        }

    }
}
