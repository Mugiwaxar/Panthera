using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.NetworkMessages;
using Panthera.Utils;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using RoR2.Projectile;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using static RoR2.DotController;

namespace Panthera.Components
{
    internal class PantheraProjectileComponent : MonoBehaviour, IProjectileImpactBehavior
    {

        public PantheraObj ptraObj;
        public ProjectileController controller;
        public ProjectileDamage projectileDamage;
        public GameObject impactEffect;
        public String impactSound;
        public string projectileName;
        public bool destroyOnHit = true;
        public bool destroyOnWorld = true;
        public bool destroyWhenNotAlive = true;
        public bool fired = false;
        public bool alive = true;
        public List<GameObject> enemiesHit = new List<GameObject>();

        public void Start()
        {
            this.controller = this.GetComponent<ProjectileController>();
            this.projectileDamage = base.GetComponent<ProjectileDamage>();
            this.ptraObj = controller?.owner?.GetComponent<PantheraObj>();
        }

        public void OnProjectileImpact(ProjectileImpactInfo impactInfo)
        {

            // Check if the Projectile has already fired //
            if (fired == true) return;

            // Check the Panthera Object //
            if (this.ptraObj == null) return;

            // Check if still Alive //
            if (!this.alive)
            {
                return;
            }

            // Check if the Enemy was not already Hit //
            GameObject enemyObj = impactInfo.collider?.GetComponent<HurtBox>()?.healthComponent?.gameObject;
            if (enemyObj != null && enemiesHit.Contains(enemyObj))
            {
                return;
            }
            enemiesHit.Add(enemyObj);

            // Get the Collider //
            Collider collider = impactInfo.collider;
            if (collider)
            {

                // Create the Damage Info //
                DamageInfo damageInfo = new DamageInfo();
                if (this.projectileDamage != null)
                {
                    damageInfo.damage = this.projectileDamage.damage;
                    damageInfo.crit = this.projectileDamage.crit;
                    damageInfo.attacker = this.controller.owner;
                    damageInfo.inflictor = base.gameObject;
                    damageInfo.position = impactInfo.estimatedPointOfImpact;
                    damageInfo.force = this.projectileDamage.force * base.transform.forward;
                    damageInfo.procChainMask = this.controller.procChainMask;
                    damageInfo.procCoefficient = this.controller.procCoefficient;
                    damageInfo.damageColorIndex = this.projectileDamage.damageColorIndex;
                    damageInfo.damageType = this.projectileDamage.damageType;
                }
                else
                {
                    Debug.Log("No projectile damage component!");
                }
                // Get the HurtBox //
                HurtBox hurtBox = collider.GetComponent<HurtBox>();
                if (hurtBox!= null)
                {
                    // Damage the Enemy //
                    HealthComponent healthComponent = hurtBox.healthComponent;
                    if (healthComponent)
                    {
                        if (healthComponent.gameObject == this.controller.owner)
                        {
                            return;
                        }
                        if (FriendlyFireManager.ShouldDirectHitProceed(healthComponent, this.controller.teamFilter.teamIndex) == true)
                        {
                            Utils.Sound.playSound(this.impactSound, base.gameObject, false);
                            if (this.impactEffect != null)
                                Utils.FXManager.SpawnEffect(this.ptraObj.gameObject, this.impactEffect, impactInfo.estimatedPointOfImpact, 1, null, new Quaternion(), false, false);
                            if (NetworkServer.active)
                            {
                                damageInfo.ModifyDamageInfo(hurtBox.damageModifier);
                                healthComponent.TakeDamage(damageInfo);
                                GlobalEventManager.instance.OnHitEnemy(damageInfo, hurtBox.healthComponent.gameObject);
                            }
                                if (this.destroyOnHit == true)
                                    this.alive = false;
                        }
                    }
                }
                else if (this.destroyOnWorld)
                    this.alive = false;

                // I don't know what this do ?? //
                damageInfo.position = base.transform.position;
                if (NetworkServer.active)
                {
                    GlobalEventManager.instance.OnHitAll(damageInfo, collider.gameObject);
                }
            }

            // Check if Air Cleave //
            if (this.projectileName.Contains("AirCleave"))
            {
                // Get the Team Component //
                TeamComponent tc = impactInfo.collider?.GetComponent<HurtBox>()?.healthComponent?.body?.teamComponent;
                // Check the Active Preset and Enemy hit //
                if (this.ptraObj.activePreset != null && tc != null && tc.teamIndex == TeamIndex.Monster)
                {

                    // Set Fired to true //
                    this.fired = true;

                    // Apply the Perspicacity Ability //
                    int perspicacityAbilityLevel = this.ptraObj.activePreset.getAbilityLevel(PantheraConfig.PerspicacityAbilityID);
                    float rand = UnityEngine.Random.Range(0f, 1f);
                    bool ok = false;
                    if (perspicacityAbilityLevel > 0)
                    {
                        if (perspicacityAbilityLevel == 1 && rand <= PantheraConfig.Perspicacity_percent1)
                            ok = true;
                        else if (perspicacityAbilityLevel == 2 && rand <= PantheraConfig.Perspicacity_percent2)
                            ok = true;
                        else if (perspicacityAbilityLevel == 3 && rand <= PantheraConfig.Perspicacity_percent3)
                            ok = true;
                        if (ok == true && NetworkClient.active == false)
                            new ClientAddComboPoint(this.ptraObj.gameObject, 1).Send(NetworkDestination.Clients);
                        else if (ok == true)
                            this.ptraObj.characterBody.comboPoint += 1;
                    }

                    // Apply the Rip-per Buff //
                    if (this.ptraObj.activePreset.getAbilityLevel(PantheraConfig.TheRipperAbilityID) > 0)
                        Passives.Ripper.AddBuff(this.ptraObj);

                }

                // Check the Active Preset and Player hit //
                HealthComponent hc = impactInfo.collider?.GetComponent<HurtBox>()?.healthComponent;
                if (this.ptraObj.activePreset != null && hc != null && tc != null && hc != this.ptraObj.healthComponent && tc.teamIndex == TeamIndex.Player)
                {
                    // Apply the Healing Cleave Ability //
                    int abilityLevel = this.ptraObj.activePreset.getAbilityLevel(PantheraConfig.HealingCleaveAbilityID);
                    if (abilityLevel > 0)
                    {
                        float healPercent = 0;
                        if (abilityLevel == 1)
                            healPercent = PantheraConfig.HealingCleave_percent1;
                        else if (abilityLevel == 2)
                            healPercent = PantheraConfig.HealingCleave_percent2;
                        else if (abilityLevel == 3)
                            healPercent = PantheraConfig.HealingCleave_percent3;
                        float amount = this.ptraObj.characterBody.maxHealth * healPercent;
                        if (NetworkServer.active == true) hc.Heal(amount, default(ProcChainMask));
                        Utils.Sound.playSound(Utils.Sound.HealingCleave, hc.gameObject, false);
                        Utils.FXManager.SpawnEffect(hc.gameObject, Base.Assets.FlashHealFX, hc.body.footPosition, 1, hc.gameObject);
                        // Set Fired to true //
                        this.fired = true;
                        // Destroy the Projectile //
                        if (this.destroyOnHit == true)
                            this.alive = false;
                    }
                }

                // Apply the Burning Spirit Ability //
                if (tc != null && this.projectileName.Contains("FireAirCleave"))
                {
                    if (NetworkServer.active == true)
                    {
                        InflictDotInfo dotInfo = new InflictDotInfo();
                        dotInfo.attackerObject = this.ptraObj.gameObject;
                        dotInfo.victimObject = tc.gameObject;
                        dotInfo.dotIndex = PantheraConfig.BurnDotIndex;
                        dotInfo.duration = PantheraConfig.BurningSpirit_burnDuration;
                        dotInfo.damageMultiplier = PantheraConfig.BurningSpirit_burnDamage;
                        DotController.InflictDot(ref dotInfo);
                    }
                    // Apply the Hell Cat Ability //
                    if (this.ptraObj.activePreset.getAbilityLevel(PantheraConfig.HellCatAbilityID) > 0)
                    {
                        this.ptraObj.characterBody.fury += 1;
                        this.ptraObj.characterBody.power += 1;
                    }
                }

            }

            // Check if Fire Bird //
            if (this.projectileName == PantheraConfig.FireBird_projectileName)
            {

                // Get the Team Component //
                TeamComponent tc = impactInfo.collider?.GetComponent<HurtBox>()?.healthComponent?.body?.teamComponent;
                // Check if Enemy //
                if (tc != null && tc.teamIndex == TeamIndex.Monster)
                {

                    // Apply the Burn //
                    if (NetworkServer.active == true)
                    {
                        InflictDotInfo dotInfo = new InflictDotInfo();
                        dotInfo.attackerObject = this.ptraObj.gameObject;
                        dotInfo.victimObject = tc.gameObject;
                        dotInfo.dotIndex = PantheraConfig.SuperBurnDotIndex;
                        dotInfo.duration = PantheraConfig.FireBird_burnDuration;
                        dotInfo.damageMultiplier = PantheraConfig.FireBird_burnDamageMultiplier;
                        DotController.InflictDot(ref dotInfo);
                    }

                    // Check the Angry Bird Ability //
                    if (this.ptraObj.activePreset.getAbilityLevel(PantheraConfig.AngryBirdAbilityID) > 0)
                    {
                        this.ptraObj.characterBody.power += 1;
                    }

                }
            }

                if (!this.alive)
            {
                if (this.destroyWhenNotAlive)
                {
                    UnityEngine.Object.Destroy(base.gameObject);
                }
            }

        }
    }
}
