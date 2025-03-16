using EntityStates;
using Panthera;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.GUI;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.OldSkills;
using Panthera.Skills;
using Panthera.Skills.Actives;
using Panthera.Utils;
using R2API;
using R2API.Networking;
using R2API.Networking.Interfaces;
using Rewired;
using RoR2;
using RoR2.Audio;
using RoR2.Orbs;
using RoR2.Projectile;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.ParticleSystem.PlaybackState;

namespace Panthera.Skills.Actives
{
    class Rip : MachineScript
    {

        //public static float aimVelocity = 0.3f;
        //public static float maxRushDistance = 2;
        //public static float rushDistanceStop = 1;

        //public OverlapAttack attack;
        public float damageMultiplier = PantheraConfig.Rip_atkDamageMultiplier;
        public string soundName = Utils.Sound.Rip1;
        public bool isGhostRip = false;
        public bool isGoldenRip = false;
        public int comboNumber = 1;
        public float startTime;
        public float baseDuration = PantheraConfig.Rip_atkDuration;
        public float attackTime = PantheraConfig.Rip_attackTime;
        public int furyAdded = PantheraConfig.Rip_furyAdded;
        public bool hasFired = false;
        public int infernalSwipeLvl = 0;

        public Rip()
        {
            base.icon = PantheraAssets.RipSkill;
            base.name = PantheraTokens.Get("skill_RipName");
            base.baseCooldown = PantheraConfig.Rip_cooldown;
            base.desc1 = string.Format(PantheraTokens.Get("skill_RipDesc"), PantheraConfig.Rip_atkDamageMultiplier * 100) + string.Format(PantheraTokens.Get("skill_RipFuryDesc"), PantheraConfig.Rip_furyAdded);
            base.desc2 = string.Format(PantheraTokens.Get("skill_AirCleaveDesc"), PantheraConfig.AirCleave_attackDamageMultiplier * 100) + string.Format(PantheraTokens.Get("skill_AirCleaveFuryDesc"), PantheraConfig.AirCleave_furyAdded);
            base.skillID = PantheraConfig.Rip_SkillID;
            base.priority = PantheraConfig.Rip_priority;
            base.interruptPower = PantheraConfig.Rip_interruptPower;
        }

        public override bool CanBeUsed(PantheraObj ptraObj)
        {
            if (ptraObj.skillLocator.getStock(PantheraConfig.Rip_SkillID) <= 0) return false;
            return true;
        }


        public override void Start()
        {

            // Set the Fake Skill Cooldown //
            //base.skillLocator.primary.DeductStock(1);

            // Launch the Fake Skill //
            base.characterBody.OnSkillActivated(base.skillLocator.primary);

            // Unhide the Crosshair //
            base.pantheraObj.crosshairComp.unHideCrosshair();

            // Check if Ghost Rip //
            if (base.pantheraObj.stealthed == true && base.pantheraObj.profileComponent.getAbilityLevel(PantheraConfig.GhostRip_AbilityID) > 0)
            {
                this.isGhostRip = true;
                this.damageMultiplier = PantheraConfig.GhostRip_damageMultiplier;
                this.furyAdded = PantheraConfig.GhostRip_furyAdded;
                this.soundName = Utils.Sound.GhostRip;
            }

            // Check if Golden Rip //
            if (base.pantheraObj.ambitionMode == true && base.pantheraObj.profileComponent.getAbilityLevel(PantheraConfig.GoldenRip_AbilityID) > 0)
            {
                this.isGoldenRip = true;
                this.damageMultiplier = PantheraConfig.GoldenRip_DamageMultiplier;
                this.furyAdded = PantheraConfig.GoldenRip_furyAdded;
            }

            // Check if Infernal Swipe //
            if (base.pantheraObj.furyMode == true)
                this.infernalSwipeLvl = base.pantheraObj.profileComponent.getAbilityLevel(PantheraConfig.InfernalSwipe_AbilityID);

            // Scan if no Enemies //
            bool enemyFound = false;
            Collider[] colliders = Physics.OverlapSphere(characterBody.corePosition + characterDirection.forward * PantheraConfig.Rip_enemiesForwardScanDistance, PantheraConfig.Rip_enemiesScanRadius, LayerIndex.entityPrecise.mask.value);
            foreach (Collider collider in colliders)
            {
                HurtBox hurtbox = collider.GetComponent<HurtBox>();
                if (hurtbox != null && hurtbox.healthComponent != null && hurtbox.healthComponent.GetComponent<TeamComponent>() != null && hurtbox.healthComponent.GetComponent<TeamComponent>().teamIndex != TeamIndex.Player)
                {
                    enemyFound = true;
                    break;
                }
            }
            if (enemyFound == false)
            {
                this.machine.SetScript((MachineScript)this.pantheraObj.characterSkills.getSkillByID(PantheraConfig.AirCleave_SkillID).Clone());
                return;
            }

            // Save the time //
            this.startTime = Time.time;

            // Stop the Sprint //
            base.pantheraObj.pantheraMotor.isSprinting = false;

            // Start the Aim Mode //
            base.StartAimMode(PantheraConfig.Rip_minimumAimTime + this.baseDuration, false);

            // Set the character to forward //
            base.characterDirection.forward = GetAimRay().direction;

            // Set the attack speed //
            this.baseCooldown /= base.attackSpeedStat;
            this.baseDuration = this.baseDuration / base.attackSpeedStat;
            this.attackTime = this.attackTime / base.attackSpeedStat;

            // Set the Cooldown //
            base.skillLocator.startCooldown(PantheraConfig.Rip_SkillID, base.baseCooldown);

            // Get the Combo Number //
            this.comboNumber = base.pantheraObj.attackNumber;

            // Play the Animation //
            string animString = "LeftRip";
            if (this.comboNumber == 2)
                animString = "RightRip";
            PlayAnimation(animString, 0.2f);

        }

        public override void Update()
        {

        }

        public override void FixedUpdate()
        {

            // Stop if the duration is reached //
            float skillDuration = Time.time - startTime;
            if (skillDuration >= this.baseDuration && this.hasFired == true)
            {
                base.EndScript();
                return;
            }

            if (this.hasFired == false && skillDuration >= PantheraConfig.Rip_attackTime)
            {

                // Set fired //
                this.hasFired = true;

                // Get the Sharpened Fangs multiplicator //
                float sharpenedFangsMult = 1;
                if (base.getAbilityLevel(PantheraConfig.SharpenedFangs_AbilityID) == 1)
                    sharpenedFangsMult += PantheraConfig.SharpenedFangs_damagePercent1;
                else if (base.getAbilityLevel(PantheraConfig.SharpenedFangs_AbilityID) == 2)
                    sharpenedFangsMult += PantheraConfig.SharpenedFangs_damagePercent2;
                else if (base.getAbilityLevel(PantheraConfig.SharpenedFangs_AbilityID) == 3)
                    sharpenedFangsMult += PantheraConfig.SharpenedFangs_damagePercent3;

                // Get the Hit FX //
                GameObject hitFX = PantheraAssets.RipHitFX;
                if (this.isGhostRip == true)
                    hitFX = PantheraAssets.GhostRipHitFX;
                else if (this.isGoldenRip == true)
                    hitFX = PantheraAssets.GoldenRipHitFX;

                // Create the Attack //
                float critChance = this.isGhostRip == true ? base.critStat * 2 : base.critStat;
                bool isCrit = Util.CheckRoll(critChance, base.characterBody.master);
                float damage = base.characterBody.damage * this.damageMultiplier * sharpenedFangsMult;
                OverlapAttack attack = Functions.CreateOverlapAttack(base.gameObject, damage, isCrit, PantheraConfig.Rip_procCoefficient, PantheraConfig.Rip_hitboxName, default, 1, hitFX);

                // Fire the attack //
                List<HurtBox> enemiesHit = new List<HurtBox>();
                attack.Fire(enemiesHit);

                // Do a small Dash //
                base.Dash(PantheraConfig.Rip_dashForce);

                // Add camera recoil
                if (this.comboNumber == 1)
                    base.AddRecoil(0, 0, 1, 1.5f);
                else
                    base.AddRecoil(0, 0, -1.5f, -1);

                // Play the Sound //
                Sound.playSound(this.soundName, base.gameObject);

                // Get the Effect //
                GameObject effect = PantheraAssets.LeftRipFX;
                if (this.isGhostRip == true && comboNumber == 1)
                    effect = PantheraAssets.GhostLeftRipFX;
                else if (this.isGhostRip == true && comboNumber == 2)
                    effect = PantheraAssets.GhostRightRipFX;
                else if (this.isGoldenRip == true && comboNumber == 1)
                    effect = PantheraAssets.GoldenLeftRipFX;
                else if (this.isGoldenRip == true && comboNumber == 2)
                    effect = PantheraAssets.GoldenRightRipFX;
                else if (this.comboNumber == 2)
                    effect = PantheraAssets.RightRipFX;

                // Spawn the Effect //
                FXManager.SpawnEffect(base.gameObject, effect, base.characterBody.corePosition, 1, base.characterBody.gameObject, base.modelTransform.rotation, true);

                // Apply the Infernal Swipe //
                if (this.infernalSwipeLvl > 0)
                {
                    // Get the Effect //
                    GameObject fireEffect = PantheraAssets.FireRipLeftFX;
                    if (comboNumber == 2)
                        fireEffect = PantheraAssets.FireRipRightFX;
                    // Spawn the Effect //
                    FXManager.SpawnEffect(base.gameObject, fireEffect, base.characterBody.corePosition, base.pantheraObj.actualModelScale, null, base.modelTransform.rotation);
                    // Play the sound //
                    Sound.playSound(Utils.Sound.FireRip1, base.gameObject);
                }

                // Get Razors Buff count //
                int razorsCount = base.characterBody.GetBuffCount(Buff.RazorsBuff);

                // Check if Enemies was Hit //
                if (enemiesHit != null && enemiesHit.Count > 0)
                {

                    // Set in combat //
                    base.characterBody.outOfCombatStopwatch = 0f;

                    // Check all Enemies //
                    List<GameObject> enemiesHurt = new List<GameObject>();
                    foreach (HurtBox enemy in enemiesHit)
                    {

                        // Get the Health Component //
                        HealthComponent hc = enemy.healthComponent;
                        if (hc == null) continue;
                        if (enemiesHurt.Contains(hc.gameObject)) continue;
                        enemiesHurt.Add(hc.gameObject);

                        // Play the sound //
                        Sound.playSound(Sound.RipHit1, hc.gameObject);

                        // Add Fury //
                        if (base.pantheraObj.profileComponent.getAbilityLevel(PantheraConfig.Fury_AbilityID) > 0)
                            base.characterBody.fury += this.furyAdded;

                        // Move the target //
                        Vector3 pushDirection = Vector3.zero;
                        if (comboNumber == 1)
                            pushDirection = base.modelTransform.transform.right * PantheraConfig.Rip_pushForce;
                        else
                            pushDirection = (-base.modelTransform.transform.right) * PantheraConfig.Rip_pushForce;
                        new ServerApplyForceToBody(hc.gameObject, pushDirection + base.characterDirection.forward).Send(NetworkDestination.Server);

                        // Apply Ghost Rip //
                        if (this.isGhostRip)
                        {
                            new ServerStunTarget(enemy.healthComponent.gameObject, PantheraConfig.GhostRip_stunDuration).Send(NetworkDestination.Server);
                            if (base.pantheraObj.profileComponent.getAbilityLevel(PantheraConfig.StealthStrike_AbilityID) > 0 && isCrit == true)
                            {
                                if (hc.alive == false)
                                {
                                    new ServerAddBuff(base.gameObject, base.gameObject, Buff.EclipseBuff).Send(NetworkDestination.Server);
                                    base.skillLocator.setCooldown(PantheraConfig.Prowl_SkillID, 0);
                                }
                                else
                                {
                                    new ServerAddBuff(base.gameObject, hc.gameObject, Buff.MortalMirageDebuff).Send(NetworkDestination.Server);
                                    hc.gameObject.GetComponent<PredatorComponent>().lastStealthStrikeTime = Time.time;
                                }
                            }
                        }

                        // Apply Golder Rip //
                        if (this.isGoldenRip)
                        {
                            Sound.playSound(Utils.Sound.GoldenRip, hc.gameObject);
                            int moneyAdded = 0;
                            int glodenRipLevel = base.pantheraObj.profileComponent.getAbilityLevel(PantheraConfig.GoldenRip_AbilityID);
                            if (glodenRipLevel == 1)
                                moneyAdded = PantheraConfig.GoldenRip_addedCoin1;
                            else if (glodenRipLevel == 2)
                                moneyAdded = PantheraConfig.GoldenRip_addedCoin2;
                            else if (glodenRipLevel == 3)
                                moneyAdded = PantheraConfig.GoldenRip_addedCoin3;
                            new ServerSpawnGoldOrb(hc.gameObject, base.gameObject, moneyAdded).Send(NetworkDestination.Server);
                        }

                        // Apply the Razors Buff //
                        if (razorsCount > 0)
                        {
                            razorsCount--;
                            new ServerAddBuff(base.gameObject, hc.gameObject, Buff.BleedOutDebuff).Send(NetworkDestination.Server);
                        }

                        // Apply Infernal Swipe //
                        if (this.infernalSwipeLvl > 0)
                        {
                            float fireDamage = damage;
                            if (infernalSwipeLvl == 1)
                                fireDamage *= PantheraConfig.InfernalSwipe_damagePercent1;
                            if (infernalSwipeLvl == 2)
                                fireDamage *= PantheraConfig.InfernalSwipe_damagePercent2;
                            new ServerInflictDamage(base.gameObject, hc.gameObject, enemy.transform.position, fireDamage, isCrit, PantheraConfig.InfernalSwipe_procCoefficient, DamageType.Generic, DamageColorIndex.WeakPoint).Send(NetworkDestination.Server);
                            float ignitionChance = 0;
                            if (infernalSwipeLvl == 1)
                                ignitionChance = PantheraConfig.InfernalSwipe_ingnitionChance1 * 100;
                            else if (infernalSwipeLvl == 2)
                                ignitionChance = PantheraConfig.InfernalSwipe_ingnitionChance2 * 100;
                            float ignitionRand = UnityEngine.Random.Range(0, 100);
                            if (ignitionRand < ignitionChance)
                                new ServerAddBuff(base.gameObject, hc.gameObject, Buff.IgnitionDebuff).Send(NetworkDestination.Server);
                        }

                    }
                }

                // Set the Razors Buff count //
                new ServerSetBuffCount(base.gameObject, (int)Buff.RazorsBuff.buffIndex, razorsCount).Send(NetworkDestination.Server);

            }

        }

        public override void Stop()
        {

            // Hide the Crosshair //
            base.pantheraObj.crosshairComp.delayHideCrosshair(PantheraConfig.Rip_hideCrosshairTime);

        }

    }
}
