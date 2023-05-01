using Panthera.Base;
using Panthera.BodyComponents;
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

        public float startTime;
        public bool hasFired = false;

        public MightyRoar()
        {
            
        }

        public static void Create()
        {
            // Create the Skill //
            PantheraSkill skill = new PantheraSkill();
            skill.skillID = PantheraConfig.MightyRoar_SkillID;
            skill.name = "MIGHTY_ROAR_SKILL_NAME";
            skill.desc = "MIGHTY_ROAR_SKILL_DESC";
            skill.icon = Assets.MightyRoar;
            skill.iconPrefab = Assets.ActiveSkillPrefab;
            skill.type = PantheraSkill.SkillType.active;
            skill.associatedSkill = typeof(MightyRoar);
            skill.priority = PantheraConfig.MightyRoar_priority;
            skill.interruptPower = PantheraConfig.MightyRoar_interruptPower;
            skill.cooldown = PantheraConfig.MightyRoar_cooldown;
            skill.requiredEnergy = PantheraConfig.MightyRoar_energyRequired;

            // Save this Skill //
            PantheraSkill.SkillDefsList.Add(skill.skillID, skill);
        }

        public override PantheraSkill getSkillDef()
        {
            return base.pantheraObj.activePreset.getSkillByID(PantheraConfig.MightyRoar_SkillID);
        }

        public override bool CanBeUsed(PantheraObj ptraObj)
        {
            base.pantheraObj = ptraObj;
            if (ptraObj.characterBody.energy < this.getSkillDef().requiredEnergy) return false;
            if (ptraObj.skillLocator.getCooldownInSecond(this.getSkillDef().skillID) > 0) return false;
            return true;
        }

        public override void Start()
        {
            // Set the cooldown //
            base.skillLocator.startCooldown(this.getSkillDef().skillID);
            // Save the time //
            this.startTime = Time.time;
            // Remove the Energy //
            this.characterBody.energy -= this.getSkillDef().requiredEnergy;
            // Unstealth //
            Passives.Stealth.DidDamageUnstealth(base.pantheraObj);
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
            if (this.hasFired == false)
            {

                // Set Fired //
                this.hasFired = true;

                // Create the Effect //
                Utils.FXManager.SpawnEffect(base.gameObject, Assets.MightyRoarFX, base.modelTransform.position, 1, base.characterBody.gameObject, base.modelTransform.rotation, true);

                // Play the Animation //
                Utils.Animation.PlayAnimation(base.pantheraObj, "Roar");

                // Get all Stats Radius //
                float radius = base.pantheraObj.activePreset.mightyRoar_radius;
                float stunDuration = base.pantheraObj.activePreset.mightyRoar_stunDuration;
                float bleedingDuration = base.pantheraObj.activePreset.mightyRoar_bleedDuration;
                float bleedDamage = base.pantheraObj.activePreset.mightyRoar_bleedDamage;

                // Get all Enemies //
                Collider[] colliders = Physics.OverlapSphere(player.transform.position, radius, LayerIndex.entityPrecise.mask.value);

                // Itinerate all Enemies found //
                List<GameObject> enemiesHit = new List<GameObject>();
                foreach (Collider collider in colliders)
                {
                    HurtBox hb = collider.GetComponent<HurtBox>();
                    if (hb == null) continue;
                    HealthComponent hc = hb.healthComponent;
                    if (hc == null) continue;
                    if (enemiesHit.Contains(hc.gameObject)) continue;
                    enemiesHit.Add(hc.gameObject);
                    TeamComponent tc = hc?.body?.teamComponent;
                    if (tc == null || tc.teamIndex != TeamIndex.Monster) continue;

                    // Stun the Target //
                    new ServerStunTarget(hc.gameObject, stunDuration).Send(NetworkDestination.Server);

                    // Bleed the Target //
                    if (bleedingDuration > 0)
                        new ServerInflictDot(base.gameObject, hc.gameObject, PantheraConfig.BleedDotIndex, bleedingDuration, bleedDamage).Send(NetworkDestination.Server);

                }

            }

        }

        public override void Stop()
        {
            
        }

    }
}
