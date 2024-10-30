using Panthera;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.GUI;
using Panthera.NetworkMessages;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Panthera.BodyComponents
{

    public class PantheraBody : CharacterBody
    {

        public PantheraObj ptraObj;

        public float _frontShield;
        public float _lastFrontShield;
        public float _block;
        public float _lastBlock;
        public float _fury;

        public float frontShield
        {
            set
            {
                _frontShield = value;
                if (_frontShield > maxFrontShield)
                    _frontShield = maxFrontShield;
                if (_frontShield < 0)
                    _frontShield = 0;
                if (_frontShield != _lastFrontShield && RoR2Application.isInMultiPlayer == true && this.ptraObj.hasAuthority() == true)
                {
                    _lastFrontShield = _frontShield;
                    new ServerSetFrontShieldAmount(gameObject, frontShield).Send(NetworkDestination.Server);
                }
            }
            get
            {
                if (_frontShield > maxFrontShield)
                    _frontShield = maxFrontShield;

                return _frontShield;
            }
        }
        public float maxFrontShield
        {
            get
            {
                if (this.ptraObj.profileComponent != null)
                    return this.ptraObj.profileComponent.getMaxFrontShield();
                return PantheraConfig.Default_MaxFrontShield;
            }
        }
        public float block
        {
            set
            {
                _block = value;
                if (_block > PantheraConfig.Default_maxBlock)
                    _block = PantheraConfig.Default_maxBlock;
                if (_block < 0)
                    _block = 0;
                if (_block != _lastBlock && RoR2Application.isInMultiPlayer == true && this.ptraObj.hasAuthority() == true)
                {
                    _lastBlock = _block;
                    new ServerSetBlockAmount(gameObject, _block).Send(NetworkDestination.Server);
                }
            }
            get
            {
                if (_block > PantheraConfig.Default_maxBlock)
                    _block = PantheraConfig.Default_maxBlock;

                return _block;
            }
        }
        public float fury
        {
            set
            {
                if (this.ptraObj.guardianMode == true || this.ptraObj.furyMode == true)
                {
                    if (value > _fury)
                        return;
                }
                _fury = value;
                if (_fury < 0)
                    _fury = 0;
                if (_fury > maxFury)
                    _fury = maxFury;
            }
            get
            {
                return _fury;
            }
        }
        public float trueFury
        {
            set
            {
                _fury = value;
                if (_fury < 0)
                    _fury = 0;
                if (_fury > maxFury)
                    _fury = maxFury;
            }
            get
            {
                return _fury;
            }
        }
        public float maxFury
        {
            get
            {
                if (this.ptraObj.profileComponent != null)
                    return this.ptraObj.profileComponent.getMaxFury();
                return PantheraConfig.Default_MaxFury;
            }
        }
        public float dodge
        {
            get
            {
                if (this.ptraObj.profileComponent != null)
                    return this.ptraObj.profileComponent.getDodge();
                return PantheraConfig.Default_Dodge;
            }
        }
        public float mastery
        {
            get
            {
                if (this.ptraObj.profileComponent != null)
                    return this.ptraObj.profileComponent.getMastery();
                return PantheraConfig.Default_Mastery;
            }
        }

        public void DoInit()
        {
            this.ptraObj = GetComponent<PantheraObj>();
            if (NetworkClient.active == false) return;
            this.fury = 0;
            this.frontShield = 0;
            this.block = 0;
        }
        public static VisibilityLevel GetVisibilityLevelHook(Func<CharacterBody, TeamIndex, VisibilityLevel> orig, CharacterBody self, TeamIndex observerTeam)
        {
            // Check if Panthera //
            if (self is not PantheraBody) return orig(self, observerTeam);
            PantheraObj ptraObj = self.GetComponent<PantheraObj>();

            // Change the Visibility //
            if (ptraObj.stealthed == true)
            {
                return VisibilityLevel.Invisible;
            }
            if (self.hasCloakBuff == false)
            {
                return VisibilityLevel.Visible;
            }
            if (self.teamComponent.teamIndex != observerTeam)
            {
                return VisibilityLevel.Cloaked;
            }
            return VisibilityLevel.Revealed;

        }
        public static void RecalculateStatsHook(Action<CharacterBody> orig, CharacterBody self)
        {

            // Save the Health //
            float actualHealth = self.healthComponent.health;

            // Call the Orig Function //
            orig(self);

            // Check if Portal Surge Mega Boss //
            if (self.gameObject.name.Contains(PantheraConfig.PortalSurge_megaBossAddedName) == true)
            {
                Components.SurgeComponent.SetMegaBossStats(self);
                self.healthComponent.health = actualHealth;
                return;
            }

            // Check if Panthera //
            PantheraBody body = self as PantheraBody;
            if (body == null)
                return;

            // Get the Panthera Object //
            PantheraObj ptraObj = body.GetComponent<PantheraObj>();

            // Get the Profile Component //
            ProfileComponent profile = ptraObj.profileComponent;

            // Check the Profile Component //
            if (profile == null)
                return;
                
            // Apply Panthera Attributes //
            self.maxHealth = profile.getMaxHealth();
            self.regen = profile.getHealthRegen();
            self.moveSpeed = profile.getMoveSpeed();
            self.damage = profile.getDamage();
            self.attackSpeed = profile.getAttackSpeed();
            self.crit = profile.getCritic();
            self.armor = profile.getDefence();
            self.maxJumpCount = (int)profile.getJumpCount();

            // Check Wind Walker //
            if (ptraObj.getAbilityLevel(PantheraConfig.WindWalker_AbilityID) > 0)
                self.maxJumpCount++;

            // Apply the Fury Mode //
            if (ptraObj.furyMode == true)
            {
                self.moveSpeed *= 1 + PantheraConfig.Fury_increasedMoveSpeed;
                self.attackSpeed *= 1 + PantheraConfig.Fury_increasedAttackSpeed;
            }

            // Apply the Guardian Mode //
            if (ptraObj.guardianMode == true)
            {
                self.regen *= 1 + PantheraConfig.Guardian_increasedHealthRegen;
                self.armor *= 1 + PantheraConfig.Guardian_increasedArmor;

                // Apply the Warden's Vitality //
                int wardensVitalityLevel = ptraObj.getAbilityLevel(PantheraConfig.WardensVitality_AbilityID);
                if (wardensVitalityLevel == 1)
                    self.maxHealth *= PantheraConfig.WardensVitality_maxHealthPercent1;
                else if (wardensVitalityLevel == 2)
                    self.maxHealth *= PantheraConfig.WardensVitality_maxHealthPercent2;
                else if (wardensVitalityLevel == 3)
                    self.maxHealth *= PantheraConfig.WardensVitality_maxHealthPercent3;
            }

            // Calculate the model Scale //
            if (ptraObj.hasAuthority() == true)
            {
                float modelScale = PantheraConfig.Model_defaultModelScale;
                if (ptraObj.guardianMode == true)
                    modelScale *= PantheraConfig.Guardian_increasedSize;
                ptraObj.changeModelScale = modelScale;
            }

            // Apply the Resilience Buff //
            int resilienceCount = self.GetBuffCount(Base.Buff.ResilienceBuff.buffIndex);
            if (resilienceCount > 0)
                self.armor *= PantheraConfig.Resilience_percentArmor * resilienceCount + 1;

            // Add max Health from Bizon Steak //
            if (self.inventory != null && ptraObj.getAbilityLevel(PantheraConfig.Predator_AbilityID) > 0)
            {
                int flatHealthItem = self.inventory.GetItemCount(PantheraConfig.ItemChange_steak);
                self.maxHealth += (float)flatHealthItem * PantheraConfig.Predator_steakHealthAdded;
            }

            // Apply the Stealth speed //
            if (ptraObj.stealthed == true)
            {
                float movePenality = 1 - PantheraConfig.Prowl_moveSpeedMultiplier;
                int swiftMovesLevel = ptraObj.getAbilityLevel(PantheraConfig.SwiftMoves_AbilityID);
                if (swiftMovesLevel == 1)
                    movePenality *= 1 - PantheraConfig.SwiftMoves_percent1;
                else if (swiftMovesLevel == 2)
                    movePenality *= 1 - PantheraConfig.SwiftMoves_percent2;
                else if (swiftMovesLevel == 3)
                    movePenality *= 1 - PantheraConfig.SwiftMoves_percent3;

                self.moveSpeed *= 1 - movePenality;

            }

            // Apply the Front Shield speed //
            if (ptraObj.frontShieldObj.active == true && ptraObj.frontShieldDeployed == false)
                self.moveSpeed *= PantheraConfig.FrontShield_moveSpeedMultiplier;

            // Recalculate the Max Barrier //
            self.maxBarrier = self.maxHealth + self.maxShield;

            // Recalculate the Barrier decay rate //
            self.barrierDecayRate = self.maxBarrier / 30f;
            if (ptraObj.guardianMode == true)
                self.barrierDecayRate *= PantheraConfig.Guardian_barrierDecayRatePercent;

            // Restore the Health //
            self.healthComponent.health = Math.Min(actualHealth, self.maxHealth);

            // Update Skill max Stocks and Cooldowns //
            if(ptraObj.hasAuthority() == true)
            {

                // Get the Skill Locator //
                PantheraSkillLocator skillLocator = body.GetComponent<PantheraSkillLocator>();

                // Recalculate Value //
                skillLocator.primary.RecalculateValues();
                skillLocator.secondary.RecalculateValues();
                skillLocator.utility.RecalculateValues();
                skillLocator.special.RecalculateValues();

                // Set Skills max Stock //
                skillLocator.setMaxStock(PantheraConfig.Rip_SkillID, skillLocator.primary.maxStock);
                skillLocator.setMaxStock(PantheraConfig.Slash_SkillID, skillLocator.secondary.maxStock);
                skillLocator.setMaxStock(PantheraConfig.Leap_SkillID, skillLocator.utility.maxStock);
                skillLocator.setMaxStock(PantheraConfig.MightyRoar_SkillID, skillLocator.special.maxStock);

                // Set Skills cooldown //
                skillLocator.setMaxCooldown(PantheraConfig.Rip_SkillID, skillLocator.primary.finalRechargeInterval);
                skillLocator.setMaxCooldown(PantheraConfig.Slash_SkillID, skillLocator.secondary.finalRechargeInterval);
                skillLocator.setMaxCooldown(PantheraConfig.Leap_SkillID, skillLocator.utility.finalRechargeInterval);
                skillLocator.setMaxCooldown(PantheraConfig.MightyRoar_SkillID, skillLocator.special.finalRechargeInterval);

                // Check if Guardian Mode //
                if (ptraObj.guardianMode == true)
                {
                    skillLocator.secondary.maxStock = 1;
                    skillLocator.secondary.finalRechargeInterval = PantheraConfig.FrontShield_cooldown;
                }

                // Check if Fury Mode //
                if (ptraObj.furyMode == true)
                {
                    skillLocator.secondary.maxStock = 1;
                    skillLocator.secondary.finalRechargeInterval = PantheraConfig.ClawsStorm_cooldown;
                }

            }

            // Apply the Eternal Fury Ability //
            int eternalFuryLvl = ptraObj.getAbilityLevel(PantheraConfig.EternalFury_AbilityID);
            float furyDecreaseTime = PantheraConfig.Fury_furyPointsDecreaseTime;
            if (eternalFuryLvl == 1)
                furyDecreaseTime = PantheraConfig.Fury_furyPointsDecreaseTime * (1 + PantheraConfig.EternalFury_reductionPercent1);
            else if (eternalFuryLvl == 2)
                furyDecreaseTime = PantheraConfig.Fury_furyPointsDecreaseTime * (1 + PantheraConfig.EternalFury_reductionPercent2);
            else if (eternalFuryLvl == 3)
                furyDecreaseTime = PantheraConfig.Fury_furyPointsDecreaseTime * (1 + PantheraConfig.EternalFury_reductionPercent3);
            ptraObj.furyDecreaseTime = furyDecreaseTime;

            //// Debug //
            //Utils.DebugInfo.addText("maxHealth", "maxHealth: " + self.maxHealth);
            //Utils.DebugInfo.addText("regen", "regen: " + self.regen);
            //Utils.DebugInfo.addText("moveSpeed", "moveSpeed: " + self.moveSpeed);
            //Utils.DebugInfo.addText("damage", "damage: " + self.damage);
            //Utils.DebugInfo.addText("attackSpeed", "attackSpeed: " + self.attackSpeed);
            //Utils.DebugInfo.addText("crit", "crit: " + self.crit);
            //Utils.DebugInfo.addText("dodge", "dodge: " + body.dodge);
            //Utils.DebugInfo.addText("armor", "armor: " + self.armor);
            //Utils.DebugInfo.addText("mastery", "mastery: " + body.mastery);
            //Utils.DebugInfo.addText("maxFury", "maxFury: " + body.maxFury);
            //Utils.DebugInfo.addText("maxFrontShield", "maxFrontShield: " + body.maxFrontShield);

            //if (NetworkClient.active == false && NetworkServer.active == true)
            //{
            //    Debug.LogWarning("maxHealth: " + self.maxHealth);
            //    Debug.LogWarning("regen: " + self.regen);
            //    Debug.LogWarning("moveSpeed: " + self.moveSpeed);
            //    Debug.LogWarning("damage: " + self.damage);
            //    Debug.LogWarning("attackSpeed: " + self.attackSpeed);
            //    Debug.LogWarning("crit: " + self.crit);
            //    Debug.LogWarning("dodge: " + body.dodge);
            //    Debug.LogWarning("armor: " + self.armor);
            //    Debug.LogWarning("mastery: " + body.mastery);
            //    Debug.LogWarning("maxFury: " + body.maxFury);
            //    Debug.LogWarning("maxFrontShield: " + body.maxFrontShield);
            //}


        }

    }
}
