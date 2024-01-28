using Panthera.Base;
using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace Panthera.GUI.Tooltips
{
    public class SimpleTooltip : MonoBehaviour
    {

        public static Component TooltipComp;
        public static GameObject TooltipObj;
        public static int showCounter = 0;

        public static void CreateTooltip(GameObject canvas)
        {
            // Create the Simple Tooltip Component //
            TooltipComp = canvas.AddComponent<SimpleTooltip>();
            // Instatiate the Tooltip Prefab //
            TooltipObj = GameObject.Instantiate(Assets.SimpleTooltipPrefab, canvas.transform, false);
            TooltipObj.SetActive(false);
        }

        public static void ShowTooltip(string text)
        {
            // Increase the Counter //
            showCounter++;
            // Set the Text //
            TooltipObj.transform.Find("Text").GetComponent<TextMeshProUGUI>().SetText(text);
        }

        public static void HideTooltip()
        {
            // Decrease the Counter //
            showCounter--;
        }

        public void Update()
        {

            // Show or Hide the Tooltip //
            if (showCounter > 0)
                TooltipObj.SetActive(true);
            else
                TooltipObj.SetActive(false);

            // Return if the Tooltip is not active //
            if (TooltipObj.active == false) return;

            // Change the pivot //
            Vector3 mousePosition = Input.mousePosition;
            RectTransform rec = TooltipObj.GetComponent<RectTransform>();
            if (mousePosition.y > Screen.height / 2)
                rec.pivot = new Vector2(0, 1);
            else
                rec.pivot = new Vector2(0, 0);

            // Updates all Layouts //
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)TooltipObj.transform);

            // Change the Position //
            Vector3 screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100);
            TooltipObj.transform.position = screenPoint;

        }

    }
}
