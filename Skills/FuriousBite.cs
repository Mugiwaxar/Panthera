using EntityStates;
using HarmonyLib;
using KinematicCharacterController;
using Panthera;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.GUI;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.OldSkills;
using Panthera.Skills;
using Panthera.SkillsPassive;
using Panthera.Utils;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using static Panthera.Base.PantheraSkill;

namespace Panthera.Skills
{
    class FuriousBite : MachineScript
    {

        public float startTime;

        public FuriousBite()
        {

        }

        public static void Create()
        {
            // Create the Skill //
            PantheraSkill skill = new PantheraSkill();
            skill.skillID = PantheraConfig.FuriousBite_SkillID;
            skill.name = "FURIOUS_BITE_SKILL_NAME";
            skill.desc = "FURIOUS_BITE_SKILL_DESC";
            skill.icon = Assets.FuriousBite;
            skill.iconPrefab = Assets.ActiveSkillPrefab;
            skill.type = PantheraSkill.SkillType.active;
            skill.associatedSkill = typeof(FuriousBite);
            skill.priority = PantheraConfig.FuriousBite_priority;
            skill.interruptPower = PantheraConfig.FuriousBite_interruptPower;
            skill.cooldown = PantheraConfig.FuriousBite_cooldown;
            skill.requiredEnergy = PantheraConfig.FuriousBite_energyRequired;

            // Save this Skill //
            PantheraSkill.SkillDefsList.Add(skill.skillID, skill);
        }

        public override PantheraSkill getSkillDef()
        {
            return base.pantheraObj.activePreset.getSkillByID(PantheraConfig.FuriousBite_SkillID);
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

            // Set the start time //
            startTime = Time.time;

            // Find a Target //
            CharacterBody target = null;
            float minTargetDistance = 999;
            Collider[] colliders = Physics.OverlapSphere(base.characterBody.corePosition + base.characterDirection.forward, PantheraConfig.FuriousBite_detectionRadius, LayerIndex.entityPrecise.mask.value);
            foreach (Collider collider in colliders)
            {
                HurtBox hurtbox = collider.GetComponent<HurtBox>();
                if (hurtbox != null && hurtbox.healthComponent != null 
                    && hurtbox.healthComponent.body != null && hurtbox.healthComponent.body != base.characterBody)
                {
                    float distance = Vector3.Distance(hurtbox.transform.position, base.characterBody.corePosition);
                    if (distance < minTargetDistance)
                    {
                        minTargetDistance = distance;
                        target = hurtbox.healthComponent.body;
                    }
                }
            }

            // Check the Target //
            if (target == null)
            {
                base.EndScript();
                return;
            }

            // Unstealth //
            Passives.Stealth.DidDamageUnstealth(base.pantheraObj);

            // Spawn the effect //
            Utils.FXManager.SpawnEffect(gameObject, Assets.BiteFX, base.characterBody.corePosition, base.pantheraObj.modelScale, null, Util.QuaternionSafeLookRotation(characterDirection.forward));

            // Play the Animation //
            Utils.Animation.PlayAnimation(base.pantheraObj, "Bite");

            // Calcule the damages //
            int cpUsed = Math.Min((int)PantheraConfig.FuriousBite_maxComboPointUsed, (int)base.characterBody.comboPoint);
            float damageAdded = cpUsed * base.pantheraObj.activePreset.furiousBite_ComboPointMultiplier;
            float damage = damageStat * (base.pantheraObj.activePreset.furiousBite_atkDamageMultiplier + damageAdded);

            // Calcule if Critic //
            bool crit = RollCrit();

            // Tell the server to inflict damage //
            new ServerInflictDamage(gameObject, target.gameObject, target.transform.position, damage, crit).Send(NetworkDestination.Server);

            // Check the Powered Jaws Ability //
            if (base.pantheraObj.activePreset.getAbilityLevel(PantheraConfig.PowerfullJawsAbilityID) > 0 && crit == true && PowerfullJaws.CanBeUsed(base.pantheraObj))
            {
                PowerfullJaws.Use(base.pantheraObj);
            }
            else
            {
                // Set the Cooldown //
                base.skillLocator.startCooldown(this.getSkillDef().skillID);
                // Remove the Combo Point //
                this.characterBody.comboPoint -= cpUsed;
            }

            // Remove the Energy //
            this.characterBody.energy -= this.getSkillDef().requiredEnergy;

            // Check and apply the Predator's Drink Ability //
            int preDrinkLevel = base.pantheraObj.activePreset.getAbilityLevel(PantheraConfig.PredatorsDrinkAbilityID);
            float percentHeal = 0;
            float maxHeal = base.characterBody.maxHealth;
            if (preDrinkLevel == 1)
                percentHeal = PantheraConfig.PredatorsDrink_percent1;
            else if (preDrinkLevel == 2)
                percentHeal = PantheraConfig.PredatorsDrink_percent2;
            else if (preDrinkLevel == 3)
                percentHeal = PantheraConfig.PredatorsDrink_percent3;
            else if (preDrinkLevel == 4)
                percentHeal = PantheraConfig.PredatorsDrink_percent4;
            else if (preDrinkLevel == 5)
                percentHeal = PantheraConfig.PredatorsDrink_percent5;
            percentHeal *= (float)cpUsed;
            if (preDrinkLevel > 0)
                percentHeal += PantheraConfig.PredatorsDrink_basePercent;
            float heal = maxHeal * percentHeal;
            if (heal > 0)
                new ServerHeal(base.characterBody.gameObject, heal).Send(NetworkDestination.Server);

        }

        public override void Update()
        {

        }

        public override void FixedUpdate()
        {
            // Get the total duration //
            float totalDuration = Time.time - startTime;

            // Stop if the time is up //
            if (totalDuration > PantheraConfig.FuriousBite_atkBaseDuration)
            {
                base.EndScript();
            }

            // Multiply the damage if stealthed //
            //if (pantheraObj.GetPassiveScript().stealthed)
            //{
            //    damage *= PantheraConfig.Passive_stealthDamageMultiplier;
            //    new ServerStunTarget(target.gameObject, PantheraConfig.Passive_stealthStunDuration).Send(NetworkDestination.Server);
            //    Utils.Sound.playSound(Utils.Sound.Stun1, gameObject);
            //}

        }

        public override void Stop()
        {

        }

    }
}
