using Panthera.Abilities;
using Panthera.Base;
using Panthera.GUI.Tooltips;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Panthera.GUI.Tabs
{
    public class SkillsTreeController
    {

        PantheraPanel pantheraPanel;
        public GameObject skillsTreeWindow;
        public RectTransform viewport;
        public RectTransform skillsTreeContent;

        Transform special1;
        Transform special2;
        Transform special3;

        public SkillsTreeController (PantheraPanel pantheraPanel)
        {

            // Set the Panthera Panel //
            this.pantheraPanel = pantheraPanel;

            // Create the Skills Tree Window //
            this.skillsTreeWindow = UnityEngine.Object.Instantiate<GameObject>(PantheraAssets.SkillTreeWindowPrefab, this.pantheraPanel.pantheraCanvas.transform, false);
            this.skillsTreeWindow.SetActive(false);
            this.viewport = (RectTransform)this.skillsTreeWindow.transform.Find("Content").Find("ScrollView").Find("Viewport");
            this.skillsTreeContent = (RectTransform)this.viewport.Find("SkillsTree");

            // Add the Button Watcher Component to the Exit Button //
            ButtonWatcher comp = this.skillsTreeWindow.transform.Find("Header").Find("SkillsTreeWindowExitButton").gameObject.AddComponent<ButtonWatcher>();
            comp.pantheraPanel = this.pantheraPanel;

            // Register all Ability Buttons //
            Transform abilities = this.skillsTreeContent.Find("Abilities");
            foreach (Transform transform in abilities)
            {
                string abilityIDString = Regex.Replace(transform.gameObject.name, @"[^\d]", "");
                int abilityID = Int32.Parse(abilityIDString);
                PantheraAbility ability = Panthera.PantheraCharacter.characterAbilities.getAbilityByID(abilityID);
                ability.associatedGUIObj = transform.gameObject;
                transform.gameObject.AddComponent<AbilitiesTooltipComponent>().associatedAbility = ability;
                ButtonWatcher buttonWatcher = transform.gameObject.AddComponent<ButtonWatcher>();
                buttonWatcher.pantheraPanel = this.pantheraPanel;
                buttonWatcher.associatedAbility = ability;
            }

            // Add all Buffs //
            Transform buffLayer = this.skillsTreeWindow.transform.Find("Content").Find("BuffsDebuffsLayer");
            foreach (PantheraBuff buff in Buff.pantheraBuffList)
            {
                GameObject newIcon = new GameObject(buff.displayName);
                newIcon.transform.SetParent(buffLayer);
                newIcon.transform.localScale = new Vector3(1, 1, 1);
                newIcon.AddComponent<Image>().sprite = buff.iconSprite;
                newIcon.AddComponent<BuffsTooltipComponent>().associatedBuff = buff;
            }

            // Register all Specials //
            this.special1 = this.skillsTreeWindow.transform.Find("Content").Find("SpecialsFrame").Find("UntamedSpiritSpecial");
            this.special2 = this.skillsTreeWindow.transform.Find("Content").Find("SpecialsFrame").Find("GodPowerSpecial");
            this.special3 = this.skillsTreeWindow.transform.Find("Content").Find("SpecialsFrame").Find("PortalSurgeSpecial");
            this.special1.gameObject.AddComponent<AbilitiesTooltipComponent>().associatedAbility = Panthera.PantheraCharacter.characterAbilities.getAbilityByID(PantheraConfig.UntamedSpirit_AbilityID);
            this.special2.gameObject.AddComponent<AbilitiesTooltipComponent>().associatedAbility = Panthera.PantheraCharacter.characterAbilities.getAbilityByID(PantheraConfig.GodPower_AbilityID);
            this.special3.gameObject.AddComponent<AbilitiesTooltipComponent>().associatedAbility = Panthera.PantheraCharacter.characterAbilities.getAbilityByID(PantheraConfig.PortalSurge_AbilityID);

            // Add the Zoom Component //
            this.skillsTreeWindow.transform.Find("Content").Find("ScrollView").Find("Viewport").gameObject.AddComponent<SkillsTreeZoomComponent>().skillsTreeController = this;

            // Add help Tooltips //
            this.skillsTreeWindow.transform.Find("Content").Find("MasteryHelp").gameObject.AddComponent<SimpleTooltipComponent>();
            this.skillsTreeContent.Find("Help").Find("RuseHelp").gameObject.AddComponent<SimpleTooltipComponent>();
            this.skillsTreeContent.Find("Help").Find("FuryHelp").gameObject.AddComponent<SimpleTooltipComponent>();
            this.skillsTreeContent.Find("Help").Find("ClassicHelp").gameObject.AddComponent<SimpleTooltipComponent>();
            this.skillsTreeContent.Find("Help").Find("GuardianHelp").gameObject.AddComponent<SimpleTooltipComponent>();

        }

        public void update()
        {

            // Update the Points Amount //
            this.skillsTreeWindow.transform.Find("Content").Find("SkillTreeAmountLimit").Find("AvailableBackground").Find("AvailableAmount").GetComponent<TextMeshProUGUI>().text = Panthera.PantheraCharacter.skillPointsLeft.ToString();
            this.skillsTreeWindow.transform.Find("Content").Find("SkillTreeAmountLimit").Find("SpentBackground").Find("SpentAmount").GetComponent<TextMeshProUGUI>().text = Panthera.PantheraCharacter.usedSkillPoints.ToString();
            this.skillsTreeWindow.transform.Find("Content").Find("SkillTreeAmountLimit").Find("MasteryBackground").Find("AvailableAmount").GetComponent<TextMeshProUGUI>().text = Panthera.ProfileComponent.getMasteryPointsLeft().ToString();

            // Update all Ability Icons //
            foreach (PantheraAbility ability in Panthera.PantheraCharacter.characterAbilities.AbilityList.Values)
            {

                // Check the Ability type //
                if (ability.type == PantheraAbility.AbilityType.special)
                    continue;

                // Update the Amount //
                if (ability.maxLevel > 0)
                {
                    ability.associatedGUIObj.transform.Find("Amount").gameObject.active = true;
                    TextMeshProUGUI pointsAmountText = ability.associatedGUIObj.transform.Find("Amount").GetComponent<TextMeshProUGUI>();
                    pointsAmountText.text = String.Format("{0}/{1}", Panthera.ProfileComponent.GetAbilityLevel(ability.abilityID), ability.maxLevel);
                }
                else
                {
                    ability.associatedGUIObj.transform.Find("Amount").gameObject.active = false;
                }

                // Update the Icon Color //
                Image abilityImage = ability.associatedGUIObj.transform.Find("Icon").GetComponent<Image>();
                if (Panthera.ProfileComponent.GetAbilityLevel(ability.abilityID) > 0 || ability.abilityID == PantheraConfig.FelineSkills_AbilityID)
                    abilityImage.color = PantheraConfig.SkillsTreeUnlockedAbilityColor;
                else
                    abilityImage.color = PantheraConfig.SkillsTreeLockedAbilityColor;

                // Update the Frame Color //
                Image abilityFrame = ability.associatedGUIObj.transform.Find("FrameBackground").GetComponent<Image>();
                if (ability.hasMastery == true && Panthera.ProfileComponent.isMastery(ability.abilityID) == true)
                    abilityFrame.color = PantheraConfig.SkillsMasteryFrameColor;
                else
                    abilityFrame.color = PantheraConfig.SkillsNormalFrameColor;

            }

            // Update Specials 1 //
            if (Panthera.ProfileComponent.GetAbilityLevel(PantheraConfig.UntamedSpirit_AbilityID) > 0)
            {
                this.special1.Find("Icon").GetComponent<Image>().color = PantheraConfig.SkillsTreeUnlockedAbilityColor;
            }
            else
            {
                this.special1.Find("Icon").GetComponent<Image>().color = PantheraConfig.SkillsTreeLockedAbilityColor;
            }

            // Update Specials 2 //
            if (Panthera.ProfileComponent.GetAbilityLevel(PantheraConfig.GodPower_AbilityID) > 0)
            {
                this.special2.Find("Icon").GetComponent<Image>().color = PantheraConfig.SkillsTreeUnlockedAbilityColor;
                this.skillsTreeWindow.transform.Find("Content").Find("SpecialsFrame").Find("ProgressLineSpecial2").GetComponent<Image>().color = PantheraConfig.SkillsTreeLineUnlockedColor;
            }
            else
            {
                this.special2.Find("Icon").GetComponent<Image>().color = PantheraConfig.SkillsTreeLockedAbilityColor;
                this.skillsTreeWindow.transform.Find("Content").Find("SpecialsFrame").Find("ProgressLineSpecial2").GetComponent<Image>().color = PantheraConfig.SkillsTreeLinelockedColor;
            }

            // Update Specials 3 //
            if (Panthera.ProfileComponent.GetAbilityLevel(PantheraConfig.PortalSurge_AbilityID) > 0)
            {
                this.special3.Find("Icon").GetComponent<Image>().color = PantheraConfig.SkillsTreeUnlockedAbilityColor;
                this.skillsTreeWindow.transform.Find("Content").Find("SpecialsFrame").Find("ProgressLineSpecial3").GetComponent<Image>().color = PantheraConfig.SkillsTreeLineUnlockedColor;
            }
            else
            {
                this.special3.Find("Icon").GetComponent<Image>().color = PantheraConfig.SkillsTreeLockedAbilityColor;
                this.skillsTreeWindow.transform.Find("Content").Find("SpecialsFrame").Find("ProgressLineSpecial3").GetComponent<Image>().color = PantheraConfig.SkillsTreeLinelockedColor;
            }

            // Update the Progress Bars //
            this.updateLines();

        }

        public void updateLines()
        {

            // Get all Lines //
            foreach (Transform line in this.skillsTreeContent.Find("Lines"))
            {

                // Check the Name //
                if (line.name.Contains("ProgressLine") == false) continue;

                // Get the ID //
                int ID = int.Parse(line.name.Replace("ProgressLine", ""));

                // Get the Ability //
                PantheraAbility ability = Panthera.PantheraCharacter.characterAbilities.getAbilityByID(ID);

                // Get the Required Ability //
                PantheraAbility requiredAbility = Panthera.PantheraCharacter.characterAbilities.getAbilityByID(ability.requiredAbility);

                // Change the Color of the Line //
                if (Panthera.ProfileComponent.GetAbilityLevel(requiredAbility.abilityID) >= requiredAbility.maxLevel)
                    line.GetComponent<Image>().color = PantheraConfig.SkillsTreeLineUnlockedColor;
                else
                    line.GetComponent<Image>().color = PantheraConfig.SkillsTreeLinelockedColor;

            }

        }

    }
}
