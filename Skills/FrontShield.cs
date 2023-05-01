using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.GUI;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.SkillsHybrid;
using Panthera.Utils;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Panthera.Skills
{
    internal class FrontShield : MachineScript
    {

        public FrontShield()
        {

        }

        public static void Create()
        {
            // Create the Skill //
            PantheraSkill skill = new PantheraSkill();
            skill.skillID = PantheraConfig.FrontShield_SkillID;
            skill.name = "FRONT_SHIELD_SKILL_NAME";
            skill.desc = "FRONT_SHIELD_SKILL_DESC";
            skill.icon = Assets.FrontShield;
            skill.iconPrefab = Assets.ActiveSkillPrefab;
            skill.type = PantheraSkill.SkillType.active;
            skill.associatedSkill = typeof(FrontShield);
            skill.priority = PantheraConfig.FrontShield_priority;
            skill.interruptPower = PantheraConfig.FrontShield_interruptPower;
            skill.cooldown = PantheraConfig.FrontShield_cooldown;

            // Save this Skill //
            PantheraSkill.SkillDefsList.Add(skill.skillID, skill);
        }

        public override PantheraSkill getSkillDef()
        {
            return base.pantheraObj.activePreset.getSkillByID(PantheraConfig.FrontShield_SkillID);
        }

        public override bool CanBeUsed(PantheraObj ptraObj)
        {
            base.pantheraObj = ptraObj;
            if (ptraObj.skillLocator.getCooldownInSecond(this.getSkillDef().skillID) > 0) return false;
            if (ptraObj.characterBody.shield <= 0) return false;
            return true;
        }

        public override void Start()
        {

            // Set the Cooldown //
            base.skillLocator.startCooldown(this.getSkillDef().skillID);

            // Set the character to forward //
            base.characterDirection.forward = base.GetAimRay().direction;

            // Start the Aim mode //
            base.StartAimMode(1, false);

            // Enable the Shield //
            //EnableShield(base.pantheraObj);

        }

        public override void Update()
        {

        }

        public override void FixedUpdate()
        {


            // Check if the Shield must stop //
            if (base.characterBody.shield <= 0 || base.wasInterrupted == true || base.inputBank.isSkillPressed(this.getSkillDef().skillID) == false)
            {
                base.EndScript();
                return;
            }

            // Check if Rip is pressed //
            if (base.inputBank.isSkillPressed(PantheraConfig.Rip_SkillID))
            {
                MachineScript script = (MachineScript)Activator.CreateInstance(typeof(ShieldBash), true);
                if (script.CanBeUsed(base.pantheraObj))
                    base.pantheraObj.skillsMachine2.SetScript(script);
            }

            // Restart the Aim mode //
            base.StartAimMode(1, false);

        }

        public override void Stop()
        {
            // Disable the Shield //
            //DisableShield(base.pantheraObj);
        }

    }
}
