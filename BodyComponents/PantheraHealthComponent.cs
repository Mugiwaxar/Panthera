using EntityStates;
using Panthera;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.NetworkMessages;
using R2API;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using RoR2.Audio;
using RoR2.Orbs;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Panthera.BodyComponents
{
    public class PantheraHealthComponent : HealthComponent
    {

        public PantheraObj ptraObj;
        public CharacterDirection characterDirection;

        public void DoInit()
        {
            ptraObj = GetComponent<PantheraObj>();
            characterDirection = GetComponent<CharacterDirection>();
        }

        public static void ModifyDamageInfoHook(Action<DamageInfo, HurtBox.DamageModifier> orig, DamageInfo self, HurtBox.DamageModifier damageModifier)
        {
            orig(self, damageModifier);
            if (damageModifier == HurtBox.DamageModifier.Barrier)
            {
                self.AddModdedDamageType(Prefab.BarrierDamageType);
            }
        }

        public static void TakeDamageHook(Action<HealthComponent, DamageInfo> orig, HealthComponent self, DamageInfo damageInfo)
        {

            // Check the Network //
            if (!NetworkServer.active)
            {
                Debug.LogWarning("[Server] function 'System.Void RoR2.HealthComponent::TakeDamage(RoR2.DamageInfo)' called on client");
                return;
            }

            // Check if Front Shield //
            if (self is FrontShieldHealthComponent)
            {
                FrontShieldHealthComponent frontShieldHC = (FrontShieldHealthComponent)self;
                frontShieldHC.onDamage(self, damageInfo);
                return;
            }

            // Check the Character //
            PantheraHealthComponent hc = self as PantheraHealthComponent;
            if (hc == null)
            {
                orig(self, damageInfo);
                return;
            }

            // Check the Enchanted Fur Ability //
            int enchantedFurLevel = hc.ptraObj.getAbilityLevel(PantheraConfig.EnchantedFur_AbilityID);
            float enchantedFurDamageMultiplier = 1;
            if (enchantedFurLevel == 1)
                enchantedFurDamageMultiplier -= PantheraConfig.EnchantedFur_percent1;
            else if (enchantedFurLevel == 2)
                enchantedFurDamageMultiplier -= PantheraConfig.EnchantedFur_percent2;
            else if (enchantedFurLevel == 3)
                enchantedFurDamageMultiplier -= PantheraConfig.EnchantedFur_percent3;
            damageInfo.damage *= enchantedFurDamageMultiplier;

            // Check the Tornado Ability //
            int tornadoLevel = hc.ptraObj.getAbilityLevel(PantheraConfig.Tornado_AbilityID);
            float tornadoDamageMultiplier = 1;
            if (hc.ptraObj.clawsStormActivated == true)
            {
                if (tornadoLevel == 1)
                    tornadoDamageMultiplier -= PantheraConfig.Tornado_resistPercent1;
                else if (tornadoLevel == 2)
                    tornadoDamageMultiplier -= PantheraConfig.Tornado_resistPercent2;
                else if (tornadoLevel == 3)
                    tornadoDamageMultiplier -= PantheraConfig.Tornado_resistPercent3;
                damageInfo.damage *= tornadoDamageMultiplier;
            }

            // Check if the Front Shield was damaged //
            if (damageInfo.HasModdedDamageType(Prefab.BarrierDamageType) && hc.ptraObj.characterBody.frontShield > 0)
            {

                // Create the Effect //
                //EffectData effectData = new EffectData
                //{
                //    origin = damageInfo.position,
                //    rotation = Util.QuaternionSafeLookRotation(damageInfo.force != Vector3.zero ? damageInfo.force : UnityEngine.Random.onUnitSphere)
                //};
                //EffectManager.SpawnEffect(Assets.BlockEffectPrefab, effectData, true);
                //EffectManager.SpawnEffect(Assets.FrontShieldHitFX, effectData, true);
                //Utils.Sound.playSound(Utils.Sound.ShieldAbsorb, self.gameObject);

                // Check the Residual Energy Ability //
                //if (hc.ptraObj != null && hc.ptraObj.activePreset != null)
                //{
                //    int resEnergyLevel = hc.ptraObj.activePreset != null ? hc.ptraObj.activePreset.getAbilityLevel(PantheraConfig.ResidualEnergyAbilityID) : 0;
                //    float addedBarrier = 0;
                //    if (resEnergyLevel == 1)
                //        addedBarrier = damageInfo.damage * PantheraConfig.ResidualEnergy_percent1;
                //    else if (resEnergyLevel == 2)
                //        addedBarrier = damageInfo.damage * PantheraConfig.ResidualEnergy_percent2;
                //    else if (resEnergyLevel == 3)
                //        addedBarrier = damageInfo.damage * PantheraConfig.ResidualEnergy_percent3;
                //    else if (resEnergyLevel == 4)
                //        addedBarrier = damageInfo.damage * PantheraConfig.ResidualEnergy_percent4;
                //    else if (resEnergyLevel == 5)
                //        addedBarrier = damageInfo.damage * PantheraConfig.ResidualEnergy_percent5;
                //    addedBarrier = (float)Math.Ceiling(addedBarrier);
                //    hc.AddBarrier(addedBarrier);
                //}

                //// Decrease the Shield //
                //if (hc.godMode == false)
                //    new ClientDamageShield(hc.gameObject, damageInfo.damage).Send(NetworkDestination.Clients);

                //// Reject the damage //
                //damageInfo.rejected = true;

            }

            //// Check the Instinctive Resistance Ability //
            //if (hc.ptraObj != null && hc.ptraObj.activePreset != null)
            //{
            //    int ripperBuffCount = hc.ptraObj.characterBody.GetBuffCount(Buff.TheRipperBuff);
            //    float resistanceAmount = hc.ptraObj.activePreset.instinctiveResistance_resistanceAmount;
            //    if (ripperBuffCount > 0 && resistanceAmount > 0)
            //    {
            //        float damageReduction = 1;
            //        damageReduction -= ripperBuffCount * resistanceAmount;
            //        int instResLevel = hc.ptraObj.activePreset != null ? hc.ptraObj.activePreset.getAbilityLevel(PantheraConfig.InstinctiveResistanceAbilityID) : 0;
            //        damageInfo.damage *= damageReduction;
            //        Debug.LogWarning("DamageReduction -> " + damageReduction);
            //    }
            //}

            //// Check the Strong Barrier Ability //
            //if (hc.ptraObj != null && hc.ptraObj.activePreset != null && hc.barrier > 0)
            //{
            //    float maxPercentDamage = hc.ptraObj.activePreset.strongBarrier_maxPercent;
            //    if (maxPercentDamage < 1)
            //    {
            //        float maxDamage = hc.body.maxHealth * maxPercentDamage;
            //        damageInfo.damage = Math.Min(damageInfo.damage, maxDamage);
            //        damageInfo.crit = false;
            //    }
            //}

            // Do the base function //
            orig(self, damageInfo);

            // Check if the Ray Slash is activated //
            //if (this.ptraObj.onRaySlashCharge == true && self.body.GetBuffCount(Buff.raySlashBuff) < PantheraConfig.RaySlash_maxRaySlashBuff)
            //{
            //    int buffsToAdd = (int)Math.Max(1, damageInfo.damage * PantheraConfig.RaySlash_damageAbsoptionMultiplier);
            //    buffsToAdd = Math.Min(buffsToAdd, PantheraConfig.RaySlash_maxRaySlashBuff - self.body.GetBuffCount(Buff.raySlashBuff));
            //    self.body.SetBuffCount(Buff.raySlashBuff.buffIndex, self.body.GetBuffCount(Buff.raySlashBuff) + buffsToAdd);
            //}

            // Nullify damage if the Ray Slash is recharging //
            //if (this.ptraObj.onRaySlashCharge == true)
            //{
            //    return;
            //}

            // Check if the Panthera Shield must be used //
            //damageInfo.damage = Shield.MustUseShield(base.body, damageInfo.damage);

            // Add the barriere related to damage //
            //if (damageInfo.damage > 0 && self.health > 0)
            //{
            //    Barrier.ApplyBarrier(this, damageInfo.damage);
            //}


        }

    }
}
