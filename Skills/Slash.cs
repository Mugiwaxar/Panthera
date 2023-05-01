using HG;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.Utils;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using static RoR2.BlastAttack;

namespace Panthera.Skills
{
    public class Slash : MachineScript
    {

        public float startTime;
        public BlastAttack attack;
        public bool hasFired = false;
        public bool isFire = false;

        public Slash()
        {

        }

        public static void Create()
        {
            // Create the Skill //
            PantheraSkill skill = new PantheraSkill();
            skill.skillID = PantheraConfig.Slash_SkillID;
            skill.name = "SLASH_SKILL_NAME";
            skill.desc = "SLASH_SKILL_DESC";
            skill.icon = Assets.Slash;
            skill.iconPrefab = Assets.ActiveSkillPrefab;
            skill.type = PantheraSkill.SkillType.active;
            skill.associatedSkill = typeof(Slash);
            skill.priority = PantheraConfig.Slash_priority;
            skill.interruptPower = PantheraConfig.Slash_interruptPower;
            skill.cooldown = PantheraConfig.Slash_Cooldown;
            skill.requiredEnergy = PantheraConfig.Slash_energyRequired;

            // Save this Skill //
            PantheraSkill.SkillDefsList.Add(skill.skillID, skill);

        }

        public override PantheraSkill getSkillDef()
        {
            return base.pantheraObj.activePreset.getSkillByID(PantheraConfig.Slash_SkillID);
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

            // Create the Attack //
            bool isCrit = base.RollCrit();
            float damage = base.characterBody.damage * base.pantheraObj.activePreset.circularSaw_damage;
            float scale = PantheraConfig.Slash_radius * base.pantheraObj.modelScale;
            this.attack = Utils.Functions.CreateBlastAttack(base.gameObject, damage, BlastAttack.FalloffModel.None, isCrit, base.characterBody.corePosition, scale, default(Vector3), Assets.RipHitFX);

            // Check Ignition level //
            if (base.pantheraObj.activePreset.getAbilityLevel(PantheraConfig.IgnitionAbilityID) > 0)
                this.isFire = true;

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
            if (skillDuration >= (PantheraConfig.Slash_duration / this.attackSpeedStat))
            {
                this.machine.EndScript();
                return;
            }

            // Fire the Attack //
            if (this.hasFired == false)
            {

                // Fire the attack //
                this.hasFired = true;
                BlastAttack.Result result = this.attack.Fire();
                List<BlastAttack.HitPoint> enemiesHit = result.hitPoints.ToList();
                Sound.playSound(Utils.Sound.Slash, base.gameObject);
                if (this.isFire == true) Sound.playSound(Utils.Sound.FireRip1, base.gameObject);
                Utils.FXManager.SpawnEffect(base.gameObject, Assets.SlashFX, base.characterBody.corePosition, 1, base.characterBody.gameObject, base.modelTransform.rotation, true);
                if (this.isFire == true) Utils.FXManager.SpawnEffect(base.gameObject, Assets.FireSlashFX, base.characterBody.corePosition, base.pantheraObj.modelScale, null, base.modelTransform.rotation);
                if (base.pantheraObj.slashComboNumber == 1) base.PlayAnimation("LeftSlash");
                if (base.pantheraObj.slashComboNumber == 2) base.PlayAnimation("RightSlash");
                // Check Enemies Hit //               
                if (enemiesHit != null && result.hitCount > 0)
                {
                    List<GameObject> enemiesHurt = new List<GameObject>();
                    foreach (HitPoint enemy in enemiesHit)
                    {

                        // Get the Enemy //
                        HealthComponent hc = enemy.hurtBox?.healthComponent;
                        if (hc == null) continue;
                        if (enemiesHurt.Contains(hc.gameObject)) continue;
                        enemiesHurt.Add(hc.gameObject);

                        // Play the Hit Sound //
                        Utils.Sound.playSound(Utils.Sound.Slash, hc.gameObject);

                        // Add a Fury Point //
                        base.characterBody.fury += 1;

                        // Check the Souls Shelter Ability //
                        if (base.pantheraObj.activePreset.getAbilityLevel(PantheraConfig.SoulsShelterAbilityID) > 0)
                        {
                            base.characterBody.power += 1;
                        }

                        // Check The Slash-per Ability //
                        if (base.pantheraObj.activePreset.getAbilityLevel(PantheraConfig.TheSlashPerAbilityID) > 0)
                            Passives.Ripper.AddBuff(base.pantheraObj);

                    }
                }

                // Set Enemies on Fire //
                if (this.isFire == true)
                {

                    // Get the Config //
                    float fireDamage = base.pantheraObj.activePreset.ignition_damage;
                    float fireDuration = base.pantheraObj.activePreset.ignition_duration;

                    // Get all Characters //
                    Collider[] colliders = Physics.OverlapSphere(player.transform.position, base.pantheraObj.activePreset.ignition_radius, LayerIndex.entityPrecise.mask.value);

                    // Itinerate all Characters found //
                    List<GameObject> charactersFound = new List<GameObject>();
                    foreach (Collider collider in colliders)
                    {
                        HurtBox hb = collider.GetComponent<HurtBox>();
                        if (hb == null) continue;
                        HealthComponent hc = hb.healthComponent;
                        if (hc == null) continue;
                        if (charactersFound.Contains(hc.gameObject)) continue;
                        charactersFound.Add(hc.gameObject);
                        TeamComponent tc = hc?.body?.teamComponent;
                        // Apply the Burn //
                        if (tc != null && tc.teamIndex == TeamIndex.Monster)
                            new ServerInflictDot(base.gameObject, hc.gameObject, PantheraConfig.BurnDotIndex, fireDuration, fireDamage).Send(NetworkDestination.Server);
                        // Apply the Sacred Flames Ability //
                        if (tc != null && tc.body != null && tc.teamIndex == TeamIndex.Player)
                        {
                            float heal = tc.body.maxHealth * base.pantheraObj.activePreset.sacredFlames_healMultiplier;
                            if (heal > 0)
                                new ServerHeal(tc.gameObject, Math.Max(1, heal)).Send(NetworkDestination.Server);
                        }

                    }

                }

            }

        }

        public override void Stop()
        {
            // Change the combo number //
            if (base.pantheraObj.slashComboNumber == 1)
                base.pantheraObj.slashComboNumber = 2;
            else if (base.pantheraObj.slashComboNumber == 2)
                base.pantheraObj.slashComboNumber = 1;
        }

    }
}
