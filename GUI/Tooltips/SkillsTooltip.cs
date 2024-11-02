using Panthera.Base;
using Panthera.MachineScripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Panthera.GUI.Tabs
{
    public class SkillsTooltip : MonoBehaviour
    {

        public static Component TooltipComp;
        public static GameObject TooltipObj;
        public static int ShowCounter = 0;

        public static void CreateTooltip(GameObject canvas)
        {
            // Create the Skills Tooltip Component //
            TooltipComp = canvas.AddComponent<SkillsTooltip>();
            // Instatiate the Tooltip Prefab //
            TooltipObj = GameObject.Instantiate(PantheraAssets.SkillsTooltipPrefab, canvas.transform, false);
            TooltipObj.SetActive(false);
        }

        public static void ShowTooltip(MachineScript script)
        {
            // Increase the Counter //
            ShowCounter++;
            // Set the Tooltip //
            TooltipObj.transform.Find("Header").Find("SkillIcon").GetComponent<Image>().sprite = script.icon;
            TooltipObj.transform.Find("Header").Find("SkillName").GetComponent<TextMeshProUGUI>().text = script.name;
            TooltipObj.transform.Find("Cooldown").Find("Amount").GetComponent<TextMeshProUGUI>().text = script.baseCooldown.ToString();
            TooltipObj.transform.Find("Description1").GetComponent<TextMeshProUGUI>().text = script.desc1;
            if(string.IsNullOrEmpty(script.desc2))
            {
                TooltipObj.transform.Find("Line2").gameObject.SetActive(false);
                TooltipObj.transform.Find("Description2").gameObject.SetActive(false);
            }
            else
            {
                TooltipObj.transform.Find("Line2").gameObject.SetActive(true);
                TooltipObj.transform.Find("Description2").gameObject.SetActive(true);
                TooltipObj.transform.Find("Description2").GetComponent<TextMeshProUGUI>().text = script.desc2;
            }
            if(Panthera.ProfileComponent.IsSkillUnlocked(script.skillID) == false)
                TooltipObj.transform.Find("Locked").gameObject.SetActive(true);
            else
                TooltipObj.transform.Find("Locked").gameObject.SetActive(false);
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
            if (TooltipObj.activeSelf == false) return;

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
