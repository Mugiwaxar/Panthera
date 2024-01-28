using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.BodyComponents
{
    public class PantheraInteractor : Interactor
    {

        public PantheraObj ptraObj;

        public static GameObject FindBestInteractableObjectHook(Func<Interactor, Ray, float, Vector3, float, GameObject> orig, Interactor self, Ray raycastRay, float maxRaycastDistance, Vector3 overlapPosition, float overlapRadius)
        {

            PantheraInteractor interactor = self as PantheraInteractor;
            //if (interactor == null || interactor.ptraObj == null || interactor.ptraObj.detectionActivated == false) return orig(self, raycastRay, maxRaycastDistance, overlapPosition, overlapRadius);
            if (interactor == null || interactor.ptraObj == null) return orig(self, raycastRay, maxRaycastDistance, overlapPosition, overlapRadius);
            LayerMask interactable = LayerIndex.CommonMasks.interactable | (1 << PantheraConfig.Detection_layerIndex);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycastRay, out raycastHit, maxRaycastDistance, interactable, QueryTriggerInteraction.Collide))
            {
                GameObject entity = EntityLocator.GetEntity(raycastHit.collider.gameObject);
                if (entity)
                {
                    IInteractable component = entity.GetComponent<IInteractable>();
                    if (component != null && ((MonoBehaviour)component).isActiveAndEnabled && component.GetInteractability(self) != Interactability.Disabled)
                    {
                        return entity;
                    }
                }
            }
            Collider[] array = Physics.OverlapSphere(overlapPosition, overlapRadius, interactable, QueryTriggerInteraction.Collide);
            int num = array.Length;
            GameObject result = null;
            float num2 = 0f;
            for (int i = 0; i < num; i++)
            {
                Collider collider = array[i];
                GameObject entity2 = EntityLocator.GetEntity(collider.gameObject);
                if (entity2)
                {
                    IInteractable component2 = entity2.GetComponent<IInteractable>();
                    if (component2 != null && ((MonoBehaviour)component2).isActiveAndEnabled && component2.GetInteractability(self) != Interactability.Disabled && !component2.ShouldIgnoreSpherecastForInteractibility(self))
                    {
                        float num3 = Vector3.Dot((collider.transform.position - overlapPosition).normalized, raycastRay.direction);
                        if (num3 > num2)
                        {
                            num2 = num3;
                            result = entity2.gameObject;
                        }
                    }
                }
            }
            return result;

        }

    }
}
