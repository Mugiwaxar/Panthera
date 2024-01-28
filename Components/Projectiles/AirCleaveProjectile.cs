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

namespace Panthera.Components.Projectiles
{
    public class AirCleaveProjectile : PantheraProjectileComponent
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
                damageInfo.procCoefficient = base.controller.procCoefficient;
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
                    new ClientAddFury(ptraObj.gameObject, PantheraConfig.AirCleave_furyAdded).Send(NetworkDestination.Clients);

                // Add the Convergence Hook Component //
                if (ptraObj.getAbilityLevel(PantheraConfig.ConvergenceHook_AbilityID) > 0 && tc.body.isBoss == false)
                {
                    if (NetworkClient.active == false)
                    {
                        ConvergenceHookComp comp = tc.body.gameObject.AddComponent<ConvergenceHookComp>();
                        comp.ptraObj = ptraObj;
                    }
                    new ClientAddConvergenceHookComp(ptraObj.gameObject, tc.body.gameObject).Send(NetworkDestination.Clients);
                }

            }

        }

    }
}
