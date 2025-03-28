﻿using EntityStates;
using Newtonsoft.Json.Linq;
using Panthera.Abilities;
using Panthera.BodyComponents;
using Panthera.Combos;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.Utils;
using R2API.Networking.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Panthera.Components
{
    public class ProfileComponent : MonoBehaviour
    {

        public PantheraBody body
        {
            get
            {
                return base.gameObject.GetComponent<PantheraBody>();
            }
        }

        // (Ability ID 1 - ??, int unlockLevel) Represent a list of all unlocked Abilities with level)
        public Dictionary<int, int> unlockedAbilitiesList = new Dictionary<int, int>();

        // (Ability ID 1 - ??, bool mastery) Represent a list of all Skills with Mastery enabled)
        public Dictionary<int, bool> masteryAbilitiesList = new Dictionary<int, bool>();

        // (Ability ID 1 - ??, bool mastery) This is a cached list that represent all unlocked/locked Skills)
        private Dictionary<int, bool> unlockedSkillsList = new Dictionary<int, bool>();

        // (Ability ID 1 - ??, bool mastery) This is a cached list that represent all disabled Skills)
        private List<int> disabledSkillsList = new List<int>();

        public int totalMasteryPoints = 0;

        #region Attributs
        public int endurance = 0;
        public int force = 0;
        public int agility = 0;
        public int swiftness = 0;
        public int dexterity = 0;
        public int spirit = 0;
        #endregion

        #region Stats
        public float getMaxHealth(int level = 0)
        {
            float mult1 = 0;
            mult1 += this.endurance * 0.15f;

            float mult2 = 2 * Mathf.Log(mult1 * 0.13f + 1, 3) + 1;

            if (level > 0 && this.body != null)
                return body.maxHealth;
            else if (this.body != null)
                return body.maxHealth * mult2;
            else
                return (PantheraConfig.Default_MaxHealth + (PantheraConfig.Default_MaxHealthLevel * level)) * mult2;
        }
        public float getMaxHealthLevel()
        {
            return PantheraConfig.Default_MaxHealthLevel;
        }
        public float getHealthRegen(int level = 0)
        {
            float mult1 = 0;
            mult1 += this.endurance * 0.1f;
            mult1 += this.spirit * 0.15f;

            float mult2 = 10 * Mathf.Log(mult1 * 0.02f + 1, 3) + 1;

            if (level > 0 && this.body != null)
                return body.regen;
            else if (this.body != null)
                return body.regen * mult2;
            else
                return (PantheraConfig.Default_HealthRegen + (PantheraConfig.Default_HealthRegenLevel * level)) * mult2;
        }
        public float getHealthRegenLevel()
        {
            return PantheraConfig.Default_HealthRegenLevel;
        }
        public float getMaxFury(int level = 0)
        {
            if (this.body != null)
                return (PantheraConfig.Default_MaxFury + (PantheraConfig.Default_MaxFuryLevel * (this.body.level - 1)));
            else
                return (PantheraConfig.Default_MaxFury + (PantheraConfig.Default_MaxFuryLevel * level));
        }
        public float getMaxFuryLevel()
        {
            return PantheraConfig.Default_MaxFuryLevel;
        }
        public float getMaxFrontShield(int level = 0)
        {
            float shieldHealthPercent = PantheraConfig.FrontShield_maxShieldHealthPercent;
            if (this.body != null)
            {
                int improvedShieldAbilityLevel = this.getAbilityLevel(PantheraConfig.ImprovedShield_AbilityID);
                if (improvedShieldAbilityLevel == 1) shieldHealthPercent += PantheraConfig.ImprovedShield_addedPercent1 * this.body.level;
                else if (improvedShieldAbilityLevel == 2) shieldHealthPercent += PantheraConfig.ImprovedShield_addedPercent2 * this.body.level;
                else if (improvedShieldAbilityLevel == 3) shieldHealthPercent += PantheraConfig.ImprovedShield_addedPercent3 * this.body.level;
                return this.body.maxHealth * shieldHealthPercent;
            }
            else
            {
                int improvedShieldAbilityLevel = Panthera.ProfileComponent.getAbilityLevel(PantheraConfig.ImprovedShield_AbilityID);
                if (improvedShieldAbilityLevel == 1) shieldHealthPercent += PantheraConfig.ImprovedShield_addedPercent1 * (level + 1);
                else if (improvedShieldAbilityLevel == 2) shieldHealthPercent += PantheraConfig.ImprovedShield_addedPercent2 * (level + 1);
                else if (improvedShieldAbilityLevel == 3) shieldHealthPercent += PantheraConfig.ImprovedShield_addedPercent3 * (level + 1);
                return getMaxHealth(level) * shieldHealthPercent;
            }
        }
        public float getMoveSpeed(int level = 0)
        {
            float mult1 = 0;
            mult1 += this.agility * 0.05f;
            mult1 += this.swiftness * 0.15f;

            float mult2 = 0.75f * Mathf.Log(mult1 * 0.7f + 1, 10) + 1;

            if (level > 0 && this.body != null)
                return body.moveSpeed;
            else if (this.body != null)
                return body.moveSpeed * mult2;
            else
                return (PantheraConfig.Default_MoveSpeed + (PantheraConfig.Default_MoveSpeedLevel * level)) * mult2;
        }
        public float getMoveSpeedLevel()
        {
            return PantheraConfig.Default_MoveSpeedLevel;
        }
        public float getDamage(int level = 0)
        {
            float mult1 = 0;
            mult1 += this.force * 0.15f;
            mult1 += this.dexterity * 0.05f;

            float mult2 = 0.75f * Mathf.Log(mult1 * 0.2f + 1, 2) + 1;

            if (level > 0 && this.body != null)
                return body.damage;
            else if (this.body != null)
                return body.damage * mult2;
            else
                return (PantheraConfig.Default_Damage + (PantheraConfig.Default_DamageLevel * level)) * mult2;
        }
        public float getDamageLevel()
        {
            return PantheraConfig.Default_DamageLevel;
        }
        public float getAttackSpeed(int level = 0)
        {
            float mult1 = 0;
            mult1 += this.swiftness * 0.1f;

            float mult2 = 1.2f * Mathf.Log(mult1 * 0.25f + 1, 5) + 1;

            if (level > 0 && this.body != null)
                return body.attackSpeed;
            else if (this.body != null)
                return body.attackSpeed * mult2;
            else
                return (PantheraConfig.Default_AttackSpeed + (PantheraConfig.Default_AttackSpeedLevel * level)) * mult2;
        }
        public float getAttackSpeedLevel()
        {
            return PantheraConfig.Default_AttackSpeedLevel;
        }
        public float getCritic(int level = 0)
        {
            float mult1 = 0;
            mult1 += this.agility * 0.15f;
            mult1 += this.dexterity * 0.1f;

            float mult2 = Mathf.Log(mult1 * 0.8f + 1, 5) + 1;

            if (level > 0 && this.body != null)
                return body.crit;
            else if (this.body != null)
                return body.crit * mult2;
            else
                return (PantheraConfig.Default_Critic + (PantheraConfig.Default_CriticLevel * level)) * mult2;
        }
        public float getCriticLevel()
        {
            return PantheraConfig.Default_CriticLevel;
        }
        public float getDodge(int level = 0)
        {
            float mult1 = 0;
            mult1 += this.agility * 0.1f;
            mult1 += this.dexterity * 0.15f;

            float mult2 = 2.5f * Mathf.Log(mult1 * 0.25f + 1, 3) + 1;

            if (this.body != null)
                return (PantheraConfig.Default_Dodge + (PantheraConfig.Default_DodgeLevel * (this.body.level - 1))) * mult2;
            else
                return (PantheraConfig.Default_Dodge + (PantheraConfig.Default_DodgeLevel * level)) * mult2;
        }
        public float getDodgeLevel()
        {
            return PantheraConfig.Default_DodgeLevel;
        }
        public float getDefense(int level = 0)
        {
            float mult1 = 0;
            mult1 += this.force * 0.15f;

            float mult2 = 1.5f * Mathf.Log(mult1 * 0.3f + 1, 3) + 1;

            if (level > 0 && this.body != null)
                return body.armor;
            else if (this.body != null)
                return body.armor * mult2;
            else
                return (PantheraConfig.Default_Defense + (PantheraConfig.Default_DefenseLevel * level)) * mult2;
        }
        public float getDefenseLevel()
        {
            return PantheraConfig.Default_DefenseLevel;
        }
        public float getMastery(int level = 0)
        {
            float mult1 = 0;
            mult1 += this.spirit * 0.2f;

            float mult2 = 3 * Mathf.Log(mult1 * 0.5f + 1, 2) + 1;

            if (this.body != null)
                return (PantheraConfig.Default_Mastery + (PantheraConfig.Default_MasteryLevel * (this.body.level - 1))) * mult2;
            else
                return (PantheraConfig.Default_Mastery + (PantheraConfig.Default_MasteryLevel * level)) * mult2;
        }
        public float getMasteryLevel()
        {
            return PantheraConfig.Default_MasteryLevel;
        }
        public float getJumpCount()
        {
            return PantheraConfig.Default_jumpCount;
        }
        #endregion

        public void loadAttributes()
        {

            // Load the File //
            PantheraSaveSystem.Load();

            // Load Endurance //
            string enduranceString = PantheraSaveSystem.ReadValue(PantheraConfig.SP_Endurance);
            int enduranceValue = Utils.Functions.StringToInt(enduranceString);
            this.endurance = Math.Max(enduranceValue, 0);

            // Load Force //
            string forceString = PantheraSaveSystem.ReadValue(PantheraConfig.SP_Force);
            int forceValue = Utils.Functions.StringToInt(forceString);
            this.force = Math.Max(forceValue, 0);

            // Load Agility //
            string agilityString = PantheraSaveSystem.ReadValue(PantheraConfig.SP_Agility);
            int agilityValue = Utils.Functions.StringToInt(agilityString);
            this.agility = Math.Max(agilityValue, 00);

            // Load Swiftness //
            string swiftnessString = PantheraSaveSystem.ReadValue(PantheraConfig.SP_Swiftness);
            int swiftnessValue = Utils.Functions.StringToInt(swiftnessString);
            this.swiftness = Math.Max(swiftnessValue, 0);

            // Load Dexterity //
            string dexterityString = PantheraSaveSystem.ReadValue(PantheraConfig.SP_Dexterity);
            int dexterityValue = Utils.Functions.StringToInt(dexterityString);
            this.dexterity = Math.Max(dexterityValue, 0);

            // Load Spirit //
            string spiritString = PantheraSaveSystem.ReadValue(PantheraConfig.SP_Spirit);
            int spiritValue = Utils.Functions.StringToInt(spiritString);
            this.spirit = Math.Max(spiritValue, 0);

        }

        public void saveAttributes()
        {

            // Save Endurance //
            PantheraSaveSystem.SetValue(PantheraConfig.SP_Endurance, this.endurance.ToString());

            // Save Force //
            PantheraSaveSystem.SetValue(PantheraConfig.SP_Force, this.force.ToString());

            // Save Agility //
            PantheraSaveSystem.SetValue(PantheraConfig.SP_Agility, this.agility.ToString());

            // Save Swiftness //
            PantheraSaveSystem.SetValue(PantheraConfig.SP_Swiftness, this.swiftness.ToString());

            // Save Dexterity //
            PantheraSaveSystem.SetValue(PantheraConfig.SP_Dexterity, this.dexterity.ToString());

            // Save Spirit //
            PantheraSaveSystem.SetValue(PantheraConfig.SP_Spirit, this.spirit.ToString());

            // Save the File //
            PantheraSaveSystem.Save();

        }

        public void loadSkillsTree()
        {

            // Load the File //
            PantheraSaveSystem.Load();

            // Clear the Lists //
            this.unlockedAbilitiesList.Clear();
            this.masteryAbilitiesList.Clear();

            // Load the Skills Tree //
            foreach (KeyValuePair<string, string> kvp in PantheraSaveSystem.savedData)
            {

                if (kvp.Key.Contains("_ABILITY_"))
                {
                    try
                    {
                        int ID = int.Parse(kvp.Key.Replace("_ABILITY_", ""));
                        int level = int.Parse(kvp.Value);
                        this.unlockedAbilitiesList[ID] = level;
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("Unable to Load a value -> " + kvp.Key);
                        Debug.LogError(e);
                    }
                }

                if (kvp.Key.Contains("_MASTERY_"))
                {
                    try
                    {
                        int ID = int.Parse(kvp.Key.Replace("_MASTERY_", ""));
                        bool value = bool.Parse(kvp.Value);
                        this.masteryAbilitiesList[ID] = value;
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("Unable to Load a value -> " + kvp.Key);
                        Debug.LogError(e);
                    }
                }

            }


            // Add the Feline Skills Ability //
            this.unlockedAbilitiesList[PantheraConfig.FelineSkills_AbilityID] = 1;

            // Load Specials //
            if (Panthera.PantheraCharacter.characterLevel >= PantheraConfig.UntamedSpitit_unlockLevel)
                this.unlockedAbilitiesList[PantheraConfig.UntamedSpirit_AbilityID] = 1;
            else
                this.unlockedAbilitiesList[PantheraConfig.UntamedSpirit_AbilityID] = 0;
            if (Panthera.PantheraCharacter.characterLevel >= PantheraConfig.GodPower_unlockLevel)
                this.unlockedAbilitiesList[PantheraConfig.GodPower_AbilityID] = 1;
            else
                this.unlockedAbilitiesList[PantheraConfig.GodPower_AbilityID] = 0;
            if (Panthera.PantheraCharacter.characterLevel >= PantheraConfig.PortalSurge_unlockLevel)
                this.unlockedAbilitiesList[PantheraConfig.PortalSurge_AbilityID] = 1;
            else
                this.unlockedAbilitiesList[PantheraConfig.PortalSurge_AbilityID] = 0;

            // Load Mastery Points //
            string stringValue = PantheraSaveSystem.ReadValue(PantheraConfig.SP_CharacterMastery);
            this.totalMasteryPoints = (int)Utils.Functions.StringToFloat(stringValue);

        }

        public void saveSkillsTree()
        {

            // Add all Abilities to the Table //
            foreach (int ID in this.unlockedAbilitiesList.Keys)
            {
                PantheraSaveSystem.SetValue("_ABILITY_" + ID.ToString(), this.unlockedAbilitiesList[ID].ToString());
            }

            // Add all Masteries //
            foreach (int ID in this.masteryAbilitiesList.Keys)
            {
                PantheraSaveSystem.SetValue("_MASTERY_" + ID.ToString(), this.masteryAbilitiesList[ID].ToString());
            }

            // Add Mastery Points //
            PantheraSaveSystem.SetValue(PantheraConfig.SP_CharacterMastery, this.totalMasteryPoints.ToString());

            // Save the File //
            PantheraSaveSystem.Save();

        }

        public int getTotalAttributesUsed()
        {
            return this.endurance + this.force + this.agility + this.swiftness + this.dexterity + this.spirit;
        }

        public int getTotalSkillPointsUsed()
        {
            int i = 0;
            foreach (KeyValuePair<int, int> pair in this.unlockedAbilitiesList)
            {
                if (pair.Key > 1)
                    i += pair.Value;
            }
            return i;
        }

        public int getTotalMasteryPointsUsed()
        {
            int i = 0;
            foreach (bool set in this.masteryAbilitiesList.Values)
            {
                if (set == true)
                    i++;
            }
            return i;
        }

        public int getMasteryPointsLeft()
        {
            return this.totalMasteryPoints - this.getTotalMasteryPointsUsed();
        }

        public Dictionary<String, int> serialize()
        {
            // Create the Dictionary //
            Dictionary<String, int> dico = new Dictionary<String, int>();

            // Add all Attributes //
            dico.Add("_endurance", this.endurance);
            dico.Add("_force", this.force);
            dico.Add("_agility", this.agility);
            dico.Add("_swiftness", this.swiftness);
            dico.Add("_dexterity", this.dexterity);
            dico.Add("_spirit", this.spirit);

            // Add Mastery Points //
            dico.Add("_Masery", this.totalMasteryPoints);

            // Add all Abilities //
            foreach (KeyValuePair<int, int> pair in this.unlockedAbilitiesList)
            {
                dico.Add("_Ability_" + pair.Key, pair.Value);
            }

            // Add all Masteries //
            foreach (KeyValuePair<int, bool> pair in this.masteryAbilitiesList)
            {
                dico.Add("_Mastery_" + pair.Key, pair.Value == true ? 1 : 0);
            }

            // Return //
            return dico;

        }

        public void deserialize(Dictionary<String, int> dico)
        {

            // Get all Attriutes //
            this.endurance = dico["_endurance"];
            this.force = dico["_force"];
            this.agility = dico["_agility"];
            this.swiftness = dico["_swiftness"];
            this.dexterity = dico["_dexterity"];
            this.spirit = dico["_spirit"];

            // Get Mastery Points //
            this.totalMasteryPoints = dico["_Masery"];

            // Get all Abilities and Masteries //
            this.unlockedAbilitiesList.Clear();
            this.masteryAbilitiesList.Clear();
            foreach (KeyValuePair<String, int> pair in dico)
            {
                if (pair.Key.Contains("_Ability_") == true)
                {
                    string key = pair.Key.Replace("_Ability_", "");
                    int keyInt = Utils.Functions.StringToInt(key);
                    this.unlockedAbilitiesList.Add(keyInt, pair.Value);
                }

                if (pair.Key.Contains("_Mastery_") == true)
                {
                    string key = pair.Key.Replace("_Mastery_", "");
                    int keyInt = Utils.Functions.StringToInt(key);
                    this.masteryAbilitiesList.Add(keyInt, pair.Value == 1 ? true : false);
                }

            }

        }

        public void syncProfile()
        {

            // Check the Panthera Object //
            if (Panthera.PantheraCharacter.pantheraObj == null)
                return;

            // Get the Panthera Object Profile //
            ProfileComponent ptraProfile = Panthera.PantheraCharacter.pantheraObj.profileComponent;

            // Check the Panthera Object Profile //
            if (ptraProfile == null)
                return;

            // Sync the Profile //
            ProfileComponent profile2 = Panthera.ProfileComponent;
            ptraProfile.deserialize(profile2.serialize());

            // Send the Profile to the Server //
            new ServerSyncProfile(ptraProfile.gameObject, ptraProfile.serialize()).Send(R2API.Networking.NetworkDestination.Server);

        }

        public int getAbilityLevel(int abilityID)
        {
            if (this.unlockedAbilitiesList.ContainsKey(abilityID))
                return this.unlockedAbilitiesList[abilityID];
            return 0;
        }
        
        public void updateCachedUnlockedSkillList()
        {
            // Create the Unlocked Skills List //
            this.unlockedSkillsList.Clear();
            foreach (KeyValuePair<int, MachineScript> pair in Panthera.PantheraCharacter.characterSkills.SkillsList)
            {
                bool unlocked = this.getAbilityLevel(pair.Value.requiredAbilityID) > 0 ? true : false;
                this.unlockedSkillsList.Add(pair.Key, unlocked);
            }
            // Save to the Main Profil Component //
            Panthera.ProfileComponent.unlockedSkillsList = this.unlockedSkillsList;
        }
        
        public bool isSkillUnlocked(int skillID)
        {
            return this.unlockedSkillsList[skillID];
        }

        public bool abilityCanBeUpgraded(int abilityID)
        {

            // Get the Ability //
            PantheraAbility ability = Panthera.PantheraCharacter.characterAbilities.AbilityList[abilityID];

            // Check if the Ability Level is not max //
            if (getAbilityLevel(abilityID) >= ability.maxLevel)
                return false;

            // Get the Required Ability //
            PantheraAbility requiredAbility = Panthera.PantheraCharacter.characterAbilities.AbilityList[ability.requiredAbility];

            // Check if the Required Skill is unlocked //
            if (requiredAbility != null && getAbilityLevel(requiredAbility.abilityID) < requiredAbility.maxLevel)
                return false;

            return true;

        }

        public bool isComboUnlocked(PantheraCombo combo)
        {
            foreach(ComboSkill comboSkill in combo.comboSkillsList)
            {
                if(this.isSkillUnlocked(comboSkill.skill.skillID) == false)
                    return false;
            }
            return true;
        }

        public void upgradeAbility(int abilityID)
        {

            // Check if enough Points //
            if (Panthera.PantheraCharacter.skillPointsLeft < 1)
                return;

            // Check if the Ability can be upgraded //
            if (this.abilityCanBeUpgraded(abilityID) == false)
                return;

            // Upgrade the Ability //
            if (this.unlockedAbilitiesList.ContainsKey(abilityID))
                this.unlockedAbilitiesList[abilityID]++;
            else
                this.unlockedAbilitiesList[abilityID] = 1;

            // Get the Required Ability //
            PantheraAbility ability = Panthera.PantheraCharacter.characterAbilities.AbilityList[abilityID];

            // Update the Desc //
            GUI.Tooltips.AbilitiesTooltip.ShowTooltip(ability, true);

        }

        public void resetAttributes()
        {

            // Reset all Attributes //
            this.endurance = 0;
            this.force = 0;
            this.agility = 0;
            this.swiftness = 0;
            this.dexterity = 0;
            this.spirit = 0;

            // Save //
            this.saveAttributes();

            // Sync Profile //
            this.syncProfile();

        }

        public void resetSkillsTree()
        {

            // Set all Abilities to 0 //
            foreach (int key in this.unlockedAbilitiesList.Keys.ToArray())
            {
                this.unlockedAbilitiesList[key] = 0;
            }

            // Set all Masteries to false //
            foreach (int key in this.masteryAbilitiesList.Keys.ToArray())
            {
                this.masteryAbilitiesList[key] = false;
            }

            // Save //
            this.saveSkillsTree();

            // Load //
            this.loadSkillsTree();

            // Sync Profile //
            this.syncProfile();

        }

        public void setMastery(int abilityID, bool set)
        {

            // Check if enough Points //
            if (this.getMasteryPointsLeft() < 1)
                return;

            // Get the Ability //
            PantheraAbility ability = Panthera.PantheraCharacter.characterAbilities.AbilityList[abilityID];

            // Check the Ability //
            if (ability.hasMastery == false) return;

            // Add to the List //
            if (this.masteryAbilitiesList.ContainsKey(abilityID) == true)
                this.masteryAbilitiesList[abilityID] = set;
            else
                this.masteryAbilitiesList.Add(abilityID, set);

        }

        public bool isMastery(int abilityID)
        {
            if (this.masteryAbilitiesList.ContainsKey(abilityID) == true)
                return this.masteryAbilitiesList[abilityID];
            else
                return false;
        }

        public void createDisabledSkillsList()
        {
            foreach (MachineScript skill in Panthera.PantheraCharacter.characterSkills.SkillsList.Values)
            {
                if (skill.activated == false)
                    this.disabledSkillsList.Add(skill.skillID);
            }
            Panthera.ProfileComponent.disabledSkillsList = this.disabledSkillsList;
        }

        public void disableSkill(int skillID, bool disable)
        {
            if(disable == true)
                this.disabledSkillsList.Add(skillID);
            else
                this.disabledSkillsList.Remove(skillID);
            Panthera.ProfileComponent.disabledSkillsList = this.disabledSkillsList;
        }

        public bool isSkillDisabled(int skillID)
        {
            if (this.disabledSkillsList.Contains(skillID) == true)
                return true;
            else
                return false;
        }

        public bool comboHaveSkillDisabled(PantheraCombo combo)
        {
            foreach(ComboSkill comboSkill in combo.comboSkillsList)
            {
                if (this.isSkillDisabled(comboSkill.skill.skillID) == true)
                    return true;
            }
            return false;   
        }

    }
}
