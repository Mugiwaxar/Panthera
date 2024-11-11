using Panthera.Base;
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
                damageInfo.procCoefficient = PantheraConfig.AirCleave_procCoefficient;
                damageInfo.damageColorIndex = base.projectileDamage.damageColorIndex;
                damageInfo.damageType = base.projectileDamage.damageType;
            }

            // Damage the Target //
            if (NetworkServer.active)
            {

                // Do the Damages //
                damageInfo.ModifyDamageInfo(hurtBox.damageModifier);
                healthComponent.TakeDamage(damageInfo);
                GlobalEventManager.instance.OnHitEnemy(damageInfo, hurtBox.healthComponent.gameObject);

                // Set the last Hit  //
                healthComponent.GetComponent<PredatorComponent>().lastHit = base.ptraObj;

                // Check if Fire Air Cleave //
                if (base.gameObject.name.Contains("Fire"))
                {
                    float fireDamage = damageInfo.damage * PantheraConfig.InfernalSwipe_damagePercent2;
                    new ServerInflictDamage(base.gameObject, healthComponent.gameObject, healthComponent.transform.position, fireDamage, damageInfo.crit, damageInfo.procCoefficient, DamageType.Generic, DamageColorIndex.WeakPoint).Send(NetworkDestination.Server);
                    float ignitionChance = PantheraConfig.InfernalSwipe_ingnitionChance2 * 100;
                    float ignitionRand = UnityEngine.Random.Range(0, 100);
                    if (ignitionRand < ignitionChance)
                        new ServerAddBuff(base.gameObject, healthComponent.gameObject, Buff.IgnitionDebuff).Send(NetworkDestination.Server);
                }
                // Move the target //
                new ServerApplyForceToBody(healthComponent.gameObject, base.transform.forward * PantheraConfig.AirCleave_pushForce).Send(NetworkDestination.Server);

            }

            // Get the Team Component //
            TeamComponent tc = healthComponent.body?.teamComponent;

            // Check the Team Component //
            if (tc != null && tc.teamIndex == TeamIndex.Monster)
            {

                // Add Fury //
                if (base.ptraObj.getAbilityLevel(PantheraConfig.Fury_AbilityID) > 0)
                    new ClientAddFury(base.ptraObj.gameObject, PantheraConfig.AirCleave_furyAdded).Send(NetworkDestination.Clients);

                // Add the Convergence Hook Component //
                if (base.ptraObj.getAbilityLevel(PantheraConfig.ConvergenceHook_AbilityID) > 0 && tc.body.isBoss == false)
                {
                    if (NetworkClient.active == false)
                    {
                        ConvergenceHookComp comp = tc.body.gameObject.AddComponent<ConvergenceHookComp>();
                        comp.ptraObj = base.ptraObj;
                    }
                    new ClientAddConvergenceHookComp(base.ptraObj.gameObject, tc.body.gameObject).Send(NetworkDestination.Clients);
                }

            }

        }

    }
}
