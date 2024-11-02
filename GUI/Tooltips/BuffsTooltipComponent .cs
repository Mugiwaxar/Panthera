using UnityEngine.EventSystems;
using UnityEngine;
using Panthera.Base;

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
