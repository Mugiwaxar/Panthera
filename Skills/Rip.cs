using EntityStates;
using Panthera.Components;
using Panthera.GUI;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.Utils;
using R2API;
using R2API.Networking;
using R2API.Networking.Interfaces;
using Rewired;
using RoR2;
using RoR2.Audio;
using RoR2.Projectile;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Panthera.Skills
{
    class Rip : MachineScript
    {

        //public static float aimVelocity = 0.3f;
        //public static float maxRushDistance = 2;
        //public static float rushDistanceStop = 1;

        public static PantheraSkill SkillDef;

        public OverlapAttack attack;
        public float damageMultiplier;
        public int comboNumber = 1;
        public string swingSoundString = "";
        public string hitSoundString = "";
        public CharacterBody target;
        public float startTime;
        public float baseDuration;
        public bool hasFired = false;

        public Rip()
        {

        }

        public static void Create()
        {
            // Create the Skill //
            PantheraSkill skill = new PantheraSkill();
            skill.skillID = PantheraConfig.Rip_SkillID;
            skill.name = Tokens.RipSkillName;
            skill.desc = Tokens.RipSkillDesc;
            skill.icon = Assets.Rip;
            skill.iconPrefab = ConfigPanel.ActiveSkillPrefab;
            skill.type = PantheraSkill.SkillType.active;
            skill.associatedSkill = typeof(Rip);
            skill.priority = PantheraConfig.Rip_priority;
            skill.interruptPower = PantheraConfig.Rip_interruptPower;
            skill.cooldown = PantheraConfig.Rip_cooldown;
            skill.requiredEnergy = PantheraConfig.Rip_requiredEnergy;

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

            // Set the Cooldown //
            PantheraSkill.SetCooldownTime(SkillDef.skillID, Time.time);

            // Remove the Energy //
            this.characterBody.energy -= SkillDef.requiredEnergy;

            // Save the time //
            this.startTime = Time.time;

            // Create the attack //
            if (comboNumber == 1) this.damageMultiplier = PantheraConfig.Rip_atk1DamageMultiplier;
            else if (comboNumber == 2) this.damageMultiplier = PantheraConfig.Rip_atk2DamageMultiplier;
            else if (comboNumber == 3) this.damageMultiplier = PantheraConfig.Rip_atk3DamageMultiplier;

            this.attack = Utils.Functions.CreateOverlapAttack(base.gameObject, this.damageMultiplier, PantheraConfig.Rip_hitboxName);

            // Set the character to forward //
            base.characterDirection.forward = base.GetAimRay().direction;

            // Get the duration //
            if (comboNumber == 1) this.baseDuration = PantheraConfig.Rip_atk1BaseDuration;
            else if (comboNumber == 2) this.baseDuration = PantheraConfig.Rip_atk2BaseDuration;
            else if (comboNumber == 3) this.baseDuration = PantheraConfig.Rip_atk3BaseDuration;

            // Set the attack //
            this.baseDuration = this.baseDuration / this.attackSpeedStat;
            base.StartAimMode(PantheraConfig.Rip_minimumAimTime + this.baseDuration, false);
            base.characterBody.outOfCombatStopwatch = 0f;
            base.pantheraObj.pantheraMotor.startSprint = false;

        }

        public override void Update()
        {
            
        }

        public override void FixedUpdate()
        {

            // Stop the dash if the target is dead //
            //if (this.target != null && this.target.healthComponent.alive == false)
            //{
            //    this.target = null;
            //}

            // Stop if the duration is reached //
            float skillDuration = Time.time - this.startTime;
            if (skillDuration >= this.baseDuration)
            {
                base.EndScript();
                return;
            }


            // Check if the attack has already fired //
            if (this.hasFired == false)
            {

                // Fire the attack //
                List<HurtBox> enemiesHit = new List<HurtBox>();
                this.hasFired = true;
                this.attack.Fire(enemiesHit);

                // Combo 1 //
                if (this.comboNumber == 1)
                {
                    Sound.playSound(Utils.Sound.Rip1, base.gameObject);
                    base.PlayAnimation("Gesture", "LeftRip");
                    Utils.Functions.SpawnEffect(base.gameObject, Assets.LeftRipFX, base.characterBody.corePosition, 1, base.modelTransform.gameObject, Util.QuaternionSafeLookRotation(base.characterDirection.forward));
                }
                // Combo 2 //
                else if (this.comboNumber == 2)
                {
                    Sound.playSound(Utils.Sound.Rip1, base.gameObject);
                    base.PlayAnimation("Gesture", "RightRip");
                    Utils.Functions.SpawnEffect(base.gameObject, Assets.RightRipFX, base.characterBody.corePosition, 1, base.modelTransform.gameObject, Util.QuaternionSafeLookRotation(base.characterDirection.forward));
                }
                // Combo 3 //
                else if (this.comboNumber == 3)
                {
                    Sound.playSound(Utils.Sound.Rip2, base.gameObject);
                    base.PlayAnimation("Gesture", "FrontRip");
                    Utils.Functions.SpawnEffect(base.gameObject, Assets.FrontRipFX, base.characterBody.corePosition, 1, base.modelTransform.gameObject, Util.QuaternionSafeLookRotation(base.characterDirection.forward));
                }

                // Apply Weak //
                if (enemiesHit != null && enemiesHit.Count > 0)
                {
                    // Add a Combo Point if this is the third combo //
                    if(this.comboNumber == 3)
                    {
                        this.characterBody.comboPoint += 1;
                    }
                    //foreach (HurtBox enemy in enemiesHit)
                    //{
                    //    new ServerApplyWeak(enemy.healthComponent.gameObject, PantheraConfig.Rip_weakDuration).Send(NetworkDestination.Server);
                    //}
                }

            }

        }

        public override void Stop()
        {

            // Increate the combo number //
            this.comboNumber += 1;
            if (this.comboNumber > 3) this.comboNumber = 1;

            // Start the combo //
            if (this.characterBody.energy >= SkillDef.requiredEnergy && base.wasInterrupted == false && base.inputBank.isSkillPressed(SkillDef.skillID)) // && !(base.inputBank.IsDirectionKeyPressed() && base.inputBank.IsKeyPressed(PantheraConfig.SprintKey)))
            {
                this.SetScript(new Rip {comboNumber = this.comboNumber});
            }

        }

    }
}
