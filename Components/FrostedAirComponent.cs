using Panthera.BodyComponents;
using Panthera.NetworkMessages;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using RoR2.Projectile;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.ParticleSystem;

namespace Panthera.Components
{
    public class FrostedAirComponent : MonoBehaviour
    {

        public PantheraObj ptraObj;
        public Collider collider;
        public EmissionModule emission;
        public float startTimer = 0;
        public Vector3 baseScale;
        public bool hadBuff = false;
        public float yPos = 0;

        public void Start()
        {
            base.gameObject.layer = LayerIndex.defaultLayer.intVal;
            this.collider = GetComponent<Collider>();
            this.emission = base.GetComponentInChildren<ParticleSystem>().emission;
            this.collider.enabled = false;
            this.emission.enabled = false;
            this.baseScale = base.transform.localScale;
            this.startTimer = Time.time;
        }

        public void FixedUpdate()
        {

            // Check if Frozen Paws Buff is active //
            bool hasFrozenPawsBuff = this.ptraObj.characterBody.HasBuff(Base.Buff.FrozenPawsBuff.buffIndex);

            // Enable/Disable the FrostedAir //
            if (hasFrozenPawsBuff == false && this.hadBuff == true)
            {
                this.hadBuff = false;
                this.collider.enabled = false;
                this.emission.enabled = false;
                this.yPos = 0;
            }
            else if (hasFrozenPawsBuff == true && this.hadBuff == false)
            {
                this.hadBuff = true;
                this.collider.enabled = true;
                this.emission.enabled = true;
                this.yPos = this.ptraObj.characterBody.footPosition.y;
            }

            // Check if too close of the ground //
            if (hasFrozenPawsBuff == true)
            {
                int layerMask = 1 << LayerIndex.world.intVal;
                if (Physics.Raycast(this.ptraObj.characterBody.corePosition, this.ptraObj.transform.TransformDirection(Vector3.down), PantheraConfig.FrozenPaws_minGroundDistance, layerMask) == true)
                    new ServerClearTimedBuffs(this.ptraObj.gameObject, Base.Buff.FrozenPawsBuff.index).Send(NetworkDestination.Server);
            }

            // Update Transform //
            try
            {
                float finalYPos = this.yPos == 0 ? this.ptraObj.characterBody.footPosition.y : this.yPos;
                base.transform.position = new Vector3(this.ptraObj.characterBody.footPosition.x, finalYPos, this.ptraObj.characterBody.footPosition.z);
                base.transform.rotation = Quaternion.Euler(new Vector3(base.transform.rotation.x, this.ptraObj.modelTransform.rotation.y, base.transform.rotation.z));
                base.transform.localScale = new Vector3(this.baseScale.x * this.ptraObj.modelScale, this.baseScale.y, this.baseScale.z * this.ptraObj.modelScale);
            }
            catch (Exception e)
            {
                // The Game ended //
                this.enabled = false;
            }

        }

    }
}
