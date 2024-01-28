using Panthera;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.MachineScripts;
using Panthera.OldSkills;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.OldSkills
{
    public class Dash : MachineScript
    {

        //public float startTime;

        //public Dash()
        //{

        //}

        //public static void Create()
        //{
        //    // Create the Skill //
        //    PantheraSkill skill = new PantheraSkill();
        //    skill.skillID = PantheraConfig.Dash_SkillID;
        //    skill.name = "DASH_SKILL_NAME";
        //    skill.desc = "DASH_SKILL_DESC";
        //    skill.icon = Assets.Dash;
        //    skill.iconPrefab = Assets.HybridSkillPrefab;
        //    skill.type = PantheraSkill.SkillType.hybrid;
        //    skill.associatedSkill = typeof(Dash);
        //    skill.priority = PantheraConfig.Dash_priority;
        //    skill.interruptPower = PantheraConfig.Dash_interruptPower;
        //    skill.cooldown = PantheraConfig.Dash_cooldown;

        //    // Save this Skill //
        //    PantheraSkill.SkillDefsList.Add(skill.skillID, skill);
        //}

        ////public override PantheraSkill getSkillDef()
        ////{
        ////    return base.pantheraObj.activePreset.getSkillByID(PantheraConfig.Dash_SkillID);
        ////}

        //public override bool CanBeUsed(PantheraObj ptraObj)
        //{
        //    pantheraObj = ptraObj;
        //    if (ptraObj.skillLocator.getCooldown(getSkillDef().skillID) > 0) return false;
        //    //if (ptraObj.characterBody.stamina < ptraObj.activePreset.dash_staminaConsumed) return false;
        //    return true;
        //}

        //public override void Start()
        //{

        //    // Save the time //
        //    startTime = Time.time;

        //    // Dash //
        //    if (pantheraObj.dashing == false)
        //        Passives.Dash.StartDash(pantheraObj);

        //}

        //public override void Update()
        //{

        //}

        //public override void FixedUpdate()
        //{
        //    float skillDuration = Time.time - startTime;
        //    if (skillDuration >= PantheraConfig.Dash_skillDuration)
        //    {
        //        EndScript();
        //        return;
        //    }
        //}

        //public override void Stop()
        //{

        //}

    }
}
