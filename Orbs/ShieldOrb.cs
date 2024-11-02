using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.NetworkMessages;
using R2API.Networking.Interfaces;
using RoR2;
using RoR2.Orbs;
using UnityEngine;

namespace Panthera.Orbs
{
    internal class ShieldOrb : Orb
    {

        public float shieldValue = 0;

        public override void Begin()
        {
            // Check the Target //
            if (this.target == null) return;

            // Get the Body //
            PantheraBody body = this.target.healthComponent.GetComponent<PantheraBody>();

            // Check the Body //
            if (body == null) return;
            
            // Set the Orb Duration //
            base.duration = PantheraConfig.ShieldOrb_Duration;

            // Calcul the Scale //
            float percent = body.frontShield / body.maxFrontShield;
            float scale = Mathf.Min(percent, 1f);

            // Create the Effect Data //
            EffectData effectData = new EffectData
            {
                scale = scale,
                origin = this.origin,
                genericFloat = base.duration
            };
            effectData.SetHurtBoxReference(this.target);

            // Spawn the Effect //
            EffectManager.SpawnEffect(PantheraAssets.ShieldOrbFX, effectData, true);

        }

        public override void OnArrival()
        {

            // Check the Target //
            if (this.target)
            {
                // Add Shield //
                new ClientAddShield(this.target.healthComponent.gameObject, this.shieldValue).Send(R2API.Networking.NetworkDestination.Clients);
            }

        }

    }
}
