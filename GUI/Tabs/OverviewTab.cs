using Panthera.Base;
using Panthera.Components;
using Panthera.GUI.Tooltips;
using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Panthera.GUI.Tabs
{
    public class OverviewTab
    {

        public PantheraPanel pantheraPanel;
        public GameObject tabObj;
        public GameObject resetAttributesWindow;
        public GameObject resetSkillsTreeWindow;

        public TextMeshProUGUI lunarCoinText;
        public Image pantheraImage;
        public TextMeshProUGUI levelText;
        public TextMeshProUGUI attributePointsText;
        public Image XPBar;
        public TextMeshProUGUI XPBarText;
        public Slider slider;
        public TextMeshProUGUI sliderLevelText;
        public GameObject resetAttributesButton;
        public GameObject resetSkillsTreeButton;

        public TextMeshProUGUI enduranceText;
        public TextMeshProUGUI forceText;
        public TextMeshProUGUI agilityText;
        public TextMeshProUGUI swiftnessText;
        public TextMeshProUGUI dexterityText;
        public TextMeshProUGUI spiritText;

        public TextMeshProUGUI healthText;
        public TextMeshProUGUI healthRegenText;
        public TextMeshProUGUI moveSpeedText;
        public TextMeshProUGUI damageText;
        public TextMeshProUGUI attackSpeedText;
        public TextMeshProUGUI criticText;
        public TextMeshProUGUI dodgeText;
        public TextMeshProUGUI defenseText;
        public TextMeshProUGUI masteryText;
        public TextMeshProUGUI furyText;
        public TextMeshProUGUI shieldText;

        public GameObject enduranceButton;
        public GameObject forceButton;
        public GameObject agilityButton;
        public GameObject swiftnessButton;
        public GameObject dexterityButton;
        public GameObject spiritButton;
        public GameObject enduranceButtonImage;
        public GameObject forceButtonImage;
        public GameObject agilityButtonImage;
        public GameObject swiftnessButtonImage;
        public GameObject dexterityButtonImage;
        public GameObject spiritButtonImage;
        public int attributeButtonsStat = 0;

        public OverviewTab(PantheraPanel pantheraPanel)
        {

            // Set the Panthera Panel //
            this.pantheraPanel = pantheraPanel;

            // Find the Overview Tab Game Object //
            this.tabObj = pantheraPanel.pantheraPanelGUI.transform.Find("MainPanel/TabContents/TabContentOverview").gameObject;

            // Find all Game Object //
            this.lunarCoinText = this.tabObj.transform.Find("LunarCoinImage").Find("LunarCoinText").GetComponent<TextMeshProUGUI>();
            this.pantheraImage = this.tabObj.transform.Find("PantheraImage").GetComponent<Image>();
            this.levelText = this.tabObj.transform.Find("LevelLayout").Find("LevelText").GetComponent<TextMeshProUGUI>();
            this.attributePointsText = this.tabObj.transform.Find("PointsLayout").Find("PointsText").GetComponent<TextMeshProUGUI>();
            this.XPBar = this.tabObj.transform.Find("XPBar").Find("Bar").GetComponent<Image>();
            this.XPBarText = this.tabObj.transform.Find("XPBar").Find("Text").GetComponent<TextMeshProUGUI>();
            this.slider = this.tabObj.transform.Find("SliderLayout").Find("Slider").GetComponent<Slider>();
            this.sliderLevelText = this.tabObj.transform.Find("SliderLayout").Find("TextLayout").Find("LevelText").GetComponent<TextMeshProUGUI>();
            this.resetAttributesButton = this.tabObj.transform.Find("OverviewResetAttributesButton").gameObject;
            this.resetSkillsTreeButton = this.tabObj.transform.Find("OverviewResetSkillsTreeButton").gameObject;

            // Find all Stat Texts //
            Transform attributesPanel = this.tabObj.transform.Find("Attributes");
            Transform statsPanel = this.tabObj.transform.Find("Stats");

            this.enduranceText = attributesPanel.Find("EnduranceLayout").Find("EnduranceValue").GetComponent<TextMeshProUGUI>();
            this.forceText = attributesPanel.Find("ForceLayout").Find("ForceValue").GetComponent<TextMeshProUGUI>();
            this.agilityText = attributesPanel.Find("AgilityLayout").Find("AgilityValue").GetComponent<TextMeshProUGUI>();
            this.swiftnessText = attributesPanel.Find("SwiftnessLayout").Find("SwiftnessValue").GetComponent<TextMeshProUGUI>();
            this.dexterityText = attributesPanel.Find("DexterityLayout").Find("DexterityValue").GetComponent<TextMeshProUGUI>();
            this.spiritText = attributesPanel.Find("SpiritLayout").Find("SpiritValue").GetComponent<TextMeshProUGUI>();

            this.healthText = statsPanel.Find("HealthLayout").Find("HealthValue").GetComponent<TextMeshProUGUI>();
            this.healthRegenText = statsPanel.Find("HealthRegenLayout").Find("HealthRegenValue").GetComponent<TextMeshProUGUI>();
            this.moveSpeedText = statsPanel.Find("MoveSpeedLayout").Find("MoveSpeedValue").GetComponent<TextMeshProUGUI>();
            this.damageText = statsPanel.Find("DamageLayout").Find("DamageValue").GetComponent<TextMeshProUGUI>();
            this.attackSpeedText = statsPanel.Find("AttackSpeedLayout").Find("AttackSpeedValue").GetComponent<TextMeshProUGUI>();
            this.criticText = statsPanel.Find("CriticLayout").Find("CriticValue").GetComponent<TextMeshProUGUI>();
            this.dodgeText = statsPanel.Find("DodgeLayout").Find("DodgeValue").GetComponent<TextMeshProUGUI>();
            this.defenseText = statsPanel.Find("DefenseLayout").Find("DefenseValue").GetComponent<TextMeshProUGUI>();
            this.masteryText = statsPanel.Find("MasteryLayout").Find("MasteryValue").GetComponent<TextMeshProUGUI>();
            this.furyText = statsPanel.Find("FuryLayout").Find("FuryValue").GetComponent<TextMeshProUGUI>();
            this.shieldText = statsPanel.Find("ShieldLayout").Find("ShieldValue").GetComponent<TextMeshProUGUI>();

            // Add the Tooltop Component to all Stat Text //
            attributesPanel.Find("EnduranceLayout").Find("EnduranceText").gameObject.AddComponent<SimpleTooltipComponent>();
            attributesPanel.Find("ForceLayout").Find("ForceText").gameObject.AddComponent<SimpleTooltipComponent>();
            attributesPanel.Find("AgilityLayout").Find("AgilityText").gameObject.AddComponent<SimpleTooltipComponent>();
            attributesPanel.Find("SwiftnessLayout").Find("SwiftnessText").gameObject.AddComponent<SimpleTooltipComponent>();
            attributesPanel.Find("DexterityLayout").Find("DexterityText").gameObject.AddComponent<SimpleTooltipComponent>();
            attributesPanel.Find("SpiritLayout").Find("SpiritText").gameObject.AddComponent<SimpleTooltipComponent>();

            // Find all Stat Buttons //
            this.enduranceButton = attributesPanel.Find("EnduranceLayout").Find("EnduranceButton").gameObject;
            this.enduranceButtonImage = this.enduranceButton.transform.Find("EnduranceButtonImage").gameObject;
            this.forceButton = attributesPanel.Find("ForceLayout").Find("ForceButton").gameObject;
            this.forceButtonImage = this.forceButton.transform.Find("ForceButtonImage").gameObject;
            this.agilityButton = attributesPanel.Find("AgilityLayout").Find("AgilityButton").gameObject;
            this.agilityButtonImage = this.agilityButton.transform.Find("AgilityButtonImage").gameObject;
            this.swiftnessButton = attributesPanel.Find("SwiftnessLayout").Find("SwiftnessButton").gameObject;
            this.swiftnessButtonImage = this.swiftnessButton.transform.Find("SwiftnessButtonImage").gameObject;
            this.dexterityButton = attributesPanel.Find("DexterityLayout").Find("DexterityButton").gameObject;
            this.dexterityButtonImage = this.dexterityButton.transform.Find("DexterityButtonImage").gameObject;
            this.spiritButton = attributesPanel.Find("SpiritLayout").Find("SpiritButton").gameObject;
            this.spiritButtonImage = this.spiritButton.transform.Find("SpiritButtonImage").gameObject;

            // Set the Level Slider to 1 //
            this.slider.value = 1;

            // Create the Reset Attributes Window //
            this.resetAttributesWindow = UnityEngine.Object.Instantiate<GameObject>(PantheraAssets.ResetAttributesWindowPrefab, pantheraPanel.pantheraCanvas.transform);
            this.resetAttributesWindow.SetActive(false);
            ButtonWatcher buttonWatcher5 = this.resetAttributesWindow.transform.Find("Content").Find("ResetAttributesButton").gameObject.AddComponent<ButtonWatcher>();
            ButtonWatcher buttonWatcher6 = this.resetAttributesWindow.transform.Find("Content").Find("CancelResetAttributesButton").gameObject.AddComponent<ButtonWatcher>();
            buttonWatcher5.pantheraPanel = pantheraPanel;
            buttonWatcher6.pantheraPanel = pantheraPanel;

            // Create the Reset Skills Tree Window //
            this.resetSkillsTreeWindow = UnityEngine.Object.Instantiate<GameObject>(PantheraAssets.ResetSkillsTreeWindowPrefab, pantheraPanel.pantheraCanvas.transform);
            this.resetSkillsTreeWindow.SetActive(false);
            ButtonWatcher buttonWatcher7 = this.resetSkillsTreeWindow.transform.Find("Content").Find("ResetSkillsTreeButton").gameObject.AddComponent<ButtonWatcher>();
            ButtonWatcher buttonWatcher8 = this.resetSkillsTreeWindow.transform.Find("Content").Find("CancelResetSkillsTreeButton").gameObject.AddComponent<ButtonWatcher>();
            buttonWatcher7.pantheraPanel = pantheraPanel;
            buttonWatcher8.pantheraPanel = pantheraPanel;

        }

        public void update()
        {

            // Update the Lunar Coin //
            this.lunarCoinText.SetText(Panthera.PantheraCharacter.lunarCoin.ToString());

            // Update the Panthera Image //
            int skinIndex = 1;
            if (Panthera.FirstLocalUser != null && Panthera.FirstLocalUser.currentNetworkUser != null)
                skinIndex = Base.Skin.GetActualSkinIndex(Panthera.FirstLocalUser.currentNetworkUser);
            if (skinIndex == 1)
                this.pantheraImage.sprite = Base.PantheraAssets.OverviewPortrait1;
            else if (skinIndex == 2)
                this.pantheraImage.sprite = Base.PantheraAssets.OverviewPortrait2;
            else if (skinIndex == 3)
                this.pantheraImage.sprite = Base.PantheraAssets.OverviewPortrait3;
            else if (skinIndex == 4)
                this.pantheraImage.sprite = Base.PantheraAssets.OverviewPortrait4;

            // Set the Level //
            this.levelText.SetText(Panthera.PantheraCharacter.characterLevel.ToString());

            // Set Attribute Points left //
            this.attributePointsText.SetText(Panthera.PantheraCharacter.attributePointsLeft.ToString());

            // Set the Experience //
            this.XPBar.fillAmount = (float)Panthera.PantheraCharacter.levelExperience / (float)Panthera.PantheraCharacter.levelMaxExperience;
            this.XPBarText.SetText("{0}/{1}", Panthera.PantheraCharacter.levelExperience, Panthera.PantheraCharacter.levelMaxExperience);

            // Set the Slider Text //
            this.sliderLevelText.SetText(slider.value.ToString());

            // Get the Slider Level //
            int addedLevel = (int)(this.slider.value - 1);

            // Get the Profile Component //
            ProfileComponent profile = Panthera.ProfileComponent;
            if (Panthera.PantheraCharacter.pantheraObj != null && Panthera.PantheraCharacter.pantheraObj.profileComponent != null)
            {
                profile = Panthera.PantheraCharacter.pantheraObj.profileComponent;
                // Block the Slider //
                this.slider.enabled = false;
                this.slider.value = Panthera.PantheraCharacter.pantheraObj.characterBody.level;
                addedLevel = (int)(this.slider.value);
            }
            else
            {
                // Unlock the Slider //
                this.slider.enabled = true;
            }

            // Update all Stats
            this.enduranceText.SetText((profile.endurance + 1).ToString());
            this.forceText.SetText((profile.force + 1).ToString());
            this.agilityText.SetText((profile.agility + 1).ToString());
            this.swiftnessText.SetText((profile.swiftness + 1).ToString());
            this.dexterityText.SetText((profile.dexterity + 1).ToString());
            this.spiritText.SetText((profile.spirit + 1).ToString());
            this.healthText.SetText(profile.getMaxHealth(addedLevel).ToString());
            this.healthRegenText.SetText(profile.getHealthRegen(addedLevel).ToString());
            this.moveSpeedText.SetText(profile.getMoveSpeed(addedLevel).ToString());
            this.damageText.SetText(profile.getDamage(addedLevel).ToString());
            this.attackSpeedText.SetText(profile.getAttackSpeed(addedLevel).ToString());
            this.criticText.SetText(profile.getCritic(addedLevel).ToString());
            this.dodgeText.SetText(profile.getDodge(addedLevel).ToString());
            this.defenseText.SetText(profile.getDefence(addedLevel).ToString());
            this.masteryText.SetText(profile.getMastery(addedLevel).ToString());
            this.furyText.SetText(profile.getMaxFury(addedLevel).ToString());
            this.shieldText.SetText(profile.getMaxFrontShield(addedLevel).ToString());

            // Update Attribute Buttons //
            if (Panthera.PantheraCharacter.attributePointsLeft > 0)
            {
                if (this.attributeButtonsStat != 1)
                {
                    this.attributeButtonsStat = 1;
                    this.enduranceButton.GetComponent<Image>().color = PantheraConfig.OverviewAttributeButtonsEnabledColor;
                    this.forceButton.GetComponent<Image>().color = PantheraConfig.OverviewAttributeButtonsEnabledColor;
                    this.agilityButton.GetComponent<Image>().color = PantheraConfig.OverviewAttributeButtonsEnabledColor;
                    this.swiftnessButton.GetComponent<Image>().color = PantheraConfig.OverviewAttributeButtonsEnabledColor;
                    this.dexterityButton.GetComponent<Image>().color = PantheraConfig.OverviewAttributeButtonsEnabledColor;
                    this.spiritButton.GetComponent<Image>().color = PantheraConfig.OverviewAttributeButtonsEnabledColor;

                    this.enduranceButtonImage.GetComponent<Image>().color = PantheraConfig.OverviewAttributeButtonsEnabledColor;
                    this.forceButtonImage.GetComponent<Image>().color = PantheraConfig.OverviewAttributeButtonsEnabledColor;
                    this.agilityButtonImage.GetComponent<Image>().color = PantheraConfig.OverviewAttributeButtonsEnabledColor;
                    this.swiftnessButtonImage.GetComponent<Image>().color = PantheraConfig.OverviewAttributeButtonsEnabledColor;
                    this.dexterityButtonImage.GetComponent<Image>().color = PantheraConfig.OverviewAttributeButtonsEnabledColor;
                    this.spiritButtonImage.GetComponent<Image>().color = PantheraConfig.OverviewAttributeButtonsEnabledColor;

                    this.enduranceButton.GetComponent<ButtonWatcher>().defaultColor = PantheraConfig.OverviewAttributeButtonsEnabledColor;
                    this.forceButton.GetComponent<ButtonWatcher>().defaultColor = PantheraConfig.OverviewAttributeButtonsEnabledColor;
                    this.agilityButton.GetComponent<ButtonWatcher>().defaultColor = PantheraConfig.OverviewAttributeButtonsEnabledColor;
                    this.swiftnessButton.GetComponent<ButtonWatcher>().defaultColor = PantheraConfig.OverviewAttributeButtonsEnabledColor;
                    this.dexterityButton.GetComponent<ButtonWatcher>().defaultColor = PantheraConfig.OverviewAttributeButtonsEnabledColor;
                    this.spiritButton.GetComponent<ButtonWatcher>().defaultColor = PantheraConfig.OverviewAttributeButtonsEnabledColor;
                }
            }
            else
            {
                if (this.attributeButtonsStat != 2)
                {
                    this.attributeButtonsStat = 2;
                    this.enduranceButton.GetComponent<Image>().color = PantheraConfig.OverviewAttributeButtonsDisabledColor;
                    this.forceButton.GetComponent<Image>().color = PantheraConfig.OverviewAttributeButtonsDisabledColor;
                    this.agilityButton.GetComponent<Image>().color = PantheraConfig.OverviewAttributeButtonsDisabledColor;
                    this.swiftnessButton.GetComponent<Image>().color = PantheraConfig.OverviewAttributeButtonsDisabledColor;
                    this.dexterityButton.GetComponent<Image>().color = PantheraConfig.OverviewAttributeButtonsDisabledColor;
                    this.spiritButton.GetComponent<Image>().color = PantheraConfig.OverviewAttributeButtonsDisabledColor;

                    this.enduranceButtonImage.GetComponent<Image>().color = PantheraConfig.OverviewAttributeButtonsDisabledColor;
                    this.forceButtonImage.GetComponent<Image>().color = PantheraConfig.OverviewAttributeButtonsDisabledColor;
                    this.agilityButtonImage.GetComponent<Image>().color = PantheraConfig.OverviewAttributeButtonsDisabledColor;
                    this.swiftnessButtonImage.GetComponent<Image>().color = PantheraConfig.OverviewAttributeButtonsDisabledColor;
                    this.dexterityButtonImage.GetComponent<Image>().color = PantheraConfig.OverviewAttributeButtonsDisabledColor;
                    this.spiritButtonImage.GetComponent<Image>().color = PantheraConfig.OverviewAttributeButtonsDisabledColor;

                    this.enduranceButton.GetComponent<ButtonWatcher>().defaultColor = PantheraConfig.OverviewAttributeButtonsDisabledColor;
                    this.forceButton.GetComponent<ButtonWatcher>().defaultColor = PantheraConfig.OverviewAttributeButtonsDisabledColor;
                    this.agilityButton.GetComponent<ButtonWatcher>().defaultColor = PantheraConfig.OverviewAttributeButtonsDisabledColor;
                    this.swiftnessButton.GetComponent<ButtonWatcher>().defaultColor = PantheraConfig.OverviewAttributeButtonsDisabledColor;
                    this.dexterityButton.GetComponent<ButtonWatcher>().defaultColor = PantheraConfig.OverviewAttributeButtonsDisabledColor;
                    this.spiritButton.GetComponent<ButtonWatcher>().defaultColor = PantheraConfig.OverviewAttributeButtonsDisabledColor;
                }
            }

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
