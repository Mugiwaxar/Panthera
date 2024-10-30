using Panthera.Abilities;
using Panthera.Base;
using Panthera.GUI.Tabs;
using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using static Panthera.Abilities.PantheraAbility;

namespace Panthera.GUI.Tooltips
{
    public class AbilitiesTooltip : MonoBehaviour
    {

        public static Component TooltipComp;
        public static GameObject TooltipObj;
        public static int ShowCounter = 0;
        public static PantheraAbility Ability;

        public static void CreateTooltip(GameObject canvas)
        {
            // Create the Ability Tooltip Component //
            TooltipComp = canvas.AddComponent<AbilitiesTooltip>();
            // Instatiate the Tooltip Prefab //
            TooltipObj = GameObject.Instantiate(PantheraAssets.AbilitiesTooltipPrefab, canvas.transform, false);
            TooltipObj.SetActive(false);
        }

        public static void ShowTooltip(PantheraAbility ability, bool update = false)
        {

            // Save the Ability //
            Ability = ability;

            // Increase the Counter //
            if (update == false)
                ShowCounter++;

            // Update the Desc //
            ability.updateDesc();

            // Get the Type String //
            string typeString = "";
            if (ability.type == AbilityType.primary) typeString = "Primary";
            else if (ability.type == AbilityType.active) typeString = "Active";
            else if (ability.type == AbilityType.passive) typeString = "Passive";

            // Set the Tooltip //
            TooltipObj.transform.Find("Header").Find("AbilityIcon").GetComponent<Image>().sprite = ability.icon;
            TooltipObj.transform.Find("Header").Find("AbilityName").GetComponent<TextMeshProUGUI>().text = ability.name;
            TooltipObj.transform.Find("Header").Find("AbilityType").GetComponent<TextMeshProUGUI>().text = typeString;
            if (string.IsNullOrEmpty(ability.cooldown.ToString()) || ability.cooldown.ToString() == "0")
            {
                TooltipObj.transform.Find("Cooldown").gameObject.active = false;
                TooltipObj.transform.Find("Line").gameObject.active = false;
            }
            else
            {
                TooltipObj.transform.Find("Cooldown").gameObject.active = true;
                TooltipObj.transform.Find("Cooldown").Find("Amount").GetComponent<TextMeshProUGUI>().text = ability.cooldown.ToString();
                TooltipObj.transform.Find("Line").gameObject.active = true;
            }
            TooltipObj.transform.Find("Cooldown").Find("Amount").GetComponent<TextMeshProUGUI>().text = ability.cooldown.ToString();
            TooltipObj.transform.Find("Description1").GetComponent<TextMeshProUGUI>().text = ability.desc1;
            if (string.IsNullOrEmpty(ability.desc2))
            {
                TooltipObj.transform.Find("Line2").gameObject.active = false;
                TooltipObj.transform.Find("Description2").gameObject.active = false;
            }
            else
            {
                TooltipObj.transform.Find("Line2").gameObject.active = true;
                TooltipObj.transform.Find("Description2").gameObject.active = true;
                TooltipObj.transform.Find("Description2").GetComponent<TextMeshProUGUI>().text = ability.desc2;
            }

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
