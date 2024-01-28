using EntityStates.AffixEarthHealer;
using EntityStates.LunarGolem;
using Panthera.Base;
using Panthera.Components;
using R2API;
using RoR2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

namespace Panthera.Utils
{
    class Tokens
    {

        public static void RegisterTokens()
        {






            // ------------------------------------------ BASE ------------------------------------------ //

            #region Character Description
            string CharacterName = "P4N7H3R4";
            string CharacterSubtitle = "Play like a predator!";
            string CharacterDesc = ColorHelper.SetCyan("Don't be the prey, be the predator!") + Environment.NewLine + Environment.NewLine
                + "P4N7H3R4 (Panthera) is a powerful Panther who belongs to the Wild Gods family. Unlike humans, Big Cats are naturally equipped to hunt and kill, this makes P4N7H3R4 a very strong predator." + Environment.NewLine + Environment.NewLine
                + ColorHelper.SetUtility("No Shield: Every Item that add Shield add Health instead.") + Environment.NewLine + Environment.NewLine
                + ColorHelper.SetUtility("Cat: You take no fall damage.");
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
            string ModelName4 = "Primal Stalker";
            PantheraTokens.Add("PANTHERA_MODEL_NAME_4", ModelName4);
            #endregion

            #region Out of Combat Text
            string base_OutOfCombatText = "OUT OF COMBAT";
            PantheraTokens.Add("base_OutOfCombatText", base_OutOfCombatText);
            #endregion


            // ------------------------------------------ SKILLS --------------------------------- //
            #region Passive
            string skill_passiveName = "[Power] Feline Skills";
            string skill_passiveDesc = "P4N7H3R4 can be configured by pressing the " + ColorHelper.SetGreen(PantheraConfig.Keys_OpenPantheraPanelActionDesc) + " Key (Default P).";
            PantheraTokens.Add("skill_passiveName", skill_passiveName);
            PantheraTokens.Add("skill_passiveDesc", skill_passiveDesc);
            #endregion

            #region Rip
            string skill_RipName = "Rip and Shred";
            string skill_RipDesc = "Rips and Shreds all Enemies in front of you for "
                + ColorHelper.SetDamage("{0}%") 
                + " damage." +Environment.NewLine
                + "If there are no Enemies, " + ColorHelper.SetUtility("Rip and Shred") + " becomes " + ColorHelper.SetUtility("Air Cleave") + ".";
            string skill_RipFuryDesc = Environment.NewLine + Environment.NewLine + ColorHelper.SetFury("Every time " + skill_RipName + " hits an Enemy, you generate {0} Fury Points.");
            PantheraTokens.Add("skill_RipName", skill_RipName);
            PantheraTokens.Add("skill_RipDesc", skill_RipDesc);
            PantheraTokens.Add("skill_RipFuryDesc", skill_RipFuryDesc);
            #endregion

            #region Air Cleave
            string skill_AirCleaveName = "Air Cleave";
            string skill_AirCleaveDesc = skill_RipDesc = ColorHelper.SetUtility("Air Cleave:") + Environment.NewLine + Environment.NewLine
                + "Cleave the air and lanch a Projectile that deals "
                + ColorHelper.SetDamage("{0}%")
                + " damage.";
            string skill_AirCleaveFuryDesc = Environment.NewLine + Environment.NewLine + ColorHelper.SetFury("If " + skill_AirCleaveName + " hit a target, you generate {0} Fury Points.");
            PantheraTokens.Add("skill_AirCleaveName", skill_AirCleaveName);
            PantheraTokens.Add("skill_AirCleaveDesc", skill_AirCleaveDesc);
            PantheraTokens.Add("skill_AirCleaveFuryDesc", skill_AirCleaveFuryDesc);
            #endregion

            #region Leap
            string skill_LeapName = "Predator Leap";
            string skill_LeapDesc = "Leap forward or towards the focused target. Allows to jump again at the end of the leap.";
            PantheraTokens.Add("skill_LeapName", skill_LeapName);
            PantheraTokens.Add("skill_LeapDesc", skill_LeapDesc);
            #endregion

            #region Mighty Roar
            string skill_MightyRoarName = "Mighty Roar";
            string skill_MightyRoarDesc = "Unleash a ferocious roar that stuns all Enemies within "
                + ColorHelper.SetUtility("{0}m")
                + " for "
                + ColorHelper.SetUtility("{1}s")
                + ".";
            PantheraTokens.Add("skill_MightyRoarName", skill_MightyRoarName);
            PantheraTokens.Add("skill_MightyRoarDesc", skill_MightyRoarDesc);
            #endregion

            #region Slash
            string skill_SlashName = "Slash";
            string skill_SlashDesc = "Slash all Enemies around you for " + ColorHelper.SetDamage("{0}%") + " damage.";
            string skill_SlashFuryDesc = Environment.NewLine + Environment.NewLine + ColorHelper.SetFury("Every time " + skill_SlashName + " hits an Enemy, you generate {0} Fury Points.");
            PantheraTokens.Add("skill_SlashName", skill_SlashName);
            PantheraTokens.Add("skill_SlashDesc", skill_SlashDesc);
            PantheraTokens.Add("skill_SlashFuryDesc", skill_SlashFuryDesc);
            #endregion

            // ------------------------------------------ ABILITIES --------------------------------- //
            #region Feline Skills
            string ability_FelineSkillsName = "Feline Skills";
            string ability_FelineSkillsDesc = "All " + ColorHelper.SetUtility("Fall Damage") + " are negated.";
            PantheraTokens.Add("ability_FelineSkillsName", ability_FelineSkillsName);
            PantheraTokens.Add("ability_FelineSkillsDesc", ability_FelineSkillsDesc);
            #endregion

            #region Sharpened Fangs
            string ability_SharpenedFangsName = "Sharpened Fangs";
            string ability_SharpenedFangsDesc = "Increases " + ColorHelper.SetUtility(skill_RipName) + " and " + ColorHelper.SetUtility(skill_AirCleaveName) + " damage by " + ColorHelper.SetDamage("{0}%") + ".";
            PantheraTokens.Add("ability_SharpenedFangsName", ability_SharpenedFangsName);
            PantheraTokens.Add("ability_SharpenedFangsDesc", ability_SharpenedFangsDesc);
            #endregion

            #region Fury
            string ability_FuryName = "Fury";
            string ability_FuryDesc = ColorHelper.SetBlue("Press again to cancel") + Environment.NewLine
                + ColorHelper.SetBlue("Disable the Guardian Mode") + Environment.NewLine + Environment.NewLine
                + "P4N7H3R4 enters a state of fury, increasing attack speed by " + ColorHelper.SetDamage("{0}%") + " and movement speed by " + ColorHelper.SetUtility("{1}%") + "."
                + Environment.NewLine + Environment.NewLine
                + ColorHelper.SetFury("While the Fury Mode is active, Fury Points are gradually consumed");
            PantheraTokens.Add("ability_FuryName", ability_FuryName);
            PantheraTokens.Add("ability_FuryDesc", ability_FuryDesc);
            #endregion

            #region Enchanted Fur
            string ability_EnchantedFurName = "Enchanted Fur";
            string ability_EnchantedFurDesc = "Reduces all incoming damage by " + ColorHelper.SetUtility("{0}%") + ".";
            PantheraTokens.Add("ability_EnchantedFurName", ability_EnchantedFurName);
            PantheraTokens.Add("ability_EnchantedFurDesc", ability_EnchantedFurDesc);
            #endregion

            #region Guardian
            string ability_GuardianName = "Guardian";
            string ability_GuardianDesc = ColorHelper.SetBlue("Press again to cancel") + Environment.NewLine
                + ColorHelper.SetBlue("Disable the Fury Mode") + Environment.NewLine + Environment.NewLine
                + "P4N7H3R4 adopts a defensive stance,  increasing Armor by " + ColorHelper.SetUtility("{0}%") + " and Health Regeneration by " + ColorHelper.SetHeal("{1}%") + "."
                + Environment.NewLine + Environment.NewLine
                + ColorHelper.SetFury("Fury Points are not generated while the Guardian Mode is active");
            PantheraTokens.Add("ability_GuardianName", ability_GuardianName);
            PantheraTokens.Add("ability_GuardianDesc", ability_GuardianDesc);
            #endregion

            #region Wind Walker
            string ability_WindWalkerName = "Wind Walker";
            string ability_WindWalkerDesc = "Adds an additional jump.";
            PantheraTokens.Add("ability_WindWalkerName", ability_WindWalkerName);
            PantheraTokens.Add("ability_WindWalkerDesc", ability_WindWalkerDesc);
            #endregion

            #region Predator
            string ability_PredatorName = "Predator";
            string ability_PredatorDesc = "Increases the Max Health added from " + ColorHelper.SetUtility("Bizon Steack") + " by an additional " + ColorHelper.SetHeal("{0}") + ".";
            PantheraTokens.Add("ability_PredatorName", ability_PredatorName);
            PantheraTokens.Add("ability_PredatorDesc", ability_PredatorDesc);
            #endregion

            #region Prowl
            string ability_ProwlName = "Prowl";
            string ability_ProwlDesc = ColorHelper.SetBlue("Press again to Cancel") + Environment.NewLine
                + ColorHelper.SetBlue("Cannot be used while in combat") + Environment.NewLine + Environment.NewLine
                + "You become "
                + ColorHelper.SetUtility("invisible")
                + " to all Enemies. The effect is canceled if you deal or take damage.";
            PantheraTokens.Add("ability_ProwlName", ability_ProwlName);
            PantheraTokens.Add("ability_ProwlDesc", ability_ProwlDesc);
            #endregion

            #region Detection
            string ability_DetectionName = "Tracking";
            string ability_DetectionDesc = ColorHelper.SetBlue("Press again to cancel") + Environment.NewLine + Environment.NewLine
                + "Greatly enhances P4N7H3R4's senses, shifting into "
                + ColorHelper.SetUtility("Predatory Mode")
                + ". This reduces environmental perception but grants the ability to detect all Enemies and Allies, even through walls.";
            PantheraTokens.Add("ability_DetectionName", ability_DetectionName);
            PantheraTokens.Add("ability_DetectionDesc", ability_DetectionDesc);
            #endregion

            #region Ambition
            string ability_AmbitionName = "Ambition";
            string ability_AmbitionDesc1 = "While Ambition is active, each Enemy killed adds a stack of the " + ColorHelper.SetUtility("Cupidity") + " Buff." + Environment.NewLine
                + "Ambition lasts for " + ColorHelper.SetUtility("{0}") + " secondes.";
            PantheraTokens.Add("ability_AmbitionName", ability_AmbitionName);
            PantheraTokens.Add("ability_AmbitionDesc1", ability_AmbitionDesc1);
            #endregion

            #region AirSlash
            string ability_AirSlashName = "Air Slash";
            string ability_AirSlashDesc = "Rips the air and launches a projectile that passes through the Enemies, dealing " + ColorHelper.SetDamage("{0}%") + " damage.";
            string Ability_AirSlashFuryDesc = Environment.NewLine + Environment.NewLine + ColorHelper.SetFury("Every time " + ability_AirSlashName + " deals damage, you generate {0} Fury Points.");
            PantheraTokens.Add("ability_AirSlashName", ability_AirSlashName);
            PantheraTokens.Add("ability_AirSlashDesc", ability_AirSlashDesc);
            PantheraTokens.Add("Ability_AirSlashFuryDesc", Ability_AirSlashFuryDesc);
            #endregion

            #region Front Shield
            string ability_FrontShieldName = "Front Shield";
            string ability_FrontShieldDesc = ColorHelper.SetGreen("Replace " + skill_SlashName + " while the Guardian Mode is active") + Environment.NewLine
                + ColorHelper.SetBlue("Keep the Button pressed to maintain the Shield") + Environment.NewLine + Environment.NewLine
                + "Generates a Shield in front of you that absorb incoming damages." + Environment.NewLine
                + "- Capacity: " + ColorHelper.SetShield("{0}%") + " of the max Health" + Environment.NewLine
                + "- Recharge delay after taking damages: " + ColorHelper.SetUtility("{1}s") + Environment.NewLine
                + "- Recharge delay after Shield destruction: " + ColorHelper.SetUtility("{2}s");
            PantheraTokens.Add("ability_FrontShieldName", ability_FrontShieldName);
            PantheraTokens.Add("ability_FrontShieldDesc", ability_FrontShieldDesc);
            #endregion

            #region Claws Storm
            string ability_ClawsStormName = "Claws Storm";
            string ability_ClawsStormDesc = ColorHelper.SetGreen("Replace " + skill_SlashName + " while Fury Mode is active") + Environment.NewLine
                + ColorHelper.SetBlue("Keep the Button pressed to keep Claws Storm activated") + Environment.NewLine + Environment.NewLine
                + "Dash forward with a storm of slashes, dealing " + ColorHelper.SetDamage("{0}%") + " damage to all nearby Enemies every " + ColorHelper.SetUtility("{1}s") + "." + Environment.NewLine + Environment.NewLine
                + ColorHelper.SetFury("Each attack consumes {2} Fury Points.");
            PantheraTokens.Add("ability_ClawsStormName", ability_ClawsStormName);
            PantheraTokens.Add("ability_ClawsStormDesc", ability_ClawsStormDesc);
            #endregion

            #region SwiftMoves
            string ability_SwiftMovesName = "Swift Moves";
            string ability_SwiftMovesDesc = "Decreases the speed penality of " + ColorHelper.SetUtility(ability_ProwlName) + " by " + ColorHelper.SetUtility("{0}%") + ".";
            PantheraTokens.Add("ability_SwiftMovesName", ability_SwiftMovesName);
            PantheraTokens.Add("ability_SwiftMovesDesc", ability_SwiftMovesDesc);
            #endregion

            #region Ghost Rip
            string ability_GhostRipName = "Ghost Rip";
            string ability_GhostRipDesc = ColorHelper.SetGreen("Replace " + skill_RipName + " while Prowl is active") + Environment.NewLine + Environment.NewLine
                + "Rips and Shreds all Enemies in front of you for " 
                + ColorHelper.SetDamage("{0}%") + " damage "
                + "and stuns them for " + ColorHelper.SetUtility("{1}s") + ".";
            string skill_GhostRipFuryDesc = Environment.NewLine + Environment.NewLine + ColorHelper.SetFury("Every time " + ability_GhostRipName + " hits an Enemy, you generate {0} Fury Points.");
            PantheraTokens.Add("ability_GhostRipName", ability_GhostRipName);
            PantheraTokens.Add("ability_GhostRipDesc", ability_GhostRipDesc);
            PantheraTokens.Add("skill_GhostRipFuryDesc", skill_GhostRipFuryDesc);
            #endregion

            #region Improved Shield
            string ability_ImprovedShieldName = "Improved Shield";
            string ability_ImprovedShieldDesc = "Increases the capacity of your " + ColorHelper.SetUtility(ability_FrontShieldName) + " by " + ColorHelper.SetShield("{0}%")
                + " of your max Health every time you level up ingame."
                + "Also, reduce the recharge delay after the Shield take damage or is destroyed by " + ColorHelper.SetUtility("{1}s") + ".";
            PantheraTokens.Add("ability_ImprovedShieldName", ability_ImprovedShieldName);
            PantheraTokens.Add("ability_ImprovedShieldDesc", ability_ImprovedShieldDesc);
            #endregion

            #region Shield Bash
            string ability_ShieldBashName = "Shield Bash";
            string ability_ShieldBashDesc = ColorHelper.SetBlue("Activated by pressing " + skill_RipName + " while the Front Shield is active.") + Environment.NewLine + Environment.NewLine
                + "Perform a forward rush that deals " + ColorHelper.SetDamage("{0}%") 
                + " damage and stuns all Enemies in its path for " + ColorHelper.SetUtility("{1}s")
                + "." + Environment.NewLine
                + "While using Shield Bash, you are immune to all damage and you gain an additional jump at the end of the rush.";
            PantheraTokens.Add("ability_ShieldBashName", ability_ShieldBashName);
            PantheraTokens.Add("ability_ShieldBashDesc", ability_ShieldBashDesc);
            #endregion

            #region Golden Rip
            string ability_GoldenRipName = "Golden Rip";
            string ability_GoldenRipDesc = ColorHelper.SetGreen("Replace " + skill_RipName + " while Ambition is active") + Environment.NewLine + Environment.NewLine
                + "Rips and Shreds all Enemies in front of you for " 
                + ColorHelper.SetDamage("{0}%") + " damage "
                + "and generating " + ColorHelper.SetYellow("{1}") + " Gold for each Enemy hit.";
            string skill_GoldenRipFuryDesc = Environment.NewLine + Environment.NewLine + ColorHelper.SetFury("Every time " + ability_GoldenRipName + " hits an Enemy, you generate {0} Fury Points.");
            PantheraTokens.Add("ability_GoldenRipName", ability_GoldenRipName);
            PantheraTokens.Add("ability_GoldenRipDesc", ability_GoldenRipDesc);
            PantheraTokens.Add("skill_GoldenRipFuryDesc", skill_GoldenRipFuryDesc);
            #endregion

            #region Tornado
            string ability_TornadoName = "Tornado";
            string ability_TornadoDesc = "While using " + ColorHelper.SetUtility(ability_ClawsStormName)
                + ", damages dealt are increased by " + ColorHelper.SetDamage("{0}%")
                + " and damages taken are decreased by " + ColorHelper.SetHeal("{1}%") + ".";
            PantheraTokens.Add("ability_TornadoName", ability_TornadoName);
            PantheraTokens.Add("ability_TornadoDesc", ability_TornadoDesc);
            #endregion

            #region Arcane Anchor
            string ability_ArcaneAnchorName = "Arcane Anchor";
            string ability_ArcaneAnchorDesc = ColorHelper.SetBlue("Activated by pressing " + skill_MightyRoarName + " while the Front Shield is active.") + Environment.NewLine
                + ColorHelper.SetBlue("Press " + ability_FrontShieldName + " and " + skill_MightyRoarName + " again to get the Shield back.") + Environment.NewLine + Environment.NewLine
                + "Deploy the Front Shield and freeze it in place. You can use all your Skills while the Shield is deployed." + Environment.NewLine
                + "The Shield doesn't recharge when deployed." + Environment.NewLine
                + "Withdrawn when you exit Guardian mode or move too far away.";
            PantheraTokens.Add("ability_ArcaneAnchorName", ability_ArcaneAnchorName);
            PantheraTokens.Add("ability_ArcaneAnchorDesc", ability_ArcaneAnchorDesc);
            #endregion

            #region Convergence Hook
            string ability_ConvergenceHookName = "Convergence Hook";
            string ability_ConvergenceHookDesc = "Pulls the last target hit by " + ColorHelper.SetUtility(skill_AirCleaveName) + " towards you." + Environment.NewLine
                + "Doesn't work on Boss.";
            PantheraTokens.Add("ability_ConvergenceHookName", ability_ConvergenceHookName);
            PantheraTokens.Add("ability_ConvergenceHookDesc", ability_ConvergenceHookDesc);
            #endregion

            #region Massive Hook
            string ability_MassiveHookName = "Massive Hook";
            string ability_MassiveHookDesc = ColorHelper.SetUtility(ability_ConvergenceHookName) + " also works with enemies hit by "
                + ColorHelper.SetUtility(ability_AirSlashName) + ".";
            PantheraTokens.Add("ability_MassiveHookName", ability_MassiveHookName);
            PantheraTokens.Add("ability_MassiveHookDesc", ability_MassiveHookDesc);
            #endregion

            #region Sixth Sense
            string ability_SixthSenseName = "Sixth Sense";
            string ability_SixthSenseDesc = ColorHelper.SetUtility(ability_DetectionName) + " also detects objects such as Chests, Barrels, and Teleporters.";
            PantheraTokens.Add("ability_SixthSenseName", ability_SixthSenseName);
            PantheraTokens.Add("ability_SixthSenseDesc", ability_SixthSenseDesc);
            #endregion

            #region Relentless Stalker
            string ability_RelentlessStalkerName = "Relentless Stalker";
            string ability_RelentlessStalkerDesc = "If " + ColorHelper.SetUtility(skill_LeapName) + " has a target, its cooldown is reduced by " + ColorHelper.SetUtility("{0}s") + ".";
            PantheraTokens.Add("ability_RelentlessStalkerName", ability_RelentlessStalkerName);
            PantheraTokens.Add("ability_RelentlessStalkerDesc", ability_RelentlessStalkerDesc);
            #endregion

            #region Roar Of Resilience
            string ability_RoarOfResilienceName = "Roar Of Resilience";
            string ability_RoarOfResilienceDesc = "When using " + ColorHelper.SetUtility(skill_MightyRoarName) + ", you gain a stack of the " + ColorHelper.SetBuff("Tenacity") + " Buff for each Enemy affected.";
            PantheraTokens.Add("ability_RoarOfResilienceName", ability_RoarOfResilienceName);
            PantheraTokens.Add("ability_RoarOfResilienceDesc", ability_RoarOfResilienceDesc);
            #endregion

            #region Claws Sharpening
            string ability_ClawsSharpeningName = "Claws Sharpening";
            string ability_ClawsSharpeningDesc = "Using " + ColorHelper.SetUtility(skill_SlashName) + " sharpens P4N7H3R4's claws, adding a stack of the Razors buff for each enemy hit";
            PantheraTokens.Add("ability_ClawsSharpeningName", ability_ClawsSharpeningName);
            PantheraTokens.Add("ability_ClawsSharpeningDesc", ability_ClawsSharpeningDesc);
            #endregion

            #region Golden Start
            string ability_GoldenStartName = "Golden Start";
            string ability_GoldenStartDesc = "Adds " + ColorHelper.SetYellow("{0}") + " Coins at the start of the run.";
            PantheraTokens.Add("ability_GoldenStartName", ability_GoldenStartName);
            PantheraTokens.Add("ability_GoldenStartDesc", ability_GoldenStartDesc);
            #endregion

            // ------------------------------------------ COMBOS --------------------------------- //
            #region Combos
            PantheraTokens.Add("combo_RipName", "Rip and Shred");
            PantheraTokens.Add("combo_SlashName", "Slash");
            PantheraTokens.Add("combo_LeapName", "Predator Leap");
            PantheraTokens.Add("combo_MightyRoarName", "MightyRoar");
            PantheraTokens.Add("combo_FuryName", "Fury");
            PantheraTokens.Add("combo_GuardianName", "Guardian");
            PantheraTokens.Add("combo_ProwlName", "Prowl");
            PantheraTokens.Add("combo_DetectionName", "Tracking");
            PantheraTokens.Add("combo_AirSlashName", "Air Slash");
            PantheraTokens.Add("combo_FrontShieldName", "Front Shield");
            PantheraTokens.Add("combo_ClawsStormName", "Claws Storm");
            PantheraTokens.Add("combo_ShieldBashName", "Shield Bash");
            PantheraTokens.Add("combo_ArcaneAnchor", "Arcane Anchor");
            PantheraTokens.Add("combo_ConvergenceHook", "Convergence Hook");
            PantheraTokens.Add("combo_MassiveHook", "Massive Hook");
            PantheraTokens.Add("combo_Ambition", "Ambition");
            #endregion


            // ------------------------------------------ BUFFS --------------------------------- //
            #region Cupidity
            string buff_CupidityName = "Cupidity";
            string buff_CupidityDesc = "For each " + ColorHelper.SetBuff("Cupidity") + " stack, the amount of Gold you receive is increased by "
                + ColorHelper.SetUtility("{0}%") + ". "
                + "When " + ColorHelper.SetUtility("Ambition") + " ends, the stacks slowly decrease.";
            PantheraTokens.Add("buff_CupidityName", buff_CupidityName);
            PantheraTokens.Add("buff_CupidityDesc", buff_CupidityDesc);
            #endregion

            #region Tenacity
            string buff_TenacityName = "Tenacity";
            string buff_TenacityDesc = "You generate " + ColorHelper.SetBarrier("{0}%") + " Barrier points per second.";
            PantheraTokens.Add("buff_TenacityName", buff_TenacityName);
            PantheraTokens.Add("buff_TenacityDesc", buff_TenacityDesc);
            #endregion

            #region Razors
            string buff_RazorsName = "Razors";
            string buff_RazorsDesc = "The next use of " + ColorHelper.SetUtility(skill_RipName) + " applies the bleeding effect of " + ColorHelper.SetUtility(skill_SlashName) + ", consuming a stack.";
            PantheraTokens.Add("buff_RazorsName", buff_RazorsName);
            PantheraTokens.Add("buff_RazorsDesc", buff_RazorsDesc);
            #endregion

            #region BleedOut
            string buff_BleedOutName = "Bleed Out";
            string buff_BleedOutDesc = "Inflicts " + ColorHelper.SetDamage("{0}%") + " bleeding damage every " + ColorHelper.SetUtility("{1}s") + ".";
            PantheraTokens.Add("buff_BleedOutName", buff_BleedOutName);
            PantheraTokens.Add("buff_BleedOutDesc", buff_BleedOutDesc);
            #endregion

        }

    }
}
