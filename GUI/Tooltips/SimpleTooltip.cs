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
        public static int ShowCounter = 0;

        public static void CreateTooltip(GameObject canvas)
        {
            // Create the Simple Tooltip Component //
            TooltipComp = canvas.AddComponent<SimpleTooltip>();
            // Instatiate the Tooltip Prefab //
            TooltipObj = GameObject.Instantiate(PantheraAssets.SimpleTooltipPrefab, canvas.transform, false);
            TooltipObj.SetActive(false);
        }

        public static void ShowTooltip(string text)
        {
            // Increase the Counter //
            ShowCounter++;
            // Set the Text //
            TooltipObj.transform.Find("Content").Find("Text").GetComponent<TextMeshProUGUI>().SetText(text);
        }

        public static void HideTooltip()
        {
            // Decrease the Counter //
            ShowCounter--;
        }

        public void Update()
        {

            // Show or Hide the Tooltip //
            if (ShowCounter > 0)
                TooltipObj.SetActive(true);
            else
                TooltipObj.SetActive(false);

            // Return if the Tooltip is not active //
            if (TooltipObj.active == false) return;

            // Change the pivot //
            Vector3 mousePosition = Input.mousePosition;
            RectTransform rec = TooltipObj.GetComponent<RectTransform>();
            if (mousePosition.y > Screen.height / 2 && mousePosition.x > Screen.width / 4 * 3)
                rec.pivot = new Vector2(1, 1);
            else if (mousePosition.y > Screen.height / 2)
                rec.pivot = new Vector2(0, 1);
            else if (mousePosition.x > Screen.width / 4 * 3)
                rec.pivot = new Vector2(1, 0);
            else
                rec.pivot = new Vector2(0, 0);

            // Change the Content Allign //
            if (mousePosition.x > Screen.width / 4 * 3)
                TooltipObj.GetComponent<HorizontalLayoutGroup>().childAlignment = TextAnchor.MiddleRight;
            else
                TooltipObj.GetComponent<HorizontalLayoutGroup>().childAlignment = TextAnchor.MiddleLeft;

            // Updates all Layouts //
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)TooltipObj.transform);

            // Change the Position //
            Vector3 screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100);
            if (mousePosition.y > Screen.height / 2 && mousePosition.x <= Screen.width / 4 * 3)
                screenPoint = new Vector3(Input.mousePosition.x + 35, Input.mousePosition.y - 35, 100);
            TooltipObj.transform.position = screenPoint;

        }

    }
}
