
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
using static RoR2.CharacterSelectSurvivorPreviewDisplayController;
using static RoR2.Navigation.NodeGraph;

namespace Panthera.Base
{

    public class Character
    {

        // (int Level, int MaxExperience) Represent a list of all level max Experience //
        public Dictionary<int, int> maxExperienceList = new Dictionary<int, int>();

        // (string name, GameObject prefab) Represent a list of all boss to defeat to obtain Mastery Points //
        public Dictionary<string, GameObject> bossList = new Dictionary<string, GameObject>();

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
                return lastValue;
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
                return this.characterLevel - 1;
            }
        }
        public int usedAttributePoints
        {
            get
            {
                if (Panthera.ProfileComponent != null)
                    return Panthera.ProfileComponent.getTotalAttributesUsed();
                else
                    return 0;
            }
        }
        public int attributePointsLeft
        {
            get
            {
                return this.totalAttributePoints - this.usedAttributePoints;
            }
        }
        public int totalSkillsPoints
        {
            get
            {
                return this.characterLevel - 1;
            }
        }
        public int skillPointsLeft
        {
            get
            {
                return totalSkillsPoints
                    - this.usedSkillPoints;
            }
        }
        public int usedSkillPoints
        {
            get
            {
                if (Panthera.ProfileComponent != null)
                    return Panthera.ProfileComponent.getTotalSkillPointsUsed();
                else
                    return 0;
            }
        }

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
            for (int i = 1; i <= PantheraConfig.MaxPantheraLevel; i++)
            {
                levelMaxXP += (int)Math.Round((i - 1) * 100 * 0.5 + 30);
                this.maxExperienceList.Add(i, levelMaxXP);
            }
        }

        public void CreateMasteryBossList()
        {
            foreach (GameObject prefab in RoR2.ContentManagement.ContentManager.masterPrefabs)
            {

                // Beetle Queen //
                if (prefab.name == "BeetleQueenMaster")
                    this.bossList.Add(prefab.name, prefab);

                // Clay Dunestrider //
                if (prefab.name == "ClayBossMaster")
                    this.bossList.Add(prefab.name, prefab);

                // Grandparent //
                if (prefab.name == "GrandparentMaster")
                    this.bossList.Add(prefab.name, prefab);

                // Grovetender //
                if (prefab.name == "GravekeeperMaster")
                    this.bossList.Add(prefab.name, prefab);

                // Imp Overlord //
                if (prefab.name == "ImpBossMaster")
                    this.bossList.Add(prefab.name, prefab);

                // Magma Worm //
                if (prefab.name == "MagmaWormMaster")
                    this.bossList.Add(prefab.name, prefab);

                // Overloading Worm //
                if (prefab.name == "ElectricWormMaster")
                    this.bossList.Add(prefab.name, prefab);

                // Scavenger //
                if (prefab.name == "ScavMaster")
                    this.bossList.Add(prefab.name, prefab);

                // Solus Control Unit //
                if (prefab.name == "RoboBallBossMaster")
                    this.bossList.Add(prefab.name, prefab);

                // Stone Titan //
                if (prefab.name == "TitanMaster")
                    this.bossList.Add(prefab.name, prefab);

                // Void Devastator //
                if (prefab.name == "VoidMegaCrabMaster")
                    this.bossList.Add(prefab.name, prefab);

                // Wandering Vagrant //
                if (prefab.name == "VagrantMaster")
                    this.bossList.Add(prefab.name, prefab);

                // Xi Construct //
                if (prefab.name == "MegaConstructMaster")
                    this.bossList.Add(prefab.name, prefab);

                // Alloy Worship Unit //
                if (prefab.name == "SuperRoboBallBossMaster")
                    this.bossList.Add(prefab.name, prefab);

                // Aurelionite //
                if (prefab.name == "TitanGoldMaster")
                    this.bossList.Add(prefab.name, prefab);

                // Guragura the Lucky //
                if (prefab.name == "ScavLunar4Master")
                    this.bossList.Add(prefab.name, prefab);

                // Kipkip the Gentle //
                if (prefab.name == "ScavLunar1Master")
                    this.bossList.Add(prefab.name, prefab);

                // Twiptwip the Devotee //
                if (prefab.name == "ScavLunar3Master")
                    this.bossList.Add(prefab.name, prefab);

                // Wipwip the Wild //
                if (prefab.name == "ScavLunar2Master")
                    this.bossList.Add(prefab.name, prefab);

                // Mithrix //
                if (prefab.name == "BrotherMaster")
                    this.bossList.Add(prefab.name, prefab);

            }
        }

    }
}
