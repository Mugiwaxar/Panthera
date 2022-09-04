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

        public static PantheraSkill SkillDef;

        public float startTime;

        public Prowl()
        {

        }

        public static void Create()
        {
            // Create the Skill //
            PantheraSkill skill = new PantheraSkill();
            skill.skillID = PantheraConfig.Prowl_SkillID;
            skill.name = Tokens.ProwlSkillName;
            skill.desc = Tokens.ProwlSkillDesc;
            skill.icon = Assets.Prowl;
            skill.iconPrefab = ConfigPanel.ActiveSkillPrefab;
            skill.type = PantheraSkill.SkillType.active;
            skill.associatedSkill = typeof(Prowl);
            skill.priority = PantheraConfig.Prowl_priority;
            skill.interruptPower = PantheraConfig.Prowl_interruptPower;
            skill.cooldown = PantheraConfig.Prowl_coolDown;

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
            if (ptraObj.GetPassiveScript().stealed == true) return true;
            if (Time.time - PantheraSkill.GetCooldownTime(SkillDef.skillID) < SkillDef.cooldown) return false;
            return true;
        }

        public override void Start()
        {

            // Save the time //
            this.startTime = Time.time;

            // Steal the Character //
            if (isStealed() == true)
            {
                Passives.Steal.UnSteal(this.pantheraObj);
                return;
            }
            else
            {
                Passives.Steal.DoSteal(this.pantheraObj);
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

        public bool isStealed()
        {
            if (this.pantheraObj.GetPassiveScript().stealed == true) return true;
            else return false;
        }

    }
}
