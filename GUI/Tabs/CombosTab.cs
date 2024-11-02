using Panthera.Base;
using Panthera.Combos;
using Panthera.GUI.Tooltips;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Panthera.GUI.Tabs
{
    public class CombosTab
    {

        public PantheraPanel pantheraPanel;
        public GameObject tabObj;
        public Transform ComboListTransform;

        public CombosTab(PantheraPanel pantheraPanel)
        {

            // Set the Panthera Panel //
            this.pantheraPanel = pantheraPanel;

            // Find the Combo Tab Game Object //
            this.tabObj = pantheraPanel.pantheraPanelGUI.transform.Find("MainPanel/TabContents/TabContentCombos").gameObject;

            // Find the Combos List Transform //
            this.ComboListTransform = this.tabObj.transform.Find("Viewport").Find("Content");

        }

        public void update()
        {

        }

        public void enable()
        {
            this.tabObj.SetActive(true);
            this.updateCombosList();
        }

        public void disable()
        {
            this.tabObj.SetActive(false);
        }
        
        public void updateCombosList()
        {

            // Clear the GUI Combos List //
            foreach (Transform elem in this.ComboListTransform)
            {
                if (elem.name != "Title")
                    GameObject.Destroy(elem.gameObject);
            }

            // Create the List //
            Dictionary<int, PantheraCombo> combosList = new Dictionary<int, PantheraCombo>();

            // Add the Unlocked Combos to the List //
            foreach (KeyValuePair<int, PantheraCombo> combo in Panthera.PantheraCharacter.CharacterCombos.CombosList)
            {
                if (Panthera.ProfileComponent.isComboUnlocked(combo.Value.comboID) == true)
                    combosList.Add(combo.Key, combo.Value);
            }

            // Add the Locked Combos to the List //
            foreach (KeyValuePair<int, PantheraCombo> combo in Panthera.PantheraCharacter.CharacterCombos.CombosList)
            {
                if (Panthera.ProfileComponent.isComboUnlocked(combo.Value.comboID) == false)
                    combosList.Add(combo.Key, combo.Value);
            }

            // Itinerate the Combos List //
            foreach (KeyValuePair<int, PantheraCombo> combo in combosList)
            {

                // Stop if the Combo is not visible //
                if (combo.Value.visible == false) continue;

                // Instantiate the Base Element //
                GameObject comboElem = GameObject.Instantiate<GameObject>(PantheraAssets.ComboBaseTemplate, this.ComboListTransform);

                // Change the Name and the Color //
                TextMeshProUGUI name = comboElem.transform.Find("ComboName").GetComponent<TextMeshProUGUI>();
                name.text = combo.Value.name;
                if (Panthera.ProfileComponent.isComboUnlocked(combo.Value.comboID) == false)
                    name.m_fontColor = PantheraConfig.ComboLockedColor;
                else
                    name.m_fontColor = PantheraConfig.ComboNormalColor;

                // Get the Skills Layout //
                Transform skillsLayout = comboElem.transform.Find("Layout");

                // Add all Skills //
                GameObject lastLine = null;
                foreach (ComboSkill skill in combo.Value.comboSkillsList)
                {
                    // Instantiate the Skill Element //
                    GameObject skillElem = GameObject.Instantiate<GameObject>(PantheraAssets.ComboSkillTemplate, skillsLayout);
                    // Change the Icon //
                    Image skillIcon = skillElem.transform.Find("Image").GetComponent<Image>();
                    skillIcon.sprite = skill.skill.icon;
                    // Change the Icon color //
                    if (Panthera.ProfileComponent.IsSkillUnlocked(skill.skill.skillID) == false)
                        skillIcon.color = PantheraConfig.SkillsLockedSkillColor;
                    else
                        skillIcon.color = PantheraConfig.SkillsNormalSkillColor;
                    // Add the Tooltip //
                    skillIcon.gameObject.AddComponent<SkillsTooltipComponent>().associatedScript = skill.skill;
                    // Change the Elem Size //
                    if (skill.biggerIcon == true)
                        skillElem.GetComponent<RectTransform>().sizeDelta = new Vector2(40, 40);
                    else
                        skillElem.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
                    // Add the Buttons //
                    Transform buttonsLayout = skillElem.transform.Find("Buttons");
                    if (skill.keyA > 0)
                    {
                        GameObject buttonElem = GameObject.Instantiate<GameObject>(PantheraAssets.ComboButtonTemplate, buttonsLayout);
                        buttonElem.GetComponent<Image>().sprite = Utils.Functions.KeyEnumToSprite(skill.keyA);
                    }
                    if (skill.keyB > 0)
                    {
                        GameObject buttonElem = GameObject.Instantiate<GameObject>(PantheraAssets.ComboButtonTemplate, buttonsLayout);
                        buttonElem.GetComponent<Image>().sprite = Utils.Functions.KeyEnumToSprite(skill.keyB);
                    }
                    if (skill.direction > 0)
                    {
                        GameObject buttonElem = GameObject.Instantiate<GameObject>(PantheraAssets.ComboButtonTemplate, buttonsLayout);
                        buttonElem.GetComponent<Image>().sprite = Utils.Functions.KeyEnumToSprite(skill.direction);
                    }
                    // Add the Line //
                    lastLine = GameObject.Instantiate<GameObject>(PantheraAssets.ComboLineTemplate, skillsLayout);
                }

                // Remove the Last useless Line //
                GameObject.DestroyImmediate(lastLine);

            }

        }

    }
}
