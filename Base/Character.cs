
using MonoMod.RuntimeDetour.HookGen;
using Panthera;
using Panthera.Base;
using Panthera.Components;
using Panthera.GUI;
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
    class Character
    {

        public static List<SurvivorDef> SurvivorDefinitions = new List<SurvivorDef>();
        public static readonly Color CharacterColor = new Color(1, 1, 1);

        public static DamageAPI.ModdedDamageType BarrierDamageType;

        // (int Level, int MaxExperience) Represent a list of all level max Experience //
        public static Dictionary<int, int> MaxExperienceList = new Dictionary<int, int>();

        public static bool AllowXP = false;
        public static int CharacterLevel
        {
            get
            {
                int lastValue = 1;
                foreach (KeyValuePair<int, int> entry in MaxExperienceList)
                {
                    if (TotalExperience >= entry.Value)
                        lastValue = entry.Key + 1;
                    else
                        break;
                }
                return Math.Min(lastValue, MaxLevel);
            }
        }
        public static int MaxLevel
        {
            get
            {
                int i = 0;
                foreach (PantheraAbility ability in PantheraAbility.AbilitytiesDefsList.Values)
                {
                    i += ability.maxLevel;
                }
                return i;
            }
        }
        public static int LevelExperience
        {
            get
            {
                if (MaxExperienceList.ContainsKey(CharacterLevel - 1) == false) return TotalExperience;
                return TotalExperience - MaxExperienceList[CharacterLevel - 1];
            }
        }
        public static int LevelMaxExperience
        {
            get
            {
                if (MaxExperienceList.ContainsKey(CharacterLevel) == false) return 0;
                if (MaxExperienceList.ContainsKey(CharacterLevel-1) == false) return MaxExperienceList[CharacterLevel];
                return MaxExperienceList[CharacterLevel] - MaxExperienceList[CharacterLevel-1];
            }
        }
        public static int TotalExperience;
        public static int AvailablePoint
        {
            get
            {
                return CharacterLevel - SpentPoint;
            }
        }
        public static int SpentPoint
        {
            get
            {
                int i = 0;
                foreach (int level in Preset.SelectedPreset.unlockedAbilitiesList.Values)
                {
                    i += level;
                }
                return i;
            }
        }
        

        public static void RegisterCharacter()
        {

            // Create the Prefabs //
            Prefab.CharacterPrefab = Prefab.CreateCharacterPrefab(Assets.MainPrefab, PantheraConfig.Model_PrefabName);
            Prefab.CharacterDisplayPrefab = Prefab.CreateDisplayPrefab(Assets.DisplayPrefab, PantheraConfig.Model_PrefabName, Prefab.CharacterPrefab);
            Prefab.RegisterSkills(Prefab.CharacterPrefab);

            // Create the survivor def //
            SurvivorDef survivorDef = ScriptableObject.CreateInstance<SurvivorDef>();

            survivorDef.bodyPrefab = Prefab.CharacterPrefab;
            survivorDef.displayPrefab = Prefab.CharacterDisplayPrefab;
            survivorDef.primaryColor = CharacterColor;
            survivorDef.displayNameToken = PantheraTokens.Get("PANTHERA_NAME");
            survivorDef.descriptionToken = PantheraTokens.Get("PANTHERA_DESC");
            survivorDef.desiredSortPosition = 999;
            //survivorDef.unlockableDef = unlockableDef;

            // Register the survivor //
            SurvivorDefinitions.Add(survivorDef);

            // Register Damage Type //
            BarrierDamageType = DamageAPI.ReserveDamageType();

        }
        
        public static void CalculMaxExperiencePerLevel()
        {
            int levelMaxXP = 0;
            for (int i = 1; i <= MaxLevel; i++)
            {
                if (i > 1000) break;
                levelMaxXP += (int)Math.Round((i - 1) * 100 * 0.3 + 100);
                MaxExperienceList.Add(i, levelMaxXP);
            }
        }

        public static void GameEndRepportHook(Action<RoR2.UI.GameEndReportPanelController, RunReport.PlayerInfo> orig, RoR2.UI.GameEndReportPanelController self, RunReport.PlayerInfo playerInfo)
        {
            orig(self, playerInfo);

            // Check the Character //
            if (playerInfo.bodyName != "PantheraBody" || AllowXP == false)
            {
                return;
            }

            // Don't allow more XP //
            AllowXP = false;

            // Calcule the total Points win //
            ulong num = 0UL;
            StatSheet statSheet = playerInfo.statSheet;
            self.AllocateStatStrips(self.statsToDisplay.Length);
            for (int i = 0; i < self.statsToDisplay.Length; i++)
            {
                string text = self.statsToDisplay[i];
                StatDef statDef = StatDef.Find(text);
                if (statDef == null)
                {
                    Debug.LogWarningFormat("GameEndReportPanelController.SetStatSheet: Could not find stat def \"{0}\".", new object[]
                    {
                            text
                    });
                }
                else
                {
                    self.AssignStatToStrip(statSheet, statDef, self.statStrips[i]);
                    num += statSheet.GetStatPointValue(statDef);
                }
            }

            // Add the win points to Experience //
            int experience = (int)(num / (ulong)PantheraConfig.ExperienceDivider);
            TotalExperience += experience;

            // Save //
            PantheraSaveSystem.SetValue("CharacterExperience", TotalExperience.ToString());
            PantheraSaveSystem.Save();

        }


    }
}
