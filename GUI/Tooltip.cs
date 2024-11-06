using EntityStates.LunarGolem;
using Panthera.Abilities;
using Panthera.Base;
using Panthera.Utils;
using RoR2.UI;
using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Panthera.GUI
{
    public class Tooltip
    {

        public enum TooltipType
        {
            passive,
            active,
            primary
        }

        public RectTransform tooltipGUI;
        public TextMeshProUGUI name;
        public TextMeshProUGUI type;
        public Transform iconLocation;
        public GameObject iconObj;
        public GameObject unlockLevel;
        public TextMeshProUGUI unlockLevelAmount;
        public GameObject cooldown;
        public TextMeshProUGUI cooldownAmount;
        public GameObject requiredFury;
        public TextMeshProUGUI requiredFuryAmount;
        public TextMeshProUGUI description;
        public GameObject requiredAbilities;
        public TextMeshProUGUI requiredAbilitiesText;

        public Tooltip(GameObject tooltipGUI)
        {

            this.tooltipGUI = (RectTransform)tooltipGUI.transform;
            this.name = tooltipGUI.transform.Find("Header").Find("AbilityName").GetComponent<TextMeshProUGUI>();
            this.type = tooltipGUI.transform.Find("Header").Find("SkillType").GetComponent<TextMeshProUGUI>();
            this.iconLocation = tooltipGUI.transform.Find("Header").Find("AbilityIcon");
            this.unlockLevel = tooltipGUI.transform.Find("UnlockLevel").gameObject;
            this.unlockLevelAmount = this.unlockLevel.transform.Find("Amount").GetComponent<TextMeshProUGUI>();
            this.cooldown = tooltipGUI.transform.Find("Cooldown").gameObject;
            this.cooldownAmount = this.cooldown.transform.Find("Amount").GetComponent<TextMeshProUGUI>();
            this.requiredFury = tooltipGUI.transform.Find("FuryCost").gameObject;
            this.requiredFuryAmount = this.requiredFury.transform.Find("Amount").GetComponent<TextMeshProUGUI>();
            this.description = tooltipGUI.transform.Find("Description").GetComponent<TextMeshProUGUI>();
            this.requiredAbilities = tooltipGUI.transform.Find("RequiredSkills").gameObject;
            this.requiredAbilitiesText = this.requiredAbilities.transform.Find("Text").GetComponent<TextMeshProUGUI>();

            tooltipGUI.SetActive(false);
            this.unlockLevel.SetActive(false);
            this.cooldown.SetActive(false);
            this.requiredFury.SetActive(false);
            this.requiredAbilities.SetActive(false);

        }

        public void updateTooltipPosition()
        {
            // Return if the Tooltip is not Active //
            if (this.tooltipGUI.gameObject.active == false) return;

            // Create the Pivot //
            float pivotX = 0;
            float pivotY = 0;

            // Change the X Pivot //
            if (Input.mousePosition.x > Screen.width / 2)
                pivotX = 1;
            else
                pivotX = 0;

            // Change the Y Pivot //
            if (Input.mousePosition.y > Screen.height / 2)
                pivotY = 1;
            else
                pivotY = 0;

            // Set the Pivot //
            this.tooltipGUI.pivot = new Vector2(pivotX, pivotY);

            // Change the Position //
            var screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100);
            this.tooltipGUI.position = Camera.main.ScreenToWorldPoint(screenPoint);

        }

        public void updateTooltip(string name, TooltipType type, Sprite icon, int unlockLevel, float cooldown,
            int requiredEnergy, int requiredPower, int requiredFury, string description, Dictionary<int, int> requiredAbilities)
        {

            // Set the Name //
            this.name.SetText(PantheraTokens.Get(name));

            // Set the type //
            if (type == TooltipType.primary)
                this.type.SetText("Primary");
            else if (type == TooltipType.active)
                this.type.SetText("Active");
            else if (type == TooltipType.passive)
                this.type.SetText("Passive");

            // Get the Icon Prefab //
            GameObject iconPrefab = PantheraAssets.PassiveSkillPrefab;
            if (type == TooltipType.primary)
                iconPrefab = PantheraAssets.PrimarySkillPrefab;
            else if (type == TooltipType.active)
                iconPrefab = PantheraAssets.ActiveSkillPrefab;
            else if (type == TooltipType.passive)
                iconPrefab = PantheraAssets.PassiveSkillPrefab;
            iconPrefab.SetActive(true);

            // Destroy the old Icon //
            GameObject.DestroyImmediate(this.iconObj);

            // Instanciate the Prefab //
            this.iconObj = GameObject.Instantiate<GameObject>(iconPrefab, this.iconLocation);

            // Set the Ability image //
            Image image = iconObj.transform.Find("Icon").GetComponent<Image>();
            image.sprite = icon;

            // Set the Cooldown //
            if (cooldown > 0)
            {
                this.cooldownAmount.SetText(cooldown.ToString());
                this.cooldown.SetActive(true);
            }
            else
            {
                this.cooldown.SetActive(false);
            }

            // Set the Fury cost //
            if (requiredFury > 0)
            {
                this.requiredFuryAmount.SetText(requiredFury.ToString());
                this.requiredFury.SetActive(true);
            }
            else
            {
                this.requiredFury.SetActive(false);
            }

            // Set the Description //
            this.description.SetText(PantheraTokens.Get(description));

            // Updates all Layouts //
            LayoutRebuilder.ForceRebuildLayoutImmediate(this.tooltipGUI);


        }

    }
}
