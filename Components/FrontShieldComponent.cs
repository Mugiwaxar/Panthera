using Panthera.BodyComponents;
using RoR2;
using RoR2.Projectile;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

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
            // Update the Shield position //
            base.transform.position = ptraObj.pantheraInputBank.aimOrigin + (ptraObj.direction.forward * PantheraConfig.FrontShield_addedforwardMultiplier);
            base.transform.position = new Vector3 (base.transform.position.x, base.transform.position.y + PantheraConfig.FrontShield_addedUpPosition, base.transform.position.z);
            base.transform.rotation = Util.QuaternionSafeLookRotation(Camera.main.transform.forward);
        }

        public void OnDestroy()
        {

        }

    }
}
