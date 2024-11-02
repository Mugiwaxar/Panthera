using UnityEngine;
using UnityEngine.EventSystems;

namespace Panthera.GUI.Tabs
{
    public class SkillsTreeZoomComponent : MonoBehaviour, IScrollHandler
    {

        public SkillsTreeController skillsTreeController;

        public void OnScroll(PointerEventData eventData)
        {
            // Return if the Skills Tree Window is not active //
            if (this.skillsTreeController.skillsTreeWindow.activeSelf == false) return;

            // Get the Transform //
            RectTransform transform = this.skillsTreeController.skillsTreeContent;

            // Calculate the scalling //
            float scrollDelta = eventData.scrollDelta.y * 0.1f;
            float currentScale = transform.localScale.x;
            float newScale = currentScale + scrollDelta;
            newScale = Mathf.Clamp(newScale, 0.5f, 3f);

            // Get the Cursor Position //
            Vector3 screenPoint = new Vector3(eventData.position.x, eventData.position.y, 100);
            Vector2 localPointInRect;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(this.skillsTreeController.skillsTreeContent, eventData.position, null, out localPointInRect);

            // Calcutate the Pivot //
            Vector2 newPivot = Rect.PointToNormalized(transform.rect, localPointInRect);
            Vector2 deltaPivot = (transform.pivot - newPivot) * transform.localScale.x;
            Vector3 deltaPosition = new Vector3(deltaPivot.x * transform.sizeDelta.x, deltaPivot.y * transform.sizeDelta.y) * -1f;

            // Set the new Pivot //
            transform.pivot = newPivot;
            transform.localPosition += deltaPosition;

            // Apply the new scale
            transform.localScale = new Vector3(newScale, newScale, newScale);


        }
    }
}
