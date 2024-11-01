using Panthera.Base;
using Panthera.NetworkMessages;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using RoR2.Projectile;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.ParticleSystem.PlaybackState;

namespace Panthera.Components.Projectiles
{
    public class AirSlashProjectile : PantheraProjectileComponent
    {
        public override void OnImpactValidated(ProjectileImpactInfo impactInfo, Collider collider, HurtBox hurtBox, HealthComponent healthComponent)
        {

            // Create the Damage Info //
            DamageInfo damageInfo = new DamageInfo();
            if (base.projectileDamage != null)
            {
                damageInfo.damage = base.projectileDamage.damage;
                damageInfo.crit = base.projectileDamage.crit;
                damageInfo.attacker = base.controller.owner;
                damageInfo.inflictor = base.gameObject;
                damageInfo.position = impactInfo.estimatedPointOfImpact;
                damageInfo.force = base.projectileDamage.force * base.transform.forward;
                damageInfo.procChainMask = base.controller.procChainMask;
                damageInfo.procCoefficient = PantheraConfig.AirSlash_procCoefficient;
                damageInfo.damageColorIndex = base.projectileDamage.damageColorIndex;
                damageInfo.damageType = base.projectileDamage.damageType;
            }

            // Damage the Target //
            if (NetworkServer.active)
            {
                damageInfo.ModifyDamageInfo(hurtBox.damageModifier);
                healthComponent.TakeDamage(damageInfo);
                GlobalEventManager.instance.OnHitEnemy(damageInfo, hurtBox.healthComponent.gameObject);
            }

            // Get the Team Component //
            TeamComponent tc = healthComponent.body?.teamComponent;

            // Check the Team Component //
            if (tc != null && tc.teamIndex == TeamIndex.Monster)
            {
                // Add Fury //
                if (ptraObj.getAbilityLevel(PantheraConfig.Fury_AbilityID) > 0)
                    new ClientAddFury(ptraObj.gameObject, PantheraConfig.AirSlash_furyAdded).Send(NetworkDestination.Clients);

                // Add the Massive Hook Component //
                if (ptraObj.getAbilityLevel(PantheraConfig.MassiveHook_AbilityID) > 0 && tc.body.isBoss == false)
                {
                    if (NetworkClient.active ==  false)
                    {
                        ConvergenceHookComp comp = tc.body.gameObject.AddComponent<ConvergenceHookComp>();
                        comp.ptraObj = ptraObj;
                        comp.massive = true;
                    }
                    new ClientAddConvergenceHookComp(ptraObj.gameObject, tc.body.gameObject, true).Send(NetworkDestination.Clients);
                }

            }

        }

    }
}
