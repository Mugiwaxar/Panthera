using Panthera.Skills;
using R2API;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Utils
{
    class Tokens
    {

        //public static string passiveColor = "#216BFF";
        //public static string attackColor = "#B900DD";


        public static string CharacterName;
        public static string CharacterSubtitle;
        public static string CharacterDesc;

        #region Skills
        public static string RipSkillName;
        public static string RipSkillDesc;
        public static string AirCleaveSkillName;
        public static string AirCleaveSkillDesc;
        public static string LeapSkillName;
        public static string LeapSkillDesc;
        public static string MightyRoarSkillName;
        public static string MightyRoarSkillDesc;
        public static string ClawStormSkillName;
        public static string ClawStormSkillDesc;
        public static string FrontShieldName;
        public static string FrontShieldDesc;
        public static string ProwlSkillName;
        public static string ProwlSkillDesc;
        public static string FuriousBiteSkillName;
        public static string FuriousBiteSkillDesc;
        #endregion

        #region Abilities
        public static string DestructionAbilityName;
        public static string DestructionAbilityDesc;
        public static string GuardianAbilityName;
        public static string GuardianAbilityDesc;
        public static string RuseAbilityName;
        public static string RuseAbilityDesc;
        public static string ImprovedLeapAbilityName;
        public static string ImprovedLeapAbilityDesc;
        #endregion

        public static void RegisterTokens()
        {

            // Passive KeyWords CharacterDesc
            //LanguageAPI.Add("KEYWORD_FASTREFLEX", "<style=cKeywordName><color=" + passiveColor + ">Fast Reflex</color></style><style=cSub>Use this skill allows you to jump again. Reaching a target reduce the cooldown of this skill by <style=cIsUtility>" + PantheraConfig.Leap_coolDownReduction + "s.</style></style>");
            //LanguageAPI.Add("KEYWORD_COMBO", "<style=cKeywordName><color=" + passiveColor + ">Combo</color></style><style=cSub>Keep pressing the button to chain the attacks.</style>");
            //LanguageAPI.Add("KEYWORD_PTRADASH", "<style=cKeywordName><color=" + passiveColor + ">Rush</color></style><style=cSub>Rushes towards nearby creatures when attacking. Allows to jump again.</style>");
            //LanguageAPI.Add("KEYWORD_TASTOFBLOOD", "<style=cKeywordName><color=" + passiveColor + ">Tast of blood</color></style><style=cSub>Using <style=cIsUtility>Furious Bite</style> or <style=cIsUtility>Mangle</style> add <style=cIsUtility>" + FuriousBite.stockAdded + "</style> stacks of recharge to this skill (max " + FuriousBite.maxStockAdded + ").</style>");
            //LanguageAPI.Add("KEYWORD_INVULNERABLE", "<style=cKeywordName><color=" + passiveColor + ">Invulnerable</color></style><style=cSub>While using this skill, you are immun to all damage.</style>");
            //LanguageAPI.Add("KEYWORD_LOADED", "<style=cKeywordName><color=" + passiveColor + ">Loaded</color></style><style=cSub>This skill last until you release the button.</style>");
            //LanguageAPI.Add("KEYWORD_STUNNINGPRESENCE", "<style=cKeywordName><color=" + passiveColor + ">Stunning Presence</color></style><style=cSub>This skill inflict <style=cIsUtility>weekeness</style> to your enemies.</style>");

            // Attack KeyWords //
            //LanguageAPI.Add("KEYWORD_POWEREDCLAWS", "<style=cKeywordName><color=" + attackColor + ">Powered Claws</color></style><style=cSub>Press the <style=cIsUtility>Sprint</style> button while using this skill send charged projectiles for <style=cIsDamage>" + PantheraConfig.Rip_atk1DamageMultiplier * 100 + "%</style>, <style=cIsDamage>" + PantheraConfig.Rip_atk2DamageMultiplier * 100 + "%</style>, <style=cIsDamage>" + PantheraConfig.Rip_atk3DamageMultiplier * 100 + "%</style> damage (use 1 stock).</style>");
            //LanguageAPI.Add("KEYWORD_MIGHTYROAR", "<style=cKeywordName><color=" + attackColor + ">Mighty Roar</color></style><style=cSub>Press the <style=cIsUtility>Utility</style> button while using this skill to stun all enemies within <style=cIsUtility>" + PantheraConfig.MightyRoar_distance + "m</style> around you (use  stock).</style>");
            //LanguageAPI.Add("KEYWORD_FURIOUSBITE", "<style=cKeywordName><color=" + attackColor + ">Furious Bite</color></style><style=cSub>Pressing the <style=cIsUtility>Primary Attack</style> button at the end of the jump bite the target and inflict <style=cIsDamage>" + PantheraConfig.FuriousBite_atkDamageMultiplier * 100 + "%</style> damage.</style>");
            //LanguageAPI.Add("KEYWORD_MANGLE", "<style=cKeywordName><color=" + attackColor + ">Mangle</color></style><style=cSub>Pressing the <style=cIsUtility>Secondary Attack</style> button at the end of the jump mangle the target and apply all Blood Stack as <style=cIsDamage>Super Bleeding</style> damage.</style>");
            //LanguageAPI.Add("KEYWORD_LEAPCERCLE", "<style=cKeywordName><color=" + attackColor + ">Leap Cercle</color></style><style=cSub>Press <style=cIsUtility>Interact</style> while using this skill to leap back to the posed cercle. Give <style=cIsUtility>Regeneration</style> for <style=cIsHealing>" + PantheraConfig.LeapCercle_regenDuration + "s</style>.</style>");
            //LanguageAPI.Add("KEYWORD_RAYSLASH", "<style=cKeywordName><color=" + attackColor + ">Ray Slash</color></style><style=cSub>Keep pressing the <style=cIsUtility>Primary Attack</style> button at the end of the charge to release a massive amount of energy (Consume <style=cIsUtility>" + PantheraConfig.RaySlash_buffRequiredToFire + " Energy Stacks</style>).</style>");

            // Character //
            CharacterName = "P4N7H3R4";
            CharacterSubtitle = "Play like a predator!";
            CharacterDesc = Utils.ColorHelper.SetCyan("Don't be the prey, be the predator!") + Environment.NewLine
                + "P4N7H3R4 (Panthera) is a powerful Panther who belongs to the Wild Gods family. Unlike humans, Big Cats are naturally equipped to hunt and kill, this makes P4N7H3R4 a very strong predator.";
            LanguageAPI.Add("PantheraName", CharacterName);
            LanguageAPI.Add("PantheraDescription", CharacterDesc);
            LanguageAPI.Add("PantheraSubtitle", CharacterSubtitle);

            // Passive skill //
            //desc = "<style=cKeywordName><color=" + passiveColor + ">The great Panther</color></style><style=cSub>Can jump twice. Immune to fall damage. ";
            //desc = desc + "<style=cIsHealing>" + PantheraConfig.Passive_lifeStealMultiplier * 100 + "%</style> of melee damage coverted to heal. <style=cIsHealing>" + PantheraConfig.Passive_barriereMultiplier * 100 + "%</style> of damage taken added as <style=cIsHealing>barrier</style>. SuperSprint.";
            //desc = desc + "Protected by a <style=cIsHealing>barrier</style> instead of dying (<style=cIsUtility>" + PantheraConfig.Passive_nineLivesCooldown + "s</style> CD).</style>" + Environment.NewLine + Environment.NewLine;
            //desc = desc + "<style=cKeywordName><color=" + passiveColor + ">Prowler</color></style><style=cSub>Prowl when not in combat. The first melee attack done inflict <style=cIsDamage>" + PantheraConfig.Passive_stealDamageMultiplier*100 + "%</style> damage and stun for <style=cIsUtility>" + PantheraConfig.Passive_stealStunDuration + "s</style>.</style>" + Environment.NewLine + Environment.NewLine;
            //desc = desc + "[ <color=" + passiveColor + ">Blood Stack</color> ]   " + Environment.NewLine;
            //desc = desc + "Every critical strike add a <style=cIsUtility>Blood Stack</style> (max <style=cIsUtility>" + PantheraConfig.Passive_maxMangleStack + "</style>)." + Environment.NewLine + Environment.NewLine;
            //LanguageAPI.Add("PANTHERA_PASSIVE_NAME", "Feline skills");
            //LanguageAPI.Add("PANTHERA_PASSIVE_DESCRIPTION", "P4N7H3R4 has a lot of skills for mobility, damage and survival. See <color=#30F9FF>Overview</color> tab for more information.");
            //LanguageAPI.Add("KEYWORD_PTRPASSIVE", desc);

            // Rip Skill //
            RipSkillName = "Rip and Shred";
            RipSkillDesc = "Rip and Shred all targets in front of you 3 times for "
                + Utils.ColorHelper.SetDamage((PantheraConfig.Rip_atk1DamageMultiplier * 100).ToString() + "%") + ", "
                + Utils.ColorHelper.SetDamage((PantheraConfig.Rip_atk2DamageMultiplier * 100).ToString() + "%") + ", "
                + Utils.ColorHelper.SetDamage((PantheraConfig.Rip_atk2DamageMultiplier * 100).ToString() + "%") + " damage."
                + Environment.NewLine
                + "Keep pressing to use continuously.";
            ;
            LanguageAPI.Add("PANTHERA_RIP_NAME", RipSkillName);
            LanguageAPI.Add("PANTHERA_RIP_DESCRIPTION", RipSkillDesc);

            // AirCLeave Skill //
            AirCleaveSkillName = "Air Cleave";
            AirCleaveSkillDesc = "Cleave the air and send a projectile that deal "
                + Utils.ColorHelper.SetDamage((PantheraConfig.AirCleave_atk1DamageMultiplier * 100).ToString() + "%")
                + " damage to all enemies on the way."
                ;
            LanguageAPI.Add("PANTHERA_AIRCLEAVE_NAME", AirCleaveSkillName);
            LanguageAPI.Add("PANTHERA_AIRCLEAVE_DESCRIPTION", AirCleaveSkillDesc);

            // Leap Skill //
            LeapSkillName = "Predator Leap";
            LeapSkillDesc = "Jump forward or to the focused target. Can be used while in air.";
            LanguageAPI.Add("PANTHERA_LEAP_NAME", LeapSkillName);
            LanguageAPI.Add("PANTHERA_LEAP_DESCRIPTION", LeapSkillDesc);

            // Mighty Roar Skill //
            MightyRoarSkillName = "Mighty Roar";
            MightyRoarSkillDesc = "Do a ferocious mighty roar that stun all targets within "
                + Utils.ColorHelper.SetUtility(PantheraConfig.MightyRoar_distance.ToString())
                + " for "
                + Utils.ColorHelper.SetUtility(PantheraConfig.MightyRoar_duration.ToString() + "s")
                + "."
                ;
            LanguageAPI.Add("PANTHERA_MIGHTY_ROAR_NAME", MightyRoarSkillName);
            LanguageAPI.Add("PANTHERA_MIGHTY_ROAR_DESCRIPTION", MightyRoarSkillDesc);

            // ClawStorm //
            ClawStormSkillName = "Claws Storm";
            ClawStormSkillDesc = "Dash in one direction with a storm of slash and inflict "
                + Utils.ColorHelper.SetDamage((PantheraConfig.ClawsStorm_damageMultiplier * 100).ToString() + "%")
                + " damages every "
                + Utils.ColorHelper.SetUtility(PantheraConfig.ClawsStorm_firedDelay.ToString() + "s")
                + "." + Environment.NewLine
                + "Keep pressing to use continuously for "
                + Utils.ColorHelper.SetFury(PantheraConfig.ClawsStorm_continuousConsumedFury.ToString())
                + " Fury point every "
                + Utils.ColorHelper.SetUtility(PantheraConfig.ClawsStorm_continuousConsumeTime.ToString() + "s")
                + "." + Environment.NewLine
                + "Require "
                + Utils.ColorHelper.SetFury(PantheraConfig.ClawsStorm_requiredFury.ToString())
                + " Fury points to activate";
            LanguageAPI.Add("PANTHERA_SKILL_2_NAME", ClawStormSkillName);
            LanguageAPI.Add("PANTHERA_SKILL_2_DESCRIPTION", ClawStormSkillDesc);

            // Shield //
            FrontShieldName = "Shield";
            FrontShieldDesc = "Create a Shield in front of you that absorb incoming damages. Stay pressed to keep the Shield activated.";

            // Prowl //
            ProwlSkillName = "Prowl";
            ProwlSkillDesc = "You become invisible to all Enemies. Canceled if you do or take damage";

            //Furious Bite //
            FuriousBiteSkillName = "Furious Bite";
            FuriousBiteSkillDesc = "Bite the target and inflict "
                + Utils.ColorHelper.SetDamage((PantheraConfig.FuriousBite_atkDamageMultiplier * 100).ToString() + "%")
                + " damage." + Environment.NewLine
                + "Consume all "
                + Utils.ColorHelper.SetCP("Combo Points") + "." + Environment.NewLine
                + "For every "
                + Utils.ColorHelper.SetCP("Combo Point") + Environment.NewLine
                + "consumed, the damage are increased by "
                + Utils.ColorHelper.SetDamage((PantheraConfig.FuriousBite_ComboPointMultiplier * 100).ToString() + "%") + ".";


            // Destruction Ability //
            DestructionAbilityName = "Destruction";
            DestructionAbilityDesc = "Improve the agressive abilities of P4N7H3R4." + Environment.NewLine
                + Utils.ColorHelper.SetGreen("Unlock: " + ClawStormSkillName) + Environment.NewLine
                + Utils.ColorHelper.SetGreen("Damage +" + PantheraConfig.Destruction_addedDamage.ToString()) + Environment.NewLine
                + Utils.ColorHelper.SetGreen("Level Damage +" + PantheraConfig.Destruction_addedDamageLevel.ToString()) + Environment.NewLine
                + Utils.ColorHelper.SetGreen("Attack Speed +" + PantheraConfig.Destruction_addedAttackSpeed.ToString()) + Environment.NewLine
                + Utils.ColorHelper.SetGreen("Level Attack Speed +" + PantheraConfig.Destruction_addedAttackSpeedLevel.ToString()) + Environment.NewLine
                + Utils.ColorHelper.SetFury("Fury:") + Environment.NewLine
                + Utils.ColorHelper.SetFury("You generate ")
                + Utils.ColorHelper.SetUtility(PantheraConfig.Fury_pointsGenerated.ToString())
                + Utils.ColorHelper.SetFury(" Fury point every ")
                + Utils.ColorHelper.SetUtility(PantheraConfig.Fury_pointsGenerationCooldown.ToString())
                + Utils.ColorHelper.SetUtility("s")
                + Utils.ColorHelper.SetFury(" while in combat.");

            // Guardian Ability //
            GuardianAbilityName = "Guardian";
            GuardianAbilityDesc = "Greatly increase the defensive abilities of P4N7H3R4." + Environment.NewLine
                + Utils.ColorHelper.SetGreen("Unlock: " + FrontShieldName) + Environment.NewLine
                + Utils.ColorHelper.SetGreen("Health +" + PantheraConfig.Guardian_addedHealth.ToString()) + Environment.NewLine
                + Utils.ColorHelper.SetGreen("Level Health +" + PantheraConfig.Guardian_addedHealthLevel.ToString()) + Environment.NewLine
                + Utils.ColorHelper.SetGreen("Health Regen +" + PantheraConfig.Guardian_addedHealthRegen.ToString()) + Environment.NewLine
                + Utils.ColorHelper.SetGreen("Level Health Regen +" + PantheraConfig.Guardian_addedHealthRegenLevel.ToString()) + Environment.NewLine
                + Utils.ColorHelper.SetGreen("Defense +" + PantheraConfig.Guardian_addedDefense.ToString()) + Environment.NewLine
                + Utils.ColorHelper.SetGreen("Level Defense +" + PantheraConfig.Guardian_addedDefenseLevel.ToString()) + Environment.NewLine;

            // Ruse Ability //
            RuseAbilityName = "Ruse";
            RuseAbilityDesc = "Unlock a smart way to play P4N7H3R4" + Environment.NewLine
                + Utils.ColorHelper.SetGreen("Unlock: " + ProwlSkillName) + Environment.NewLine
                + Utils.ColorHelper.SetFury("You generate ")
                + Utils.ColorHelper.SetUtility("1")
                + Utils.ColorHelper.SetFury(" combo point everytime the third attack of ")
                + Utils.ColorHelper.SetUtility(RipSkillName)
                + Utils.ColorHelper.SetFury(" do damage.");

            // Improved Leap Ability //
            ImprovedLeapAbilityName = "Improved Leap";
            ImprovedLeapAbilityDesc = "Decrease the " + Language.GetString("PANTHERA_LEAP_NAME") + " cooldown, also decrease the cooldown when a target was hit." + Environment.NewLine + Environment.NewLine
                + "Cooldown reduction:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.ImprovedLeap_cooldownReduction1 + "s", PantheraConfig.ImprovedLeapAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.ImprovedLeap_cooldownReduction2 + "s", PantheraConfig.ImprovedLeapAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.ImprovedLeap_cooldownReduction3 + "s", PantheraConfig.ImprovedLeapAbilityID, 3) + Environment.NewLine + Environment.NewLine
                + "Target hit Cooldown reduction:" + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 1: " + PantheraConfig.ImprovedLeap_targetReduction1 + "s", PantheraConfig.ImprovedLeapAbilityID, 1) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 2: " + PantheraConfig.ImprovedLeap_targetReduction2 + "s", PantheraConfig.ImprovedLeapAbilityID, 2) + Environment.NewLine
                + Utils.ColorHelper.GetColorForAbility("Level 3: " + PantheraConfig.ImprovedLeap_targetReduction3 + "s", PantheraConfig.ImprovedLeapAbilityID, 3);

            // Ray Slash //
            //desc = "Absorb all damage around you and create <style=cIsUtility>Energy Stacks</style> (Max <style=cIsUtility>" + PantheraConfig.RaySlash_maxRaySlashBuff + "</style>).";
            //LanguageAPI.Add("PANTHERA_SKILL_4_NAME", "Power of the Wild God");
            //LanguageAPI.Add("PANTHERA_SKILL_4_DESCRIPTION", desc);

        }

    }
}
