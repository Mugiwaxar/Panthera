using Panthera.Base;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Panthera.GUI.Tooltips
{
    public class BuffsTooltip : MonoBehaviour
    {

        public static Component TooltipComp;
        public static GameObject TooltipObj;
        public static int ShowCounter = 0;
        public static PantheraBuff Buff;

        public static void CreateTooltip(GameObject canvas)
        {
            // Create the Buff Tooltip Component //
            TooltipComp = canvas.AddComponent<BuffsTooltip>();
            // Instatiate the Tooltip Prefab //
            TooltipObj = GameObject.Instantiate(PantheraAssets.BuffsTooltipPrefab, canvas.transform, false);
            TooltipObj.SetActive(false);
        }

        public static void ShowTooltip(PantheraBuff buff)
        {

            // Save the Buff //
            Buff = buff;

            // Increase the Counter //
            ShowCounter++;

            // Get the Type String //
            /*
            string typeString = "";
            if (buff.isDebuff == false)
                typeString = "Buff";
            else 
                typeString = "Debuff";
            */

            // Set the Tooltip //
            TooltipObj.transform.Find("Header").Find("BuffIcon").GetComponent<Image>().sprite = buff.iconSprite;
            TooltipObj.transform.Find("Header").Find("BuffName").GetComponent<TextMeshProUGUI>().text = buff.displayName;
            TooltipObj.transform.Find("Header").Find("BuffType").GetComponent<TextMeshProUGUI>().text = buff.isDebuff == false ? "Buff" : "Debuff";
            TooltipObj.transform.Find("Duration").Find("Amount").GetComponent<TextMeshProUGUI>().text = buff.duration == 0 ? "Inf" : buff.duration.ToString();
            TooltipObj.transform.Find("MaxStacks").Find("Amount").GetComponent<TextMeshProUGUI>().text = buff.maxStacks == 0 ? "Inf" : buff.maxStacks.ToString();
            TooltipObj.transform.Find("Description1").GetComponent<TextMeshProUGUI>().text = buff.desc;
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
