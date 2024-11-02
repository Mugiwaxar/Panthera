using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.NetworkMessages;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using UnityEngine;

namespace Panthera.Components
{
    public class FrontShieldHealthComponent : HealthComponent
    {

        public PantheraObj ptraObj;

        public void onDamage(DamageInfo damageInfo)
        {

            // Create the Effect //
            EffectData effectData = new EffectData
            {
                origin = damageInfo.position,
                rotation = Util.QuaternionSafeLookRotation(damageInfo.force != Vector3.zero ? damageInfo.force : UnityEngine.Random.onUnitSphere)
            };
            EffectManager.SpawnEffect(PantheraAssets.AbsorbEffectPrefab, effectData, true);
            EffectManager.SpawnEffect(PantheraAssets.FrontShieldHitFX, effectData, true);

            // Decrease the Shield //
            if (this.ptraObj.healthComponent.godMode == false)
                new ClientDamageShield(ptraObj.gameObject, damageInfo.damage).Send(NetworkDestination.Clients);

        }

    }
}
