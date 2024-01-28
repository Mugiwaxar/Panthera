using Panthera.Base;
using Panthera.MachineScripts;
using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Panthera.GUI.Tabs
{
    public class SkillsTooltip : MonoBehaviour
    {

        public static Component TooltipComp;
        public static GameObject TooltipObj;
        public static int showCounter = 0;

        public static void CreateTooltip(GameObject canvas)
        {
            // Create the Skills Tooltip Component //
            TooltipComp = canvas.AddComponent<SkillsTooltip>();
            // Instatiate the Tooltip Prefab //
            TooltipObj = GameObject.Instantiate(Assets.SkillsTooltipPrefab, canvas.transform, false);
            TooltipObj.SetActive(false);
        }

        public static void ShowTooltip(MachineScript script)
        {
            // Increase the Counter //
            showCounter++;
            // Set the Tooltip //
            TooltipObj.transform.Find("Header").Find("SkillIcon").GetComponent<Image>().sprite = script.icon;
            TooltipObj.transform.Find("Header").Find("SkillName").GetComponent<TextMeshProUGUI>().text = script.name;
            TooltipObj.transform.Find("Cooldown").Find("Amount").GetComponent<TextMeshProUGUI>().text = script.baseCooldown.ToString();
            TooltipObj.transform.Find("Description1").GetComponent<TextMeshProUGUI>().text = script.desc1;
            if(string.IsNullOrEmpty(script.desc2))
            {
                TooltipObj.transform.Find("Line2").gameObject.active = false;
                TooltipObj.transform.Find("Description2").gameObject.active = false;
            }
            else
            {
                TooltipObj.transform.Find("Line2").gameObject.active = true;
                TooltipObj.transform.Find("Description2").gameObject.active = true;
                TooltipObj.transform.Find("Description2").GetComponent<TextMeshProUGUI>().text = script.desc2;
            }
            if(script.locked == true)
                TooltipObj.transform.Find("Locked").gameObject.active = true;
            else
                TooltipObj.transform.Find("Locked").gameObject.active = false;
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

            // Change the pivot //
            Vector3 mousePosition = Input.mousePosition;
            RectTransform rec = TooltipObj.GetComponent<RectTransform>();
            if (mousePosition.y > Screen.height / 2)
                rec.pivot = new Vector2(0, 1);
            else
                rec.pivot = new Vector2(0, 0);

            // Return if the Tooltip is not active //
            if (TooltipObj.active == false) return;

            // Updates all Layouts //
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)TooltipObj.transform);

            // Change the Position //
            Vector3 screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100);
            TooltipObj.transform.position = screenPoint;

        }
    }
}
