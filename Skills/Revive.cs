using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Skills
{
    internal class Revive : MachineScript
    {

        public float startTime;
        public int effectID;
        public GameObject targetPlayer;
        public bool succeeds = false;

        public Revive()
        {

        }

        public static void Create()
        {
            // Create the Skill //
            PantheraSkill skill = new PantheraSkill();
            skill.skillID = PantheraConfig.Revive_SkillID;
            skill.name = "REVIVE_SKILL_NAME";
            skill.desc = "REVIVE_SKILL_DESC";
            skill.icon = Assets.Revive;
            skill.iconPrefab = Assets.ActiveSkillPrefab;
            skill.type = PantheraSkill.SkillType.active;
            skill.associatedSkill = typeof(Revive);
            skill.priority = PantheraConfig.Revive_priority;
            skill.interruptPower = PantheraConfig.Revive_interruptPower;
            skill.cooldown = PantheraConfig.Revive_cooldown;
            skill.requiredPower = PantheraConfig.Revive_powerRequired;

            // Save this Skill //
            PantheraSkill.SkillDefsList.Add(skill.skillID, skill);
        }

        public override PantheraSkill getSkillDef()
        {
            return base.pantheraObj.activePreset.getSkillByID(PantheraConfig.Revive_SkillID);
        }

        public override bool CanBeUsed(PantheraObj ptraObj)
        {
            base.pantheraObj = ptraObj;
            if (ptraObj.characterBody.power < this.getSkillDef().requiredPower) return false;
            if (ptraObj.skillLocator.getCooldownInSecond(this.getSkillDef().skillID) > 0) return false;
            return true;
        }

        public override void Start()
        {

            // Save the time //
            this.startTime = Time.time;

            // Unstealth //
            Passives.Stealth.DidDamageUnstealth(base.pantheraObj);

            // Create the Effect //
            effectID = Utils.FXManager.SpawnEffect(base.gameObject, Assets.ReviveFX, base.modelTransform.position, base.pantheraObj.modelScale, null, base.modelTransform.rotation);

            // Start the Sound //
            Utils.Sound.playSound(Utils.Sound.ReviveLoopPlay, base.gameObject);

            // Play the start Animation //
            Utils.Animation.PlayAnimation(base.pantheraObj, "KneelStart");

            // Look for a dead Player //
            foreach (NetworkUser player in NetworkUser.instancesList)
            {
                if (player.master != null && player.master.lostBodyToDeath == true)
                {
                    this.targetPlayer = player.master.gameObject;
                    break;
                }
            }

        }

        public override void Update()
        {

        }

        public override void FixedUpdate()
        {

            // Stop if the Character has moved //
            if (base.characterMotor.velocity.magnitude > 0)
            {
                Debug.LogWarning("Moved");
                this.machine.EndScript();
                return;
            }

            // Get the total duration //
            float duration = Time.time - this.startTime;

            // Stop if the duration is reached //
            float skillDuration = Time.time - this.startTime;
            if (skillDuration >= PantheraConfig.Revive_duration)
            {
                if (this.targetPlayer != null)
                {
                    new ServerRespawn(this.targetPlayer, base.characterBody.corePosition + (base.characterDirection.forward * PantheraConfig.Revive_targetForwardMultiplier), base.modelTransform.rotation * Quaternion.AngleAxis(180, Vector3.up)).Send(NetworkDestination.Server);
                    this.succeeds = true;
                    Utils.Sound.playSound(Utils.Sound.Revive, this.targetPlayer);
                        
                }
                this.machine.EndScript();
                return;
            }

            // Wait before cheking the Target //
            if (duration < PantheraConfig.Revive_CheckTargetDuration)
            {
                return;
            }

            // Check the Target //
            if (this.targetPlayer == null)
            {
                Debug.LogWarning("No Target");
                this.machine.EndScript();
                return;
            }

        }

        public override void Stop()
        {
            Debug.LogWarning("Stop");
            // Set the cooldown //
            if (this.succeeds == true)
                base.skillLocator.startCooldown(this.getSkillDef().skillID);
            else
                base.skillLocator.setCooldownInSecond(this.getSkillDef().skillID, PantheraConfig.Revive_failCooldown);

            // Remove the Power //
            if (this.succeeds == true)
                this.characterBody.power -= this.getSkillDef().requiredPower;

            // Play the Fail sound //
            if (this.succeeds == false)
                Utils.Sound.playSound(Utils.Sound.ReviveFailed, base.gameObject);

            // Play the stop Animation //
            Utils.Animation.PlayAnimation(base.pantheraObj, "KneelEnd");

            // Stop the Effect //
            Utils.FXManager.DestroyFX(this.effectID);

            // Stop the Sound //
            Utils.Sound.playSound(Utils.Sound.ReviveLoopStop, base.gameObject);


        }

    }
}
