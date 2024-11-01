using HG;
using Panthera;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.OldSkills;
using Panthera.Skills.Actives;
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

namespace Panthera.Skills.Actives
{
    public class Slash : MachineScript
    {

        public int comboNumber = 1;
        public float startTime;

        public Slash()
        {
            base.icon = PantheraAssets.SlashSkill;
            base.name = PantheraTokens.Get("skill_SlashName");
            base.baseCooldown = PantheraConfig.Slash_cooldown;
            base.desc1 = string.Format(PantheraTokens.Get("skill_SlashDesc"), PantheraConfig.Slash_damageMultiplier * 100) + string.Format(PantheraTokens.Get("skill_SlashFuryDesc"), PantheraConfig.Slash_furyAdded);
            base.desc2 = null;
            base.skillID = PantheraConfig.Slash_SkillID;
            base.priority = PantheraConfig.Slash_priority;
            base.interruptPower = PantheraConfig.Slash_interruptPower;
        }


        public override bool CanBeUsed(PantheraObj ptraObj)
        {
            if (ptraObj.skillLocator.getStock(PantheraConfig.Slash_SkillID) <= 0) return false;
            return true;
        }

        public override void Start()
        {

            // Set the cooldown //
            base.skillLocator.startCooldown(PantheraConfig.Slash_SkillID);

            // Set the Fake Skill Cooldown //
            //base.skillLocator.secondary.DeductStock(1);

            // Launch the Fake Skill //
            base.characterBody.OnSkillActivated(base.skillLocator.secondary);

            // Save the time //
            this.startTime = Time.time;

            // Get the Combo Number //
            this.comboNumber = base.pantheraObj.attackNumber;

            // Create the Attack //
            bool isCrit = RollCrit();
            float damage = base.characterBody.damage * PantheraConfig.Slash_damageMultiplier;
            float scale = PantheraConfig.Slash_radius * base.pantheraObj.modelScale;
            BlastAttack attack = Functions.CreateBlastAttack(base.gameObject, damage, FalloffModel.None, isCrit, base.characterBody.corePosition, scale, default, PantheraAssets.RipHitFX);

            // Get the Result //
            Result result = attack.Fire();

            // Create the List //
            List<HitPoint> enemiesHit = result.hitPoints.ToList();

            // Play the Sound //
            Sound.playSound(Sound.Slash, base.gameObject);

            // Spawn the Effect //
            FXManager.SpawnEffect(base.gameObject, PantheraAssets.SlashFX, base.characterBody.corePosition, 1, base.characterBody.gameObject, base.modelTransform.rotation, true);

            // Play the Animation //
            if (this.comboNumber == 1) base.PlayAnimation("LeftSlash");
            if (this.comboNumber == 2) base.PlayAnimation("RightSlash");

            // Check the Enemies Hit //               
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
                    Sound.playSound(Sound.Slash, hc.gameObject);
                    // Add Fury Point //
                    if (base.pantheraObj.GetAbilityLevel(PantheraConfig.Fury_AbilityID) > 0)
                        base.characterBody.fury += PantheraConfig.Slash_furyAdded;
                    // Add the Razors Buff //
                    if (base.pantheraObj.GetAbilityLevel(PantheraConfig.ClawsSharpening_AbilityID) > 0 && base.characterBody.GetBuffCount(Buff.RazorsBuff) < PantheraConfig.Tenacity_maxStacks)
                        new ServerAddBuff(base.gameObject, base.gameObject, Buff.RazorsBuff).Send(NetworkDestination.Server);
                }
            }

        }

        public override void Update()
        {

        }

        public override void FixedUpdate()
        {
            // Get the total duration //
            float duration = Time.time - startTime;
            // Stop if the duration is reached //
            float skillDuration = Time.time - startTime;
            if (skillDuration >= PantheraConfig.Slash_duration / base.attackSpeedStat)
            {
                base.machine.EndScript();
                return;
            }
        }

        public override void Stop()
        {

        }

    }
}
