using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.NetworkMessages;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Components
{
    public class FrontShieldHealthComponent : HealthComponent
    {

        public PantheraObj ptraObj;

        public void onDamage(HealthComponent hc, DamageInfo damageInfo)
        {

            // Create the Effect //
            EffectData effectData = new EffectData
            {
                origin = damageInfo.position,
                rotation = Util.QuaternionSafeLookRotation(damageInfo.force != Vector3.zero ? damageInfo.force : UnityEngine.Random.onUnitSphere)
            };
            EffectManager.SpawnEffect(Assets.BlockEffectPrefab, effectData, true);
            EffectManager.SpawnEffect(Assets.FrontShieldHitFX, effectData, true);
            Utils.Sound.playSound(Utils.Sound.ShieldAbsorb, hc.gameObject);

            // Decrease the Shield //
            if (this.ptraObj.healthComponent.godMode == false)
                new ClientDamageShield(ptraObj.gameObject, damageInfo.damage).Send(NetworkDestination.Clients);

        }

    }
}
