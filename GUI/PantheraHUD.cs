using Panthera.Components;
using RoR2;
using RoR2.UI;
using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Panthera.GUI
{
    internal class PantheraHUD : MonoBehaviour
    {

        public static PantheraHUD Instance;
        public static GameObject PantheraHUDPrefab = Assets.MainAssetBundle.LoadAsset<GameObject>("PantheraHUD");

        public HUD origHUD;
        public GameObject newHUD;
        public GameObject origMainContainer;
        public PantheraObj ptraObj;
        public PantheraBody pantheraBody;
        public PantheraHealthComponent healthComponent;
        public PantheraInputBank inputBank;

        public RectTransform buffsDisplay;
        public RectTransform equipmentDisplay;

        public Material healthBarMat;
        public Material barrierbarMat;
        public Material xpBarMat;
        public Material energyBarMat;
        public Material powerBarMat;
        public GameObject furyBarObj;
        public Material furyBarMat;
        public GameObject comboBarObj;
        public Image filledComboBarImg;
        public GameObject shieldBarObj;
        public Image filledShieldBarImg;
        public Image ShildIconRed;
        public Image redShieldBarImg;
        public TextMeshProUGUI healthText;
        public TextMeshProUGUI xpText;
        public TextMeshProUGUI levelText;
        public TextMeshProUGUI energyText;
        public TextMeshProUGUI powerText;
        public TextMeshProUGUI furyText;
        public TextMeshProUGUI shieldText;

        public GameObject actionBar;

        // (SlotID 1 - 10, Slot Object //
        public Dictionary<int, GameObject> HUDSkillSlotsList = new Dictionary<int, GameObject>();

        public static void Init()
        {
            // Create the hook //
            On.RoR2.UI.HUD.Awake += HUDAwake;
        }

        public static void HUDAwake(On.RoR2.UI.HUD.orig_Awake orig, RoR2.UI.HUD self)
        {
            // Call the base function //
            orig(self);
            // Check if Client //
            if (NetworkClient.active == false)
            {
                return;
            }
            // Create the Panthera HUD //
            Instance = self.mainContainer.AddComponent<PantheraHUD>();
            Instance.origHUD = self;
            Instance.origMainContainer = self.mainContainer;
        }

        public void CreateHUD()
        {

            // Check if the selected character is Panthera on Client //
            if (NetworkClient.active == false)
            {
                this.enabled = false;
                return;
            }

            // Remove the unwanted origin HUD things //
            this.origHUD.healthBar.gameObject.SetActive(false);
            this.origHUD.expBar.gameObject.SetActive(false);
            this.origHUD.levelText.gameObject.SetActive(false);
            Transform scaler = this.origMainContainer.transform.GetChild(7)?.GetChild(2)?.GetChild(2)?.GetChild(0).transform;
            this.equipmentDisplay = (RectTransform)scaler.GetChild(2);
            scaler.GetChild(3).gameObject.SetActive(false);
            scaler.GetChild(4).gameObject.SetActive(false);
            scaler.GetChild(5).gameObject.SetActive(false);
            scaler.GetChild(6).gameObject.SetActive(false);
            scaler.GetChild(7).gameObject.SetActive(false);
            scaler.GetChild(8).gameObject.SetActive(false);
            foreach (SkillIcon icon in this.origHUD.skillIcons)
            {
                icon.gameObject.SetActive(false);
            }

            // Up the Notification Area //
            RectTransform notifArea = (RectTransform)this.origMainContainer.transform.FindChild("NotificationArea");
            notifArea.localPosition = new Vector3(notifArea.localPosition.x, notifArea.localPosition.y + 200, notifArea.localPosition.z);

            // Instantiate the new HUD //
            this.newHUD = UnityEngine.Object.Instantiate<GameObject>(PantheraHUDPrefab, this.origMainContainer.transform, false);

            // Find all Bars //
            Transform leftFrame = this.newHUD.transform.Find("LeftUnitFrame");
            Transform rightFrame = this.newHUD.transform.Find("RightUnitFrame");
            this.healthBarMat = leftFrame.Find("Bars")?.Find("HealthBar")?.GetComponent<Image>()?.materialForRendering;
            this.barrierbarMat = leftFrame.Find("Bars")?.Find("BarrierBar")?.GetComponent<Image>()?.materialForRendering;
            this.xpBarMat = leftFrame.Find("Bars")?.Find("XpBar")?.GetComponent<Image>()?.materialForRendering;
            this.energyBarMat = rightFrame.Find("Bars")?.Find("EnergyBar")?.GetComponent<Image>()?.materialForRendering;
            this.powerBarMat = rightFrame.Find("Bars")?.Find("PowerBar")?.GetComponent<Image>()?.materialForRendering;
            this.furyBarObj = rightFrame.Find("FuryBar").gameObject;
            this.furyBarMat = this.furyBarObj?.transform?.Find("Health_ORB")?.GetComponent<Image>()?.materialForRendering;
            this.comboBarObj = this.newHUD.transform.Find("ComboBar")?.gameObject;
            this.filledComboBarImg = this.comboBarObj?.transform?.Find("FilledComboBar")?.GetComponent<Image>();
            this.shieldBarObj = this.newHUD.transform.Find("ShieldBar")?.gameObject;
            this.filledShieldBarImg = this.shieldBarObj?.transform?.Find("ShieldBarFilled")?.GetComponent<Image>();
            this.redShieldBarImg = this.shieldBarObj?.transform?.Find("ShieldBarRed")?.GetComponent<Image>();

            // Find all texts //
            this.healthText = leftFrame.Find("Bars")?.Find("BarrierBar")?.Find("Text").GetComponent<TextMeshProUGUI>();
            this.xpText = leftFrame.Find("Bars")?.Find("XpBar")?.Find("Text").GetComponent<TextMeshProUGUI>();
            this.levelText = leftFrame.Find("Level")?.Find("Text")?.GetComponent<TextMeshProUGUI>();
            this.energyText = rightFrame.Find("Bars").Find("EnergyBar")?.Find("Text")?.GetComponent<TextMeshProUGUI>();
            this.powerText = rightFrame.Find("Bars").Find("PowerBar")?.Find("Text")?.GetComponent<TextMeshProUGUI>();
            this.furyText = rightFrame.Find("FuryBar").Find("Text")?.GetComponent<TextMeshProUGUI>();
            this.shieldText = this.shieldBarObj?.transform?.Find("ShieldBarText")?.GetComponent<TextMeshProUGUI>();

            // Find all HUD Skill Slots //
            this.actionBar = this.newHUD.transform.Find("ActionBar").gameObject;
            for (int i = 1; i <= 10; i++)
            {
                // Find the Skill Slot //
                GameObject skillSlot = this.actionBar.transform.Find("BarSkill" + (i).ToString()).gameObject;
                HUDSkillSlotsList.Add(i, skillSlot);
            }

            // Hide unused GUI Components //
            this.furyBarObj.SetActive(false);
            this.comboBarObj.SetActive(false);
            this.shieldBarObj.SetActive(false);

            // Get the Panthera Object //
            this.ptraObj = PantheraObj.Instance;

            // Find the Panthera Body //
            this.pantheraBody = this.ptraObj.characterBody;

            // Find the Heal Component //
            this.healthComponent = this.ptraObj.healthComponent;

            // Find the Input Bank //
            this.inputBank = this.ptraObj.pantheraInputBank;

            // Set the Buffs position //
            this.buffsDisplay = (RectTransform)this.origHUD.buffDisplay.transform;
            this.buffsDisplay.SetParent(leftFrame);
            this.buffsDisplay.anchoredPosition = new Vector2(200, -80);

            // Set the Equipment position //
            this.equipmentDisplay.SetParent(rightFrame);
            this.equipmentDisplay.anchoredPosition = new Vector2(-715, 300);

        }

        public void Update()
        {

            if (this.buffsDisplay == null || this.pantheraBody == null 
                || this.pantheraBody.teamComponent == null || TeamManager.instance == null)
            {
                return;
            }

            // Update the Buffs position //
            if(this.buffsDisplay != null && this.buffsDisplay)
                this.buffsDisplay.eulerAngles = new Vector3(0, 0, 0);

            // Get the Team Index //
            RoR2.TeamIndex index = this.pantheraBody.teamComponent.teamIndex;

            // Set the Health Bar and Text //
            this.setHealth(this.healthComponent.health, this.healthComponent.fullHealth);

            // Set the Barrier Bar //
            this.setBarrier(this.healthComponent.barrier, this.healthComponent.fullBarrier);

            // Set the XP Bar and Text //
            ulong maxXP = TeamManager.instance.GetTeamNextLevelExperience(index) - TeamManager.instance.GetTeamCurrentLevelExperience(index);
            ulong currentXP = TeamManager.instance.GetTeamExperience(index) - TeamManager.instance.GetTeamCurrentLevelExperience(index);
            this.setXP((float)currentXP, (float)maxXP);

            // Set the Level //
            this.setLevel((int)TeamManager.instance.GetTeamLevel(index));

            // Set the Energy //
            this.setEnergy(this.pantheraBody.energy, this.ptraObj.activePreset.maxEnergy);

            // Set the Power //
            this.setPower(this.pantheraBody.power, this.ptraObj.activePreset.maxPower);

            // Set the Fury //
            this.setFury(this.pantheraBody.fury, this.ptraObj.activePreset.maxFury);

            // Set the Combo Points //
            this.setComboPoint(this.pantheraBody.comboPoint, this.ptraObj.activePreset.maxComboPoint);

            // Set the Shield //
            this.setShield(this.pantheraBody.shield, this.ptraObj.activePreset.maxShield);

            // Update Action Bars //
            this.updateActionBar();

        }

        public void setHealth(float value, float max)
        {
            float newValue = 1.2f - (value / max * 2.4f);
            this.healthBarMat.SetFloat("PositionUV_X_1", newValue);
            this.healthText.SetText("{0}/{1}", (int)value, (int)max);
        }

        public void setBarrier(float value, float max)
        {
            float newValue = 1.2f - (value / max * 2.4f);
            this.barrierbarMat.SetFloat("PositionUV_X_1", newValue);
        }

        public void setXP(float value, float max)
        {
            float newValue = 1.2f - (value / max * 2.4f);
            this.xpBarMat.SetFloat("PositionUV_X_1", newValue);
            this.xpText.SetText("{0}/{1}", (int)value, (int)max);
        }

        public void setLevel(int level)
        {
            this.levelText.SetText(level.ToString());
        }

        public void setEnergy(float value, float max)
        {
            float newValue = 1.2f - (value / max * 2.4f);
            this.energyBarMat.SetFloat("PositionUV_X_1", newValue);
            this.energyText.SetText("{0}/{1}", (int)value, (int)max);
        }

        public void setPower(float value, float max)
        {
            float newValue = 1.2f - (value / max * 2.4f);
            this.powerBarMat.SetFloat("PositionUV_X_1", newValue);
            this.powerText.SetText("{0}/{1}", (int)value, (int)max);
        }

        public void setFury(float value, float max)
        {
            if (this.ptraObj.activePreset.getAbilityLevel(PantheraConfig.DestructionAbilityID) > 0)
            {
                this.furyBarObj.SetActive(true);
            }
            else
            {
                this.furyBarObj.SetActive(false);
                return;
            }
            float newValue = 1.8f - (value / max * 3.6f);
            this.furyBarMat.SetFloat("PositionUV_Y_1", newValue);
            this.furyText.SetText("{0}/{1}", (int)value, (int)max);
        }

        public void setComboPoint(float value, float max)
        {
            if (this.ptraObj.activePreset.getAbilityLevel(PantheraConfig.RuseAbilityID) > 0)
            {
                this.comboBarObj.SetActive(true);
            }
            else
            {
                this.comboBarObj.SetActive(false);
                return;
            }
            float newValue = value / max;
            this.filledComboBarImg.fillAmount = newValue;
        }

        public void setShield(float value, float max)
        {

            if (this.ptraObj.activePreset.getAbilityLevel(PantheraConfig.GuardianAbilityID) > 0)
            {
                if (this.ptraObj.frontShield.active == true || value < max)
                    this.shieldBarObj.SetActive(true);
                else
                    this.shieldBarObj.SetActive(false);
            }
            else
            {
                this.shieldBarObj.SetActive(false);
                return;
            }

            if (value == 0)
            {
                this.shieldBarObj.transform.Find("ShieldBarFilled").gameObject.SetActive(false);
                this.shieldBarObj.transform.Find("ShieldBarRed").gameObject.SetActive(true);
            }
            else
            {
                this.shieldBarObj.transform.Find("ShieldBarFilled").gameObject.SetActive(true);
                this.shieldBarObj.transform.Find("ShieldBarRed").gameObject.SetActive(false);
            }

            float newValue = value / max;
            this.filledShieldBarImg.fillAmount = newValue;
            this.shieldText.SetText("{0}", (int)value);
        }

        public void updateActionBar()
        {

            // Check the Switch Bar button //
            int j = 0;
            if (this.inputBank.switchBarPressed) j = 10;

            // Set up each Slot //
            for (int i = 1; i <= 10; i++)
            {

                // Find the ActionBar Slot //
                GameObject actionBarSLot = this.HUDSkillSlotsList[i];

                // Find the Skill //
                PantheraSkill skill = null;
                if (this.ptraObj.activePreset.slotsSkillsLinkList.ContainsKey(i + j))
                    skill = this.ptraObj.activePreset.slotsSkillsLinkList[i + j];

                // Find all Components //
                Image image = actionBarSLot.transform.Find("AbilityContainer").Find("AbilityIcon").GetComponent<Image>();
                TextMeshProUGUI CDText = actionBarSLot.transform.Find("CooldownText").GetComponent<TextMeshProUGUI>();
                Image CDImage = actionBarSLot.transform.Find("CooldownFill").GetComponent<Image>();

                // Check if there is a skill //
                if (skill != null)
                {
                    // Set the Image //
                    image.sprite = skill.icon;
                    image.color = new Color(image.color.r, image.color.g, image.color.b, 255);
                    // Calcule the Cooldown //
                    float lastUsedTime = Time.time - PantheraSkill.GetCooldownTime(skill.skillID);
                    if (lastUsedTime > skill.cooldown) lastUsedTime = skill.cooldown;
                    float cooldownRemaining = skill.cooldown - lastUsedTime;
                    float fillAmount = cooldownRemaining / skill.cooldown;
                    // Set the Cooldown //
                    CDImage.gameObject.SetActive(true);
                    CDImage.fillAmount = fillAmount;
                    if (cooldownRemaining > 0f)
                    {
                        CDText.text = ((int)Math.Ceiling(cooldownRemaining)).ToString() + "s";
                        CDText.gameObject.SetActive(true);
                        CDImage.gameObject.SetActive(true);
                    }
                    else
                    {
                        CDText.gameObject.SetActive(false);
                        CDImage.gameObject.SetActive(false);
                    }
                }
                else
                {
                    // Remove the Image //
                    image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
                    // Remove the Cooldown //
                    CDText.gameObject.SetActive(false);
                    CDImage.gameObject.SetActive(false);
                }

            }
        }

    }
}