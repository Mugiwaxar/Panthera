using UnityEngine.EventSystems;
using UnityEngine;
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
