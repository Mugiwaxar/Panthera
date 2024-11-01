using Panthera.Base;
using Panthera.GUI.Tooltips;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Panthera.GUI.Tabs
{
    public class SkillsTab
    {

        public PantheraPanel pantheraPanel;
        public GameObject tabObj;
        public SkillsTreeController skillTreeController;

        public TextMeshProUGUI skill1Text;
        public TextMeshProUGUI skill2Text;
        public TextMeshProUGUI skill3Text;
        public TextMeshProUGUI skill4Text;
        public TextMeshProUGUI ability1Text;
        public TextMeshProUGUI ability2Text;
        public TextMeshProUGUI ability3Text;
        public TextMeshProUGUI ability4Text;
        public TextMeshProUGUI spell1Text;
        public TextMeshProUGUI spell2Text;
        public TextMeshProUGUI spell3Text;
        public TextMeshProUGUI spell4Text;
        public TextMeshProUGUI spell5Text;
        public TextMeshProUGUI spell6Text;
        public TextMeshProUGUI spell7Text;
        public TextMeshProUGUI spell8Text;

        public Image skill1Icon;
        public Image skill2Icon;
        public Image skill3Icon;
        public Image skill4Icon;
        public Image ability1Icon;
        public Image ability2Icon;
        public Image ability3Icon;
        public Image ability4Icon;
        public Image spell1Icon;
        public Image spell2Icon;
        public Image spell3Icon;
        public Image spell4Icon;
        public Image spell5Icon;
        public Image spell6Icon;
        public Image spell7Icon;
        public Image spell8Icon;

        public SkillsTab(PantheraPanel pantheraPanel)
        {

            // Set the Panthera Panel //
            this.pantheraPanel = pantheraPanel;

            // Find the Skills Tab Game Object //
            this.tabObj = pantheraPanel.pantheraPanelGUI.transform.Find("MainPanel/TabContents/TabContentSkills").gameObject;

            // Create the Skills Tree Controller //
            this.skillTreeController = new SkillsTreeController(this.pantheraPanel);

            // Find all Texts //
            this.skill1Text = this.tabObj.transform.Find("PrimarySkillsLayer").Find("Skill1Layer").Find("Skill1Text").GetComponent<TextMeshProUGUI>();
            this.skill2Text = this.tabObj.transform.Find("PrimarySkillsLayer").Find("Skill2Layer").Find("Skill2Text").GetComponent<TextMeshProUGUI>();
            this.skill3Text = this.tabObj.transform.Find("PrimarySkillsLayer").Find("Skill3Layer").Find("Skill3Text").GetComponent<TextMeshProUGUI>();
            this.skill4Text = this.tabObj.transform.Find("PrimarySkillsLayer").Find("Skill4Layer").Find("Skill4Text").GetComponent<TextMeshProUGUI>();
            this.ability1Text = this.tabObj.transform.Find("AbilitiesLayer").Find("TextLayer").Find("Ability1Layer").Find("Ability1Text").GetComponent<TextMeshProUGUI>();
            this.ability2Text = this.tabObj.transform.Find("AbilitiesLayer").Find("TextLayer").Find("Ability2Layer").Find("Ability2Text").GetComponent<TextMeshProUGUI>();
            this.ability3Text = this.tabObj.transform.Find("AbilitiesLayer").Find("TextLayer").Find("Ability3Layer").Find("Ability3Text").GetComponent<TextMeshProUGUI>();
            this.ability4Text = this.tabObj.transform.Find("AbilitiesLayer").Find("TextLayer").Find("Ability4Layer").Find("Ability4Text").GetComponent<TextMeshProUGUI>();
            this.spell1Text = this.tabObj.transform.Find("SpellsLayer").Find("Spell1Layer").Find("Spell1Text").GetComponent<TextMeshProUGUI>();
            this.spell2Text = this.tabObj.transform.Find("SpellsLayer").Find("Spell2Layer").Find("Spell2Text").GetComponent<TextMeshProUGUI>();
            this.spell3Text = this.tabObj.transform.Find("SpellsLayer").Find("Spell3Layer").Find("Spell3Text").GetComponent<TextMeshProUGUI>();
            this.spell4Text = this.tabObj.transform.Find("SpellsLayer").Find("Spell4Layer").Find("Spell4Text").GetComponent<TextMeshProUGUI>();
            this.spell5Text = this.tabObj.transform.Find("SpellsLayer").Find("Spell5Layer").Find("Spell5Text").GetComponent<TextMeshProUGUI>();
            this.spell6Text = this.tabObj.transform.Find("SpellsLayer").Find("Spell6Layer").Find("Spell6Text").GetComponent<TextMeshProUGUI>();
            this.spell7Text = this.tabObj.transform.Find("SpellsLayer").Find("Spell7Layer").Find("Spell7Text").GetComponent<TextMeshProUGUI>();
            this.spell8Text = this.tabObj.transform.Find("SpellsLayer").Find("Spell8Layer").Find("Spell8Text").GetComponent<TextMeshProUGUI>();

            // Find all Images //
            this.skill1Icon = this.tabObj.transform.Find("PrimarySkillsLayer").Find("Skill1Layer").Find("Skill1Image").GetComponent<Image>();
            this.skill2Icon = this.tabObj.transform.Find("PrimarySkillsLayer").Find("Skill2Layer").Find("Skill2Image").GetComponent<Image>();
            this.skill3Icon = this.tabObj.transform.Find("PrimarySkillsLayer").Find("Skill3Layer").Find("Skill3Image").GetComponent<Image>();
            this.skill4Icon = this.tabObj.transform.Find("PrimarySkillsLayer").Find("Skill4Layer").Find("Skill4Image").GetComponent<Image>();
            this.ability1Icon = this.tabObj.transform.Find("AbilitiesLayer").Find("ArrowsLayer").Find("Ability1Image").GetComponent<Image>();
            this.ability2Icon = this.tabObj.transform.Find("AbilitiesLayer").Find("ArrowsLayer").Find("Ability2Image").GetComponent<Image>();
            this.ability3Icon = this.tabObj.transform.Find("AbilitiesLayer").Find("ArrowsLayer").Find("Ability3Image").GetComponent<Image>();
            this.ability4Icon = this.tabObj.transform.Find("AbilitiesLayer").Find("ArrowsLayer").Find("Ability4Image").GetComponent<Image>();
            this.spell1Icon = this.tabObj.transform.Find("SpellsLayer").Find("Spell1Layer").Find("Spell1Image").GetComponent<Image>();
            this.spell2Icon = this.tabObj.transform.Find("SpellsLayer").Find("Spell2Layer").Find("Spell2Image").GetComponent<Image>();
            this.spell3Icon = this.tabObj.transform.Find("SpellsLayer").Find("Spell3Layer").Find("Spell3Image").GetComponent<Image>();
            this.spell4Icon = this.tabObj.transform.Find("SpellsLayer").Find("Spell4Layer").Find("Spell4Image").GetComponent<Image>();
            this.spell5Icon = this.tabObj.transform.Find("SpellsLayer").Find("Spell5Layer").Find("Spell5Image").GetComponent<Image>();
            this.spell6Icon = this.tabObj.transform.Find("SpellsLayer").Find("Spell6Layer").Find("Spell6Image").GetComponent<Image>();
            this.spell7Icon = this.tabObj.transform.Find("SpellsLayer").Find("Spell7Layer").Find("Spell7Image").GetComponent<Image>();
            this.spell8Icon = this.tabObj.transform.Find("SpellsLayer").Find("Spell8Layer").Find("Spell8Image").GetComponent<Image>();

            // Add the Tooltip to all Icons //
            this.skill1Icon.gameObject.AddComponent<SkillsTooltipComponent>().associatedScript = Panthera.PantheraCharacter.characterSkills.getSkillByID(PantheraConfig.Rip_SkillID);
            this.skill2Icon.gameObject.AddComponent<SkillsTooltipComponent>().associatedScript = Panthera.PantheraCharacter.characterSkills.getSkillByID(PantheraConfig.Slash_SkillID);
            this.skill3Icon.gameObject.AddComponent<SkillsTooltipComponent>().associatedScript = Panthera.PantheraCharacter.characterSkills.getSkillByID(PantheraConfig.Leap_SkillID);
            this.skill4Icon.gameObject.AddComponent<SkillsTooltipComponent>().associatedScript = Panthera.PantheraCharacter.characterSkills.getSkillByID(PantheraConfig.MightyRoar_SkillID);
            this.ability1Icon.gameObject.AddComponent<SkillsTooltipComponent>().associatedScript = Panthera.PantheraCharacter.characterSkills.getSkillByID(PantheraConfig.Prowl_SkillID);
            this.ability2Icon.gameObject.AddComponent<SkillsTooltipComponent>().associatedScript = Panthera.PantheraCharacter.characterSkills.getSkillByID(PantheraConfig.Fury_SkillID);
            this.ability3Icon.gameObject.AddComponent<SkillsTooltipComponent>().associatedScript = Panthera.PantheraCharacter.characterSkills.getSkillByID(PantheraConfig.Detection_SkillID);
            this.ability4Icon.gameObject.AddComponent<SkillsTooltipComponent>().associatedScript = Panthera.PantheraCharacter.characterSkills.getSkillByID(PantheraConfig.Guardian_SkillID);
            this.spell1Icon.gameObject.AddComponent<SkillsTooltipComponent>();
            this.spell2Icon.gameObject.AddComponent<SkillsTooltipComponent>();
            this.spell3Icon.gameObject.AddComponent<SkillsTooltipComponent>().associatedScript = Panthera.PantheraCharacter.characterSkills.getSkillByID(PantheraConfig.Ambition_SkillID);
            this.spell4Icon.gameObject.AddComponent<SkillsTooltipComponent>();
            this.spell5Icon.gameObject.AddComponent<SkillsTooltipComponent>();
            this.spell6Icon.gameObject.AddComponent<SkillsTooltipComponent>();
            this.spell7Icon.gameObject.AddComponent<SkillsTooltipComponent>();
            this.spell8Icon.gameObject.AddComponent<SkillsTooltipComponent>().associatedScript = Panthera.PantheraCharacter.characterSkills.getSkillByID(PantheraConfig.PortalSurge_SkillID); ;

            // Change all Texts //
            this.skill1Text.text = PantheraTokens.Get("skill_RipName");
            this.skill2Text.text = PantheraTokens.Get("skill_SlashName");
            this.skill3Text.text = PantheraTokens.Get("skill_LeapName");
            this.skill4Text.text = PantheraTokens.Get("skill_MightyRoarName");
            this.ability1Text.text = PantheraTokens.Get("ability_ProwlName");
            this.ability2Text.text = PantheraTokens.Get("ability_FuryName");
            this.ability3Text.text = PantheraTokens.Get("ability_DetectionName");
            this.ability4Text.text = PantheraTokens.Get("ability_GuardianName");
            this.spell3Text.text = PantheraTokens.Get("ability_AmbitionName");
            this.spell8Text.text = PantheraTokens.Get("ability_PortalSurgeName");

        }

        public void update()
        {
            // Update the Skills Tree Controller //
            if (this.skillTreeController.skillsTreeWindow.active == true)
                this.skillTreeController.update();
            // Update all Skills Icons //
            this.updateSkillsIcons();
        }

        public void updateSkillsIcons()
        {

            // Prowl Skill //
            if (Panthera.ProfileComponent.IsSkillUnlocked(PantheraConfig.Prowl_SkillID) == false)
                this.ability1Icon.color = PantheraConfig.SkillsLockedSkillColor;
            else
                this.ability1Icon.color = PantheraConfig.SkillsNormalSkillColor;

            // Fury Skill //
            if (Panthera.ProfileComponent.IsSkillUnlocked(PantheraConfig.Fury_SkillID) == false)
                this.ability2Icon.color = PantheraConfig.SkillsLockedSkillColor;
            else
                this.ability2Icon.color = PantheraConfig.SkillsNormalSkillColor;

            // Detection Skill //
            if (Panthera.ProfileComponent.IsSkillUnlocked(PantheraConfig.Detection_SkillID) == false)
                this.ability3Icon.color = PantheraConfig.SkillsLockedSkillColor;
            else
                this.ability3Icon.color = PantheraConfig.SkillsNormalSkillColor;

            // Guardian Skill //
            if (Panthera.ProfileComponent.IsSkillUnlocked(PantheraConfig.Guardian_SkillID) == false)
                this.ability4Icon.color = PantheraConfig.SkillsLockedSkillColor;
            else
                this.ability4Icon.color = PantheraConfig.SkillsNormalSkillColor;

            this.spell1Icon.color = PantheraConfig.SkillsLockedSkillColor;
            this.spell2Icon.color = PantheraConfig.SkillsLockedSkillColor;

            // Ambition Skill //
            if (Panthera.ProfileComponent.IsSkillUnlocked(PantheraConfig.Ambition_SkillID) == false)
                this.spell3Icon.color = PantheraConfig.SkillsLockedSkillColor;
            else
                this.spell3Icon.color = PantheraConfig.SkillsNormalSkillColor;

            this.spell4Icon.color = PantheraConfig.SkillsLockedSkillColor;
            this.spell5Icon.color = PantheraConfig.SkillsLockedSkillColor;
            this.spell6Icon.color = PantheraConfig.SkillsLockedSkillColor;
            this.spell7Icon.color = PantheraConfig.SkillsLockedSkillColor;

            // Portal Surge Skill //
            if (Panthera.ProfileComponent.IsSkillUnlocked(PantheraConfig.PortalSurge_SkillID) == false)
                this.spell8Icon.color = PantheraConfig.SkillsLockedSkillColor;
            else
                this.spell8Icon.color = PantheraConfig.SkillsNormalSkillColor;

        }

        public void enable()
        {
            this.tabObj.SetActive(true);
        }

        public void disable()
        {
            this.tabObj.SetActive(false);
        }

    }
}
