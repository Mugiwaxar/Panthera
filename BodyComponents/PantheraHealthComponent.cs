using Panthera.Base;
using Panthera.Components;
using Panthera.NetworkMessages;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Panthera.BodyComponents
{
    public class PantheraHealthComponent : HealthComponent
    {

        public PantheraObj ptraObj;
        public CharacterDirection characterDirection;
        public float lastBlockTime = 0;

        public void DoInit()
        {
            ptraObj = GetComponent<PantheraObj>();
            characterDirection = GetComponent<CharacterDirection>();
        }

        //public static void ModifyDamageInfoHook(Action<DamageInfo, HurtBox.DamageModifier> orig, DamageInfo self, HurtBox.DamageModifier damageModifier)
        //{
        //    orig(self, damageModifier);
        //    if (damageModifier == HurtBox.DamageModifier.Barrier)
        //    {
        //        self.AddModdedDamageType(Prefab.BarrierDamageType);
        //    }
        //}

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
                frontShieldHC.onDamage(damageInfo);
                return;
            }

            // Check the Character //
            PantheraHealthComponent hc = self as PantheraHealthComponent;
            if (hc == null)
            {
                orig(self, damageInfo);
                return;
            }

            // Check if Immun //
            if (Time.time - hc.lastBlockTime < PantheraConfig.Default_blockImmunDuration)
                return;

            // Check if Dodged //
            float rand = UnityEngine.Random.Range(0, 10000);
            rand /= 100;
            if (rand <= hc.ptraObj.characterBody.dodge)
            {
                // Create the Effect //
                EffectData effectData = new EffectData
                {
                    origin = damageInfo.position,
                    rotation = Util.QuaternionSafeLookRotation(damageInfo.force != Vector3.zero ? damageInfo.force : UnityEngine.Random.onUnitSphere)
                };
                EffectManager.SpawnEffect(PantheraAssets.DodgeEffectPrefab, effectData, true);
                return;
            }

            // Check if Blocked //
            if (hc.ptraObj.characterBody.block > 0)
            {
                // Create the Effect //
                EffectData effectData = new EffectData
                {
                    origin = damageInfo.position,
                    rotation = Util.QuaternionSafeLookRotation(damageInfo.force != Vector3.zero ? damageInfo.force : UnityEngine.Random.onUnitSphere)
                };
                EffectManager.SpawnEffect(PantheraAssets.BlockEffectPrefab, effectData, true);
                hc.lastBlockTime = Time.time;
                new ClientBlockUsed(hc.gameObject).Send(NetworkDestination.Clients);
                return;
            }

            // Apply the Extended Protection Ability //
            int extendedProtectionLevel = hc.ptraObj.GetAbilityLevel(PantheraConfig.ExtendedProtection_AbilityID);
            if (hc.ptraObj.frontShieldObj.activeInHierarchy == true && extendedProtectionLevel > 0)
            {
                float absorbedDamagePercent = 0;
                if (extendedProtectionLevel == 1)
                    absorbedDamagePercent = PantheraConfig.ExtendedProtection_percent1;
                else if (extendedProtectionLevel == 2)
                    absorbedDamagePercent = PantheraConfig.ExtendedProtection_percent2;
                else if (extendedProtectionLevel == 3)
                    absorbedDamagePercent = PantheraConfig.ExtendedProtection_percent3;
                else if (extendedProtectionLevel == 4)
                    absorbedDamagePercent = PantheraConfig.ExtendedProtection_percent4;
                float absorbedDamage = Mathf.Ceil(damageInfo.damage * absorbedDamagePercent);
                damageInfo.damage -= absorbedDamage;
                new ClientDamageShield(hc.ptraObj.gameObject, absorbedDamage).Send(NetworkDestination.Clients);
            }


            // Apply the Savage Revitalization Mastery //
            if (hc.ptraObj.guardianMode == true && hc.ptraObj.IsMastery(PantheraConfig.SavageRevitalization_AbilityID) == true)
            {
                float chance = hc.ptraObj.characterBody.mastery / 2;
                if (UnityEngine.Random.Range(0, 100) < chance)
                    new ServerAddBuff(hc.gameObject, hc.gameObject, Base.Buff.RegenerationBuff, 1, PantheraConfig.SavageRevitalization_MasteryBuffTime).Send(NetworkDestination.Server);
            }

            // Apply the Furrify Ability //
            if (hc.ptraObj.guardianMode == true && hc.ptraObj.GetAbilityLevel(PantheraConfig.Furrify_AbilityID) > 0)
            {
                if (damageInfo.damage > (hc.ptraObj.characterBody.maxHealth * PantheraConfig.Furrify_percent))
                {
                    int resilienceCount = hc.ptraObj.characterBody.GetBuffCount(Base.Buff.ResilienceBuff.buffIndex);
                    resilienceCount++;
                    resilienceCount = Math.Min(PantheraConfig.Resilience_maxStack, resilienceCount);
                    new ServerClearTimedBuffs(hc.gameObject, (Base.Buff.ResilienceBuff.index)).Send(NetworkDestination.Server);
                    new ServerAddBuff(hc.gameObject, hc.gameObject, Base.Buff.ResilienceBuff, resilienceCount).Send(NetworkDestination.Server);
                    Utils.Sound.playSound(Utils.Sound.Resilience, hc.gameObject);
                }
            }

            // Check the Enchanted Fur Ability //
            int enchantedFurLevel = hc.ptraObj.GetAbilityLevel(PantheraConfig.EnchantedFur_AbilityID);
            float enchantedFurDamageMultiplier = 1;
            if (enchantedFurLevel == 1)
                enchantedFurDamageMultiplier -= PantheraConfig.EnchantedFur_percent1;
            else if (enchantedFurLevel == 2)
                enchantedFurDamageMultiplier -= PantheraConfig.EnchantedFur_percent2;
            else if (enchantedFurLevel == 3)
                enchantedFurDamageMultiplier -= PantheraConfig.EnchantedFur_percent3;
            damageInfo.damage *= enchantedFurDamageMultiplier;

            // Check the Tornado Ability //
            int tornadoLevel = hc.ptraObj.GetAbilityLevel(PantheraConfig.Tornado_AbilityID);
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

            // Check the Innate Protection Ability //
            int protectionLevel = hc.ptraObj.GetAbilityLevel(PantheraConfig.InnateProtection_AbilityID);
            if (hc.ptraObj.guardianMode == true && protectionLevel > 0)
            {
                float maxDamagePercent = 1;
                if (protectionLevel == 1)
                    maxDamagePercent = PantheraConfig.InnateProtection_percent1;
                else if (protectionLevel == 2)
                    maxDamagePercent = PantheraConfig.InnateProtection_percent2;
                float maxDamage = hc.ptraObj.characterBody.maxHealth * maxDamagePercent;
                if (damageInfo.damage > maxDamage)
                {
                    // Create the Effect //
                    EffectData effectData = new EffectData
                    {
                        origin = damageInfo.position,
                        rotation = Util.QuaternionSafeLookRotation(damageInfo.force != Vector3.zero ? damageInfo.force : UnityEngine.Random.onUnitSphere)
                    };
                    EffectManager.SpawnEffect(PantheraAssets.ReducedEffectPrefab, effectData, true);
                }
                damageInfo.damage = Math.Min(maxDamage, damageInfo.damage);
            }

            // Do the base function //
            orig(self, damageInfo);

        }

    }
}
