using System;
using System.Collections.Generic;
using System.Text;
using Panthera.BodyComponents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Panthera.GUI.Components
{
    public class PantheraSkillIcon : MonoBehaviour
    {
        public PantheraObj ptraObj;
        public int skillID;

        public Func<PantheraObj, bool> isEnabled;

        public RechargeSkill skill;

        public Image skillFillImage, skillImage;
        public TextMeshProUGUI skillTextImage;

        public Sprite enabledSprite, disabledSprite;

        public void Init(PantheraObj ptraObj, int skillID, Sprite enabledSprite, Sprite disabledSprite = null, Func<PantheraObj, bool> enable = null)
        {
            this.ptraObj = ptraObj;
            this.skillID = skillID;
            this.enabledSprite = enabledSprite;
            this.disabledSprite = disabledSprite;
            this.isEnabled = enable;
        }

        public void Start()
        {
            var objName = this.name.Replace("Image", "").Replace("(Clone)", "");
            var fillObj = this.transform.Find($"{objName}CooldownFill").gameObject;
            fillObj.SetActive(true);
            var textObj = this.transform.Find($"{objName}CooldownText").gameObject;
            textObj.SetActive(true);

            this.skillImage = this.GetComponent<Image>();
            this.skillImage.sprite = this.disabledSprite ?? this.enabledSprite;
            this.skillImage.color = PantheraConfig.SkillsLockedSkillColor;
            this.skillFillImage = fillObj.GetComponent<Image>();
            this.skillTextImage = textObj.GetComponent<TextMeshProUGUI>();
        }

        public void Update()
        {
            if (!this.ptraObj)
                return;

            this.skill ??= this.ptraObj.skillLocator.rechargeSkillList[skillID];

            if (this.isEnabled != null && this.disabledSprite != null)
                this.skillImage.sprite = isEnabled(this.ptraObj) ? this.enabledSprite : this.disabledSprite;

            this.skillImage.color = Panthera.ProfileComponent.IsSkillUnlocked(skillID) 
                ? PantheraConfig.SkillsNormalSkillColor 
                : PantheraConfig.SkillsLockedSkillColor;

            if (this.skillID == PantheraConfig.Detection_SkillID)
                UpdateDetectionSkill();

            UpdateCooldown();
        }
        public void UpdateDetectionSkill()
        {
            if (this.skill.cooldown > this.skill.baseCooldown - PantheraConfig.Detection_cooldown)
                this.skillFillImage.color = PantheraConfig.DetectionCDFillRechargeColor;
            else
                this.skillFillImage.color = PantheraConfig.DetectionCDFillNormalColor;
        }
        public void UpdateCooldown()
        {
            bool active = skill.cooldown > 0;

            skillFillImage.gameObject.SetActive(active);
            skillTextImage.gameObject.SetActive(active);

            skillFillImage.fillAmount = skill.cooldown / skill.baseCooldown;
            skillTextImage.text = (Mathf.FloorToInt(skill.cooldown) + 1).ToString();
        }
    }
}
