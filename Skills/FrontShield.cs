using Panthera.Components;
using Panthera.GUI;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.Utils;
using R2API.Networking;
using R2API.Networking.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Panthera.Skills
{
    internal class FrontShield : MachineScript
    {

        public static PantheraSkill SkillDef;

        public FrontShield()
        {

        }

        public static void Create()
        {
            // Create the Skill //
            PantheraSkill skill = new PantheraSkill();
            skill.skillID = PantheraConfig.FrontShield_SkillID;
            skill.name = Tokens.FrontShieldName;
            skill.desc = Tokens.FrontShieldDesc;
            skill.icon = Assets.FrontShield;
            skill.iconPrefab = ConfigPanel.ActiveSkillPrefab;
            skill.type = PantheraSkill.SkillType.active;
            skill.associatedSkill = typeof(FrontShield);
            skill.priority = PantheraConfig.FrontShield_priority;
            skill.interruptPower = PantheraConfig.FrontShield_interruptPower;
            skill.cooldown = PantheraConfig.FrontShield_cooldown;

            // Save this Skill //
            SkillDef = skill;
            PantheraSkill.SkillDefsList.Add(skill.skillID, skill);
        }

        public override PantheraSkill getSkillDef()
        {
            return SkillDef;
        }

        public override bool CanBeUsed(PantheraObj ptraObj)
        {
            if (Time.time - PantheraSkill.GetCooldownTime(SkillDef.skillID) < SkillDef.cooldown) return false;
            if (ptraObj.characterBody.shield <= 0) return false;
            return true;
        }

        public static void EnableShield(PantheraObj obj)
        {
            obj.frontShield.SetActive(true);
            if (NetworkServer.active == false) new ServerSetFrontShield(obj.gameObject, true).Send(NetworkDestination.Server);
        }

        public static void DisableShield(PantheraObj obj)
        {
            obj.frontShield.SetActive(false);
            if (NetworkServer.active == false) new ServerSetFrontShield(obj.gameObject, false).Send(NetworkDestination.Server);
        }

        public override void Start()
        {

            // Set the Cooldown //
            PantheraSkill.SetCooldownTime(SkillDef.skillID, Time.time);

            // Set the character to forward //
            base.characterDirection.forward = base.GetAimRay().direction;

            // Start the Aim mode //
            base.StartAimMode(1, false);

            // Enable the Shield //
            EnableShield(base.pantheraObj);

        }

        public override void Update()
        {

        }

        public override void FixedUpdate()
        {


            // Check if the Shield must stop //
            if(base.characterBody.shield <= 0 || base.wasInterrupted == true || base.inputBank.isSkillPressed(SkillDef.skillID) == false)
            {
                base.EndScript();
                return;
            }

            // Restart the Aim mode //
            base.StartAimMode(1, false);

        }

        public override void Stop()
        {
            // Disable the Shield //
            DisableShield(base.pantheraObj);
        }

    }
}
