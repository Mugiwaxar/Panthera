using Panthera;
using Panthera.Components;
using Panthera.GUI;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Components
{

    internal class Preset
    {

        public static Preset SelectedPreset;
        public static Preset ActivePreset;

        public ConfigPanel configPanel;
        public int presetID;

        #region Stats
        public float maxHealth
        {
            get
            {
                float maxHealth = PantheraConfig.Default_MaxHealth;
                if (this.getAbilityLevel(PantheraConfig.GuardianAbilityID) > 0)
                    maxHealth += PantheraConfig.Guardian_addedHealth;
                return maxHealth;
            }
        }
        public float maxHealthLevel
        {
            get
            {
                float maxHealthLevel = PantheraConfig.Default_MaxHealthLevel;
                if (this.getAbilityLevel(PantheraConfig.GuardianAbilityID) > 0)
                    maxHealthLevel += PantheraConfig.Guardian_addedHealthLevel;
                return maxHealthLevel;
            }
        }
        public float healthRegen
        {
            get
            {
                float healthRegen = PantheraConfig.Default_HealthRegen;
                if (this.getAbilityLevel(PantheraConfig.GuardianAbilityID) > 0)
                    healthRegen += PantheraConfig.Guardian_addedHealthRegen;
                return healthRegen;
            }
        }
        public float healthRegenLevel
        {
            get
            {
                float healthRegenLevel = PantheraConfig.Default_HealthRegenLevel;
                if (this.getAbilityLevel(PantheraConfig.GuardianAbilityID) > 0)
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
                if (PantheraObj.Instance != null && PantheraObj.Instance.characterBody != null && PantheraObj.Instance.characterBody.maxHealth > 0)
                    return PantheraObj.Instance.characterBody.maxHealth * PantheraConfig.FrontShield_maxShieldHealthPercent;

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
                if (this.getAbilityLevel(PantheraConfig.DestructionAbilityID) > 0)
                    damage += PantheraConfig.Destruction_addedDamage;
                return damage;
            }
        }
        public float damageLevel
        {
            get
            {
                float damageLevel = PantheraConfig.Default_DamageLevel;
                if (this.getAbilityLevel(PantheraConfig.DestructionAbilityID) > 0)
                    damageLevel += PantheraConfig.Destruction_addedDamageLevel;
                return damageLevel;
            }
        }
        public float attackSpeed
        {
            get
            {
                float attackSpeed = PantheraConfig.Default_AttackSpeed;
                if (this.getAbilityLevel(PantheraConfig.DestructionAbilityID) > 0)
                    attackSpeed += PantheraConfig.Destruction_addedAttackSpeed;
                return attackSpeed;
            }
        }
        public float attackSpeedLevel
        {
            get
            {
                float attackSpeedLevel = PantheraConfig.Default_AttackSpeedLevel;
                if (this.getAbilityLevel(PantheraConfig.DestructionAbilityID) > 0)
                    attackSpeedLevel += PantheraConfig.Destruction_addedAttackSpeedLevel;
                return attackSpeedLevel;
            }
        }
        public float critic
        {
            get
            {
                return PantheraConfig.Default_Critic;
            }
        }
        public float criticLevel
        {
            get
            {
                return PantheraConfig.Default_CriticLevel;
            }
        }
        public float defense
        {
            get
            {
                float defense = PantheraConfig.Default_Defense;
                if (this.getAbilityLevel(PantheraConfig.GuardianAbilityID) > 0)
                    defense += PantheraConfig.Guardian_addedDefense;
                return defense;
            }
        }
        public float defenseLevel
        {
            get
            {
                float defenseLevel = PantheraConfig.Default_DefenseLevel;
                if (this.getAbilityLevel(PantheraConfig.GuardianAbilityID) > 0)
                    defenseLevel += PantheraConfig.Guardian_addedDefenseLevel;
                return defenseLevel;
            }
        }
        public float jumpCount
        {
            get
            {
                return PantheraConfig.Default_jumpCount;
            }
        }
        #endregion

        // (Ability ID 1 - ??, int unlockLevel) Represent a list of all unlocked Ability with level)
        public Dictionary<int, int> unlockedAbilitiesList = new Dictionary<int, int>();
        // (Skill ID 1 - ??, PantheraSkill Object) Represent a list of all unlocked skills)
        public Dictionary<int, PantheraSkill> unlockedSkillsList = new Dictionary<int, PantheraSkill>();
        // (Slot ID 1 - 20, PantheraSkill Object) Represent a list of all Skill Bar slot linked with the Skill inserted //
        public Dictionary<int, PantheraSkill> slotsSkillsLinkList = new Dictionary<int, PantheraSkill>();

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
                this.slotsSkillsLinkList.Add(1, PantheraSkill.SkillDefsList[PantheraConfig.Rip_SkillID]);
                this.slotsSkillsLinkList.Add(2, PantheraSkill.SkillDefsList[PantheraConfig.AirCleave_SkillID]);
                this.slotsSkillsLinkList.Add(3, PantheraSkill.SkillDefsList[PantheraConfig.Leap_SkillID]);
                this.slotsSkillsLinkList.Add(4, PantheraSkill.SkillDefsList[PantheraConfig.MightyRoar_SkillID]);

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
            this.buildSkillList();

            // Save //
            PantheraSaveSystem.SavePreset(this.presetID, this.saveData());

        }

        public void readData(Dictionary<string, string> dataList)
        {
            // Read all the Data //
            foreach(KeyValuePair<string, string> entry in dataList)
            {
                // Check if this is an Ability //
                if(entry.Key.Contains("Ability"))
                {
                    // Get the ID //
                    string abilityIDString = entry.Key.Replace("Ability", "");
                    int abilityID = Int32.Parse(abilityIDString);
                    // Get the Level //
                    int level = Int32.Parse(entry.Value);
                    // Register to the List //
                    this.unlockedAbilitiesList.Add(abilityID, level);
                }
                // Check if this is a Slot //
                if (entry.Key.Contains("Slot"))
                {
                    // Get the ID //
                    string slotIDString = entry.Key.Replace("Slot", "");
                    int slotID = Int32.Parse(slotIDString);
                    // Get the Skill //
                    int skillID = Int32.Parse(entry.Value);
                    PantheraSkill skill = PantheraSkill.SkillDefsList.ContainsKey(skillID) ? PantheraSkill.SkillDefsList[skillID] : null;
                    // Register the Skill inside the Slot //
                    this.slotsSkillsLinkList.Add(slotID, skill);
                }
            }

        }

        public Dictionary<string, string> saveData()
        {
            // Create the Dictionary //
            Dictionary<string, string> data = new Dictionary<string, string>();

            // Add the Abilities //
            foreach (KeyValuePair<int, int> entry in this.unlockedAbilitiesList)
            {
                data.Add("Ability" + entry.Key, entry.Value.ToString());
            }

            // Add the Skill Bars //
            foreach (KeyValuePair<int, PantheraSkill> entry in this.slotsSkillsLinkList)
            {
                if (entry.Value != null)
                    data.Add("Slot" + entry.Key, entry.Value.skillID.ToString());
            }

            // Return the Data //
            return data;


        }

        public void buildSkillList()
        {

            // Clear the Skill List //
            this.unlockedSkillsList.Clear();

            // Add all defauld Skills //
            this.unlockedSkillsList.Add(PantheraConfig.Rip_SkillID, PantheraSkill.SkillDefsList[PantheraConfig.Rip_SkillID]);
            this.unlockedSkillsList.Add(PantheraConfig.AirCleave_SkillID, PantheraSkill.SkillDefsList[PantheraConfig.AirCleave_SkillID]);
            this.unlockedSkillsList.Add(PantheraConfig.Leap_SkillID, PantheraSkill.SkillDefsList[PantheraConfig.Leap_SkillID]);
            this.unlockedSkillsList.Add(PantheraConfig.MightyRoar_SkillID, PantheraSkill.SkillDefsList[PantheraConfig.MightyRoar_SkillID]);

            // Add ClawStorm //
            if (this.getAbilityLevel(PantheraConfig.DestructionAbilityID) > 0)
                this.unlockedSkillsList.Add(PantheraConfig.ClawsStorm_SkillID, PantheraSkill.SkillDefsList[PantheraConfig.ClawsStorm_SkillID]);

            // Add Front Shield //
            if (this.getAbilityLevel(PantheraConfig.GuardianAbilityID) > 0)
                this.unlockedSkillsList.Add(PantheraConfig.FrontShield_SkillID, PantheraSkill.SkillDefsList[PantheraConfig.FrontShield_SkillID]);

            // Add Prowl //
            if (this.getAbilityLevel(PantheraConfig.RuseAbilityID) > 0)
                this.unlockedSkillsList.Add(PantheraConfig.Prowl_SkillID, PantheraSkill.SkillDefsList[PantheraConfig.Prowl_SkillID]);

            // Add Furious Bite //
            if (this.getAbilityLevel(PantheraConfig.RuseAbilityID) > 0)
                this.unlockedSkillsList.Add(PantheraConfig.FuriousBite_SkillID, PantheraSkill.SkillDefsList[PantheraConfig.FuriousBite_SkillID]);

        }

        public void addAbilityPoint(int abilityID)
        {
            if (this.unlockedAbilitiesList.ContainsKey(abilityID)) this.unlockedAbilitiesList[abilityID] += 1;
            else this.unlockedAbilitiesList.Add(abilityID, 1);
        }

        public int getAbilityLevel(int abilityID)
        {
            if (this.unlockedAbilitiesList.ContainsKey(abilityID)) return this.unlockedAbilitiesList[abilityID];
            else return 0;
        }

        public void addSkillToSlot(int slotID, PantheraSkill skill)
        {
            if (this.slotsSkillsLinkList.ContainsKey(slotID)) this.slotsSkillsLinkList.Remove(slotID);
            this.slotsSkillsLinkList.Add(slotID, skill);
            this.configPanel.updateSkillBars();
            PantheraSaveSystem.SavePreset(this.presetID, this.saveData());
        }

        public void removeSkillFromSlot(int slotID)
        {
            if (this.slotsSkillsLinkList.ContainsKey(slotID)) this.slotsSkillsLinkList.Remove(slotID);
            this.configPanel.updateSkillBars();
            PantheraSaveSystem.SavePreset(this.presetID, this.saveData());
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
            if (actionID == PantheraConfig.Skill1Key && slotsSkillsLinkList.ContainsKey(1 + j)) return slotsSkillsLinkList[1 + j];
            if (actionID == PantheraConfig.Skill2Key && slotsSkillsLinkList.ContainsKey(2 + j)) return slotsSkillsLinkList[2 + j];
            if (actionID == PantheraConfig.Skill3Key && slotsSkillsLinkList.ContainsKey(3 + j)) return slotsSkillsLinkList[3 + j];
            if (actionID == PantheraConfig.Skill4Key && slotsSkillsLinkList.ContainsKey(4 + j)) return slotsSkillsLinkList[4 + j];
            if (actionID == PantheraConfig.Skill5Key && slotsSkillsLinkList.ContainsKey(5 + j)) return slotsSkillsLinkList[5 + j];
            if (actionID == PantheraConfig.Skill6Key && slotsSkillsLinkList.ContainsKey(6 + j)) return slotsSkillsLinkList[6 + j];
            if (actionID == PantheraConfig.Skill7Key && slotsSkillsLinkList.ContainsKey(7 + j)) return slotsSkillsLinkList[7 + j];
            if (actionID == PantheraConfig.Skill8Key && slotsSkillsLinkList.ContainsKey(8 + j)) return slotsSkillsLinkList[8 + j];
            if (actionID == PantheraConfig.Skill9Key && slotsSkillsLinkList.ContainsKey(9 + j)) return slotsSkillsLinkList[9 + j];
            if (actionID == PantheraConfig.Skill10Key && slotsSkillsLinkList.ContainsKey(10 + j)) return slotsSkillsLinkList[10 + j];
            return null;
        }

    }
}
