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
using UnityEngine.Rendering.PostProcessing;
using static UnityEngine.Rendering.PostProcessing.HopooSSRRenderer;
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
                + "P4N7H3R4 (Panthera) is a powerful Panther who belongs to the Wild Gods family. Unlike humans, Big Cats are naturally equipped to hunt and kill, this makes P4N7H3R4 a very strong predator.";
            PantheraTokens.Add("PANTHERA_NAME", CharacterName);
            PantheraTokens.Add("PANTHERA_DESC", CharacterDesc);
            PantheraTokens.Add("PANTHERA_SUBTITLE", CharacterSubtitle);
            #endregion

            #region Model
            PantheraTokens.Add("PANTHERA_MODEL_NAME_1", "Leopard");
            PantheraTokens.Add("PANTHERA_MODEL_NAME_2", "Tiger");
            PantheraTokens.Add("PANTHERA_MODEL_NAME_3", "Armored");
            PantheraTokens.Add("PANTHERA_MODEL_NAME_4", "Ashamane");
            PantheraTokens.Add("PANTHERA_MODEL_NAME_5", "Primal Stalker");
            #endregion

            #region Attributes

            string attribute_Endurance = "Max health: " + ColorHelper.SetUtility("+++") + Environment.NewLine
                                       + "Health regen: " + ColorHelper.SetUtility("+") + Environment.NewLine;
            PantheraTokens.Add("attribute_Endurance", attribute_Endurance);

            string attribute_Force = "Damage: " + ColorHelper.SetUtility("++") + Environment.NewLine
                                   + "Defense: " + ColorHelper.SetUtility("++");
            PantheraTokens.Add("attribute_Force", attribute_Force);


            string attribute_Agility = "Move speed: " + ColorHelper.SetUtility("+") + Environment.NewLine
                                     + "Critic: " + ColorHelper.SetUtility("++") + Environment.NewLine
                                     + "Dodge: " + ColorHelper.SetUtility("+");
            PantheraTokens.Add("attribute_Agility", attribute_Agility);

            string attribute_Swiftness = "Move speed: " + ColorHelper.SetUtility("++") + Environment.NewLine
                                       + "Attack speed: " + ColorHelper.SetUtility("++") + Environment.NewLine;
            PantheraTokens.Add("attribute_Swiftness", attribute_Swiftness);

            string attribute_Dexterity = "Damages: " + ColorHelper.SetUtility("+") + Environment.NewLine
                                       + "Critic: " + ColorHelper.SetUtility("+") + Environment.NewLine
                                       + "Dodge: " + ColorHelper.SetUtility("++");
            PantheraTokens.Add("attribute_Dexterity", attribute_Dexterity);

            string attribute_Spirit = "Health regen: : " + ColorHelper.SetUtility("+") + Environment.NewLine
                                      + "Mastery: " + ColorHelper.SetUtility("+++");
            PantheraTokens.Add("attribute_Spirit", attribute_Spirit);

            #endregion

            #region Absorb Text
            string base_AbsorbText = "ABSORB";
            PantheraTokens.Add("base_AbsorbText", base_AbsorbText);
            #endregion

            #region Dodge Text
            string base_DodgeText = "DODGE";
            PantheraTokens.Add("base_DodgeText", base_DodgeText);
            #endregion

            #region Block Text
            string base_BlockText = "BLOCK";
            PantheraTokens.Add("base_BlockText", base_BlockText);
            #endregion

            #region Out of Combat Text
            string base_OutOfCombatText = "OUT OF COMBAT";
            PantheraTokens.Add("base_OutOfCombatText", base_OutOfCombatText);
            #endregion

            #region Reduced Text
            string base_ReducedText = "REDUCED";
            PantheraTokens.Add("base_ReducedText", base_ReducedText);
            #endregion

            #region Help Text

            string help_Mastery = ColorHelper.SetMastery("Mastery Points") + " are consumed when right-clicking a compatible skill, activating its specialized " + ColorHelper.SetMastery("[Mastery]") + " effect." + Environment.NewLine
                + "Right-click once more to deactivate it (can only be done in the lobby).";
            PantheraTokens.Add("help_Mastery", help_Mastery);

            string help_Ruse = "The " + ColorHelper.SetUtility("Ruse") + " skills tree enables a balanced gameplay style, focusing on both offense and defense." + Environment.NewLine
                + "Its skills are more technical and require an increased level of character expertise, allowing for heavy damage to single targets.";
            PantheraTokens.Add("help_Ruse", help_Ruse);

            string help_Fury = "The " + ColorHelper.SetUtility("Fury") + " skills tree introduces an extremely offensive and aggressive gameplay style." + Environment.NewLine
                + "Its skills enable swift combat and inflict mass damage to multiple enemies simultaneously.";
            PantheraTokens.Add("help_Fury", help_Fury);

            string help_Classic = "The " + ColorHelper.SetUtility("Classic") + " skills tree enables a much more traditional style of gameplay, with minimal alterations to the character's abilities." + Environment.NewLine
                + "Its skills primarily focus on enhancing basic mechanics.";
            PantheraTokens.Add("help_Classic", help_Classic);

            string help_Gardian = "The " + ColorHelper.SetUtility("Gardian") + " skills tree introduces a defensive playstyle to the character." + Environment.NewLine
                + "Its skills are centered around enhancing survival capabilities and assisting other players through healing and capacity improvement.";
            PantheraTokens.Add("help_Gardian", help_Gardian);

            #endregion

            // ------------------------------------------ SKILLS --------------------------------- //
            #region Passive
            string skill_passiveName = "[Power] Feline Skills";
            string skill_passiveDesc = "Press [" + ColorHelper.SetGreen(PantheraConfig.Keys_OpenPantheraPanelActionDesc) + "] key (Default P) to open P4N7H3R4 panel. This can be done in the lobby. Levels are retained between runs";
            PantheraTokens.Add("skill_passiveName", skill_passiveName);
            PantheraTokens.Add("skill_passiveDesc", skill_passiveDesc);
            #endregion

            #region Rip
            string skill_RipName = "Rip and Shred";
            string skill_RipDesc = "Rips and shreds all enemies in front of you for "
                + ColorHelper.SetDamage("{0}%")
                + " damage." + Environment.NewLine
                + "If there are no enemies, [" + ColorHelper.SetUtility("Rip and Shred") + "] becomes [" + ColorHelper.SetUtility("Air Cleave") + "].";
            string skill_RipFuryDesc = Environment.NewLine + Environment.NewLine + ColorHelper.SetFury("Every time " + skill_RipName + " hits an enemy, you generate {0} Fury Points.");
            PantheraTokens.Add("skill_RipName", skill_RipName);
            PantheraTokens.Add("skill_RipDesc", skill_RipDesc);
            PantheraTokens.Add("skill_RipFuryDesc", skill_RipFuryDesc);
            #endregion

            #region Air Cleave
            string skill_AirCleaveName = "Air Cleave";
            string skill_AirCleaveDesc = skill_RipDesc = ColorHelper.SetUtility("[Air Cleave]") + Environment.NewLine + Environment.NewLine
                + "Cleave the air and lanch a projectile that deals "
                + ColorHelper.SetDamage("{0}%")
                + " damage.";
            string skill_AirCleaveFuryDesc = Environment.NewLine + Environment.NewLine + ColorHelper.SetFury("If " + skill_AirCleaveName + " hit a target, you generate {0} Fury Points.");
            PantheraTokens.Add("skill_AirCleaveName", skill_AirCleaveName);
            PantheraTokens.Add("skill_AirCleaveDesc", skill_AirCleaveDesc);
            PantheraTokens.Add("skill_AirCleaveFuryDesc", skill_AirCleaveFuryDesc);
            #endregion

            #region Leap
            string skill_LeapName = "Predator Leap";
            string skill_LeapDesc = "Leap forward or towards the focused target." + Environment.NewLine
                                  + "Keep the " + ColorHelper.SetUtility("Primary Skill") + " Button pressed at the end of the jump to launch [" + ColorHelper.SetUtility("Feral Bite") + "].";
            PantheraTokens.Add("skill_LeapName", skill_LeapName);
            PantheraTokens.Add("skill_LeapDesc", skill_LeapDesc);
            #endregion

            #region Mighty Roar
            string skill_MightyRoarName = "Mighty Roar";
            string skill_MightyRoarDesc = "Unleash a ferocious roar that stuns all enemies within "
                + ColorHelper.SetUtility("{0}m")
                + " for "
                + ColorHelper.SetUtility("{1}s")
                + ".";
            PantheraTokens.Add("skill_MightyRoarName", skill_MightyRoarName);
            PantheraTokens.Add("skill_MightyRoarDesc", skill_MightyRoarDesc);
            #endregion

            #region Slash
            string skill_SlashName = "Slash";
            string skill_SlashDesc = "Slash all enemies around you for " + ColorHelper.SetDamage("{0}%") + " damage, applying a stack of the " + ColorHelper.SetBuff("Bleed Out") 
                                   + " debuff to each enemy hit that lasts for " + ColorHelper.SetUtility( PantheraConfig.Slash_BleedDuration + "s") + ".";
            string skill_SlashFuryDesc = Environment.NewLine + Environment.NewLine + ColorHelper.SetFury("Every time " + skill_SlashName + " hits an enemy, you generate {0} Fury Points.");
            PantheraTokens.Add("skill_SlashName", skill_SlashName);
            PantheraTokens.Add("skill_SlashDesc", skill_SlashDesc);
            PantheraTokens.Add("skill_SlashFuryDesc", skill_SlashFuryDesc);
            #endregion

            #region Feral Bite
            string skill_FeralBiteName = "Feral Bite";
            string skill_FeralBiteDesc = "Ferociously bites the target, dealing "
                + ColorHelper.SetDamage("{0}%")
                + " damage while converting "
                + ColorHelper.SetHeal("{1}%")
                + " of it into healing" + Environment.NewLine
                + "If the target die, the [" + ColorHelper.SetUtility("Predator Leap") + "] cooldown is removed.";
            string skill_FeralBiteFuryDesc = Environment.NewLine + Environment.NewLine + ColorHelper.SetFury(skill_FeralBiteName + " generate {0} Fury Points, doubled if critic.");
            PantheraTokens.Add("skill_FeralBiteName", skill_FeralBiteName);
            PantheraTokens.Add("skill_FeralBiteDesc", skill_FeralBiteDesc);
            PantheraTokens.Add("skill_FeralBiteFuryDesc", skill_FeralBiteFuryDesc);
            #endregion

            // ------------------------------------------ SPECIALS --------------------------------- //
            #region Untamed Spirit
            string ability_UntamedSpiritName = "Untamed Spirit";
            string ability_UntamedSpiritDesc = ColorHelper.SetUtility("Unlocked at P4N7H3R4 level {0}") + Environment.NewLine + Environment.NewLine
                + "You are immune to all freeze or stun effects.";
            PantheraTokens.Add("ability_UntamedSpiritName", ability_UntamedSpiritName);
            PantheraTokens.Add("ability_UntamedSpiritDesc", ability_UntamedSpiritDesc);
            #endregion

            #region God Power
            string ability_GodPowerName = "God Power";
            string ability_GodPowerDesc = ColorHelper.SetUtility("Unlocked at P4N7H3R4 level {0}") + Environment.NewLine + Environment.NewLine
                + "Not used now ...";
            PantheraTokens.Add("ability_GodPowerName", ability_GodPowerName);
            PantheraTokens.Add("ability_GodPowerDesc", ability_GodPowerDesc);
            #endregion

            #region Portal Surge
            string ability_PortalSurgeName = "Portal Surge";
            string ability_PortalSurgeDesc = ColorHelper.SetUtility("Unlocked at P4N7H3R4 level " + "{0}") + Environment.NewLine + Environment.NewLine
                + "Overcharge the teleporter, significantly increasing its difficulty, extending its charge time, and summoning an overpowed boss." + Environment.NewLine
                + "You earn one " + ColorHelper.SetMastery("Mastery Point") + " upon the teleporter's completion." + Environment.NewLine
                + "You must be above the teleporter to activate this skill." + Environment.NewLine
                + "Requires in-game level " + ColorHelper.SetUtility("{1}") + "." + Environment.NewLine
                + "Cost " + ColorHelper.SetUtility("{2}") + " Lunar Coins." + Environment.NewLine + Environment.NewLine
                + ColorHelper.SetRed("Warning: Be prepared for very high difficulty.");
            PantheraTokens.Add("ability_PortalSurgeName", ability_PortalSurgeName);
            PantheraTokens.Add("ability_PortalSurgeDesc", ability_PortalSurgeDesc);
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
                + ColorHelper.SetFury("While the Fury Mode is active, Fury Points stop generating and are gradually consumed.");
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
                + "P4N7H3R4 adopts a defensive stance, increasing the Armor by " + ColorHelper.SetUtility("{0}%") + " and Health Regeneration by " + ColorHelper.SetHeal("{1}%") + "." + Environment.NewLine
                + "Reduces the Barrier decay rate by " + ColorHelper.SetBarrier("{2}%") + "."
                + Environment.NewLine + Environment.NewLine
                + ColorHelper.SetFury("Fury Points are not generated while the Guardian Mode is active");
            string ability_GuardianMasteryDesc = ColorHelper.SetMastery("[Mastery]") + Environment.NewLine
                + "When you adopt the Guardian mode, you recover " + ColorHelper.SetHeal("{0}%") + " of your max Health. This amount is increased by the Mastery stat.";
            PantheraTokens.Add("ability_GuardianName", ability_GuardianName);
            PantheraTokens.Add("ability_GuardianDesc", ability_GuardianDesc);
            PantheraTokens.Add("ability_GuardianMasteryDesc", ability_GuardianMasteryDesc);
            #endregion

            #region Wind Walker
            string ability_WindWalkerName = "Wind Walker";
            string ability_WindWalkerDesc = "Adds an additional jump.";
            PantheraTokens.Add("ability_WindWalkerName", ability_WindWalkerName);
            PantheraTokens.Add("ability_WindWalkerDesc", ability_WindWalkerDesc);
            #endregion

            #region Predator
            string ability_PredatorName = "Predator";
            string ability_PredatorDesc = "Increases the max Health added from " + ColorHelper.SetUtility("Bizon Steack") + " by an additional " + ColorHelper.SetHeal("{0}") + ".";
            PantheraTokens.Add("ability_PredatorName", ability_PredatorName);
            PantheraTokens.Add("ability_PredatorDesc", ability_PredatorDesc);
            #endregion

            #region Prowl
            string ability_ProwlName = "Prowl";
            string ability_ProwlDesc = ColorHelper.SetBlue("Press again to Cancel") + Environment.NewLine
                + ColorHelper.SetBlue("Cannot be used while in combat") + Environment.NewLine + Environment.NewLine
                + "You become "
                + ColorHelper.SetUtility("invisible")
                + " to all enemies. The effect is canceled if you deal or take damage.";
            PantheraTokens.Add("ability_ProwlName", ability_ProwlName);
            PantheraTokens.Add("ability_ProwlDesc", ability_ProwlDesc);
            #endregion

            #region Detection
            string ability_DetectionName = "Tracking";
            string ability_DetectionDesc = ColorHelper.SetBlue("Press again to cancel") + Environment.NewLine + Environment.NewLine
                + "Greatly enhances P4N7H3R4's senses, shifting into "
                + ColorHelper.SetUtility("Predatory Mode")
                + ". This reduces environmental perception but grants the ability to detect all allies and enemies, even through walls." + Environment.NewLine
                + "Maximum Duration: " + ColorHelper.SetUtility("{0}s") + ".";
            PantheraTokens.Add("ability_DetectionName", ability_DetectionName);
            PantheraTokens.Add("ability_DetectionDesc", ability_DetectionDesc);
            #endregion

            #region Ambition
            string ability_AmbitionName = "Ambition";
            string ability_AmbitionDesc1 = "While Ambition is active, each enemy killed adds a stack of the " + ColorHelper.SetUtility("Cupidity") + " buff." + Environment.NewLine
                + "Ambition lasts for " + ColorHelper.SetUtility("{0}") + " secondes.";
            PantheraTokens.Add("ability_AmbitionName", ability_AmbitionName);
            PantheraTokens.Add("ability_AmbitionDesc1", ability_AmbitionDesc1);
            #endregion

            #region AirSlash
            string ability_AirSlashName = "Air Slash";
            string ability_AirSlashDesc = "Rips the air and launches a projectile that passes through the enemies, dealing " + ColorHelper.SetDamage("{0}%") + " damage.";
            string Ability_AirSlashFuryDesc = Environment.NewLine + Environment.NewLine + ColorHelper.SetFury("Every time " + ability_AirSlashName + " deals damage, you generate {0} Fury Points.");
            PantheraTokens.Add("ability_AirSlashName", ability_AirSlashName);
            PantheraTokens.Add("ability_AirSlashDesc", ability_AirSlashDesc);
            PantheraTokens.Add("Ability_AirSlashFuryDesc", Ability_AirSlashFuryDesc);
            #endregion

            #region Front Shield
            string ability_FrontShieldName = "Front Shield";
            string ability_FrontShieldDesc = ColorHelper.SetGreen("Replace " + skill_SlashName + " while the Guardian Mode is active") + Environment.NewLine
                + ColorHelper.SetBlue("Keep the button pressed to maintain the Shield") + Environment.NewLine + Environment.NewLine
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
                + ColorHelper.SetBlue("Keep the button pressed to maintain Claws Storm") + Environment.NewLine + Environment.NewLine
                + "Dash forward with a storm of slashes, dealing " + ColorHelper.SetDamage("{0}%") + " damage to all nearby enemies every " + ColorHelper.SetUtility("{1}s") + "." + Environment.NewLine + Environment.NewLine
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
                + "Rips and Shreds all enemies in front of you for "
                + ColorHelper.SetDamage("{0}%") + " damage "
                + "and stuns them for " + ColorHelper.SetUtility("{1}s") + "." + Environment.NewLine
                + "The Critic Chance of Ghost Rip is multiplied by " + ColorHelper.SetUtility("{2}") + ".";
            string skill_GhostRipFuryDesc = Environment.NewLine + Environment.NewLine + ColorHelper.SetFury("Every time " + ability_GhostRipName + " hits an enemy, you generate {0} Fury Points.");
            PantheraTokens.Add("ability_GhostRipName", ability_GhostRipName);
            PantheraTokens.Add("ability_GhostRipDesc", ability_GhostRipDesc);
            PantheraTokens.Add("skill_GhostRipFuryDesc", skill_GhostRipFuryDesc);
            #endregion

            #region Improved Shield
            string ability_ImprovedShieldName = "Improved Shield";
            string ability_ImprovedShieldDesc = "Increases the capacity of your " + ColorHelper.SetUtility(ability_FrontShieldName) + " by " + ColorHelper.SetShield("{0}%")
                + " of your max Health every time you level up ingame." + Environment.NewLine
                + "Also, reduce the recharge delay after the Shield take damage or is destroyed by " + ColorHelper.SetUtility("{1}s") + ".";
            PantheraTokens.Add("ability_ImprovedShieldName", ability_ImprovedShieldName);
            PantheraTokens.Add("ability_ImprovedShieldDesc", ability_ImprovedShieldDesc);
            #endregion

            #region Shield Bash
            string ability_ShieldBashName = "Shield Bash";
            string ability_ShieldBashDesc = ColorHelper.SetBlue("Activated by pressing " + skill_RipName + " while the Front Shield is active.") + Environment.NewLine + Environment.NewLine
                + "Perform a forward rush that deals " + ColorHelper.SetDamage("{0}%")
                + " damage and stuns all enemies in its path for " + ColorHelper.SetUtility("{1}s")
                + "." + Environment.NewLine
                + "While using Shield Bash, you are immune to all damage and you gain an additional jump at the end of the rush.";
            PantheraTokens.Add("ability_ShieldBashName", ability_ShieldBashName);
            PantheraTokens.Add("ability_ShieldBashDesc", ability_ShieldBashDesc);
            #endregion

            #region Golden Rip
            string ability_GoldenRipName = "Golden Rip";
            string ability_GoldenRipDesc = ColorHelper.SetGreen("Replace " + skill_RipName + " while Ambition is active") + Environment.NewLine + Environment.NewLine
                + "Rips and Shreds all enemies in front of you for "
                + ColorHelper.SetDamage("{0}%") + " damage "
                + "and generating " + ColorHelper.SetYellow("{1}") + " Gold for each Enemy hit.";
            string skill_GoldenRipFuryDesc = Environment.NewLine + Environment.NewLine + ColorHelper.SetFury("Every time " + ability_GoldenRipName + " hits an enemy, you generate {0} Fury Points.");
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
                + ColorHelper.SetBlue("Press " + ability_FrontShieldName + " again to get the Shield back.") + Environment.NewLine + Environment.NewLine
                + "Deploy the Front Shield and freeze it in place. You can use all your skills while the Shield is deployed." + Environment.NewLine
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
            string ability_RoarOfResilienceDesc = "When using " + ColorHelper.SetUtility(skill_MightyRoarName) + ", you gain a stack of the " + ColorHelper.SetBuff("Tenacity") + " buff for each enemy affected.";
            PantheraTokens.Add("ability_RoarOfResilienceName", ability_RoarOfResilienceName);
            PantheraTokens.Add("ability_RoarOfResilienceDesc", ability_RoarOfResilienceDesc);
            #endregion

            #region Claws Sharpening
            string ability_ClawsSharpeningName = "Claws Sharpening";
            string ability_ClawsSharpeningDesc = "Using " + ColorHelper.SetUtility(skill_SlashName) + " sharpens P4N7H3R4's claws, adding a stack of the " + ColorHelper.SetBuff("Razors") + " buff for each enemy hit";
            PantheraTokens.Add("ability_ClawsSharpeningName", ability_ClawsSharpeningName);
            PantheraTokens.Add("ability_ClawsSharpeningDesc", ability_ClawsSharpeningDesc);
            #endregion

            #region Golden Start
            string ability_GoldenStartName = "Golden Start";
            string ability_GoldenStartDesc = "Adds " + ColorHelper.SetYellow("{0}") + " Golds at the start of the run.";
            PantheraTokens.Add("ability_GoldenStartName", ability_GoldenStartName);
            PantheraTokens.Add("ability_GoldenStartDesc", ability_GoldenStartDesc);
            #endregion

            #region Stealth Strike
            string ability_StealthStrikeName = "Stealth Strike";
            string ability_StealthStrikeDesc = "If " + ColorHelper.SetUtility(ability_GhostRipName) + " crits, it applies the " + ColorHelper.SetDeBuff("Mortal Mirage") + " debuff.";
            PantheraTokens.Add("ability_StealthStrikeName", ability_StealthStrikeName);
            PantheraTokens.Add("ability_StealthStrikeDesc", ability_StealthStrikeDesc);
            #endregion

            #region Cryo-Leap
            string ability_CryoLeapName = "Cryo-Leap";
            string ability_CryoLeapDesc = "At the end of the " + ColorHelper.SetUtility(skill_LeapName) + " when the enemy is reached, you gain the " + ColorHelper.SetBuff("Frozen Paws") + " buff"
                + " for " + ColorHelper.SetUtility("{0}s") + ".";
            PantheraTokens.Add("ability_CryoLeapName", ability_CryoLeapName);
            PantheraTokens.Add("ability_CryoLeapDesc", ability_CryoLeapDesc);
            #endregion

            #region Shadow Stalker
            string ability_ShadowStalkerName = "Shadow Stalker";
            string ability_ShadowStalkerDesc = ColorHelper.SetUtility(ability_ProwlName) + " persists for "
                + ColorHelper.SetUtility("{0}s") + " after attacking or taking damage.";
            PantheraTokens.Add("ability_ShadowStalkerName", ability_ShadowStalkerName);
            PantheraTokens.Add("ability_ShadowStalkerDesc", ability_ShadowStalkerDesc);
            #endregion

            #region Warden's Vitality
            string ability_WardensVitalityName = "Warden's Vitality";
            string ability_WardensVitalityDesc = "In " + ColorHelper.SetUtility("Guardian mode") + ", your max Health is increased by " + ColorHelper.SetHeal("{0}%") + ". "
            + "You also gain " + ColorHelper.SetBlock("{1}") + " Block points." + Environment.NewLine
            + "This ability does not allow to have beyond " + ColorHelper.SetBlock("{1}") + " Block points.";
            PantheraTokens.Add("ability_WardensVitalityName", ability_WardensVitalityName);
            PantheraTokens.Add("ability_WardensVitalityDesc", ability_WardensVitalityDesc);
            #endregion

            #region Savage Revitalization
            string ability_SavageRevitalizationName = "Savage Revitalization";
            string ability_SavageRevitalizationDesc = "Activating Guardian mode grants you " + ColorHelper.SetUtility("{0}") + " stacks of the " + ColorHelper.SetBuff("Regeneration") + " buff, each lasting  " + ColorHelper.SetUtility("{1}") + " seconds." + Environment.NewLine
                + "This ability does not allow stacking beyond " + ColorHelper.SetUtility("{0}") + " stacks.";
            string ability_SavageRevitalizationMasteryDesc = ColorHelper.SetMastery("[Mastery]") + Environment.NewLine
                + "If you take damage, you have a " + ColorHelper.SetUtility("{0}%") + " chance to gain one stack of the " + ColorHelper.SetBuff("Regeneration") + " buff, lasting "
                + ColorHelper.SetUtility("{1}") + " seconds. The activation chance is determined by your Mastery stat." + Environment.NewLine
                + "Can only appen if Guardian mode is enabled.";
            PantheraTokens.Add("ability_SavageRevitalizationName", ability_SavageRevitalizationName);
            PantheraTokens.Add("ability_SavageRevitalizationDesc", ability_SavageRevitalizationDesc);
            PantheraTokens.Add("ability_SavageRevitalizationMasteryDesc", ability_SavageRevitalizationMasteryDesc);
            #endregion

            #region Innate Protection
            string ability_InnateProtectionName = "Innate Protection";
            string ability_InnateProtectionDesc = "Protects you from excessively heavy attacks, preventing them from inflicting more than " + ColorHelper.SetHeal("{0}%") + " of your maximum Health." + Environment.NewLine
                + "Can only appen if Guardian mode is enabled.";
            PantheraTokens.Add("ability_InnateProtectionName", ability_InnateProtectionName);
            PantheraTokens.Add("ability_InnateProtectionDesc", ability_InnateProtectionDesc);
            #endregion

            #region Furrify
            string ability_FurrifyName = "Furrify";
            string ability_FurrifyDesc = "Each time you take damage exceeding " + ColorHelper.SetUtility("{0}%") + " of your max Health, you gain a stack of the "
                + ColorHelper.SetBuff("Resilience") + " buff. When this effect occurs, the duration of all other stacks is reset." + Environment.NewLine
                + "Can only appen if Guardian mode is enabled.";
            PantheraTokens.Add("ability_FurrifyName", ability_FurrifyName);
            PantheraTokens.Add("ability_FurrifyDesc", ability_FurrifyDesc);
            #endregion

            #region Concentration
            string ability_ConcentrationName = "Concentration";
            string ability_ConcentrationDesc = ColorHelper.SetUtility(ability_DetectionName) + " enables a color-based detection of enemies and objects corresponding to their Health level, and the status of Chests (open, closed, purchasable).";
            PantheraTokens.Add("ability_ConcentrationName", ability_ConcentrationName);
            PantheraTokens.Add("ability_ConcentrationDesc", ability_ConcentrationDesc);
            #endregion

            #region Superior Flair
            string ability_SuperiorFlairName = "Superior Flair";
            string ability_SuperiorFlairDesc = "Extends the duration of " + ColorHelper.SetUtility(ability_DetectionName) + " to " + ColorHelper.SetUtility("{0}") + " seconds.";
            PantheraTokens.Add("ability_SuperiorFlairName", ability_SuperiorFlairName);
            PantheraTokens.Add("ability_SuperiorFlairDesc", ability_SuperiorFlairDesc);
            #endregion

            #region Eternal Fury
            string ability_EternalFuryName = "Eternal Fury";
            string ability_EternalFuryDesc = "Reduces passive Fury consumption by " + ColorHelper.SetFury("{0}%") + " and starts the stage with " + ColorHelper.SetFury("{1}%") + " of its maximum.";
            PantheraTokens.Add("ability_EternalFuryName", ability_EternalFuryName);
            PantheraTokens.Add("ability_EternalFuryDesc", ability_EternalFuryDesc);
            #endregion

            #region Inner Rage
            string ability_InnerRageName = "Inner Rage";
            string ability_InnerRageDesc = "Whenever you take damage, " + ColorHelper.SetFury("{0}%") + " of that damage is converted into Fury. This effect also works when the Fury mode or the Gardian mode is active.";
            string ability_InnerRageMasteryDesc = ColorHelper.SetMastery("[Mastery]") + Environment.NewLine
                + "When this effect occurs, you have a " + ColorHelper.SetUtility("{0}%") + " chance to gain one stack of the " + ColorHelper.SetBuff("Enrage") + " buff, lasting "
                + ColorHelper.SetUtility("{1}s") + " (" + ColorHelper.SetUtility("{2}s") + " if the Fury mod is active). The activation chance is determined by your Mastery stat.";
            PantheraTokens.Add("ability_InnerRageName", ability_InnerRageName);
            PantheraTokens.Add("ability_InnerRageDesc", ability_InnerRageDesc);
            PantheraTokens.Add("ability_InnerRageMasteryDesc", ability_InnerRageMasteryDesc);
            #endregion

            #region Infernal Swipe
            string ability_InfernalSwipeName = "Infernal Swipe";
            string ability_InfernalSwipeDesc = "The Fury mode ignite P4N7H3R4's claws, " + ColorHelper.SetUtility(skill_RipName) + " deals " + ColorHelper.SetDamage("{0}%")
                + " additional fire damage and has a " + ColorHelper.SetDamage("{1}%") + " chance to apply the " + ColorHelper.SetDeBuff("Ignition") + " debuff to the affected enemies.";
            PantheraTokens.Add("ability_InfernalSwipeName", ability_InfernalSwipeName);
            PantheraTokens.Add("ability_InfernalSwipeDesc", ability_InfernalSwipeDesc);
            #endregion

            #region Heat Wave
            string ability_HeatWaveName = "Heat Wave";
            string ability_HeatWaveDesc = "The effect of " + ColorHelper.SetUtility(ability_InfernalSwipeName) + " also apply to " + ColorHelper.SetUtility(skill_AirCleaveName) + ".";
            PantheraTokens.Add("ability_HeatWaveName", ability_HeatWaveName);
            PantheraTokens.Add("ability_HeatWaveDesc", ability_HeatWaveDesc);
            #endregion

            #region Kinetic Resorption
            string ability_KineticResorptionName = "Kinetic Resorption";
            string ability_KineticResorptionDesc = "Absorbs the impact energy of " + ColorHelper.SetUtility(ability_ShieldBashName) + ", converting " + ColorHelper.SetShield("{0}%") + " of the damage into Shield";
            PantheraTokens.Add("ability_KineticResorptionName", ability_KineticResorptionName);
            PantheraTokens.Add("ability_KineticResorptionDesc", ability_KineticResorptionDesc);
            #endregion

            #region Extended Protection
            string ability_ExtendedProtectionName = "Extended Protection";
            string ability_ExtendedProtectionDesc = "Extends the protection of the " + ColorHelper.SetUtility(ability_FrontShieldName) + ", which also absorbs " + ColorHelper.SetShield("{0}%") + " of the damage you take when active.";
            PantheraTokens.Add("ability_ExtendedProtectionName", ability_ExtendedProtectionName);
            PantheraTokens.Add("ability_ExtendedProtectionDesc", ability_ExtendedProtectionDesc);
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
            PantheraTokens.Add("combo_ArcaneAnchorName", "Arcane Anchor");
            PantheraTokens.Add("combo_ConvergenceHookName", "Convergence Hook");
            PantheraTokens.Add("combo_MassiveHookName", "Massive Hook");
            PantheraTokens.Add("combo_AmbitionName", "Ambition");
            PantheraTokens.Add("combo_PortalSurgeName", "Portal Surge");
            PantheraTokens.Add("combo_FeralBiteName", "Feral Bite");
            #endregion


            // ------------------------------------------ BUFFS --------------------------------- //
            #region Eclipse
            string buff_EclipseName = "Eclipse";
            string buff_EclipseDesc = "The next " + ColorHelper.SetUtility(ability_ProwlName) + " can be used in combat.";
            PantheraTokens.Add("buff_EclipseName", buff_EclipseName);
            PantheraTokens.Add("buff_EclipseDesc", buff_EclipseDesc);
            #endregion

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
            string buff_RazorsDesc = "The next time " + ColorHelper.SetUtility(skill_RipName) + " is used, it applies " + ColorHelper.SetDeBuff("Bleed Out")
                + " to the affected enemy, consuming one stack of the " + ColorHelper.SetBuff("Razors") + " buff.";
            PantheraTokens.Add("buff_RazorsName", buff_RazorsName);
            PantheraTokens.Add("buff_RazorsDesc", buff_RazorsDesc);
            #endregion

            #region Mortal Mirage
            string buff_MortalMirageName = "Mortal Mirage";
            string buff_MortalMirageDesc = "If an enemy affected by Mortal Mirage dies, the cooldown of " + ColorHelper.SetUtility(ability_ProwlName) + " is reset, and you gain the " + ColorHelper.SetBuff("Eclipse") + " buff.";
            PantheraTokens.Add("buff_MortalMirageName", buff_MortalMirageName);
            PantheraTokens.Add("buff_MortalMirageDesc", buff_MortalMirageDesc);
            #endregion

            #region Bleed Out
            string buff_BleedOutName = "Bleed Out";
            string buff_BleedOutDesc = "Inflicts " + ColorHelper.SetDamage("{0}%") + " bleeding damage every " + ColorHelper.SetUtility("{1}s") + ".";
            PantheraTokens.Add("buff_BleedOutName", buff_BleedOutName);
            PantheraTokens.Add("buff_BleedOutDesc", buff_BleedOutDesc);
            #endregion

            #region Frozen Paws
            string buff_FrozenPawsName = "Frozen Paws";
            string buff_FrozenPawsDesc = "Intense cold freezes the air beneath P4N7H3R4's paws, creating a walkable platform. This buff is removed if you jump.";
            PantheraTokens.Add("buff_FrozenPawsName", buff_FrozenPawsName);
            PantheraTokens.Add("buff_FrozenPawsDesc", buff_FrozenPawsDesc);
            #endregion

            #region Regeneration
            string buff_RegenerationName = "Regeneration";
            string buff_RegenerationDesc = "Heals " + ColorHelper.SetHeal("{0}%") + " of max Health per second.";
            PantheraTokens.Add("buff_RegenerationName", buff_RegenerationName);
            PantheraTokens.Add("buff_RegenerationDesc", buff_RegenerationDesc);
            #endregion

            #region Resilience
            string buff_ResilienceName = "Resilience";
            string buff_ResilienceDesc = "Increases your Armor by " + ColorHelper.SetUtility("{0}%") + ".";
            PantheraTokens.Add("buff_ResilienceName", buff_ResilienceName);
            PantheraTokens.Add("buff_ResilienceDesc", buff_ResilienceDesc);
            #endregion

            #region Enrage
            string buff_EnrageName = "Enrage";
            string buff_EnrageDesc = "Generates " + ColorHelper.SetFury("{0}%") + " of max Fury per second. This Buff also works when the Fury mode or the Gardian mode is active.";
            PantheraTokens.Add("buff_EnrageName", buff_EnrageName);
            PantheraTokens.Add("buff_EnrageDesc", buff_EnrageDesc);
            #endregion

            #region Ignition
            string buff_IgnitionName = "Ignition";
            string buff_IgnitionDesc = "Inflicts " + ColorHelper.SetDamage("{0}%") + " fire damage every " + ColorHelper.SetUtility("{1}s") + ".";
            PantheraTokens.Add("buff_IgnitionName", buff_IgnitionName);
            PantheraTokens.Add("buff_IgnitionDesc", buff_IgnitionDesc);
            #endregion

        }

    }
}
