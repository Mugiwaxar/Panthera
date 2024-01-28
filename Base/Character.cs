
using MonoMod.RuntimeDetour.HookGen;
using Panthera;
using Panthera.Abilities;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.GUI;
using Panthera.MachineScripts;
using Panthera.Utils;
using R2API;
using RoR2;
using RoR2.Projectile;
using RoR2.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using static R2API.LoadoutAPI;
using static RoR2.CharacterSelectSurvivorPreviewDisplayController;
using static RoR2.Navigation.NodeGraph;

namespace Panthera.Base
{

    public class Character
    {

        // (int Level, int MaxExperience) Represent a list of all level max Experience //
        public Dictionary<int, int> maxExperienceList = new Dictionary<int, int>();

        public CharacterSkills characterSkills;
        public CharacterAbilities characterAbilities;
        public CharacterCombos characterCombos;
        public PantheraObj pantheraObj;

        public int characterLevel
        {
            get
            {
                int lastValue = 1;
                foreach (KeyValuePair<int, int> entry in this.maxExperienceList)
                {
                    if (this.totalExperience >= entry.Value)
                        lastValue = entry.Key + 1;
                    else
                        break;
                }
                return Math.Min(lastValue, this.maxLevel);
            }
        }
        public int maxLevel
        {
            get
            {
                int i = 0;
                foreach (PantheraAbility ability in characterAbilities.AbilityList.Values)
                {
                    i += ability.maxLevel;
                }
                return i;
            }
        }
        public int levelExperience
        {
            get
            {
                if (this.maxExperienceList.ContainsKey(this.characterLevel - 1) == false) return this.totalExperience;
                return this.totalExperience - this.maxExperienceList[characterLevel - 1];
            }
        }
        public int levelMaxExperience
        {
            get
            {
                if (this.maxExperienceList.ContainsKey(this.characterLevel) == false) return 0;
                if (this.maxExperienceList.ContainsKey(this.characterLevel - 1) == false) return this.maxExperienceList[characterLevel];
                return this.maxExperienceList[this.characterLevel] - this.maxExperienceList[this.characterLevel - 1];
            }
        }
        public int totalExperience
        {
            get
            {
                string stringValue = PantheraSaveSystem.ReadValue(PantheraConfig.SP_CharacterXP);
                int value = (int)Utils.Functions.StringToFloat(stringValue);
                return value;
            }
            set
            {
                PantheraSaveSystem.SetValue(PantheraConfig.SP_CharacterXP, value.ToString());
                PantheraSaveSystem.Save();
            }
        }
        public int totalAttributePoints
        {
            get
            { 
               return this.characterLevel;
            }
        }
        public int usedAttributePoints
        {
            get
            {
                return (int)(this.endurance + this.force + this.agility + this.swiftness + this.dexterity) - 5;
            }
        }
        public int attributePointsLeft
        {
            get
            {
                return this.totalAttributePoints - this.usedAttributePoints;
            }
        }
        public int skillPointsLeft
        {
            get
            {
                return this.characterLevel - this.usedSkillPoints;
            }
        }
        public int usedSkillPoints
        {
            get
            {
                int i = 0;
                foreach (int level in this.characterAbilities.unlockedAbilitiesList.Values)
                {
                    i += level;
                }
                return i;
            }
        }

        public float _serverMaxShield;
        public float maxShield
        {
            get
            {
                //if (ptraObj != null && ptraObj.characterBody != null && ptraObj.characterBody.maxHealth > 0)
                //    return ptraObj.characterBody.maxHealth * frontShield_maxShieldHealthPercent;
                float maxShield = PantheraConfig.Default_MaxShield;
                return maxShield;
            }
        }

        public float jumpCount
        {
            get
            {
                float jumpCount = PantheraConfig.Default_jumpCount;
                return jumpCount;
            }
        }
        public float barrierDecayRateMultiplier
        {
            get
            {
                float rateMultiplier = 1;
                return rateMultiplier;
            }
        }

        #region Attributes
        public float endurance
        {
            get
            {
                string stringValue = PantheraSaveSystem.ReadValue(PantheraConfig.SP_Endurance);
                float value = Utils.Functions.StringToFloat(stringValue);
                return Math.Max(value, 1);
            }
            set
            {
                PantheraSaveSystem.SetValue(PantheraConfig.SP_Endurance, value.ToString());
                PantheraSaveSystem.Save();
            }
        }
        public float force
        {
            get
            {
                string stringValue = PantheraSaveSystem.ReadValue(PantheraConfig.SP_Force);
                float value = Utils.Functions.StringToFloat(stringValue);
                return Math.Max(value, 1);
            }
            set
            {
                PantheraSaveSystem.SetValue(PantheraConfig.SP_Force, value.ToString());
                PantheraSaveSystem.Save();
            }
        }
        public float agility
        {
            get
            {
                string stringValue = PantheraSaveSystem.ReadValue(PantheraConfig.SP_Agility);
                float value = Utils.Functions.StringToFloat(stringValue);
                return Math.Max(value, 1);
            }
            set
            {
                PantheraSaveSystem.SetValue(PantheraConfig.SP_Agility, value.ToString());
                PantheraSaveSystem.Save();
            }
        }
        public float swiftness
        {
            get
            {
                string stringValue = PantheraSaveSystem.ReadValue(PantheraConfig.SP_Swiftness);
                float value = Utils.Functions.StringToFloat(stringValue);
                return Math.Max(value, 1);
            }
            set
            {
                PantheraSaveSystem.SetValue(PantheraConfig.SP_Swiftness, value.ToString());
                PantheraSaveSystem.Save();
            }
        }
        public float dexterity
        {
            get
            {
                string stringValue = PantheraSaveSystem.ReadValue(PantheraConfig.SP_Dexterity);
                float value = Utils.Functions.StringToFloat(stringValue);
                return Math.Max(value, 1);
            }
            set
            {
                PantheraSaveSystem.SetValue(PantheraConfig.SP_Dexterity, value.ToString());
                PantheraSaveSystem.Save();
            }
        }
        #endregion

        #region Stat Multipliers
        public float maxHealthMult
        {
            get
            {
                float mult = 1;
                mult += this.endurance * 0.05f;
                return mult;
            }
        }
        public float healthRegenMult
        {
            get
            {
                float mult = 1;
                mult += this.endurance * 0.03f;
                return mult;
            }
        }
        public float moveSpeedMult
        {
            get
            {
                float mult = 1;
                mult += this.agility * 0.02f;
                mult += this.swiftness * 0.04f;
                return mult;
            }
        }
        public float damageMult
        {
            get
            {
                float mult = 1;
                mult += this.force * 0.05f;
                mult += this.dexterity * 0.02f;
                return mult;
            }
        }
        public float attackSpeedMult
        {
            get
            {
                float mult = 1;
                mult += this.agility * 0.01f;
                mult += this.swiftness * 0.03f;
                return mult;
            }
        }
        public float critMult
        {
            get
            {
                float mult = 1;
                mult += this.agility * 0.02f;
                mult += this.dexterity * 0.04f;
                return mult;
            }
        }
        public float DefenseMult
        {
            get
            {
                float mult = 1;
                mult += this.endurance * 0.02f;
                mult += this.force * 0.04f;
                return mult;
            }
        }
        #endregion

        public int lunarCoin
        {
            get
            {
                return (int)Panthera.LoadedUserProfile.coins;
            }
            set
            {
                Panthera.LoadedUserProfile.coins = (uint)value;
            }
        }

        public Character()
        {

        }

        public void init()
        {
            // Create the Character Skills //
            this.characterSkills = new CharacterSkills();
            // Create the Character Abilities //
            this.characterAbilities = new CharacterAbilities();
            // Create the Character Combos //
            this.characterCombos = new CharacterCombos();
            // Create the level experience list //
            CalculMaxExperiencePerLevel();
        }

        public void CalculMaxExperiencePerLevel()
        {
            int levelMaxXP = 0;
            for (int i = 1; i <= maxLevel; i++)
            {
                if (i > 1000) break;
                levelMaxXP += (int)Math.Round((i - 1) * 100 * 0.5 + 30);
                this.maxExperienceList.Add(i, levelMaxXP);
            }
        }

        public void resetCharacter()
        {
            // Set all Attributes to zero //
            this.endurance = 0;
            this.force = 0;
            this.agility = 0;
            this.swiftness = 0;
            this.dexterity = 0;
            // Clear the Skills List //
            this.characterAbilities.unlockedAbilitiesList.Clear();
            this.characterAbilities.unlockedAbilitiesList.Add(PantheraConfig.FelineSkills_AbilityID, 0);
            // Save //
            PantheraSaveSystem.Save();
        }

    }
}
