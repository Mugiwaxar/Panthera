using Panthera.Base;
using Panthera.BodyComponents;
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

namespace Panthera.Skills
{
    internal class Prowl : MachineScript
    {

        public float startTime;

        public Prowl()
        {

        }

        public static void Create()
        {
            // Create the Skill //
            PantheraSkill skill = new PantheraSkill();
            skill.skillID = PantheraConfig.Prowl_SkillID;
            skill.name = "PROWL_SKILL_NAME";
            skill.desc = "PROWL_SKILL_DESC";
            skill.icon = Assets.Prowl;
            skill.iconPrefab = Assets.ActiveSkillPrefab;
            skill.type = PantheraSkill.SkillType.active;
            skill.associatedSkill = typeof(Prowl);
            skill.priority = PantheraConfig.Prowl_priority;
            skill.interruptPower = PantheraConfig.Prowl_interruptPower;
            skill.cooldown = PantheraConfig.Prowl_coolDown;

            // Save this Skill //
            PantheraSkill.SkillDefsList.Add(skill.skillID, skill);
        }

        public override PantheraSkill getSkillDef()
        {
            return base.pantheraObj.activePreset.getSkillByID(PantheraConfig.Prowl_SkillID);
        }

        public override bool CanBeUsed(PantheraObj ptraObj)
        {
            base.pantheraObj = ptraObj;
            if (ptraObj.stealthed == true) return true;
            if (ptraObj.skillLocator.getCooldownInSecond(this.getSkillDef().skillID) > 0) return false;
            return true;
        }

        public override void Start()
        {

            // Save the time //
            this.startTime = Time.time;

            // Steal the Character //
            if (isStealthed() == true)
            {
                Passives.Stealth.UnStealth(this.pantheraObj);
                return;
            }
            else
            {
                Passives.Stealth.DoStealth(this.pantheraObj);
                base.pantheraObj.prowlActivationTime = Time.time;
            }

        }

        public override void Update()
        {

        }

        public override void FixedUpdate()
        {
            float skillDuration = Time.time - this.startTime;
            if (skillDuration >= PantheraConfig.Prowl_skillDuration)
            {
                base.EndScript();
                return;
            }
        }

        public override void Stop()
        {

        }

        public bool isStealthed()
        {
            if (this.pantheraObj.stealthed == true) return true;
            else return false;
        }

    }
}
