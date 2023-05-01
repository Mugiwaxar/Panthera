using EntityStates.AffixEarthHealer;
using Panthera.Base;
using Panthera.Components;
using Panthera.Skills;
using R2API;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Utils
{
    class Tokens
    {

        //public static string passiveColor = "#216BFF";
        //public static string attackColor = "#B900DD";

        public static void RegisterTokens()
        {

            // ------------------------------------------ BASE --------------------------------- //

            #region Character
            string CharacterName = "P4N7H3R4";
            string CharacterSubtitle = "Play like a predator!";
            string CharacterDesc = Utils.ColorHelper.SetCyan("Don't be the prey, be the predator!") + Environment.NewLine + Environment.NewLine
                + "P4N7H3R4 (Panthera) is a powerful Panther who belongs to the Wild Gods family. Unlike humans, Big Cats are naturally equipped to hunt and kill, this makes P4N7H3R4 a very strong predator." + Environment.NewLine + Environment.NewLine
                + Utils.ColorHelper.SetUtility("No Shield: Every Item that add Shield add Health instead.") + Environment.NewLine + Environment.NewLine
                + Utils.ColorHelper.SetUtility("Cat: You take no fall damage.");
            PantheraTokens.Add("PANTHERA_NAME", CharacterName);
            PantheraTokens.Add("PANTHERA_DESC", CharacterDesc);
            PantheraTokens.Add("PANTHERA_SUBTITLE", CharacterSubtitle);
            #endregion

            #region Model
            string ModelName1 = "White";
            PantheraTokens.Add("PANTHERA_MODEL_NAME_1", ModelName1);
            string ModelName2 = "Orange";
            PantheraTokens.Add("PANTHERA_MODEL_NAME_2", ModelName2);
            string ModelName3 = "Primal Stalker";
            PantheraTokens.Add("PANTHERA_MODEL_NAME_3", ModelName3);
            #endregion

            #region Passive Skill
            string PassiveName = "Feline Skills";
            string PassiveDesc = "P4N7H3R4 has a Skills Trees that can be configured by pressing the Configuration Button.";
            LanguageAPI.Add("PANTHERA_PASSIVE_NAME", PassiveName);
            LanguageAPI.Add("PANTHERA_PASSIVE_DESCRIPTION", PassiveDesc);
            #endregion

            // ------------------------------------------ SKILLS --------------------------------- //

            // ---------- General ---------- //

            #region Rip
            string RipSkillDamage1 = (PantheraConfig.Rip_atk1DamageMultiplier * 100).ToString();
            string RipSkillDamage2 = (PantheraConfig.Rip_atk2DamageMultiplier * 100).ToString();
            string RipSkillDamage3= (PantheraConfig.Rip_atk3DamageMultiplier * 100).ToString();
            string RipSkillName = "Rip and Shred";
            string RipSkillDesc = "Rip and Shred all targets in front of you 3 times for "
                + Utils.ColorHelper.SetDamage(RipSkillDamage1 + "%") + ", "
                + Utils.ColorHelper.SetDamage(RipSkillDamage2 + "%") + ", "
                + Utils.ColorHelper.SetDamage(RipSkillDamage3 + "%") + " damage."
                + Environment.NewLine
                + "Keep pressing to use continuously.";
            ;
            PantheraTokens.Add("RIP_SKILL_NAME", RipSkillName);
            PantheraTokens.Add("RIP_SKILL_DESC", RipSkillDesc);
            #endregion

            #region Air Cleave
            string AirCleaveDamage = (PantheraConfig.AirCleave_atk1DamageMultiplier * 100).ToString();
            string AirCleaveSkillName = "Air Cleave";
            string AirCleaveSkillDesc = "Cleave the air and send a Projectile that deal "
                + Utils.ColorHelper.SetDamage(AirCleaveDamage + "%")
                + " damage to all Enemies on the way."
                ;
            PantheraTokens.Add("AIR_CLEAVE_SKILL_NAME", AirCleaveSkillName);
            PantheraTokens.Add("AIR_CLEAVE_SKILL_DESC", AirCleaveSkillDesc);
            #endregion

            // ---------- Destruction ---------- //

            #region Claws Storm
            string ClawsStormDamage = Preset.SelectedPreset != null ? (Preset.SelectedPreset.clawsStorm_damageMultiplier * 100).ToString() : (PantheraConfig.ClawsStorm_damageMultiplier * 100).ToString();
            string ClawsStormFireDelay = Preset.SelectedPreset != null ? (Preset.SelectedPreset.clawsStorm_firedDelay).ToString() : (PantheraConfig.ClawsStorm_firedDelay).ToString();
            string ClawsStormConsFury = PantheraConfig.ClawsStorm_continuousConsumedFury.ToString();
            string ClawsStormConsTime = PantheraConfig.ClawsStorm_continuousConsumeTime.ToString();
            string ClawsStormRequiredFury = PantheraConfig.ClawsStorm_requiredFury.ToString();
            string ClawsStormSkillName = "Claws Storm";
            string ClawsStormSkillDesc = "Dash in one direction with a storm of Slash and inflict "
                + Utils.ColorHelper.SetDamage(ClawsStormDamage + "%")
                + " damages every "
                + Utils.ColorHelper.SetUtility(ClawsStormFireDelay + "s")
                + "." + Environment.NewLine
                + "Keep pressing to use continuously for "
                + Utils.ColorHelper.SetFury(ClawsStormConsFury + " Fury")
                + " Point every "
                + Utils.ColorHelper.SetUtility(ClawsStormConsTime + "s")
                + "." + Environment.NewLine
                + "Require "
                + Utils.ColorHelper.SetFury(ClawsStormRequiredFury + " Fury")
                + " Points to activate";
            PantheraTokens.Add("CLAWSSTORM_SKILL_NAME", ClawsStormSkillName);
            PantheraTokens.Add("CLAWSSTORM_SKILL_DESC", ClawsStormSkillDesc);
            #endregion

            #region The Rip-per
            string TheRipperDamageIncrease = (PantheraConfig.TheRipper_damageIncreasePercent * 100).ToString();
            string TheRipperSpeedIncrease = (PantheraConfig.TheRipper_speedIncreasePercent * 100).ToString();
            string TheRipperBuffDuration = (PantheraConfig.TheRipper_buffDuration).ToString();
            string TheRipperMaxStack = Preset.SelectedPreset != null ? (Preset.SelectedPreset.theRipper_maxStack).ToString() : (PantheraConfig.TheRipper_maxStack).ToString();
            string TheRipperSkillName = "The Rip-per";
            string TheRipperSkillDesc = "Every time "
                + ColorHelper.SetUtility(RipSkillName) + " or "
                + ColorHelper.SetUtility(AirCleaveSkillName)
                + " hit a target, you gain a buff that multiply the damage of Rip and Shred by "
                + Utils.ColorHelper.SetDamage(TheRipperDamageIncrease + "%")
                + " and divide the speed by "
                + Utils.ColorHelper.SetDamage(TheRipperSpeedIncrease + "%") +
                " for " + Utils.ColorHelper.SetUtility(TheRipperBuffDuration + "s") + "." + Environment.NewLine
                + "Can stack "
                + Utils.ColorHelper.SetUtility(TheRipperMaxStack) + " Time.";
            PantheraTokens.Add("THE_RIPPER_SKILL_NAME", TheRipperSkillName);
            PantheraTokens.Add("THE_RIPPER_SKILL_DESC", TheRipperSkillDesc);
            #endregion

            #region Slash
            string SlashDamage = Preset.SelectedPreset != null ? (Preset.SelectedPreset.circularSaw_damage * 100).ToString() : (PantheraConfig.Slash_damageMultiplier).ToString();
            string SlashSkillName = "Slash";
            string SlashSkillDesc = "Slash all Enemies around you for " + Utils.ColorHelper.SetDamage(SlashDamage + "%") + " damage." + Environment.NewLine
                + "Every Enemy hit generate " + Utils.ColorHelper.SetFury("1 Fury") + " Point.";
            PantheraTokens.Add("SLASH_SKILL_NAME", SlashSkillName);
            PantheraTokens.Add("SLASH_SKILL_DESC", SlashSkillDesc);
            #endregion

            #region Fire Bird
            string FireBirdDamage = (PantheraConfig.FireBird_damageMultiplier * 100).ToString();
            string FireBirdBurnDuration = PantheraConfig.FireBird_burnDuration.ToString();
            string FireBirdBurnDamage = (PantheraConfig.FireBird_burnDamageMultiplier * 100).ToString();
            string FireBirdSkillName = "Fire Bird";
            string FireBirdSkillDesc = "Send a giant Fire Bird in front of you that deals " + Utils.ColorHelper.SetDamage(FireBirdDamage + "%") + " damage." + Environment.NewLine
                + "Set Enemies on Strong Burn " + Utils.ColorHelper.SetUtility(FireBirdBurnDuration + "s")
                + " for " + Utils.ColorHelper.SetDamage(FireBirdBurnDamage + "%") + " damage.";
            PantheraTokens.Add("FIRE_BIRD_SKILL_NAME", FireBirdSkillName);
            PantheraTokens.Add("FIRE_BIRD_SKILL_DESC", FireBirdSkillDesc);
            #endregion

            // ---------- Guardian ---------- //

            #region Front Shield
            string FrontShieldmaxHP = Preset.SelectedPreset != null ? (Preset.SelectedPreset.frontShield_maxShieldHealthPercent * 100).ToString() : (PantheraConfig.FrontShield_maxShieldHealthPercent * 100).ToString();
            string FrontShieldDamageReduction = Preset.SelectedPreset != null ? (Preset.SelectedPreset.frontShield_damageDecreaseMultiplier * 100).ToString() : (PantheraConfig.FrontShield_damageDecreaseMultiplier * 100).ToString();
            string FrontShieldRDamageDelay = Preset.SelectedPreset != null ? (Preset.SelectedPreset.frontShield_rechargeDelayAfterDamage).ToString() : (PantheraConfig.FrontShield_rechargeDelayAfterDamage).ToString();
            string FrontShieldRDestroyedDelay = Preset.SelectedPreset != null ? (Preset.SelectedPreset.frontShield_rechargeDelayAfterDestroyed).ToString() : (PantheraConfig.FrontShield_rechargeDelayAfterDestroyed).ToString();
            string FrontShieldSkillName = "Shield";
            string FrontShieldSkillDesc = "Create a Shield in front of you that absorb incoming damages. Stay pressed to keep the Shield activated." + Environment.NewLine + Environment.NewLine
                + "- Capacity: " + Utils.ColorHelper.SetShield(FrontShieldmaxHP + "%") + " of the max Health" + Environment.NewLine
                + "- Damage absorbed reduction: " + Utils.ColorHelper.SetHeal(FrontShieldDamageReduction + "%") + Environment.NewLine
                + "- Recharge delay after damages: " + Utils.ColorHelper.SetUtility(FrontShieldRDamageDelay + "s") + Environment.NewLine
                + "- Recharge delay after destroyed: " + Utils.ColorHelper.SetUtility(FrontShieldRDestroyedDelay + "s");
            PantheraTokens.Add("FRONT_SHIELD_SKILL_NAME", FrontShieldSkillName);
            PantheraTokens.Add("FRONT_SHIELD_SKILL_DESC", FrontShieldSkillDesc);
            #endregion

            #region Shield Bash
            string ShieldBashDamage = Preset.SelectedPreset != null ? (Preset.SelectedPreset.shieldBash_damage * 100).ToString() : (PantheraConfig.ShieldBash_damage1).ToString();
            string ShieldBashStun = Preset.SelectedPreset != null ? (Preset.SelectedPreset.shieldBash_stunDuration).ToString() : (PantheraConfig.ShieldBash_stunDuration1).ToString();
            string ShieldBashSkillName = "Shield Bash";
            string ShieldBashSkillDesc = Utils.ColorHelper.SetBlue("Activated by pressing Rip and Shred while the Shield is active.") + Environment.NewLine
                + "Do a forward rush that stuns all Enemies in the way for " + Utils.ColorHelper.SetUtility(ShieldBashStun + "s")
                + ". Inflict " + Utils.ColorHelper.SetDamage(ShieldBashDamage + "%") + " damage." + Environment.NewLine
                + "The shield does not decrease when using Shield Bash.";
            PantheraTokens.Add("SHIELD_BASH_SKILL_NAME", ShieldBashSkillName);
            PantheraTokens.Add("SHIELD_BASH_SKILL_DESC", ShieldBashSkillDesc);
            #endregion

            // ---------- Ruse ---------- //

            #region Mighty Roar
            string MightyRoarRadius = Preset.SelectedPreset != null ? (Preset.SelectedPreset.mightyRoar_radius).ToString() : (PantheraConfig.MightyRoar_radius).ToString();
            string MightyRoarDuration = Preset.SelectedPreset != null ? (Preset.SelectedPreset.mightyRoar_stunDuration).ToString() : (PantheraConfig.MightyRoar_stunDuration).ToString();
            string MightyRoarSkillName = "Mighty Roar";
            string MightyRoarSkillDesc = "Do a ferocious Mighty Roar that "
                + ColorHelper.SetUtility("stuns")
                + " all Targets within "
                + Utils.ColorHelper.SetUtility(MightyRoarRadius + "m")
                + " for "
                + Utils.ColorHelper.SetUtility(MightyRoarDuration + "s")
                + "."
                ;
            PantheraTokens.Add("MIGHTY_ROAR_SKILL_NAME", MightyRoarSkillName);
            PantheraTokens.Add("MIGHTY_ROAR_SKILL_DESC", MightyRoarSkillDesc);
            #endregion

            #region Prowl
            string ProwlSkillName = "Prowl";
            string ProwlSkillDesc = ColorHelper.SetBlue("Press again to Cancel.") + Environment.NewLine
                + "You become "
                + ColorHelper.SetUtility("invisible")
                + " to all Enemies. Canceled if you do or take Damage.";
            PantheraTokens.Add("PROWL_SKILL_NAME", ProwlSkillName);
            PantheraTokens.Add("PROWL_SKILL_DESC", ProwlSkillDesc);
            #endregion

            #region Furious Bite
            string FuriousBiteDamage = Preset.SelectedPreset != null ? (Preset.SelectedPreset.furiousBite_atkDamageMultiplier * 100).ToString() : (PantheraConfig.FuriousBite_atkDamageMultiplier * 100).ToString();
            string FuriousBiteCPDamage = Preset.SelectedPreset != null ? (Preset.SelectedPreset.furiousBite_ComboPointMultiplier * 100).ToString() : (PantheraConfig.FuriousBite_ComboPointMultiplier * 100).ToString();
            string FuriousBiteSkillName = "Furious Bite";
            string FuriousBiteSkillDesc = "Bite the target and inflict "
                + Utils.ColorHelper.SetDamage(FuriousBiteDamage + "%")
                + " damage." + Environment.NewLine
                + "Consume all "
                + Utils.ColorHelper.SetComboPoint("Combo Points") + "." + Environment.NewLine
                + "For every "
                + Utils.ColorHelper.SetComboPoint("Combo Point") + Environment.NewLine
                + "consumed, the damage are increased by "
                + Utils.ColorHelper.SetDamage(FuriousBiteCPDamage + "%") + ".";
            PantheraTokens.Add("FURIOUS_BITE_SKILL_NAME", FuriousBiteSkillName);
            PantheraTokens.Add("FURIOUS_BITE_SKILL_DESC", FuriousBiteSkillDesc);
            #endregion

            // ---------- Utility ---------- //

            #region Leap
            string LeapSkillName = "Predator Leap";
            string LeapSkillDesc = Utils.ColorHelper.SetBlue("Activated by double pressing the Jump Key.") + Environment.NewLine
                + "Jump forward or to the focused Target. Can be used while in air.";
            PantheraTokens.Add("LEAP_SKILL_NAME", LeapSkillName);
            PantheraTokens.Add("LEAP_SKILL_DESC", LeapSkillDesc);
            #endregion

            #region Dash
            string DashMoveSpeed = Preset.SelectedPreset != null ? (Preset.SelectedPreset.dash_speedMultiplier * 100).ToString() : (PantheraConfig.Dash_speedMultiplier1 * 100).ToString();
            string DashEnergyConsumed = Preset.SelectedPreset != null ? (Preset.SelectedPreset.dash_staminaConsumed*60).ToString() : (PantheraConfig.Dash_staminaConsumed1*60).ToString(); ;
            string DashSkillName = "Dash";
            string DashSkillDesc = Utils.ColorHelper.SetBlue("Activated by pressing Run again while Running.") + Environment.NewLine
                + "Improve your Move Speed by " + Utils.ColorHelper.SetUtility(DashMoveSpeed) + "%, consume " + Utils.ColorHelper.SetStamina(DashEnergyConsumed ) + " Stamina per second. Press again to stop the Dash.";
            PantheraTokens.Add("DASH_SKILL_NAME", DashSkillName);
            PantheraTokens.Add("DASH_SKILL_DESC", DashSkillDesc);
            #endregion

            #region Zone Heal
            string ZoneHealDuration = Preset.SelectedPreset != null ? (Preset.SelectedPreset.zoneHeal_healDuration).ToString() : (PantheraConfig.ZoneHeal_healDuration).ToString();
            string ZoneHealPercentHeal = Preset.SelectedPreset != null ? (Preset.SelectedPreset.zoneHeal_percentHeal * 100).ToString() : (PantheraConfig.ZoneHeal_percentHeal1 * 100).ToString();
            string ZoneHealRate = PantheraConfig.ZoneHeal_healRate.ToString();
            string ZoneHealSkillName = "Zone Heal";
            string ZoneHealSkillDesc = Utils.ColorHelper.SetBlue("Can only be used while on the ground.") + Environment.NewLine
                + "Place a Heal Circle on the ground that last " + Utils.ColorHelper.SetUtility(ZoneHealDuration + "s") + "."
                + " All Players on the cercle will be Healed for " + Utils.ColorHelper.SetHeal(ZoneHealPercentHeal + "%")
                + " every " + Utils.ColorHelper.SetUtility(ZoneHealRate + "s") + ".";
            PantheraTokens.Add("ZONE_HEAL_SKILL_NAME", ZoneHealSkillName);
            PantheraTokens.Add("ZONE_HEAL_SKILL_DESC", ZoneHealSkillDesc);
            #endregion

            #region Revive
            string ReviveSkillName = "Revive";
            string ReviveSkillDesc = "Revive a random dead Ally. Canceled if you move.";
            PantheraTokens.Add("REVIVE_SKILL_NAME", ReviveSkillName);
            PantheraTokens.Add("REVIVE_SKILL_DESC", ReviveSkillDesc);
            #endregion

            #region Detection
            string DetectionStaminaConsumed = Preset.SelectedPreset != null ? (Preset.SelectedPreset.detection_staminaConsumed).ToString() : (PantheraConfig.Detection_staminaConsumed).ToString();
            string DetectionSkillName = "Detection";
            string DetectionSkillDesc = ColorHelper.SetBlue("Press again to cancel.") + Environment.NewLine
                + "Greatly increase P4N7H3R4's senses and brings you into "
                + ColorHelper.SetUtility("Predatory Mode")
                + ", decrease the perception of the environment but allows to see all the Enemies and Allies even through walls." + Environment.NewLine
                + "Consume " + ColorHelper.SetStamina(DetectionStaminaConsumed) + " Stamina per second.";
            PantheraTokens.Add("DETECTION_SKILL_NAME", DetectionSkillName);
            PantheraTokens.Add("DETECTION_SKILL_DESC", DetectionSkillDesc);
            #endregion

            #region Regeneration
            string RegenerationCooldown = PantheraConfig.Regeneration_cooldown.ToString();
            string RegenerationPercentHeal = Preset.SelectedPreset != null ? (Preset.SelectedPreset.regeneration_percentHeal * 100).ToString() : (PantheraConfig.Regeneration_percent1 * 100).ToString();
            string RegenerationSkillName = "Regeneration";
            string RegenerationSkillDesc = "You regenerate "
                + ColorHelper.SetHeal(RegenerationPercentHeal + "%")
                + " of your Health every "
                + ColorHelper.SetUtility(PantheraConfig.Regeneration_cooldown + "s") + ".";
            PantheraTokens.Add("REGENERATION_SKILL_NAME", RegenerationSkillName);
            PantheraTokens.Add("REGENERATION_SKILL_DESC", RegenerationSkillDesc);
            #endregion

            // ------------------------------------------ ABILITY --------------------------------- //

            // ---------- Destruction ---------- //

            #region Destruction
            string DestructionAbilityName = "Destruction";
            string DestructionAbilityDesc = "Improve the agressive Abilities of P4N7H3R4." + Environment.NewLine + Environment.NewLine
                + Utils.ColorHelper.SetGreen("- Unlock " + ClawsStormSkillName) + Environment.NewLine
                + Utils.ColorHelper.SetGreen("- Damage: ") + Utils.ColorHelper.SetUtility("+" + PantheraConfig.Destruction_addedDamage.ToString()) + Environment.NewLine
                + Utils.ColorHelper.SetGreen("- Level Damage: ") + Utils.ColorHelper.SetUtility("+" + PantheraConfig.Destruction_addedDamageLevel.ToString()) + Environment.NewLine
                + Utils.ColorHelper.SetGreen("- Attack Speed: ") + Utils.ColorHelper.SetUtility("+" + PantheraConfig.Destruction_addedAttackSpeed.ToString()) + Environment.NewLine
                + Utils.ColorHelper.SetGreen("- Level Attack Speed: ") + Utils.ColorHelper.SetUtility("+" + PantheraConfig.Destruction_addedAttackSpeedLevel.ToString()) + Environment.NewLine + Environment.NewLine
                + Utils.ColorHelper.SetFury("You generate ")
                + Utils.ColorHelper.SetUtility(PantheraConfig.Fury_pointsGenerated.ToString())
                + Utils.ColorHelper.SetFury(" Fury point every ")
                + Utils.ColorHelper.SetUtility(PantheraConfig.Fury_pointsGenerationCooldown.ToString())
                + Utils.ColorHelper.SetUtility("s")
                + Utils.ColorHelper.SetFury(" while in combat.") + Environment.NewLine + Environment.NewLine
                + Utils.ColorHelper.SetFury("You also generate ")
                + Utils.ColorHelper.SetUtility(PantheraConfig.Fury_pointsGenerated.ToString())
                + Utils.ColorHelper.SetFury(" Fury point everytime you take damage");
            PantheraTokens.Add("DESTRUCTION_ABILITY_NAME", DestructionAbilityName);
            PantheraTokens.Add("DESTRUCTION_ABILITY_DESC", DestructionAbilityDesc);
            #endregion

            #region Improved Claws Storm 
            string ImprovedClawsStormAbilityName = "Sharpened Claws";
            string ImprovedClawsStormAbilityDesc = "Reduce the "
            + ColorHelper.SetUtility(ClawsStormSkillName)
            + " cooldown and increase its damage." + Environment.NewLine + Environment.NewLine
            + "Skill Cooldown:" + Environment.NewLine
            + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.ImprovedClawsStorm_cooldown1 + "s", PantheraConfig.ImprovedClawsStormAbilityID, 1) + Environment.NewLine
            + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.ImprovedClawsStorm_cooldown2 + "s", PantheraConfig.ImprovedClawsStormAbilityID, 2) + Environment.NewLine
            + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.ImprovedClawsStorm_cooldown3 + "s", PantheraConfig.ImprovedClawsStormAbilityID, 3) + Environment.NewLine
            + Utils.ColorHelper.GetColorForAbility("Level 4: " + PantheraConfig.ImprovedClawsStorm_cooldown4 + "s", PantheraConfig.ImprovedClawsStormAbilityID, 4) + Environment.NewLine
            + Utils.ColorHelper.GetColorForAbility("Level 5: " + PantheraConfig.ImprovedClawsStorm_cooldown5 + "s", PantheraConfig.ImprovedClawsStormAbilityID, 5) + Environment.NewLine + Environment.NewLine
            + "Base Damages:" + Environment.NewLine
            + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.ImprovedClawsStorm_DamageIncrease1 * 100 + "%", PantheraConfig.ImprovedClawsStormAbilityID, 1) + Environment.NewLine
            + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.ImprovedClawsStorm_DamageIncrease2 * 100 + "%", PantheraConfig.ImprovedClawsStormAbilityID, 2) + Environment.NewLine
            + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.ImprovedClawsStorm_DamageIncrease2 * 100 + "%", PantheraConfig.ImprovedClawsStormAbilityID, 3) + Environment.NewLine
            + Utils.ColorHelper.GetColorForAbility("Level 4: " + PantheraConfig.ImprovedClawsStorm_DamageIncrease4 * 100 + "%", PantheraConfig.ImprovedClawsStormAbilityID, 4) + Environment.NewLine
            + Utils.ColorHelper.GetColorForAbility("Level 5: " + PantheraConfig.ImprovedClawsStorm_DamageIncrease5 * 100 + "%", PantheraConfig.ImprovedClawsStormAbilityID, 5);
            PantheraTokens.Add("IMPROVED_CLAWS_STORM_ABILITY_NAME", ImprovedClawsStormAbilityName);
            PantheraTokens.Add("IMPROVED_CLAWS_STORM_ABILITY_DESC", ImprovedClawsStormAbilityDesc);
            #endregion

            #region Healing Storm
            string HealingStormAbilityName = "Healing Storm";
            string HealingStormAbilityDesc = ColorHelper.SetHeal("Heal")
                + " the Player for a percentage of damage done by "
                + ColorHelper.SetUtility(ClawsStormSkillName) + "." + Environment.NewLine + Environment.NewLine
                + "Percentage amount:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.HealingStrom_percent1 + "%", PantheraConfig.HealingStormAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.HealingStrom_percent2 + "%", PantheraConfig.HealingStormAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.HealingStrom_percent3 + "%", PantheraConfig.HealingStormAbilityID, 3) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 4: " + PantheraConfig.HealingStrom_percent4 + "%", PantheraConfig.HealingStormAbilityID, 4) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 5: " + PantheraConfig.HealingStrom_percent5 + "%", PantheraConfig.HealingStormAbilityID, 5);
            PantheraTokens.Add("HEALING_STORM_ABILITY_NAME", HealingStormAbilityName);
            PantheraTokens.Add("HEALING_STORM_ABILITY_DESC", HealingStormAbilityDesc);
            #endregion

            #region Tornado
            string TornadoAbilityName = "Tornado";
            string TornadoAbilityDesc = "Increase the speed of "
                + ColorHelper.SetUtility(ClawsStormSkillName) + "." + Environment.NewLine + Environment.NewLine
                + "Damage interval:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.Tornado_speed1 + "s", PantheraConfig.TornadoAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.Tornado_speed2 + "s", PantheraConfig.TornadoAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.Tornado_speed3 + "s", PantheraConfig.TornadoAbilityID, 3);
            PantheraTokens.Add("TORNADO_ABILITY_NAME", TornadoAbilityName);
            PantheraTokens.Add("TORNADO_ABILITY_DESC", TornadoAbilityDesc);
            #endregion

            #region The Rip-per
            string TheRipperAbilityName = "The Rip-per";
            string TheRipperAbilityDesc = "Everytime "
                + ColorHelper.SetUtility(RipSkillName)
                + " or "
                + ColorHelper.SetUtility(AirCleaveSkillName)
                + " hit a target, you gain a Buff that multiply the Damage of "
                + ColorHelper.SetUtility(RipSkillName)
                + " by "
                + Utils.ColorHelper.SetDamage(PantheraConfig.TheRipper_damageIncreasePercent * 100 + "%")
                + " and divide its Speed by "
                + Utils.ColorHelper.SetDamage(PantheraConfig.TheRipper_speedIncreasePercent * 100 + "%") +
                " for " + Utils.ColorHelper.SetUtility(PantheraConfig.TheRipper_buffDuration + "s") + "." + Environment.NewLine
                + "Can stack "
                + Utils.ColorHelper.SetUtility(PantheraConfig.TheRipper_maxStack.ToString()) + " Time.";
            PantheraTokens.Add("THE_RIPPER_ABILITY_NAME", TheRipperAbilityName);
            PantheraTokens.Add("THE_RIPPER_ABILITY_DESC", TheRipperAbilityDesc);
            #endregion

            #region Instinctive Resistance
            string InstinctiveResistanceAbilityName = "Instinctive Resistance";
            string InstinctiveResistanceAbilityDesc = "Each Stack of "
                + ColorHelper.SetUtility(TheRipperAbilityName)
                + " Buff reduce the Damage you take." + Environment.NewLine + Environment.NewLine
                + "Damage reduction:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.InstinctiveResistance_percent1 * 100 + "% per Stack", PantheraConfig.InstinctiveResistanceAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.InstinctiveResistance_percent2 * 100 + "% per Stack", PantheraConfig.InstinctiveResistanceAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.InstinctiveResistance_percent3 * 100 + "% per Stack", PantheraConfig.InstinctiveResistanceAbilityID, 3) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 4: " + PantheraConfig.InstinctiveResistance_percent4 * 100 + "% per Stack", PantheraConfig.InstinctiveResistanceAbilityID, 4) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 5: " + PantheraConfig.InstinctiveResistance_percent5 * 100 + "% per Stack", PantheraConfig.InstinctiveResistanceAbilityID, 5) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 6: " + PantheraConfig.InstinctiveResistance_percent6 * 100 + "% per Stack", PantheraConfig.InstinctiveResistanceAbilityID, 6);
            PantheraTokens.Add("INSTINCTIVE_RESISTANCE_ABILITY_NAME", InstinctiveResistanceAbilityName);
            PantheraTokens.Add("INSTINCTIVE_RESISTANCE_ABILITY_DESC", InstinctiveResistanceAbilityDesc);
            #endregion

            #region Bloody Rage
            string BloodyRageAbilityName = "Bloody Rage";
            string BloodyRageAbilityDesc = "Each Stack of "
                + ColorHelper.SetUtility(TheRipperAbilityName)
                + " Buff give you a chance to Add " + Utils.ColorHelper.SetFury("1 Fury") + " point when "
                + ColorHelper.SetUtility(RipSkillName)
                + " do damage." + Environment.NewLine + Environment.NewLine
                + "Chance per Stack:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.BloodyRage_Percent1 * 100 + "% per Stack", PantheraConfig.BloodyRageAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.BloodyRage_Percent2 * 100 + "% per Stack", PantheraConfig.BloodyRageAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.BloodyRage_Percent3 * 100 + "% per Stack", PantheraConfig.BloodyRageAbilityID, 3) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 4: " + PantheraConfig.BloodyRage_Percent4 * 100 + "% per Stack", PantheraConfig.BloodyRageAbilityID, 4) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 5: " + PantheraConfig.BloodyRage_Percent5 * 100 + "% per Stack", PantheraConfig.BloodyRageAbilityID, 5);
            PantheraTokens.Add("BLOODY_RAGE_ABILITY_NAME", BloodyRageAbilityName);
            PantheraTokens.Add("BLOODY_RAGE_ABILITY_DESC", BloodyRageAbilityDesc);
            #endregion

            #region God of Reapers
            string GodOfReapersAbilityName = "God of Reapers";
            string GodOfReapersAbilityDesc = "Increase the maximum Stack of "
                + ColorHelper.SetUtility(TheRipperAbilityName)
                + " Buff." + Environment.NewLine + Environment.NewLine
                + "Total Buffs Stack:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.GodOfReapers_maxBuff1 + " Stacks", PantheraConfig.GodOfReapersAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.GodOfReapers_maxBuff2 + " Stacks", PantheraConfig.GodOfReapersAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.GodOfReapers_maxBuff3 + " Stacks", PantheraConfig.GodOfReapersAbilityID, 3) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 4: " + PantheraConfig.GodOfReapers_maxBuff4 + " Stacks", PantheraConfig.GodOfReapersAbilityID, 4) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 5: " + PantheraConfig.GodOfReapers_maxBuff5 + " Stacks", PantheraConfig.GodOfReapersAbilityID, 5);
            PantheraTokens.Add("GOD_OF_REAPERS_ABILITY_NAME", GodOfReapersAbilityName);
            PantheraTokens.Add("GOD_OF_REAPERS_ABILITY_DESC", GodOfReapersAbilityDesc);
            #endregion

            #region Burning Spirit
            string BurningSpiritAbilityName = "Burning Spirit";
            string BurningSpiritAbilityDesc = "If you have at least " + Utils.ColorHelper.SetUtility(PantheraConfig.BurningSpirit_ripperStackNeeded.ToString())
                + " Stacks of "
                + ColorHelper.SetUtility(TheRipperAbilityName)
                + " Buff, your "
                + ColorHelper.SetUtility(RipSkillName)
                + " and your "
                + ColorHelper.SetUtility(AirCleaveSkillName)
                + " is improved with Fire. All Enemies hit will be set on fire " + Utils.ColorHelper.SetUtility(PantheraConfig.BurningSpirit_burnDuration + "s")
                + " for " + Utils.ColorHelper.SetDamage(PantheraConfig.BurningSpirit_burnDamage * 100 + "%") + " damage.";
            PantheraTokens.Add("BURNING_SPIRIT_ABILITY_NAME", BurningSpiritAbilityName);
            PantheraTokens.Add("BURNING_SPIRIT_ABILITY_DESC", BurningSpiritAbilityDesc);
            #endregion

            #region Hell Cat
            string HellCatAbilityName = "Hell Cat";
            string HellCatAbilityDesc = "Everytime "
                + ColorHelper.SetUtility(BurningSpiritAbilityName)
                + " set an Enemy on fire, you generate " + Utils.ColorHelper.SetFury("1 Fury") + " Point and "
                + Utils.ColorHelper.SetPower("1 God Power") + " Point.";
            PantheraTokens.Add("HELL_CAT_ABILITY_NAME", HellCatAbilityName);
            PantheraTokens.Add("HELL_CAT_ABILITY_DESC", HellCatAbilityDesc);
            #endregion

            #region Circular Saw
            string CircularSawAbilityName = "Circular Saw";
            string CircularSawAbilityDesc = "Improve the Damage and reduce the Energy required of "
                + ColorHelper.SetUtility(SlashSkillName) + "." + Environment.NewLine + Environment.NewLine
                + "Damage:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.CircualSaw_damage1 * 100 + "%", PantheraConfig.CircularSawAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.CircualSaw_damage2 * 100 + "%", PantheraConfig.CircularSawAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.CircualSaw_damage3 * 100 + "%", PantheraConfig.CircularSawAbilityID, 3) + Environment.NewLine + Environment.NewLine
                + "Energy required:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.CircualSaw_energy1, PantheraConfig.CircularSawAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.CircualSaw_energy2, PantheraConfig.CircularSawAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.CircualSaw_energy3, PantheraConfig.CircularSawAbilityID, 3);
            PantheraTokens.Add("CIRCULAR_SAW_ABILITY_NAME", CircularSawAbilityName);
            PantheraTokens.Add("CIRCULAR_SAW_ABILITY_DESC", CircularSawAbilityDesc);
            #endregion

            #region Ignition
            string IgnitionAbilityName = "Ignition";
            string IgnitionAbilityDesc = ColorHelper.SetUtility(SlashSkillName)
                + " also create an heat burst that set all Enemies on fire in a radius of "
                + Utils.ColorHelper.SetUtility(PantheraConfig.Ignition_radius + "m") + "." + Environment.NewLine + Environment.NewLine
                + "Damage:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.Ignition_burnDamage1 * 100 + "%", PantheraConfig.IgnitionAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.Ignition_burnDamage2 * 100 + "%", PantheraConfig.IgnitionAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.Ignition_burnDamage3 * 100 + "%", PantheraConfig.IgnitionAbilityID, 3) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 4: " + PantheraConfig.Ignition_burnDamage4 * 100 + "%", PantheraConfig.IgnitionAbilityID, 4) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 5: " + PantheraConfig.Ignition_burnDamage5 * 100 + "%", PantheraConfig.IgnitionAbilityID, 5) + Environment.NewLine
                + "Burn duration:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.Ignition_burnDuration1 + "s", PantheraConfig.IgnitionAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.Ignition_burnDuration2 + "s", PantheraConfig.IgnitionAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.Ignition_burnDuration3 + "s", PantheraConfig.IgnitionAbilityID, 3) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 4: " + PantheraConfig.Ignition_burnDuration4 + "s", PantheraConfig.IgnitionAbilityID, 4) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 5: " + PantheraConfig.Ignition_burnDuration5 + "s", PantheraConfig.IgnitionAbilityID, 5);
            PantheraTokens.Add("IGNITION_ABILITY_NAME", IgnitionAbilityName);
            PantheraTokens.Add("IGNITION_ABILITY_DESC", IgnitionAbilityDesc);
            #endregion

            #region Souls Shelter
            string SoulsShelterAbilityName = "Souls Shelter";
            string SoulsShelterAbilityDesc = "For each Enemy hit by "
                + ColorHelper.SetUtility(SlashSkillName)
                + ", you generate " + Utils.ColorHelper.SetPower("1 God Power") + " Point.";
            PantheraTokens.Add("SOULS_SHELTER_ABILITY_NAME", SoulsShelterAbilityName);
            PantheraTokens.Add("SOULS_SHELTER_ABILITY_DESC", SoulsShelterAbilityDesc);
            #endregion

            #region The Slash-per
            string TheSlashPerAbilityName = "The Slash-per";
            string TheSlashPerAbilityDesc = "Add a Stack of "
                + ColorHelper.SetUtility(TheRipperSkillName)
                + " Buff everytime "
                + ColorHelper.SetUtility(SlashSkillName)
                + " deal damage";
            PantheraTokens.Add("THE_SLASHPER_ABILITY_NAME", TheSlashPerAbilityName);
            PantheraTokens.Add("THE_SLASHPER_ABILITY_DESC", TheSlashPerAbilityDesc);
            #endregion

            #region High Temperature
            string HighTemperatureAbilityName = "High Temperature";
            string HighTemperatureAbilityDesc = "Improve the Radius of "
                + ColorHelper.SetUtility(IgnitionAbilityName) + "." + Environment.NewLine + Environment.NewLine
                + "Radius:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.HighTemperature_radius1 + "m", PantheraConfig.HighTemperatureAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.HighTemperature_radius2 + "m", PantheraConfig.HighTemperatureAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.HighTemperature_radius3 + "m", PantheraConfig.HighTemperatureAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 4: " + PantheraConfig.HighTemperature_radius4 + "m", PantheraConfig.HighTemperatureAbilityID, 4);
            PantheraTokens.Add("HIGH_TEMPERATURE_ABILITY_NAME", HighTemperatureAbilityName);
            PantheraTokens.Add("HIGH_TEMPERATURE_ABILITY_DESC", HighTemperatureAbilityDesc);
            #endregion

            #region Sacred Flames
            string SacredFlamesAbilityName = "Sacred Flames";
            string SacredFlamesAbilityDesc = "The flammes created by your "
                + ColorHelper.SetUtility(IgnitionAbilityName)
                + " also "
                + ColorHelper.SetHeal("Heal")
                + " you and all Allies in radius." + Environment.NewLine + Environment.NewLine
                + "Heal amount:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.SacredFlames_percent1 * 100 + "%", PantheraConfig.SacredFlamesAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.SacredFlames_percent2 * 100 + "%", PantheraConfig.SacredFlamesAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.SacredFlames_percent3 * 100 + "%", PantheraConfig.SacredFlamesAbilityID, 3);
            PantheraTokens.Add("SACRED_FLAMES_ABILITY_NAME", SacredFlamesAbilityName);
            PantheraTokens.Add("SACRED_FLAMES_ABILITY_DESC", SacredFlamesAbilityDesc);
            #endregion

            #region Angry Bird
            string AngryBirdAbilityName = "Angry Bird";
            string AngryBirdAbilityDesc = ColorHelper.SetUtility(FireBirdSkillName)
                + " generate " + Utils.ColorHelper.SetPower("1 God Power") + " Point for each Enemy burnt.";
            PantheraTokens.Add("ANGRY_BIRD_ABILITY_NAME", AngryBirdAbilityName);
            PantheraTokens.Add("ANGRY_BIRD_ABILITY_DESC", AngryBirdAbilityDesc);
            #endregion

            // ---------- Guardian ---------- //

            #region Guardian 
            string GuardianAbilityName = "Guardian";
            string GuardianAbilityDesc = "Greatly increase the defensive Abilities of P4N7H3R4." + Environment.NewLine 
                + "Make your Character " + ((PantheraConfig.Guardian_modelScale - PantheraConfig.Model_defaultModelScale) * 100) + "% bigger (affect some Skills size)." + Environment.NewLine + Environment.NewLine
                + Utils.ColorHelper.SetGreen("- Unlock " + FrontShieldSkillName) + Environment.NewLine
                + Utils.ColorHelper.SetGreen("- Health: ") + Utils.ColorHelper.SetUtility("+" + PantheraConfig.Guardian_addedHealth.ToString()) + Environment.NewLine
                + Utils.ColorHelper.SetGreen("- Level Health: ") + Utils.ColorHelper.SetUtility("+" + PantheraConfig.Guardian_addedHealthLevel.ToString()) + Environment.NewLine
                + Utils.ColorHelper.SetGreen("- Health Regen: ") + Utils.ColorHelper.SetUtility("+" + PantheraConfig.Guardian_addedHealthRegen.ToString()) + Environment.NewLine
                + Utils.ColorHelper.SetGreen("- Level Health Regen: ") + Utils.ColorHelper.SetUtility("+" + PantheraConfig.Guardian_addedHealthRegenLevel.ToString()) + Environment.NewLine
                + Utils.ColorHelper.SetGreen("- Defense: ") + Utils.ColorHelper.SetUtility("+" + PantheraConfig.Guardian_addedDefense.ToString()) + Environment.NewLine
                + Utils.ColorHelper.SetGreen("- Level Defense: ") + Utils.ColorHelper.SetUtility("+" + PantheraConfig.Guardian_addedDefenseLevel.ToString()) + Environment.NewLine;
            PantheraTokens.Add("GUARDIAN_ABILITY_NAME", GuardianAbilityName);
            PantheraTokens.Add("GUARDIAN_ABILITY_DESC", GuardianAbilityDesc);
            #endregion

            #region Shield Focus
            string ShieldFocusAbilityName = "Shield Focus";
            string ShieldFocusAbilityDesc = "Improve the "
                + ColorHelper.SetShield("Shield Capacity") + " and " + ColorHelper.SetShield("Damages Absorbsion") + " efficiency." + Environment.NewLine + Environment.NewLine
                + "Capacity:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.ShieldFocus_healthPercent1 * 100 + "% of max Health", PantheraConfig.ShieldFocusAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.ShieldFocus_healthPercent2 * 100 + "% of max Health", PantheraConfig.ShieldFocusAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.ShieldFocus_healthPercent3 * 100 + "% of max Health", PantheraConfig.ShieldFocusAbilityID, 3) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 4: " + PantheraConfig.ShieldFocus_healthPercent4 * 100 + "% of max Health", PantheraConfig.ShieldFocusAbilityID, 4) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 5: " + PantheraConfig.ShieldFocus_healthPercent5 * 100 + "% of max Health", PantheraConfig.ShieldFocusAbilityID, 5) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 6: " + PantheraConfig.ShieldFocus_healthPercent6 * 100 + "% of max Health", PantheraConfig.ShieldFocusAbilityID, 6) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 7: " + PantheraConfig.ShieldFocus_healthPercent7 * 100 + "% of max Health", PantheraConfig.ShieldFocusAbilityID, 7) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 8: " + PantheraConfig.ShieldFocus_healthPercent8 * 100 + "% of max Health", PantheraConfig.ShieldFocusAbilityID, 8) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 9: " + PantheraConfig.ShieldFocus_healthPercent9 * 100 + "% of max Health", PantheraConfig.ShieldFocusAbilityID, 9) + Environment.NewLine + Environment.NewLine
                + "Absorbed damage reduction:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.ShieldFocus_damagePercent1 * 100 + "%", PantheraConfig.ShieldFocusAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.ShieldFocus_damagePercent2 * 100 + "%", PantheraConfig.ShieldFocusAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.ShieldFocus_damagePercent3 * 100 + "%", PantheraConfig.ShieldFocusAbilityID, 3) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 4: " + PantheraConfig.ShieldFocus_damagePercent4 * 100 + "%", PantheraConfig.ShieldFocusAbilityID, 4) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 5: " + PantheraConfig.ShieldFocus_damagePercent5 * 100 + "%", PantheraConfig.ShieldFocusAbilityID, 5) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 6: " + PantheraConfig.ShieldFocus_damagePercent6 * 100 + "%", PantheraConfig.ShieldFocusAbilityID, 6) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 7: " + PantheraConfig.ShieldFocus_damagePercent7 * 100 + "%", PantheraConfig.ShieldFocusAbilityID, 7) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 8: " + PantheraConfig.ShieldFocus_damagePercent8 * 100 + "%", PantheraConfig.ShieldFocusAbilityID, 8) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 9: " + PantheraConfig.ShieldFocus_damagePercent9 * 100 + "%", PantheraConfig.ShieldFocusAbilityID, 9);
            PantheraTokens.Add("SHIELD_FOCUS_ABILITY_NAME", ShieldFocusAbilityName);
            PantheraTokens.Add("SHIELD_FOCUS_ABILITY_DESC", ShieldFocusAbilityDesc);
            #endregion

            #region Residual Energy
            string ResidualEnergyAbilityName = "Residual Energy";
            string ResidualEnergyAbilityDesc = "Add "
                + ColorHelper.SetHeal("Barrier")
                + " equivalent to a percent of the absorbed damage of your "
                + ColorHelper.SetUtility(FrontShieldSkillName)
                + " and reduce the "
                + ColorHelper.SetHeal("Barrier")
                + " decay rate to " + Utils.ColorHelper.SetUtility((PantheraConfig.ResidualEnergy_decay * 100).ToString() + "%") + "." + Environment.NewLine + Environment.NewLine
                + "Barrier Percent:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.ResidualEnergy_percent1 * 100 + "%", PantheraConfig.ResidualEnergyAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.ResidualEnergy_percent2 * 100 + "%", PantheraConfig.ResidualEnergyAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.ResidualEnergy_percent3 * 100 + "%", PantheraConfig.ResidualEnergyAbilityID, 3) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 4: " + PantheraConfig.ResidualEnergy_percent4 * 100 + "%", PantheraConfig.ResidualEnergyAbilityID, 4) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 5: " + PantheraConfig.ResidualEnergy_percent5 * 100 + "%", PantheraConfig.ResidualEnergyAbilityID, 5);
            PantheraTokens.Add("RESIDUAL_ENERGY_ABILITY_NAME", ResidualEnergyAbilityName);
            PantheraTokens.Add("RESIDUAL_ENERGY_ABILITY_DESC", ResidualEnergyAbilityDesc);
            #endregion

            #region Defensive Haste
            string DefensiveHasteAbilityName = "Defensive Hast";
            string DefensiveHasteAbilityDesc = "Reduce the delay needed to recharge the "
                + ColorHelper.SetShield("Shield")
                + " after absorbing damage and the delay needed to recover when it is broken." + Environment.NewLine + Environment.NewLine
                + "Recharge Dalay:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.DefensiveHaste_damageDelay1 + "s", PantheraConfig.DefensiveHasteAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.DefensiveHaste_damageDelay2 + "s", PantheraConfig.DefensiveHasteAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.DefensiveHaste_damageDelay3 + "s", PantheraConfig.DefensiveHasteAbilityID, 3) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 4: " + PantheraConfig.DefensiveHaste_damageDelay4 + "s", PantheraConfig.DefensiveHasteAbilityID, 4) + Environment.NewLine + Environment.NewLine
                + "Recover Dalay:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.DefensiveHaste_destroyedDelay1 + "s", PantheraConfig.DefensiveHasteAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.DefensiveHaste_destroyedDelay2 + "s", PantheraConfig.DefensiveHasteAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.DefensiveHaste_destroyedDelay3 + "s", PantheraConfig.DefensiveHasteAbilityID, 3) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 4: " + PantheraConfig.DefensiveHaste_destroyedDelay4 + "s", PantheraConfig.DefensiveHasteAbilityID, 4);
            PantheraTokens.Add("DEFENSIVE_HASTE_ABILITY_NAME", DefensiveHasteAbilityName);
            PantheraTokens.Add("DEFENSIVE_HASTE_ABILITY_DESC", DefensiveHasteAbilityDesc);
            #endregion

            #region Shield Bash
            string ShieldBashAbilityName = "Shield Bash";
            string ShieldBashAbilityDesc = Utils.ColorHelper.SetBlue("Activated by pressing " + RipSkillName + " while the " + FrontShieldSkillName + " is active.") + Environment.NewLine
                + "Do a forward rush that "
                + ColorHelper.SetUtility("Stuns")
                + " all Enemies in the way." + Environment.NewLine
                + "The Shield does not decrease while using "
                + ColorHelper.SetUtility(ShieldBashSkillName) + "." + Environment.NewLine + Environment.NewLine
                + "Damage:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.ShieldBash_damage1 * 100 + "%", PantheraConfig.ShieldBashAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.ShieldBash_damage2 * 100 + "%", PantheraConfig.ShieldBashAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.ShieldBash_damage3 * 100 + "%", PantheraConfig.ShieldBashAbilityID, 3) + Environment.NewLine + Environment.NewLine
                + "Stun duration:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.ShieldBash_stunDuration1 + "s", PantheraConfig.ShieldBashAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.ShieldBash_stunDuration2 + "s", PantheraConfig.ShieldBashAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.ShieldBash_stunDuration3 + "s", PantheraConfig.ShieldBashAbilityID, 3);
            PantheraTokens.Add("SHIELD_BASH_ABILITY_NAME", ShieldBashAbilityName);
            PantheraTokens.Add("SHIELD_BASH_ABILITY_DESC", ShieldBashAbilityDesc);
            #endregion

            #region Kinetic Absorbtion
            string KineticAbsorbtionAbilityName = "Kenetic Absorbion";
            string KineticAbsorbtionAbilityDesc = "You regenerate a percentage of your "
                + ColorHelper.SetShield("Shield")
                + " for every Enemy hit by "
                + ColorHelper.SetUtility(ShieldBashSkillName) + "." + Environment.NewLine + Environment.NewLine
                + "Percent Regenerated:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.KineticAbsorbtion_percent1 * 100 + "%", PantheraConfig.KineticAbsorbtionAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.KineticAbsorbtion_percent2 * 100 + "%", PantheraConfig.KineticAbsorbtionAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.KineticAbsorbtion_percent3 * 100 + "%", PantheraConfig.KineticAbsorbtionAbilityID, 3);
            PantheraTokens.Add("KINEMATIC_ABSORBTION_ABILITY_NAME", KineticAbsorbtionAbilityName);
            PantheraTokens.Add("KINEMATIC_ABSORBTION_ABILITY_DESC", KineticAbsorbtionAbilityDesc);
            #endregion

            #region Shield of Power
            string ShieldOfPowerAbilityName = "Shield of Power";
            string ShieldOfPowerAbilityDesc = "Everytime your "
                + ColorHelper.SetUtility("Shield")
                + " absorb damage, you generate " + Utils.ColorHelper.SetPower("1 God Power") + ".";
            PantheraTokens.Add("SHIELD_OF_POWER_ABILITY_NAME", ShieldOfPowerAbilityName);
            PantheraTokens.Add("SHIELD_OF_POWER_ABILITY_DESC", ShieldOfPowerAbilityDesc);
            #endregion

            #region Strong Barrier
            string StrongBarrierAbilityName = "Stong Barrier";
            string StrongBarrierAbilityDesc = "When P4N7H3R4 is protected by a "
                + ColorHelper.SetHeal("Barrier")
                + ", the damage you take can't be Critical or bigger than a percentage of your max Health." + Environment.NewLine + Environment.NewLine
                + "Health percent:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.StrongBarrier_percent1 * 100 + "%", PantheraConfig.StrongBarrierAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.StrongBarrier_percent2 * 100 + "%", PantheraConfig.StrongBarrierAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.StrongBarrier_percent3 * 100 + "%", PantheraConfig.StrongBarrierAbilityID, 3);
            PantheraTokens.Add("STRONG_BARRIER_ABILITY_NAME", StrongBarrierAbilityName);
            PantheraTokens.Add("STRONG_BARRIER_ABILITY_DESC", StrongBarrierAbilityDesc);
            #endregion

            // ---------- Ruse ---------- //

            #region Ruse 
            string RuseAbilityName = "Ruse";
            string RuseAbilityDesc = "Unlock a smart way to play P4N7H3R4." + Environment.NewLine + Environment.NewLine
                + Utils.ColorHelper.SetGreen("- Unlock " + FuriousBiteSkillName) + Environment.NewLine
                + Utils.ColorHelper.SetGreen("- Critical Chance: ") + Utils.ColorHelper.SetUtility("+" + PantheraConfig.Ruse_addedCrit.ToString()) + Environment.NewLine
                + Utils.ColorHelper.SetGreen("- Level Critical Chance: ") + Utils.ColorHelper.SetUtility("+" + PantheraConfig.Ruse_addedCritLevel.ToString()) + Environment.NewLine + Environment.NewLine
                + Utils.ColorHelper.SetComboPoint("You generate ")
                + Utils.ColorHelper.SetUtility("1 combo")
                + Utils.ColorHelper.SetComboPoint(" Point everytime the third Attack of ")
                + Utils.ColorHelper.SetUtility(RipSkillName)
                + Utils.ColorHelper.SetComboPoint(" do damage.");
            PantheraTokens.Add("RUSE_ABILITY_NAME", RuseAbilityName);
            PantheraTokens.Add("RUSE_ABILITY_DESC", RuseAbilityDesc);
            #endregion

            #region Sharpened Fangs
            string SharpenedFangsAbilityName = "Sharpened Fangs";
            string SharpenedFrangsAbilityDesc = "Incrase the base "
                + ColorHelper.SetDamage("Damage")
                + " and the additional "
                + ColorHelper.SetDamage("Combo Damage")
                + " of "
                + ColorHelper.SetUtility(FuriousBiteSkillName) + ". Decrease the "
                + ColorHelper.SetEnergy("Energy")
                + " used." + Environment.NewLine + Environment.NewLine
                + "Base Damage and Damage added per Combo Point:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.SharpenedFangs_baseDamageMultiplier1 * 100 + "% - " + PantheraConfig.SharpenedFangs_comboDamageMultiplier1 * 100 + "%/CP", PantheraConfig.SharpenedFrangsAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.SharpenedFangs_baseDamageMultiplier2 * 100 + "% - " + PantheraConfig.SharpenedFangs_comboDamageMultiplier2 * 100 + "%/CP", PantheraConfig.SharpenedFrangsAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.SharpenedFangs_baseDamageMultiplier3 * 100 + "% - " + PantheraConfig.SharpenedFangs_comboDamageMultiplier3 * 100 + "%/CP", PantheraConfig.SharpenedFrangsAbilityID, 3) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 4: " + PantheraConfig.SharpenedFangs_baseDamageMultiplier4 * 100 + "% - " + PantheraConfig.SharpenedFangs_comboDamageMultiplier4 * 100 + "%/CP", PantheraConfig.SharpenedFrangsAbilityID, 4) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 5: " + PantheraConfig.SharpenedFangs_baseDamageMultiplier5 * 100 + "% - " + PantheraConfig.SharpenedFangs_comboDamageMultiplier5 * 100 + "%/CP", PantheraConfig.SharpenedFrangsAbilityID, 5) + Environment.NewLine + Environment.NewLine
                + "Energy cost:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.SharpenedFangs_energyCost1 + " Energy", PantheraConfig.SharpenedFrangsAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.SharpenedFangs_energyCost2 + " Energy", PantheraConfig.SharpenedFrangsAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.SharpenedFangs_energyCost3 + " Energy", PantheraConfig.SharpenedFrangsAbilityID, 3) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 4: " + PantheraConfig.SharpenedFangs_energyCost4 + " Energy", PantheraConfig.SharpenedFrangsAbilityID, 4) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 5 : " + PantheraConfig.SharpenedFangs_energyCost5 + " Energy", PantheraConfig.SharpenedFrangsAbilityID, 5);
            PantheraTokens.Add("SHARPENED_FANGS_ABILITY_NAME", SharpenedFangsAbilityName);
            PantheraTokens.Add("SHARPENED_FANGS_ABILITY_DESC", SharpenedFrangsAbilityDesc);
            #endregion

            #region Powerfull Jaws
            string PowerfullJawsAbilityName = "Powerfull Jaws";
            string PowerFullJawsAbilityDesc = "If " + ColorHelper.SetUtility(FuriousBiteSkillName)
                + " do a Critical Strike, the Cooldown is canceled and all the "
                + ColorHelper.SetComboPoint("Compo Points")
                + " are restored." + Environment.NewLine
                + "Can only happen every "
                + Utils.ColorHelper.SetUtility(PantheraConfig.PowerfullJaws_cooldown + "s")
                + ".";
            PantheraTokens.Add("POWERFULL_JAWS_ABILITY_NAME", PowerfullJawsAbilityName);
            PantheraTokens.Add("POWERFULL_JAWS_ABILITY_DESC", PowerFullJawsAbilityDesc);
            #endregion

            #region Predator's Drink
            string PredatorsDrinkAbilityName = "Predator's Drink";
            string PredatorsDrinkDesc = "The Player is Healed for "
                + Utils.ColorHelper.SetHeal(PantheraConfig.PredatorsDrink_basePercent * 100 + "%")
                + " of the max Health when "
                + ColorHelper.SetUtility(FuriousBiteSkillName)
                + " is used. Add additional Heal per Combo Point used." + Environment.NewLine + Environment.NewLine
                + "Percentage per Combo point:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.PredatorsDrink_percent1 * 100 + "%/CP", PantheraConfig.PredatorsDrinkAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.PredatorsDrink_percent2 * 100 + "%/CP", PantheraConfig.PredatorsDrinkAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.PredatorsDrink_percent3 * 100 + "%/CP", PantheraConfig.PredatorsDrinkAbilityID, 3) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 4: " + PantheraConfig.PredatorsDrink_percent4 * 100 + "%/CP", PantheraConfig.PredatorsDrinkAbilityID, 4) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 5: " + PantheraConfig.PredatorsDrink_percent5 * 100 + "%/CP", PantheraConfig.PredatorsDrinkAbilityID, 5);
            PantheraTokens.Add("PREDATORS_DRINK_ABILITY_NAME", PredatorsDrinkAbilityName);
            PantheraTokens.Add("PREDATORS_DRINK_ABILITY_DESC", PredatorsDrinkDesc);
            #endregion

            #region Shadow's Master
            string ShadowsMasterAbilityName = "Shadow's Master";
            string ShadowsMasterAbilityDesc = "Reduce the Cooldown of "
                + ColorHelper.SetUtility(ProwlSkillName)
                + ", also reduce the Cooldown when you leave the Combat." + Environment.NewLine + Environment.NewLine
                + "Cooldown reduction:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.ShadowsMaster_cooldown1 + "s", PantheraConfig.ShadowsMasterAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.ShadowsMaster_cooldown2 + "s", PantheraConfig.ShadowsMasterAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.ShadowsMaster_cooldown3 + "s", PantheraConfig.ShadowsMasterAbilityID, 3) + Environment.NewLine + Environment.NewLine
                + "Leaving combat cooldown reduced to:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.ShadowsMaster_cooldownReduction1 + "s", PantheraConfig.ShadowsMasterAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.ShadowsMaster_cooldownReduction2 + "s", PantheraConfig.ShadowsMasterAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.ShadowsMaster_cooldownReduction3 + "s", PantheraConfig.ShadowsMasterAbilityID, 3);
            PantheraTokens.Add("SHADOWS_MASTER_ABILITY_NAME", ShadowsMasterAbilityName);
            PantheraTokens.Add("SHADOWS_MASTER_ABILITY_DESC", ShadowsMasterAbilityDesc);
            #endregion

            #region Silent Predator
            string SilentPredatorName = "Silent Predator";
            string SilentPredatorAbilityDesc = "Reduces the Speed Penalty of "
                + ColorHelper.SetUtility(ProwlSkillName)
                + ", allows to Run at level 3." + Environment.NewLine + Environment.NewLine
                + "Penalty reduction:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.SilentPredator_reduction1 * 100 + "%", PantheraConfig.SilentPredatorAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.SilentPredator_reduction2 * 100 + "%", PantheraConfig.SilentPredatorAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.SilentPredator_reduction3 * 100 + "% + run allowed", PantheraConfig.SilentPredatorAbilityID, 3);
            PantheraTokens.Add("SILENT_PREDATOR_ABILITY_NAME", SilentPredatorName);
            PantheraTokens.Add("SILENT_PREDATOR_ABILITY_DESC", SilentPredatorAbilityDesc);
            #endregion

            #region Primal Stalker
            string PrimalStalkerAbilityName = "Primal Stalker";
            string PrimalStalkerAbilityDesc = "After activating "
                + ColorHelper.SetUtility(ProwlSkillName)
                + ", you stay Stealthy for a fixed amount of time even if you take Damage. Doing Damage doesn't remove "
                + ColorHelper.SetUtility(ProwlSkillName)
                + " immediately." + Environment.NewLine + Environment.NewLine
                + "Fixed Prowl time:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.PrimalStalker_fixedTime1 + "s", PantheraConfig.PrimalStalkerAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.PrimalStalker_fixedTime2 + "s", PantheraConfig.PrimalStalkerAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.PrimalStalker_fixedTime3 + "s", PantheraConfig.PrimalStalkerAbilityID, 3);
            PantheraTokens.Add("PRIMAL_STALKER_ABILITY_NAME", PrimalStalkerAbilityName);
            PantheraTokens.Add("PRIMAL_STALKER_ABILITY_DESC", PrimalStalkerAbilityDesc);
            #endregion

            #region Perspicacity
            string PerspicacityAbilityName = "Perspicacity";
            string PerspicacityAbilityDesc = ColorHelper.SetUtility(AirCleaveSkillName)
                + " has a change to generate " + Utils.ColorHelper.SetComboPoint("1 Combo Point") + " if a target was hit." + Environment.NewLine + Environment.NewLine
                + "Activation chance:" + Environment.NewLine
            + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.Perspicacity_percent1 * 100 + "%", PantheraConfig.PerspicacityAbilityID, 1) + Environment.NewLine
            + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.Perspicacity_percent2 * 100 + "%", PantheraConfig.PerspicacityAbilityID, 2) + Environment.NewLine
            + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.Perspicacity_percent3 * 100 + "%", PantheraConfig.PerspicacityAbilityID, 3);
            PantheraTokens.Add("PERSPICACITY_ABILITY_NAME", PerspicacityAbilityName);
            PantheraTokens.Add("PERSPICACITY_ABILITY_DESC", PerspicacityAbilityDesc);
            #endregion

            #region Ghost Rip
            string GhostRipAbilityName = "Ghost Rip";
            string GhostRipAbilityDesc = ColorHelper.SetUtility("Stun")
                + " the Targets and improve the Critical Chance of "
                + ColorHelper.SetUtility(RipSkillName)
                + " when used while "
                + ColorHelper.SetUtility(ProwlSkillName)
                + " is active." + Environment.NewLine + Environment.NewLine
                + "Critical chance added:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.GhostRip_critPercent1 + "%", PantheraConfig.GhostRipAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.GhostRip_critPercent2 + "%", PantheraConfig.GhostRipAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.GhostRip_critPercent3 + "%", PantheraConfig.GhostRipAbilityID, 3) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 4: " + PantheraConfig.GhostRip_critPercent4 + "%", PantheraConfig.GhostRipAbilityID, 4) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 5: " + PantheraConfig.GhostRip_critPercent5 + "%", PantheraConfig.GhostRipAbilityID, 5) + Environment.NewLine
                + "Stun duration:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.GhostRip_stunDuration1 + "s", PantheraConfig.GhostRipAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.GhostRip_stunDuration2 + "s", PantheraConfig.GhostRipAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.GhostRip_stunDuration3 + "s", PantheraConfig.GhostRipAbilityID, 3) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 4: " + PantheraConfig.GhostRip_stunDuration4 + "s", PantheraConfig.GhostRipAbilityID, 4) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 5: " + PantheraConfig.GhostRip_stunDuration5 + "s", PantheraConfig.GhostRipAbilityID, 5);
            PantheraTokens.Add("GHOST_RIP_ABILITY_NAME", GhostRipAbilityName);
            PantheraTokens.Add("GHOST_RIP_ABILITY_DESC", GhostRipAbilityDesc);
            #endregion

            #region Echo
            string EchoAbilityName = "Echo";
            string EchoAbilityDesc = "Improve the "
                + ColorHelper.SetUtility(MightyRoarSkillName)
                + " Radius and Stun duration and decrease the Cooldown." + Environment.NewLine + Environment.NewLine
                + "Cooldown:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.Echo_cooldown1 + "s", PantheraConfig.EchoAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.Echo_cooldown2 + "s", PantheraConfig.EchoAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.Echo_cooldown3 + "s", PantheraConfig.EchoAbilityID, 3) + Environment.NewLine + Environment.NewLine
                + "Radius:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.Echo_radius1 + "m", PantheraConfig.EchoAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.Echo_radius2 + "m", PantheraConfig.EchoAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.Echo_radius3 + "m", PantheraConfig.EchoAbilityID, 3) + Environment.NewLine + Environment.NewLine
                + "Stun Duration:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.Echo_stunDuration1 + "s", PantheraConfig.EchoAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.Echo_stunDuration2 + "s", PantheraConfig.EchoAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.Echo_stunDuration3 + "s", PantheraConfig.EchoAbilityID, 3);
            PantheraTokens.Add("ECHO_ABILITY_NAME", EchoAbilityName);
            PantheraTokens.Add("ECHO_ABILITY_DESC", EchoAbilityDesc);
            #endregion

            #region Piercing Waves
            string PiercingWavesAbilityName = "Piercing Waves";
            string PiercingWavesAbilityDesc = ColorHelper.SetUtility(MightyRoarSkillName)
                + " deals "
                + ColorHelper.SetDamage("Bleed Damage")
                + " to all affected Enemies." + Environment.NewLine + Environment.NewLine
                + "Bleed duration:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.PiercingWaves_bleedDuration1 + "s", PantheraConfig.PiercingWavesAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.PiercingWaves_bleedDuration2 + "s", PantheraConfig.PiercingWavesAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.PiercingWaves_bleedDuration3 + "s", PantheraConfig.PiercingWavesAbilityID, 3) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 4: " + PantheraConfig.PiercingWaves_bleedDuration4 + "s", PantheraConfig.PiercingWavesAbilityID, 4) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 5: " + PantheraConfig.PiercingWaves_bleedDuration5 + "s", PantheraConfig.PiercingWavesAbilityID, 5) + Environment.NewLine + Environment.NewLine
                + "Damage:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.PiercingWaves_bleedDamage1 * 100 + "%", PantheraConfig.PiercingWavesAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.PiercingWaves_bleedDamage2 * 100 + "%", PantheraConfig.PiercingWavesAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.PiercingWaves_bleedDamage3 * 100 + "%", PantheraConfig.PiercingWavesAbilityID, 3) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 4: " + PantheraConfig.PiercingWaves_bleedDamage4 * 100 + "%", PantheraConfig.PiercingWavesAbilityID, 4) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 5: " + PantheraConfig.PiercingWaves_bleedDamage5 * 100 + "%", PantheraConfig.PiercingWavesAbilityID, 5);
            PantheraTokens.Add("PIERCING_WAVES_ABILITY_NAME", PiercingWavesAbilityName);
            PantheraTokens.Add("PIERCING_WAVES_ABILITY_DESC", PiercingWavesAbilityDesc);
            #endregion

            // ---------- Utility ---------- //

            #region Leap 
            string LeapAbilityName = "Predator Leap";
            string LeapAbilityDesc = Utils.ColorHelper.SetBlue("Activated by double pressing the Jump Key.") + Environment.NewLine
                + "Jump forward or to the focused target. Can be used while in air." + Environment.NewLine + Environment.NewLine
                + "Skill Cooldown:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.Leap_cooldown1 + "s", PantheraConfig.LeapAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.Leap_cooldown2 + "s", PantheraConfig.LeapAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.Leap_cooldown3 + "s", PantheraConfig.LeapAbilityID, 3) + Environment.NewLine + Environment.NewLine
                + "Target hit Cooldown reduction:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: -" + PantheraConfig.Leap_targetReduction1 + "s", PantheraConfig.LeapAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: -" + PantheraConfig.Leap_targetReduction2 + "s", PantheraConfig.LeapAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: -" + PantheraConfig.Leap_targetReduction3 + "s", PantheraConfig.LeapAbilityID, 3);
            PantheraTokens.Add("LEAP_ABILITY_NAME", LeapAbilityName);
            PantheraTokens.Add("LEAP_ABILITY_DESC", LeapAbilityDesc);
            #endregion

            #region Dash
            string DashAbilityName = "Dash";
            string DashAbilityDesc = Utils.ColorHelper.SetBlue("Activated by pressing Run again when Running.") + Environment.NewLine
                + "Greatly improve your " 
                + ColorHelper.SetUtility("Move Speed")
                + " but consume your " 
                + ColorHelper.SetStamina("Stamina")
                + ". Press again to stop the Dash." + Environment.NewLine + Environment.NewLine
                + "Stamina Consumed:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.Dash_staminaConsumed1 + "/s", PantheraConfig.DashAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.Dash_staminaConsumed2 + "/s", PantheraConfig.DashAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.Dash_staminaConsumed3 + "/s", PantheraConfig.DashAbilityID, 3) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 4: " + PantheraConfig.Dash_staminaConsumed4 + "/s", PantheraConfig.DashAbilityID, 4) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 5: " + PantheraConfig.Dash_staminaConsumed5 + "/s", PantheraConfig.DashAbilityID, 5) + Environment.NewLine + Environment.NewLine
                + "Added speed:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: +" + PantheraConfig.Dash_speedMultiplier1 * 100 + "%", PantheraConfig.DashAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: +" + PantheraConfig.Dash_speedMultiplier2 * 100 + "%", PantheraConfig.DashAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: +" + PantheraConfig.Dash_speedMultiplier3 * 100 + "%", PantheraConfig.DashAbilityID, 3) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 4: +" + PantheraConfig.Dash_speedMultiplier4 * 100 + "%", PantheraConfig.DashAbilityID, 4) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 5: +" + PantheraConfig.Dash_speedMultiplier5 * 100 + "%", PantheraConfig.DashAbilityID, 5);
            PantheraTokens.Add("DASH_ABILITY_NAME", DashAbilityName);
            PantheraTokens.Add("DASH_ABILITY_DESC", DashAbilityDesc);
            #endregion

            #region Zone Heal
            string ZoneHealAbilityName = "Zone Heal";
            string ZoneHealAbilityDesc = Utils.ColorHelper.SetBlue("Can only be used while on the ground.") + Environment.NewLine
                + "Place a Heal Circle on the ground that "
                + ColorHelper.SetHeal("Heal")
                + " all Players standing on." + Environment.NewLine + Environment.NewLine
                + "Heal percent:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.ZoneHeal_percentHeal1 * 100 + "%", PantheraConfig.ZoneHealAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.ZoneHeal_percentHeal2 * 100 + "%", PantheraConfig.ZoneHealAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.ZoneHeal_percentHeal3 * 100 + "%", PantheraConfig.ZoneHealAbilityID, 3) + Environment.NewLine + Environment.NewLine
                + "Skill Cooldown:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.ZoneHeal_cooldown1 + "s", PantheraConfig.ZoneHealAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.ZoneHeal_cooldown2 + "s", PantheraConfig.ZoneHealAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.ZoneHeal_cooldown3 + "s", PantheraConfig.ZoneHealAbilityID, 3);
            PantheraTokens.Add("ZONE_HEAL_ABILITY_NAME", ZoneHealAbilityName);
            PantheraTokens.Add("ZONE_HEAL_ABILITY_DESC", ZoneHealAbilityDesc);
            #endregion

            #region Wind Walker
            string WindWalkerAbilityName = "Wind Walker";
            string WindWalkerAbilityDesc = Utils.ColorHelper.SetBlue("Activated by pressing Jump while in air.") + Environment.NewLine
                + "Allows to do a "
                + ColorHelper.SetUtility("double Jump");
            PantheraTokens.Add("WIND_WALKER_ABILITY_NAME", WindWalkerAbilityName);
            PantheraTokens.Add("WIND_WALKER_ABILITY_DESC", WindWalkerAbilityDesc);
            #endregion

            #region Save My Friend!
            string SaveMyFriendAbilityName = "Save My Friend!";
            string SaveMyFriendAbilityDesc = Utils.ColorHelper.SetBlue("You can switch the Tracker to Allies by keep pressing the Interact Button.") + Environment.NewLine
                + Utils.ColorHelper.SetBlue("Activated by double pressing the Jump Key.") + Environment.NewLine
                + "Leap to an Ally and run with it regenerating its Health and nullifying all Damage." + Environment.NewLine
                + "You keep the Ally for " + Utils.ColorHelper.SetUtility(PantheraConfig.SaveMyFriend_compMaxDuration + "s")
                + " and Heal it every " + Utils.ColorHelper.SetUtility(PantheraConfig.SaveMyFriend_healInterval.ToString()) + " second for "
                + Utils.ColorHelper.SetHeal(PantheraConfig.SaveMyFriend_healPercent * 100 + "%") + " of P4N7H3R4 max Health.";
            PantheraTokens.Add("SAVE_MY_FRIEND_ABILITY_NAME", SaveMyFriendAbilityName);
            PantheraTokens.Add("SAVE_MY_FRIEND_ABILITY_DESC", SaveMyFriendAbilityDesc);
            #endregion

            #region Passive Power
            string PassivePowerAbilityName = "Passive Power";
            string PassivePowerAbilityDesc = "Every " + Utils.ColorHelper.SetUtility(PantheraConfig.PassivePower_cooldown + "s")
                + ", you generate " + Utils.ColorHelper.SetPower("1 God Power") + " Point.";
            PantheraTokens.Add("PASSIVE_POWER_ABILITY_NAME", PassivePowerAbilityName);
            PantheraTokens.Add("PASSIVE_POWER_ABILITY_DESC", PassivePowerAbilityDesc);
            #endregion

            #region Prescience
            string PrescienceAbilityName = "Prescience";
            string PrescienceAbilityDesc = ColorHelper.SetUtility(DetectionSkillName)
                + " also activate the prescience of P4N7H3R4, adds "
                + ColorHelper.SetUtility("Chests /Barrels/Shrines/...")
                + " to the "
                + ColorHelper.SetUtility("Predatory Mode") + ".";
            PantheraTokens.Add("PRESCIENCE_ABILITY_NAME", PrescienceAbilityName);
            PantheraTokens.Add("PRESCIENCE_ABILITY_DESC", PrescienceAbilityDesc);
            #endregion

            #region Concentration
            string ConcentrationAbilityName = "Concentration";
            string ConcentrationAbilityDesc = "Add Colors to the "
                + ColorHelper.SetUtility("Predatory Mode") 
                + " related to the "
                + ColorHelper.SetUtility("Enemies/Players")
                + " Health or the "
                + ColorHelper.SetUtility("Chests/Barrels/Shrines/...") + " state (Close/Opened/...).";
            PantheraTokens.Add("CONCENTRATION_ABILITY_NAME", ConcentrationAbilityName);
            PantheraTokens.Add("CONCENTRATION_ABILITY_DESC", ConcentrationAbilityDesc);
            #endregion

            #region Precision
            string PrecisionAbilityName = "Precision";
            string PrecisionAbilityDesc = "While in "
                + ColorHelper.SetUtility("Predatory Mode")
                + ", P4N7H3R4 can target Enemies weak points, increase the Crilical Strike."
                + Environment.NewLine + Environment.NewLine
                + "Critical Strike Added:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.Precision_percent1 * 100 + "%", PantheraConfig.PrecisionAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.Precision_percent2 * 100 + "%", PantheraConfig.PrecisionAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.Precision_percent3 * 100 + "%", PantheraConfig.PrecisionAbilityID, 3);
            PantheraTokens.Add("PRECISION_ABILITY_NAME", PrecisionAbilityName);
            PantheraTokens.Add("PRECISION_ABILITY_DESC", PrecisionAbilityDesc);
            #endregion

            #region Determination
            string DeterminationAbilityName = "Concentration";
            string DeterminationAbilityDesc = "Decrease the "
                + ColorHelper.SetStamina("Stamina")
                + " consumed by "
                + ColorHelper.SetUtility(DetectionSkillName) + "." + Environment.NewLine + Environment.NewLine
                + "Stamina Consumed:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.Determination_comsumed1 + " per second", PantheraConfig.DeterminationAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.Determination_comsumed2 + " per second", PantheraConfig.DeterminationAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.Determination_comsumed3 + " per second", PantheraConfig.DeterminationAbilityID, 3);
            PantheraTokens.Add("DETERMINATION_ABILITY_NAME", DeterminationAbilityName);
            PantheraTokens.Add("DETERMINATION_ABILITY_DESC", DeterminationAbilityDesc);
            #endregion

            #region Regeneration
            string RegenerationAbilityName = "Regeneration";
            string RegenerationAbilityDesc = "You regenerate a percentage of your "
                + ColorHelper.SetHeal("Health") + " every "
                + ColorHelper.SetUtility(PantheraConfig.Regeneration_cooldown + "s") + "."
                + Environment.NewLine + Environment.NewLine
                + "Health regenerated:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.Regeneration_percent1 * 100 + "%", PantheraConfig.RegenerationAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.Regeneration_percent2 * 100 + "%", PantheraConfig.RegenerationAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.Regeneration_percent3 * 100 + "%", PantheraConfig.RegenerationAbilityID, 3);
            PantheraTokens.Add("REGENERATION_ABILITY_NAME", RegenerationAbilityName);
            PantheraTokens.Add("REGENERATION_ABILITY_DESC", RegenerationAbilityDesc);
            #endregion

            #region Healing Cleave
            string HealingCleaveAbilityName = "Healing Cleave";
            string HealingCleaveAbilityDesc = "If an "
                + ColorHelper.SetUtility(AirCleaveSkillName)
                + " projectile hits an Ally, it is "
                + ColorHelper.SetHeal("Healed")
                + " for a percentage of P4N7H3R4's Health." + Environment.NewLine + Environment.NewLine
                + "Heal percent:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.HealingCleave_percent1 * 100 + "%", PantheraConfig.HealingCleaveAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.HealingCleave_percent2 * 100 + "%", PantheraConfig.HealingCleaveAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.HealingCleave_percent3 * 100 + "%", PantheraConfig.HealingCleaveAbilityID, 3);
            PantheraTokens.Add("HEALING_CLEAVE_ABILITY_NAME", HealingCleaveAbilityName);
            PantheraTokens.Add("HEALING_CLEAVE_ABILITY_DESC", HealingCleaveAbilityDesc);
            #endregion

        }

    }
}
