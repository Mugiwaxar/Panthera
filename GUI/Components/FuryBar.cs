using System;
using System.Collections.Generic;
using System.Text;
using Panthera.BodyComponents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Panthera.GUI.Components
{
    public class FuryBar : MonoBehaviour
    {
        public PantheraObj ptraObj;

        public Material furyBarMat;
        public TextMeshProUGUI textComp;
        public Vector3 pos;

        public void OnEnable()
        {
            this.transform.localPosition = pos;
            this.furyBarMat = this.transform.Find("Health_ORB").GetComponent<Image>().materialForRendering;
            this.textComp = this.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        }

        public void Update()
        {
            // Update the Fury Bar //
            this.transform.localPosition = pos;
            if (!this.ptraObj)
                return;
            float furyValue = this.ptraObj.characterBody.fury;
            float furyShownValue = 1 - (furyValue / this.ptraObj.characterBody.maxFury * 2);

            this.furyBarMat.SetFloat("PositionUV_X_1", furyShownValue);
            this.textComp.text = Math.Round(furyValue).ToString();
        }
    }
}
