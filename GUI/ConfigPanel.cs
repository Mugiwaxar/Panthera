using Panthera.Components;
using Panthera.Utils;
using Rewired;
using RoR2;
using RoR2.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Panthera.GUI
{
    internal class ConfigPanel : MonoBehaviour
    {

        public static ConfigPanel instance;

        public static GameObject ConfigButtonPrefab = Assets.MainAssetBundle.LoadAsset<GameObject>("ConfigButton");
        public static GameObject ConfigPanelPrefab = Assets.MainAssetBundle.LoadAsset<GameObject>("PantheraPanel");
        public static GameObject ActiveSkillPrefab = Assets.MainAssetBundle.LoadAsset<GameObject>("SkillIconActive");
        public static GameObject PassiveSkillPrefab = Assets.MainAssetBundle.LoadAsset<GameObject>("SkillIconPassive");
        public static GameObject KeyBindWindowPrefab = Assets.MainAssetBundle.LoadAsset<GameObject>("KeyBindWindow");
        public static GameObject ActivePresetWindowPrefab = Assets.MainAssetBundle.LoadAsset<GameObject>("ActivatePresetWindow");
        public static GameObject ResetPresetWindowPrefab = Assets.MainAssetBundle.LoadAsset<GameObject>("ResetPresetWindow");

        public CharacterSelectController origGUI;
        public Player REPlayer;

        public GameObject canva;
        public GameObject loadoutButton;

        public GameObject configButton;
        public GameObject levelUpIcon;
        public GameObject configPanelGUI;
        public Transform dragArea;

        // (Preset ID 1-10, Preset Object) Represent all Preset Buttons on the Left of the GUI //
        public Dictionary<int, GameObject> presetButtonsList = new Dictionary<int, GameObject>();

        // (AbilityID 1-??, Ability Button) Represent all Ability Buttons inside the Skills Tree Tab //
        public Dictionary<int, GameObject> abilityButtonsList = new Dictionary<int, GameObject>();

        // (Slot ID 1-20, Slot Object) Represent the Action Bars Slots Objects inside the Skills Tab //
        public Dictionary<int, GameObject> skillsSlotList = new Dictionary<int, GameObject>();

        // (Action ID, KeyBind Object) Represent every line of Keys Bind (Keyboard + Mouse + Gamepad) inside the Keys Bind Tab //
        public Dictionary<int, KeyBind> keysBindList = new Dictionary<int, KeyBind>();

        public GameObject overviewTab;
        public GameObject skillTreesTab;
        public GameObject skillsTab;
        public GameObject keysBindTab;

        public GameObject skillTree1;
        public GameObject skillTree2;
        public GameObject skillTree3;
        public GameObject skillTree4;
        public TextMeshProUGUI skillsAvailableAmount;
        public TextMeshProUGUI skillsSpentAmount;
        public GameObject abilitiesTooltip;

        public GameObject skillsZone;
        public GameObject skillTreeTooltip;
        public GameObject skillsTooltip;

        public GameObject keyBindWindow;
        public TextMeshProUGUI keyBindWindowText;
        public GameObject activatePresetWindow;
        public GameObject resetPresetWindow;

        public int lunarCoin
        {
            get
            {
                return (int)this.origGUI.localUser.userProfile.coins;
            }
            set
            {
                this.origGUI.localUser.userProfile.coins = (uint)value;
            }
        }

        public bool firstStart = false;

        public static void Init()
        {
            // Create the hook //
            On.RoR2.UI.CharacterSelectController.Awake += AddConfigPanel;
        }

        public static void AddConfigPanel(On.RoR2.UI.CharacterSelectController.orig_Awake orig, RoR2.UI.CharacterSelectController self)
        {

            // Use the original function //
            orig(self);

            if (NetworkClient.active == false)
            {
                return;
            }

            // Get the Character Selection Canva //
            GameObject canva = self.transform.Find("SafeArea").gameObject;

            // Add the Config Panel to the Canva //
            instance = canva.AddComponent<ConfigPanel>();
            instance.origGUI = self;
            instance.canva = canva;

            // Get the Character Panel //
            Transform characterPanel = instance.canva.transform.Find("LeftHandPanel (Layer: Main)");
            Transform characterInfoPanel = characterPanel.GetChild(3);
            Transform buttonList = characterInfoPanel.GetChild(1);
            instance.loadoutButton = buttonList.GetChild(3).gameObject;

        }

        public void Start()
        {

            // Get the File name of the User Profil //
            PantheraSaveSystem.saveFileName = this.origGUI.localUser.userProfile.fileName;

            // Set KeysBinder REPlayer and REMaps //
            KeysBinder.InitPlayer(this.origGUI.localUser.inputPlayer);

            // Load the Save //
            PantheraSaveSystem.Load();

            // Check if this is the first time the mod is loaded //
            this.firstStart = PantheraSaveSystem.ReadValue("FirstStartDone") == null ? true : false;
            PantheraSaveSystem.SetValue("FirstStartDone", "Ok");

            // Get the last used Preset //
            int lastUsedPreset = 0;
            string lastUsedPresetString = PantheraSaveSystem.ReadValue("LastPresetUsed");
            if (lastUsedPresetString != null)
                lastUsedPreset = Int32.Parse(lastUsedPresetString);
            else
                lastUsedPreset = 1;
            PantheraSaveSystem.SetValue("LastPresetUsed", lastUsedPreset.ToString());

            // Read the Level  //
            string levelString = PantheraSaveSystem.ReadValue("CharacterLevel");
            if (levelString == null)
            {
                levelString = "1";
                PantheraSaveSystem.SetValue("CharacterLevel", levelString);
            }
            Character.CharacterLevel = Int32.Parse(levelString);

            // Read the Experience //
            string experienceString = PantheraSaveSystem.ReadValue("CharacterExperience");
            if (experienceString == null)
            {
                experienceString = "0";
                PantheraSaveSystem.SetValue("CharacterExperience", experienceString);
            }
            Character.Experience = Int32.Parse(experienceString);

            // Read the Last Viewed Level //
            string lastViewedLevelString = PantheraSaveSystem.ReadValue("LastViewedLevel");
            if (lastViewedLevelString == null)
            {
                lastViewedLevelString = "0";
                PantheraSaveSystem.SetValue("LastViewedLevel", lastViewedLevelString);
            }
            int lastViewedLevel = Int32.Parse(lastViewedLevelString);

            // Save //
            PantheraSaveSystem.Save();

            // Load the Preset //
            Dictionary<string, string> dataList = PantheraSaveSystem.LoadPreset(lastUsedPreset);

            // Create the Preset //
            Preset.SelectedPreset = new Preset(this, lastUsedPreset, dataList);
            Preset.ActivePreset = Preset.SelectedPreset;

            // Create the Config Button //
            this.configButton = UnityEngine.Object.Instantiate<GameObject>(ConfigButtonPrefab, canva.transform, false);
            this.configButton.GetComponent<Button>().onClick.AddListener(this.configButtonClicked);
            this.levelUpIcon = configButton.transform.Find("LevelUpIcon").gameObject;

            // Set the Level Up Icon Visibility //
            if (lastViewedLevel != Character.CharacterLevel)
            {
                this.levelUpIcon.SetActive(true);
            }
            else
            {
                this.levelUpIcon.SetActive(false);
            }

            // Disable the Button //
            this.configButton.SetActive(false);

            // Down the button in Multiplayer //
            if (RoR2Application.isInMultiPlayer)
            {
                this.configButton.transform.localPosition = new Vector3(this.configButton.transform.localPosition.x, this.configButton.transform.localPosition.y - 10, this.configButton.transform.localPosition.z);    
            }

                // Add the Button Watcher Component //
                ButtonWatcher comp = this.configButton.AddComponent<ButtonWatcher>();
            comp.configPanel = this;

            // Create the Config Panel //
            this.configPanelGUI = UnityEngine.Object.Instantiate<GameObject>(ConfigPanelPrefab, canva.transform, false);
            this.dragArea = this.configPanelGUI.transform.Find("DragArea");

            // Disable the Config Panel //
            this.configPanelGUI.SetActive(false);

            // Init Presets Buttons from the Main GUI //
            this.initPresets();

            // Register Tabs //
            this.registerTabs();

            // Active/Unactive Tabs //
            this.overviewTab.active = true;
            this.skillTreesTab.active = false;
            this.skillsTab.active = false;
            this.keysBindTab.active = false;

            // Register the Skills Zone //
            this.skillsZone = this.skillsTab.transform.Find("SkillsArea")?.Find("Content").gameObject;

            // Register all Buttons events //
            this.registerAllButtons();

            // Register all Skill Trees //
            this.registerAllSkillTrees();

            // Register Action Bars Skills Slots from the Skills Tab //
            this.registerAllSkillsSlots();

            // Register all Keys Bind from the Keys Bind Tab //
            this.registerAllKeysBind();

            // Create the Key Bind Window //
            this.keyBindWindow = UnityEngine.Object.Instantiate<GameObject>(KeyBindWindowPrefab, canva.transform);
            this.keyBindWindow.SetActive(false);
            this.keyBindWindowText = this.keyBindWindow.transform.Find("Content").Find("KeysBind").Find("Text").GetComponent<TextMeshProUGUI>();
            ButtonWatcher buttonWatcher1 = this.keyBindWindow.transform.Find("Content").Find("RemoveButton").gameObject.AddComponent<ButtonWatcher>();
            ButtonWatcher buttonWatcher2 = this.keyBindWindow.transform.Find("Content").Find("CancelButton").gameObject.AddComponent<ButtonWatcher>();
            buttonWatcher1.configPanel = this;
            buttonWatcher2.configPanel = this;

            // Create the Active Preset Window //
            this.activatePresetWindow = UnityEngine.Object.Instantiate<GameObject>(ActivePresetWindowPrefab, canva.transform);
            this.activatePresetWindow.SetActive(false);
            ButtonWatcher buttonWatcher3 = this.activatePresetWindow.transform.Find("Content").Find("ActivatePrtButton").gameObject.AddComponent<ButtonWatcher>();
            ButtonWatcher buttonWatcher4 = this.activatePresetWindow.transform.Find("Content").Find("CancelActivatePrtButton").gameObject.AddComponent<ButtonWatcher>();
            buttonWatcher3.configPanel = this;
            buttonWatcher4.configPanel = this;

            // Create the Reset Preset Window //
            this.resetPresetWindow = UnityEngine.Object.Instantiate<GameObject>(ResetPresetWindowPrefab, canva.transform);
            this.resetPresetWindow.SetActive(false);
            ButtonWatcher buttonWatcher5 = this.resetPresetWindow.transform.Find("Content").Find("ResetPrtButton").gameObject.AddComponent<ButtonWatcher>();
            ButtonWatcher buttonWatcher6 = this.resetPresetWindow.transform.Find("Content").Find("CancelResetPrtButton").gameObject.AddComponent<ButtonWatcher>();
            buttonWatcher5.configPanel = this;
            buttonWatcher6.configPanel = this;

            // Build the Skills List //
            Preset.SelectedPreset.buildSkillList();

            // --------------------------------------------------------------------------------- //
            // --------------------------------------------------------------------------------- //
            // --------------------------------------------------------------------------------- //
            // --------------------------------------------------------------------------------- //
            // --------------------------------------------------------------------------------- //
            // --------------------------------------------------------------------------------- //
            //                                  TO REMOVE                                        //
            this.lunarCoin = 52;
            // --------------------------------------------------------------------------------- //
            // --------------------------------------------------------------------------------- //
            // --------------------------------------------------------------------------------- //
            // --------------------------------------------------------------------------------- //
            // --------------------------------------------------------------------------------- //

            // Update all Values //
            this.updateAllValues();

            // Set the end of the first start //
            this.firstStart = false;

        }

        public void Update()
        {
            // Get the Selected survivor //
            var charName = origGUI.survivorName.text;

            // Check if this is P4N7H3R4 //
            if (charName == "P4N7H3R4")
            {
                // Activate the Config Button //
                this.configButton.SetActive(true);
                // Deactivate the Loadout Button //
                this.loadoutButton.SetActive(false);
            }
            else
            {
                // Deactivate the Config Button //
                this.configButton.SetActive(false);
                // Activate the Loadout Button //
                this.loadoutButton.SetActive(true);
            }

        }

        public void updateAllValues()
        {
            // Update the color of all Preset Buttons //
            this.updatePresetColor();
            // Reload all Maps //
            KeysBinder.InitPlayer(this.origGUI.localUser.inputPlayer);
            // Reload Skills List //
            Preset.SelectedPreset.buildSkillList();
            // Set all Stats Text //
            this.updateOverviewTab();
            // Update the Available/Spent Skills Points //
            this.updateSkillsPoint();
            // Build the Skills List //
            this.updateSkillsIconsList();
            // Update the Skill Bars //
            this.updateSkillBars();
            // Update all Keys Bind //
            this.updateAllKeyBindTexts();
            // Save //
            PantheraSaveSystem.SetValue("LastPresetUsed", Preset.ActivePreset.presetID.ToString());
            PantheraSaveSystem.Save();
            // Save the Preset //
            PantheraSaveSystem.SavePreset(Preset.SelectedPreset.presetID, Preset.SelectedPreset.saveData());
        }

        private void configButtonClicked()
        {
            // Change the color of the Config Button //
            this.configButton.GetComponent<Image>().color = PantheraConfig.ConfigButtonColor;
            // Hide the Last Level Icon //
            this.levelUpIcon.SetActive(false);
            PantheraSaveSystem.SetValue("LastViewedLevel", Character.CharacterLevel.ToString());
            PantheraSaveSystem.Save();
            // Activate the Config Panel //
            this.configPanelGUI.SetActive(true);
            // Disable the Gamepad //
            KeysBinder.GamepadSetEnable(false);
            // Play the Sound //
            Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject);
        }

        private void initPresets()
        {
            // Find all Preset Buttons //
            Transform presets = this.configPanelGUI.transform.Find("MainPanel").Find("Presets");
            this.presetButtonsList.Add(1, presets.Find("Preset1").gameObject);
            this.presetButtonsList.Add(2, presets.Find("Preset2").gameObject);
            this.presetButtonsList.Add(3, presets.Find("Preset3").gameObject);
            this.presetButtonsList.Add(4, presets.Find("Preset4").gameObject);
            this.presetButtonsList.Add(5, presets.Find("Preset5").gameObject);
            this.presetButtonsList.Add(6, presets.Find("Preset6").gameObject);
            this.presetButtonsList.Add(7, presets.Find("Preset7").gameObject);
            this.presetButtonsList.Add(8, presets.Find("Preset8").gameObject);
            this.presetButtonsList.Add(9, presets.Find("Preset9").gameObject);
            this.presetButtonsList.Add(10, presets.Find("Preset10").gameObject);
        }

        private void registerTabs()
        {
            // Find the Overview Tab //
            this.overviewTab = this.configPanelGUI.transform.Find("MainPanel/TabContents/TabContentOverview").gameObject;
            // Find the Skill Trees Tab //
            this.skillTreesTab = this.configPanelGUI.transform.Find("MainPanel/TabContents/TabContentSkillTree").gameObject;
            // Find the Skill Trees Tooltip //
            this.skillTreeTooltip = this.skillTreesTab.transform.Find("Tooltip")?.gameObject;
            this.skillTreeTooltip.SetActive(false);
            // Find the Skills Tab //
            this.skillsTab = this.configPanelGUI.transform.Find("MainPanel/TabContents/TabContentSkills").gameObject;
            // Find the Skills Tooltip //
            this.skillsTooltip = this.skillsTab.transform.Find("Tooltip")?.gameObject;
            this.skillsTooltip.SetActive(false);
            // Find the Keys Bind Tab //
            this.keysBindTab = this.configPanelGUI.transform.Find("MainPanel/TabContents/TabContentKeysBind").gameObject;
            // Find the Keys Bind Pages //

        }

        private void registerAllButtons()
        {
            // Find all Panthera Components //
            Component[] components = this.configPanelGUI.GetComponentsInChildren<Component>(true);
            // Register all Buttons //
            foreach (Component component in components)
            {
                // Check if this is a Button //
                if (component is Button button)
                {
                    // Add the Button Watcher Component //
                    ButtonWatcher comp = button.gameObject.AddComponent<ButtonWatcher>();
                    comp.configPanel = instance;

                    // Check if this is an Ability //
                    if (component.gameObject.name.Contains("Ability"))
                    {
                        string abilitIDString = Regex.Replace(component.gameObject.name, @"[^\d]", "");
                        int abilityID = Int32.Parse(abilitIDString);
                        this.abilityButtonsList.Add(abilityID, component.gameObject);
                    }

                }
            }
        }

        private void registerAllSkillTrees()
        {
            this.skillTree1 = this.skillTreesTab.transform.Find("TabsContent").Find("SkillTree1").gameObject;
            this.skillTree2 = this.skillTreesTab.transform.Find("TabsContent").Find("SkillTree2").gameObject;
            this.skillTree3 = this.skillTreesTab.transform.Find("TabsContent").Find("SkillTree3").gameObject;
            this.skillTree4 = this.skillTreesTab.transform.Find("TabsContent").Find("SkillTree4").gameObject;
            this.skillsAvailableAmount = this.skillTreesTab.transform.Find("SkillTreeAmountLimit").Find("AvailableBackground").Find("AvailableAmount").GetComponent<TextMeshProUGUI>();
            this.skillsSpentAmount = this.skillTreesTab.transform.Find("SkillTreeAmountLimit").Find("SpentBackground").Find("SpentAmount").GetComponent<TextMeshProUGUI>();
            this.abilitiesTooltip = this.skillTreesTab.transform.Find("Tooltip").gameObject;
            this.skillTree1.SetActive(true);
            this.skillTree2.SetActive(false);
            this.skillTree3.SetActive(false);
            this.skillTree4.SetActive(false);
            this.abilitiesTooltip.SetActive(false);
        }

        private void registerAllSkillsSlots()
        {

            // Find the Action Bars //
            GameObject actionBar1 = this.skillsTab.transform.Find("ActionBar1")?.gameObject;
            GameObject actionBar2 = this.skillsTab.transform.Find("ActionBar2")?.gameObject;

            // Instantiate the SkillsSlot list //
            for (int i = 1; i <= 20; i++)
            {
                if (i <= 10)
                    this.skillsSlotList[i] = actionBar1?.transform.Find("BarSkill" + (i).ToString())?.gameObject;
                else
                    this.skillsSlotList[i] = actionBar2?.transform.Find("BarSkill" + (i).ToString())?.gameObject;
            }

            // Add the DragNDrop Component //
            foreach (KeyValuePair<int, GameObject> entry in this.skillsSlotList)
            {
                GameObject icon = entry.Value.transform.Find("AbilityContainer").Find("AbilityIcon").gameObject;
                // Add the DragNDrop Component //
                DragNDrop dragComp = icon.AddComponent<DragNDrop>();
                dragComp.dragArea = this.dragArea;
                dragComp.configPanel = this;
                dragComp.startingDragSlotID = entry.Key;
            }

        }

        private void registerAllKeysBind()
        {
            // Look for all Keys Bind //
            foreach (Transform transform in ConfigPanel.instance.keysBindTab.transform.FindChild("Content"))
            {
                // Exclude the Title Layout //
                if (transform.name == "TitleLayout") continue;

                // Create the Key Bind Object //
                KeyBind keyBind = new KeyBind();

                // Get the Name //
                keyBind.name = transform.GetChild(0).name;

                // Get the Action ID //
                keyBind.actionID = KeysBinder.ActionList[keyBind.name];

                // Gets Buttons //
                keyBind.UIKeyboardButton = transform.GetChild(1).gameObject;
                keyBind.UIMouseButton = transform.GetChild(2).gameObject;
                keyBind.UIGamepadButton = transform.GetChild(3).gameObject;

                // Add to the List //
                this.keysBindList.Add(keyBind.actionID, keyBind);

            }
        }

        public void changePreset(int presetID)
        {

            // Save the current Preset //
            PantheraSaveSystem.SavePreset(Preset.SelectedPreset.presetID, Preset.SelectedPreset.saveData());

            // Load the Preset //
            Dictionary<string, string> dataList = PantheraSaveSystem.LoadPreset(presetID);

            // Create the Preset //
            Preset.SelectedPreset = new Preset(this, presetID, dataList);

            // Update all //
            this.updateAllValues();

        }

        public void updatePresetColor()
        {
            // Itinerate all Preset Buttons //
            foreach (KeyValuePair<int, GameObject> entry in this.presetButtonsList)
            {
                // Get the two Images //
                GameObject presetObj = entry.Value;
                Image image1 = presetObj.GetComponent<Image>();
                Image image2 = presetObj.transform.Find("Image").GetComponent<Image>();
                if (entry.Key == Preset.ActivePreset.presetID)
                {
                    image1.color = PantheraConfig.PresetButtonSelectedColor;
                    image2.color = PantheraConfig.PresetButtonSelectedColor;
                }
                else if (entry.Key == Preset.SelectedPreset.presetID)
                {
                    image1.color = PantheraConfig.PresetImageNormalColor;
                    image2.color = PantheraConfig.PresetButtonSelectedColor;
                }
                else
                {
                    image1.color = PantheraConfig.PresetImageNormalColor;
                    image2.color = PantheraConfig.PresetFrameNormalColor;
                }
            }
        }

        public void updateOverviewTab()
        {
            // Get the Stats Object //
            Preset stats = Preset.SelectedPreset;
            // Set the Level //
            this.overviewTab.transform.Find("LevelText").GetComponent<TextMeshProUGUI>().SetText(String.Format("Level: {0}", Character.CharacterLevel));
            // Set the Experience //
            this.overviewTab.transform.Find("XPBar").Find("Text").GetComponent<TextMeshProUGUI>().SetText("{0}/{1}", Character.Experience, Character.MaxExperience);
            this.overviewTab.transform.Find("XPBar").Find("Bar").GetComponent<Image>().fillAmount =  (float)Character.Experience / (float)Character.MaxExperience;
            // Set the Lunar Coin Text //
            this.overviewTab.transform.Find("LunarCoinImage").Find("Text").GetComponent<TextMeshProUGUI>().SetText(this.lunarCoin.ToString());
            // Get the Stats Panel //
            Transform StatsPanel = this.overviewTab.transform.Find("Stats");
            // Set all values //
            StatsPanel.Find("HealthLayout").Find("HealthValue").GetComponent<TextMeshProUGUI>().SetText(String.Format("{0} (+{1})", stats.maxHealth, stats.maxHealthLevel));
            StatsPanel.Find("HealthRegenLayout").Find("HealthRegenValue").GetComponent<TextMeshProUGUI>().SetText(String.Format("{0} (+{1})", stats.healthRegen, stats.healthRegenLevel));
            StatsPanel.Find("EnergyLayout").Find("EnergyValue").GetComponent<TextMeshProUGUI>().SetText(String.Format("{0}", stats.maxEnergy));
            StatsPanel.Find("EnergyRegenLayout").Find("EnergyRegenValue").GetComponent<TextMeshProUGUI>().SetText(String.Format("{0}", stats.energyRegen));
            StatsPanel.Find("FuryLayout").Find("FuryValue").GetComponent<TextMeshProUGUI>().SetText(String.Format("{0}", stats.maxFury));
            StatsPanel.Find("PowerLayout").Find("PowerValue").GetComponent<TextMeshProUGUI>().SetText(String.Format("{0}", stats.maxPower));
            StatsPanel.Find("MoveSpeedLayout").Find("MoveSpeedValue").GetComponent<TextMeshProUGUI>().SetText(String.Format("{0} (+{1})", stats.moveSpeed, stats.moveSpeed));
            StatsPanel.Find("DamageLayout").Find("DamageValue").GetComponent<TextMeshProUGUI>().SetText(String.Format("{0} (+{1})", stats.damage, stats.damageLevel));
            StatsPanel.Find("AttackSpeedLayout").Find("AttackSpeedValue").GetComponent<TextMeshProUGUI>().SetText(String.Format("{0} (+{1})", stats.attackSpeed, stats.attackSpeedLevel));
            StatsPanel.Find("CriticLayout").Find("CriticValue").GetComponent<TextMeshProUGUI>().SetText(String.Format("{0} (+{1})", stats.critic, stats.criticLevel));
            StatsPanel.Find("DefenseLayout").Find("DefenseValue").GetComponent<TextMeshProUGUI>().SetText(String.Format("{0} (+{1})", stats.defense, stats.defenseLevel));
            // Set the Activate Button Color //
            Image activatePresetButtonImage = this.overviewTab.transform.Find("ActivateButton").GetComponent<Image>();
            if (Preset.SelectedPreset.presetID == Preset.ActivePreset.presetID)
                activatePresetButtonImage.color = PantheraConfig.PresetActivateButtonDisabledColor;
            else
                activatePresetButtonImage.color = PantheraConfig.PresetActivateButtonNormalColor;

        }

        public void updateSkillsPoint()
        {
            // Update the Available Points Text //
            this.skillsAvailableAmount.text = Character.AvailablePoint.ToString();
            // Update the Spent Points Text //
            this.skillsSpentAmount.text = Character.SpentPoint.ToString();
            // Update all Ability Icons //
            foreach(KeyValuePair<int, GameObject> entry in this.abilityButtonsList)
            {
                PantheraAbility ability = PantheraAbility.AbilitytiesDefsList[entry.Key];
                int level = Preset.SelectedPreset.getAbilityLevel(ability.abilityID);
                TextMeshProUGUI text = entry.Value.transform.Find("Amount").GetComponent<TextMeshProUGUI>();
                text.text = String.Format("{0}/{1}", level, ability.maxLevel);
            }
        }

        public void updateSkillsIconsList()
        {
            // Destroy the old List //
            foreach (Transform child in this.skillsZone.transform)
            {
                GameObject.Destroy(child.gameObject);
            }


            // Create the new List //
            foreach (PantheraSkill skill in Preset.SelectedPreset.unlockedSkillsList.Values)
            {

                // Instansiate the Skill Icon //
                GameObject skillIcon = GameObject.Instantiate<GameObject>(skill.iconPrefab, this.skillsZone.transform);
                skillIcon.SetActive(true);

                // Add the Button Watcher //
                ButtonWatcher comp = skillIcon.GetComponent<Button>().gameObject.AddComponent<ButtonWatcher>();
                comp.configPanel = instance;
                comp.attachedSkill = skill;

                // Set the Ability image //
                Image image = skillIcon.transform.Find("Icon").GetComponent<Image>();
                image.sprite = skill.icon;

                // Add the DragNDrop Component //
                DragNDrop dragComp = skillIcon.gameObject.AddComponent<DragNDrop>();
                dragComp.attachedSkill = skill;
                dragComp.dragArea = this.dragArea;
                dragComp.configPanel = this;

            }
        }

        public void updateSkillBars()
        {
            // Itinerate all Slots //
            foreach (GameObject slot in this.skillsSlotList.Values)
            {
                // Get the associated Skill //
                PantheraSkill skill = null;
                int slotID = this.getSlotIdFromObject(slot);
                if (slotID != 0 && Preset.SelectedPreset.slotsSkillsLinkList.ContainsKey(slotID))
                    skill = Preset.SelectedPreset.slotsSkillsLinkList[slotID];
                Image image = slot.transform.Find("AbilityContainer").Find("AbilityIcon").GetComponent<Image>();
                TextMeshProUGUI CDText = slot.transform.Find("CooldownText").GetComponent<TextMeshProUGUI>();

                // Check the Skill //
                if (skill == null)
                {
                    image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
                    CDText.gameObject.SetActive(false);
                }
                else
                {
                    image.color = new Color(image.color.r, image.color.g, image.color.b, 255);
                    image.sprite = skill.icon;
                    if (skill.cooldown >= 1)
                    {
                        CDText.gameObject.SetActive(true);
                        CDText.text = skill.cooldown.ToString();
                    }
                }

            }
        }

        public void createAbilityTooltip(PantheraAbility ability)
        {
            // Enable the Tooltip //
            this.abilitiesTooltip.SetActive(true);

            // Get the Content //
            Transform content = this.abilitiesTooltip.transform.Find("Content");

            // Set the Name //
            this.abilitiesTooltip.transform.Find("Content")?.Find("Header")?.Find("AbilityName")?.GetComponent<TextMeshProUGUI>()?.SetText(ability.name);

            // Set the type //
            if (ability.type == PantheraAbility.AbilityType.primary)
                content.Find("Header")?.Find("SkillType")?.GetComponent<TextMeshProUGUI>()?.SetText("Primary");
            else if (ability.type == PantheraAbility.AbilityType.active)
                content.Find("Header")?.Find("SkillType")?.GetComponent<TextMeshProUGUI>()?.SetText("Active");
            else if (ability.type == PantheraAbility.AbilityType.passive)
                content.Find("Header")?.Find("SkillType")?.GetComponent<TextMeshProUGUI>()?.SetText("Passive");

            // Set the Icon //
            Image image = this.abilitiesTooltip.transform.Find("Content")?.Find("Header")?.Find("AbilityIcon")?.Find("Icon")?.GetComponent<Image>();
            image.sprite = ability.icon;

            // Set the Required Points //
            if (ability.requiredPoint > 0)
            {
                if (Character.SpentPoint < ability.requiredPoint)
                    content.Find("RequiredSkillPoint")?.Find("Amount")?.GetComponent<TextMeshProUGUI>()?.SetText(Utils.ColorHelper.SetRed(ability.requiredPoint.ToString()));
                else
                    content.Find("RequiredSkillPoint")?.Find("Amount")?.GetComponent<TextMeshProUGUI>()?.SetText(Utils.ColorHelper.SetGreen(ability.requiredPoint.ToString()));
                content.Find("RequiredSkillPoint")?.gameObject.SetActive(true);
            }
            else
            {
                content.Find("RequiredSkillPoint")?.gameObject.SetActive(false);
            }

            // Set the Unlock Level //
            if (ability.unlockLevel > 0)
            {
                content.Find("UnlockLevel")?.Find("Amount")?.GetComponent<TextMeshProUGUI>()?.SetText(ability.unlockLevel.ToString());
                content.Find("UnlockLevel")?.gameObject.SetActive(true);
            }
            else
            {
                content.Find("UnlockLevel")?.gameObject.SetActive(false);
            }

            // Set the Cooldown //
            if (ability.cooldown > 0)
            {
                content.Find("Cooldown")?.Find("Amount")?.GetComponent<TextMeshProUGUI>()?.SetText(ability.cooldown.ToString());
                content.Find("Cooldown")?.gameObject.SetActive(true);
            }
            else
            {
                content.Find("Cooldown")?.gameObject.SetActive(false);
            }

            // Set the Energy cost //
            if (ability.requiredEnergy > 0)
            {
                content.Find("EnergyCost")?.Find("Amount")?.GetComponent<TextMeshProUGUI>()?.SetText(ability.requiredEnergy.ToString());
                content.Find("EnergyCost")?.gameObject.SetActive(true);
            }
            else
            {
                content.Find("EnergyCost")?.gameObject.SetActive(false);
            }

            // Set the Power cost //
            if (ability.requiredPower > 0)
            {
                content.Find("GodPowerCost")?.Find("Amount")?.GetComponent<TextMeshProUGUI>()?.SetText(ability.requiredPower.ToString());
                content.Find("GodPowerCost")?.gameObject.SetActive(true);
            }
            else
            {
                content.Find("GodPowerCost")?.gameObject.SetActive(false);
            }

            // Set the Fury cost //
            if (ability.requiredFury > 0)
            {
                content.Find("FuryCost")?.Find("Amount")?.GetComponent<TextMeshProUGUI>()?.SetText(ability.requiredFury.ToString());
                content.Find("FuryCost")?.gameObject.SetActive(true);
            }
            else
            {
                content.Find("FuryCost")?.gameObject.SetActive(false);
            }

            // Set the Required Skills //
            if (ability.requiredSkills.Count > 0)
            {
                string skillsList = "";
                foreach(KeyValuePair<int, int> entry in ability.requiredSkills)
                {
                    PantheraAbility abilityRequired = PantheraAbility.AbilitytiesDefsList[entry.Key];
                    skillsList += "  -" + abilityRequired.name + " " + entry.Value + "/" + abilityRequired.maxLevel + Environment.NewLine;
                }
                content.Find("RequiredSkills")?.Find("Text")?.GetComponent<TextMeshProUGUI>()?.SetText(skillsList);
            }
            else
            {
                content.Find("RequiredSkills")?.gameObject.SetActive(false);
            }

            // Set the Description //
            this.abilitiesTooltip.transform.Find("Content")?.Find("Description")?.GetComponent<TextMeshProUGUI>()?.SetText(ability.desc);

        }

        public void createSkillsTooltip(PantheraSkill skill)
        {
            // Enable the Tooltip //
            this.skillsTooltip.SetActive(true);

            // Get the Content //
            Transform content = this.skillsTooltip.transform.Find("Content");

            // Set the Name //
            this.skillsTooltip.transform.Find("Content")?.Find("Header")?.Find("AbilityName")?.GetComponent<TextMeshProUGUI>()?.SetText(skill.name);

            // Set the type //
            if (skill.type == PantheraSkill.SkillType.active)
                content.Find("Header")?.Find("SkillType")?.GetComponent<TextMeshProUGUI>()?.SetText("Active");
            else if (skill.type == PantheraSkill.SkillType.active)
                content.Find("Header")?.Find("SkillType")?.GetComponent<TextMeshProUGUI>()?.SetText("Passive");

            // Set the Icon //
            Image image = this.skillsTooltip.transform.Find("Content")?.Find("Header")?.Find("AbilityIcon")?.Find("Icon")?.GetComponent<Image>();
            image.sprite = skill.icon;

            // Set the Unlock Level //
            if (skill.unlockLevel > 0)
            {
                content.Find("UnlockLevel")?.Find("Amount")?.GetComponent<TextMeshProUGUI>()?.SetText(skill.unlockLevel.ToString());
                content.Find("UnlockLevel")?.gameObject.SetActive(true);
            }
            else
            {
                content.Find("UnlockLevel")?.gameObject.SetActive(false);
            }

            // Set the Cooldown //
            if (skill.cooldown > 0)
            {
                content.Find("Cooldown")?.Find("Amount")?.GetComponent<TextMeshProUGUI>()?.SetText(skill.cooldown.ToString());
                content.Find("Cooldown")?.gameObject.SetActive(true);
            }
            else
            {
                content.Find("Cooldown")?.gameObject.SetActive(false);
            }

            // Set the Energy cost //
            if (skill.requiredEnergy > 0)
            {
                content.Find("EnergyCost")?.Find("Amount")?.GetComponent<TextMeshProUGUI>()?.SetText(skill.requiredEnergy.ToString());
                content.Find("EnergyCost")?.gameObject.SetActive(true);
            }
            else
            {
                content.Find("EnergyCost")?.gameObject.SetActive(false);
            }

            // Set the Power cost //
            if (skill.requiredPower > 0)
            {
                content.Find("GodPowerCost")?.Find("Amount")?.GetComponent<TextMeshProUGUI>()?.SetText(skill.requiredPower.ToString());
                content.Find("GodPowerCost")?.gameObject.SetActive(true);
            }
            else
            {
                content.Find("GodPowerCost")?.gameObject.SetActive(false);
            }

            // Set the Fury cost //
            if (skill.requiredFury > 0)
            {
                content.Find("FuryCost")?.Find("Amount")?.GetComponent<TextMeshProUGUI>()?.SetText(skill.requiredFury.ToString());
                content.Find("FuryCost")?.gameObject.SetActive(true);
            }
            else
            {
                content.Find("FuryCost")?.gameObject.SetActive(false);
            }

            // Set the Description //
            this.skillsTooltip.transform.Find("Content")?.Find("Description")?.GetComponent<TextMeshProUGUI>()?.SetText(skill.desc);
        }

        public void updateAllKeyBindTexts()
        {
            // Itinerate all KeyBinds //
            foreach (KeyBind keyBind in this.keysBindList.Values)
            {
                // Find the Action ID //
                int actionID = KeysBinder.ActionList[keyBind.name];
                // Fill the Keyboard Key //
                TextMeshProUGUI keyboardText = keyBind.UIKeyboardButton.transform.Find("Text").GetComponent<TextMeshProUGUI>();
                ActionElementMap elementMap = KeysBinder.REKeyboardMap.GetFirstElementMapWithAction(actionID);
                keyboardText.text = elementMap?.elementIdentifierName;
                // Fill the Mouse Key //
                TextMeshProUGUI mouseText = keyBind.UIMouseButton.transform.Find("Text").GetComponent<TextMeshProUGUI>();
                ActionElementMap elementMap2 = KeysBinder.REMouseMap.GetFirstElementMapWithAction(actionID);
                mouseText.text = elementMap2?.elementIdentifierName;
                // Fill the Gamepad Key //
                TextMeshProUGUI gamepadText = keyBind.UIGamepadButton.transform.Find("Text").GetComponent<TextMeshProUGUI>();
                ActionElementMap elementMap3 = KeysBinder.REGamepadMap?.GetFirstElementMapWithAction(actionID);
                if (elementMap3 != null)
                    gamepadText.text = elementMap3?.elementIdentifierName;
                else
                    gamepadText.text = "No Gamepad";
            }

            // Check if there are Keys conflict //
            this.updateKeysBindConflict();

        }

        public void updateKeysBindConflict()
        {

            // Turn all Key Bind Label Color to normal //
            foreach (KeyBind keyBind in this.keysBindList.Values)
            {
                keyBind.UIKeyboardButton.GetComponent<Image>().color = PantheraConfig.KeysBindButtonDefaultColor;
                keyBind.UIMouseButton.GetComponent<Image>().color = PantheraConfig.KeysBindButtonDefaultColor;
                keyBind.UIGamepadButton.GetComponent<Image>().color = PantheraConfig.KeysBindButtonDefaultColor;
            }

            // Keyboard //
            foreach (ElementAssignmentConflictInfo info in KeysBinder.REKeyboardMap.ElementAssignmentConflicts(KeysBinder.REKeyboardMap, false))
            {
                // Check if the Key Bind exist //
                if (this.keysBindList.ContainsKey(info.actionId) == false) continue;
                // Turn the Key Bind Label to Red //
                this.keysBindList[info.actionId].UIKeyboardButton.GetComponent<Image>().color = PantheraConfig.KeysBindButtonConflictColor;
            }

            // Mouse //
            foreach (ElementAssignmentConflictInfo info in KeysBinder.REMouseMap.ElementAssignmentConflicts(KeysBinder.REMouseMap, false))
            {
                // Check if the Key Bind exist //
                if (this.keysBindList.ContainsKey(info.actionId) == false) continue;
                // Turn the Key Bind Label to Red //
                this.keysBindList[info.actionId].UIMouseButton.GetComponent<Image>().color = PantheraConfig.KeysBindButtonConflictColor;
            }

            // Joystick //
            if (KeysBinder.REGamepadMap != null)
            {
                foreach (ElementAssignmentConflictInfo info in KeysBinder.REGamepadMap.ElementAssignmentConflicts(KeysBinder.REGamepadMap, false))
                {
                    // Check if the Key Bind exist //
                    if (this.keysBindList.ContainsKey(info.actionId) == false) continue;
                    // Turn the Key Bind Label to Red //
                    this.keysBindList[info.actionId].UIGamepadButton.GetComponent<Image>().color = PantheraConfig.KeysBindButtonConflictColor;
                }
            }

        }

        public int getSlotIdFromObject(GameObject obj)
        {
            foreach (KeyValuePair<int, GameObject> value in this.skillsSlotList)
            {
                if (obj == value.Value) return value.Key;
            }
            return 0;
        }

        public int getActionIdFromKeyBindObject(GameObject obj)
        {
            foreach (KeyValuePair<int, KeyBind> value in this.keysBindList)
            {
                if (obj == value.Value.UIKeyboardButton) return value.Key;
                if (obj == value.Value.UIMouseButton) return value.Key;
                if (obj == value.Value.UIGamepadButton) return value.Key;
            }
            return 0;
        }

    }

}
