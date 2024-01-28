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

        #region Attributes
        public float enduranceCopy;
        public float forceCopy;
        public float agilityCopy;
        public float swiftnessCopy;
        public float dexterityCopy;
        public float maxHealthMult
        {
            get
            {
                float mult = 1;
                mult += this.enduranceCopy * 0.05f;
                return mult;
            }
        }
        public float healthRegenMult
        {
            get
            {
                float mult = 1;
                mult += this.enduranceCopy * 0.03f;
                return mult;
            }
        }
        public float moveSpeedMult
        {
            get
            {
                float mult = 1;
                mult += this.agilityCopy * 0.02f;
                mult += this.swiftnessCopy * 0.04f;
                return mult;
            }
        }
        public float damageMult
        {
            get
            {
                float mult = 1;
                mult += this.forceCopy * 0.05f;
                mult += this.dexterityCopy * 0.02f;
                return mult;
            }
        }
        public float attackSpeedMult
        {
            get
            {
                float mult = 1;
                mult += this.agilityCopy * 0.01f;
                mult += this.swiftnessCopy * 0.03f;
                return mult;
            }
        }
        public float critMult
        {
            get
            {
                float mult = 1;
                mult += this.agilityCopy * 0.02f;
                mult += this.dexterityCopy * 0.04f;
                return mult;
            }
        }
        public float DefenseMult
        {
            get
            {
                float mult = 1;
                mult += this.enduranceCopy * 0.02f;
                mult += this.forceCopy * 0.04f;
                return mult;
            }
        }
        #endregion

        #region Stats
        public float _energy;
        public float _stamina;
        public float _power;
        public float _fury = 0;
        public float _comboPoint;
        public float _frontShield;
        public float energy
        {
            set
            {
                _energy = value;
                if (_energy < 0)
                    _energy = 0;

            }
            get
            {
                return _energy;
            }
        }
        public float stamina
        {
            set
            {
                _stamina = value;
                if (_stamina < 0)
                    _stamina = 0;

            }
            get
            {
                return _stamina;
            }
        }
        public float power
        {
            set
            {
                _power = value;
                if (_power < 0)
                    _power = 0;

            }
            get
            {
                return _power;
            }
        }
        public float maxFury = PantheraConfig.Default_MaxFury;
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
        public float comboPoint
        {
            set
            {
                _comboPoint = value;
                if (_comboPoint < 0)
                    _comboPoint = 0;

            }
            get
            {
                return _comboPoint;
            }
        }
        public float _lastFrontShield;
        public float maxFrontShield = 0;
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
        #endregion

        public void DoInit()
        {
            this.ptraObj = GetComponent<PantheraObj>();
            if (NetworkClient.active == false) return;
            this.energy = PantheraConfig.Default_Energy;
            this.stamina = PantheraConfig.Default_Stamina;
            this.power = 0;
            this.fury = 0;
            this.comboPoint = 0;
            this.frontShield = 0;
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

            // Check if Panthera //
            PantheraBody body = self as PantheraBody;
            if (body == null) return;

            // Get the Panthera Object //
            PantheraObj ptraObj = body.GetComponent<PantheraObj>();

            // Apply Panthera Attributes //
            self.maxHealth *= body.maxHealthMult;
            self.regen *= body.healthRegenMult;
            self.moveSpeed *= body.moveSpeedMult;
            self.damage *= body.damageMult;
            self.attackSpeed *= body.attackSpeedMult;
            self.crit *= body.critMult;
            self.armor *= body.DefenseMult;

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
            }

            // Add max Health from Bizon Steak //
            if(self.inventory != null && ptraObj.getAbilityLevel(PantheraConfig.Predator_AbilityID) > 0)
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

            // Calculate the max Front Shield //
            float shieldHealthPercent = PantheraConfig.FrontShield_maxShieldHealthPercent;
            int improvedShieldAbilityLevel = ptraObj.getAbilityLevel(PantheraConfig.ImprovedShield_AbilityID);
            if (improvedShieldAbilityLevel == 1) shieldHealthPercent += PantheraConfig.ImprovedShield_addedPercent1 * self.level;
            else if (improvedShieldAbilityLevel == 2) shieldHealthPercent += PantheraConfig.ImprovedShield_addedPercent2 * self.level;
            else if (improvedShieldAbilityLevel == 3) shieldHealthPercent += PantheraConfig.ImprovedShield_addedPercent3 * self.level;
            body.maxFrontShield = self.maxHealth * shieldHealthPercent;

            // Restore the Health //
            self.healthComponent.health = actualHealth;

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

        }

        //public static void RecalculateStatsHookOld(Action<CharacterBody> orig, CharacterBody self)
        //{

        //    // Check if Panthera //
        //    PantheraBody body = self as PantheraBody;
        //    if (body == null || body.ptraObj == null || body.ptraObj.activePreset == null)
        //    {
        //        orig(self);
        //        return;
        //    }
        //    bool isPanthera = true;

        //    // Return if Inventory is null //
        //    if (self.inventory == null) return;

        //    // --------------------------------------- Recalculate all Stats --------------------------------------- //

        //    #region Buffs
        //    // Get all Buffs //
        //    bool engiShieldBuff = self.HasBuff(RoR2Content.Buffs.EngiShield);
        //    bool engiTeamShieldBuff = self.HasBuff(JunkContent.Buffs.EngiTeamShield);
        //    bool affixLunarBuff = self.HasBuff(RoR2Content.Buffs.AffixLunar);
        //    bool affixLunarBlueBuff = self.HasBuff(RoR2Content.Buffs.AffixBlue);
        //    bool meatRegenBoostBuff = self.HasBuff(JunkContent.Buffs.MeatRegenBoost);
        //    int crocoRegenBuff = self.GetBuffCount(RoR2Content.Buffs.CrocoRegen);
        //    bool onFireBuff = self.HasBuff(RoR2Content.Buffs.OnFire);
        //    bool strongerBurnBuff = self.HasBuff(DLC1Content.Buffs.StrongerBurn);
        //    int killMoveSpeedBuff = self.GetBuffCount(DLC1Content.Buffs.KillMoveSpeed);
        //    bool whipBoostBuff = self.HasBuff(RoR2Content.Buffs.WhipBoost);
        //    bool bugWingsBuff = self.HasBuff(RoR2Content.Buffs.BugWings);
        //    bool warBannerBuff = self.HasBuff(RoR2Content.Buffs.Warbanner);
        //    bool EnrageAncientWispBuff = self.HasBuff(JunkContent.Buffs.EnrageAncientWisp);
        //    bool cloakSpeedBuff = self.HasBuff(RoR2Content.Buffs.CloakSpeed);
        //    bool warCryBuff = self.HasBuff(RoR2Content.Buffs.WarCryBuff);
        //    bool teamWarCryBuff = self.HasBuff(RoR2Content.Buffs.TeamWarCry);
        //    bool Slow30Buff = self.HasBuff(JunkContent.Buffs.Slow30);
        //    bool Slow50Buff = self.HasBuff(RoR2Content.Buffs.Slow50);
        //    bool Slow60Buff = self.HasBuff(RoR2Content.Buffs.Slow60);
        //    bool Slow80Buff = self.HasBuff(RoR2Content.Buffs.Slow80);
        //    bool clayGooBuff = self.HasBuff(RoR2Content.Buffs.ClayGoo);
        //    bool crippleBuff = self.HasBuff(RoR2Content.Buffs.Cripple);
        //    bool jailerSlowBuff = self.HasBuff(DLC1Content.Buffs.JailerSlow);
        //    int beetleJuiceBuff = self.GetBuffCount(RoR2Content.Buffs.BeetleJuice);
        //    bool entangleBuff = self.HasBuff(RoR2Content.Buffs.Entangle);
        //    bool nullifierBuff = self.HasBuff(RoR2Content.Buffs.Nullified);
        //    bool lunarSecondaryRootBuff = self.HasBuff(RoR2Content.Buffs.LunarSecondaryRoot);
        //    bool goldEmpoweredBuff = self.HasBuff(JunkContent.Buffs.GoldEmpowered);
        //    bool powerBuff = self.HasBuff(RoR2Content.Buffs.PowerBuff);
        //    int attackSpeedOnCritBuff = self.GetBuffCount(RoR2Content.Buffs.AttackSpeedOnCrit);
        //    bool energizedBuff = self.HasBuff(RoR2Content.Buffs.Energized);
        //    bool fullCritBuff = self.HasBuff(RoR2Content.Buffs.FullCrit);
        //    bool armorBoostBuff = self.HasBuff(RoR2Content.Buffs.ArmorBoost);
        //    bool smallArmorBoostBuff = self.HasBuff(RoR2Content.Buffs.SmallArmorBoost);
        //    bool outOfCombatArmorBuff = self.HasBuff(DLC1Content.Buffs.OutOfCombatArmorBuff);
        //    bool elephantArmorBoostBuff = self.HasBuff(RoR2Content.Buffs.ElephantArmorBoost);
        //    bool voidSurvivorCorruptModeBuff = self.HasBuff(DLC1Content.Buffs.VoidSurvivorCorruptMode);
        //    bool pulverizedBuff = self.HasBuff(RoR2Content.Buffs.Pulverized);
        //    int permanentDebuffBuff = self.GetBuffCount(DLC1Content.Buffs.PermanentDebuff);
        //    int permanentCurseBuff = self.GetBuffCount(RoR2Content.Buffs.PermanentCurse);
        //    bool weakBuff = self.HasBuff(RoR2Content.Buffs.Weak);
        //    bool tonicBuff = self.HasBuff(RoR2Content.Buffs.TonicBuff);
        //    int tonicAffliction = self.inventory.GetItemCount(RoR2Content.Items.TonicAffliction);
        //    bool blindedBuff = self.HasBuff(DLC1Content.Buffs.Blinded);
        //    #endregion

        //    #region Equipment
        //    // Get the actual Equipment //
        //    EquipmentIndex equipmentIndex = self.inventory.currentEquipmentIndex;
        //    EquipmentDef equipmentDef = EquipmentCatalog.GetEquipmentDef(equipmentIndex);
        //    int eliteVoidEquipment = ((equipmentIndex == DLC1Content.Equipment.EliteVoidEquipment.equipmentIndex) ? 1 : 0);
        //    bool eliteYellowEquipment = (equipmentDef == JunkContent.Equipment.EliteYellowEquipment) ? true : false;
        //    #endregion

        //    #region Items
        //    // Get all Items //
        //    int boostHpItem = self.inventory.GetItemCount(RoR2Content.Items.BoostHp);
        //    int pearlItem = self.inventory.GetItemCount(RoR2Content.Items.Pearl);
        //    int shinyPearlItem = self.inventory.GetItemCount(RoR2Content.Items.ShinyPearl);
        //    int halfSpeedDoubleHealthItem = self.inventory.GetItemCount(DLC1Content.Items.HalfSpeedDoubleHealth);
        //    int infusionItem = self.inventory.GetItemCount(RoR2Content.Items.Infusion);
        //    uint infusionBonusItem = self.inventory.infusionBonus;
        //    int personalShieldItem = self.inventory.GetItemCount(RoR2Content.Items.PersonalShield);
        //    int flatHealthItem = self.inventory.GetItemCount(RoR2Content.Items.FlatHealth);
        //    int knurlItem = self.inventory.GetItemCount(RoR2Content.Items.Knurl);
        //    int cutHPItem = self.inventory.GetItemCount(RoR2Content.Items.CutHp);
        //    int invadingDoppelgangerItem = self.inventory.GetItemCount(RoR2Content.Items.InvadingDoppelganger);
        //    int summonedEchoItem = self.inventory.GetItemCount(RoR2Content.Items.SummonedEcho);
        //    int missileVoidItem = self.inventory.GetItemCount(DLC1Content.Items.MissileVoid);
        //    int shieldOnlyItem = self.inventory.GetItemCount(RoR2Content.Items.ShieldOnly);
        //    int healWhileSafeItem = self.inventory.GetItemCount(RoR2Content.Items.HealWhileSafe);
        //    int drizzlePlayerHelperItem = self.inventory.GetItemCount(RoR2Content.Items.DrizzlePlayerHelper);
        //    int monsoonPlayerHelperItem = self.inventory.GetItemCount(RoR2Content.Items.MonsoonPlayerHelper);
        //    int healthDecayItem = self.inventory.GetItemCount(RoR2Content.Items.HealthDecay);
        //    int hoofItem = self.inventory.GetItemCount(RoR2Content.Items.Hoof);
        //    int attackSpeedAndMoveSpeedItem = self.inventory.GetItemCount(DLC1Content.Items.AttackSpeedAndMoveSpeed);
        //    int sprintBonusItem = self.inventory.GetItemCount(RoR2Content.Items.SprintBonus);
        //    int sprintOutOfCombatItem = self.inventory.GetItemCount(RoR2Content.Items.SprintOutOfCombat);
        //    int featherItem = self.inventory.GetItemCount(RoR2Content.Items.Feather);
        //    int boostDamageItem = self.inventory.GetItemCount(RoR2Content.Items.BoostDamage);
        //    int teamSizeDamageBonusItem = self.inventory.GetItemCount(RoR2Content.Items.TeamSizeDamageBonus);
        //    int boostAttackSpeedItem = self.inventory.GetItemCount(RoR2Content.Items.BoostAttackSpeed);
        //    int syringeItem = self.inventory.GetItemCount(RoR2Content.Items.Syringe);
        //    int droneWeaponsBoostItem = self.inventory.GetItemCount(DLC1Content.Items.DroneWeaponsBoost);
        //    int halfAttackSpeedHalfCooldownsItem = self.inventory.GetItemCount(DLC1Content.Items.HalfAttackSpeedHalfCooldowns);
        //    int critDamageItem = self.inventory.GetItemCount(DLC1Content.Items.CritDamage);
        //    int critGlassesItem = self.inventory.GetItemCount(RoR2Content.Items.CritGlasses);
        //    int attackSpeedOnCritItem = self.inventory.GetItemCount(RoR2Content.Items.AttackSpeedOnCrit);
        //    int bleedOnHitAndExplodeItem = self.inventory.GetItemCount(RoR2Content.Items.BleedOnHitAndExplode);
        //    int cooldownOnCritItem = self.inventory.GetItemCount(JunkContent.Items.CooldownOnCrit);
        //    int healOnCritItem = self.inventory.GetItemCount(RoR2Content.Items.HealOnCrit);
        //    int critHealItem = self.inventory.GetItemCount(JunkContent.Items.CritHeal);
        //    int convertCritChanceToCritDamageItem = self.inventory.GetItemCount(DLC1Content.Items.ConvertCritChanceToCritDamage);
        //    int outOfCombatArmorItem = self.inventory.GetItemCount(DLC1Content.Items.OutOfCombatArmor);
        //    int sprintArmorItem = self.inventory.GetItemCount(RoR2Content.Items.SprintArmor);
        //    int lunarBadLuckItem = self.inventory.GetItemCount(RoR2Content.Items.LunarBadLuck);
        //    int alienHeadItem = self.inventory.GetItemCount(RoR2Content.Items.AlienHead);
        //    int secondarySkillMagazineItem = self.inventory.GetItemCount(RoR2Content.Items.SecondarySkillMagazine);
        //    int utilitySkillMagazineItem = self.inventory.GetItemCount(RoR2Content.Items.UtilitySkillMagazine);
        //    int EquipmentMagazineVoidItem = self.inventory.GetItemCount(DLC1Content.Items.EquipmentMagazineVoid);
        //    int bleedOnHitItem = self.inventory.GetItemCount(RoR2Content.Items.BleedOnHit);
        //    #endregion

        //    #region Level
        //    // Get the Level //
        //    float characterlevel = self.level;
        //    TeamManager.instance.GetTeamExperience(self.teamComponent.teamIndex);
        //    float teamLevel = TeamManager.instance.GetTeamLevel(self.teamComponent.teamIndex);
        //    self.level = teamLevel;
        //    // Ambient Level? Should be used by Monsters //
        //    int ambientLevelItem = self.inventory.GetItemCount(RoR2Content.Items.UseAmbientLevel);
        //    if (ambientLevelItem > 0)
        //    {
        //        self.level = Math.Max(self.level, (float)Run.instance.ambientLevelFloor);
        //    }
        //    // Level Bonus?? Uknown Item //
        //    int levelBonusItem = self.inventory.GetItemCount(RoR2Content.Items.LevelBonus);
        //    self.level += (float)levelBonusItem;
        //    // The Level - 1 //
        //    float levelBase = self.level - 1f;
        //    #endregion

        //    #region Flags
        //    // Check if Elite, not really used for Panthera //
        //    self.isElite = (self.eliteBuffCount > 0);

        //    // Protect from One Shot //
        //    self.hasOneShotProtection = self.isPlayerControlled;
        //    self.oneShotProtectionFraction = 0.1f;

        //    // Check if is Glass ? //
        //    int lunarDaggerItem = self.inventory.GetItemCount(RoR2Content.Items.LunarDagger);
        //    bool glassArtifact = self.teamComponent.teamIndex == TeamIndex.Player && RunArtifactManager.instance.IsArtifactEnabled(RoR2Content.Artifacts.glassArtifactDef);
        //    self.isGlass = (glassArtifact || lunarDaggerItem > 0);

        //    // Check if Back Stab //
        //    self.canPerformBackstab = ((self.bodyFlags & CharacterBody.BodyFlags.HasBackstabPassive) == CharacterBody.BodyFlags.HasBackstabPassive);
        //    self.canReceiveBackstab = ((self.bodyFlags & CharacterBody.BodyFlags.HasBackstabImmunity) != CharacterBody.BodyFlags.HasBackstabImmunity);
        //    #endregion

        //    #region Cooldown Reduction
        //    // Not used by Panthera !! //
        //    if (isPanthera == false)
        //    {
        //        float flatCooldownReduction = 0f;
        //        if (lunarBadLuckItem > 0)
        //        {
        //            flatCooldownReduction += 2f + 1f * (float)(lunarBadLuckItem - 1);
        //        }
        //        float cooldownScale = 1f;
        //        if (goldEmpoweredBuff == true)
        //        {
        //            cooldownScale *= 0.25f;
        //        }
        //        for (int i = 0; i < alienHeadItem; i++)
        //        {
        //            cooldownScale *= 0.75f;
        //        }
        //        for (int j = 0; j < halfAttackSpeedHalfCooldownsItem; j++)
        //        {
        //            cooldownScale *= 0.5f;
        //        }
        //        for (int k = 0; k < droneWeaponsBoostItem; k++)
        //        {
        //            cooldownScale *= 0.5f;
        //        }
        //        if (self.teamComponent.teamIndex == TeamIndex.Monster && Run.instance.selectedDifficulty >= DifficultyIndex.Eclipse7)
        //        {
        //            cooldownScale *= 0.5f;
        //        }
        //        if (self.HasBuff(RoR2Content.Buffs.NoCooldowns))
        //        {
        //            cooldownScale = 0f;
        //        }
        //        if (self.skillLocator.primary)
        //        {
        //            self.skillLocator.primary.cooldownScale = cooldownScale;
        //            self.skillLocator.primary.flatCooldownReduction = flatCooldownReduction;
        //        }
        //        // ------------------------------------------- Create compilation error ???? ------------------------------------------- //
        //        //if (self.skillLocator.secondaryBonusStockSkill)
        //        //{
        //        //    self.skillLocator.secondaryBonusStockSkill.cooldownScale = cooldownScale;
        //        //    self.skillLocator.secondaryBonusStockSkill.SetBonusStockFromBody(secondarySkillMagazineItem);
        //        //    self.skillLocator.secondaryBonusStockSkill.flatCooldownReduction = flatCooldownReduction;
        //        //}
        //        //if (self.skillLocator.utilityBonusStockSkill)
        //        //{
        //        //    float cooldownScale2 = cooldownScale;
        //        //    if (utilitySkillMagazineItem > 0)
        //        //    {
        //        //        cooldownScale2 *= 0.6666667f;
        //        //    }
        //        //    self.skillLocator.utilityBonusStockSkill.cooldownScale = cooldownScale2;
        //        //    self.skillLocator.utilityBonusStockSkill.flatCooldownReduction = flatCooldownReduction;
        //        //    self.skillLocator.utilityBonusStockSkill.SetBonusStockFromBody(utilitySkillMagazineItem * 2);
        //        //}
        //        //if (self.skillLocator.specialBonusStockSkill)
        //        //{
        //        //    self.skillLocator.specialBonusStockSkill.cooldownScale = cooldownScale;
        //        //    if (EquipmentMagazineVoidItem > 0)
        //        //    {
        //        //        self.skillLocator.specialBonusStockSkill.cooldownScale *= 0.67f;
        //        //    }
        //        //    self.skillLocator.specialBonusStockSkill.flatCooldownReduction = flatCooldownReduction;
        //        //    self.skillLocator.specialBonusStockSkill.SetBonusStockFromBody(EquipmentMagazineVoidItem);
        //        // --------------------------------------------------------------------------------------------------------------------- //
        //    }

        //    #endregion

        //    #region Max Health
        //    // Calcule Max Health //
        //    float maxHealth = self.maxHealth;
        //    float baseMaxHealth = self.baseMaxHealth + self.levelMaxHealth * levelBase;
        //    float baseMaxHealthMultiplier = 1f;
        //    baseMaxHealthMultiplier += (float)boostHpItem * 0.1f;
        //    baseMaxHealthMultiplier += (float)(pearlItem + shinyPearlItem) * 0.1f;
        //    baseMaxHealthMultiplier += (float)eliteVoidEquipment * 0.5f;
        //    baseMaxHealthMultiplier += (float)halfSpeedDoubleHealthItem * 1f;
        //    if (infusionItem > 0)
        //    {
        //        baseMaxHealth += infusionBonusItem;
        //    }
        //    // ------- Panthera Modif: Bizon Steak ------- //
        //    // Bizon Steak add more Health to Panthera //
        //    baseMaxHealth += (float)flatHealthItem * PantheraConfig.ItemChange_steakHealthAdded;
        //    // ------------------------------------------- //
        //    baseMaxHealth += (float)knurlItem * 40f;
        //    baseMaxHealth *= baseMaxHealthMultiplier;
        //    baseMaxHealth /= (float)(cutHPItem + 1);
        //    if (invadingDoppelgangerItem > 0)
        //    {
        //        baseMaxHealth *= 10f;
        //    }
        //    if (summonedEchoItem > 0)
        //    {
        //        baseMaxHealth *= 0.1f;
        //    }
        //    self.maxHealth = baseMaxHealth;
        //    #endregion

        //    #region Max Shield
        //    // Calcule the Shield //
        //    float maxShield = self.maxShield;
        //    float baseMaxShield = self.baseMaxShield + self.levelMaxShield * levelBase;
        //    baseMaxShield += (float)personalShieldItem * 0.08f * self.maxHealth;
        //    if (engiShieldBuff == true)
        //    {
        //        baseMaxShield += self.maxHealth * 1f;
        //    }
        //    if (engiTeamShieldBuff == true)
        //    {
        //        baseMaxShield += self.maxHealth * 0.5f;
        //    }
        //    if (missileVoidItem > 0)
        //    {
        //        baseMaxShield += self.maxHealth * 0.1f;
        //    }
        //    // -------- Panthera Modif: No Shield -------- //
        //    // Those one just don't have to Happen !! //
        //    if ((shieldOnlyItem > 0 || affixLunarBuff == true) && isPanthera == false)
        //    {
        //        baseMaxShield += self.maxHealth * (1.5f + (float)(shieldOnlyItem - 1) * 0.25f);
        //        self.maxHealth = 1f;
        //    }
        //    if (affixLunarBlueBuff == true && isPanthera == false)
        //    {
        //        float newHealth = self.maxHealth * 0.5f;
        //        self.maxHealth -= newHealth;
        //        baseMaxShield += self.maxHealth;
        //    }
        //    // ------------------------------------------- //
        //    // -------- Panthera Modif: No Shield -------- //
        //    // Remove Shield and add to Health //
        //    self.maxShield = 0;
        //    self.maxHealth += baseMaxShield;
        //    // ------------------------------------------- //
        //    #endregion

        //    #region Health Regen
        //    // Calcule the Health Regen //
        //    float baseRegen = self.baseRegen + self.levelRegen * levelBase;
        //    float baseAddedRegen = 1f + levelBase * 0.2f;
        //    float knurlItemRegen = (float)knurlItem * 1.6f * baseAddedRegen;
        //    float outOfDangerRegen = ((self.outOfDanger && healWhileSafeItem > 0) ? (3f * (float)healWhileSafeItem) : 0f) * baseAddedRegen;
        //    float addedMeatRegen = (meatRegenBoostBuff == true ? 2f : 0f) * baseAddedRegen;
        //    float addedCrocoRegen = (float)crocoRegenBuff * self.maxHealth * 0.1f;
        //    float addedShinyPerlRegen = (float)shinyPearlItem * 0.1f * baseAddedRegen;
        //    float helperAddedRegen = 1f;
        //    if (drizzlePlayerHelperItem > 0)
        //    {
        //        helperAddedRegen += 0.5f;
        //    }
        //    if (monsoonPlayerHelperItem > 0)
        //    {
        //        helperAddedRegen -= 0.4f;
        //    }
        //    float totalRegenAdded = (baseRegen + knurlItemRegen + outOfDangerRegen + addedMeatRegen + addedShinyPerlRegen) * helperAddedRegen;
        //    if (onFireBuff == true || strongerBurnBuff == true)
        //    {
        //        totalRegenAdded = Mathf.Min(0f, totalRegenAdded);
        //    }
        //    totalRegenAdded += addedCrocoRegen;
        //    // -------- Panthera Modif: No Shield -------- //
        //    // Those one just don't have to Happen !! //
        //    if ((shieldOnlyItem > 0 || affixLunarBuff == true) && isPanthera == false)
        //    {
        //        totalRegenAdded = Mathf.Max(totalRegenAdded, 0f);
        //    }
        //    // ------------------------------------------- //
        //    if (healthDecayItem > 0)
        //    {
        //        totalRegenAdded = Mathf.Min(totalRegenAdded, 0f) - self.maxHealth / self.cursePenalty / (float)healthDecayItem;
        //    }
        //    self.regen = totalRegenAdded;
        //    #endregion

        //    #region Move Speed
        //    float baseMoveSpeed = self.baseMoveSpeed + self.levelMoveSpeed * levelBase;
        //    float moveSpeedMultiplier = 1f;
        //    if (eliteYellowEquipment == true)
        //    {
        //        baseMoveSpeed += 2f;
        //    }
        //    if (body.ptraObj.dashing == true)
        //    {
        //        baseMoveSpeed *= body.ptraObj.activePreset.dash_speedMultiplier;
        //    }
        //    else if (self.isSprinting == true)
        //    {
        //        baseMoveSpeed *= self.sprintingSpeedMultiplier;
        //    }
        //    moveSpeedMultiplier += (float)hoofItem * 0.14f;
        //    moveSpeedMultiplier += (float)attackSpeedAndMoveSpeedItem * 0.07f;
        //    moveSpeedMultiplier += (float)shinyPearlItem * 0.1f;
        //    moveSpeedMultiplier += (float)killMoveSpeedBuff * 0.25f;
        //    if (self.teamComponent.teamIndex == TeamIndex.Monster && Run.instance.selectedDifficulty >= DifficultyIndex.Eclipse4)
        //    {
        //        moveSpeedMultiplier += 0.4f;
        //    }
        //    if (body.ptraObj.dashing == true && sprintBonusItem > 0)
        //    {
        //        moveSpeedMultiplier += 0.25f * (float)sprintBonusItem / body.ptraObj.activePreset.dash_speedMultiplier;
        //    }
        //    else if (self.isSprinting && sprintBonusItem > 0)
        //    {
        //        moveSpeedMultiplier += 0.25f * (float)sprintBonusItem / self.sprintingSpeedMultiplier;
        //    }
        //    if (sprintOutOfCombatItem > 0 && whipBoostBuff == true)
        //    {
        //        moveSpeedMultiplier += (float)sprintOutOfCombatItem * 0.3f;
        //    }
        //    if (summonedEchoItem > 0)
        //    {
        //        moveSpeedMultiplier += 0.66f;
        //    }
        //    if (bugWingsBuff == true)
        //    {
        //        moveSpeedMultiplier += 0.2f;
        //    }
        //    if (warBannerBuff == true)
        //    {
        //        moveSpeedMultiplier += 0.3f;
        //    }
        //    if (EnrageAncientWispBuff == true)
        //    {
        //        moveSpeedMultiplier += 0.4f;
        //    }
        //    if (cloakSpeedBuff == true)
        //    {
        //        moveSpeedMultiplier += 0.4f;
        //    }
        //    if (warCryBuff == true || teamWarCryBuff == true)
        //    {
        //        moveSpeedMultiplier += 0.5f;
        //    }
        //    if (engiTeamShieldBuff == true)
        //    {
        //        moveSpeedMultiplier += 0.3f;
        //    }
        //    if (affixLunarBuff == true)
        //    {
        //        moveSpeedMultiplier += 0.3f;
        //    }
        //    float moveSpeedDivider = 1f;
        //    if (Slow30Buff == true)
        //    {
        //        moveSpeedDivider += 0.3f;
        //    }
        //    if (Slow50Buff == true)
        //    {
        //        moveSpeedDivider += 0.5f;
        //    }
        //    if (Slow60Buff == true)
        //    {
        //        moveSpeedDivider += 0.6f;
        //    }
        //    if (Slow80Buff == true)
        //    {
        //        moveSpeedDivider += 0.8f;
        //    }
        //    if (clayGooBuff == true)
        //    {
        //        moveSpeedDivider += 0.5f;
        //    }
        //    if (crippleBuff == true)
        //    {
        //        moveSpeedDivider += 1f;
        //    }
        //    if (jailerSlowBuff == true)
        //    {
        //        moveSpeedDivider += 1f;
        //    }
        //    moveSpeedDivider += (float)halfSpeedDoubleHealthItem * 1f;
        //    baseMoveSpeed *= moveSpeedMultiplier / moveSpeedDivider;
        //    if (beetleJuiceBuff > 0)
        //    {
        //        baseMoveSpeed *= 1f - 0.05f * (float)beetleJuiceBuff;
        //    }
        //    self.moveSpeed = baseMoveSpeed;
        //    #endregion

        //    #region Acceleration
        //    self.acceleration = self.moveSpeed / self.baseMoveSpeed * self.baseAcceleration;
        //    if (entangleBuff == true || nullifierBuff == true || lunarSecondaryRootBuff == true)
        //    {
        //        self.moveSpeed = 0f;
        //        self.acceleration = 80f;
        //    }
        //    // ------- Panthera Modif: Start Sleep ------- //
        //    // Stop the acceleration if Panthera is Sleeping //
        //    if (body.ptraObj.getPassiveScript() != null && body.ptraObj.getPassiveScript().isSleeping == true)
        //    {
        //        self.moveSpeed = 0;
        //        self.acceleration = 0;
        //    }
        //    // Stop the acceleration if Panthera is Dead //
        //    if (self.healthComponent != null && self.healthComponent.alive == false)
        //    {
        //        self.moveSpeed = 0;
        //        self.acceleration = 0;
        //    }
        //    // ------------------------------------------- //
        //    #endregion

        //    #region Jump
        //    self.jumpPower = self.baseJumpPower + self.levelJumpPower * levelBase;
        //    self.maxJumpHeight = Trajectory.CalculateApex(self.jumpPower);
        //    self.maxJumpCount = self.baseJumpCount + featherItem;
        //    #endregion

        //    #region Damage
        //    float baseDamage = self.baseDamage + self.levelDamage * levelBase;
        //    float baseDamageMultiplier = 1f;
        //    if (boostDamageItem > 0)
        //    {
        //        baseDamageMultiplier += (float)boostDamageItem * 0.1f;
        //    }
        //    if (teamSizeDamageBonusItem > 0)
        //    {
        //        int num69 = Math.Max(TeamComponent.GetTeamMembers(self.teamComponent.teamIndex).Count - 1, 0);
        //        baseDamageMultiplier += (float)(num69 * teamSizeDamageBonusItem) * 1f;
        //    }
        //    if (beetleJuiceBuff > 0)
        //    {
        //        baseDamageMultiplier -= 0.05f * (float)beetleJuiceBuff;
        //    }
        //    if (goldEmpoweredBuff == true)
        //    {
        //        baseDamageMultiplier += 1f;
        //    }
        //    if (powerBuff == true)
        //    {
        //        baseDamageMultiplier += 0.5f;
        //    }
        //    baseDamageMultiplier += (float)shinyPearlItem * 0.1f;
        //    baseDamageMultiplier += Mathf.Pow(2f, (float)lunarDaggerItem) - 1f;
        //    baseDamageMultiplier -= (float)eliteVoidEquipment * 0.3f;
        //    baseDamage *= baseDamageMultiplier;
        //    if (invadingDoppelgangerItem > 0)
        //    {
        //        baseDamage *= 0.04f;
        //    }
        //    if (glassArtifact)
        //    {
        //        baseDamage *= 5f;
        //    }
        //    self.damage = baseDamage;
        //    #endregion

        //    #region Attack Speed
        //    float baseAttackSpeed = self.baseAttackSpeed + self.levelAttackSpeed * levelBase;
        //    float attackSpeedMultiplier = 1f;
        //    attackSpeedMultiplier += (float)boostAttackSpeedItem * 0.1f;
        //    attackSpeedMultiplier += (float)syringeItem * 0.15f;
        //    attackSpeedMultiplier += (float)attackSpeedAndMoveSpeedItem * 0.075f;
        //    attackSpeedMultiplier += (float)droneWeaponsBoostItem * 0.5f;
        //    if (eliteYellowEquipment == true)
        //    {
        //        attackSpeedMultiplier += 0.5f;
        //    }
        //    attackSpeedMultiplier += (float)attackSpeedOnCritBuff * 0.12f;
        //    if (warBannerBuff == true)
        //    {
        //        attackSpeedMultiplier += 0.3f;
        //    }
        //    if (energizedBuff == true)
        //    {
        //        attackSpeedMultiplier += 0.7f;
        //    }
        //    if (warCryBuff == true || teamWarCryBuff == true)
        //    {
        //        attackSpeedMultiplier += 1f;
        //    }
        //    attackSpeedMultiplier += (float)shinyPearlItem * 0.1f;
        //    attackSpeedMultiplier /= (float)(halfAttackSpeedHalfCooldownsItem + 1);
        //    attackSpeedMultiplier = Mathf.Max(attackSpeedMultiplier, 0.1f);
        //    baseAttackSpeed *= attackSpeedMultiplier;
        //    if (beetleJuiceBuff > 0)
        //    {
        //        baseAttackSpeed *= 1f - 0.05f * (float)beetleJuiceBuff;
        //    }
        //    self.attackSpeed = baseAttackSpeed;
        //    #endregion

        //    #region Critical Strike
        //    self.critMultiplier = 2f + 1f * (float)critDamageItem;
        //    float baseCrit = self.baseCrit + self.levelCrit * levelBase;
        //    baseCrit += (float)critGlassesItem * 10f;
        //    if (attackSpeedOnCritItem > 0)
        //    {
        //        baseCrit += 5f;
        //    }
        //    if (bleedOnHitAndExplodeItem > 0)
        //    {
        //        baseCrit += 5f;
        //    }
        //    if (cooldownOnCritItem > 0)
        //    {
        //        baseCrit += 5f;
        //    }
        //    if (healOnCritItem > 0)
        //    {
        //        baseCrit += 5f;
        //    }
        //    if (critHealItem > 0)
        //    {
        //        baseCrit += 5f;
        //    }
        //    if (fullCritBuff == true)
        //    {
        //        baseCrit += 100f;
        //    }
        //    // ------- Panthera Modif: Pecision Ability ------- //
        //    // Add Critical Strike related to the Precision Ability level //
        //    if (body.ptraObj.activePreset.precision_critic > 0 && body.ptraObj.detectionActivated == true)
        //    {
        //        baseCrit += body.ptraObj.activePreset.precision_critic * 100;
        //    }
        //    // ------------------------------------------------ //
        //    baseCrit += (float)shinyPearlItem * 10f;
        //    if (convertCritChanceToCritDamageItem == 0)
        //    {
        //        self.crit = baseCrit;
        //    }
        //    else
        //    {
        //        self.critMultiplier += baseCrit * 0.01f;
        //        self.crit = 0f;
        //    }
        //    #endregion

        //    #region Armor
        //    self.armor = self.baseArmor + self.levelArmor * levelBase;
        //    if (shinyPearlItem > 0)
        //    {
        //        self.armor *= 1f + 0.1f * (float)shinyPearlItem;
        //    }
        //    self.armor += (float)drizzlePlayerHelperItem * 70f;
        //    self.armor += (armorBoostBuff == true ? 200f : 0f);
        //    self.armor += (smallArmorBoostBuff == true ? 100f : 0f);
        //    self.armor += (outOfCombatArmorBuff == true ? (100f * (float)outOfCombatArmorItem) : 0f);
        //    self.armor += (elephantArmorBoostBuff == true ? 500f : 0f);
        //    self.armor += (voidSurvivorCorruptModeBuff == true ? 100f : 0f);
        //    if (crippleBuff == true)
        //    {
        //        self.armor -= 20f;
        //    }
        //    if (pulverizedBuff == true)
        //    {
        //        self.armor -= 60f;
        //    }
        //    if (self.isSprinting && sprintArmorItem > 0)
        //    {
        //        self.armor += (float)(sprintArmorItem * 30);
        //    }
        //    self.armor -= (float)permanentDebuffBuff * 2f;
        //    #endregion

        //    #region Crit Heal
        //    self.critHeal = 0f;
        //    if (critHealItem > 0)
        //    {
        //        float crit = self.crit;
        //        self.crit /= (float)(critHealItem + 1);
        //        self.critHeal = crit - self.crit;
        //    }
        //    #endregion

        //    #region Curse and Weak
        //    self.cursePenalty = 1f;
        //    if (lunarDaggerItem > 0)
        //    {
        //        self.cursePenalty = Mathf.Pow(2f, (float)lunarDaggerItem);
        //    }
        //    if (glassArtifact)
        //    {
        //        self.cursePenalty *= 10f;
        //    }
        //    if (permanentCurseBuff > 0)
        //    {
        //        self.cursePenalty += (float)permanentCurseBuff * 0.01f;
        //    }
        //    if (weakBuff == true)
        //    {
        //        self.armor -= 30f;
        //        self.damage *= 0.6f;
        //        self.moveSpeed *= 0.6f;
        //    }
        //    if (tonicBuff == true)
        //    {
        //        self.maxHealth *= 1.5f;
        //        self.maxShield *= 1.5f;
        //        self.attackSpeed *= 1.7f;
        //        self.moveSpeed *= 1.3f;
        //        self.armor += 20f;
        //        self.damage *= 2f;
        //        self.regen *= 4f;
        //    }
        //    else if (tonicAffliction > 0)
        //    {
        //        float afflictionMultiplier = Mathf.Pow(0.95f, (float)tonicAffliction);
        //        self.attackSpeed *= afflictionMultiplier;
        //        self.moveSpeed *= afflictionMultiplier;
        //        self.damage *= afflictionMultiplier;
        //        self.regen *= afflictionMultiplier;
        //        self.cursePenalty += 0.1f * (float)tonicAffliction;
        //    }
        //    self.maxHealth /= self.cursePenalty;
        //    self.maxShield /= self.cursePenalty;
        //    self.oneShotProtectionFraction = Mathf.Max(0f, self.oneShotProtectionFraction - (1f - 1f / self.cursePenalty));
        //    #endregion

        //    #region Barrier
        //    self.maxBarrier = self.maxHealth + self.maxShield;
        //    // -------- Panthera Modif: Decay Rate -------- //
        //    self.barrierDecayRate = (self.maxBarrier / 30f) * body.ptraObj.activePreset.barrierDecayRateMultiplier;
        //    // -------------------------------------------- //
        //    #endregion

        //    #region Bleed Chance
        //    self.bleedChance = 10f * (float)bleedOnHitItem;
        //    #endregion

        //    #region Fix Health/Shield difference
        //    if (NetworkServer.active)
        //    {
        //        float healthDifference = self.maxHealth - maxHealth;
        //        float shieldDifference = self.maxShield - maxShield;
        //        if (healthDifference > 0f)
        //        {
        //            self.healthComponent.Heal(healthDifference, default(ProcChainMask), false);
        //        }
        //        else if (self.healthComponent.health > self.maxHealth)
        //        {
        //            self.healthComponent.Networkhealth = Mathf.Max(self.healthComponent.health + healthDifference, self.maxHealth);
        //        }
        //        // -------- Panthera Modif: No Shield -------- //
        //        if (isPanthera == false)
        //        {
        //            if (shieldDifference > 0f)
        //            {
        //                self.healthComponent.RechargeShield(shieldDifference);
        //            }
        //            else if (self.healthComponent.shield > self.maxShield)
        //            {
        //                self.healthComponent.Networkshield = Mathf.Max(self.healthComponent.shield + shieldDifference, self.maxShield);
        //            }
        //        }
        //        else
        //        {
        //            self.healthComponent.Networkshield = 0;
        //        }
        //        // ------------------------------------------ //
        //    }
        //    #endregion

        //    #region Vision
        //    self.visionDistance = self.baseVisionDistance;
        //    if (blindedBuff == true)
        //    {
        //        self.visionDistance = Mathf.Min(self.visionDistance, 15f);
        //    }
        //    #endregion

        //    // ----------------------------------------------------------------------------------------------------- //

        //    // Calculate the Level Changes //
        //    if (self.level != characterlevel)
        //    {
        //        self.OnCalculatedLevelChanged(characterlevel, self.level);
        //    }

        //    // Update Effects //
        //    self.UpdateAllTemporaryVisualEffects();

        //    // Set Stats as calculated //
        //    self.statsDirty = false;


        //}

    }
}
