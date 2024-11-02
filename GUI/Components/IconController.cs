using Panthera.Base;
using Panthera.BodyComponents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Panthera.GUI.KeysBinder;

namespace Panthera.GUI.Components
{
    public class IconController : MonoBehaviour
    {
        public PantheraHUD hud;
        public PantheraObj ptraObj;


        public GameObject spellModeImage;
        public GameObject[] abilityActiveImages;
        public GameObject[] spellActiveImages;

        public void OnEnable()
        {
            this.hud = GetComponent<PantheraHUD>();
            this.ptraObj = this.hud.ptraObj;

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
            hud.spellsIcons.transform.localPosition = new Vector3(-120, 200, 0);
            hud.abilitiesIcons.transform.localPosition = new Vector3(0, 230, 0);

            UpdateCooldowns();
            UpdateIcons();
        }

        public void UpdateCooldowns()
        {
            // Update others Skills Cooldown //
            foreach (var pair in this.ptraObj.skillLocator.rechargeSkillList)
            {
                var rechargeSkill = pair.Value;

                var rechargeSkillImage = this.hud.cooldownFrame.transform.Find(rechargeSkill.skill.name);
                if (!rechargeSkill.skill.showCooldown || rechargeSkill.stock >= rechargeSkill.maxStock)
                {
                    if (rechargeSkillImage)
                    {
                        GameObject.DestroyImmediate(rechargeSkillImage.gameObject);
                    }
                }
                else
                {
                    if (!rechargeSkillImage)
                    {
                        // Instantiate the Skill Template //
                        rechargeSkillImage = GameObject.Instantiate<GameObject>(PantheraAssets.HUDCooldownSkillTemplate, this.hud.cooldownFrame.transform).transform;
                        rechargeSkillImage.gameObject.name = rechargeSkill.skill.name;
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
        public void UpdateIcons()
        {
            // Get the Panthera Input Bank //
            var input = this.ptraObj.pantheraInputBank;
            var spellModeEnabled = input.keysPressedList.HasFlag(KeysEnum.SpellsMode);

            this.spellModeImage.SetActive(spellModeEnabled);

            // Update the Ability Frame //
            this.abilityActiveImages[0].SetActive(!spellModeEnabled && input.keysPressedList.HasFlag(KeysEnum.Ability1));
            this.abilityActiveImages[1].SetActive(!spellModeEnabled && input.keysPressedList.HasFlag(KeysEnum.Ability2));
            this.abilityActiveImages[2].SetActive(!spellModeEnabled && input.keysPressedList.HasFlag(KeysEnum.Ability3));
            this.abilityActiveImages[3].SetActive(!spellModeEnabled && input.keysPressedList.HasFlag(KeysEnum.Ability4));

            // Update the Spell Frame //
            this.spellActiveImages[0].SetActive(spellModeEnabled && input.keysPressedList.HasFlag(KeysEnum.Skill1));
            this.spellActiveImages[1].SetActive(spellModeEnabled && input.keysPressedList.HasFlag(KeysEnum.Skill2));
            this.spellActiveImages[2].SetActive(spellModeEnabled && input.keysPressedList.HasFlag(KeysEnum.Skill3));
            this.spellActiveImages[3].SetActive(spellModeEnabled && input.keysPressedList.HasFlag(KeysEnum.Skill4));

            this.spellActiveImages[4].SetActive(spellModeEnabled && input.keysPressedList.HasFlag(KeysEnum.Ability1));
            this.spellActiveImages[5].SetActive(spellModeEnabled && input.keysPressedList.HasFlag(KeysEnum.Ability2));
            this.spellActiveImages[6].SetActive(spellModeEnabled && input.keysPressedList.HasFlag(KeysEnum.Ability3));
            this.spellActiveImages[7].SetActive(spellModeEnabled && input.keysPressedList.HasFlag(KeysEnum.Ability4));

            // Change the Skill 1 Icon if Prowl or Ambition is activated //
            var targetSkill = this.hud.hud.skillIcons[0].targetSkill;
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
            targetSkill = this.hud.hud.skillIcons[1].targetSkill;
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
