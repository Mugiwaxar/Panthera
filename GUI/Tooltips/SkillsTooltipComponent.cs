using Panthera.GUI.Tabs;
using Panthera.MachineScripts;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Panthera.GUI.Tooltips
{
    public class SkillsTooltipComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        public MachineScript associatedScript;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (this.associatedScript != null)
            {
                SkillsTooltip.ShowTooltip(associatedScript);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (SkillsTooltip.ShowCounter > 0)
                SkillsTooltip.HideTooltip();
        }

    }
}
