using Panthera.Components;
using Panthera.Utils;
using Rewired;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Panthera.GUI
{
    internal class ButtonWatcher : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {

        public ConfigPanel configPanel;
        public PantheraSkill attachedSkill;
        public Color defaultColor;

        public void OnPointerClick(PointerEventData eventData)
        {

            // Exit Button //
            if (this.gameObject.name == "ExitButton")
            {
                this.configPanel.configPanelGUI.gameObject.SetActive(false);
                this.configPanel.updateAllValues();
                KeysBinder.GamepadSetEnable(true);
                PantheraSaveSystem.SavePreset(Preset.SelectedPreset.presetID, Preset.SelectedPreset.saveData());
                Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject);
                return;
            }

            // Open the Overview Tab //
            if (this.gameObject.name == "Overview")
            {
                this.configPanel.overviewTab.active = true;
                this.configPanel.skillTreesTab.active = false;
                this.configPanel.skillsTab.active = false;
                this.configPanel.keysBindTab.active = false;
                this.configPanel.updateOverviewTab();
                Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject);
                return;
            }

            // Open the Skill Trees Tab //
            if (this.gameObject.name == "Skilltrees")
            {
                this.configPanel.overviewTab.active = false;
                this.configPanel.skillTreesTab.active = true;
                this.configPanel.skillsTab.active = false;
                this.configPanel.keysBindTab.active = false;
                this.configPanel.updateAllValues();
                Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject);
                return;
            }

            // Open the Skills Tab //
            if (this.gameObject.name == "Skills")
            {
                this.configPanel.overviewTab.active = false;
                this.configPanel.skillTreesTab.active = false;
                this.configPanel.skillsTab.active = true;
                this.configPanel.keysBindTab.active = false;
                this.configPanel.updateSkillsIconsList();
                Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject);
                return;
            }

            // Open the Keys Bind Tab //
            if (this.gameObject.name == "KeysBind")
            {
                this.configPanel.overviewTab.active = false;
                this.configPanel.skillTreesTab.active = false;
                this.configPanel.skillsTab.active = false;
                this.configPanel.keysBindTab.active = true;
                this.configPanel.updateAllKeyBindTexts();
                Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject);
                return;
            }

            // Skill Trees Tab 1 Button pressed //
            if (this.gameObject.name == "SkillTreeTab1")
            {
                this.configPanel.skillTree1.SetActive(true);
                this.configPanel.skillTree2.SetActive(false);
                this.configPanel.skillTree3.SetActive(false);
                this.configPanel.skillTree4.SetActive(false);
                this.configPanel.updateAllValues();
                Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject);
            }

            // Skill Trees Tab 2 Button pressed //
            if (this.gameObject.name == "SkillTreeTab2")
            {
                this.configPanel.skillTree1.SetActive(false);
                this.configPanel.skillTree2.SetActive(true);
                this.configPanel.skillTree3.SetActive(false);
                this.configPanel.skillTree4.SetActive(false);
                this.configPanel.updateAllValues();
                Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject);
            }

            // Skill Trees Tab 3 Button pressed //
            if (this.gameObject.name == "SkillTreeTab3")
            {
                this.configPanel.skillTree1.SetActive(false);
                this.configPanel.skillTree2.SetActive(false);
                this.configPanel.skillTree3.SetActive(true);
                this.configPanel.skillTree4.SetActive(false);
                this.configPanel.updateAllValues();
                Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject);
            }

            // Skill Trees Tab 4 Button pressed //
            if (this.gameObject.name == "SkillTreeTab4")
            {
                this.configPanel.skillTree1.SetActive(false);
                this.configPanel.skillTree2.SetActive(false);
                this.configPanel.skillTree3.SetActive(false);
                this.configPanel.skillTree4.SetActive(true);
                this.configPanel.updateAllValues();
                Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject);
            }

            // Skill Tree Ability Button pressed //
            if (this.gameObject.name.Contains("Ability"))
            {
                string abilitIDString = Regex.Replace(this.gameObject.name, @"[^\d]", "");
                int abilityID = Int32.Parse(abilitIDString);
                PantheraAbility ability = PantheraAbility.AbilitytiesDefsList[abilityID];
                if (PantheraAbility.CanBeUpgraded(ability) == false) return;
                Preset.SelectedPreset.addAbilityPoint(ability.abilityID);
                Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject);
                this.configPanel.updateAllValues();
            }

            // Bar Skill Button pressed //
            if (this.gameObject.name.Contains("BarSkill") && eventData.button == PointerEventData.InputButton.Right)
            {
                int slotID = this.configPanel.getSlotIdFromObject(this.gameObject);
                Preset.SelectedPreset.removeSkillFromSlot(slotID);
                Utils.Sound.playSound(Utils.Sound.Page2, this.gameObject);
            }

            // Key Bind Button pressed //
            if (this.gameObject.name.Contains("KBButton") == true)
            {
                this.configPanel.keyBindWindow.SetActive(true);
                KeysBinder.StartMapping(this);
                Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject);
            }

            // Key Bind Window Cancel Button pressed //
            if (this.gameObject.name == ("CancelButton"))
            {
                Image image = this.GetComponent<Image>();
                image.color = this.defaultColor;
                this.configPanel.keyBindWindow.SetActive(false);
                KeysBinder.CurrentInputMapper.Clear();
                this.configPanel.updateAllKeyBindTexts();
                KeysBinder.CurrentInputMapper = null;
                KeysBinder.CurrentMapping = null;
                Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject);
            }

            // Key Bind Window Remove Button pressed //
            if (this.gameObject.name == ("RemoveButton"))
            {
                Image image = this.GetComponent<Image>();
                image.color = this.defaultColor;
                this.configPanel.keyBindWindow.SetActive(false);
                KeysBinder.CurrentContext.controllerMap.DeleteElementMapsWithAction(KeysBinder.CurrentContext.actionId);
                KeysBinder.CurrentInputMapper.Clear();
                this.configPanel.updateAllKeyBindTexts();
                KeysBinder.CurrentInputMapper = null;
                KeysBinder.CurrentMapping = null;
                Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject);
            }

            // Preset Button //
            if (this.gameObject.name.Contains("Preset") == true)
            {
                // Find the Preset ID //
                string presetIDString = this.gameObject.name.Replace("Preset", "");
                int presetID = Int32.Parse(presetIDString);
                // Check the Preset ID //
                if (presetID != 0)
                {
                    // Load the Preset //
                    this.configPanel.changePreset(presetID);
                }
                Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject);
            }

            // Overview Reset Preset Button //
            if (this.gameObject.name == "ResetButton")
            {
                this.configPanel.resetPresetWindow.transform.Find("Content").Find("CancelResetPrtButton").GetComponent<Image>().color = PantheraConfig.PresetResetButtonNormalColor;
                this.configPanel.resetPresetWindow.transform.Find("Content").Find("ResetPrtButton").GetComponent<Image>().color = PantheraConfig.PresetResetButtonNormalColor;
                if (this.configPanel.lunarCoin < PantheraConfig.ResetPresetCost)
                    this.configPanel.resetPresetWindow.transform.Find("Content").Find("ResetPrtButton").GetComponent<Image>().color = PantheraConfig.PresetResetButtonDisabledColor;
                this.configPanel.resetPresetWindow.SetActive(true);
                Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject);
            }

            // Overview Activate Preset Button //
            if (this.gameObject.name == "ActivateButton")
            {
                this.configPanel.activatePresetWindow.transform.Find("Content").Find("CancelActivatePrtButton").GetComponent<Image>().color = PantheraConfig.PresetActivateButtonNormalColor;
                this.configPanel.activatePresetWindow.transform.Find("Content").Find("ActivatePrtButton").GetComponent<Image>().color = PantheraConfig.PresetActivateButtonNormalColor;
                if (Preset.SelectedPreset.presetID == Preset.ActivePreset.presetID) return;
                if (this.configPanel.lunarCoin < PantheraConfig.ActivatePresetCost)
                    this.configPanel.activatePresetWindow.transform.Find("Content").Find("ActivatePrtButton").GetComponent<Image>().color = PantheraConfig.PresetActivateButtonDisabledColor;
                this.configPanel.activatePresetWindow.SetActive(true);
                Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject);
            }

            // Overview Reset Preset Cancel Button pressed //
            if (this.gameObject.name == "CancelResetPrtButton")
            {
                this.configPanel.resetPresetWindow.SetActive(false);
                Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject);
            }

            // Overview Activate Preset Cancel Button pressed //
            if (this.gameObject.name == "CancelActivatePrtButton")
            {
                this.configPanel.activatePresetWindow.SetActive(false);
                Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject);
            }

            // Overview Reset Preset Reset Button //
            if (this.gameObject.name == "ResetPrtButton")
            {
                if (this.configPanel.lunarCoin < PantheraConfig.ResetPresetCost) return;
                int presetID = Preset.SelectedPreset.presetID;
                Preset.SelectedPreset = new Preset(this.configPanel, presetID);
                if (presetID == Preset.ActivePreset.presetID)
                    Preset.ActivePreset = Preset.SelectedPreset;
                this.configPanel.lunarCoin -= PantheraConfig.ResetPresetCost;
                this.configPanel.updateAllValues();
                this.configPanel.resetPresetWindow.SetActive(false);
                Utils.Sound.playSound(Utils.Sound.ResetPreset, this.gameObject);
            }

            // Overview Activate Preset Activate Button //
            if (this.gameObject.name == "ActivatePrtButton")
            {
                if (this.configPanel.lunarCoin < PantheraConfig.ActivatePresetCost) return;
                Preset.ActivePreset = Preset.SelectedPreset;
                this.configPanel.lunarCoin -= PantheraConfig.ActivatePresetCost;
                this.configPanel.updateAllValues();
                this.configPanel.activatePresetWindow.SetActive(false);
                Utils.Sound.playSound(Utils.Sound.SwitchPreset, this.gameObject);
            }

        }

        public void OnPointerEnter(PointerEventData eventData)
        {

            // Config Button //
            if (this.name == "ConfigButton(Clone)")
            {
                // Check if the Config Panel is not already opened //
                if (this.configPanel.configPanelGUI.active == true) return;
                // Change the color //
                Image image = this.GetComponent<Image>();
                this.defaultColor = image.color;
                image.color = PantheraConfig.ConfigButtonHoveredColor;
            }

            // Exit Button //
            if (this.name == "ExitButton")
            {
                // Change the color //
                Image image = this.GetComponent<Image>();
                this.defaultColor = image.color;
                image.color = PantheraConfig.ExitButtonHoveredColor;
            }

            // Tab Buttons //
            if (this.name == "Overview" || this.name == "Skilltrees" || this.name == "Skills" || this.name == "KeysBind")
            {
                // Change the color //
                Image image = this.GetComponent<Image>();
                this.defaultColor = image.color;
                image.color = PantheraConfig.TabButtonHoveredColor;
            }

            // Skill Tree Tab Buttons //
            if (this.name.Contains("SkillTreeTab"))
            {
                // Change the color //
                Image image = this.transform.Find("Background").GetComponent<Image>();
                this.defaultColor = image.color;
                image.color = PantheraConfig.SkillTreeTabButtonHoveredColor;
            }

            // Skill Tree Ability Button //
            if (this.gameObject.name.Contains("Ability"))
            {
                Image image = this.transform.Find("FrameBackground").GetComponent<Image>();
                this.defaultColor = image.color;
                image.color = PantheraConfig.SkillTreeAbilityHoveredColor;
                string abilitIDString = Regex.Replace(this.gameObject.name, @"[^\d]", "");
                int abilityID = Int32.Parse(abilitIDString);
                PantheraAbility ability = PantheraAbility.AbilitytiesDefsList[abilityID];
                this.configPanel.createAbilityTooltip(ability);
            }

            // Skills Ability Buttons //
            if (this.name == "SkillIconActive(Clone)" || this.name == "SkillIconPassive(Clone)")
            {
                // Change the color //
                Image image = this.transform.Find("FrameBackground").GetComponent<Image>();
                this.defaultColor = image.color;
                image.color = PantheraConfig.AbilityButtonHoveredColor;
                this.configPanel.createSkillsTooltip(this.attachedSkill);
            }

            // Skill Bar Button //
            if(this.name.Contains("BarSkill"))
            {
                // Change the Color //
                Image image = this.transform.Find("BackgroundFrame").GetComponent<Image>();
                this.defaultColor = image.color;
                image.color = PantheraConfig.SkillButtonHoveredColor;
            }

            // Key Bind Button //
            if (this.name.Contains("KBButton"))
            {
                Image image = this.GetComponent<Image>();
                this.defaultColor = image.color;
                image.color = PantheraConfig.KeysBindButtonHoveredColor;
            }

            // Key Bind Window Confirm or Cancel Button //
            if (this.name == "RemoveButton" || this.name == "CancelButton")
            {
                Image image = this.GetComponent<Image>();
                this.defaultColor = image.color;
                image.color = PantheraConfig.KeysBindWindowButtonHoveredColor;
                KeysBinder.CurrentInputMapper.Stop();
            }

            // Preset Buttons //
            if (this.name.Contains("Preset") == true)
            {
                Image image = this.GetComponent<Image>();
                this.defaultColor = image.color;
                image.color = PantheraConfig.PresetButtonHoveredColor;
            }

            // Overview Buttons //
            if (this.name == "ResetButton" || this.name == "ActivateButton" || this.name == "CancelActivatePrtButton" || this.name == "ActivatePrtButton" || this.name == "CancelResetPrtButton" || this.name == "ResetPrtButton")
            {
                if (this.name == "ActivateButton" && Preset.SelectedPreset.presetID == Preset.ActivePreset.presetID) return;
                if (this.name == "ActivatePrtButton" && this.configPanel.lunarCoin < PantheraConfig.ActivatePresetCost) return;
                if (this.name == "ResetPrtButton" && this.configPanel.lunarCoin < PantheraConfig.ResetPresetCost) return;
                Image image = this.GetComponent<Image>();
                this.defaultColor = image.color;
                image.color = PantheraConfig.OverviewButtonsHoveredColor;
            }

        }

        public void OnPointerExit(PointerEventData eventData)
        {

            // Config Button //
            if (this.name == "ConfigButton(Clone)")
            {
                // Check if the Config Panel is not already opened //
                if (this.configPanel.configPanelGUI.active == true) return;
                // Change the color //
                Image image = this.GetComponent<Image>();
                image.color = this.defaultColor;
            }

            // Exit Button //
            if (this.name == "ExitButton")
            {
                // Change the color //
                Image image = this.GetComponent<Image>();
                image.color = this.defaultColor;
            }

            // Tab Buttons //
            if (this.name == "Overview" || this.name == "Skilltrees" || this.name == "Skills" || this.name == "KeysBind")
            {
                // Change the color //
                Image image = this.GetComponent<Image>();
                image.color = this.defaultColor;
            }

            // Skill Tree Tab Buttons //
            if (this.name.Contains("SkillTreeTab"))
            {
                // Change the color //
                Image image = this.transform.Find("Background").GetComponent<Image>();
                image.color = this.defaultColor;
            }

            // Skill Tree Ability Button //
            if (this.gameObject.name.Contains("Ability"))
            {
                Image image = this.transform.Find("FrameBackground").GetComponent<Image>();
                image.color = this.defaultColor;
                this.configPanel.abilitiesTooltip.SetActive(false);
            }

            // Skills Ability Buttons //
            if (this.name == "SkillIconActive(Clone)" || this.name == "SkillIconPassive(Clone)")
            {
                // Change the color //
                Image image = this.transform.Find("FrameBackground")?.GetComponent<Image>();
                image.color = this.defaultColor;
                this.configPanel.skillsTooltip.SetActive(false);
            }

            // Skill Bar Button //
            if (this.name.Contains("BarSkill"))
            {
                // Change the Color //
                Image image = this.transform.Find("BackgroundFrame").GetComponent<Image>();
                image.color = this.defaultColor;
            }

            // Key Bind Button //
            if (this.name.Contains("KBButton"))
            {
                Image image = this.GetComponent<Image>();
                image.color = this.defaultColor;
            }

            // Key Bind Window Remove or Cancel Button //
            if (this.name == "RemoveButton" || this.name == "CancelButton")
            {
                Image image = this.GetComponent<Image>();
                image.color = this.defaultColor;
                KeysBinder.CurrentInputMapper.Start(KeysBinder.CurrentContext);
            }

            // Preset Buttons //
            if (this.name.Contains("Preset") == true)
            {
                Image image = this.GetComponent<Image>();
                image.color = this.defaultColor;
            }

            // Overview Buttons //
            if (this.name == "ResetButton" || this.name == "ActivateButton" || this.name == "CancelActivatePrtButton" || this.name == "ActivatePrtButton" || this.name == "CancelResetPrtButton" || this.name == "ResetPrtButton")
            {
                if (this.name == "ActivateButton" && Preset.SelectedPreset.presetID == Preset.ActivePreset.presetID) return;
                if (this.name == "ActivatePrtButton" && this.configPanel.lunarCoin < PantheraConfig.ActivatePresetCost) return;
                if (this.name == "ResetPrtButton" && this.configPanel.lunarCoin < PantheraConfig.ResetPresetCost) return;
                Image image = this.GetComponent<Image>();
                image.color = this.defaultColor;
            }

        }

    }
}
