using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Panthera.Base;
using Panthera.BodyComponents;
using Rewired.Utils;
using RoR2;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Panthera.GUI.KeysBinder;

namespace Panthera.GUI.Components
{
    public class IconCooldown : MonoBehaviour
    {
        public PantheraHUD hud;
        public PantheraObj ptraObj;
        public PantheraSkillLocator skillLoc;

        public RechargeSkill ripSkill, frontShieldSkill, clawStormSkill, slashSkill, leapSkill, mightyRoarSkill;
        public RechargeSkill prowlSkill, furySkill, detectionSkill, guardianSkill, ambitionSpell, portalSpell;

        public GameObject spellModeImage;
        public GameObject[] abilityActiveImages;
        public GameObject[] spellActiveImages;
        public Color lockedSkill, normalSkill;

        // prowl
        public Image prowlFillImage, prowlImage;
        public TextMeshProUGUI prowlTextImage;

        // fury
        public Image furyFillImage, furyImage;
        public TextMeshProUGUI furyTextImage;

        // detection
        public Image detectionFillImage, detectionImage;
        public TextMeshProUGUI detectionTextImage;

        // guardian
        public Image guardianFillImage, guardianImage;
        public TextMeshProUGUI guardianTextImage;

        // ambition
        public Image ambitionFillImage, ambitionImage;
        public TextMeshProUGUI ambitionTextImage;

        // portal
        public Image portalFillImage, portalImage;
        public TextMeshProUGUI portalTextImage;

        public void OnEnable()
        {
            lockedSkill = PantheraConfig.SkillsLockedSkillColor;
            normalSkill = PantheraConfig.SkillsNormalSkillColor;

            this.hud = GetComponent<PantheraHUD>();
            this.ptraObj = this.hud.ptraObj;
            this.skillLoc = this.ptraObj.skillLocator;

            this.ripSkill = this.skillLoc.rechargeSkillList[PantheraConfig.Rip_SkillID];
            this.frontShieldSkill = this.skillLoc.rechargeSkillList[PantheraConfig.FrontShield_SkillID];
            this.clawStormSkill = this.skillLoc.rechargeSkillList[PantheraConfig.ClawsStorm_SkillID];
            this.slashSkill = this.skillLoc.rechargeSkillList[PantheraConfig.Slash_SkillID];
            this.leapSkill = this.skillLoc.rechargeSkillList[PantheraConfig.Leap_SkillID];
            this.mightyRoarSkill = this.skillLoc.rechargeSkillList[PantheraConfig.MightyRoar_SkillID];


            // ability 1
            // prowl
            var prowlFillObj = hud.ability1Obj.transform.Find("Ability1CooldownFill").gameObject;
            prowlFillObj.SetActive(true);
            var prowlTextObj = hud.ability1Obj.transform.Find("Ability1CooldownText").gameObject;
            prowlTextObj.SetActive(true);

            this.prowlSkill = this.skillLoc.rechargeSkillList[PantheraConfig.Prowl_SkillID];
            this.prowlImage = this.hud.ability1Obj.GetComponent<Image>();
            this.prowlFillImage = prowlFillObj.GetComponent<Image>();
            this.prowlTextImage = prowlTextObj.GetComponent<TextMeshProUGUI>();

            // ability 2
            // fury
            var furyFillObj = hud.ability2Obj.transform.Find("Ability2CooldownFill").gameObject;
            furyFillObj.SetActive(true);
            var furyTextObj = hud.ability2Obj.transform.Find("Ability2CooldownText").gameObject;
            furyTextObj.SetActive(true);

            this.furySkill = this.skillLoc.rechargeSkillList[PantheraConfig.Fury_SkillID];
            this.furyImage = this.hud.ability2Obj.GetComponent<Image>();
            this.furyFillImage = furyFillObj.GetComponent<Image>();
            this.furyTextImage = furyTextObj.GetComponent<TextMeshProUGUI>();

            // ability 3
            // detection
            var detectionFillObj = hud.ability3Obj.transform.Find("Ability3CooldownFill").gameObject;
            detectionFillObj.SetActive(true);
            var detectionTextObj = hud.ability3Obj.transform.Find("Ability3CooldownText").gameObject;
            detectionTextObj.SetActive(true);

            this.detectionSkill = this.skillLoc.rechargeSkillList[PantheraConfig.Detection_SkillID];
            this.detectionImage = this.hud.ability3Obj.GetComponent<Image>();
            this.detectionFillImage = detectionFillObj.GetComponent<Image>();
            this.detectionTextImage = detectionTextObj.GetComponent<TextMeshProUGUI>();

            // ability 4
            // guardian
            var guardianFillObj = hud.ability4Obj.transform.Find("Ability4CooldownFill").gameObject;
            guardianFillObj.SetActive(true);
            var guardianTextObj = hud.ability4Obj.transform.Find("Ability4CooldownText").gameObject;
            guardianTextObj.SetActive(true);

            this.guardianSkill = this.skillLoc.rechargeSkillList[PantheraConfig.Guardian_SkillID];
            this.guardianImage = this.hud.ability4Obj.GetComponent<Image>();
            this.guardianFillImage = guardianFillObj.GetComponent<Image>();
            this.guardianTextImage = guardianTextObj.GetComponent<TextMeshProUGUI>();

            // spell 3
            // ambition
            var ambitionFillObj = hud.spell3Obj.transform.Find("Spell3CooldownFill").gameObject;
            ambitionFillObj.SetActive(true);
            var ambitionTextObj = hud.spell3Obj.transform.Find("Spell3CooldownText").gameObject;
            ambitionTextObj.SetActive(true);

            this.ambitionSpell = this.skillLoc.rechargeSkillList[PantheraConfig.Ambition_SkillID];
            this.ambitionImage = this.hud.spell3Obj.GetComponent<Image>();
            this.ambitionFillImage = ambitionFillObj.GetComponent<Image>();
            this.ambitionTextImage = ambitionTextObj.GetComponent<TextMeshProUGUI>();

            // spell 8
            // portal
            var portalFillObj = hud.spell8Obj.transform.Find("Spell8CooldownFill").gameObject;
            portalFillObj.SetActive(true);
            var portalTextObj = hud.spell8Obj.transform.Find("Spell8CooldownText").gameObject;
            portalTextObj.SetActive(true);

            this.portalSpell = this.skillLoc.rechargeSkillList[PantheraConfig.PortalSurge_SkillID];
            this.portalImage = this.hud.spell8Obj.GetComponent<Image>();
            this.portalFillImage = portalFillObj.GetComponent<Image>();
            this.portalTextImage = portalTextObj.GetComponent<TextMeshProUGUI>();

            this.spellModeImage = this.hud.spellsModeObj.transform.Find("SpellsModeActiveImage").gameObject;
            this.abilityActiveImages =
            [
                hud.ability1Obj.transform.Find("Ability1ActiveImage").gameObject,
                hud.ability2Obj.transform.Find("Ability2ActiveImage").gameObject,
                hud.ability3Obj.transform.Find("Ability3ActiveImage").gameObject,
                hud.ability4Obj.transform.Find("Ability4ActiveImage").gameObject
            ];

            this.spellActiveImages =
            [
                hud.spell1Obj.transform.Find("Spell1ActiveImage").gameObject,
                hud.spell2Obj.transform.Find("Spell2ActiveImage").gameObject,
                hud.spell3Obj.transform.Find("Spell3ActiveImage").gameObject,
                hud.spell4Obj.transform.Find("Spell4ActiveImage").gameObject,

                hud.spell5Obj.transform.Find("Spell5ActiveImage").gameObject,
                hud.spell6Obj.transform.Find("Spell6ActiveImage").gameObject,
                hud.spell7Obj.transform.Find("Spell7ActiveImage").gameObject,
                hud.spell8Obj.transform.Find("Spell8ActiveImage").gameObject,
            ];
        }

        public void Update()
        {
            if (!this.ptraObj)
                return;

            UpdateStocks();
            UpdateCooldowns();
            UpdateIcons();
        }

        /// <summary>
        /// Update the Skill Cooldown and Stock Icon
        /// </summary>
        public void UpdateStocks()
        {
            // Rip
            this.skillLoc.primary.rechargeStopwatch = this.ripSkill.cooldown > 0 ? this.ripSkill.baseCooldown - this.ripSkill.cooldown : 0;
            this.skillLoc.primary.stock = this.ripSkill.stock;

            // Update the Skill2 Cooldown and Stock Icon //
            if (this.ptraObj.guardianMode)
            {
                // Front Shield
                this.skillLoc.secondary.rechargeStopwatch = this.frontShieldSkill.cooldown > 0 ? this.frontShieldSkill.baseCooldown - this.frontShieldSkill.cooldown : 0;
                this.skillLoc.secondary.stock = this.frontShieldSkill.stock;
            }
            else if (this.ptraObj.furyMode)
            {
                // Claw Storm
                this.skillLoc.secondary.rechargeStopwatch = this.clawStormSkill.cooldown > 0 ? this.clawStormSkill.baseCooldown - this.clawStormSkill.cooldown : 0;
                this.skillLoc.secondary.stock = this.clawStormSkill.stock;
            }
            else
            {
                // Slash
                this.skillLoc.secondary.rechargeStopwatch = this.slashSkill.cooldown > 0 ? this.slashSkill.baseCooldown - this.slashSkill.cooldown : 0;
                this.skillLoc.secondary.stock = this.slashSkill.stock;
            }

            // Leap
            this.skillLoc.utility.rechargeStopwatch = this.leapSkill.cooldown > 0 ? this.leapSkill.baseCooldown - this.leapSkill.cooldown : 0;
            this.skillLoc.utility.stock = this.leapSkill.stock;

            // Mighty Roar
            this.skillLoc.special.rechargeStopwatch = this.mightyRoarSkill.cooldown > 0 ? this.mightyRoarSkill.baseCooldown - this.mightyRoarSkill.cooldown : 0;
            this.skillLoc.special.stock = this.mightyRoarSkill.stock;
        }

        public void UpdateSingleCooldown(RechargeSkill skill, Image fillImage, TextMeshProUGUI textImage)
        {
            bool active = skill.cooldown > 0;

            fillImage.gameObject.SetActive(active);
            textImage.gameObject.SetActive(active);

            fillImage.fillAmount = skill.cooldown / skill.baseCooldown;
            textImage.text = (Mathf.FloorToInt(skill.cooldown) + 1).ToString();
        }

        public void UpdateCooldowns()
        {
            // Prowl Cooldown //
            UpdateSingleCooldown(this.prowlSkill, this.prowlFillImage, this.prowlTextImage);

            // Fury Cooldown //
            UpdateSingleCooldown(this.furySkill, this.furyFillImage, this.furyTextImage);

            // Detection Cooldown //
            UpdateSingleCooldown(this.detectionSkill, this.detectionFillImage, this.detectionTextImage);

            if (this.detectionSkill.cooldown > this.detectionSkill.baseCooldown - PantheraConfig.Detection_cooldown)
                this.detectionFillImage.color = PantheraConfig.DetectionCDFillRechargeColor;
            else
                this.detectionFillImage.color = PantheraConfig.DetectionCDFillNormalColor;

            // Guardian Cooldown //
            UpdateSingleCooldown(this.guardianSkill, this.guardianFillImage, this.guardianTextImage);

            // Ambition Cooldown //
            UpdateSingleCooldown(this.ambitionSpell, this.ambitionFillImage, this.ambitionTextImage);

            // Portal Surge Cooldown //
            UpdateSingleCooldown(this.portalSpell, this.portalFillImage, this.portalTextImage);
            
            // Update others Skills Cooldown //
            foreach (var rechargeSkill in this.skillLoc.rechargeSkillList.Values)
            {
                var rechargeSkillImage = this.hud.cooldownFrame.transform.Find(rechargeSkill.skill.name);
                if (!rechargeSkill.skill.showCooldown || rechargeSkill.stock >= rechargeSkill.maxStock)
                {
                    if (rechargeSkillImage)
                    {
                        GameObject.Destroy(rechargeSkillImage.gameObject);
                    }
                }
                else
                {
                    if (!rechargeSkillImage)
                    {
                        // Instantiate the Skill Template //
                        rechargeSkillImage = GameObject.Instantiate<GameObject>(PantheraAssets.HUDCooldownSkillTemplate, this.hud.cooldownFrame.transform).transform;
                    }

                    // Change the Icon //
                    rechargeSkillImage.Find("Image").GetComponent<Image>().sprite = rechargeSkill.skill.icon;

                    // Set the Cooldown Text //
                    rechargeSkillImage.Find("CooldownText").GetComponent<TextMeshProUGUI>().text = (Mathf.FloorToInt(rechargeSkill.cooldown) + 1).ToString();

                    // Set the Cooldown Fill Image //
                    rechargeSkillImage.Find("CooldownFill").GetComponent<Image>().fillAmount = rechargeSkill.cooldown / rechargeSkill.baseCooldown;
                }
            }
        }
        private bool IsSkillUnlocked(int id) => Panthera.ProfileComponent.IsSkillUnlocked(id);
        public void UpdateIcons()
        {
            // Get the Panthera Input Bank //
            var input = this.ptraObj.pantheraInputBank;
            var spellModeEnabled = input.keysPressedList.Contains(KeysEnum.SpellsMode);

            this.spellModeImage.SetActive(spellModeEnabled);

            // Update the Ability Frame //
            this.abilityActiveImages[0].SetActive(!spellModeEnabled && input.keysPressedList.Contains(KeysEnum.Ability1));
            this.abilityActiveImages[1].SetActive(!spellModeEnabled && input.keysPressedList.Contains(KeysEnum.Ability2));
            this.abilityActiveImages[2].SetActive(!spellModeEnabled && input.keysPressedList.Contains(KeysEnum.Ability3));
            this.abilityActiveImages[3].SetActive(!spellModeEnabled && input.keysPressedList.Contains(KeysEnum.Ability4));

            // Update the Spell Frame //
            this.spellActiveImages[0].SetActive(spellModeEnabled && input.keysPressedList.Contains(KeysEnum.Skill1));
            this.spellActiveImages[1].SetActive(spellModeEnabled && input.keysPressedList.Contains(KeysEnum.Skill2));
            this.spellActiveImages[2].SetActive(spellModeEnabled && input.keysPressedList.Contains(KeysEnum.Skill3));
            this.spellActiveImages[3].SetActive(spellModeEnabled && input.keysPressedList.Contains(KeysEnum.Skill4));

            this.spellActiveImages[4].SetActive(spellModeEnabled && input.keysPressedList.Contains(KeysEnum.Ability1));
            this.spellActiveImages[5].SetActive(spellModeEnabled && input.keysPressedList.Contains(KeysEnum.Ability2));
            this.spellActiveImages[6].SetActive(spellModeEnabled && input.keysPressedList.Contains(KeysEnum.Ability3));
            this.spellActiveImages[7].SetActive(spellModeEnabled && input.keysPressedList.Contains(KeysEnum.Ability4));

            // update colors
            this.prowlImage.color = IsSkillUnlocked(PantheraConfig.Prowl_SkillID) ? normalSkill : lockedSkill;
            this.furyImage.color = IsSkillUnlocked(PantheraConfig.Fury_SkillID) ? normalSkill : lockedSkill;
            this.detectionImage.color = IsSkillUnlocked(PantheraConfig.Detection_SkillID) ? normalSkill : lockedSkill;
            this.guardianImage.color = IsSkillUnlocked(PantheraConfig.Guardian_SkillID) ? normalSkill : lockedSkill;
            this.ambitionImage.color = IsSkillUnlocked(PantheraConfig.Ambition_SkillID) ? normalSkill : lockedSkill;
            this.portalImage.color = IsSkillUnlocked(PantheraConfig.PortalSurge_SkillID) ? normalSkill : lockedSkill;

            // update images
            this.prowlImage.sprite = this.ptraObj.stealthed ? PantheraAssets.ProwlSkill : PantheraAssets.ProwlSkillDisabled;
            this.furyImage.sprite = this.ptraObj.furyMode ? PantheraAssets.FurySkill : PantheraAssets.FurySkillDisabled;
            this.detectionImage.sprite = this.ptraObj.detectionMode ? PantheraAssets.DetectionSkill : PantheraAssets.DetectionSkillDisabled;
            this.guardianImage.sprite = this.ptraObj.guardianMode ? PantheraAssets.GuardianSkill : PantheraAssets.GuardianSkillDisabled;

            // Change the Skill 1 Icon if Prowl or Ambition is activated //
            var targetSkill = this.hud.origHUD.skillIcons[0].targetSkill;
            if (targetSkill && targetSkill.skillDef)
            {
                if (this.ptraObj.stealthed && this.ptraObj.GetAbilityLevel(PantheraConfig.GhostRip_AbilityID) > 0)
                    targetSkill.skillDef.icon = PantheraAssets.GhostRipSkillMenu;
                else if (this.ptraObj.ambitionMode && this.ptraObj.GetAbilityLevel(PantheraConfig.GoldenRip_AbilityID) > 0)
                    targetSkill.skillDef.icon = PantheraAssets.GoldenRipSkillMenu;
                else
                    targetSkill.skillDef.icon = PantheraAssets.RipSkillMenu;
            }

            // Change the Skill 2 Icon if Guardian, Fury Mode or Arcane Anchor is activated //
            targetSkill = this.hud.origHUD.skillIcons[1].targetSkill;
            if (targetSkill && targetSkill.skillDef)
            {
                if (this.ptraObj.frontShieldDeployed)
                    targetSkill.skillDef.icon = PantheraAssets.ArcaneAnchorSkillMenu;
                else if (this.ptraObj.guardianMode && this.ptraObj.GetAbilityLevel(PantheraConfig.FrontShield_AbilityID) > 0)
                    targetSkill.skillDef.icon = PantheraAssets.FrontShieldSkillMenu;
                else if (this.ptraObj.furyMode && this.ptraObj.GetAbilityLevel(PantheraConfig.ClawsStorm_AbilityID) > 0)
                    targetSkill.skillDef.icon = PantheraAssets.ClawStormSkillMenu;
                else
                    targetSkill.skillDef.icon = PantheraAssets.SlashSkillMenu;
            }
        }
    }
}
