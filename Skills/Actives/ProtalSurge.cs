using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.Utils;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Panthera.Skills.Actives
{
    public class PortalSurge : MachineScript
    {

        public float startTime;
        public int effectID;
        public int telepoterEffectID;
        public GameObject teleporter;
        public bool playedEndEffect;
        public bool succeeds = false;

        public PortalSurge()
        {
            base.icon = PantheraAssets.PortalSurgeSkill;
            base.name = PantheraTokens.Get("ability_PortalSurgeName");
            base.baseCooldown = PantheraConfig.PortalSurge_cooldown;
            base.desc1 = String.Format(Utils.PantheraTokens.Get("ability_PortalSurgeDesc"), PantheraConfig.PortalSurge_unlockLevel, PantheraConfig.PortalSurge_requiredIngameLevel, PantheraConfig.PortalSurge_lunarCost);
            base.desc2 = null;
            base.machineToUse = 1;
            base.removeStealth = true;
            base.skillID = PantheraConfig.PortalSurge_SkillID;
            base.requiredAbilityID = PantheraConfig.PortalSurge_AbilityID;
            base.priority = PantheraConfig.PortalSurge_priority;
            base.interruptPower = PantheraConfig.PortalSurge_interruptPower;
        }

        public override bool CanBeUsed(PantheraObj ptraObj)
        {
            if (Panthera.PantheraCharacter.lunarCoin < PantheraConfig.PortalSurge_lunarCost) return false;
            if (ptraObj.skillLocator.getStock(PantheraConfig.PortalSurge_SkillID) <= 0) return false;
            if (ptraObj.characterBody.level < PantheraConfig.PortalSurge_requiredIngameLevel) return false;
            return true;
        }

        public override void Start()
        {

            // Save the time //
            this.startTime = Time.time;

            // Start the Sound //
                Utils.Sound.playSound(Utils.Sound.PortalCharging, base.gameObject);

            // Play the start Animation //
                Utils.Animation.PlayAnimation(base.pantheraObj, "KneelStart");

            // Scan around //
            Collider[] colliders = Physics.OverlapSphere(base.characterBody.footPosition, PantheraConfig.PortalSurge_detectionRadius);

            // Try to Find the Teleporter //
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.name == "TeleporterBaseMesh")
                    this.teleporter = collider.gameObject.transform.parent.gameObject;
            }

            // Create the Effect //
            this.effectID = Utils.FXManager.SpawnEffect(base.gameObject, PantheraAssets.PortalPlayerChargingFX, modelTransform.position, 1, base.gameObject, base.modelTransform.rotation);

            // Create the Teleporter Effect //
            if (this.teleporter != null)
                this.telepoterEffectID = Utils.FXManager.SpawnEffect(base.gameObject, PantheraAssets.PortalChargingFX, this.teleporter.transform.position, 1, this.teleporter, this.teleporter.transform.rotation);

            // Check if not already Surged or activated //
            if (this.teleporter != null && (this.teleporter.transform.Find("PortalOverChargeFX(Clone)") != null || this.teleporter.GetComponent<HoldoutZoneController>().enabled == true || this.teleporter.GetComponent<HoldoutZoneController>().charge > 0))
                this.teleporter = null;

        }

        public override void Update()
        {

        }

        public override void FixedUpdate()
        {

            // Stop if the Character has moved //
            if (base.characterMotor.velocity.magnitude > 0)
            {
                base.machine.EndScript();
                return;
            }

            // Get the Total Skill Duration //
            float skillDuration = Time.time - startTime;

            // Stop if the duration is reached //
            if (skillDuration >= PantheraConfig.PortalSurge_duration)
            {
                if (this.teleporter != null)
                {
                    //new ServerRespawn(targetPlayer, characterBody.corePosition + characterDirection.forward * PantheraConfig.Revive_targetForwardMultiplier, modelTransform.rotation * Quaternion.AngleAxis(180, Vector3.up)).Send(NetworkDestination.Server);
                    this.succeeds = true;
                    // Create the explosion Effect //
                    Utils.FXManager.SpawnEffect(base.gameObject, PantheraAssets.PortalChargingExplosionFX, this.teleporter.transform.position, 1, this.teleporter, this.teleporter.transform.rotation);
                }
                base.machine.EndScript();
                return;
            }

            // Wait before cheking the Target //
            if (skillDuration < PantheraConfig.PortalSurge_FailTime)
            {
                return;
            }

            // Check the Target //
            if (this.teleporter == null)
            {
                base.machine.EndScript();
                return;
            }

            // Play the End Effect //
            if(skillDuration > PantheraConfig.PortalSurge_endEffectTime && this.playedEndEffect == false)
            {
                this.playedEndEffect = true;
                Utils.FXManager.SpawnEffect(base.gameObject, PantheraAssets.PortalCharingEndFX, this.teleporter.transform.position, 1, this.teleporter, this.teleporter.transform.rotation);
            }

        }

        public override void Stop()
        {

            // Check if Succeeds //
            if (this.succeeds == true)
            {
                // Start the Cooldown //
                base.skillLocator.startCooldown(PantheraConfig.PortalSurge_SkillID);
                // Create the new Teleporter Effect //
                Utils.FXManager.SpawnEffect(base.gameObject, PantheraAssets.PortalOverChargeFX, this.teleporter.transform.position, 1, this.teleporter, this.teleporter.transform.rotation);
                // Send to the Server //
                new ServerSetPortalSurge(this.gameObject, this.teleporter, true).Send(R2API.Networking.NetworkDestination.Server);
                // Consume Lunar Coins //
                Panthera.PantheraCharacter.lunarCoin -= PantheraConfig.PortalSurge_lunarCost;
                // Kill all Monsters //
                foreach (TeamComponent tc in TeamComponent.GetTeamMembers(TeamIndex.Monster).ToArray())
                {
                    new ServerInflictDamage(base.gameObject, tc.gameObject, tc.transform.position, PantheraConfig.PortalSurge_damage).Send(R2API.Networking.NetworkDestination.Server);
                }
            }
            else
            {
                // Start the Cooldown //
                base.skillLocator.startCooldown(PantheraConfig.PortalSurge_SkillID, PantheraConfig.PortalSurge_failCoolDown);
                // Play the Fail sound //
                Utils.Sound.playSound(Utils.Sound.PortalChargeFailed, gameObject);
                // Stop the Sound //
                Utils.Sound.playSound(Utils.Sound.PortalChargingStop, gameObject);
            }

            // Play the stop Animation //
            Utils.Animation.PlayAnimation(base.pantheraObj, "KneelEnd");

            // Stop the Effect //
            Utils.FXManager.DestroyEffect(this.effectID);

            // Stop the Teleporter Effect //
            Utils.FXManager.DestroyEffect(this.telepoterEffectID);

        }
    }

}