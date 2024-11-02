using UnityEngine.EventSystems;
using UnityEngine;
using Panthera.MachineScripts;
using Panthera.GUI.Tabs;

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
