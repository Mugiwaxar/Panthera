using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Combos;
using Panthera.Components;
using Panthera.GUI.Tooltips;
using Panthera.Utils;
using RewiredConsts;
using RoR2;
using RoR2.UI;
using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static Panthera.GUI.KeysBinder;
using static RoR2.Skills.ComboSkillDef;

namespace Panthera.GUI
{
    public class PantheraHUD : MonoBehaviour
    {

        public HUD origHUD;
        public GameObject origMainContainer;

        public PantheraObj ptraObj;

        public LevelText pantheraLevelTextComp;
        public RectTransform pantheraLevelBarRect;
        public GameObject furyBarObj;
        public Material furyBarMat;

        public GameObject abilitiesIcons;
        public GameObject spellsIcons;
        public GameObject combosFrame;
        public GameObject comboFailedFrame;
        public GameObject cooldownFrame;
        public GameObject shieldBar;
        public GameObject blockBar;

        public GameObject levelUpObj;
        public int lastSawLevel = 0;

        public GameObject ability1Obj;
        public GameObject ability2Obj;
        public GameObject ability3Obj;
        public GameObject ability4Obj;
        public GameObject spellsModeObj;
        public GameObject spell1Obj;
        public GameObject spell2Obj;
        public GameObject spell3Obj;
        public GameObject spell4Obj;
        public GameObject spell5Obj;
        public GameObject spell6Obj;
        public GameObject spell7Obj;
        public GameObject spell8Obj;

        public static void HUDAwakeHook(Action<RoR2.UI.HUD> orig, RoR2.UI.HUD self)
        {

            // Call the base function //
            orig(self);

            // Check if Client //
            if (NetworkClient.active == false)
                return;

            // Create the Panthera HUD //
            if (Panthera.PantheraHUD != null)
                GameObject.DestroyImmediate(Panthera.PantheraHUD);
            Panthera.PantheraHUD = self.mainContainer.AddComponent<PantheraHUD>();
            Panthera.PantheraHUD.origHUD = self;
            Panthera.PantheraHUD.origMainContainer = self.mainContainer;

            // Add the Debug Info Component //
            Utils.DebugInfo.DebugInfoComp = self.mainContainer.gameObject.AddComponent<Utils.DebugInfo>();

            // Disable the HUD //
            Panthera.PantheraHUD.enabled = false;

        }

        public void StartHUD(PantheraObj ptraObj)
        {

            // Save the Panthera Object //
            this.ptraObj = ptraObj;

            // Enable the HUD //
            Panthera.PantheraHUD.enabled = true;

            // Disable the Cursor //
            if (Panthera.InputPlayer != null && MPEventSystemManager.FindEventSystem(Panthera.InputPlayer) != null)
            {
                MPEventSystemManager.FindEventSystem(Panthera.InputPlayer).cursorOpenerCount = 0;
            }

            // Init the Crosshair //
            GameObject crosshairObj = GameObject.Instantiate<GameObject>(PantheraAssets.CrosshairPrefab, this.origMainContainer.transform.Find("MainUIArea").Find("CrosshairCanvas"));
            this.ptraObj.crosshairComp = crosshairObj.AddComponent<CrosshairComp>();
            this.ptraObj.crosshairComp.ptraObj = this.ptraObj;
            this.ptraObj.crosshairComp.crosshairObj = crosshairObj;

            // Get the Cluster //
            GameObject levelDisplayCluster = this.origMainContainer.transform.Find("MainUIArea").Find("SpringCanvas").Find("BottomLeftCluster").Find("BarRoots").Find("LevelDisplayCluster")?.gameObject;

            // Create the Panthera XP Bar //
            GameObject XPBar = GameObject.Instantiate<GameObject>(levelDisplayCluster, levelDisplayCluster.transform.parent);
            XPBar.name = "PantheraLevel";
            GameObject.DestroyImmediate(XPBar.transform.Find("BuffDisplayRoot").gameObject);
            GameObject.DestroyImmediate(XPBar.transform.Find("ExpBarRoot").GetComponent<ExpBar>());
            XPBar.transform.Find("LevelDisplayRoot").Find("PrefixText").GetComponent<HGTextMeshProUGUI>().color = PantheraConfig.PantheraHUDLevelBarColor;
            XPBar.transform.Find("LevelDisplayRoot").Find("ValueText").GetComponent<HGTextMeshProUGUI>().color = PantheraConfig.PantheraHUDLevelBarColor;
            XPBar.transform.Find("ExpBarRoot").Find("ShrunkenRoot").Find("FillPanel").GetComponent<Image>().color = PantheraConfig.PantheraHUDLevelBarColor;
            XPBar.transform.Find("LevelDisplayRoot").GetComponent<LevelText>().SetDisplayData((uint)Panthera.PantheraCharacter.characterLevel);
            this.pantheraLevelTextComp = XPBar.transform.Find("LevelDisplayRoot").GetComponent<LevelText>();
            this.pantheraLevelBarRect = XPBar.transform.Find("ExpBarRoot").Find("ShrunkenRoot").Find("FillPanel").GetComponent<RectTransform>();
            this.levelUpObj = GameObject.Instantiate<GameObject>(new GameObject(), this.origMainContainer.transform.Find("MainUIArea").Find("SpringCanvas").Find("BottomCenterCluster"));
            this.levelUpObj.name = "LevelUpImage";
            levelUpObj.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            levelUpObj.transform.localPosition = new Vector3(0, -48, 0);
            Image imageComp = levelUpObj.AddComponent<Image>();
            imageComp.sprite = PantheraAssets.LevelUpIcon;
            this.levelUpObj.active = false;

            // Change the Health Bar Order //
            GameObject healthbarRoot = origMainContainer.transform.Find("MainUIArea").Find("SpringCanvas").Find("BottomLeftCluster").Find("BarRoots").Find("HealthbarRoot").gameObject;
            healthbarRoot.transform.SetParent(levelDisplayCluster.transform.parent.parent);
            healthbarRoot.transform.SetParent(levelDisplayCluster.transform.parent);

            // Create the Fury Bar //
            this.furyBarObj = GameObject.Instantiate<GameObject>(PantheraAssets.FuryBar, origMainContainer.transform.Find("MainUIArea").Find("SpringCanvas").Find("BottomLeftCluster"));
            this.furyBarObj.transform.localPosition = new Vector3(-5, 38, 0);
            this.furyBarMat = this.furyBarObj?.transform?.Find("Health_ORB").GetComponent<Image>().materialForRendering;

            // Set the Current Level //
            this.lastSawLevel = Panthera.PantheraCharacter.characterLevel;

            // Instantiate the Abilities Icons //
            this.abilitiesIcons = GameObject.Instantiate<GameObject>(PantheraAssets.AbilitiesIcons, origMainContainer.transform.Find("MainUIArea").Find("SpringCanvas").Find("BottomLeftCluster"));
            this.abilitiesIcons.transform.localPosition = new Vector3(-15, 230, 0);
            this.abilitiesIcons.SetActive(true);

            // Instantiate the Spells Icons //
            this.spellsIcons = GameObject.Instantiate<GameObject>(PantheraAssets.SpellsIcons, origMainContainer.transform.Find("MainUIArea").Find("SpringCanvas").Find("BottomRightCluster"));
            this.spellsIcons.transform.localPosition = new Vector3(30, 140, 0);
            this.spellsIcons.SetActive(true);

            // Get all Ability Objects //
            this.ability1Obj = this.abilitiesIcons.transform.Find("Content").Find("Ability1Image").gameObject;
            this.ability2Obj = this.abilitiesIcons.transform.Find("Content").Find("Ability2Image").gameObject;
            this.ability3Obj = this.abilitiesIcons.transform.Find("Content").Find("Ability3Image").gameObject;
            this.ability4Obj = this.abilitiesIcons.transform.Find("Content").Find("Ability4Image").gameObject;

            // Change the Skills Colors //
            this.ability1Obj.GetComponent<Image>().color = PantheraConfig.SkillsLockedSkillColor;
            this.ability2Obj.GetComponent<Image>().color = PantheraConfig.SkillsLockedSkillColor;
            this.ability3Obj.GetComponent<Image>().color = PantheraConfig.SkillsLockedSkillColor;
            this.ability4Obj.GetComponent<Image>().color = PantheraConfig.SkillsLockedSkillColor;

            // Get all Spell Objects //
            this.spellsModeObj = this.spellsIcons.transform.Find("Content").Find("SpellsModeLayer").gameObject;
            this.spell1Obj = this.spellsIcons.transform.Find("Content").Find("Spell1Layer").Find("Spell1Image").gameObject;
            this.spell2Obj = this.spellsIcons.transform.Find("Content").Find("Spell2Layer").Find("Spell2Image").gameObject;
            this.spell3Obj = this.spellsIcons.transform.Find("Content").Find("Spell3Layer").Find("Spell3Image").gameObject;
            this.spell4Obj = this.spellsIcons.transform.Find("Content").Find("Spell4Layer").Find("Spell4Image").gameObject;
            this.spell5Obj = this.spellsIcons.transform.Find("Content").Find("Spell5Layer").Find("Spell5Image").gameObject;
            this.spell6Obj = this.spellsIcons.transform.Find("Content").Find("Spell6Layer").Find("Spell6Image").gameObject;
            this.spell7Obj = this.spellsIcons.transform.Find("Content").Find("Spell7Layer").Find("Spell7Image").gameObject;
            this.spell8Obj = this.spellsIcons.transform.Find("Content").Find("Spell8Layer").Find("Spell8Image").gameObject;

            // Change the Spells Colors //
            this.spell1Obj.GetComponent<Image>().color = PantheraConfig.SkillsLockedSkillColor;
            this.spell2Obj.GetComponent<Image>().color = PantheraConfig.SkillsLockedSkillColor;
            this.spell3Obj.GetComponent<Image>().color = PantheraConfig.SkillsLockedSkillColor;
            this.spell4Obj.GetComponent<Image>().color = PantheraConfig.SkillsLockedSkillColor;
            this.spell5Obj.GetComponent<Image>().color = PantheraConfig.SkillsLockedSkillColor;
            this.spell6Obj.GetComponent<Image>().color = PantheraConfig.SkillsLockedSkillColor;
            this.spell7Obj.GetComponent<Image>().color = PantheraConfig.SkillsLockedSkillColor;
            this.spell8Obj.GetComponent<Image>().color = PantheraConfig.SkillsLockedSkillColor;

            // Instantiate the Combos Template //
            this.combosFrame = GameObject.Instantiate<GameObject>(PantheraAssets.HUDComboBaseTemplate, origMainContainer.transform.Find("MainUIArea").Find("SpringCanvas"));
            this.combosFrame.transform.localPosition = new Vector3(0, -480, 0);

            // Instantiate the Cooldown Frame //
            this.cooldownFrame = GameObject.Instantiate<GameObject>(PantheraAssets.HUDCooldownFrame, origMainContainer.transform.Find("MainUIArea").Find("SpringCanvas").Find("BottomRightCluster"));
            this.cooldownFrame.transform.localPosition = new Vector3(-257, 80, 0);

            // Instantiate the Shield Bar //
            this.shieldBar = GameObject.Instantiate<GameObject>(PantheraAssets.HUDShieldBar, origMainContainer.transform.Find("MainUIArea").Find("SpringCanvas"));
            this.shieldBar.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            this.shieldBar.transform.localPosition = new Vector3(0, -350, 0);

            // Instantiate the Block Bar //
            Transform blockBarParrent = this.origHUD.healthBar.barContainer;
            this.blockBar = GameObject.Instantiate<GameObject>(PantheraAssets.HUDBlockBar, blockBarParrent);
            this.blockBar.transform.localScale = new Vector3(0.42f, 0.55f, 0.42f);
            this.blockBar.transform.localPosition = new Vector3(-0.5f, 0, 0);
            this.blockBar.transform.Find("Image").GetComponent<Image>().color = PantheraConfig.BlockBarColor;

        }

        public void Update()
        {

            // Check the Panthera Object //
            if (this.ptraObj == null)
                return;

            // Update the Panthera Level Bar //
            float xpFloat = (float)Panthera.PantheraCharacter.levelExperience / (float)Panthera.PantheraCharacter.levelMaxExperience;
            this.pantheraLevelBarRect.anchorMax = new Vector2(Math.Min(xpFloat, 1), 1);

            // Update te Panthera Level Text //
            this.pantheraLevelTextComp.SetDisplayData((uint)Panthera.PantheraCharacter.characterLevel);

            // Update the Fury Bar //
            float furyValue = this.ptraObj.characterBody.fury;
            float furyShownValue = 1 - (furyValue / this.ptraObj.characterBody.maxFury * 2);
            this.furyBarMat.SetFloat("PositionUV_X_1", furyShownValue);
            this.furyBarObj.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = Math.Round(furyValue).ToString();

            // Show the LevelUP Image //
            if (this.lastSawLevel != Panthera.PantheraCharacter.characterLevel)
            {
                this.lastSawLevel = Panthera.PantheraCharacter.characterLevel;
                this.levelUpObj.active = true;
            }

            // Update Shield Bar //
            float value = this.ptraObj.characterBody.frontShield;
            float max = this.ptraObj.characterBody.maxFrontShield;

            if (this.ptraObj.getAbilityLevel(PantheraConfig.FrontShield_AbilityID) > 0)
            {
                if (this.ptraObj.frontShieldObj.active == true || value < max)
                    this.shieldBar.SetActive(true);
                else
                    this.shieldBar.SetActive(false);
            }
            else
            {
                this.shieldBar.SetActive(false);
            }

            if (value == 0)
            {
                this.shieldBar.transform.Find("ShieldBarFilled").gameObject.SetActive(false);
                this.shieldBar.transform.Find("ShieldBarRed").gameObject.SetActive(true);
            }
            else
            {
                this.shieldBar.transform.Find("ShieldBarFilled").gameObject.SetActive(true);
                this.shieldBar.transform.Find("ShieldBarRed").gameObject.SetActive(false);
            }

            float frontShieldShownValue = value / max;
            this.shieldBar.transform.Find("ShieldBarFilled").GetComponent<Image>().fillAmount = frontShieldShownValue;
            this.shieldBar.transform.Find("ShieldBarText").GetComponent<TextMeshProUGUI>().SetText(Math.Floor(value).ToString());

            // Update the Block Bar //
            this.blockBar.GetComponent<RectTransform>().SetAsLastSibling();
            this.blockBar.transform.Find("Image").GetComponent<Image>().fillAmount = this.ptraObj.characterBody.block / 10;

            // Update all Icons Cooldown //
            this.updateCooldowns();

            // Update all Skills Icon //
            this.updateIcons();

            // Update the Combos Display //
            this.updateCombos();

        }

        public void updateCooldowns()
        {


            // Update the Skill1 Cooldown and Stock Icon //
            RechargeSkill rechargeSkill1 = this.ptraObj.skillLocator.rechargeSkillList[PantheraConfig.Rip_SkillID];
            if (rechargeSkill1.cooldown > 0)
                this.ptraObj.skillLocator.primary.rechargeStopwatch = this.ptraObj.skillLocator.getMaxCooldown(PantheraConfig.Rip_SkillID) - rechargeSkill1.cooldown;
            else
                this.ptraObj.skillLocator.primary.rechargeStopwatch = 0;
            this.ptraObj.skillLocator.primary.stock = this.ptraObj.skillLocator.getStock(PantheraConfig.Rip_SkillID);

            // Update the Skill2 Cooldown and Stock Icon //
            if (this.ptraObj.guardianMode == true)
            {
                RechargeSkill rechargeSkill2A = this.ptraObj.skillLocator.rechargeSkillList[PantheraConfig.FrontShield_SkillID];
                if (rechargeSkill2A.cooldown > 0)
                    this.ptraObj.skillLocator.secondary.rechargeStopwatch = this.ptraObj.skillLocator.getMaxCooldown(PantheraConfig.FrontShield_SkillID) - rechargeSkill2A.cooldown;
                else
                    this.ptraObj.skillLocator.secondary.rechargeStopwatch = 0;
                this.ptraObj.skillLocator.secondary.stock = this.ptraObj.skillLocator.getStock(PantheraConfig.FrontShield_SkillID);
            }
            else if (this.ptraObj.furyMode == true)
            {
                RechargeSkill rechargeSkill2B = this.ptraObj.skillLocator.rechargeSkillList[PantheraConfig.ClawsStorm_SkillID];
                if (rechargeSkill2B.cooldown > 0)
                    this.ptraObj.skillLocator.secondary.rechargeStopwatch = this.ptraObj.skillLocator.getMaxCooldown(PantheraConfig.ClawsStorm_SkillID) - rechargeSkill2B.cooldown;
                else
                    this.ptraObj.skillLocator.secondary.rechargeStopwatch = 0;
                this.ptraObj.skillLocator.secondary.stock = this.ptraObj.skillLocator.getStock(PantheraConfig.ClawsStorm_SkillID);
            }
            else
            {
                RechargeSkill rechargeSkill2C = this.ptraObj.skillLocator.rechargeSkillList[PantheraConfig.Slash_SkillID];
                if (rechargeSkill2C.cooldown > 0)
                    this.ptraObj.skillLocator.secondary.rechargeStopwatch = this.ptraObj.skillLocator.getMaxCooldown(PantheraConfig.Slash_SkillID) - rechargeSkill2C.cooldown;
                else
                    this.ptraObj.skillLocator.secondary.rechargeStopwatch = 0;
                this.ptraObj.skillLocator.secondary.stock = this.ptraObj.skillLocator.getStock(PantheraConfig.Slash_SkillID);
            }


            // Update the Skill3 Cooldown and Stock Icon //
            RechargeSkill rechargeSkill3 = this.ptraObj.skillLocator.rechargeSkillList[PantheraConfig.Leap_SkillID];
            if (rechargeSkill3.cooldown > 0)
                this.ptraObj.skillLocator.utility.rechargeStopwatch = this.ptraObj.skillLocator.getMaxCooldown(PantheraConfig.Leap_SkillID) - rechargeSkill3.cooldown;
            else
                this.ptraObj.skillLocator.utility.rechargeStopwatch = 0;
            this.ptraObj.skillLocator.utility.stock = this.ptraObj.skillLocator.getStock(PantheraConfig.Leap_SkillID);

            // Update the Skill4 Cooldown and Stock Icon //
            RechargeSkill rechargeSkill4 = this.ptraObj.skillLocator.rechargeSkillList[PantheraConfig.MightyRoar_SkillID];
            if (rechargeSkill4.cooldown > 0)
                this.ptraObj.skillLocator.special.rechargeStopwatch = this.ptraObj.skillLocator.getMaxCooldown(PantheraConfig.MightyRoar_SkillID) - rechargeSkill4.cooldown;
            else
                this.ptraObj.skillLocator.special.rechargeStopwatch = 0;
            this.ptraObj.skillLocator.special.stock = this.ptraObj.skillLocator.getStock(PantheraConfig.MightyRoar_SkillID);

            // Prowl Cooldown //
            float prowlCooldown = this.ptraObj.skillLocator.getCooldown(PantheraConfig.Prowl_SkillID);
            float prowlMaxCooldown = this.ptraObj.skillLocator.getMaxCooldown(PantheraConfig.Prowl_SkillID);
            if (prowlCooldown > 0)
            {
                this.ability1Obj.transform.Find("Ability1CooldownFill").gameObject.active = true;
                this.ability1Obj.transform.Find("Ability1CooldownText").gameObject.active = true;
                this.ability1Obj.transform.Find("Ability1CooldownFill").GetComponent<Image>().fillAmount = prowlCooldown / prowlMaxCooldown;
                this.ability1Obj.transform.Find("Ability1CooldownText").GetComponent<TextMeshProUGUI>().text = (Math.Floor(prowlCooldown) + 1).ToString();
            }
            else
            {
                this.ability1Obj.transform.Find("Ability1CooldownFill").gameObject.active = false;
                this.ability1Obj.transform.Find("Ability1CooldownText").gameObject.active = false;
            }

            // Fury Cooldown //
            float furyCooldown = this.ptraObj.skillLocator.getCooldown(PantheraConfig.Fury_SkillID);
            float furyMaxCooldown = this.ptraObj.skillLocator.getMaxCooldown(PantheraConfig.Fury_SkillID);
            if (furyCooldown > 0)
            {
                this.ability2Obj.transform.Find("Ability2CooldownFill").gameObject.active = true;
                this.ability2Obj.transform.Find("Ability2CooldownText").gameObject.active = true;
                this.ability2Obj.transform.Find("Ability2CooldownFill").GetComponent<Image>().fillAmount = furyCooldown / furyMaxCooldown;
                this.ability2Obj.transform.Find("Ability2CooldownText").GetComponent<TextMeshProUGUI>().text = (Math.Floor(furyCooldown) + 1).ToString();
            }
            else
            {
                this.ability2Obj.transform.Find("Ability2CooldownFill").gameObject.active = false;
                this.ability2Obj.transform.Find("Ability2CooldownText").gameObject.active = false;
            }

            // Detection Cooldown //
            float DetectionCooldown = this.ptraObj.skillLocator.getCooldown(PantheraConfig.Detection_SkillID);
            float DetectionMaxCooldown = this.ptraObj.skillLocator.getMaxCooldown(PantheraConfig.Detection_SkillID);
            if (DetectionCooldown > 0)
            {
                this.ability3Obj.transform.Find("Ability3CooldownFill").gameObject.active = true;
                this.ability3Obj.transform.Find("Ability3CooldownText").gameObject.active = true;
                this.ability3Obj.transform.Find("Ability3CooldownFill").GetComponent<Image>().fillAmount = DetectionCooldown / DetectionMaxCooldown;
                this.ability3Obj.transform.Find("Ability3CooldownText").GetComponent<TextMeshProUGUI>().text = (Math.Floor(DetectionCooldown)).ToString();
            }
            else
            {
                this.ability3Obj.transform.Find("Ability3CooldownFill").gameObject.active = false;
                this.ability3Obj.transform.Find("Ability3CooldownText").gameObject.active = false;
            }
            if (DetectionCooldown > DetectionMaxCooldown - PantheraConfig.Detection_cooldown)
                this.ability3Obj.transform.Find("Ability3CooldownFill").GetComponent<Image>().color = PantheraConfig.DetectionCDFillRechargeColor;
            else
                this.ability3Obj.transform.Find("Ability3CooldownFill").GetComponent<Image>().color = PantheraConfig.DetectionCDFillNormalColor;

            // Guardian Cooldown //
            float guardianCooldown = this.ptraObj.skillLocator.getCooldown(PantheraConfig.Guardian_SkillID);
            float guardianMaxCooldown = this.ptraObj.skillLocator.getMaxCooldown(PantheraConfig.Guardian_SkillID);
            if (guardianCooldown > 0)
            {
                this.ability4Obj.transform.Find("Ability4CooldownFill").gameObject.active = true;
                this.ability4Obj.transform.Find("Ability4CooldownText").gameObject.active = true;
                this.ability4Obj.transform.Find("Ability4CooldownFill").GetComponent<Image>().fillAmount = guardianCooldown / guardianMaxCooldown;
                this.ability4Obj.transform.Find("Ability4CooldownText").GetComponent<TextMeshProUGUI>().text = (Math.Floor(guardianCooldown) + 1).ToString();
            }
            else
            {
                this.ability4Obj.transform.Find("Ability4CooldownFill").gameObject.active = false;
                this.ability4Obj.transform.Find("Ability4CooldownText").gameObject.active = false;
            }

            // Ambition Cooldown //
            float ambitionCooldown = this.ptraObj.skillLocator.getCooldown(PantheraConfig.Ambition_SkillID);
            float ambitionMaxCooldown = this.ptraObj.skillLocator.getMaxCooldown(PantheraConfig.Ambition_SkillID);
            if (ambitionCooldown > 0)
            {
                this.spell3Obj.transform.Find("Spell3CooldownFill").gameObject.active = true;
                this.spell3Obj.transform.Find("Spell3CooldownText").gameObject.active = true;
                this.spell3Obj.transform.Find("Spell3CooldownFill").GetComponent<Image>().fillAmount = ambitionCooldown / ambitionMaxCooldown;
                this.spell3Obj.transform.Find("Spell3CooldownText").GetComponent<TextMeshProUGUI>().text = (Math.Floor(ambitionCooldown) + 1).ToString();
            }
            else
            {
                this.spell3Obj.transform.Find("Spell3CooldownFill").gameObject.active = false;
                this.spell3Obj.transform.Find("Spell3CooldownText").gameObject.active = false;
            }

            // Portal Surge Cooldown //
            float portalSurgeCooldown = this.ptraObj.skillLocator.getCooldown(PantheraConfig.PortalSurge_SkillID);
            float portalSurgeMaxCooldown = this.ptraObj.skillLocator.getMaxCooldown(PantheraConfig.PortalSurge_SkillID);
            if (portalSurgeCooldown > 0)
            {
                this.spell8Obj.transform.Find("Spell8CooldownFill").gameObject.active = true;
                this.spell8Obj.transform.Find("Spell8CooldownText").gameObject.active = true;
                this.spell8Obj.transform.Find("Spell8CooldownFill").GetComponent<Image>().fillAmount = portalSurgeCooldown / portalSurgeMaxCooldown;
                this.spell8Obj.transform.Find("Spell8CooldownText").GetComponent<TextMeshProUGUI>().text = (Math.Floor(portalSurgeCooldown) + 1).ToString();
            }
            else
            {
                this.spell8Obj.transform.Find("Spell8CooldownFill").gameObject.active = false;
                this.spell8Obj.transform.Find("Spell8CooldownText").gameObject.active = false;
            }

            // Clear the Cooldown Frame //
            foreach (Transform child in this.cooldownFrame.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            // Update others Skills Cooldown //
            foreach (KeyValuePair<int, RechargeSkill> pair in this.ptraObj.skillLocator.rechargeSkillList)
            {

                // Check if the Cooldown must be displayed //
                if (pair.Value.skill.showCooldown == false || pair.Value.stock >= pair.Value.maxStock)
                    continue;

                // Instantiate the Skill Template //
                GameObject skillTemplate = GameObject.Instantiate<GameObject>(PantheraAssets.HUDCooldownSkillTemplate, this.cooldownFrame.transform);

                // Change the Icon //
                skillTemplate.transform.Find("Image").GetComponent<Image>().sprite = pair.Value.skill.icon;

                // Set the Cooldown Text //
                skillTemplate.transform.Find("CooldownText").GetComponent<TextMeshProUGUI>().text = ((int)Math.Ceiling(pair.Value.cooldown)).ToString();

                // Set the Cooldown Fill Image //
                skillTemplate.transform.Find("CooldownFill").GetComponent<Image>().fillAmount = pair.Value.cooldown / pair.Value.baseCooldown;

            }

        }

        public void updateIcons()
        {

            // Get the Panthera Input Bank //
            PantheraInputBank input = this.ptraObj.pantheraInputBank;

            // Update the Ability 1 Frame //
            if (input.keysPressed.HasFlag(KeysEnum.SpellsMode) == false && input.keysPressed.HasFlag(KeysEnum.Ability1))
                this.ability1Obj.transform.Find("Ability1ActiveImage").gameObject.active = true;
            else
                this.ability1Obj.transform.Find("Ability1ActiveImage").gameObject.active = false;

            // Update the Ability 2 Frame //
            if (input.keysPressed.HasFlag(KeysEnum.SpellsMode) == false && input.keysPressed.HasFlag(KeysEnum.Ability2))
                this.ability2Obj.transform.Find("Ability2ActiveImage").gameObject.active = true;
            else
                this.ability2Obj.transform.Find("Ability2ActiveImage").gameObject.active = false;

            // Update the Ability 3 Frame //
            if (input.keysPressed.HasFlag(KeysEnum.SpellsMode) == false && input.keysPressed.HasFlag(KeysEnum.Ability3))
                this.ability3Obj.transform.Find("Ability3ActiveImage").gameObject.active = true;
            else
                this.ability3Obj.transform.Find("Ability3ActiveImage").gameObject.active = false;

            // Update the Ability 4 Frame //
            if (input.keysPressed.HasFlag(KeysEnum.SpellsMode) == false && input.keysPressed.HasFlag(KeysEnum.Ability4))
                this.ability4Obj.transform.Find("Ability4ActiveImage").gameObject.active = true;
            else
                this.ability4Obj.transform.Find("Ability4ActiveImage").gameObject.active = false;

            // Update the Spells Mode Frame //
            if (input.keysPressed.HasFlag(KeysEnum.SpellsMode))
                this.spellsModeObj.transform.Find("SpellsModeActiveImage").gameObject.active = true;
            else
                this.spellsModeObj.transform.Find("SpellsModeActiveImage").gameObject.active = false;

            // Update the Spell 1 Frame //
            if (input.keysPressed.HasFlag(KeysEnum.SpellsMode) && input.keysPressed.HasFlag(KeysEnum.Skill1))
                this.spell1Obj.transform.Find("Spell1ActiveImage").gameObject.active = true;
            else
                this.spell1Obj.transform.Find("Spell1ActiveImage").gameObject.active = false;

            // Update the Spell 2 Frame //
            if (input.keysPressed.HasFlag(KeysEnum.SpellsMode) && input.keysPressed.HasFlag(KeysEnum.Skill2))
                this.spell2Obj.transform.Find("Spell2ActiveImage").gameObject.active = true;
            else
                this.spell2Obj.transform.Find("Spell2ActiveImage").gameObject.active = false;

            // Update the Spell 3 Frame //
            if (input.keysPressed.HasFlag(KeysEnum.SpellsMode) && input.keysPressed.HasFlag(KeysEnum.Skill3))
                this.spell3Obj.transform.Find("Spell3ActiveImage").gameObject.active = true;
            else
                this.spell3Obj.transform.Find("Spell3ActiveImage").gameObject.active = false;

            // Update the Spell 4 Frame //
            if (input.keysPressed.HasFlag(KeysEnum.SpellsMode) && input.keysPressed.HasFlag(KeysEnum.Skill4))
                this.spell4Obj.transform.Find("Spell4ActiveImage").gameObject.active = true;
            else
                this.spell4Obj.transform.Find("Spell4ActiveImage").gameObject.active = false;

            // Update the Spell 5 Frame //
            if (input.keysPressed.HasFlag(KeysEnum.SpellsMode) && input.keysPressed.HasFlag(KeysEnum.Ability1))
                this.spell5Obj.transform.Find("Spell5ActiveImage").gameObject.active = true;
            else
                this.spell5Obj.transform.Find("Spell5ActiveImage").gameObject.active = false;

            // Update the Spell 6 Frame //
            if (input.keysPressed.HasFlag(KeysEnum.SpellsMode) && input.keysPressed.HasFlag(KeysEnum.Ability2))
                this.spell6Obj.transform.Find("Spell6ActiveImage").gameObject.active = true;
            else
                this.spell6Obj.transform.Find("Spell6ActiveImage").gameObject.active = false;

            // Update the Spell 7 Frame //
            if (input.keysPressed.HasFlag(KeysEnum.SpellsMode) && input.keysPressed.HasFlag(KeysEnum.Ability3))
                this.spell7Obj.transform.Find("Spell7ActiveImage").gameObject.active = true;
            else
                this.spell7Obj.transform.Find("Spell7ActiveImage").gameObject.active = false;

            // Update the Spell 8 Frame //
            if (input.keysPressed.HasFlag(KeysEnum.SpellsMode) && input.keysPressed.HasFlag(KeysEnum.Ability4))
                this.spell8Obj.transform.Find("Spell8ActiveImage").gameObject.active = true;
            else
                this.spell8Obj.transform.Find("Spell8ActiveImage").gameObject.active = false;

            // Update the Ability 1 Icon //
            if (Panthera.ProfileComponent.isSkillUnlocked(PantheraConfig.Prowl_SkillID) == false)
                this.ability1Obj.GetComponent<Image>().color = PantheraConfig.SkillsLockedSkillColor;
            else
                this.ability1Obj.GetComponent<Image>().color = PantheraConfig.SkillsNormalSkillColor;
            if (this.ptraObj.stealthed == true)
                this.ability1Obj.GetComponent<Image>().sprite = PantheraAssets.ProwlSkill;
            else
                this.ability1Obj.GetComponent<Image>().sprite = PantheraAssets.ProwlSkillDisabled;

            // Update the Ability 2 Icon //
            if (Panthera.ProfileComponent.isSkillUnlocked(PantheraConfig.Fury_SkillID) == false)
                this.ability2Obj.GetComponent<Image>().color = PantheraConfig.SkillsLockedSkillColor;
            else
                this.ability2Obj.GetComponent<Image>().color = PantheraConfig.SkillsNormalSkillColor;
            if (this.ptraObj.furyMode == true)
                this.ability2Obj.GetComponent<Image>().sprite = PantheraAssets.FurySkill;
            else
                this.ability2Obj.GetComponent<Image>().sprite = PantheraAssets.FurySkillDisabled;

            // Update the Ability 3 Icon //
            if (Panthera.ProfileComponent.isSkillUnlocked(PantheraConfig.Detection_SkillID) == false)
                this.ability3Obj.GetComponent<Image>().color = PantheraConfig.SkillsLockedSkillColor;
            else
                this.ability3Obj.GetComponent<Image>().color = PantheraConfig.SkillsNormalSkillColor;
            if (this.ptraObj.detectionMode == true)
                this.ability3Obj.GetComponent<Image>().sprite = PantheraAssets.DetectionSkill;
            else
                this.ability3Obj.GetComponent<Image>().sprite = PantheraAssets.DetectionSkillDisabled;

            // Update the Ability 4 Icon //
            if (Panthera.ProfileComponent.isSkillUnlocked(PantheraConfig.Guardian_SkillID) == false)
                this.ability4Obj.GetComponent<Image>().color = PantheraConfig.SkillsLockedSkillColor;
            else
                this.ability4Obj.GetComponent<Image>().color = PantheraConfig.SkillsNormalSkillColor;
            if (this.ptraObj.guardianMode == true)
                this.ability4Obj.GetComponent<Image>().sprite = PantheraAssets.GuardianSkill;
            else
                this.ability4Obj.GetComponent<Image>().sprite = PantheraAssets.GuardianSkillDisabled;

            // Update the Spell 3 Icon //
            if (Panthera.ProfileComponent.isSkillUnlocked(PantheraConfig.Ambition_SkillID) == false)
                this.spell3Obj.GetComponent<Image>().color = PantheraConfig.SkillsLockedSkillColor;
            else
                this.spell3Obj.GetComponent<Image>().color = PantheraConfig.SkillsNormalSkillColor;

            // Update the Spell 8 Icon //
            if (Panthera.ProfileComponent.isSkillUnlocked(PantheraConfig.PortalSurge_SkillID) == false)
                this.spell8Obj.GetComponent<Image>().color = PantheraConfig.SkillsLockedSkillColor;
            else
                this.spell8Obj.GetComponent<Image>().color = PantheraConfig.SkillsNormalSkillColor;

            // Change the Skill 1 Icon if Prowl or Ambition is activated //
            GenericSkill targetSkill1 = this.origMainContainer.transform.Find("MainUIArea").Find("SpringCanvas").Find("BottomRightCluster").Find("Scaler").Find("Skill1Root").GetComponent<SkillIcon>().targetSkill;
            if (targetSkill1 != null && targetSkill1.skillDef != null)
            {
                if (this.ptraObj.stealthed == true && this.ptraObj.getAbilityLevel(PantheraConfig.GhostRip_AbilityID) > 0)
                    targetSkill1.skillDef.icon = PantheraAssets.GhostRipSkillMenu;
                else if (this.ptraObj.ambitionMode == true && this.ptraObj.getAbilityLevel(PantheraConfig.GoldenRip_AbilityID) > 0)
                    targetSkill1.skillDef.icon = PantheraAssets.GoldenRipSkillMenu;
                else
                    targetSkill1.skillDef.icon = PantheraAssets.RipSkillMenu;
            }

            // Change the Skill 2 Icon if Guardian, Fury Mode or Arcane Anchor is activated //
            GenericSkill targetSkill2 = this.origMainContainer.transform.Find("MainUIArea").Find("SpringCanvas").Find("BottomRightCluster").Find("Scaler").Find("Skill2Root").GetComponent<SkillIcon>().targetSkill;
            if (targetSkill2 != null && targetSkill2.skillDef != null)
            {
                if (this.ptraObj.frontShieldDeployed == true)
                    targetSkill2.skillDef.icon = PantheraAssets.ArcaneAnchorSkillMenu;
                else if (this.ptraObj.guardianMode == true && this.ptraObj.getAbilityLevel(PantheraConfig.FrontShield_AbilityID) > 0)
                    targetSkill2.skillDef.icon = PantheraAssets.FrontShieldSkillMenu;
                else if (this.ptraObj.furyMode == true && this.ptraObj.getAbilityLevel(PantheraConfig.ClawsStorm_AbilityID) > 0)
                    targetSkill2.skillDef.icon = PantheraAssets.ClawStormSkillMenu;
                else
                    targetSkill2.skillDef.icon = PantheraAssets.SlashSkillMenu;
            }

        }

        public void updateCombos()
        {

            // Get the Combos Component //
            PantheraComboComponent comboComp = this.ptraObj.comboComponent;

            // Get the Skills Layout //
            Transform skillsLayout = combosFrame.transform.Find("Layout");

            // Clear the HUD Combos List //
            foreach (Transform elem in skillsLayout)
            {
                GameObject.Destroy(elem.gameObject);
            }

            // Create the Last Line Variable //
            GameObject lastLine = null;

            // Itinerate the Skills List //
            foreach (ComboSkill comboSkill in comboComp.actualCombosList)
            {
                // Instantiate the Skill Element //
                GameObject skillElem = GameObject.Instantiate<GameObject>(PantheraAssets.HUDComboSkillTemplate, skillsLayout);
                // Change the Icon //
                Image skillIcon = skillElem.transform.Find("Image").GetComponent<Image>();
                skillIcon.sprite = comboSkill.skill.icon;
                // Change the Icon color //
                if (Panthera.ProfileComponent.isSkillUnlocked(comboSkill.skill.skillID) == false)
                    skillIcon.color = PantheraConfig.SkillsLockedSkillColor;
                else
                    skillIcon.color = PantheraConfig.SkillsNormalSkillColor;
                // Add the Tooltip //
                // skillIcon.gameObject.AddComponent<SkillsTooltipComponent>().associatedScript = combo.skill;
                // Change the Elem Size //
                if (comboSkill.biggerIcon == true)
                    skillElem.GetComponent<RectTransform>().sizeDelta = new Vector2(40, 40);
                else
                    skillElem.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
                // Add the Buttons //
                Transform buttonsLayout = skillElem.transform.Find("Buttons");
                if (comboSkill.keyA > 0)
                {
                    GameObject buttonElem = GameObject.Instantiate<GameObject>(PantheraAssets.HUDComboButtonTemplate, buttonsLayout);
                    buttonElem.GetComponent<Image>().sprite = Utils.Functions.KeyEnumToSprite(comboSkill.keyA);
                }
                if (comboSkill.keyB > 0)
                {
                    GameObject buttonElem = GameObject.Instantiate<GameObject>(PantheraAssets.HUDComboButtonTemplate, buttonsLayout);
                    buttonElem.GetComponent<Image>().sprite = Utils.Functions.KeyEnumToSprite(comboSkill.keyB);
                }
                if (comboSkill.direction > 0)
                {
                    GameObject buttonElem = GameObject.Instantiate<GameObject>(PantheraAssets.HUDComboButtonTemplate, buttonsLayout);
                    buttonElem.GetComponent<Image>().sprite = Utils.Functions.KeyEnumToSprite(comboSkill.direction);
                }
                // Add the Cooldown //
                float coolDown = this.ptraObj.skillLocator.getCooldown(comboSkill.skill.skillID);
                float maxCooldown = this.ptraObj.skillLocator.getMaxCooldown(comboSkill.skill.skillID);
                skillElem.transform.Find("CooldownFill").GetComponent<Image>().fillAmount = coolDown / maxCooldown;
                skillElem.transform.Find("CooldownText").GetComponent<TextMeshProUGUI>().text = ((int)Math.Ceiling(coolDown)).ToString();
                if (coolDown <= 0)
                {
                    //skillElem.transform.Find("Image").Find("Frame").GetComponent<Image>().color = PantheraConfig.ComboCooldownNormalIconColor;
                    skillElem.transform.Find("CooldownText").gameObject.active = false;
                }
                else
                {
                    //skillElem.transform.Find("Image").Find("Frame").GetComponent<Image>().color = PantheraConfig.ComboCooldownLoadingIconColor;
                    skillElem.transform.Find("CooldownText").gameObject.active = true;
                }
                // Add the Line //
                lastLine = GameObject.Instantiate<GameObject>(PantheraAssets.HUDComboLineTemplate, skillsLayout);

            }

            // Remove the Last useless Line //
            GameObject.DestroyImmediate(lastLine);

            // Create the last failed Skill Icon //
            ComboSkill lastFailedCombo = comboComp.lastFailedSkill;
            if (lastFailedCombo != null)
            {
                // Instantiate the Combo Failed Frame //
                if (this.comboFailedFrame != null)
                    GameObject.DestroyImmediate(this.comboFailedFrame);
                this.comboFailedFrame = GameObject.Instantiate<GameObject>(PantheraAssets.HUDComboSkillTemplate, origMainContainer.transform.Find("MainUIArea").Find("SpringCanvas"));
                this.comboFailedFrame.transform.localPosition = new Vector3(0, -400, 0);
                this.comboFailedFrame.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                this.comboFailedFrame.active = false;
                // Enable the Frame //
                this.comboFailedFrame.active = true;
                // Change the Icon //
                Image skillIcon = this.comboFailedFrame.transform.Find("Image").GetComponent<Image>();
                skillIcon.sprite = lastFailedCombo.skill.icon;
                // Add the Buttons //
                Transform buttonsLayout = this.comboFailedFrame.transform.Find("Buttons");
                if (lastFailedCombo.keyA > 0)
                {
                    GameObject buttonElem = GameObject.Instantiate<GameObject>(PantheraAssets.HUDComboButtonTemplate, buttonsLayout);
                    buttonElem.GetComponent<Image>().sprite = Utils.Functions.KeyEnumToSprite(lastFailedCombo.keyA);
                }
                if (lastFailedCombo.keyB > 0)
                {
                    GameObject buttonElem = GameObject.Instantiate<GameObject>(PantheraAssets.HUDComboButtonTemplate, buttonsLayout);
                    buttonElem.GetComponent<Image>().sprite = Utils.Functions.KeyEnumToSprite(lastFailedCombo.keyB);
                }
                if (lastFailedCombo.direction > 0)
                {
                    GameObject buttonElem = GameObject.Instantiate<GameObject>(PantheraAssets.HUDComboButtonTemplate, buttonsLayout);
                    buttonElem.GetComponent<Image>().sprite = Utils.Functions.KeyEnumToSprite(lastFailedCombo.direction);
                }
                // Add the Cooldown //
                float coolDown = this.ptraObj.skillLocator.getCooldown(lastFailedCombo.skill.skillID);
                float maxCooldown = this.ptraObj.skillLocator.getMaxCooldown(lastFailedCombo.skill.skillID);
                this.comboFailedFrame.transform.Find("CooldownFill").GetComponent<Image>().fillAmount = coolDown / maxCooldown;
                this.comboFailedFrame.transform.Find("CooldownText").GetComponent<TextMeshProUGUI>().text = ((int)Math.Ceiling(coolDown)).ToString();
                if (coolDown <= 0)
                {
                    this.comboFailedFrame.transform.Find("Image").Find("Frame").GetComponent<Image>().color = PantheraConfig.ComboCooldownNormalIconColor;
                    this.comboFailedFrame.transform.Find("CooldownText").gameObject.active = false;
                }
                else
                {
                    this.comboFailedFrame.transform.Find("Image").Find("Frame").GetComponent<Image>().color = PantheraConfig.ComboCooldownLoadingIconColor;
                    this.comboFailedFrame.transform.Find("CooldownText").gameObject.active = true;
                }
            }
            else
            {
                // Destroy the Combo failed Frame //
                if (this.comboFailedFrame != null)
                    GameObject.DestroyImmediate(this.comboFailedFrame);
            }

        }

    }

}
