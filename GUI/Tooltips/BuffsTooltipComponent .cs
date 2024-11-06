using Panthera.Abilities;
using Panthera.Base;
using Panthera.GUI.Tabs;
using Panthera.MachineScripts;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Panthera.GUI.Tooltips
{
    public class BuffsTooltipComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        public PantheraBuff associatedBuff;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (this.associatedBuff != null)
            {
                BuffsTooltip.ShowTooltip(associatedBuff);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (BuffsTooltip.ShowCounter > 0)
                BuffsTooltip.HideTooltip();
        }

    }
}
