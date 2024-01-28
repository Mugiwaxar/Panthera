using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.NetworkMessages;
using R2API.Networking.Interfaces;
using R2API.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Panthera.Components
{
    public class CrosshairComp : MonoBehaviour
    {

        public PantheraObj ptraObj;
        public GameObject crosshairObj;
        public Image loadingCrosshairImg;
        private float _loadingCrosshairLevel;
        public float loadingCrosshairLevel
        {
            get
            {
                return _loadingCrosshairLevel;
            }
            set
            {
                if (value > 100)
                    value = 100;
                if (value < 0)
                    value = 0;
                _loadingCrosshairLevel = value;
            }
        }
        public Coroutine hideCrosshairCoroutine;

        public void Start()
        {
            // Find the Loading Crosshair Image //
            this.loadingCrosshairImg = this.crosshairObj.transform.Find("LoadingCrossHair").GetComponent<Image>();
            // Hide the Crosshair //
            this.crosshairObj.SetActive(false);
        }

        public void FixedUpdate()
        {
            // Update the Loading Crosshair Image //
            this.loadingCrosshairImg.fillAmount = this._loadingCrosshairLevel;
        }

        public void unHideCrosshair()
        {
            // Check if the Coroutine must stop //
            if (this.hideCrosshairCoroutine != null)
            {
                this.StopCoroutine(this.hideCrosshairCoroutine);
                this.hideCrosshairCoroutine = null;
            }
            // Show the Crosshair //
            this.crosshairObj.SetActive(true);
        }

        public void delayHideCrosshair(float time)
        {
            // Delay hide the Crosshair //
            if(this.gameObject.active == true)
                this.hideCrosshairCoroutine = base.StartCoroutine(DelayUnstealth(this.crosshairObj, time));
        }

        public static IEnumerator DelayUnstealth(GameObject crosshairObj, float delay)
        {
            // Wait the time //
            yield return new WaitForSeconds(delay);
            // Hide the CrossHair //
            crosshairObj.SetActive(false);
        }

    }
}
