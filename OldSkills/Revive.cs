using Panthera;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.OldSkills;
using Panthera.Skills.Passives;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.OldSkills
{
    public class Revive : MachineScript
    {

        //public float startTime;
        //public int effectID;
        //public GameObject targetPlayer;
        //public bool succeeds = false;

        //public Revive()
        //{

        //}

        //public static void Create()
        //{
        //    // Create the Skill //
        //    PantheraSkill skill = new PantheraSkill();
        //    skill.skillID = PantheraConfig.Revive_SkillID;
        //    skill.name = "REVIVE_SKILL_NAME";
        //    skill.desc = "REVIVE_SKILL_DESC";
        //    skill.icon = PantheraAssets.Revive;
        //    skill.iconPrefab = PantheraAssets.ActiveSkillPrefab;
        //    skill.type = PantheraSkill.SkillType.active;
        //    skill.associatedSkill = typeof(Revive);
        //    skill.priority = PantheraConfig.Revive_priority;
        //    skill.interruptPower = PantheraConfig.Revive_interruptPower;
        //    skill.cooldown = PantheraConfig.Revive_cooldown;
        //    skill.requiredPower = PantheraConfig.Revive_powerRequired;

        //    // Save this Skill //
        //    PantheraSkill.SkillDefsList.Add(skill.skillID, skill);
        //}

        ////public override PantheraSkill getSkillDef()
        ////{
        ////    return base.pantheraObj.activePreset.getSkillByID(PantheraConfig.Revive_SkillID);
        ////}

        //public override bool CanBeUsed(PantheraObj ptraObj)
        //{
        //    pantheraObj = ptraObj;
        //    if (ptraObj.characterBody.power < getSkillDef().requiredPower) return false;
        //    if (ptraObj.skillLocator.getCooldown(getSkillDef().skillID) > 0) return false;
        //    return true;
        //}

        //public override void Start()
        //{

        //    // Save the time //
        //    startTime = Time.time;

        //    // Unstealth //
        //    //Skills.Passives.Stealth.DidDamageUnstealth(pantheraObj);

        //    // Create the Effect //
        //    effectID = Utils.FXManager.SpawnEffect(gameObject, PantheraAssets.ReviveFX, modelTransform.position, pantheraObj.modelScale, null, modelTransform.rotation);

        //    // Start the Sound //
        //    Utils.Sound.playSound(Utils.Sound.ReviveLoopPlay, gameObject);

        //    // Play the start Animation //
        //    Utils.Animation.PlayAnimation(pantheraObj, "KneelStart");

        //    // Look for a dead Player //
        //    foreach (NetworkUser player in NetworkUser.instancesList)
        //    {
        //        if (player.master != null && player.master.lostBodyToDeath == true)
        //        {
        //            targetPlayer = player.master.gameObject;
        //            break;
        //        }
        //    }

        //}

        //public override void Update()
        //{

        //}

        //public override void FixedUpdate()
        //{

        //    // Stop if the Character has moved //
        //    if (characterMotor.velocity.magnitude > 0)
        //    {
        //        Debug.LogWarning("Moved");
        //        machine.EndScript();
        //        return;
        //    }

        //    // Get the total duration //
        //    float duration = Time.time - startTime;

        //    // Stop if the duration is reached //
        //    float skillDuration = Time.time - startTime;
        //    if (skillDuration >= PantheraConfig.Revive_duration)
        //    {
        //        if (targetPlayer != null)
        //        {
        //            new ServerRespawn(targetPlayer, characterBody.corePosition + characterDirection.forward * PantheraConfig.Revive_targetForwardMultiplier, modelTransform.rotation * Quaternion.AngleAxis(180, Vector3.up)).Send(NetworkDestination.Server);
        //            succeeds = true;
        //            Utils.Sound.playSound(Utils.Sound.Revive, targetPlayer);

        //        }
        //        machine.EndScript();
        //        return;
        //    }

        //    // Wait before cheking the Target //
        //    if (duration < PantheraConfig.Revive_CheckTargetDuration)
        //    {
        //        return;
        //    }

        //    // Check the Target //
        //    if (targetPlayer == null)
        //    {
        //        Debug.LogWarning("No Target");
        //        machine.EndScript();
        //        return;
        //    }

        //}

        //public override void Stop()
        //{
        //    Debug.LogWarning("Stop");
        //    // Set the cooldown //
        //    //if (succeeds == true)
        //    //    skillLocator.startCooldown(getSkillDef().skillID);
        //    //else
        //    //    skillLocator.setCooldownInSecond(getSkillDef().skillID, PantheraConfig.Revive_failCooldown);

        //    // Remove the Power //
        //    if (succeeds == true)
        //        characterBody.power -= getSkillDef().requiredPower;

        //    // Play the Fail sound //
        //    if (succeeds == false)
        //        Utils.Sound.playSound(Utils.Sound.ReviveFailed, gameObject);

        //    // Play the stop Animation //
        //    Utils.Animation.PlayAnimation(pantheraObj, "KneelEnd");

        //    // Stop the Effect //
        //    Utils.FXManager.DestroyEffect(effectID);

        //    // Stop the Sound //
        //    Utils.Sound.playSound(Utils.Sound.ReviveLoopStop, gameObject);


        //}

    }
}
