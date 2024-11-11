using EntityStates;
using KinematicCharacterController;
using Panthera;
using Panthera.Base;
using Panthera.Components;
using Panthera.GUI;
using Panthera.Machines;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.OldSkills;
using Panthera.Skills;
using Panthera.Utils;
using R2API;
using R2API.Networking;
using R2API.Networking.Interfaces;
using Rewired;
using RoR2;
using RoR2.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering;
using static RoR2.DotController;
using static UnityEngine.ParticleSystem;

namespace Panthera.MachineScripts
{
    public class BigCatPassive : MachineScript
    {

        public float lastNineLivesTime;
        public float lastDamageTime = 0;
        public float lastFuryDecreasedTime = 0;
        public float lastFuryGeneratedTime = 0;
        public float lastShieldGeneratedTime = 0;
        public float lastShieldDamageTime = 0;
        public float destroyedShieldTime = 0;
        public float lastPassivePowerTime = 0;
        public float lastRegenerationTime = 0;
        public float lastDetectionScanTime = 0;
        public bool isSleeping;
        public bool unsleepAsked;
        public float unsleepTime;
        public int pantheraLastLevel;
        public bool isOutOfCombat = true;
        public bool wasOutOfCombat = true;
        public float cupidityTimer = 0;
        public bool wasGod = false;

        public override void Start()
        {

            // Debug the Camera (I don't know what this debug the camera but it works) //
            base.pantheraObj.characterBody.GetComponent<CapsuleCollider>().radius = 0.5f;

            // Register all Events //
            //GlobalEventManager.onCharacterDeathGlobal += OnCharacterDieEventClient;
            GlobalEventManager.onClientDamageNotified += OnDamage;

            // Set all Cooldowns to zero //
            //this.skillLocator.cooldownList.Clear();

            // Add the nine lives buff //
            //new ServerSetBuffCount(this.gameObject, (int)Buff.nineLives.buffIndex, 1).Send(NetworkDestination.Server);

            // Init the HUD //
            //PantheraHUD.Instance.CreateHUD(base.pantheraObj);

            // Create the First spawn Animation //
            if (base.masterObj.firstStarted == false)
            {
                base.masterObj.firstStarted = true;
                this.isSleeping = true;
                Utils.Animation.SetBoolean(base.pantheraObj, "IsSleeping", true);
                Utils.Animation.PlayAnimation(base.pantheraObj, "SleepLoop");
                this.wasGod = base.healthComponent.godMode;
                new ServerSetGodMode(base.gameObject, true).Send(NetworkDestination.Server);
                base.characterBody.RecalculateStats();
            }
            else
            {
                Utils.Animation.SetBoolean(base.pantheraObj, "IsSleeping", false);
            }

            // Apply the Guardian Size //
            //if (CharacterAbilities.getAbilityLevel(PantheraConfig.GuardianAbilityID) > 0)
            //{
            //    float modelScale = PantheraConfig.Guardian_modelScale;
            //    base.pantheraObj.transform.localScale = new Vector3(modelScale, modelScale, modelScale);
            //    base.modelTransform.localScale = new Vector3(modelScale, modelScale, modelScale);
            //    new ServerChangePantheraScale(base.gameObject, modelScale).Send(NetworkDestination.Server);
            //}

            // Save the Last Level //
            this.pantheraLastLevel = Panthera.PantheraCharacter.characterLevel;

        }

        public override void Update()
        {

            // Remove Fury //
            float lastFuryDecrease = Time.time - this.lastFuryDecreasedTime;
            if (base.pantheraObj.furyMode == true && lastFuryDecrease >= base.pantheraObj.furyDecreaseTime)
            {
                this.lastFuryDecreasedTime = Time.time;
                base.characterBody.fury--;
                if (base.characterBody.fury <= 0)
                    Skills.Passives.FuryMode.FuryOff(base.pantheraObj);
            }

            //// Add Energy //
            //base.characterBody.energy += PantheraConfig.Default_EnergyRegen / 60;

            //// Add Stamina //
            //if (base.pantheraObj.dashing == false && base.pantheraObj.detectionActivated == false)
            //    base.characterBody.stamina += PantheraConfig.Default_StaminaRegen / 60;

            // Add Fury //
            //if (CharacterAbilities.getAbilityLevel(PantheraConfig.DestructionAbilityID) > 0)
            //{
            //    if (noDamageTime < PantheraConfig.Fury_damageTimeBeforStopGenerate &&
            //        Time.time - this.lastFuryGeneratedTime > PantheraConfig.Fury_pointsGenerationCooldown)
            //    {
            //        base.characterBody.fury += 1;
            //        this.lastFuryGeneratedTime = Time.time;
            //    }
            //}

            // Add Shield //
            if (base.pantheraObj.getAbilityLevel(PantheraConfig.Guardian_AbilityID) > 0 && base.pantheraObj.frontShieldDeployed == false)
            {
                float lastDamage = Time.time - this.lastShieldDamageTime;
                float lastBroken = Time.time - this.destroyedShieldTime;
                float lastRecharge = Time.time - this.lastShieldGeneratedTime;

                // Check the Improved Shield Ability //
                float rechargeRemoved = 0;
                int improvedShieldAbilityLevel = base.pantheraObj.getAbilityLevel(PantheraConfig.ImprovedShield_AbilityID);
                if (improvedShieldAbilityLevel == 1) rechargeRemoved = PantheraConfig.ImprovedShield_RemovedRechargeDelay1;
                if (improvedShieldAbilityLevel == 2) rechargeRemoved = PantheraConfig.ImprovedShield_RemovedRechargeDelay2;
                if (improvedShieldAbilityLevel == 3) rechargeRemoved = PantheraConfig.ImprovedShield_RemovedRechargeDelay3;

                if (lastDamage > (PantheraConfig.FrontShield_rechargeDelayAfterDamage - rechargeRemoved) && lastBroken > (PantheraConfig.FrontShield_rechargeDelayAfterDestroyed - rechargeRemoved) && lastRecharge > PantheraConfig.FrontShield_rechargeRatetime)
                {
                    float recharge = base.characterBody.maxFrontShield * (PantheraConfig.FrontShield_rechargeRatePercent * PantheraConfig.FrontShield_rechargeRatetime);
                    base.characterBody.frontShield += recharge;
                    this.lastShieldGeneratedTime = Time.time;
                }
            }

            // Apply the Front Shield Color //
            if (base.pantheraObj.frontShieldObj.active == true)
            {
                if (base.characterBody.frontShield / base.characterBody.maxFrontShield > PantheraConfig.FrontShield_criticColorPercent)
                {
                    base.pantheraObj.frontShieldObj.transform.Find("ShieldFX").Find("MagicShieldFX").GetComponent<ParticleSystem>().startColor = PantheraConfig.FrontShieldNormalColor;
                    base.pantheraObj.frontShieldObj.transform.Find("ShieldFX").Find("CenterShield").GetComponent<ParticleSystem>().startColor = PantheraConfig.FrontShieldNormalColor;
                }
                else
                {
                    base.pantheraObj.frontShieldObj.transform.Find("ShieldFX").Find("MagicShieldFX").GetComponent<ParticleSystem>().startColor = PantheraConfig.FrontShieldCriticColor;
                    base.pantheraObj.frontShieldObj.transform.Find("ShieldFX").Find("CenterShield").GetComponent<ParticleSystem>().startColor = PantheraConfig.FrontShieldCriticColor;
                }
            }

            // Apply Passive Power Ability //
            //if (CharacterAbilities.getAbilityLevel(PantheraConfig.PassivePowerAbilityID) > 0 && base.characterBody.power < PantheraConfig.Default_MaxPower)
            //{
            //    if (Time.time - this.lastPassivePowerTime > PantheraConfig.PassivePower_cooldown)
            //    {
            //        base.characterBody.power += 1;
            //        this.lastPassivePowerTime = Time.time;
            //    }
            //}

            // Apply Regeneration Ability //
            //if (CharacterAbilities.getAbilityLevel(PantheraConfig.RegenerationAbilityID) > 0 && base.healthComponent.health < base.characterBody.maxHealth)
            //{
            //    if (Time.time - this.lastRegenerationTime > PantheraConfig.Regeneration_cooldown)
            //    {
            //        float heal = base.characterBody.maxHealth * PantheraConfig.Regeneration_percent1;
            //        new ServerHeal(base.gameObject, heal).Send(NetworkDestination.Server);
            //        this.lastRegenerationTime = Time.time;
            //    }
            //}

            // Apply the Shadow's Master Ability //
            //int shadowsMasterAbilityLevel = CharacterAbilities.getAbilityLevel(PantheraConfig.ShadowsMasterAbilityID);
            //if (shadowsMasterAbilityLevel > 0
            //    && base.characterBody.outOfCombat == true && base.characterBody.outOfDanger == true)
            //{
            //    if (shadowsMasterAbilityLevel == 1 && base.skillLocator.getCooldownInSecond(PantheraConfig.Prowl_SkillID) > PantheraConfig.ShadowsMaster_cooldownReduction1)
            //        base.skillLocator.setCooldownInSecond(PantheraConfig.Prowl_SkillID, PantheraConfig.ShadowsMaster_cooldownReduction1);
            //    if (shadowsMasterAbilityLevel == 2 && base.skillLocator.getCooldownInSecond(PantheraConfig.Prowl_SkillID) > PantheraConfig.ShadowsMaster_cooldownReduction2)
            //        base.skillLocator.setCooldownInSecond(PantheraConfig.Prowl_SkillID, PantheraConfig.ShadowsMaster_cooldownReduction2);
            //    if (shadowsMasterAbilityLevel == 3 && base.skillLocator.getCooldownInSecond(PantheraConfig.Prowl_SkillID) > PantheraConfig.ShadowsMaster_cooldownReduction3)
            //        base.skillLocator.setCooldownInSecond(PantheraConfig.Prowl_SkillID, PantheraConfig.ShadowsMaster_cooldownReduction3);
            //}

            //// Check the Dash //
            //if (base.pantheraObj.dashing == true)
            //{
            //    // Stop Dashing if no more Stamina //
            //    if (PantheraConfig.Dash_staminaConsumed1 / 60.0f > base.characterBody.stamina)
            //        OldPassives.Dash.StopDash(base.pantheraObj);

            //    // Stop Dashing if not running //
            //    if (base.characterBody.isSprinting == false && base.characterMotor.isSprinting == false)
            //        OldPassives.Dash.StopDash(base.pantheraObj);

            //    // Check if still Dashing and consume Stamina //
            //    if (base.pantheraObj.dashing == true)
            //        base.characterBody.stamina -= PantheraConfig.Dash_staminaConsumed1 / 60.0f;
            //}

        }

        public override void FixedUpdate()
        {

            // Check if Sleeping //
            if (this.isSleeping == true && this.unsleepAsked == false)
            {
                if (base.inputBank.moveVector.sqrMagnitude > 0)
                {
                    this.unsleepAsked = true;
                    this.unsleepTime = Time.time;
                    Utils.Animation.SetBoolean(pantheraObj, "IsSleeping", false);
                }
            }

            // Check if must Unsleep //
            if (unsleepAsked == true)
            {
                if (Time.time - this.unsleepTime > PantheraConfig.unsleepDuration)
                {
                    this.unsleepAsked = false;
                    this.isSleeping = false;
                    new ServerSetGodMode(base.gameObject, this.wasGod).Send(NetworkDestination.Server);
                    base.characterBody.RecalculateStats();
                    // Check the Golden Start Ability //
                    if (base.pantheraObj.getAbilityLevel(PantheraConfig.GoldenStart_AbilityID) > 0)
                        new ServerAddGold(base.gameObject, PantheraConfig.GoldenStart_addedGold).Send(NetworkDestination.Server);
                }
            }

            // Stealth the player if he get the Cloak buff from an item //
            if (base.characterBody.hasCloakBuff == true)
            {
                Skills.Passives.Stealth.DoStealth(base.pantheraObj);
            }

            // Check if Out of Combat //
            if (Time.time - this.lastDamageTime > PantheraConfig.Prowl_outOfCombatTime)
            {
                this.isOutOfCombat = true;
            }
            else
            {
                this.isOutOfCombat = false;
            }

            // Check if the Out of Combat text must be displayed //
            if (this.isOutOfCombat == true && this.wasOutOfCombat == false)
            {
                // Set wasOutOfCombat to true //
                this.wasOutOfCombat = true;
                // Create the Out of Combat Text //
                Utils.FXManager.SpawnEffect(base.gameObject, PantheraAssets.OutOfCombatEffectPrefab, base.characterBody.corePosition + Vector3.up, 1, null, default, false, false);
            }
            else if (this.isOutOfCombat == false && this.wasOutOfCombat == true)
            {
                // Set wasOutOfCombat to false //
                this.wasOutOfCombat = false;
            }

            // Decrease Cupidity Buffs //
            if (this.pantheraObj.ambitionMode == false && Time.time - this.cupidityTimer > PantheraConfig.Cupidity_decreaseTime)
            {
                this.cupidityTimer = Time.time;
                int cupidityBuffCount = base.characterBody.GetBuffCount(Base.Buff.CupidityBuff);
                if (cupidityBuffCount > 0)
                    new ServerSetBuffCount(base.gameObject, (int)Base.Buff.CupidityBuff.buffIndex, cupidityBuffCount - 1).Send(NetworkDestination.Server);
            }

            // Check if Ambition must Stop //
            if (this.pantheraObj.ambitionMode == true && Time.time - this.pantheraObj.ambitionTimer > PantheraConfig.Ambition_buffDuration)
                Skills.Passives.AmbitionMode.AmbitionOff(this.pantheraObj);

            // Check the Detection //
            if (base.pantheraObj.detectionMode == true)
            {

                // Increase the Cooldown //
                float detectionCooldown = base.skillLocator.getCooldown(PantheraConfig.Detection_SkillID);
                detectionCooldown += Time.deltaTime * 2;
                base.skillLocator.setCooldown(PantheraConfig.Detection_SkillID, detectionCooldown);

                // Check if Detecion must stop //
                float maxTime = Skills.Passives.Detection.GetDetectionMaxTime(base.pantheraObj);
                if (detectionCooldown > maxTime)
                    Skills.Passives.Detection.DisableDetection(base.pantheraObj);

                // Check if Rescan //
                if (Time.time - this.lastDetectionScanTime > PantheraConfig.Detection_scanRate && base.pantheraObj.detectionMode == true)
                {
                    this.lastDetectionScanTime = Time.time;
                    Skills.Passives.Detection.ReScanBody(base.pantheraObj);
                }

            }

            // Apply the Enrage Buff //
            int enrageCount = base.characterBody.GetBuffCount(Base.Buff.EnrageBuff);
            if (enrageCount > 0)
            {
                float generatedFury = base.characterBody.maxFury * PantheraConfig.Enrage_furyPercent * enrageCount / 60;
                base.characterBody.trueFury += generatedFury;
            }

            // Apply nine lives buff if needed //
            //float nineLivesTime = Time.time - this.lastNineLivesTime;
            //if (this.characterBody.HasBuff(Buff.nineLives) == false && nineLivesTime > PantheraConfig.Passive_nineLivesCooldown)
            //{
            //    new ServerSetBuffCount(this.gameObject, (int)Buff.nineLives.buffIndex, 1).Send(NetworkDestination.Server);
            //}

            // If the prey must be released //
            //if (base.pantheraObj.holdedPrey != null && base.inputBank.IsKeyDown(PantheraConfig.InteractKey))
            //{
            //    HoldedPrey holdedPrey = base.pantheraObj.holdedPrey.GetComponent<HoldedPrey>();
            //    if (holdedPrey != null)
            //    {
            //        GameObject.Destroy(base.pantheraObj.holdedPrey.GetComponent<HoldedPrey>(), PantheraConfig.KeepThePrey_destroyComponentDelay);
            //        base.pantheraObj.holdedPrey.GetComponent<HoldedPrey>().released = true;
            //        if (NetworkServer.active == false) new ServerReleasePrey(base.gameObject, base.pantheraObj.holdedPrey.gameObject, base.pantheraObj.holdedPrey.transform.position).Send(NetworkDestination.Server);
            //        base.pantheraObj.holdedPrey = null;
            //    }
            //}

            // Check the Shield //
            //Shield.CheckShieldState(base.characterBody, base.pantheraFX);

        }

        public override void Stop()
        {
            // Stop all Events //
            //GlobalEventManager.onCharacterDeathGlobal -= OnCharacterDieEventClient;
            GlobalEventManager.onClientDamageNotified -= OnDamage;
        }

        public void OnDamage(DamageDealtMessage damageDealtMessage)
        {
            // Check the character //
            if (base.gameObject == null) return;
            if (damageDealtMessage.attacker == base.gameObject) this.OnDamageDone(damageDealtMessage);
            if (damageDealtMessage.victim == base.gameObject) this.OnDamageReceived(damageDealtMessage);
        }

        public void OnDamageDone(DamageDealtMessage damageDealtMessage)
        {

            // Check all values //
            if (damageDealtMessage.attacker == null || damageDealtMessage.victim == null || damageDealtMessage.victim == base.gameObject) return;

            // Get all values //
            float damage = damageDealtMessage.damage;
            bool crit = damageDealtMessage.crit;
            DamageType damageType = damageDealtMessage.damageType;

            // Set the Predator Component has damaged by this Player //
            PredatorComponent predComp = damageDealtMessage.victim.GetComponent<PredatorComponent>();
            if (predComp != null)
                predComp.damaged = true;

            // Set the last damage time //
            this.lastDamageTime = Time.time;

        }

        public void OnDamageReceived(DamageDealtMessage damageDealtMessage)
        {

            // Set the last damage time //
            this.lastDamageTime = Time.time;

            // Stop the Stealth //
            Skills.Passives.Stealth.TookDamageUnstealth(base.pantheraObj);

            // Apply the Inner Rage Ability //
            int innerRageLvl = base.pantheraObj.getAbilityLevel(PantheraConfig.InnerRage_AbilityID);
            if (innerRageLvl == 1)
                base.characterBody.trueFury += damageDealtMessage.damage * PantheraConfig.InnerRage_addedFuryPercent1;
            else if (innerRageLvl == 2)
                base.characterBody.trueFury += damageDealtMessage.damage * PantheraConfig.InnerRage_addedFuryPercent2;
            else if (innerRageLvl == 3)
                base.characterBody.trueFury += damageDealtMessage.damage * PantheraConfig.InnerRage_addedFuryPercent3;
            else if (innerRageLvl == 4)
                base.characterBody.trueFury += damageDealtMessage.damage * PantheraConfig.InnerRage_addedFuryPercent4;

            // Apply the Inner Rage Mastery //
            if (base.pantheraObj.isMastery(PantheraConfig.InnerRage_AbilityID))
            {
                float enrageChance = base.characterBody.mastery;
                float enrageTime = base.pantheraObj.furyMode == true ? PantheraConfig.InnerRage_enrageTimeFuryMode : PantheraConfig.InnerRage_enrageTime;
                float enrageRand = UnityEngine.Random.Range(0, 100);
                if (enrageRand <= enrageChance)
                {
                    new ServerAddBuff(base.gameObject, base.gameObject, Base.Buff.EnrageBuff, 1, enrageTime).Send(NetworkDestination.Server);
                    Utils.Sound.playSound(Utils.Sound.Enrage, base.gameObject);
                }

            }

        }

        public void OnCharacterDieEventClient(GameObject attacker, GameObject victim)
        {
            if (attacker == null || victim == null) return;
            if (victim.GetComponent<TeamComponent>() == null || victim.GetComponent<TeamComponent>().teamIndex == TeamIndex.Player) return;
            OnEnemyDie(attacker, victim);
        }

        public void OnEnemyDie(GameObject attacker, GameObject victim)
        {

            // Get the Victime Body //
            CharacterBody victimBody = victim.GetComponent<CharacterBody>();

            // Get the Predator Component //
            PredatorComponent predComp = victim.GetComponent<PredatorComponent>();

            // Check the Predator Component //
            if (predComp == null) return;

            // Add XP to P4N7H3RA //
            if (predComp.damaged == true)
            {

                // Calculate the amount //
                int amount = (int)victimBody.level;
                if (victimBody.isElite == true) amount *= 2;
                if (victimBody.isBoss == true) amount *= 5;

                // Add the Experience //
                Panthera.PantheraCharacter.totalExperience += Math.Max(amount, 1);

                // Check if Level UP //
                if (Panthera.PantheraCharacter.characterLevel != this.pantheraLastLevel)
                {
                    this.pantheraLastLevel = Panthera.PantheraCharacter.characterLevel;
                    Utils.Sound.playSound(Utils.Sound.LevelUP, base.gameObject);
                    Utils.FXManager.SpawnEffect(base.gameObject, PantheraAssets.LevelUPFX, modelTransform.position, pantheraObj.actualModelScale, base.characterBody.gameObject, base.modelTransform.rotation, true);
                }
            }

            // Add the Cupidity Buff //
            if (predComp.damaged == true)
            {
                Skills.Passives.AmbitionMode.OnEnemyDie(base.pantheraObj);
            }

            // Apply the Mortal Mirage Debuff //
            if (Time.time - predComp.lastStealthStrikeTime < PantheraConfig.MortalMirage_duration)
            {
                new ServerAddBuff(base.gameObject, base.gameObject, Buff.EclipseBuff).Send(NetworkDestination.Server);
                base.skillLocator.setCooldown(PantheraConfig.Prowl_SkillID, 0);
            }

        }

    }

}
