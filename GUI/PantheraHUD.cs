using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Combos;
using Panthera.Components;
using Panthera.GUI.Components;
using RoR2;
using RoR2.UI;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Panthera.GUI
{
    public class PantheraHUD : MonoBehaviour
    {
        public bool isRiskUI = true;
        public HUD hud;
        public ChildLocator origChildLoc;
        public GameObject origMainContainer;
        public Transform springCanvas;

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
            Panthera.PantheraHUD.hud = self;
            Panthera.PantheraHUD.origMainContainer = self.mainContainer;
            Panthera.PantheraHUD.origChildLoc = self.GetComponent<ChildLocator>();

            // Add the Debug Info Component //
            Utils.DebugInfo.DebugInfoComp = self.mainContainer.gameObject.AddComponent<Utils.DebugInfo>();

            // Disable the HUD //
            Panthera.PantheraHUD.enabled = false;

        }
        private Transform Find(Transform parent, string name)
        {
            if (!parent)
            {
                Debug.LogWarning($"Parent for {name} is null, can't search for child");
                return null;
            }

            var res = parent.Find(name);

            if (!res)
            {
                Debug.LogError($"{parent.name} does not have child {name}, did you mean: ");
                foreach (Transform t in parent)
                    Debug.LogWarning(t.name);
            }
            return res;
        }
        public void StartHUD(PantheraObj ptraObj)
        {
            /*
            MainUIArea/CrosshairCanvas
            MainUIArea/SpringCanvas
            MainUIArea/SpringCanvas/BottomLeftCluster
            MainUIArea/SpringCanvas/BottomLeftCluster/BarRoots
            MainUIArea/SpringCanvas/BottomLeftCluster/BarRoots/HealthbarRoot
            MainUIArea/SpringCanvas/BottomLeftCluster/BarRoots/LevelDisplayCluster
            MainUIArea/SpringCanvas/BottomCenterCluster
            MainUIArea/SpringCanvas/BottomRightCluster
            
            XPBar    MainUIArea/SpringCanvas/BottomLeftCluster/BarRoots/LevelDisplayCluster

            DestroyImmediate    XPBar BuffDisplayRoot
            DestroyImmediate    XPBar ExpBarRoot.GetComponent<ExpBar>()

            XPBar    LevelDisplayRoot/PrefixText        .GetComponent<HGTextMeshProUGUI>()
            XPBar    LevelDisplayRoot/ValueText         .GetComponent<HGTextMeshProUGUI>()
            XPBar    ExpBarRoot/ShrunkenRoot/FillPanel  .GetComponent<Image>()
            XPBar    LevelDisplayRoot                   .GetComponent<LevelText>()
            XPBar    LevelDisplayRoot                   .GetComponent<LevelText>();
            XPBar    ExpBarRoot/ShrunkenRoot/FillPanel  .GetComponent<RectTransform>();

            RightInfoBar
            TopCenterCluster
            ScopeContainer
            CrosshairExtras
            BottomLeftCluster
            BossHealthBar

           ?? RightUtilityArea
             */
            // Save the Panthera Object //
            this.ptraObj = ptraObj;

            // Enable the HUD //
            Panthera.PantheraHUD.enabled = true;

            // Disable the Cursor //
            if (Panthera.InputPlayer != null && MPEventSystemManager.FindEventSystem(Panthera.InputPlayer) != null)
            {
                MPEventSystemManager.FindEventSystem(Panthera.InputPlayer).cursorOpenerCount = 0;
            }
            var main = Find(this.origMainContainer.transform, "MainUIArea");
            springCanvas = Find(main, "SpringCanvas");

            var topCenter = origChildLoc.FindChild("TopCenterCluster");
            var bottomLeft = origChildLoc.FindChild("BottomLeftCluster");
            var bottomRight = Find(springCanvas, "BottomRightCluster");
            var bottomCenter = Find(springCanvas, "BottomCenterCluster");

            var rightInfo = origChildLoc.FindChild("RightInfoBar");
            var scope = origChildLoc.FindChild("ScopeContainer");
            var xhairExtras = origChildLoc.FindChild("CrosshairExtras");
            var bossHp = origChildLoc.FindChild("BossHealthBar");

            // Init the Crosshair //
            GameObject crosshairObj = GameObject.Instantiate<GameObject>(PantheraAssets.CrosshairPrefab, xhairExtras.parent);
            this.ptraObj.crosshairComp = crosshairObj.AddComponent<CrosshairComp>();
            this.ptraObj.crosshairComp.ptraObj = this.ptraObj;
            this.ptraObj.crosshairComp.crosshairObj = crosshairObj;

            // Get the Cluster //
            var hpBarRoot = this.hud.healthBar.transform;
            var xpBarRoot = this.hud.expBar.transform.parent;

            // Create the Panthera XP Bar //
            GameObject XPBar = GameObject.Instantiate<GameObject>(xpBarRoot.gameObject, xpBarRoot.parent);
            XPBar.name = "PantheraLevel";

            // image and xpbar component
            var sunkenRootParent = Find(XPBar.transform, isRiskUI ? "LevelBar" : "ExpBarRoot");
            var sunkenRoot = Find(sunkenRootParent, "SunkenRoot");
            GameObject.DestroyImmediate(sunkenRootParent.GetComponent<ExpBar>());

            if (!isRiskUI)
                GameObject.DestroyImmediate(XPBar.transform.Find("BuffDisplayRoot").gameObject);

            // text
            var levelDisplayRoot = isRiskUI ? XPBar.transform : Find(XPBar.transform, "LevelDisplayRoot");
            var prefixText = Find(levelDisplayRoot, "PrefixText");
            var valueText = Find(levelDisplayRoot, isRiskUI ? "LevelText" : "ValueText");
            var fillPanel = Find(sunkenRoot, "FillPanel");

            prefixText.GetComponent<TextMeshProUGUI>().color = PantheraConfig.PantheraHUDLevelBarColor;
            valueText.GetComponent<TextMeshProUGUI>().color = PantheraConfig.PantheraHUDLevelBarColor;
            fillPanel.GetComponent<Image>().color = PantheraConfig.PantheraHUDLevelBarColor;
            levelDisplayRoot.GetComponent<LevelText>().SetDisplayData((uint)Panthera.PantheraCharacter.characterLevel);
            
            this.pantheraLevelTextComp = levelDisplayRoot.GetComponent<LevelText>();
            this.pantheraLevelBarRect = fillPanel.GetComponent<RectTransform>();
            
            this.levelUpObj = GameObject.Instantiate<GameObject>(new GameObject(), bottomCenter);
            this.levelUpObj.name = "LevelUpImage";
            levelUpObj.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            levelUpObj.transform.localPosition = new Vector3(0, -48, 0);

            Image imageComp = levelUpObj.AddComponent<Image>();
            imageComp.sprite = PantheraAssets.LevelUpIcon;
            this.levelUpObj.SetActive(false);

            // Change the Health Bar Order //
            if (!isRiskUI)
            {
                hpBarRoot.SetAsLastSibling();
            }

            // Create the Fury Bar //
            this.furyBarObj = GameObject.Instantiate<GameObject>(PantheraAssets.FuryBar, hpBarRoot.parent);
            this.furyBarObj.transform.localPosition = new Vector3(-5, 38, 0);
            this.furyBarMat = this.furyBarObj?.transform?.Find("Health_ORB").GetComponent<Image>().materialForRendering;

            // Set the Current Level //
            this.lastSawLevel = Panthera.PantheraCharacter.characterLevel;

            // Instantiate the Abilities Icons //
            this.abilitiesIcons = GameObject.Instantiate<GameObject>(PantheraAssets.AbilitiesIcons, bottomLeft);
            this.abilitiesIcons.transform.localPosition = new Vector3(-15, 230, 0);
            this.abilitiesIcons.SetActive(true);

            // Instantiate the Spells Icons //
            this.spellsIcons = GameObject.Instantiate<GameObject>(PantheraAssets.SpellsIcons, bottomRight);
            this.spellsIcons.transform.localPosition = new Vector3(30, 140, 0);
            this.spellsIcons.SetActive(true);

            // Get all Ability Objects //
            this.ability1Obj = this.abilitiesIcons.transform.Find("Content").Find("Ability1Image").gameObject;
            this.ability1Obj.AddComponent<PantheraSkillIcon>().Init(this.ptraObj, PantheraConfig.Prowl_SkillID, PantheraAssets.ProwlSkill, PantheraAssets.ProwlSkillDisabled, (ptra) => ptra.stealthed);

            this.ability2Obj = this.abilitiesIcons.transform.Find("Content").Find("Ability2Image").gameObject;
            this.ability2Obj.AddComponent<PantheraSkillIcon>().Init(this.ptraObj, PantheraConfig.Fury_SkillID, PantheraAssets.FurySkill, PantheraAssets.FurySkillDisabled, (ptra) => ptra.furyMode);

            this.ability3Obj = this.abilitiesIcons.transform.Find("Content").Find("Ability3Image").gameObject;
            this.ability3Obj.AddComponent<PantheraSkillIcon>().Init(this.ptraObj, PantheraConfig.Detection_SkillID, PantheraAssets.DetectionSkill, PantheraAssets.DetectionSkillDisabled, (ptra) => ptra.detectionMode);

            this.ability4Obj = this.abilitiesIcons.transform.Find("Content").Find("Ability4Image").gameObject;
            this.ability4Obj.AddComponent<PantheraSkillIcon>().Init(this.ptraObj, PantheraConfig.Guardian_SkillID, PantheraAssets.GuardianSkill, PantheraAssets.GuardianSkillDisabled, (ptra) => ptra.guardianMode);

            // Get all Spell Objects //
            this.spellsModeObj = this.spellsIcons.transform.Find("Content").Find("SpellsModeLayer").gameObject;

            this.spell1Obj = this.spellsIcons.transform.Find("Content").Find("Spell1Layer").Find("Spell1Image").gameObject;
            //this.spell1Obj.AddComponent<PantheraSkillIcon>().Init(this.ptraObj, PantheraConfig.PortalSurge_SkillID, PantheraAssets.PortalSurgeSkill);

            this.spell2Obj = this.spellsIcons.transform.Find("Content").Find("Spell2Layer").Find("Spell2Image").gameObject;
            //this.spell2Obj.AddComponent<PantheraSkillIcon>().Init(this.ptraObj, PantheraConfig.spell, PantheraAssets.PortalSurgeSkill);

            this.spell3Obj = this.spellsIcons.transform.Find("Content").Find("Spell3Layer").Find("Spell3Image").gameObject;
            this.spell3Obj.AddComponent<PantheraSkillIcon>().Init(this.ptraObj, PantheraConfig.Ambition_SkillID, PantheraAssets.AmbitionSkill);

            this.spell4Obj = this.spellsIcons.transform.Find("Content").Find("Spell4Layer").Find("Spell4Image").gameObject;
            //this.spell8Obj.AddComponent<PantheraSkillIcon>().Init(this.ptraObj, PantheraConfig.PortalSurge_SkillID, PantheraAssets.PortalSurgeSkill);

            this.spell5Obj = this.spellsIcons.transform.Find("Content").Find("Spell5Layer").Find("Spell5Image").gameObject;
            //this.spell8Obj.AddComponent<PantheraSkillIcon>().Init(this.ptraObj, PantheraConfig.PortalSurge_SkillID, PantheraAssets.PortalSurgeSkill);

            this.spell6Obj = this.spellsIcons.transform.Find("Content").Find("Spell6Layer").Find("Spell6Image").gameObject;
            //this.spell8Obj.AddComponent<PantheraSkillIcon>().Init(this.ptraObj, PantheraConfig.PortalSurge_SkillID, PantheraAssets.PortalSurgeSkill);

            this.spell7Obj = this.spellsIcons.transform.Find("Content").Find("Spell7Layer").Find("Spell7Image").gameObject;
            //this.spell8Obj.AddComponent<PantheraSkillIcon>().Init(this.ptraObj, PantheraConfig.PortalSurge_SkillID, PantheraAssets.PortalSurgeSkill);

            this.spell8Obj = this.spellsIcons.transform.Find("Content").Find("Spell8Layer").Find("Spell8Image").gameObject;
            this.spell8Obj.AddComponent<PantheraSkillIcon>().Init(this.ptraObj, PantheraConfig.PortalSurge_SkillID, PantheraAssets.PortalSurgeSkill);

            // Instantiate the Combos Template //
            this.combosFrame = GameObject.Instantiate<GameObject>(PantheraAssets.HUDComboBaseTemplate, springCanvas);
            this.combosFrame.transform.localPosition = new Vector3(0, -480, 0);

            // Instantiate the Cooldown Frame //
            this.cooldownFrame = GameObject.Instantiate<GameObject>(PantheraAssets.HUDCooldownFrame, bottomRight);
            this.cooldownFrame.transform.localPosition = new Vector3(-257, 80, 0);

            // Instantiate the Shield Bar //
            this.shieldBar = GameObject.Instantiate<GameObject>(PantheraAssets.HUDShieldBar, springCanvas);
            this.shieldBar.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            this.shieldBar.transform.localPosition = new Vector3(0, -350, 0);
            this.shieldBar.AddComponent<ShieldBar>().ptraObj = this.ptraObj;

            // Instantiate the Block Bar //
            Transform blockBarParrent = this.hud.healthBar.barContainer;
            this.blockBar = GameObject.Instantiate<GameObject>(PantheraAssets.HUDBlockBar, blockBarParrent);
            this.blockBar.transform.localScale = new Vector3(0.42f, 0.55f, 0.42f);
            this.blockBar.transform.localPosition = new Vector3(-0.5f, 0, 0);
            this.blockBar.transform.Find("Image").GetComponent<Image>().color = PantheraConfig.BlockBarColor;

            origMainContainer.AddComponent<IconController>();
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
                this.levelUpObj.SetActive(true);
            }

            // Update Shield Bar //
            float value = this.ptraObj.characterBody.frontShield;
            float max = this.ptraObj.characterBody.maxFrontShield;

            if (this.ptraObj.GetAbilityLevel(PantheraConfig.FrontShield_AbilityID) > 0)
            {
                if (this.ptraObj.frontShieldObj.activeInHierarchy || value < max)
                    this.shieldBar.SetActive(true);
                else
                    this.shieldBar.SetActive(false);
            }
            else
            {
                this.shieldBar.SetActive(false);
            }
            // Update the Block Bar //
            this.blockBar.GetComponent<RectTransform>().SetAsLastSibling();
            this.blockBar.transform.Find("Image").GetComponent<Image>().fillAmount = this.ptraObj.characterBody.block / 10;

            // Update the Combos Display //
            this.updateCombos();

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
                if (Panthera.ProfileComponent.IsSkillUnlocked(comboSkill.skill.skillID) == false)
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
                float coolDown = this.ptraObj.skillLocator.GetCooldown(comboSkill.skill.skillID);
                float maxCooldown = this.ptraObj.skillLocator.GetMaxCooldown(comboSkill.skill.skillID);
                skillElem.transform.Find("CooldownFill").GetComponent<Image>().fillAmount = coolDown / maxCooldown;
                skillElem.transform.Find("CooldownText").GetComponent<TextMeshProUGUI>().text = ((int)Math.Ceiling(coolDown)).ToString();
                if (coolDown <= 0)
                {
                    //skillElem.transform.Find("Image").Find("Frame").GetComponent<Image>().color = PantheraConfig.ComboCooldownNormalIconColor;
                    skillElem.transform.Find("CooldownText").gameObject.SetActive(false);
                }
                else
                {
                    //skillElem.transform.Find("Image").Find("Frame").GetComponent<Image>().color = PantheraConfig.ComboCooldownLoadingIconColor;
                    skillElem.transform.Find("CooldownText").gameObject.SetActive(true);
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
                if (this.comboFailedFrame!= null)
                    GameObject.DestroyImmediate(this.comboFailedFrame);
                this.comboFailedFrame = GameObject.Instantiate<GameObject>(PantheraAssets.HUDComboSkillTemplate, springCanvas);
                this.comboFailedFrame.transform.localPosition = new Vector3(0, -400, 0);
                this.comboFailedFrame.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

                // Enable the Frame //
                this.comboFailedFrame.SetActive(true);
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
                float coolDown = this.ptraObj.skillLocator.GetCooldown(lastFailedCombo.skill.skillID);
                float maxCooldown = this.ptraObj.skillLocator.GetMaxCooldown(lastFailedCombo.skill.skillID);
                this.comboFailedFrame.transform.Find("CooldownFill").GetComponent<Image>().fillAmount = coolDown / maxCooldown;
                this.comboFailedFrame.transform.Find("CooldownText").GetComponent<TextMeshProUGUI>().text = ((int)Math.Ceiling(coolDown)).ToString();
                if (coolDown <= 0)
                {
                    this.comboFailedFrame.transform.Find("Image").Find("Frame").GetComponent<Image>().color = PantheraConfig.ComboCooldownNormalIconColor;
                    this.comboFailedFrame.transform.Find("CooldownText").gameObject.SetActive(false);
                }
                else
                {
                    this.comboFailedFrame.transform.Find("Image").Find("Frame").GetComponent<Image>().color = PantheraConfig.ComboCooldownLoadingIconColor;
                    this.comboFailedFrame.transform.Find("CooldownText").gameObject.SetActive(true);
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
