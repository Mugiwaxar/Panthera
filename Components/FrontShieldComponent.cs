using Panthera.BodyComponents;
using RoR2;
using RoR2.Projectile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using static RoR2.BlastAttack;

namespace Panthera.Components
{
    public class FrontShieldComponent : MonoBehaviour
    {

        public PantheraObj ptraObj;

        public void Start()
        {
            // Set the Shield disabled at the start //
            this.gameObject.SetActive(false);
        }

        public void FixedUpdate()
        {
            // Check if the Shield is Deployed //
            if (this.ptraObj == null || this.ptraObj.frontShieldDeployed == true) return;
            // Update the Shield Transform //
            base.transform.position = ptraObj.pantheraInputBank.aimOrigin + (ptraObj.direction.forward * PantheraConfig.FrontShield_addedforwardMultiplier);
            base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + PantheraConfig.FrontShield_addedUpPosition, base.transform.position.z);
            //if (this.ptraObj.hasAuthority() == true)
            //    base.transform.rotation = Util.QuaternionSafeLookRotation(Camera.main.transform.forward);
            //else
            base.transform.rotation = Util.QuaternionSafeLookRotation(this.ptraObj.pantheraInputBank.aimDirection);
        }

        public void OnDestroy()
        {

        }

        public static BlastAttack.HitPoint[] OnBlastAttack(Func<RoR2.BlastAttack, BlastAttack.HitPoint[]> orig, RoR2.BlastAttack self)
        {

            // Call the Orig Function //
            BlastAttack.HitPoint[] hitPoints = orig(self);

            // Create the HitPoints buffer List //
            List<BlastAttack.HitPoint> hitPointsBuffer = new List<BlastAttack.HitPoint>();

            // Check the HitPoints List //
            foreach (BlastAttack.HitPoint hitPoint in hitPoints)
            {
                // Collect all Data //
                Vector3 attackPos = self.position;
                Vector3 damagePos = hitPoint.hitPosition;
                HurtBox hurtBox = hitPoint.hurtBox;
                bool addToList = true;

                // Check the HurtBox //
                if (hurtBox != null && hurtBox.healthComponent != null && hurtBox.healthComponent is not FrontShieldHealthComponent && hurtBox.collider != null)
                {

                    // Get the Layer Mask //
                    int worldMask = 1 << LayerIndex.world.intVal;

                    // Get the direction and the distance //
                    Vector3 direction = damagePos - attackPos;
                    float distance = self.radius + 1;

                    // Do the Raycast to detect the Front Shield //
                    RaycastHit[] hits = Physics.RaycastAll(attackPos, direction, distance, worldMask, QueryTriggerInteraction.Collide);

                    // Check all Objects //
                    foreach (RaycastHit hit in hits)
                    {

                        // Check if this is a Front Shield //
                        if (hit.collider != null && hit.collider.gameObject.name == PantheraConfig.FrontShield_worldHitboxName)
                        {
                            // Get and check the Front Shield //
                            GameObject frontShieldObj = hit.collider.gameObject.transform.parent.gameObject;
                            if (frontShieldObj != null && frontShieldObj.active == true)
                            {
                                // Don't add this HitPoint to the List //
                                addToList = false;
                            }

                        }
                    }

                }

                // Add the HitPoint to the List //
                if (addToList == true)
                {
                    hitPointsBuffer.Add(hitPoint);
                }

            }

            // Return the List //
            return hitPointsBuffer.ToArray();

        }

    }
}
