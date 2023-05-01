using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using R2API.Networking;
using R2API.Networking.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Skills
{
    internal class ZoneHeal : MachineScript
    {

        public float startTime;
        public bool fired = false;

        public ZoneHeal()
        {

        }

        public static void Create()
        {
            // Create the Skill //
            PantheraSkill skill = new PantheraSkill();
            skill.skillID = PantheraConfig.ZoneHeal_SkillID;
            skill.name = "ZONE_HEAL_SKILL_NAME";
            skill.desc = "ZONE_HEAL_SKILL_DESC";
            skill.icon = Assets.ZoneHeal;
            skill.iconPrefab = Assets.ActiveSkillPrefab;
            skill.type = PantheraSkill.SkillType.active;
            skill.associatedSkill = typeof(ZoneHeal);
            skill.priority = PantheraConfig.ZoneHeal_priority;
            skill.interruptPower = PantheraConfig.ZoneHeal_interruptPower;
            skill.cooldown = PantheraConfig.ZoneHeal_cooldown1;
            skill.requiredPower = PantheraConfig.ZoneHeal_RequiredPower;

            // Save this Skill //
            PantheraSkill.SkillDefsList.Add(skill.skillID, skill);
        }

        public override PantheraSkill getSkillDef()
        {
            return base.pantheraObj.activePreset.getSkillByID(PantheraConfig.ZoneHeal_SkillID);
        }

        public override bool CanBeUsed(PantheraObj ptraObj)
        {
            base.pantheraObj = ptraObj;
            if (ptraObj.characterBody.power < this.getSkillDef().requiredPower) return false;
            if (ptraObj.skillLocator.getCooldownInSecond(this.getSkillDef().skillID) > 0) return false;
            if (ptraObj.pantheraMotor.isGrounded == false) return false;
            return true;
        }

        public override void Start()
        {
            // Set the cooldown //
            base.skillLocator.startCooldown(this.getSkillDef().skillID);
            // Save the time //
            this.startTime = Time.time;
            // Remove the God Power //
            base.characterBody.power -= this.getSkillDef().requiredPower;
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
            if (skillDuration >= PantheraConfig.ZoneHeal_skillDuration)
            {
                this.machine.EndScript();
                return;
            }

            // Do the Zone Heal //
            if (this.fired == false)
            {
                this.fired = true;
                int effectID = Utils.FXManager.SpawnEffect(base.gameObject, Assets.ZoneHealFX, base.modelTransform.position, PantheraConfig.ZoneHeal_fxScale, null, base.modelTransform.rotation);
                ZoneHealComponent comp = Utils.FXManager.GetFX(effectID).AddComponent<Components.ZoneHealComponent>();
                comp.duration = base.pantheraObj.activePreset.zoneHeal_healDuration;
                comp.healRate = PantheraConfig.ZoneHeal_healRate;
                comp.healPercentAmount = base.pantheraObj.activePreset.zoneHeal_percentHeal;
            }

        }

        public override void Stop()
        {

        }

    }
}
