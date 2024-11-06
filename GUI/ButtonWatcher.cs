using Panthera.Abilities;
using Panthera.Base;
using Panthera.Components;
using Panthera.GUI;
using Panthera.NetworkMessages;
using Panthera.Utils;
using R2API.Networking;
using R2API.Networking.Interfaces;
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
    public class ButtonWatcher : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {

        public PantheraPanel pantheraPanel;
        public Color defaultColor;
        public KeyBind keyBindObj;
        public PantheraAbility associatedAbility;

        public void OnPointerClick(PointerEventData eventData)
        {

            // Scale Windows Button //
            if (this.name == "ScaleWindowButton")
            {
                // Change the Size of the Panthera Panel //
                if (this.pantheraPanel.scaled == false)
                {
                    this.pantheraPanel.setScaled(true);
                }
                else
                {
                    this.pantheraPanel.setScaled(false);
                }
                Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject, false);
                return;
            }

            // Exit Button //
            if (this.name == "ExitButton")
            {
                this.pantheraPanel.close();
                return;
            }

            // Open the Overview Tab //
            if (this.name == "Overview")
            {
                this.pantheraPanel.switchOverviewTable();
                Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject, false);
                return;
            }

            // Open the Skill Trees Tab //
            if (this.name == "Skills")
            {
                this.pantheraPanel.switchSkillsTab();
                Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject, false);
                return;
            }

            // Open the Skills Tab //
            if (this.name == "Combos")
            {
                this.pantheraPanel.switchCombosTab();
                Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject, false);
                return;
            }

            // Open the Keys Bind Tab //
            if (this.name == "KeysBind")
            {
                this.pantheraPanel.switchKeysBindTab();
                Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject, false);
                return;
            }

            // Overview Endurance Button //
            if (this.name == "EnduranceButton")
            {
                if (Panthera.PantheraCharacter.attributePointsLeft > 0)
                {
                    Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject, false);
                    Panthera.ProfileComponent.endurance += 1;
                    Panthera.ProfileComponent.syncProfile();
                    Panthera.ProfileComponent.saveAttributes();
                }
                return;
            }

            // Overview Force Button //
            if (this.name == "ForceButton")
            {
                if (Panthera.PantheraCharacter.attributePointsLeft > 0)
                {
                    Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject, false);
                    Panthera.ProfileComponent.force += 1;
                    Panthera.ProfileComponent.syncProfile();
                    Panthera.ProfileComponent.saveAttributes();
                }
                return;
            }

            // Overview Agility Button //
            if (this.name == "AgilityButton")
            {
                if (Panthera.PantheraCharacter.attributePointsLeft > 0)
                {
                    Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject, false);
                    Panthera.ProfileComponent.agility += 1;
                    Panthera.ProfileComponent.syncProfile();
                    Panthera.ProfileComponent.saveAttributes();
                }
                return;
            }

            // Overview Swiftness Button //
            if (this.name == "SwiftnessButton")
            {
                if (Panthera.PantheraCharacter.attributePointsLeft > 0)
                {
                    Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject, false);
                    Panthera.ProfileComponent.swiftness += 1;
                    Panthera.ProfileComponent.syncProfile();
                    Panthera.ProfileComponent.saveAttributes();
                }
                return;
            }

            // Overview Dexterity Button //
            if (this.name == "DexterityButton")
            {
                if (Panthera.PantheraCharacter.attributePointsLeft > 0)
                {
                    Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject, false);
                    Panthera.ProfileComponent.dexterity += 1;
                    Panthera.ProfileComponent.syncProfile();
                    Panthera.ProfileComponent.saveAttributes();
                }
                return;
            }

            // Overview Spirit Button //
            if (this.name == "SpiritButton")
            {
                if (Panthera.PantheraCharacter.attributePointsLeft > 0)
                {
                    Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject, false);
                    Panthera.ProfileComponent.spirit += 1;
                    Panthera.ProfileComponent.syncProfile();
                    Panthera.ProfileComponent.saveAttributes();
                }
                return;
            }

            // Overview Reset Attribute Button //
            if (this.name == "OverviewResetAttributesButton")
            {
                this.pantheraPanel.overviewTab.resetAttributesWindow.transform.Find("Content").Find("CancelResetAttributesButton").GetComponent<Image>().color = PantheraConfig.PresetResetButtonNormalColor;
                this.pantheraPanel.overviewTab.resetAttributesWindow.transform.Find("Content").Find("ResetAttributesButton").GetComponent<Image>().color = PantheraConfig.PresetResetButtonNormalColor;
                if (Panthera.PantheraCharacter.lunarCoin < PantheraConfig.ResetAttributesCost)
                    this.pantheraPanel.overviewTab.resetAttributesWindow.transform.Find("Content").Find("ResetAttributesButton").GetComponent<Image>().color = PantheraConfig.PresetResetButtonDisabledColor;
                this.pantheraPanel.overviewTab.resetAttributesWindow.SetActive(true);
                Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject, false);
                return;
            }

            // Overview Reset Character Cancel Button pressed //
            if (this.name == "CancelResetAttributesButton")
            {
                this.pantheraPanel.overviewTab.resetAttributesWindow.SetActive(false);
                Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject, false);
                return;
            }

            // Overview Reset Character Reset Button //
            if (this.gameObject.name == "ResetAttributesButton")
            {
                if (Panthera.PantheraCharacter.lunarCoin < PantheraConfig.ResetAttributesCost) return;
                Panthera.PantheraCharacter.lunarCoin -= PantheraConfig.ResetAttributesCost;
                Panthera.ProfileComponent.resetAttributes();
                this.pantheraPanel.overviewTab.resetAttributesWindow.SetActive(false);
                Utils.Sound.playSound(Utils.Sound.ResetChar, this.gameObject, false);
                return;
            }

            // Overview Reset Skills Tree Button //
            if (this.name == "OverviewResetSkillsTreeButton")
            {
                this.pantheraPanel.overviewTab.resetSkillsTreeWindow.transform.Find("Content").Find("CancelResetSkillsTreeButton").GetComponent<Image>().color = PantheraConfig.PresetResetButtonNormalColor;
                this.pantheraPanel.overviewTab.resetSkillsTreeWindow.transform.Find("Content").Find("ResetSkillsTreeButton").GetComponent<Image>().color = PantheraConfig.PresetResetButtonNormalColor;
                if (Panthera.PantheraCharacter.lunarCoin < PantheraConfig.ResetSkillsTreeCost)
                    this.pantheraPanel.overviewTab.resetSkillsTreeWindow.transform.Find("Content").Find("ResetSkillsTreeButton").GetComponent<Image>().color = PantheraConfig.PresetResetButtonDisabledColor;
                this.pantheraPanel.overviewTab.resetSkillsTreeWindow.SetActive(true);
                Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject, false);
                return;
            }

            // Overview Reset Character Cancel Button pressed //
            if (this.name == "CancelResetSkillsTreeButton")
            {
                this.pantheraPanel.overviewTab.resetSkillsTreeWindow.SetActive(false);
                Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject, false);
                return;
            }

            // Overview Reset Character Reset Button //
            if (this.gameObject.name == "ResetSkillsTreeButton")
            {
                if (Panthera.PantheraCharacter.lunarCoin < PantheraConfig.ResetSkillsTreeCost) return;
                Panthera.PantheraCharacter.lunarCoin -= PantheraConfig.ResetSkillsTreeCost;
                Panthera.ProfileComponent.resetSkillsTree();
                this.pantheraPanel.overviewTab.resetSkillsTreeWindow.SetActive(false);
                Utils.Sound.playSound(Utils.Sound.ResetChar, this.gameObject, false);
                return;
            }

            // Skills Open Skills Tree Button //
            if (this.gameObject.name == "OpenSkillsTreeButton" == true)
            {
                this.pantheraPanel.skillsTab.skillTreeController.skillsTreeWindow.active = true;
                Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject, false);
                return;
            }

            // Skills Tree Window Exit Button //
            if (this.name == "SkillsTreeWindowExitButton")
            {
                this.pantheraPanel.skillsTab.skillTreeController.skillsTreeWindow.active = false;
                return;
            }

            // Key Bind Button pressed //
            if (this.gameObject.name.Contains("KBButton") == true)
            {
                this.pantheraPanel.keysBindTab.keysBindWindow.active = true;
                KeysBinder.StartMapping(this.name, this.keyBindObj, this.pantheraPanel.keysBindTab);
                Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject, false);
                return;
            }

            // Key Bind Window Cancel Button pressed //
            if (this.gameObject.name == ("KeysBindCancelButton"))
            {
                Image image = this.GetComponent<Image>();
                image.color = this.defaultColor;
                this.pantheraPanel.keysBindTab.keysBindWindow.SetActive(false);
                KeysBinder.InputMapper.Stop();
                KeysBinder.CurrentMapping = null;
                Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject, false);
                return;
            }

            // Key Bind Window Remove Button pressed //
            if (this.gameObject.name == ("KeysBindRemoveButton"))
            {
                Image image = this.GetComponent<Image>();
                image.color = this.defaultColor;
                this.pantheraPanel.keysBindTab.keysBindWindow.SetActive(false);
                KeysBinder.CurrentContext.controllerMap.DeleteElementMapsWithAction(KeysBinder.CurrentContext.actionId);
                ControllerMap map = KeysBinder.CurrentContext.controllerMap;
                KeysBinder.InputMapper.Stop();
                KeysBinder.CurrentMapping = null;
                Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject, false);
                return;
            }

            // Reset Key Bind Button pressed //
            if (this.gameObject.name == "DefaultButton")
            {
                this.pantheraPanel.keysBindTab.keysBindResetWindow.active = true;
                Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject, false);
                return;
            }

            // Reset Key Bind Window Cancel Button pressed //
            if (this.gameObject.name == ("CancelResetKeysBindButton"))
            {
                Image image = this.GetComponent<Image>();
                image.color = this.defaultColor;
                this.pantheraPanel.keysBindTab.keysBindResetWindow.SetActive(false);
                Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject, false);
                return;
            }

            // Reset Key Bind Window Reset Button pressed //
            if (this.gameObject.name == ("ResetResetKeysBindButton"))
            {
                Image image = this.GetComponent<Image>();
                image.color = this.defaultColor;
                this.pantheraPanel.keysBindTab.keysBindResetWindow.SetActive(false);
                KeysBinder.ResetAllKeysBind();
                Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject, false);
                return;
            }

            // Skills Tree Ability Button Left Click //
            if (this.name.Contains("Ability") == true && eventData.button == PointerEventData.InputButton.Left)
            {
                if (Panthera.PantheraCharacter.skillPointsLeft < 1)
                    return;
                Panthera.ProfileComponent.upgradeAbility(this.associatedAbility.abilityID);
                Panthera.ProfileComponent.syncProfile();
                Panthera.ProfileComponent.saveSkillsTree();
                Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject, false);
                return;
            }

            // Skills Tree Ability Button Right Click //
            if (this.name.Contains("Ability") == true && eventData.button == PointerEventData.InputButton.Right)
            {
                if (this.associatedAbility.hasMastery == true && Panthera.ProfileComponent.isMastery(this.associatedAbility.abilityID) == true && Panthera.PantheraCharacter.pantheraObj == null)
                    Panthera.ProfileComponent.setMastery(this.associatedAbility.abilityID, false);
                else if (this.associatedAbility.hasMastery == true && Panthera.ProfileComponent.isMastery(this.associatedAbility.abilityID) == false && Panthera.ProfileComponent.getMasteryPointsLeft() > 0)
                    Panthera.ProfileComponent.setMastery(this.associatedAbility.abilityID, true);
                else
                    return;
                Panthera.ProfileComponent.syncProfile();
                Panthera.ProfileComponent.saveSkillsTree();
                Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject, false);
                return;
            }

        }

        public void OnPointerEnter(PointerEventData eventData)
        {

            // Scale Windows Button //
            if (this.name == "ScaleWindowButton")
            {
                // Change the color //
                Image image = this.GetComponent<Image>();
                image.color = PantheraConfig.ScaleWindowButtonHoveredColor;
                return;
            }

            // Exit Button //
            if (this.name == "ExitButton" || this.name == "SkillsTreeWindowExitButton")
            {
                // Change the color //
                Image image = this.GetComponent<Image>();
                image.color = PantheraConfig.ExitButtonHoveredColor;
                return;
            }

            // Tab Buttons //
            if (this.name == "Overview" || this.name == "Skills" || this.name == "Combos" || this.name == "KeysBind")
            {
                // Change the color //
                Image image = this.GetComponent<Image>();
                image.color = PantheraConfig.TabButtonHoveredColor;
                return;
            }

            // Overview Stats Buttons //
            if (this.name == "EnduranceButton" || this.name == "ForceButton" || this.name == "AgilityButton" || this.name == "SwiftnessButton" || this.name == "DexterityButton" || this.name == "SpiritButton")
            {
                if (Panthera.PantheraCharacter.attributePointsLeft > 0)
                {
                    Image image = this.GetComponent<Image>();
                    image.color = PantheraConfig.OverviewButtonsHoveredColor;
                }
                return;
            }

            // Overview Reset Attributes Button //
            if (this.name == "OverviewResetAttributesButton" || this.name == "CancelResetAttributesButton" || this.name == "ResetAttributesButton")
            {
                if (this.name == "ResetAttributesButton" && Panthera.PantheraCharacter.lunarCoin < PantheraConfig.ResetAttributesCost) return;
                Image image = this.GetComponent<Image>();
                image.color = PantheraConfig.OverviewButtonsHoveredColor;
                return;
            }

            // Overview Reset Skills Tree Button //
            if (this.name == "OverviewResetSkillsTreeButton" || this.name == "CancelResetSkillsTreeButton" || this.name == "ResetSkillsTreeButton")
            {
                if (this.name == "ResetSkillsTreeButton" && Panthera.PantheraCharacter.lunarCoin < PantheraConfig.ResetSkillsTreeCost) return;
                Image image = this.GetComponent<Image>();
                image.color = PantheraConfig.OverviewButtonsHoveredColor;
                return;
            }

            // Skills Open Skills Tree Button //
            if (this.name == "OpenSkillsTreeButton")
            {
                Image image = this.GetComponent<Image>();
                image.color = PantheraConfig.SkillButtonHoveredColor;
                return;
            }

            // Reset and Key Bind Button //
            if (this.name.Contains("KBButton") || this.name == "DefaultButton")
            {
                Image image = this.GetComponent<Image>();
                image.color = PantheraConfig.KeysBindButtonHoveredColor;
                return;
            }

            // Reset and Key Bind Window Confirm or Cancel Button //
            if (this.name == "KeysBindRemoveButton" || this.name == "KeysBindCancelButton" || this.name == "CancelResetKeysBindButton" || this.name == "ResetResetKeysBindButton")
            {
                Image image = this.GetComponent<Image>();
                image.color = PantheraConfig.KeysBindWindowButtonHoveredColor;
                KeysBinder.InputMapper.Stop();
                return;
            }

            // Skills Tree Ability Button //
            if (this.name.Contains("Ability") == true)
            {
                Image image = this.transform.Find("FrameBackground").GetComponent<Image>();
                image.color = PantheraConfig.SkillsTreeAbilityHoveredColor;
                return;
            }

        }

        public void OnPointerExit(PointerEventData eventData)
        {

            // Scale Windows Button //
            if (this.name == "ScaleWindowButton")
            {
                // Change the color //
                Image image = this.GetComponent<Image>();
                image.color = this.defaultColor;
                return;
            }

            // Exit Button //
            if (this.name == "ExitButton" || this.name == "SkillsTreeWindowExitButton")
            {
                // Change the color //
                Image image = this.GetComponent<Image>();
                image.color = this.defaultColor;
                return;
            }

            // Tab Buttons //
            if (this.name == "Overview" || this.name == "Skills" || this.name == "Combos" || this.name == "KeysBind")
            {
                // Change the color //
                Image image = this.GetComponent<Image>();
                image.color = this.defaultColor;
                return;
            }

            // Overview Stats Buttons //
            if (this.name == "EnduranceButton" || this.name == "ForceButton" || this.name == "AgilityButton" || this.name == "SwiftnessButton" || this.name == "DexterityButton" || this.name == "SpiritButton")
            {
                if (Panthera.PantheraCharacter.attributePointsLeft > 0)
                {
                    if (Panthera.PantheraCharacter.attributePointsLeft > 0)
                    {
                        Image image = this.GetComponent<Image>();
                        image.color = this.defaultColor;
                    }
                }
                return;
            }

            // Overview Reset Attributes Button //
            if (this.name == "OverviewResetAttributesButton" || this.name == "CancelResetAttributesButton" || this.name == "ResetAttributesButton")
            {
                if (this.name == "ResetAttributesButton" && Panthera.PantheraCharacter.lunarCoin < PantheraConfig.ResetAttributesCost) return;
                Image image = this.GetComponent<Image>();
                image.color = this.defaultColor;
                return;
            }

            // Overview Reset Skills Tree Button //
            if (this.name == "OverviewResetSkillsTreeButton" || this.name == "CancelResetSkillsTreeButton" || this.name == "ResetSkillsTreeButton")
            {
                if (this.name == "ResetSkillsTreeButton" && Panthera.PantheraCharacter.lunarCoin < PantheraConfig.ResetSkillsTreeCost) return;
                Image image = this.GetComponent<Image>();
                image.color = this.defaultColor;
                return;
            }

            // Skills Open Skills Tree Button //
            if (this.name == "OpenSkillsTreeButton")
            {
                Image image = this.GetComponent<Image>();
                image.color = this.defaultColor;
                return;
            }

            // Reset and Key Bind Button //
            if (this.name.Contains("KBButton") || this.name == "DefaultButton")
            {
                Image image = this.GetComponent<Image>();
                image.color = this.defaultColor;
                return;
            }

            // Reset and Key Bind Window Remove or Cancel Button //
            if (this.name == "KeysBindRemoveButton" || this.name == "KeysBindCancelButton" || this.name == "CancelResetKeysBindButton" || this.name == "ResetResetKeysBindButton")
            {
                Image image = this.GetComponent<Image>();
                image.color = this.defaultColor;
                KeysBinder.InputMapper.Start(KeysBinder.CurrentContext);
                return;
            }

            // Skills Tree Ability Button //
            if (this.name.Contains("Ability") == true)
            {
                Image image = this.transform.Find("FrameBackground").GetComponent<Image>();
                image.color = this.defaultColor;
                return;
            }

        }

        public void Start()
        {
            // Set the Default Color //
            Image image = this.GetComponent<Image>();
            if (image != null)
                this.defaultColor = image.color;
        }

        public void OnDisable()
        {
            // Reset the Buttons to the Default Color if the Panthera Panel is hiden //
            Image image = this.GetComponent<Image>();
            if (image != null)
                image.color = this.defaultColor;
        }
    }
}
