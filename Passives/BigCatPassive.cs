using EntityStates;
using KinematicCharacterController;
using Panthera.Components;
using Panthera.GUI;
using Panthera.Machines;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.Passives;
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

namespace Panthera.Skills
{
    class BigCatPassive : MachineScript
    {

        public float lastNineLivesTime;
        public float lastDamageTime = 0;
        public bool stealed = false;
        public float lastFuryGeneratedTime = 0;
        public float lastShieldDamageTime = 0;
        public float destroyedShieldTime = 0;

        public override void Start()
        {            

            // Debug the Camera //
            this.pantheraObj.characterBody.GetComponent<CapsuleCollider>().radius = 0.5f;

            // Register all Events //
            GlobalEventManager.onCharacterDeathGlobal += OnCharacterDieEvent;
            GlobalEventManager.onClientDamageNotified += OnDamage;
            //On.RoR2.CharacterBody.OnSkillActivated += OnSkillUsed;

            // Set all Cooldowns to zero //
            PantheraSkill.CooldownList.Clear();

            // Add the nine lives buff //
            //new ServerSetBuffCount(this.gameObject, (int)Buff.nineLives.buffIndex, 1).Send(NetworkDestination.Server);

            // Init the HUD //
            PantheraHUD.Instance.CreateHUD();


        }

        public override void Update()
        {

            // Calcule the last Damage Time //
            float noDamageTime = Time.time - this.lastDamageTime;

            // Add Energy //
            this.characterBody.energy += base.pantheraObj.activePreset.energyRegen / 60;

            // Add Fury //
            if (base.pantheraObj.activePreset.getAbilityLevel(PantheraConfig.DestructionAbilityID) > 0)
            {
                if (noDamageTime < PantheraConfig.Fury_damageTimeBeforStopGenerate &&
                    Time.time - this.lastFuryGeneratedTime > PantheraConfig.Fury_pointsGenerationCooldown)
                {
                    this.characterBody.fury += 1;
                    this.lastFuryGeneratedTime = Time.time;
                }
            }

            // Add Shield //
            if (base.pantheraObj.activePreset.getAbilityLevel(PantheraConfig.GuardianAbilityID) > 0)
            {
                float lastDamage = Time.time - this.lastShieldDamageTime;
                float lastBroken = Time.time - this.destroyedShieldTime;
                if (lastDamage > PantheraConfig.FrontShield_rechargeDelayAfterDamage && lastBroken > PantheraConfig.FrontShield_rechargeDelayAfterDestroyed)
                {
                    if (base.pantheraObj.frontShield.active == false)
                        base.characterBody.shield += (base.pantheraObj.activePreset.maxShield * PantheraConfig.FrontShield_rechargeRatePercent) / 60f;
                }
            }

            // Send the Shield to the Server //
            if (NetworkServer.active == false) new ServerSendFrontShield(base.characterBody.gameObject, base.characterBody.shield).Send(NetworkDestination.Server);

        }

        public override void FixedUpdate()
        {

            // Apply the Character Fade level //
            float fadeLevel = this.pantheraObj.modelLocator.modelTransform.GetComponent<CharacterModel>().fade;
            this.pantheraFX.SetFadeLevel(fadeLevel);

            // Steal the player if he get the Cloak buff from an item //
            if (base.characterBody.hasCloakBuff == true)
            {
                Steal.DoSteal(base.pantheraObj);
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
            GlobalEventManager.onCharacterDeathGlobal -= OnCharacterDieEvent;
            GlobalEventManager.onClientDamageNotified -= OnDamage;
            //On.RoR2.CharacterBody.OnSkillActivated -= OnSkillUsed;
        }

        public void OnDamage(DamageDealtMessage damageDealtMessage)
        {
            // Check the character //
            if (this.gameObject == null) return;
            if (damageDealtMessage.attacker == this.gameObject) OnDamageDone(damageDealtMessage);
            if (damageDealtMessage.victim == this.gameObject) OnDamageReceived(damageDealtMessage);
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

            // Stop the steal //
            Steal.UnSteal(base.pantheraObj);

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

            // Stop the steal //
            Steal.UnSteal(base.pantheraObj);

        }

        //public void OnSkillUsed(On.RoR2.CharacterBody.orig_OnSkillActivated orig, CharacterBody self, GenericSkill skill)
        //{
        //    orig(self, skill);

        //    // Check the Character //
        //    if (self != this.characterBody)
        //    {
        //        return;
        //    }

        //}

        public void OnCharacterDieEvent(DamageReport damageReport)
        {
            if (damageReport.attacker == null || damageReport.attacker != base.gameObject || damageReport.victim == null) return;
            OnCharacterDie(damageReport.attacker, damageReport.victim.gameObject);
        }

        public void OnCharacterDie(GameObject attacker, GameObject victime)
        {

        }

    }

}
