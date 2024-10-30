using Panthera;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.OldSkills;
using R2API.Networking;
using R2API.Networking.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.OldSkills
{
    public class ZoneHeal : MachineScript
    {

        //public float startTime;
        //public bool fired = false;

        //public ZoneHeal()
        //{

        //}

        //public static void Create()
        //{
        //    // Create the Skill //
        //    PantheraSkill skill = new PantheraSkill();
        //    skill.skillID = PantheraConfig.ZoneHeal_SkillID;
        //    skill.name = "ZONE_HEAL_SKILL_NAME";
        //    skill.desc = "ZONE_HEAL_SKILL_DESC";
        //    skill.icon = PantheraAssets.ZoneHeal;
        //    skill.iconPrefab = PantheraAssets.ActiveSkillPrefab;
        //    skill.type = PantheraSkill.SkillType.active;
        //    skill.associatedSkill = typeof(ZoneHeal);
        //    skill.priority = PantheraConfig.ZoneHeal_priority;
        //    skill.interruptPower = PantheraConfig.ZoneHeal_interruptPower;
        //    skill.cooldown = PantheraConfig.ZoneHeal_cooldown1;
        //    skill.requiredPower = PantheraConfig.ZoneHeal_RequiredPower;

        //    // Save this Skill //
        //    PantheraSkill.SkillDefsList.Add(skill.skillID, skill);
        //}

        ////public override PantheraSkill getSkillDef()
        ////{
        ////    return base.pantheraObj.activePreset.getSkillByID(PantheraConfig.ZoneHeal_SkillID);
        ////}

        //public override bool CanBeUsed(PantheraObj ptraObj)
        //{
        //    //base.pantheraObj = ptraObj;
        //    //if (ptraObj.characterBody.power < this.getSkillDef().requiredPower) return false;
        //    //if (ptraObj.skillLocator.getCooldownInSecond(this.getSkillDef().skillID) > 0) return false;
        //    //if (ptraObj.pantheraMotor.isGrounded == false) return false;
        //    return true;
        //}

        //public override void Start()
        //{
        //    // Set the cooldown //
        //    skillLocator.startCooldown(getSkillDef().skillID);
        //    // Save the time //
        //    startTime = Time.time;
        //    // Remove the God Power //
        //    characterBody.power -= getSkillDef().requiredPower;
        //}

        //public override void Update()
        //{

        //}

        //public override void FixedUpdate()
        //{
        //    // Get the total duration //
        //    float duration = Time.time - startTime;

        //    // Stop if the duration is reached //
        //    float skillDuration = Time.time - startTime;
        //    if (skillDuration >= PantheraConfig.ZoneHeal_skillDuration)
        //    {
        //        machine.EndScript();
        //        return;
        //    }

        //    // Do the Zone Heal //
        //    if (fired == false)
        //    {
        //        fired = true;
        //        int effectID = Utils.FXManager.SpawnEffect(gameObject, PantheraAssets.ZoneHealFX, modelTransform.position, PantheraConfig.ZoneHeal_fxScale, null, modelTransform.rotation);
        //        ZoneHealComponent comp = Utils.FXManager.GetEffect(effectID).AddComponent<ZoneHealComponent>();
        //        comp.duration = PantheraConfig.ZoneHeal_healDuration;
        //        comp.healRate = PantheraConfig.ZoneHeal_healRate;
        //        comp.healPercentAmount = PantheraConfig.ZoneHeal_percentHeal1;
        //    }

        //}

        //public override void Stop()
        //{

        //}

    }
}
