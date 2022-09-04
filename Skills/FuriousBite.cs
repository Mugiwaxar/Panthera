using EntityStates;
using HarmonyLib;
using KinematicCharacterController;
using Panthera;
using Panthera.Components;
using Panthera.GUI;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.OldSkills;
using Panthera.Skills;
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

namespace Panthera.Skills
{
    class FuriousBite : MachineScript
    {

        public static PantheraSkill SkillDef;

        public float startTime;

        public FuriousBite()
        {

        }

        public static void Create()
        {
            // Create the Skill //
            PantheraSkill skill = new PantheraSkill();
            skill.skillID = PantheraConfig.FuriousBite_SkillID;
            skill.name = Tokens.FuriousBiteSkillName;
            skill.desc = Tokens.FuriousBiteSkillDesc;
            skill.icon = Assets.FuriousBite;
            skill.iconPrefab = ConfigPanel.ActiveSkillPrefab;
            skill.type = PantheraSkill.SkillType.active;
            skill.associatedSkill = typeof(FuriousBite);
            skill.priority = PantheraConfig.FuriousBite_priority;
            skill.interruptPower = PantheraConfig.FuriousBite_interruptPower;
            skill.cooldown = PantheraConfig.FuriousBite_cooldown;
            skill.requiredEnergy = PantheraConfig.FuriousBite_energyRequired;

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
            // Get the Skill Def //
            SkillDef = PantheraSkill.SkillDefsList[PantheraConfig.FuriousBite_SkillID];

            // Set the start time //
            startTime = Time.time;

            // Find a Target //
            CharacterBody target = null;
            float minTargetDistance = 999;
            Collider[] colliders = Physics.OverlapSphere(base.characterBody.corePosition + base.characterDirection.forward, PantheraConfig.FuriousBite_maxDistanceToActivate, LayerIndex.entityPrecise.mask.value);
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

            // Spawn the effect //
            Utils.Functions.SpawnEffect(gameObject, Assets.BiteFX, base.characterBody.corePosition, PantheraConfig.Model_generalScale, null, Util.QuaternionSafeLookRotation(characterDirection.forward));

            // Calcule the damages //
            int cpUsed = Math.Min((int)PantheraConfig.FuriousBite_maxComboPointUsed, (int)base.characterBody.comboPoint);
            float damageAdded = cpUsed * PantheraConfig.FuriousBite_ComboPointMultiplier;
            float damage = damageStat * (PantheraConfig.FuriousBite_atkDamageMultiplier + damageAdded);

            // Tell the server to inflict damage //
            new ServerInflictDamage(gameObject, target.gameObject, characterBody.corePosition, damage, RollCrit(), PantheraConfig.FuriousBite_atkDamageType).Send(NetworkDestination.Server);

            // Set the Cooldown //
            PantheraSkill.SetCooldownTime(SkillDef.skillID, Time.time);

            // Remove the Energy //
            this.characterBody.energy -= SkillDef.requiredEnergy;

            // Remove the Combo Point //
            this.characterBody.comboPoint -= cpUsed;

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

            // Multiply the damage if stealed //
            //if (pantheraObj.GetPassiveScript().stealed)
            //{
            //    damage *= PantheraConfig.Passive_stealDamageMultiplier;
            //    new ServerStunTarget(target.gameObject, PantheraConfig.Passive_stealStunDuration).Send(NetworkDestination.Server);
            //    Utils.Sound.playSound(Utils.Sound.Stun1, gameObject);
            //}

        }

        public override void Stop()
        {

        }

    }
}
