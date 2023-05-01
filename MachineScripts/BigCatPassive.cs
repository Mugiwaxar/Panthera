using EntityStates;
using KinematicCharacterController;
using Panthera;
using Panthera.Components;
using Panthera.GUI;
using Panthera.Machines;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.Passives;
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
using System.Text;
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
        public float lastFadeLevel;
        public bool wasStealthed;

        public override void Start()
        {

            // Debug the Camera (I don't know what this debug the camera but it works) //
            base.pantheraObj.characterBody.GetComponent<CapsuleCollider>().radius = 0.5f;

            // Register all Events //
            GlobalEventManager.onCharacterDeathGlobal += OnCharacterDieEventClient;
            GlobalEventManager.onClientDamageNotified += OnDamage;

            // Set all Cooldowns to zero //
            //this.skillLocator.cooldownList.Clear();

            // Add the nine lives buff //
            //new ServerSetBuffCount(this.gameObject, (int)Buff.nineLives.buffIndex, 1).Send(NetworkDestination.Server);

            // Init the HUD //
            PantheraHUD.Instance.CreateHUD(base.pantheraObj);

            // Create the First spawn Animation //
            if (base.masterObj.firstStarted == false)
            {
                base.masterObj.firstStarted = true;
                this.isSleeping = true;
                Utils.Animation.SetBoolean(base.pantheraObj, "IsSleeping", true);
                Utils.Animation.PlayAnimation(base.pantheraObj, "SleepLoop");
                base.characterDirection.enabled = false;
                base.characterBody.RecalculateStats();
                new ServerAddBuff(base.gameObject, (int)PantheraConfig.InvincibilityBuffDef.buffIndex).Send(NetworkDestination.Server);
            }
            else
            {
                Utils.Animation.SetBoolean(base.pantheraObj, "IsSleeping", false);
            }

            // Apply the Guardian Size //
            if (base.pantheraObj.activePreset.getAbilityLevel(PantheraConfig.GuardianAbilityID) > 0)
            {
                float modelScale = PantheraConfig.Guardian_modelScale;
                base.pantheraObj.transform.localScale = new Vector3(modelScale, modelScale, modelScale);
                base.modelTransform.localScale = new Vector3(modelScale, modelScale, modelScale);
                new ServerChangePantheraScale(base.gameObject, modelScale).Send(NetworkDestination.Server);
            }

        }

        public override void Update()
        {

            // Calcule the last Damage Time //
            float noDamageTime = Time.time - this.lastDamageTime;

            // Add Energy //
            base.characterBody.energy += base.pantheraObj.activePreset.energyRegen / 60;

            // Add Stamina //
            if (base.pantheraObj.dashing == false && base.pantheraObj.detectionActivated == false)
                base.characterBody.stamina += base.pantheraObj.activePreset.staminaRegen / 60;

            // Add Fury //
            if (base.pantheraObj.activePreset.getAbilityLevel(PantheraConfig.DestructionAbilityID) > 0)
            {
                if (noDamageTime < PantheraConfig.Fury_damageTimeBeforStopGenerate &&
                    Time.time - this.lastFuryGeneratedTime > PantheraConfig.Fury_pointsGenerationCooldown)
                {
                    base.characterBody.fury += 1;
                    this.lastFuryGeneratedTime = Time.time;
                }
            }

            // Add Shield //
            if (base.pantheraObj.activePreset.getAbilityLevel(PantheraConfig.GuardianAbilityID) > 0)
            {
                float lastDamage = Time.time - this.lastShieldDamageTime;
                float lastBroken = Time.time - this.destroyedShieldTime;
                float lastRecharge = Time.time - this.lastShieldGeneratedTime;
                if (lastDamage > base.pantheraObj.activePreset.frontShield_rechargeDelayAfterDamage
                    && lastBroken > base.pantheraObj.activePreset.frontShield_rechargeDelayAfterDestroyed
                    && lastRecharge > PantheraConfig.FrontShield_rechargeRatetime)
                {
                    if (base.pantheraObj.frontShield.active == false)
                    {
                        float recharge = base.pantheraObj.activePreset.maxShield * (PantheraConfig.FrontShield_rechargeRatePercent * PantheraConfig.FrontShield_rechargeRatetime);
                        this.lastShieldGeneratedTime = Time.time;
                        base.characterBody.shield += recharge;
                    }
                }
            }

            // Apply Passive Power Ability //
            if (base.pantheraObj.activePreset.getAbilityLevel(PantheraConfig.PassivePowerAbilityID) > 0 && base.characterBody.power < base.pantheraObj.activePreset.maxPower)
            {
                if (Time.time - this.lastPassivePowerTime > PantheraConfig.PassivePower_cooldown)
                {
                    base.characterBody.power += 1;
                    this.lastPassivePowerTime = Time.time;
                }
            }

            // Apply Regeneration Ability //
            if (base.pantheraObj.activePreset.getAbilityLevel(PantheraConfig.RegenerationAbilityID) > 0 && base.healthComponent.health < base.characterBody.maxHealth)
            {
                if (Time.time - this.lastRegenerationTime > PantheraConfig.Regeneration_cooldown)
                {
                    float heal = base.characterBody.maxHealth * base.pantheraObj.activePreset.regeneration_percentHeal;
                    new ServerHeal(base.gameObject, heal).Send(NetworkDestination.Server);
                    this.lastRegenerationTime = Time.time;
                }
            }

            // Apply the Shadow's Master Ability //
            int shadowsMasterAbilityLevel = base.pantheraObj.activePreset.getAbilityLevel(PantheraConfig.ShadowsMasterAbilityID);
            if (shadowsMasterAbilityLevel > 0
                && base.characterBody.outOfCombat == true && base.characterBody.outOfDanger == true)
            {
                if (shadowsMasterAbilityLevel == 1 && base.skillLocator.getCooldownInSecond(PantheraConfig.Prowl_SkillID) > PantheraConfig.ShadowsMaster_cooldownReduction1)
                    base.skillLocator.setCooldownInSecond(PantheraConfig.Prowl_SkillID, PantheraConfig.ShadowsMaster_cooldownReduction1);
                if (shadowsMasterAbilityLevel == 2 && base.skillLocator.getCooldownInSecond(PantheraConfig.Prowl_SkillID) > PantheraConfig.ShadowsMaster_cooldownReduction2)
                    base.skillLocator.setCooldownInSecond(PantheraConfig.Prowl_SkillID, PantheraConfig.ShadowsMaster_cooldownReduction2);
                if (shadowsMasterAbilityLevel == 3 && base.skillLocator.getCooldownInSecond(PantheraConfig.Prowl_SkillID) > PantheraConfig.ShadowsMaster_cooldownReduction3)
                    base.skillLocator.setCooldownInSecond(PantheraConfig.Prowl_SkillID, PantheraConfig.ShadowsMaster_cooldownReduction3);
            }

            // Check the Dash //
            if (base.pantheraObj.dashing == true)
            {
                // Stop Dashing if no more Stamina //
                if ((float)base.pantheraObj.activePreset.dash_staminaConsumed / 60 > base.characterBody.stamina)
                    Passives.Dash.StopDash(base.pantheraObj);

                // Stop Dashing if not running //
                if (base.characterBody.isSprinting == false && base.characterMotor.startSprint == false)
                    Passives.Dash.StopDash(base.pantheraObj);

                // Check if still Dashing and consume Stamina //
                if (base.pantheraObj.dashing == true)
                    base.characterBody.stamina -= (float)base.pantheraObj.activePreset.dash_staminaConsumed / 60;
            }

            // Check the Detection //
            if (base.pantheraObj.detectionActivated == true)
            {

                // Stop Detection if no more Stamine //
                if ((float)base.pantheraObj.activePreset.detection_staminaConsumed / 60 >= base.characterBody.stamina)
                    Passives.Detection.DisableDetection(base.pantheraObj);

                // Check if Rescan //
                if (Time.time - this.lastDetectionScanTime > PantheraConfig.Detection_scanRate && base.pantheraObj.detectionActivated == true)
                {
                    this.lastDetectionScanTime = Time.time;
                    Passives.Detection.ReScanBody(base.pantheraObj);
                }

                // Check if Detection is still active and consume Stamina //
                if (base.pantheraObj.detectionActivated == true)
                    base.characterBody.stamina -= (float)base.pantheraObj.activePreset.detection_staminaConsumed / 60;
            }

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
                    base.characterDirection.enabled = true;
                    new ServerRemoveBuff(base.gameObject, (int)PantheraConfig.InvincibilityBuffDef.buffIndex).Send(NetworkDestination.Server);
                    base.characterBody.RecalculateStats();
                }
            }


            // Apply the Character Fade level //
            float fadeLevel = base.pantheraObj.modelLocator.modelTransform.GetComponent<CharacterModel>().fade;
            if (fadeLevel != this.lastFadeLevel || base.pantheraObj.stealthed != this.wasStealthed)
            {
                this.wasStealthed = base.pantheraObj.stealthed;
                this.lastFadeLevel = fadeLevel;
                base.pantheraFX.SetFadeLevel(fadeLevel);
            }

            // Stealth the player if he get the Cloak buff from an item //
            if (base.characterBody.hasCloakBuff == true)
            {
                Stealth.DoStealth(base.pantheraObj);
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
            GlobalEventManager.onCharacterDeathGlobal -= OnCharacterDieEventClient;
            GlobalEventManager.onClientDamageNotified -= OnDamage;
        }

        public void OnDamage(DamageDealtMessage damageDealtMessage)
        {
            // Check the character //
            if (base.gameObject == null) return;
            if (damageDealtMessage.attacker == base.gameObject) OnDamageDone(damageDealtMessage);
            if (damageDealtMessage.victim == base.gameObject) OnDamageReceived(damageDealtMessage);
        }

        public void OnDamageDone(DamageDealtMessage damageDealtMessage)
        {

            // Get all values //
            float damage = damageDealtMessage.damage;
            bool crit = damageDealtMessage.crit;
            DamageType damageType = damageDealtMessage.damageType;

            // Set the last damage time //
            this.lastDamageTime = Time.time;

            // Life Steal //
            //LifeSteal.Heal(base.characterBody, damageDealtMessage.position, damageDealtMessage.damageType, damageDealtMessage.damage);

            // Mangle buff //
            //if (crit == true)
            //{
            //    int mangleBuffCount = base.characterBody.GetBuffCount(Buff.mangleBuff);
            //    if (mangleBuffCount < PantheraConfig.Passive_maxMangleStack)
            //    {
            //        new ServerSetBuffCount(this.gameObject, (int)Buff.mangleBuff.buffIndex, mangleBuffCount + 1).Send(NetworkDestination.Server);
            //    }
            //}

            // Add the Shield buff //
            //Shield.AddShieldStack(base.characterBody, base.pantheraFX);



            // Bleed damage //
            //if (damageInfo.damageType == DamageType.Generic)
            //{
            //    InflictDotInfo dotInfo;
            //    dotInfo.victimObject = targetHC.gameObject;
            //    dotInfo.attackerObject = damageInfo.attacker.gameObject;
            //    dotInfo.dotIndex = this.bleedDotIndex;
            //    dotInfo.duration = BigCatPassive.bleedDuration;
            //    dotInfo.damageMultiplier = BigCatPassive.bleedDamage;
            //    DotController.InflictDot(ref dotInfo);
            //}

        }

        public void OnDamageReceived(DamageDealtMessage damageDealtMessage)
        {

            // Set the last damage time //
            this.lastDamageTime = Time.time;

            // Stop the Stealth //
            Stealth.TookDamageUnstealth(base.pantheraObj);

            // Generate Fury //
            if (base.pantheraObj.activePreset.getAbilityLevel(PantheraConfig.DestructionAbilityID) > 0)
                    base.characterBody.fury += 1;

        }

        public void OnCharacterDieEventClient(DamageReport damageReport)
        {
            if (damageReport.attacker == null || damageReport.attacker != base.gameObject || damageReport.victim == null) return;
            OnCharacterDie(damageReport.attacker, damageReport.victim.gameObject);
        }

        public void OnCharacterDie(GameObject attacker, GameObject victime)
        {

        }

    }

}
