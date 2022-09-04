using Panthera.Components;
using Panthera.GUI;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.Utils;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Skills
{
    class MightyRoar : MachineScript
    {

        public static PantheraSkill SkillDef;

        public float startTime;
        public bool fired = false;

        public MightyRoar()
        {
            
        }

        public static void Create()
        {
            // Create the Skill //
            PantheraSkill skill = new PantheraSkill();
            skill.skillID = PantheraConfig.MightyRoar_SkillID;
            skill.name = Tokens.MightyRoarSkillName;
            skill.desc = Tokens.MightyRoarSkillDesc;
            skill.icon = Assets.MightyRoar;
            skill.iconPrefab = ConfigPanel.ActiveSkillPrefab;
            skill.type = PantheraSkill.SkillType.active;
            skill.associatedSkill = typeof(MightyRoar);
            skill.priority = PantheraConfig.MightyRoar_priority;
            skill.interruptPower = PantheraConfig.MightyRoar_interruptPower;
            skill.cooldown = PantheraConfig.MightyRoar_cooldown;
            skill.requiredEnergy = PantheraConfig.MightyRoar_energyRequired;

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
            if (ptraObj.characterBody.energy < SkillDef.requiredEnergy) return false;
            if (Time.time - PantheraSkill.GetCooldownTime(SkillDef.skillID) < SkillDef.cooldown) return false;
            return true;
        }

        public override void Start()
        {
            // Save the time //
            PantheraSkill.SetCooldownTime(SkillDef.skillID, Time.time);
            this.startTime = Time.time;
            // Remove the Energy //
            this.characterBody.energy -= SkillDef.requiredEnergy;
        }
        
        public override void Update()
        {

        }

        public override void FixedUpdate()
        {
            // Get the total duration //
            float duration = Time.time - this.startTime;

            // Stop if the duration is reached //
            float skillDuration = Time.time - this.startTime;
            if (skillDuration >= PantheraConfig.MightyRoar_duration)
            {
                this.machine.EndScript();
                return;
            }

            // Do the Mighty Roar //
            if (this.fired == false)
            {
                this.fired = true;
                Utils.Functions.SpawnEffect(base.gameObject, Assets.MightyRoarFX, modelTransform.position, PantheraConfig.Model_generalScale, this.characterBody.gameObject);
                new ServerDoMightyRoar(base.gameObject).Send(NetworkDestination.Server);
            }

        }

        public override void Stop()
        {
            
        }

    }
}
