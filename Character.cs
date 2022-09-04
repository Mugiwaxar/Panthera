
using Panthera.Components;
using Panthera.GUI;
using Panthera.Utils;
using R2API;
using RoR2;
using RoR2.Projectile;
using RoR2.Stats;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

namespace Panthera
{
    class Character
    {

        internal static List<SurvivorDef> SurvivorDefinitions = new List<SurvivorDef>();
        public static UnityEngine.GameObject CharacterDisplay;
        public static readonly Color CharacterColor = new Color(1, 1, 1);

        public static int CharacterLevel = 1;
        public static int _CharacterExperience;
        public static int Experience
        {
            get
            {
                return _CharacterExperience;
            }
            set
            {
                _CharacterExperience = value;
                if (_CharacterExperience > MaxExperience)
                {
                    _CharacterExperience = 0;
                    CharacterLevel++;
                }
            }
        }
        public static int MaxExperience
        {
            get
            {
                return (int)Math.Round((CharacterLevel - 1) * 100 * 0.3 + 100);
            }
        }
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

        // Register the Character //
        public static void RegisterCharacter()
        {

            // Create the display prefab //
            GameObject displayPrefab = PrefabAPI.InstantiateClone(Resources.Load<GameObject>("Prefabs/CharacterBodies/CommandoBody"), "PantheraDisplay");
            Panthera.DestroyImmediate(displayPrefab.transform.Find("ModelBase").gameObject);
            Panthera.DestroyImmediate(displayPrefab.transform.Find("CameraPivot").gameObject);
            Panthera.DestroyImmediate(displayPrefab.transform.Find("AimOrigin").gameObject);

            // Get the display model //
            GameObject displayModel = GameObject.Instantiate(Assets.MainAssetBundle.LoadAsset<GameObject>("PantheraIntro"));

            // Set up the display model //
            GameObject modelBase = new GameObject("ModelBase");
            modelBase.transform.parent = displayPrefab.transform;
            modelBase.transform.localPosition = new Vector3(0f, -0.92f, 0f);
            modelBase.transform.localRotation = Quaternion.identity;
            modelBase.transform.localScale = new Vector3(1f, 1f, 1f);

            GameObject cameraPivot = new GameObject("CameraPivot");
            cameraPivot.transform.parent = modelBase.transform;
            cameraPivot.transform.localPosition = new Vector3(0f, 1.6f, 0f);
            cameraPivot.transform.localRotation = Quaternion.identity;
            cameraPivot.transform.localScale = Vector3.one;

            GameObject aimOrigin = new GameObject("AimOrigin");
            aimOrigin.transform.parent = modelBase.transform;
            aimOrigin.transform.localPosition = new Vector3(0f, 2.2f, 0f);
            aimOrigin.transform.localRotation = Quaternion.identity;
            aimOrigin.transform.localScale = Vector3.one;
            displayPrefab.GetComponent<CharacterBody>().aimOriginTransform = aimOrigin.transform;

            displayModel.transform.parent = modelBase.transform;
            displayModel.transform.localPosition = Vector3.zero;
            displayModel.transform.localRotation = Quaternion.identity;

            displayModel.AddComponent<CharacterModel>().baseRendererInfos = displayPrefab.GetComponentInChildren<CharacterModel>().baseRendererInfos;

            CharacterDisplay = displayModel.gameObject;
            CharacterDisplay.AddComponent<DisplayPrefabSound>();

            // Create the survivor def //
            SurvivorDef survivorDef = ScriptableObject.CreateInstance<SurvivorDef>();

            survivorDef.bodyPrefab = Prefab.characterPrefab;
            survivorDef.displayPrefab = CharacterDisplay;
            survivorDef.primaryColor = CharacterColor;
            survivorDef.displayNameToken = "PantheraName";
            survivorDef.descriptionToken = "PantheraDescription";
            survivorDef.desiredSortPosition = 999;
            //survivorDef.unlockableDef = unlockableDef;

            // Register the survivor //
            SurvivorDefinitions.Add(survivorDef);

        }

        public static void GameEndRepport(On.RoR2.UI.GameEndReportPanelController.orig_SetPlayerInfo orig, RoR2.UI.GameEndReportPanelController self, RunReport.PlayerInfo playerInfo)
        {
            orig(self, playerInfo);

            // Check the Character //
            if (playerInfo.bodyName != "PantheraBody")
            {
                return;
            }

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

            // Add the point win to Experience //
            int experience = (int) (num / (ulong) PantheraConfig.ExperienceDivider);
            Debug.LogWarning("Added " + experience.ToString() + " points");
            Character.Experience += experience;

            // Save //
            PantheraSaveSystem.SetValue("CharacterLevel", CharacterLevel.ToString());
            PantheraSaveSystem.SetValue("CharacterExperience", Experience.ToString());
            PantheraSaveSystem.Save();

            // Remove the Hook //
            On.RoR2.UI.GameEndReportPanelController.SetPlayerInfo -= Character.GameEndRepport;

        }


    }
}
