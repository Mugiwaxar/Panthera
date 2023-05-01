using Panthera;
using Panthera.Ability.Destruction;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.GUI;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Base
{

    public class Preset
    {

        public static Preset SelectedPreset;
        public static Preset ActivePreset;

        public ConfigPanel configPanel;
        public int presetID;
        public PantheraObj ptraObj;

        #region Dictionaries
        // (Ability ID 1 - ??, int unlockLevel) Represent a list of all unlocked Ability with level)
        public Dictionary<int, int> unlockedAbilitiesList = new Dictionary<int, int>();
        // (Skill ID 1 - ??, PantheraSkill Object) Represent a list of all unlocked skills)
        public Dictionary<int, PantheraSkill> unlockedSkillsList = new Dictionary<int, PantheraSkill>();
        // (Slot ID 1 - 20, int skillID) Represent a list of all Skill Bar slot linked with the Skill ID inserted //
        public Dictionary<int, int> slotsSkillsLinkList = new Dictionary<int, int>();
        #endregion

        #region Stats
        public float maxHealth
        {
            get
            {
                float maxHealth = PantheraConfig.Default_MaxHealth;
                if (getAbilityLevel(PantheraConfig.GuardianAbilityID) > 0)
                    maxHealth += PantheraConfig.Guardian_addedHealth;
                return maxHealth;
            }
        }
        public float maxHealthLevel
        {
            get
            {
                float maxHealthLevel = PantheraConfig.Default_MaxHealthLevel;
                if (getAbilityLevel(PantheraConfig.GuardianAbilityID) > 0)
                    maxHealthLevel += PantheraConfig.Guardian_addedHealthLevel;
                return maxHealthLevel;
            }
        }
        public float healthRegen
        {
            get
            {
                float healthRegen = PantheraConfig.Default_HealthRegen;
                if (getAbilityLevel(PantheraConfig.GuardianAbilityID) > 0)
                    healthRegen += PantheraConfig.Guardian_addedHealthRegen;
                return healthRegen;
            }
        }
        public float healthRegenLevel
        {
            get
            {
                float healthRegenLevel = PantheraConfig.Default_HealthRegenLevel;
                if (getAbilityLevel(PantheraConfig.GuardianAbilityID) > 0)
                    healthRegenLevel += PantheraConfig.Guardian_addedHealthRegenLevel;
                return healthRegenLevel;
            }
        }
        public float maxEnergy
        {
            get
            {
                return PantheraConfig.Default_Energy;
            }
        }
        public float energyRegen
        {
            get
            {
                return PantheraConfig.Default_EnergyRegen;
            }
        }
        public float maxStamina
        {
            get
            {
                return PantheraConfig.Default_DefaultStamina;
            }
        }
        public float staminaRegen
        {
            get
            {
                return PantheraConfig.Default_StaminaRegen;
            }
        }
        public float maxFury
        {
            get
            {
                return PantheraConfig.Default_MaxFury;
            }
        }
        public float maxPower
        {
            get
            {
                return PantheraConfig.Default_MaxPower;
            }
        }
        public float maxComboPoint
        {
            get
            {
                return PantheraConfig.Default_MaxComboPoint;
            }
        }
        public float _serverMaxShield;
        public float maxShield
        {
            get
            {
                if (ptraObj != null && ptraObj.characterBody != null && ptraObj.characterBody.maxHealth > 0)
                    return ptraObj.characterBody.maxHealth * frontShield_maxShieldHealthPercent;

                return PantheraConfig.Default_MaxShield;
            }
        }
        public float moveSpeed
        {
            get
            {
                return PantheraConfig.Default_MoveSpeed;
            }
        }
        public float moveSpeedLevel
        {
            get
            {
                return PantheraConfig.Default_MoveSpeedLevel;
            }
        }
        public float damage
        {
            get
            {
                float damage = PantheraConfig.Default_Damage;
                if (getAbilityLevel(PantheraConfig.DestructionAbilityID) > 0)
                    damage += PantheraConfig.Destruction_addedDamage;
                return damage;
            }
        }
        public float damageLevel
        {
            get
            {
                float damageLevel = PantheraConfig.Default_DamageLevel;
                if (getAbilityLevel(PantheraConfig.DestructionAbilityID) > 0)
                    damageLevel += PantheraConfig.Destruction_addedDamageLevel;
                return damageLevel;
            }
        }
        public float attackSpeed
        {
            get
            {
                float attackSpeed = PantheraConfig.Default_AttackSpeed;
                if (getAbilityLevel(PantheraConfig.DestructionAbilityID) > 0)
                    attackSpeed += PantheraConfig.Destruction_addedAttackSpeed;
                return attackSpeed;
            }
        }
        public float attackSpeedLevel
        {
            get
            {
                float attackSpeedLevel = PantheraConfig.Default_AttackSpeedLevel;
                if (getAbilityLevel(PantheraConfig.DestructionAbilityID) > 0)
                    attackSpeedLevel += PantheraConfig.Destruction_addedAttackSpeedLevel;
                return attackSpeedLevel;
            }
        }
        public float critic
        {
            get
            {
                float crit = PantheraConfig.Default_Critic;
                if (getAbilityLevel(PantheraConfig.RuseAbilityID) > 0)
                    crit += PantheraConfig.Ruse_addedCrit;
                return crit;
            }
        }
        public float criticLevel
        {
            get
            {
                float critLevel = PantheraConfig.Default_CriticLevel;
                if (getAbilityLevel(PantheraConfig.RuseAbilityID) > 0)
                    critLevel += PantheraConfig.Ruse_addedCritLevel;
                return critLevel;
            }
        }
        public float defense
        {
            get
            {
                float defense = PantheraConfig.Default_Defense;
                if (getAbilityLevel(PantheraConfig.GuardianAbilityID) > 0)
                    defense += PantheraConfig.Guardian_addedDefense;
                return defense;
            }
        }
        public float defenseLevel
        {
            get
            {
                float defenseLevel = PantheraConfig.Default_DefenseLevel;
                if (getAbilityLevel(PantheraConfig.GuardianAbilityID) > 0)
                    defenseLevel += PantheraConfig.Guardian_addedDefenseLevel;
                return defenseLevel;
            }
        }
        public float jumpCount
        {
            get
            {
                if (getAbilityLevel(PantheraConfig.WindWalkerAbilityID) > 0)
                    return PantheraConfig.Default_jumpCount + 1;
                return PantheraConfig.Default_jumpCount;
            }
        }
        public float barrierDecayRateMultiplier
        {
            get
            {
                float rateMultiplier = 1;
                if (this.getAbilityLevel(PantheraConfig.ResidualEnergyAbilityID) > 0)
                    rateMultiplier = PantheraConfig.ResidualEnergy_decay;
                return rateMultiplier;
            }
        }
        #endregion

        #region Skills
        public float clawsStorm_damageMultiplier
        {
            get
            {
                int level = getAbilityLevel(PantheraConfig.ImprovedClawsStormAbilityID);
                if (level == 1)
                    return PantheraConfig.ImprovedClawsStorm_DamageIncrease1;
                else if (level == 2)
                    return PantheraConfig.ImprovedClawsStorm_DamageIncrease2;
                else if (level == 3)
                    return PantheraConfig.ImprovedClawsStorm_DamageIncrease3;
                else if (level == 4)
                    return PantheraConfig.ImprovedClawsStorm_DamageIncrease4;
                else if (level == 5)
                    return PantheraConfig.ImprovedClawsStorm_DamageIncrease5;
                return PantheraConfig.ClawsStorm_damageMultiplier;
            }
        }
        public float clawsStorm_firedDelay
        {
            get
            {
                int level = getAbilityLevel(PantheraConfig.TornadoAbilityID);
                if (level == 1)
                    return PantheraConfig.Tornado_speed1;
                else if (level == 2)
                    return PantheraConfig.Tornado_speed2;
                else if (level == 3)
                    return PantheraConfig.Tornado_speed3;
                return PantheraConfig.ClawsStorm_firedDelay;
            }
        }
        public float clawsStorm_healMultiplier
        {
            get
            {
                float healMult = 0;
                int level = getAbilityLevel(PantheraConfig.HealingStormAbilityID);
                if (level == 1)
                    healMult = PantheraConfig.HealingStrom_percent1;
                if (level == 2)
                    healMult = PantheraConfig.HealingStrom_percent2;
                if (level == 3)
                    healMult = PantheraConfig.HealingStrom_percent3;
                if (level == 4)
                    healMult = PantheraConfig.HealingStrom_percent4;
                if (level == 5)
                    healMult = PantheraConfig.HealingStrom_percent5;
                return healMult;
            }
        }
        public float furiousBite_atkDamageMultiplier
        {
            get
            {
                int level = getAbilityLevel(PantheraConfig.SharpenedFrangsAbilityID);
                if (level == 1)
                    return PantheraConfig.SharpenedFangs_baseDamageMultiplier1;
                else if (level == 2)
                    return PantheraConfig.SharpenedFangs_baseDamageMultiplier2;
                else if (level == 3)
                    return PantheraConfig.SharpenedFangs_baseDamageMultiplier3;
                else if (level == 4)
                    return PantheraConfig.SharpenedFangs_baseDamageMultiplier4;
                else if (level == 5)
                    return PantheraConfig.SharpenedFangs_baseDamageMultiplier5;

                return PantheraConfig.FuriousBite_atkDamageMultiplier;
            }
        }
        public float furiousBite_ComboPointMultiplier
        {
            get
            {
                int level = getAbilityLevel(PantheraConfig.SharpenedFrangsAbilityID);
                if (level == 1)
                    return PantheraConfig.SharpenedFangs_comboDamageMultiplier1;
                else if (level == 2)
                    return PantheraConfig.SharpenedFangs_comboDamageMultiplier2;
                else if (level == 3)
                    return PantheraConfig.SharpenedFangs_comboDamageMultiplier3;
                else if (level == 4)
                    return PantheraConfig.SharpenedFangs_comboDamageMultiplier4;
                else if (level == 5)
                    return PantheraConfig.SharpenedFangs_comboDamageMultiplier5;
                return PantheraConfig.FuriousBite_ComboPointMultiplier;
            }
        }
        public float frontShield_rechargeDelayAfterDamage
        {
            get
            {
                int level = getAbilityLevel(PantheraConfig.DefensiveHasteAbilityID);
                if (level == 1)
                    return PantheraConfig.DefensiveHaste_damageDelay1;
                else if (level == 2)
                    return PantheraConfig.DefensiveHaste_damageDelay2;
                else if (level == 3)
                    return PantheraConfig.DefensiveHaste_damageDelay3;
                else if (level == 4)
                    return PantheraConfig.DefensiveHaste_damageDelay4;
                return PantheraConfig.FrontShield_rechargeDelayAfterDamage;
            }
        }
        public float frontShield_rechargeDelayAfterDestroyed
        {
            get
            {
                int level = getAbilityLevel(PantheraConfig.DefensiveHasteAbilityID);
                if (level == 1)
                    return PantheraConfig.DefensiveHaste_destroyedDelay1;
                else if (level == 2)
                    return PantheraConfig.DefensiveHaste_destroyedDelay2;
                else if (level == 3)
                    return PantheraConfig.DefensiveHaste_destroyedDelay3;
                else if (level == 4)
                    return PantheraConfig.DefensiveHaste_destroyedDelay4;
                return PantheraConfig.FrontShield_rechargeDelayAfterDestroyed;
            }
        }
        public float frontShield_maxShieldHealthPercent
        {
            get
            {
                int level = getAbilityLevel(PantheraConfig.ShieldFocusAbilityID);
                if (level == 1)
                    return PantheraConfig.ShieldFocus_healthPercent1;
                else if (level == 2)
                    return PantheraConfig.ShieldFocus_healthPercent2;
                else if (level == 3)
                    return PantheraConfig.ShieldFocus_healthPercent3;
                else if (level == 4)
                    return PantheraConfig.ShieldFocus_healthPercent4;
                else if (level == 5)
                    return PantheraConfig.ShieldFocus_healthPercent5;
                else if (level == 6)
                    return PantheraConfig.ShieldFocus_healthPercent6;
                else if (level == 7)
                    return PantheraConfig.ShieldFocus_healthPercent7;
                else if (level == 8)
                    return PantheraConfig.ShieldFocus_healthPercent8;
                else if (level == 9)
                    return PantheraConfig.ShieldFocus_healthPercent9;
                return PantheraConfig.FrontShield_maxShieldHealthPercent;
            }
        }
        public float frontShield_damageDecreaseMultiplier
        {
            get
            {
                int level = getAbilityLevel(PantheraConfig.ShieldFocusAbilityID);
                if (level == 1)
                    return PantheraConfig.ShieldFocus_damagePercent1;
                else if (level == 2)
                    return PantheraConfig.ShieldFocus_damagePercent2;
                else if (level == 3)
                    return PantheraConfig.ShieldFocus_damagePercent3;
                else if (level == 4)
                    return PantheraConfig.ShieldFocus_damagePercent4;
                else if (level == 5)
                    return PantheraConfig.ShieldFocus_damagePercent5;
                else if (level == 6)
                    return PantheraConfig.ShieldFocus_damagePercent6;
                else if (level == 7)
                    return PantheraConfig.ShieldFocus_damagePercent7;
                else if (level == 8)
                    return PantheraConfig.ShieldFocus_damagePercent8;
                else if (level == 9)
                    return PantheraConfig.ShieldFocus_damagePercent9;
                return PantheraConfig.FrontShield_damageDecreaseMultiplier;
            }
        }
        public float dash_speedMultiplier
        {
            get
            {
                int level = getAbilityLevel(PantheraConfig.DashAbilityID);
                if (level == 1)
                    return PantheraConfig.Dash_speedMultiplier1;
                else if (level == 2)
                    return PantheraConfig.Dash_speedMultiplier2;
                else if (level == 3)
                    return PantheraConfig.Dash_speedMultiplier3;
                else if (level == 4)
                    return PantheraConfig.Dash_speedMultiplier4;
                else if (level == 5)
                    return PantheraConfig.Dash_speedMultiplier5;
                return 1;
            }
        }
        public float dash_staminaConsumed
        {
            get
            {
                int level = getAbilityLevel(PantheraConfig.DashAbilityID);
                if (level == 1)
                    return PantheraConfig.Dash_staminaConsumed1;
                else if (level == 2)
                    return PantheraConfig.Dash_staminaConsumed2;
                else if (level == 3)
                    return PantheraConfig.Dash_staminaConsumed3;
                else if (level == 4)
                    return PantheraConfig.Dash_staminaConsumed4;
                else if (level == 5)
                    return PantheraConfig.Dash_staminaConsumed5;
                return PantheraConfig.Dash_staminaConsumed1;
            }
        }
        public float shieldBash_damage
        {
            get
            {
                int level = getAbilityLevel(PantheraConfig.ShieldBashAbilityID);
                if (level == 1)
                    return PantheraConfig.ShieldBash_damage1;
                else if (level == 2)
                    return PantheraConfig.ShieldBash_damage2;
                else if (level == 3)
                    return PantheraConfig.ShieldBash_damage3;
                return PantheraConfig.ShieldBash_damage1;
            }
        }
        public float shieldBash_stunDuration
        {
            get
            {
                int level = getAbilityLevel(PantheraConfig.ShieldBashAbilityID);
                if (level == 1)
                    return PantheraConfig.ShieldBash_stunDuration1;
                else if (level == 2)
                    return PantheraConfig.ShieldBash_stunDuration2;
                else if (level == 3)
                    return PantheraConfig.ShieldBash_stunDuration3;
                return PantheraConfig.ShieldBash_stunDuration1;
            }
        }
        public float zoneHeal_healDuration
        {
            get
            {
                int level = getAbilityLevel(PantheraConfig.ZoneHealAbilityID);
                if (level == 1)
                    return PantheraConfig.ZoneHeal_healDuration;
                else if (level == 2)
                    return PantheraConfig.ZoneHeal_healDuration;
                else if (level == 3)
                    return PantheraConfig.ZoneHeal_healDuration;
                return PantheraConfig.ZoneHeal_healDuration;
            }
        }
        public float zoneHeal_percentHeal
        {
            get
            {
                int level = getAbilityLevel(PantheraConfig.ZoneHealAbilityID);
                if (level == 1)
                    return PantheraConfig.ZoneHeal_percentHeal1;
                else if (level == 2)
                    return PantheraConfig.ZoneHeal_percentHeal2;
                else if (level == 3)
                    return PantheraConfig.ZoneHeal_percentHeal3;
                return PantheraConfig.ZoneHeal_percentHeal1;
            }
        }
        public int theRipper_maxStack
        {
            get
            {
                int maxStack = PantheraConfig.TheRipper_maxStack;
                int level = getAbilityLevel(PantheraConfig.GodOfReapersAbilityID);
                if (level == 1)
                    return PantheraConfig.GodOfReapers_maxBuff1;
                else if (level == 2)
                    return PantheraConfig.GodOfReapers_maxBuff2;
                else if (level == 3)
                    return PantheraConfig.GodOfReapers_maxBuff3;
                else if (level == 4)
                    return PantheraConfig.GodOfReapers_maxBuff4;
                else if (level == 5)
                    return PantheraConfig.GodOfReapers_maxBuff5;
                return maxStack;
            }
        }
        public float mightyRoar_radius
        {
            get
            {
                int level = getAbilityLevel(PantheraConfig.EchoAbilityID);
                if (level == 1)
                    return PantheraConfig.Echo_radius1;
                else if (level == 2)
                    return PantheraConfig.Echo_radius2;
                else if (level == 3)
                    return PantheraConfig.Echo_radius3;
                return PantheraConfig.MightyRoar_radius;
            }
        }
        public float mightyRoar_stunDuration
        {
            get
            {
                int level = getAbilityLevel(PantheraConfig.EchoAbilityID);
                if (level == 1)
                    return PantheraConfig.Echo_stunDuration1;
                else if (level == 2)
                    return PantheraConfig.Echo_stunDuration2;
                else if (level == 3)
                    return PantheraConfig.Echo_stunDuration3;
                return PantheraConfig.MightyRoar_stunDuration;
            }
        }
        public float mightyRoar_bleedDuration
        {
            get
            {
                int level = getAbilityLevel(PantheraConfig.PiercingWavesAbilityID);
                if (level == 1)
                    return PantheraConfig.PiercingWaves_bleedDuration1;
                else if (level == 2)
                    return PantheraConfig.PiercingWaves_bleedDuration2;
                else if (level == 3)
                    return PantheraConfig.PiercingWaves_bleedDuration3;
                else if (level == 4)
                    return PantheraConfig.PiercingWaves_bleedDuration4;
                else if (level == 5)
                    return PantheraConfig.PiercingWaves_bleedDuration5;
                return 0;
            }
        }
        public float mightyRoar_bleedDamage
        {
            get
            {
                int level = getAbilityLevel(PantheraConfig.PiercingWavesAbilityID);
                if (level == 1)
                    return PantheraConfig.PiercingWaves_bleedDamage1;
                else if (level == 2)
                    return PantheraConfig.PiercingWaves_bleedDamage2;
                else if (level == 3)
                    return PantheraConfig.PiercingWaves_bleedDamage3;
                else if (level == 4)
                    return PantheraConfig.PiercingWaves_bleedDamage4;
                else if (level == 5)
                    return PantheraConfig.PiercingWaves_bleedDamage5;
                return 0;
            }
        }
        public float prowl_unstealthDelay
        {
            get
            {
                int level = getAbilityLevel(PantheraConfig.PrimalStalkerAbilityID);
                if (level == 1)
                    return PantheraConfig.PrimalStalker_fixedTime1;
                else if (level == 2)
                    return PantheraConfig.PrimalStalker_fixedTime2;
                else if (level == 3)
                    return PantheraConfig.PrimalStalker_fixedTime3;
                return 0;
            }
        }
        public float circularSaw_damage
        {
            get
            {
                int level = getAbilityLevel(PantheraConfig.CircularSawAbilityID);
                float damage = PantheraConfig.Slash_damageMultiplier;
                if (level == 1)
                    damage = PantheraConfig.CircualSaw_damage1;
                else if (level == 2)
                    damage = PantheraConfig.CircualSaw_damage2;
                else if (level == 3)
                    damage = PantheraConfig.CircualSaw_damage3;
                return damage;
            }
        }
        public float ignition_damage
        {
            get
            {
                int level = getAbilityLevel(PantheraConfig.IgnitionAbilityID);
                float damage = PantheraConfig.Ignition_burnDamage1;
                if (level == 1)
                    damage = PantheraConfig.Ignition_burnDamage1;
                else if (level == 2)
                    damage = PantheraConfig.Ignition_burnDamage2;
                else if (level == 3)
                    damage = PantheraConfig.Ignition_burnDamage3;
                else if (level == 4)
                    damage = PantheraConfig.Ignition_burnDamage4;
                else if (level == 5)
                    damage = PantheraConfig.Ignition_burnDamage5;
                return damage;
            }
        }
        public float ignition_duration
        {
            get
            {
                int level = getAbilityLevel(PantheraConfig.IgnitionAbilityID);
                float duration = PantheraConfig.Ignition_burnDuration1;
                if (level == 1)
                    duration = PantheraConfig.Ignition_burnDuration1;
                else if (level == 2)
                    duration = PantheraConfig.Ignition_burnDuration2;
                else if (level == 3)
                    duration = PantheraConfig.Ignition_burnDuration3;
                else if (level == 4)
                    duration = PantheraConfig.Ignition_burnDuration4;
                else if (level == 5)
                    duration = PantheraConfig.Ignition_burnDuration5;
                return duration;
            }
        }
        public float ignition_radius
        {
            get
            {
                int level = getAbilityLevel(PantheraConfig.HighTemperatureAbilityID);
                float radius = PantheraConfig.Ignition_radius;
                if (level == 1)
                    radius = PantheraConfig.HighTemperature_radius1;
                else if (level == 2)
                    radius = PantheraConfig.HighTemperature_radius2;
                else if (level == 3)
                    radius = PantheraConfig.HighTemperature_radius3;
                else if (level == 4)
                    radius = PantheraConfig.HighTemperature_radius4;
                return radius;
            }
        }
        public float sacredFlames_healMultiplier
        {
            get
            {
                int level = getAbilityLevel(PantheraConfig.SacredFlamesAbilityID);
                float amount = 0;
                if (level == 1)
                    amount = PantheraConfig.SacredFlames_percent1;
                else if (level == 2)
                    amount = PantheraConfig.SacredFlames_percent2;
                else if (level == 3)
                    amount = PantheraConfig.SacredFlames_percent3;
                return amount;
            }
        }
        public float precision_critic
        {
            get
            {
                int level = getAbilityLevel(PantheraConfig.PrecisionAbilityID);
                float amount = 0;
                if (level == 1)
                    amount = PantheraConfig.Precision_percent1;
                else if (level == 2)
                    amount = PantheraConfig.Precision_percent2;
                else if (level == 3)
                    amount = PantheraConfig.Precision_percent3;
                return amount;
            }
        }
        public float detection_staminaConsumed
        {
            get
            {
                int level = getAbilityLevel(PantheraConfig.DeterminationAbilityID);
                float amount = PantheraConfig.Detection_staminaConsumed;
                if (level == 1)
                    amount = PantheraConfig.Determination_comsumed1;
                else if (level == 2)
                    amount = PantheraConfig.Determination_comsumed2;
                else if (level == 3)
                    amount = PantheraConfig.Determination_comsumed3;
                return amount;
            }
        }
        public float regeneration_percentHeal
        {
            get
            {
                int level = getAbilityLevel(PantheraConfig.RegenerationAbilityID);
                float amount = 0;
                if (level == 1)
                    amount = PantheraConfig.Regeneration_percent1;
                else if (level == 2)
                    amount = PantheraConfig.Regeneration_percent2;
                else if (level == 3)
                    amount = PantheraConfig.Regeneration_percent3;
                return amount;
            }
        }
        public float instinctiveResistance_resistanceAmount
        {
            get
            {
                int level = getAbilityLevel(PantheraConfig.InstinctiveResistanceAbilityID);
                float resistance = 0;
                if (level == 1)
                    resistance = PantheraConfig.InstinctiveResistance_percent1;
                else if (level == 2)
                    resistance = PantheraConfig.InstinctiveResistance_percent2;
                else if (level == 3)
                    resistance = PantheraConfig.InstinctiveResistance_percent3;
                else if (level == 4)
                    resistance = PantheraConfig.InstinctiveResistance_percent4;
                else if (level == 5)
                    resistance = PantheraConfig.InstinctiveResistance_percent5;
                else if (level == 6)
                    resistance = PantheraConfig.InstinctiveResistance_percent6;
                return resistance;
            }
        }
        public float strongBarrier_maxPercent
        {
            get
            {
                int level = getAbilityLevel(PantheraConfig.StrongBarrierAbilityID);
                float reductionPercent = 1;
                if (level == 1)
                    reductionPercent = PantheraConfig.StrongBarrier_percent1;
                else if (level == 2)
                    reductionPercent = PantheraConfig.StrongBarrier_percent2;
                else if (level == 3)
                    reductionPercent = PantheraConfig.StrongBarrier_percent3;
                return reductionPercent;
            }
        }
        #endregion

        public Preset()
        {

        }

        public Preset(ConfigPanel configPanel, int presetID, Dictionary<string, string> data = null)
        {
            SelectedPreset = this;
            this.configPanel = configPanel;
            this.presetID = presetID;

            if (data == null)
            {

                // Add default Skills inside the Skill Bar //
                slotsSkillsLinkList.Add(1, PantheraConfig.Rip_SkillID);
                slotsSkillsLinkList.Add(2, PantheraConfig.AirCleave_SkillID);

                // Set all defauld Key Binds //
                if (this.configPanel.firstStart == true)
                    KeysBinder.SetAllDefaultKeyBinds();

            }
            else
            {
                // Read the Data //
                readData(data);
            }

            // Build the Skills Slots List //
            buildSkillList();

            // Save //
            PantheraSaveSystem.SavePreset(this.presetID, saveData());

        }

        public void readData(Dictionary<string, string> dataList)
        {
            // Read all the Data //
            foreach (KeyValuePair<string, string> entry in dataList)
            {
                // Check if this is an Ability //
                if (entry.Key.Contains("Ability"))
                {
                    // Get the ID //
                    string abilityIDString = entry.Key.Replace("Ability", "");
                    int abilityID = int.Parse(abilityIDString);
                    // Get the Level //
                    int level = int.Parse(entry.Value);
                    // Register to the List //
                    unlockedAbilitiesList.Add(abilityID, level);
                }
                // Check if this is a Slot //
                if (entry.Key.Contains("Slot"))
                {
                    // Get the ID //
                    string slotIDString = entry.Key.Replace("Slot", "");
                    int slotID = int.Parse(slotIDString);
                    // Get the Skill //
                    int skillID = int.Parse(entry.Value);
                    // Register the Skill inside the Slot //
                    slotsSkillsLinkList.Add(slotID, skillID);
                }
            }

        }

        public Dictionary<string, string> saveData()
        {
            // Create the Dictionary //
            Dictionary<string, string> data = new Dictionary<string, string>();

            // Add the Abilities //
            foreach (KeyValuePair<int, int> entry in unlockedAbilitiesList)
            {
                data.Add("Ability" + entry.Key, entry.Value.ToString());
            }

            // Add the Skill Bars //
            foreach (KeyValuePair<int, int> entry in slotsSkillsLinkList)
            {
                if (entry.Value != null)
                    data.Add("Slot" + entry.Key, entry.Value.ToString());
            }

            // Return the Data //
            return data;


        }

        public void buildSkillList()
        {

            // Clear the Skill List //
            unlockedSkillsList.Clear();

            // Add all defauld Skills //
            unlockedSkillsList.Add(PantheraConfig.Rip_SkillID, PantheraSkill.GetSkillDef(PantheraConfig.Rip_SkillID).clone());
            unlockedSkillsList.Add(PantheraConfig.AirCleave_SkillID, PantheraSkill.GetSkillDef(PantheraConfig.AirCleave_SkillID).clone());

            // Add ClawStorm //
            if (getAbilityLevel(PantheraConfig.DestructionAbilityID) > 0)
                unlockedSkillsList.Add(PantheraConfig.ClawsStorm_SkillID, PantheraSkill.GetSkillDef(PantheraConfig.ClawsStorm_SkillID).clone());

            // Add Front Shield //
            if (getAbilityLevel(PantheraConfig.GuardianAbilityID) > 0)
                unlockedSkillsList.Add(PantheraConfig.FrontShield_SkillID, PantheraSkill.GetSkillDef(PantheraConfig.FrontShield_SkillID).clone());

            // Add Prowl //
            if (getAbilityLevel(PantheraConfig.ProwlAbilityID) > 0)
                unlockedSkillsList.Add(PantheraConfig.Prowl_SkillID, PantheraSkill.GetSkillDef(PantheraConfig.Prowl_SkillID).clone());

            // Add Furious Bite //
            if (getAbilityLevel(PantheraConfig.RuseAbilityID) > 0)
                unlockedSkillsList.Add(PantheraConfig.FuriousBite_SkillID, PantheraSkill.GetSkillDef(PantheraConfig.FuriousBite_SkillID).clone());

            // Add MightyRoar //
            if (getAbilityLevel(PantheraConfig.MightyRoarAbilityID) > 0)
                unlockedSkillsList.Add(PantheraConfig.MightyRoar_SkillID, PantheraSkill.GetSkillDef(PantheraConfig.MightyRoar_SkillID).clone());

            // Add Leap //
            if (getAbilityLevel(PantheraConfig.LeapAbilityID) > 0)
                unlockedSkillsList.Add(PantheraConfig.Leap_SkillID, PantheraSkill.GetSkillDef(PantheraConfig.Leap_SkillID).clone());

            // Add Dash //
            if (getAbilityLevel(PantheraConfig.DashAbilityID) > 0)
                unlockedSkillsList.Add(PantheraConfig.Dash_SkillID, PantheraSkill.GetSkillDef(PantheraConfig.Dash_SkillID).clone());

            // Add Shield Bash //
            if (getAbilityLevel(PantheraConfig.ShieldBashAbilityID) > 0)
                unlockedSkillsList.Add(PantheraConfig.ShieldBash_SkillID, PantheraSkill.GetSkillDef(PantheraConfig.ShieldBash_SkillID).clone());

            // Add Zone Heal //
            if (getAbilityLevel(PantheraConfig.ZoneHealAbilityID) > 0)
                unlockedSkillsList.Add(PantheraConfig.ZoneHeal_SkillID, PantheraSkill.GetSkillDef(PantheraConfig.ZoneHeal_SkillID).clone());

            // Add Wind Walker //
            if (getAbilityLevel(PantheraConfig.WindWalkerAbilityID) > 0)
                unlockedSkillsList.Add(PantheraConfig.WindWalker_SkillID, PantheraSkill.GetSkillDef(PantheraConfig.WindWalker_SkillID).clone());

            // Add The Ripper //
            if (getAbilityLevel(PantheraConfig.TheRipperAbilityID) > 0)
                unlockedSkillsList.Add(PantheraConfig.TheRipper_SkillID, PantheraSkill.GetSkillDef(PantheraConfig.TheRipper_SkillID).clone());

            // Add Save My Friend //
            if (getAbilityLevel(PantheraConfig.SaveMyFriendAbilityID) > 0)
                unlockedSkillsList.Add(PantheraConfig.SaveMyFriend_SkillID, PantheraSkill.GetSkillDef(PantheraConfig.SaveMyFriend_SkillID).clone());

            // Add Shield of Power //
            if (getAbilityLevel(PantheraConfig.ShieldOfPowerAbilityID) > 0)
                unlockedSkillsList.Add(PantheraConfig.ShieldOfPower_SkillID, PantheraSkill.GetSkillDef(PantheraConfig.ShieldOfPower_SkillID).clone());

            // Add Burning Spirit //
            if (getAbilityLevel(PantheraConfig.BurningSpiritAbilityID) > 0)
                unlockedSkillsList.Add(PantheraConfig.BurningSpirit_SkillID, PantheraSkill.GetSkillDef(PantheraConfig.BurningSpirit_SkillID).clone());

            // Add Fire Slash //
            if (getAbilityLevel(PantheraConfig.SlashAbilityID) > 0)
                unlockedSkillsList.Add(PantheraConfig.Slash_SkillID, PantheraSkill.GetSkillDef(PantheraConfig.Slash_SkillID).clone());

            // Add Fire Bird //
            if (getAbilityLevel(PantheraConfig.FireBirdAbilityID) > 0)
                unlockedSkillsList.Add(PantheraConfig.FireBird_SkillID, PantheraSkill.GetSkillDef(PantheraConfig.FireBird_SkillID).clone());

            // Add Regeneration //
            if (getAbilityLevel(PantheraConfig.RegenerationAbilityID) > 0)
                unlockedSkillsList.Add(PantheraConfig.Regeneration_SkillID, PantheraSkill.GetSkillDef(PantheraConfig.Regeneration_SkillID).clone());

            // Add Passive Power //
            if (getAbilityLevel(PantheraConfig.PassivePowerAbilityID) > 0)
                unlockedSkillsList.Add(PantheraConfig.PassivePower_SkillID, PantheraSkill.GetSkillDef(PantheraConfig.PassivePower_SkillID).clone());

            // Add Revive //
            if (getAbilityLevel(PantheraConfig.ReviveAbilityID) > 0)
                unlockedSkillsList.Add(PantheraConfig.Revive_SkillID, PantheraSkill.GetSkillDef(PantheraConfig.Revive_SkillID).clone());

            // Add Detection //
            if (getAbilityLevel(PantheraConfig.DetectionAbilityID) > 0)
                unlockedSkillsList.Add(PantheraConfig.Detection_SkillID, PantheraSkill.GetSkillDef(PantheraConfig.Detection_SkillID).clone());

            // Apply all Abilities //
            applyAbilities();
        }

        public void applyAbilities()
        {

            // Apply the Leap //
            PantheraSkill leapSkill = this.getSkillByID(PantheraConfig.Leap_SkillID);
            if (leapSkill != null)
            {
                int leapLevel = getAbilityLevel(PantheraConfig.LeapAbilityID);
                if (leapLevel == 0)
                    leapSkill.cooldown = PantheraConfig.Leap_cooldown1;
                else if (leapLevel == 1)
                    leapSkill.cooldown = PantheraConfig.Leap_cooldown1;
                else if (leapLevel == 2)
                    leapSkill.cooldown = PantheraConfig.Leap_cooldown2;
                else if (leapLevel == 3)
                    leapSkill.cooldown = PantheraConfig.Leap_cooldown3;
            }

            // Apply the Improved Claws Stowm //
            PantheraSkill clawsStormSkill = this.getSkillByID(PantheraConfig.ClawsStorm_SkillID);
            if (clawsStormSkill != null)
            {
                int improvedClawsStormLevel = getAbilityLevel(PantheraConfig.ImprovedClawsStormAbilityID);
                if (improvedClawsStormLevel == 0)
                    clawsStormSkill.cooldown = PantheraConfig.ClawsStorm_cooldown;
                if (improvedClawsStormLevel == 1)
                    clawsStormSkill.cooldown = PantheraConfig.ImprovedClawsStorm_cooldown1;
                if (improvedClawsStormLevel == 2)
                    clawsStormSkill.cooldown = PantheraConfig.ImprovedClawsStorm_cooldown2;
                if (improvedClawsStormLevel == 3)
                    clawsStormSkill.cooldown = PantheraConfig.ImprovedClawsStorm_cooldown3;
                if (improvedClawsStormLevel == 4)
                    clawsStormSkill.cooldown = PantheraConfig.ImprovedClawsStorm_cooldown4;
                if (improvedClawsStormLevel == 5)
                    clawsStormSkill.cooldown = PantheraConfig.ImprovedClawsStorm_cooldown5;
            }

            // Apply the Sharpened Fangs //
            PantheraSkill furiousBiteSkill = this.getSkillByID(PantheraConfig.FuriousBite_SkillID);
            if (furiousBiteSkill != null)
            {
                int sharpenedFangsLevel = getAbilityLevel(PantheraConfig.SharpenedFrangsAbilityID);
                if (sharpenedFangsLevel == 0)
                    furiousBiteSkill.requiredEnergy = PantheraConfig.FuriousBite_energyRequired;
                if (sharpenedFangsLevel == 1)
                    furiousBiteSkill.requiredEnergy = PantheraConfig.SharpenedFangs_energyCost1;
                if (sharpenedFangsLevel == 2)
                    furiousBiteSkill.requiredEnergy = PantheraConfig.SharpenedFangs_energyCost2;
                if (sharpenedFangsLevel == 3)
                    furiousBiteSkill.requiredEnergy = PantheraConfig.SharpenedFangs_energyCost3;
                if (sharpenedFangsLevel == 4)
                    furiousBiteSkill.requiredEnergy = PantheraConfig.SharpenedFangs_energyCost4;
                if (sharpenedFangsLevel == 5)
                    furiousBiteSkill.requiredEnergy = PantheraConfig.SharpenedFangs_energyCost5;
            }

            // Apply the Shadow's Master //
            PantheraSkill prowlSkill = this.getSkillByID(PantheraConfig.Prowl_SkillID);
            if (prowlSkill != null)
            {
                int shadowsMasterLevel = getAbilityLevel(PantheraConfig.ShadowsMasterAbilityID);
                if (shadowsMasterLevel == 0)
                    prowlSkill.cooldown = PantheraConfig.Prowl_coolDown;
                else if (shadowsMasterLevel == 1)
                    prowlSkill.cooldown = PantheraConfig.ShadowsMaster_cooldown1;
                else if (shadowsMasterLevel == 2)
                    prowlSkill.cooldown = PantheraConfig.ShadowsMaster_cooldown2;
                else if (shadowsMasterLevel == 3)
                    prowlSkill.cooldown = PantheraConfig.ShadowsMaster_cooldown3;
            }

            // Apply Zone Heal //
            PantheraSkill zoneHealSkill = this.getSkillByID(PantheraConfig.ZoneHeal_SkillID);
            if (zoneHealSkill != null)
            {
                int zoneHealLevel = getAbilityLevel(PantheraConfig.ZoneHealAbilityID);
                if (zoneHealLevel == 0)
                    zoneHealSkill.cooldown = PantheraConfig.ZoneHeal_cooldown1;
                else if (zoneHealLevel == 1)
                    zoneHealSkill.cooldown = PantheraConfig.ZoneHeal_cooldown1;
                else if (zoneHealLevel == 2)
                    zoneHealSkill.cooldown = PantheraConfig.ZoneHeal_cooldown2;
                else if (zoneHealLevel == 3)
                    zoneHealSkill.cooldown = PantheraConfig.ZoneHeal_cooldown3;
            }

            // Apply Echo //
            PantheraSkill mightyRoarSkill = this.getSkillByID(PantheraConfig.MightyRoar_SkillID);
            if (mightyRoarSkill != null)
            {
                int echoLevel = getAbilityLevel(PantheraConfig.EchoAbilityID);
                if (echoLevel == 0)
                    mightyRoarSkill.cooldown = PantheraConfig.MightyRoar_cooldown;
                else if (echoLevel == 1)
                    mightyRoarSkill.cooldown = PantheraConfig.Echo_cooldown1;
                else if (echoLevel == 2)
                    mightyRoarSkill.cooldown = PantheraConfig.Echo_cooldown2;
                else if (echoLevel == 3)
                    mightyRoarSkill.cooldown = PantheraConfig.Echo_cooldown3;
            }

            // Apply the Circular Saw //
            PantheraSkill slashSkill = this.getSkillByID(PantheraConfig.Slash_SkillID);
            if (slashSkill != null)
            {
                int circularSawLevel = getAbilityLevel(PantheraConfig.CircularSawAbilityID);
                if (circularSawLevel == 0)
                    slashSkill.requiredEnergy = PantheraConfig.Slash_energyRequired;
                if (circularSawLevel == 1)
                    slashSkill.requiredEnergy = PantheraConfig.CircualSaw_energy1;
                if (circularSawLevel == 2)
                    slashSkill.requiredEnergy = PantheraConfig.CircualSaw_energy2;
                if (circularSawLevel == 3)
                    slashSkill.requiredEnergy = PantheraConfig.CircualSaw_energy3;
            }

        }

        public void addAbilityPoint(int abilityID)
        {
            if (unlockedAbilitiesList.ContainsKey(abilityID)) unlockedAbilitiesList[abilityID] += 1;
            else unlockedAbilitiesList.Add(abilityID, 1);
        }

        public int getAbilityLevel(int abilityID)
        {
            if (unlockedAbilitiesList.ContainsKey(abilityID)) return unlockedAbilitiesList[abilityID];
            else return 0;
        }

        public PantheraSkill getSkillByID(int skillID)
        {
            if (this.unlockedSkillsList.ContainsKey(skillID)) return this.unlockedSkillsList[skillID];
            return null;
        }

        public PantheraSkill getSkillBySlotID(int slotID)
        {
            int skillID = 0;
            if (this.slotsSkillsLinkList.ContainsKey(slotID))
                skillID = this.slotsSkillsLinkList[slotID];
            if (skillID != 0 && this.unlockedSkillsList.ContainsKey(skillID))
                return this.unlockedSkillsList[skillID];
            return null;
        }

        public void addSkillToSlot(int slotID, int skillID)
        {
            if (slotsSkillsLinkList.ContainsKey(slotID)) slotsSkillsLinkList.Remove(slotID);
            slotsSkillsLinkList.Add(slotID, skillID);
            configPanel.updateSkillBars();
            PantheraSaveSystem.SavePreset(presetID, saveData());
        }

        public void removeSkillFromSlot(int slotID)
        {
            if (slotsSkillsLinkList.ContainsKey(slotID)) slotsSkillsLinkList.Remove(slotID);
            configPanel.updateSkillBars();
            PantheraSaveSystem.SavePreset(presetID, saveData());
        }

        public PantheraSkill getPressedSkill(int actionID, bool switchBarPressed)
        {
            // Return if actionID == 0 //
            if (actionID == 0) return null;

            // Check if the Switch Bar button is Pressed //
            int j = 0;
            if (switchBarPressed == true) j = 10;

            // Check if the Action is not 0 //
            if (actionID == 0) return null;
            // Find the Skill //
            int skillID = 0;
            if (actionID == PantheraConfig.Skill1Key && slotsSkillsLinkList.ContainsKey(1 + j)) skillID = slotsSkillsLinkList[1 + j];
            if (actionID == PantheraConfig.Skill2Key && slotsSkillsLinkList.ContainsKey(2 + j)) skillID = slotsSkillsLinkList[2 + j];
            if (actionID == PantheraConfig.Skill3Key && slotsSkillsLinkList.ContainsKey(3 + j)) skillID = slotsSkillsLinkList[3 + j];
            if (actionID == PantheraConfig.Skill4Key && slotsSkillsLinkList.ContainsKey(4 + j)) skillID = slotsSkillsLinkList[4 + j];
            if (actionID == PantheraConfig.Skill5Key && slotsSkillsLinkList.ContainsKey(5 + j)) skillID = slotsSkillsLinkList[5 + j];
            if (actionID == PantheraConfig.Skill6Key && slotsSkillsLinkList.ContainsKey(6 + j)) skillID = slotsSkillsLinkList[6 + j];
            if (actionID == PantheraConfig.Skill7Key && slotsSkillsLinkList.ContainsKey(7 + j)) skillID = slotsSkillsLinkList[7 + j];
            if (actionID == PantheraConfig.Skill8Key && slotsSkillsLinkList.ContainsKey(8 + j)) skillID = slotsSkillsLinkList[8 + j];
            if (actionID == PantheraConfig.Skill9Key && slotsSkillsLinkList.ContainsKey(9 + j)) skillID = slotsSkillsLinkList[9 + j];
            if (actionID == PantheraConfig.Skill10Key && slotsSkillsLinkList.ContainsKey(10 + j)) skillID = slotsSkillsLinkList[10 + j];
            if (skillID == 0)
                return null;
            return this.ptraObj.activePreset.getSkillByID(skillID);
        }

    }
}
