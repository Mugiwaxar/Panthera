using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

namespace Panthera.Utils
{
    public class DebugInfo : MonoBehaviour
    {

        public static DebugInfo DebugInfoComp;
        GameObject debugInfoObj;
        Dictionary<string, TextMeshProUGUI> infosList = new Dictionary<string, TextMeshProUGUI>();

        public void Start()
        {
            // Instantiate the Debug Info Object //
            if (NetworkClient.active == true)
                this.debugInfoObj = GameObject.Instantiate<GameObject>(Base.PantheraAssets.debugInfo, Panthera.PantheraHUD.origMainContainer.transform);
        }

        public static void addText(string key, string text)
        {

            // Check the Component //
            if (DebugInfoComp == null || DebugInfoComp.debugInfoObj == null)
                return;

            // Check if the key Already exist //
            if (DebugInfoComp.infosList.ContainsKey(key))
            {
                // Modify the Text //
                DebugInfoComp.infosList[key].text = text;
            }
            else
            {
                // Create a new Text //
                GameObject newTextObj = GameObject.Instantiate<GameObject>(Base.PantheraAssets.debugInfoText, DebugInfoComp.debugInfoObj.transform);
                TextMeshProUGUI newTextComp = newTextObj.GetComponent<TextMeshProUGUI>();
                newTextComp.text = text;
                DebugInfoComp.infosList.Add(key, newTextComp);
            }

        }

    }
}
