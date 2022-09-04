using EntityStates;
using Panthera.NetworkMessages;
using Panthera.Passives;
using Panthera.Skills;
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

namespace Panthera.Components
{
    class PantheraHealthComponent : HealthComponent
    {

        public PantheraObj ptraObj;
        public CharacterDirection characterDirection;
        public static DamageAPI.ModdedDamageType barrierDamageType;

        public float _frontShield = 0;
        public float frontShield
        {
            get
            {
                if (NetworkClient.active == false)
                    return this._frontShield;
                else if (this.ptraObj != null && this.ptraObj.characterBody != null)
                    return this.ptraObj.characterBody.shield;
                else
                    return 0;
            }
            set
            {
                this._frontShield = value;
            }
        }

        public void DoInit()
        {
            this.ptraObj = this.GetComponent<PantheraObj>();
            this.characterDirection = this.GetComponent<CharacterDirection>();            
        }

        public static void ModifyDamageInfo(On.RoR2.DamageInfo.orig_ModifyDamageInfo orig, DamageInfo self, HurtBox.DamageModifier damageModifier)
        {
            orig(self, damageModifier);
            if (damageModifier == HurtBox.DamageModifier.Barrier)
            {
                self.AddModdedDamageType(barrierDamageType);
            }
        }

        public void OnTakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo damageInfo)
        {

            // Check the Network //
            if (!NetworkServer.active)
            {
                Debug.LogWarning("[Server] function 'System.Void RoR2.HealthComponent::TakeDamage(RoR2.DamageInfo)' called on client");
                return;
            }

            // Check the Character //
            if (self is PantheraHealthComponent == false)
            {
                orig(self, damageInfo);
                return;
            }

            // Check if this is damage on Barrier //
            if (DamageAPI.HasModdedDamageType(damageInfo, barrierDamageType) && this.frontShield > 0)
            {

                // Reject the damage //
                damageInfo.rejected = true;

                // Create the Effect //
                EffectData effectData = new EffectData
                {
                    origin = damageInfo.position,
                    rotation = Util.QuaternionSafeLookRotation((damageInfo.force != Vector3.zero) ? damageInfo.force : UnityEngine.Random.onUnitSphere)
                };
                EffectManager.SpawnEffect(Assets.BlockEffectPrefab, effectData, true);

                // Decrease the Shield //
                new ClientShieldDamage(this.gameObject, damageInfo.damage).Send(NetworkDestination.Clients);

            }

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

            // Do the base function //
            orig(self, damageInfo);

            // Add the barriere related to damage //
            //if (damageInfo.damage > 0 && self.health > 0)
            //{
            //    Barrier.ApplyBarrier(this, damageInfo.damage);
            //}


        }

        #region Old Function
        //          public void OnTakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo damageInfo)
        //{

        //           Check the Character //
        //	if (NetworkServer.active == false) return;
        //	if (self is PantheraHealthComponent == false)
        //	{
        //		orig(self, damageInfo);
        //		return;
        //	}

        //           Return if the character is invincible //
        //          if (!self.alive || self.godMode)
        //          {
        //              return;
        //          }

        //           Save the health before the attack //
        //          float healthBeforeAttack = self.health;

        //           Check if the Ray Slash is activated //
        //          if (this.ptraObj.onRaySlashCharge == true && self.body.GetBuffCount(Buff.raySlashBuff) < PantheraConfig.RaySlash_maxRaySlashBuff)
        //          {
        //              int buffsToAdd = (int)Math.Max(1, damageInfo.damage * PantheraConfig.RaySlash_damageAbsoptionMultiplier);
        //              buffsToAdd = Math.Min(buffsToAdd, PantheraConfig.RaySlash_maxRaySlashBuff - self.body.GetBuffCount(Buff.raySlashBuff));
        //              self.body.SetBuffCount(Buff.raySlashBuff.buffIndex, self.body.GetBuffCount(Buff.raySlashBuff) + buffsToAdd);
        //          }

        //           Nullify damage if the Ray Slash is recharging //
        //          if (this.ptraObj.onRaySlashCharge == true)
        //          {
        //              return;
        //          }

        //           One shoot protection //
        //          if (self.ospTimer > 0f)
        //          {
        //              return;
        //          }

        //           Prepare the attacker info //
        //          CharacterBody characterBody = null;
        //          TeamIndex teamIndex = TeamIndex.None;
        //          Vector3 vector = Vector3.zero;

        //           Get the character combined health (Health + Shield + Barrier) //
        //          float combinedHealth = self.combinedHealth;

        //           Get the attacker info //
        //          if (damageInfo.attacker)
        //          {
        //              characterBody = damageInfo.attacker.GetComponent<CharacterBody>();
        //              if (characterBody)
        //              {
        //                  teamIndex = characterBody.teamComponent.teamIndex;
        //                  vector = characterBody.corePosition - damageInfo.position;
        //              }
        //          }

        //           Check if the Panthera Shield must be used //
        //          damageInfo.damage = Shield.MustUseShield(base.body, damageInfo.damage);

        //           Calcule the if the bear item must trigger //
        //          if (self.itemCounts.bear > 0 && Util.CheckRoll(Util.ConvertAmplificationPercentageIntoReductionPercentage(15f * (float)self.itemCounts.bear), 0f, null))
        //          {
        //              EffectData effectData = new EffectData
        //              {
        //                  origin = damageInfo.position,
        //                  rotation = Util.QuaternionSafeLookRotation((damageInfo.force != Vector3.zero) ? damageInfo.force : UnityEngine.Random.onUnitSphere)
        //              };
        //              EffectManager.SpawnEffect(HealthComponent.AssetReferences.bearEffectPrefab, effectData, true);
        //              damageInfo.rejected = true;
        //          }

        //           Check if the hidden invincibility buff must trigger //
        //          bool flag = (damageInfo.damageType & DamageType.BypassArmor) > DamageType.Generic;
        //          if (self.body.HasBuff(RoR2Content.Buffs.HiddenInvincibility) && !flag)
        //          {
        //              damageInfo.rejected = true;
        //          }

        //           Check if the immune buff must trigger //
        //          if (self.body.HasBuff(RoR2Content.Buffs.Immune) && (!characterBody || !characterBody.HasBuff(RoR2Content.Buffs.GoldEmpowered)))
        //          {
        //              EffectManager.SpawnEffect(HealthComponent.AssetReferences.damageRejectedPrefab, new EffectData
        //              {
        //                  origin = damageInfo.position
        //              }, true);
        //              damageInfo.rejected = true;
        //          }

        //           Check if captain armor must be activated //
        //          if (!damageInfo.rejected && self.body.HasBuff(RoR2Content.Buffs.BodyArmor))
        //          {
        //              self.body.RemoveBuff(RoR2Content.Buffs.BodyArmor);
        //              EffectData effectData2 = new EffectData
        //              {
        //                  origin = damageInfo.position,
        //                  rotation = Util.QuaternionSafeLookRotation((damageInfo.force != Vector3.zero) ? damageInfo.force : UnityEngine.Random.onUnitSphere)
        //              };
        //              EffectManager.SpawnEffect(HealthComponent.AssetReferences.captainBodyArmorBlockEffectPrefab, effectData2, true);
        //              damageInfo.rejected = true;
        //          }

        //           Server thing, I don't know //
        //          IOnIncomingDamageServerReceiver[] array = self.onIncomingDamageReceivers;
        //          for (int i = 0; i < array.Length; i++)
        //          {
        //              array[i].OnIncomingDamageServer(damageInfo);
        //          }

        //           Return if the damage was rejected somewhere //
        //          if (damageInfo.rejected)
        //          {
        //              return;
        //          }

        //           Scale damage with friendly fire scale //
        //          float num = damageInfo.damage;
        //          if (teamIndex == self.body.teamComponent.teamIndex)
        //          {
        //              TeamDef teamDef = TeamCatalog.GetTeamDef(teamIndex);
        //              if (teamDef != null)
        //              {
        //                  num *= teamDef.friendlyFireScaling;
        //              }
        //          }

        //           Check if damage is > 0 //
        //          if (num > 0f)
        //          {
        //               Check the attacker character body //
        //              if (characterBody)
        //              {

        //                   Check if a backstab must trigger //
        //                  if (characterBody.canPerformBackstab && (damageInfo.damageType & DamageType.DoT) != DamageType.DoT && (damageInfo.procChainMask.HasProc(ProcType.Backstab) || BackstabManager.IsBackstab(-vector, self.body)))
        //                  {
        //                      damageInfo.crit = true;
        //                      damageInfo.procChainMask.AddProc(ProcType.Backstab);
        //                      if (BackstabManager.backstabImpactEffectPrefab)
        //                      {
        //                          EffectManager.SimpleImpactEffect(BackstabManager.backstabImpactEffectPrefab, damageInfo.position, -damageInfo.force, true);
        //                      }
        //                  }

        //                   Check the attacker character master //
        //                  CharacterMaster master = characterBody.master;
        //                  if (master && master.inventory)
        //                  {

        //                       Check if the crowbar item must trigger //
        //                      if (combinedHealth >= self.fullCombinedHealth * 0.9f)
        //                      {
        //                          int itemCount = master.inventory.GetItemCount(RoR2Content.Items.Crowbar);
        //                          if (itemCount > 0)
        //                          {
        //                              num *= 1f + 0.75f * (float)itemCount;
        //                              EffectManager.SimpleImpactEffect(HealthComponent.AssetReferences.crowbarImpactEffectPrefab, damageInfo.position, -damageInfo.force, true);
        //                          }
        //                      }

        //                       Check if the focus crystal item must trigger //
        //                      int itemCount2 = master.inventory.GetItemCount(RoR2Content.Items.NearbyDamageBonus);
        //                      if (itemCount2 > 0 && vector.sqrMagnitude <= 169f)
        //                      {
        //                          damageInfo.damageColorIndex = DamageColorIndex.Nearby;
        //                          num *= 1f + (float)itemCount2 * 0.2f;
        //                          EffectManager.SimpleImpactEffect(HealthComponent.AssetReferences.diamondDamageBonusImpactEffectPrefab, damageInfo.position, vector, true);
        //                      }

        //                       Check the proc coefficient to know if debuff must apply //
        //                      if (damageInfo.procCoefficient > 0f)
        //                      {
        //                           Check if the shattering justice item must trigger //
        //                          int itemCount3 = master.inventory.GetItemCount(RoR2Content.Items.ArmorReductionOnHit);
        //                          if (itemCount3 > 0 && !self.body.HasBuff(RoR2Content.Buffs.Pulverized))
        //                          {
        //                              self.body.AddTimedBuff(RoR2Content.Buffs.PulverizeBuildup, 2f * damageInfo.procCoefficient);
        //                              if (self.body.GetBuffCount(RoR2Content.Buffs.PulverizeBuildup) >= 5)
        //                              {
        //                                  self.body.ClearTimedBuffs(RoR2Content.Buffs.PulverizeBuildup);
        //                                  self.body.AddTimedBuff(RoR2Content.Buffs.Pulverized, 8f * (float)itemCount3);
        //                                  EffectManager.SpawnEffect(HealthComponent.AssetReferences.pulverizedEffectPrefab, new EffectData
        //                                  {
        //                                      origin = self.body.corePosition,
        //                                      scale = self.body.radius
        //                                  }, true);
        //                              }
        //                          }

        //                           Check if the Merc buff must be applied //
        //                          if (self.body.HasBuff(RoR2Content.Buffs.MercExpose))
        //                          {
        //                              self.body.RemoveBuff(RoR2Content.Buffs.MercExpose);
        //                              if (characterBody && characterBody.bodyIndex == BodyCatalog.FindBodyIndex("MercBody"))
        //                              {
        //                                  float num2 = characterBody.damage * 3.5f;
        //                                  num += num2;
        //                                  damageInfo.damage += num2;
        //                                  SkillLocator skillLocator = characterBody.skillLocator;
        //                                  if (skillLocator)
        //                                  {
        //                                      skillLocator.DeductCooldownFromAllSkillsServer(1f);
        //                                  }
        //                                  EffectManager.SimpleImpactEffect(HealthComponent.AssetReferences.mercExposeConsumeEffectPrefab, damageInfo.position, Vector3.up, true);
        //                              }
        //                          }
        //                      }

        //                       Check if the armor-piercing rounds item must trigger //
        //                      if (self.body.isBoss)
        //                      {
        //                          int itemCount4 = master.inventory.GetItemCount(RoR2Content.Items.BossDamageBonus);
        //                          if (itemCount4 > 0)
        //                          {
        //                              num *= 1f + 0.2f * (float)itemCount4;
        //                              damageInfo.damageColorIndex = DamageColorIndex.WeakPoint;
        //                              EffectManager.SimpleImpactEffect(HealthComponent.AssetReferences.bossDamageBonusImpactEffectPrefab, damageInfo.position, -damageInfo.force, true);
        //                          }
        //                      }

        //                  }
        //              }

        //               Multiply damage if critique //
        //              if (damageInfo.crit)
        //              {
        //                  num *= 2f;
        //              }

        //               Check if weak point must be applied //
        //              if ((damageInfo.damageType & DamageType.WeakPointHit) != DamageType.Generic)
        //              {
        //                  num *= 1.5f;
        //                  damageInfo.damageColorIndex = DamageColorIndex.WeakPoint;
        //              }

        //               Check if the death Mark buff must trigger //
        //              if (self.body.HasBuff(RoR2Content.Buffs.DeathMark))
        //              {
        //                  num *= 1.5f;
        //                  damageInfo.damageColorIndex = DamageColorIndex.DeathMark;
        //              }

        //               Re-check the invincibility //
        //              if (!flag)
        //              {

        //                   Get the armor //
        //                  float num3 = self.body.armor;
        //                  num3 += self.adaptiveArmorValue;

        //                   Get if this is aoe damage //
        //                  bool flag2 = (damageInfo.damageType & DamageType.AOE) > DamageType.Generic;

        //                   Check if the character is resistant to aoe //
        //                  if ((self.body.bodyFlags & CharacterBody.BodyFlags.ResistantToAOE) > CharacterBody.BodyFlags.None && flag2)
        //                  {
        //                      num3 += 300f;
        //                  }

        //                   Calculate the armor damage reduction //
        //                  float num4 = (num3 >= 0f) ? (1f - num3 / (num3 + 100f)) : (2f - 100f / (100f - num3));
        //                  num = Mathf.Max(1f, num * num4);

        //                   Check if the character has repulsion armor plate //
        //                  if (self.itemCounts.armorPlate > 0)
        //                  {
        //                      num = Mathf.Max(1f, num - 5f * (float)self.itemCounts.armorPlate);
        //                      EntitySoundManager.EmitSoundServer(Resources.Load<NetworkSoundEventDef>("NetworkSoundEventDefs/nseArmorPlateBlock").index, base.gameObject);
        //                  }

        //                   Heal if the character has Planula item //
        //                  if (self.itemCounts.parentEgg > 0)
        //                  {
        //                      self.Heal((float)self.itemCounts.parentEgg * 15f, default(ProcChainMask), true);
        //                      EntitySoundManager.EmitSoundServer(Resources.Load<NetworkSoundEventDef>("NetworkSoundEventDefs/nseParentEggHeal").index, base.gameObject);
        //                  }

        //              }

        //               Check if the paladin? barrier must absorb the damage //
        //              if ((damageInfo.damageType & DamageType.BarrierBlocked) != DamageType.Generic)
        //              {
        //                  damageInfo.force *= 0.5f;
        //                  IBarrier component = base.GetComponent<IBarrier>();
        //                  if (component != null)
        //                  {
        //                      component.BlockedDamage(damageInfo, num);
        //                  }
        //                  damageInfo.procCoefficient = 0f;
        //                  num = 0f;
        //              }

        //               Check if the character can't be one shot //
        //              if (self.body.hasOneShotProtection && (damageInfo.damageType & DamageType.BypassOneShotProtection) != DamageType.BypassOneShotProtection)
        //              {
        //                  float num5 = (self.fullCombinedHealth + self.barrier) * (1f - self.body.oneShotProtectionFraction);
        //                  float b = Mathf.Max(0f, num5 - self.serverDamageTakenThisUpdate);
        //                  float num6 = num;
        //                  num = Mathf.Min(num, b);
        //                  if (num != num6)
        //                  {
        //                      self.TriggerOneShotProtection();
        //                  }
        //              }

        //               Check if the damage must be upgraded by low heal //
        //              if ((damageInfo.damageType & DamageType.BonusToLowHealth) > DamageType.Generic)
        //              {
        //                  float num7 = Mathf.Lerp(3f, 1f, self.combinedHealthFraction);
        //                  num *= num7;
        //              }

        //               Check if the lunarShell? buff must trigger //
        //              if (self.body.HasBuff(RoR2Content.Buffs.LunarShell) && num > self.fullHealth * 0.1f)
        //              {
        //                  num = self.fullHealth * 0.1f;
        //              }

        //          }

        //           Check if the character must be slowed with slow buff //
        //          if ((damageInfo.damageType & DamageType.SlowOnHit) != DamageType.Generic)
        //          {
        //              self.body.AddTimedBuff(RoR2Content.Buffs.Slow50, 2f);
        //          }

        //           Check if the character must be slowed with clay buff //
        //          if ((damageInfo.damageType & DamageType.ClayGoo) != DamageType.Generic && (self.body.bodyFlags & CharacterBody.BodyFlags.ImmuneToGoo) == CharacterBody.BodyFlags.None)
        //          {
        //              self.body.AddTimedBuff(RoR2Content.Buffs.ClayGoo, 2f);
        //          }

        //           Check if a nulify stack must be added //
        //          if ((damageInfo.damageType & DamageType.Nullify) != DamageType.Generic)
        //          {
        //              self.body.AddTimedBuff(RoR2Content.Buffs.NullifyStack, 8f);
        //          }

        //           Check if the crippe buff must be added //
        //          if ((damageInfo.damageType & DamageType.CrippleOnHit) != DamageType.Generic || (characterBody && characterBody.HasBuff(RoR2Content.Buffs.AffixLunar)))
        //          {
        //              self.body.AddTimedBuff(RoR2Content.Buffs.Cripple, 3f);
        //          }

        //           Apply the merc expose //
        //          if ((damageInfo.damageType & DamageType.ApplyMercExpose) != DamageType.Generic)
        //          {
        //              Debug.LogFormat("Adding expose", Array.Empty<object>());
        //              self.body.AddBuff(RoR2Content.Buffs.MercExpose);
        //          }

        //           Check if the brittle crown must trigger //
        //          CharacterMaster master2 = self.body.master;
        //          if (self.itemCounts.goldOnHit > 0 && master2)
        //          {
        //              uint num8 = (uint)(num / self.fullCombinedHealth * master2.money * (float)self.itemCounts.goldOnHit);
        //              float damage = damageInfo.damage;
        //              HealthComponent.ItemCounts itemCounts = self.itemCounts;
        //              uint num9 = num8;
        //              master2.money = (uint)Mathf.Max(0f, master2.money - num9);
        //              EffectManager.SimpleImpactEffect(HealthComponent.AssetReferences.coinImpactEffectPrefab, damageInfo.position, Vector3.up, true);
        //          }

        //           Check if the damage must be reduced by the adaptive armor //
        //          if (self.itemCounts.adaptiveArmor > 0)
        //          {
        //              float num10 = num / self.fullCombinedHealth * 100f * 30f * (float)self.itemCounts.adaptiveArmor;
        //              self.adaptiveArmorValue = Mathf.Min(self.adaptiveArmorValue + num10, 400f);
        //          }

        //           Check if the eclipse8 debuff must be applied //
        //          float num11 = num;
        //          if (self.body.teamComponent.teamIndex == TeamIndex.Player && Run.instance.selectedDifficulty >= DifficultyIndex.Eclipse8)
        //          {
        //              float num12 = num11 / self.fullCombinedHealth * 100f;
        //              float num13 = 0.4f;
        //              int num14 = Mathf.FloorToInt(num12 * num13);
        //              for (int j = 0; j < num14; j++)
        //              {
        //                  self.body.AddBuff(RoR2Content.Buffs.PermanentCurse);
        //              }
        //          }

        //           Check if the barrier must absorb damage //
        //          if (num11 > 0f && self.barrier > 0f)
        //          {
        //              if (num11 <= self.barrier)
        //              {
        //                  self.Networkbarrier = self.barrier - num11;
        //                  num11 = 0f;
        //              }
        //              else
        //              {
        //                  num11 -= self.barrier;
        //                  self.Networkbarrier = 0f;
        //              }
        //          }

        //           Check if the shield must absorb damage //
        //          if (num11 > 0f && self.shield > 0f)
        //          {
        //              if (num11 <= self.shield)
        //              {
        //                  self.Networkshield = self.shield - num11;
        //                  num11 = 0f;
        //              }
        //              else
        //              {
        //                  num11 -= self.shield;
        //                  self.Networkshield = 0f;
        //                  float scale = 1f;
        //                  if (self.body)
        //                  {
        //                      scale = self.body.radius;
        //                  }
        //                  EffectManager.SpawnEffect(HealthComponent.AssetReferences.shieldBreakEffectPrefab, new EffectData
        //                  {
        //                      origin = base.transform.position,
        //                      scale = scale
        //                  }, true);
        //              }
        //          }

        //           Check if the damages are non lethal and apply the damages //
        //          if (num11 > 0f)
        //          {
        //              float num15 = self.health - num11;
        //              if (num15 < 1f && (damageInfo.damageType & DamageType.NonLethal) != DamageType.Generic && self.health >= 1f)
        //              {
        //                  num15 = 1f;
        //              }
        //              self.Networkhealth = num15;
        //          }

        //           Check if the character can be executed //
        //          GameObject gameObject = null;
        //          float num16 = float.NegativeInfinity;
        //           if ((self.body.bodyFlags & CharacterBody.BodyFlags.ImmuneToExecutes) <= CharacterBody.BodyFlags.None)
        //          if (PantheraConfig.canBeExecuted == true)
        //          {
        //              if (self.isInFrozenState && num16 < 0.3f)
        //              {
        //                  num16 = 0.3f;
        //                  gameObject = FrozenState.executeEffectPrefab;
        //              }
        //              if (self.body.isElite && characterBody)
        //              {
        //                  float executeEliteHealthFraction = characterBody.executeEliteHealthFraction;
        //                  if (num16 < executeEliteHealthFraction)
        //                  {
        //                      num16 = executeEliteHealthFraction;
        //                      gameObject = HealthComponent.AssetReferences.executeEffectPrefab;
        //                  }
        //              }
        //          }

        //           Execute the character //
        //          float executionHealthLost = 0f;
        //          bool flag3 = false;
        //          if (PantheraConfig.canBeExecuted == true && num16 > 0f && self.combinedHealthFraction <= num16)
        //          {
        //              flag3 = true;
        //              executionHealthLost = Mathf.Max(self.combinedHealth, 0f);
        //              if (self.health > 0f)
        //              {
        //                  self.Networkhealth = 0f;
        //              }
        //              if (self.shield > 0f)
        //              {
        //                  self.Networkshield = 0f;
        //              }
        //              if (self.barrier > 0f)
        //              {
        //                  self.Networkbarrier = 0f;
        //              }
        //          }

        //           Apply the force //
        //          self.TakeDamageForce(damageInfo, false, false);

        //           Report the damage to the client ? //
        //          DamageReport damageReport = new DamageReport(damageInfo, self, num, combinedHealth);
        //          IOnTakeDamageServerReceiver[] array2 = self.onTakeDamageReceivers;
        //          for (int i = 0; i < array2.Length; i++)
        //          {
        //              array2[i].OnTakeDamageServer(damageReport);
        //          }

        //           Report the damage to all others clients //
        //          if (num > 0f)
        //          {
        //              HealthComponent.SendDamageDealt(damageReport);
        //          }

        //           Set the last hit time //
        //          self.UpdateLastHitTime(damageReport.damageDealt, damageInfo.position, (damageInfo.damageType & DamageType.Silent) > DamageType.Generic, damageInfo.attacker);
        //          if (damageInfo.attacker)
        //          {
        //              List<IOnDamageDealtServerReceiver> gameObjectComponents = GetComponentsCache<IOnDamageDealtServerReceiver>.GetGameObjectComponents(damageInfo.attacker);
        //              foreach (IOnDamageDealtServerReceiver onDamageDealtServerReceiver in gameObjectComponents)
        //              {
        //                  onDamageDealtServerReceiver.OnDamageDealtServer(damageReport);
        //              }
        //              GetComponentsCache<IOnDamageDealtServerReceiver>.ReturnBuffer(gameObjectComponents);
        //          }

        //           Check the inflictor ? //
        //          if (damageInfo.inflictor)
        //          {
        //              List<IOnDamageInflictedServerReceiver> gameObjectComponents2 = GetComponentsCache<IOnDamageInflictedServerReceiver>.GetGameObjectComponents(damageInfo.inflictor);
        //              foreach (IOnDamageInflictedServerReceiver onDamageInflictedServerReceiver in gameObjectComponents2)
        //              {
        //                  onDamageInflictedServerReceiver.OnDamageInflictedServer(damageReport);
        //              }
        //              GetComponentsCache<IOnDamageInflictedServerReceiver>.ReturnBuffer(gameObjectComponents2);
        //          }

        //           Report the damage to the event manager //
        //          GlobalEventManager.ServerDamageDealt(damageReport);

        //           Check if nine lives has to be used //
        //          if (self.alive == false && self.body.HasBuff(Buff.nineLives))
        //          {
        //              NineLives.ApplyNineLives(this);
        //          }

        //           Check if the character is dead //
        //          if (!self.alive)
        //          {
        //               Set this damage as the killing damage //
        //              self.killingDamageType = damageInfo.damageType;

        //               Tell the event manager the character was executer //
        //              if (flag3)
        //              {
        //                  GlobalEventManager.ServerCharacterExecuted(damageReport, executionHealthLost);
        //                  if (gameObject != null)
        //                  {
        //                      EffectManager.SpawnEffect(gameObject, new EffectData
        //                      {
        //                          origin = self.body.corePosition,
        //                          scale = (self.body ? self.body.radius : 1f)
        //                      }, true);
        //                  }
        //              }

        //               Report the death to the server //
        //              IOnKilledServerReceiver[] components = base.GetComponents<IOnKilledServerReceiver>();
        //              for (int i = 0; i < components.Length; i++)
        //              {
        //                  components[i].OnKilledServer(damageReport);
        //              }

        //               Report the killer to the server //
        //              if (damageInfo.attacker)
        //              {
        //                  IOnKilledOtherServerReceiver[] components2 = damageInfo.attacker.GetComponents<IOnKilledOtherServerReceiver>();
        //                  for (int i = 0; i < components2.Length; i++)
        //                  {
        //                      components2[i].OnKilledOtherServer(damageReport);
        //                  }
        //              }

        //               Trigger the death event //
        //              if (Util.CheckRoll(self.globalDeathEventChanceCoefficient * 100f, 0f, null))
        //              {
        //                  GlobalEventManager.instance.OnCharacterDeath(damageReport);
        //                  return;
        //              }

        //          }
        //          else
        //          {

        //               Check if damage have to be sent back //
        //              int a = 5 + 2 * (self.itemCounts.thorns - 1);
        //              if (self.itemCounts.thorns > 0 && !damageReport.damageInfo.procChainMask.HasProc(ProcType.Thorns))
        //              {
        //                  bool flag4 = self.itemCounts.invadingDoppelganger > 0;
        //                  float radius = 25f + 10f * (float)(self.itemCounts.thorns - 1);
        //                  bool isCrit = self.body.RollCrit();
        //                  float damageValue = 1.6f * self.body.damage;
        //                  TeamIndex teamIndex2 = self.body.teamComponent.teamIndex;
        //                  HurtBox[] hurtBoxes = new SphereSearch
        //                  {
        //                      origin = damageReport.damageInfo.position,
        //                      radius = radius,
        //                      mask = LayerIndex.entityPrecise.mask,
        //                      queryTriggerInteraction = QueryTriggerInteraction.UseGlobal
        //                  }.RefreshCandidates().FilterCandidatesByHurtBoxTeam(TeamMask.GetEnemyTeams(teamIndex2)).OrderCandidatesByDistance().FilterCandidatesByDistinctHurtBoxEntities().GetHurtBoxes();
        //                  for (int k = 0; k < Mathf.Min(a, hurtBoxes.Length); k++)
        //                  {
        //                      LightningOrb lightningOrb = new LightningOrb();
        //                      lightningOrb.attacker = base.gameObject;
        //                      lightningOrb.bouncedObjects = null;
        //                      lightningOrb.bouncesRemaining = 0;
        //                      lightningOrb.damageCoefficientPerBounce = 1f;
        //                      lightningOrb.damageColorIndex = DamageColorIndex.Item;
        //                      lightningOrb.damageValue = damageValue;
        //                      lightningOrb.isCrit = isCrit;
        //                      lightningOrb.lightningType = LightningOrb.LightningType.RazorWire;
        //                      lightningOrb.origin = damageReport.damageInfo.position;
        //                      lightningOrb.procChainMask = default(ProcChainMask);
        //                      lightningOrb.procChainMask.AddProc(ProcType.Thorns);
        //                      lightningOrb.procCoefficient = (flag4 ? 0f : 0.5f);
        //                      lightningOrb.range = 0f;
        //                      lightningOrb.teamIndex = teamIndex2;
        //                      lightningOrb.target = hurtBoxes[k];
        //                      OrbManager.instance.AddOrb(lightningOrb);
        //                  }
        //              }
        //          }

        //           Add the barriere related to damage //
        //          float finalDamage = healthBeforeAttack - self.health;
        //          if (finalDamage > 0 && self.health > 0)
        //          {
        //              Barrier.ApplyBarrier(this, finalDamage);
        //          }

        //      }

        #endregion

    }
}
