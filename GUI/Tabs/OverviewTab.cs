using Panthera.Base;
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
        public GameObject resetCharacterWindow;

        public TextMeshProUGUI lunarCoinText;
        public Image pantheraImage;
        public TextMeshProUGUI levelText;
        public TextMeshProUGUI attributePointsText;
        public Image XPBar;
        public TextMeshProUGUI XPBarText;
        public Slider slider;
        public TextMeshProUGUI sliderLevelText;
        public GameObject resetButton;

        public TextMeshProUGUI enduranceText;
        public TextMeshProUGUI forceText;
        public TextMeshProUGUI agilityText;
        public TextMeshProUGUI swiftnessText;
        public TextMeshProUGUI dexterityText;

        public TextMeshProUGUI healthText;
        public TextMeshProUGUI healthRegenText;
        public TextMeshProUGUI moveSpeedText;
        public TextMeshProUGUI damageText;
        public TextMeshProUGUI attackSpeedText;
        public TextMeshProUGUI criticText;
        public TextMeshProUGUI defenseText;

        public GameObject enduranceButton;
        public GameObject forceButton;
        public GameObject agilityButton;
        public GameObject swiftnessButton;
        public GameObject dexterityButton;
        public GameObject enduranceButtonImage;
        public GameObject forceButtonImage;
        public GameObject agilityButtonImage;
        public GameObject swiftnessButtonImage;
        public GameObject dexterityButtonImage;
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
            this.resetButton = this.tabObj.transform.Find("OverviewResetButton").gameObject;

            // Find all Stat Texts //
            Transform statsPanel = this.tabObj.transform.Find("Stats");

            this.enduranceText = statsPanel.Find("EnduranceLayout").Find("EnduranceValue").GetComponent<TextMeshProUGUI>();
            this.forceText = statsPanel.Find("ForceLayout").Find("ForceValue").GetComponent<TextMeshProUGUI>();
            this.agilityText = statsPanel.Find("AgilityLayout").Find("AgilityValue").GetComponent<TextMeshProUGUI>();
            this.swiftnessText = statsPanel.Find("SwiftnessLayout").Find("SwiftnessValue").GetComponent<TextMeshProUGUI>();
            this.dexterityText = statsPanel.Find("DexterityLayout").Find("DexterityValue").GetComponent<TextMeshProUGUI>();

            this.healthText = statsPanel.Find("HealthLayout").Find("HealthValue").GetComponent<TextMeshProUGUI>();
            this.healthRegenText = statsPanel.Find("HealthRegenLayout").Find("HealthRegenValue").GetComponent<TextMeshProUGUI>();
            this.moveSpeedText = statsPanel.Find("MoveSpeedLayout").Find("MoveSpeedValue").GetComponent<TextMeshProUGUI>();
            this.damageText = statsPanel.Find("DamageLayout").Find("DamageValue").GetComponent<TextMeshProUGUI>();
            this.attackSpeedText = statsPanel.Find("AttackSpeedLayout").Find("AttackSpeedValue").GetComponent<TextMeshProUGUI>();
            this.criticText = statsPanel.Find("CriticLayout").Find("CriticValue").GetComponent<TextMeshProUGUI>();
            this.defenseText = statsPanel.Find("DefenseLayout").Find("DefenseValue").GetComponent<TextMeshProUGUI>();

            // Add the Tooltop Component to all Stat Text //
            statsPanel.Find("EnduranceLayout").Find("EnduranceText").gameObject.AddComponent<SimpleTooltipComponent>();
            statsPanel.Find("ForceLayout").Find("ForceText").gameObject.AddComponent<SimpleTooltipComponent>();
            statsPanel.Find("AgilityLayout").Find("AgilityText").gameObject.AddComponent<SimpleTooltipComponent>();
            statsPanel.Find("SwiftnessLayout").Find("SwiftnessText").gameObject.AddComponent<SimpleTooltipComponent>();
            statsPanel.Find("DexterityLayout").Find("DexterityText").gameObject.AddComponent<SimpleTooltipComponent>();

            statsPanel.Find("HealthLayout").Find("HealthText").gameObject.AddComponent<SimpleTooltipComponent>();
            statsPanel.Find("HealthRegenLayout").Find("HealthRegenText").gameObject.AddComponent<SimpleTooltipComponent>();
            statsPanel.Find("MoveSpeedLayout").Find("MoveSpeedText").gameObject.AddComponent<SimpleTooltipComponent>();
            statsPanel.Find("DamageLayout").Find("DamageText").gameObject.AddComponent<SimpleTooltipComponent>();
            statsPanel.Find("AttackSpeedLayout").Find("AttackSpeedText").gameObject.AddComponent<SimpleTooltipComponent>();
            statsPanel.Find("CriticLayout").Find("CriticText").gameObject.AddComponent<SimpleTooltipComponent>();
            statsPanel.Find("DefenseLayout").Find("DefenseText").gameObject.AddComponent<SimpleTooltipComponent>();

            // Find all Stat Buttons //
            this.enduranceButton = statsPanel.Find("EnduranceLayout").Find("EnduranceButton").gameObject;
            this.enduranceButtonImage = this.enduranceButton.transform.Find("EnduranceButtonImage").gameObject;
            this.forceButton = statsPanel.Find("ForceLayout").Find("ForceButton").gameObject;
            this.forceButtonImage = this.forceButton.transform.Find("ForceButtonImage").gameObject;
            this.agilityButton = statsPanel.Find("AgilityLayout").Find("AgilityButton").gameObject;
            this.agilityButtonImage = this.agilityButton.transform.Find("AgilityButtonImage").gameObject;
            this.swiftnessButton = statsPanel.Find("SwiftnessLayout").Find("SwiftnessButton").gameObject;
            this.swiftnessButtonImage = this.swiftnessButton.transform.Find("SwiftnessButtonImage").gameObject;
            this.dexterityButton = statsPanel.Find("DexterityLayout").Find("DexterityButton").gameObject;
            this.dexterityButtonImage = this.dexterityButton.transform.Find("DexterityButtonImage").gameObject;

            // Add the Tooltip Component to all Stat Buttons //
            this.enduranceButtonImage.AddComponent<SimpleTooltipComponent>();
            this.forceButtonImage.AddComponent<SimpleTooltipComponent>();
            this.agilityButtonImage.AddComponent<SimpleTooltipComponent>();
            this.swiftnessButtonImage.AddComponent<SimpleTooltipComponent>();
            this.dexterityButtonImage.AddComponent<SimpleTooltipComponent>();

            // Add the Tooltip Component to the Lunar Coin //
            this.tabObj.transform.Find("LunarCoinImage").gameObject.AddComponent<SimpleTooltipComponent>();
            this.lunarCoinText.gameObject.AddComponent<SimpleTooltipComponent>();

            // Set the Level Slider to 1 //
            this.slider.value = 1;

            // Add the Tooltip Component to the Slider //
            this.tabObj.transform.Find("SliderLayout").gameObject.AddComponent<SimpleTooltipComponent>();

            // Add the Tooltip Component to the Reset Button //
            this.resetButton.AddComponent<SimpleTooltipComponent>();

            // Create the Reset Preset Window //
            this.resetCharacterWindow = UnityEngine.Object.Instantiate<GameObject>(Assets.ResetCharacterWindowPrefab, pantheraPanel.pantheraCanvas.transform);
            this.resetCharacterWindow.SetActive(false);
            ButtonWatcher buttonWatcher5 = this.resetCharacterWindow.transform.Find("Content").Find("ResetCharButton").gameObject.AddComponent<ButtonWatcher>();
            ButtonWatcher buttonWatcher6 = this.resetCharacterWindow.transform.Find("Content").Find("CancelResetCharButton").gameObject.AddComponent<ButtonWatcher>();
            buttonWatcher5.pantheraPanel = pantheraPanel;
            buttonWatcher6.pantheraPanel = pantheraPanel;

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
                this.pantheraImage.sprite = Base.Assets.OverviewPortrait1;
            else if (skinIndex == 2)
                this.pantheraImage.sprite = Base.Assets.OverviewPortrait2;
            else if (skinIndex == 3)
                this.pantheraImage.sprite = Base.Assets.OverviewPortrait3;
            else if (skinIndex == 4)
                this.pantheraImage.sprite = Base.Assets.OverviewPortrait4;

            // Set the Level //
            this.levelText.SetText(Panthera.PantheraCharacter.characterLevel.ToString());

            // Set Attribute Points left //
            this.attributePointsText.SetText(Panthera.PantheraCharacter.attributePointsLeft.ToString());

            // Set the Experience //
            this.XPBar.fillAmount = (float)Panthera.PantheraCharacter.levelExperience / (float)Panthera.PantheraCharacter.levelMaxExperience;
            this.XPBarText.SetText("{0}/{1}", Panthera.PantheraCharacter.levelExperience, Panthera.PantheraCharacter.levelMaxExperience);

            // Set the Slider Text //
            this.sliderLevelText.SetText(slider.value.ToString());

            // Update all Stats //
            int addedLevel = (int) (slider.value - 1);
            float maxHealth = (PantheraConfig.Default_MaxHealth + (PantheraConfig.Default_MaxHealthLevel * addedLevel)) * Panthera.PantheraCharacter.maxHealthMult;
            float healthRegen = (PantheraConfig.Default_HealthRegen + (PantheraConfig.Default_HealthRegenLevel * addedLevel)) * Panthera.PantheraCharacter.healthRegenMult;
            float moveSpeed = (PantheraConfig.Default_MoveSpeed + (PantheraConfig.Default_MoveSpeedLevel * addedLevel)) * Panthera.PantheraCharacter.moveSpeedMult;
            float damage = (PantheraConfig.Default_Damage + (PantheraConfig.Default_DamageLevel * addedLevel)) * Panthera.PantheraCharacter.damageMult;
            float attackSpeed = (PantheraConfig.Default_AttackSpeed + (PantheraConfig.Default_AttackSpeedLevel * addedLevel)) * Panthera.PantheraCharacter.attackSpeedMult;
            float critic = (PantheraConfig.Default_Critic + (PantheraConfig.Default_CriticLevel * addedLevel)) * Panthera.PantheraCharacter.critMult;
            float defense = (PantheraConfig.Default_Defense + (PantheraConfig.Default_DefenseLevel * addedLevel)) * Panthera.PantheraCharacter.DefenseMult;
            this.enduranceText.SetText((Panthera.PantheraCharacter.endurance).ToString());
            this.forceText.SetText((Panthera.PantheraCharacter.force).ToString());
            this.agilityText.SetText((Panthera.PantheraCharacter.agility).ToString());
            this.swiftnessText.SetText((Panthera.PantheraCharacter.swiftness).ToString());
            this.dexterityText.SetText((Panthera.PantheraCharacter.dexterity).ToString());
            this.healthText.SetText(maxHealth.ToString());
            this.healthRegenText.SetText(healthRegen.ToString());
            this.moveSpeedText.SetText(moveSpeed.ToString());
            this.damageText.SetText(damage.ToString());
            this.attackSpeedText.SetText(attackSpeed.ToString());
            this.criticText.SetText(critic.ToString());
            this.defenseText.SetText(defense.ToString());

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

                    this.enduranceButtonImage.GetComponent<Image>().color = PantheraConfig.OverviewAttributeButtonsEnabledColor;
                    this.forceButtonImage.GetComponent<Image>().color = PantheraConfig.OverviewAttributeButtonsEnabledColor;
                    this.agilityButtonImage.GetComponent<Image>().color = PantheraConfig.OverviewAttributeButtonsEnabledColor;
                    this.swiftnessButtonImage.GetComponent<Image>().color = PantheraConfig.OverviewAttributeButtonsEnabledColor;
                    this.dexterityButtonImage.GetComponent<Image>().color = PantheraConfig.OverviewAttributeButtonsEnabledColor;

                    this.enduranceButton.GetComponent<ButtonWatcher>().defaultColor = PantheraConfig.OverviewAttributeButtonsEnabledColor;
                    this.forceButton.GetComponent<ButtonWatcher>().defaultColor = PantheraConfig.OverviewAttributeButtonsEnabledColor;
                    this.agilityButton.GetComponent<ButtonWatcher>().defaultColor = PantheraConfig.OverviewAttributeButtonsEnabledColor;
                    this.swiftnessButton.GetComponent<ButtonWatcher>().defaultColor = PantheraConfig.OverviewAttributeButtonsEnabledColor;
                    this.dexterityButton.GetComponent<ButtonWatcher>().defaultColor = PantheraConfig.OverviewAttributeButtonsEnabledColor;
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

                    this.enduranceButtonImage.GetComponent<Image>().color = PantheraConfig.OverviewAttributeButtonsDisabledColor;
                    this.forceButtonImage.GetComponent<Image>().color = PantheraConfig.OverviewAttributeButtonsDisabledColor;
                    this.agilityButtonImage.GetComponent<Image>().color = PantheraConfig.OverviewAttributeButtonsDisabledColor;
                    this.swiftnessButtonImage.GetComponent<Image>().color = PantheraConfig.OverviewAttributeButtonsDisabledColor;
                    this.dexterityButtonImage.GetComponent<Image>().color = PantheraConfig.OverviewAttributeButtonsDisabledColor;

                    this.enduranceButton.GetComponent<ButtonWatcher>().defaultColor = PantheraConfig.OverviewAttributeButtonsDisabledColor;
                    this.forceButton.GetComponent<ButtonWatcher>().defaultColor = PantheraConfig.OverviewAttributeButtonsDisabledColor;
                    this.agilityButton.GetComponent<ButtonWatcher>().defaultColor = PantheraConfig.OverviewAttributeButtonsDisabledColor;
                    this.swiftnessButton.GetComponent<ButtonWatcher>().defaultColor = PantheraConfig.OverviewAttributeButtonsDisabledColor;
                    this.dexterityButton.GetComponent<ButtonWatcher>().defaultColor = PantheraConfig.OverviewAttributeButtonsDisabledColor;
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
