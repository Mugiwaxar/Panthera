using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.EventSystems;
using UnityEngine;
using Panthera.GUI.Tabs;
using Panthera.MachineScripts;
using Panthera.Abilities;

namespace Panthera.GUI.Tooltips
{
    public class AbilitiesTooltipComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        public PantheraAbility associatedAbility;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (this.associatedAbility != null)
            {
                AbilitiesTooltip.ShowTooltip(associatedAbility);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (AbilitiesTooltip.ShowCounter > 0)
                AbilitiesTooltip.HideTooltip();
        }

    }
}
