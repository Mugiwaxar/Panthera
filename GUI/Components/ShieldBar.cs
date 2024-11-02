using System;
using Panthera.BodyComponents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Panthera.GUI.Components
{
    public class ShieldBar : MonoBehaviour
    {
        public PantheraObj ptraObj;

        public GameObject shieldBarFilled, shieldBarRed;
        public Image fillImage;
        public TextMeshProUGUI valueText;
        
        public void OnEnable()
        {
            this.shieldBarFilled = this.transform.Find("ShieldBarFilled").gameObject;
            this.shieldBarFilled.SetActive(false);

            this.shieldBarRed = this.transform.Find("ShieldBarRed").gameObject;
            this.shieldBarRed.SetActive(true);

            this.valueText = this.transform.Find("ShieldBarText").GetComponent<TextMeshProUGUI>();
            this.fillImage = shieldBarFilled.GetComponent<Image>();
        }

        public void Update()
        {
            if (!this.ptraObj || !this.ptraObj.characterBody)
                return;

            // Update Shield Bar //
            float value = this.ptraObj.characterBody.frontShield;
            float max = this.ptraObj.characterBody.maxFrontShield;

            if (value == 0)
            {
                this.shieldBarFilled.SetActive(false);
                this.shieldBarRed.SetActive(true);
            }
            else
            {
                this.shieldBarFilled.SetActive(true);
                this.shieldBarRed.SetActive(false);
            }

            float frontShieldShownValue = value / max;
            this.fillImage.fillAmount = frontShieldShownValue;
            this.valueText.SetText(Math.Floor(value).ToString());

        }
    }
}
