using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.Utils;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using System.Collections.Generic;
using UnityEngine;

namespace Panthera.Skills.Actives
{
    class MightyRoar : MachineScript
    {

        public float startTime;
        public bool hasFired = false;

        public MightyRoar()
        {
            base.icon = PantheraAssets.MightyRoarSkill;
            base.name = PantheraTokens.Get("skill_MightyRoarName");
            base.baseCooldown = PantheraConfig.MightyRoar_cooldown;
            base.desc1 = string.Format(PantheraTokens.Get("skill_MightyRoarDesc"), PantheraConfig.MightyRoar_radius, PantheraConfig.MightyRoar_stunDuration);
            base.desc2 = null;
            base.skillID = PantheraConfig.MightyRoar_SkillID;
            base.priority = PantheraConfig.MightyRoar_priority;
            base.interruptPower = PantheraConfig.MightyRoar_interruptPower;
        }

        //public static void Create()
        //{
        //    // Create the Skill //
        //    PantheraSkill skill = new PantheraSkill();
        //    skill.skillID = PantheraConfig.MightyRoar_SkillID;
        //    skill.name = "MIGHTY_ROAR_SKILL_NAME";
        //    skill.desc = "MIGHTY_ROAR_SKILL_DESC";
        //    skill.icon = PantheraAssets.MightyRoar;
        //    skill.iconPrefab = PantheraAssets.ActiveSkillPrefab;
        //    skill.type = PantheraSkill.SkillType.active;
        //    skill.associatedSkill = typeof(MightyRoar);
        //    skill.priority = PantheraConfig.MightyRoar_priority;
        //    skill.interruptPower = PantheraConfig.MightyRoar_interruptPower;
        //    skill.cooldown = PantheraConfig.MightyRoar_cooldown;
        //    skill.requiredEnergy = PantheraConfig.MightyRoar_energyRequired;

        //    // Save this Skill //
        //    PantheraSkill.SkillDefsList.Add(skill.skillID, skill);
        //}

        //public override PantheraSkill getSkillDef()
        //{
        //    return base.pantheraObj.activePreset.getSkillByID(PantheraConfig.MightyRoar_SkillID);
        //}

        public override bool CanBeUsed(PantheraObj ptraObj)
        {
            //if (ptraObj.characterBody.energy < getSkillDef().requiredEnergy) return false;
            if (ptraObj.skillLocator.GetStock(PantheraConfig.MightyRoar_SkillID) <= 0) return false;
            return true;
        }

        public override void Start()
        {

            // Set the cooldown //
            base.skillLocator.StartCooldown(PantheraConfig.MightyRoar_SkillID);

            // Set the Fake Skill Cooldown //
            //base.skillLocator.special.DeductStock(1);

            // Launch the Fake Skill //
            base.characterBody.OnSkillActivated(base.skillLocator.special);

            // Save the time //
            this.startTime = Time.time;

            // Remove the Energy //
            //base.characterBody.energy -= getSkillDef().requiredEnergy;

            // Unstealth //
            //OldPassives.Stealth.DidDamageUnstealth(pantheraObj);

        }

        public override void Update()
        {

        }

        public override void FixedUpdate()
        {
            // Get the total duration //
            float duration = Time.time - this.startTime;

            // Stop if the duration is reached //
            float skillDuration = Time.time - startTime;
            if (skillDuration >= PantheraConfig.MightyRoar_duration)
            {
                base.machine.EndScript();
                return;
            }

            // Do the Mighty Roar //
            if (this.hasFired == false)
            {

                // Set Fired //
                this.hasFired = true;

                // Create the Effect //
                FXManager.SpawnEffect(base.gameObject, PantheraAssets.MightyRoarFX, base.modelTransform.position, 1, base.characterBody.gameObject, base.modelTransform.rotation, true);

                // Play the Animation //
                Utils.Animation.PlayAnimation(base.pantheraObj, "Roar");

                // Play the Sound //
                Sound.playSound(Sound.MightyRoar, gameObject);

                // Get all Stats Radius //
                float radius = PantheraConfig.MightyRoar_radius;
                float stunDuration = PantheraConfig.MightyRoar_stunDuration;
                //float bleedingDuration = CharacterAbilities.mightyRoar_bleedDuration;
                //float bleedDamage = CharacterAbilities.mightyRoar_bleedDamage;

                // Get all Enemies //
                Collider[] colliders = Physics.OverlapSphere(base.gameObject.transform.position, radius, LayerIndex.entityPrecise.mask.value);

                // Itinerate all Enemies found //
                List<GameObject> enemiesHit = new List<GameObject>();
                foreach (Collider collider in colliders)
                {

                    // Get the Health Component //
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

                    // Add the Tenacity Buff //
                    if (base.pantheraObj.GetAbilityLevel(PantheraConfig.RoarOfResilience_AbilityID) > 0)
                        new ServerAddBuff(base.gameObject, base.gameObject, Buff.TenacityBuff).Send(NetworkDestination.Server);

                    // Bleed the Target //
                    //if (bleedingDuration > 0)
                    //    new ServerInflictDot(gameObject, hc.gameObject, PantheraConfig.BleedDotIndex, bleedingDuration, bleedDamage).Send(NetworkDestination.Server);

                }

            }

        }

        public override void Stop()
        {

        }

    }
}
