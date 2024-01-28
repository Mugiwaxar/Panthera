using Panthera.BodyComponents;
using Rewired.Utils;
using RoR2;
using RoR2.Projectile;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Components
{
    public class ConvergenceHookComp : MonoBehaviour
    {

        public PantheraObj ptraObj;
        public float startTime;
        public bool massive = false;
        public bool activated = false;
        public bool stunDone = false;

        public void Start()
        {
            // Clear the Component List if not massive //
            if (this.massive == false)
            {
                foreach (ConvergenceHookComp comp in this.ptraObj.convergenceCompsList)
                {
                    GameObject.Destroy(comp);
                }
                this.ptraObj.convergenceCompsList.Clear();
            }
            // Add this one //
            this.ptraObj.convergenceCompsList.Add(this);
            // Save the Time //
            this.startTime = Time.time;
        }

        public void FixedUpdate()
        {
            // Check if the Component must be destroyed //
            if (Time.time - this.startTime > PantheraConfig.ConvergenceHook_compDuration)
            {
                GameObject.Destroy(this);
                return;
            }
            // Check if the Enemy should be pulled //
            if (this.activated == true)
            {
                // Stun the Target //
                if (this.stunDone == false)
                {
                    this.stunDone = true;
                    SetStateOnHurt setState = this.GetComponent<SetStateOnHurt>();
                    if (setState == null) return;
                    setState.SetStun(PantheraConfig.ConvergenceHook_stunDuration);
                }
                // Stop if the Distance is reached //
                if (Vector3.Distance(this.ptraObj.characterBody.aimOrigin, this.GetComponent<Collider>().ClosestPoint(this.ptraObj.characterBody.aimOrigin)) < PantheraConfig.ConvergenceHook_StopDistance)
                {
                    GameObject.Destroy(this);
                    return;
                }
                // Get the Motor Component //
                IDisplacementReceiver component = this.gameObject.GetComponent<IDisplacementReceiver>();
                if ((Component)component)
                {
                    // Move the Enemy //
                    Vector3 move = this.ptraObj.characterBody.aimOrigin - this.transform.position;
                    component.AddDisplacement(move.normalized * PantheraConfig.ConvergenceHook_hookSpeed);
                }
            }
        }

    }
}
