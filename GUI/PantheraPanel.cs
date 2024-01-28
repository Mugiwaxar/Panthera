using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.GUI.Tabs;
using Panthera.GUI.Tooltips;
using Panthera.Utils;
using Rewired;
using Rewired.Data;
using RewiredConsts;
using RoR2;
using RoR2.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Player = Rewired.Player;

namespace Panthera.GUI
{
    public class PantheraPanel : MonoBehaviour
    {

        public static CharacterSelectController CharacterSelectUI;
        public static CanvasGroup OrigGUICanvaGroup;

        public GameObject pantheraCanvas;
        public GameObject pantheraPanelGUI;
        public KeysBinder KeysBinder; // TO DO !!!

        public PantheraObj ptraObj
        {
            get
            {
                return Panthera.PantheraCharacter.pantheraObj;
            }
        }

        public OverviewTab overviewTab;
        public SkillsTab skillsTab;
        public CombosTab combosTab;
        public KeysBindTab keysBindTab;

        public bool scaled
        {
            get
            {
                string scaleString = PantheraSaveSystem.ReadValue("PantheraPanelScaled");
                if (scaleString != null && scaleString.ToLower() == "true")
                {
                    return true;
                }
                else
                {
                    return false;
                }
                    
            }
            set
            {
                PantheraSaveSystem.SetValue("PantheraPanelScaled", value.ToString().ToLower());
                PantheraSaveSystem.Save();
            }
        }

        public static void CharacterSelectionStart(Action<RoR2.UI.CharacterSelectController> orig, RoR2.UI.CharacterSelectController self)
        {
            // Use the original function //
            orig(self);
            // Save the UI //
            CharacterSelectUI = self;
        }

        public void Start()
        {

            // Create the Panthera Canva //
            this.pantheraCanvas = UnityEngine.Object.Instantiate<GameObject>(Assets.PantheraCanvas, Panthera.Instance.transform, false);

            // Create the Panthera Panel GUI //
            this.pantheraPanelGUI = UnityEngine.Object.Instantiate<GameObject>(Assets.ConfigPanelPrefab, this.pantheraCanvas.transform, false);
            this.pantheraPanelGUI.SetActive(false);

            // Add the Button Watcher to all Buttons //
            this.registerAllButtons();

            // Create all Tabs //
            this.createTabs();

            // Create all Tooltips //
            SimpleTooltip.CreateTooltip(this.pantheraCanvas);
            SkillsTooltip.CreateTooltip(this.pantheraCanvas);
            AbilitiesTooltip.CreateTooltip(this.pantheraCanvas);
            BuffsTooltip.CreateTooltip(this.pantheraCanvas);

        }

        private void createTabs()
        {

            // Creating the Overview Tab //
            this.overviewTab = new OverviewTab(this);
            this.overviewTab.tabObj.SetActive(true);

            // Creating the Skills Tab //
            this.skillsTab = new SkillsTab(this);
            this.skillsTab.tabObj.SetActive(false);

            // Creating the Combos Tab //
            this.combosTab = new CombosTab(this);
            this.combosTab.tabObj.SetActive(false);

            // Creating the Keys Bind Tab //
            this.keysBindTab = new KeysBindTab(this);
            this.keysBindTab.tabObj.SetActive(false);

        }

        private void registerAllButtons()
        {
            // Find all Panthera Components //
            Component[] components = this.pantheraPanelGUI.GetComponentsInChildren<Component>(true);
            // Register all Buttons //
            foreach (Component component in components)
            {
                // Check if this is a Button //
                if (component is Button button)
                {
                    // Add the Button Watcher Component //
                    ButtonWatcher comp = button.gameObject.AddComponent<ButtonWatcher>();
                    comp.pantheraPanel = this;
                }
            }
        }

        public void Update()
        {

            // Stop if the Panthera Panel GUI is inactive //
            if (this.pantheraPanelGUI.active == false)
                return;

            // Close if the Character Selection is null //
            if (CharacterSelectUI == null && this.ptraObj == null)
            {
                this.close();
                return;
            }

            // Close if P4N7H3R4 is not played //
            if(CharacterSelectUI != null && CharacterSelectUI.survivorName.text != "P4N7H3R4")
            {
                this.close();
                return;
            }

            // Updates Tabs //
            if (this.overviewTab.tabObj.active == true) 
               this.overviewTab.update();
            if (this.skillsTab.tabObj.active == true)
                this.skillsTab.update();
            if (this.combosTab.tabObj.active == true)
                this.combosTab.update();
            if (this.keysBindTab.tabObj.active == true)
                this.keysBindTab.update();

        }

        public void open()
        {

            // Check if inside Character Selection or ingame //
            if (CharacterSelectUI == null && this.ptraObj == null) return;

            // Check if P4N7H3R4 is selected //
            if (CharacterSelectUI != null && CharacterSelectUI.survivorName.text != "P4N7H3R4")
                return;

            // Load //
            PantheraSaveSystem.Load();

            // Scale if Needed //
            this.scale(this.scaled);

            // Enable the Config Panel //
            this.pantheraPanelGUI.SetActive(true);

            // Disable the Gamepad //
            KeysBinder.GamepadSetEnable(false);

            // Enable the Cursor //
            if (this.ptraObj != null && Panthera.InputPlayer != null && MPEventSystemManager.FindEventSystem(Panthera.InputPlayer) != null)
            {
                MPEventSystemManager.FindEventSystem(Panthera.InputPlayer).cursorOpenerCount = 1;
            }

            // Recalculate Stats //
            if (this.ptraObj != null && this.ptraObj.characterBody != null)
                this.ptraObj.characterBody.RecalculateStats();

            // Set the Level for the HUD //
            if (Panthera.PantheraHUD != null)
                Panthera.PantheraHUD.levelUpObj.active = false;

            // Play the Sound //
            Utils.Sound.playSound(Utils.Sound.OpenGUI, this.gameObject, false);

        }

        public void close()
        {

            // Disable the Config Panel //
            this.pantheraPanelGUI.SetActive(false);

            // Hide all Tooltips //
            SimpleTooltip.showCounter = 0;
            SkillsTooltip.showCounter = 0;
            AbilitiesTooltip.ShowCounter = 0;

            // Enable the Gamepad //
            KeysBinder.GamepadSetEnable(true);

            // Close the Skills Tree Window //
            this.skillsTab.skillTreeController.skillsTreeWindow.active = false;

            // Disable the Cursor //
            if (this.ptraObj != null && Panthera.InputPlayer != null && MPEventSystemManager.FindEventSystem(Panthera.InputPlayer) != null)
            {
                MPEventSystemManager.FindEventSystem(Panthera.InputPlayer).cursorOpenerCount = 0;
            }

            // Recalculate Stats //
            if (this.ptraObj != null && this.ptraObj.characterBody != null)
                this.ptraObj.characterBody.RecalculateStats();

            // Play the Sound //
            Utils.Sound.playSound(Utils.Sound.CloseGUI, this.gameObject, false);

            // Save //
            PantheraSaveSystem.Save();

        }

        public void setScaled(bool scale)
        {
            this.scaled = scale;
            this.scale(scale);
        }

        public void scale(bool scale)
        {
            if (scale == true)
                this.pantheraPanelGUI.transform.localScale = new Vector3(1.35f, 1.35f, 1.35f);
            else
                this.pantheraPanelGUI.transform.localScale = new Vector3(1, 1, 1);
        }

        public void switchOverviewTable()
        {
            this.overviewTab.enable();
            this.skillsTab.disable();
            this.combosTab.disable();
            this.keysBindTab.disable();
        }

        public void switchSkillsTab()
        {
            this.overviewTab.disable();
            this.skillsTab.enable();
            this.combosTab.disable();
            this.keysBindTab.disable();
        }

        public void switchCombosTab()
        {
            this.overviewTab.disable();
            this.skillsTab.disable();
            this.combosTab.enable();
            this.keysBindTab.disable();
        }

        public void switchKeysBindTab()
        {
            this.overviewTab.disable();
            this.skillsTab.disable();
            this.combosTab.disable();
            this.keysBindTab.enable();
        }

    }
}
