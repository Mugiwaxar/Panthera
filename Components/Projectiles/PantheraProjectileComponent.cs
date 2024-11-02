using Panthera.BodyComponents;
using Panthera.Utils;
using RoR2;
using RoR2.Projectile;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Panthera.Components.Projectiles
{
    public class PantheraProjectileComponent : MonoBehaviour //, IProjectileImpactBehavior
    {

        public PantheraObj ptraObj;
        public ProjectileController controller;
        public ProjectileDamage projectileDamage;
        public GameObject impactEffect;
        public string impactSound;
        public string projectileName;
        public bool destroyOnHit = true;
        public bool destroyOnWorld = true;
        public bool alive = true;
        public bool fired = false;
        public List<GameObject> enemiesHit = new List<GameObject>();

        public void SetupProjectile(PantheraProjectileComponent ptraProjComp, string name, string impactSound = "", GameObject impactEffect = null, bool destroyOnHit = true, bool destroyOnWorld = true)
        {
            // Set up Panthera Projectile Component //
            ptraProjComp.projectileName = name;
            ptraProjComp.impactSound = impactSound;
            ptraProjComp.impactEffect = impactEffect;
            ptraProjComp.destroyOnHit = destroyOnHit;
            ptraProjComp.destroyOnWorld = destroyOnWorld;
        }

        public void Start()
        {
            this.controller = GetComponent<ProjectileController>();
            this.projectileDamage = GetComponent<ProjectileDamage>();
            this.ptraObj = controller?.owner?.GetComponent<PantheraObj>();
        }

        public void OnTriggerEnter(Collider collider)
        {

            // Check if the Server is active //
            if (NetworkServer.active == false)
                return;

            // Create the Projectile Impact Info //
            Vector3 vector = Vector3.zero;
            ProjectileImpactInfo impactInfo = new ProjectileImpactInfo
            {
                collider = collider,
                estimatedPointOfImpact = base.transform.position,
                estimatedImpactNormal = -vector.normalized
            };

            // Call the Fonction //
            this.EnterCollision(impactInfo);

        }

        public void OnTriggerExit(Collider collider)
        {

            // Check if the Server is active //
            if (NetworkServer.active == false)
                return;

            // Create the Projectile Impact Info //
            Vector3 vector = Vector3.zero;
            ProjectileImpactInfo impactInfo = new ProjectileImpactInfo
            {
                collider = collider,
                estimatedPointOfImpact = base.transform.position,
                estimatedImpactNormal = -vector.normalized
            };

            // Call the Fonction //
            this.LeaveCollision(impactInfo);

        }

        public void OnTriggerStay(Collider collider)
        {

            // Check if the Server is active //
            if (NetworkServer.active == false)
                return;

            // Create the Projectile Impact Info //
            Vector3 vector = Vector3.zero;
            ProjectileImpactInfo impactInfo = new ProjectileImpactInfo
            {
                collider = collider,
                estimatedPointOfImpact = base.transform.position,
                estimatedImpactNormal = -vector.normalized
            };

            // Call the Fonction //
            this.StayCollision(impactInfo);

        }

        public void EnterCollision(ProjectileImpactInfo impactInfo)
        {

            // Check if still Alive and destroy if needed //
            if (this.alive == false)
            {
                GameObject.Destroy(base.gameObject);
                return;
            }

            // Check the Panthera Object //
            if (this.ptraObj == null) return;

            // Check if the Projectile has already fired //
            if (this.fired == true) return;

            // Get the Collider //
            Collider collider = impactInfo.collider;

            // Create the HurtBox //
            HurtBox hurtBox = null;

            // Create the Health Component //
            HealthComponent healthComponent = null;

            // Check the Collider
            if (collider != null)
            {

                // Get the HurtBox //
                hurtBox = collider.GetComponent<HurtBox>();

                // Check the HurtBox //
                if (hurtBox != null)
                {

                    // Get the Health Component //
                    healthComponent = hurtBox.healthComponent;

                    // Check the Health Component //
                    if (healthComponent == null || healthComponent.gameObject == this.controller.owner)
                        return;

                    // Check if the Enemy was not already Hit //
                    if (this.enemiesHit.Contains(healthComponent.gameObject))
                        return;
                    else
                        this.enemiesHit.Add(healthComponent.gameObject);

                    // Check if the hit can process //
                    if (FriendlyFireManager.ShouldDirectHitProceed(healthComponent, this.controller.teamFilter.teamIndex) == true)
                    {
                        if (this.impactSound != null)
                            Sound.playSound(this.impactSound, base.gameObject, false);
                        if (this.impactEffect != null)
                            FXManager.SpawnEffect(this.ptraObj.gameObject, this.impactEffect, impactInfo.estimatedPointOfImpact, 1, null, new Quaternion(), false, false);
                    }
                    else
                    {
                        return;
                    }

                }
                // Destroy on World //
                else if (this.destroyOnWorld)
                {
                    this.alive = false;
                }
                    
            }

            // Check the Projectile //
            if (this.alive == false)
            {
                Destroy(gameObject);
                return;
            }
            else if (collider != null && hurtBox != null && healthComponent != null)
            {
                // Validate the Impact //
                this.OnImpactValidated(impactInfo, collider, hurtBox, healthComponent);
                // Destroy the Projectile //
                if (this.destroyOnHit == true)
                {
                    this.fired = true;
                    this.alive = false;
                    Destroy(base.gameObject);
                }
                    
            }

            // I don't know what this do ?? //
            //damageInfo.position = transform.position;
            //if (NetworkServer.active)
            //{
            //    GlobalEventManager.instance.OnHitAll(damageInfo, collider.gameObject);
            //}

        }

        public void LeaveCollision(ProjectileImpactInfo impactInfo)
        {


            // Check if still Alive and destroy if needed //
            if (this.alive == false)
            {
                GameObject.Destroy(base.gameObject);
                return;
            }

            // Check the Panthera Object //
            if (this.ptraObj == null) return;

            // Get the Health Component //
            HealthComponent healthComponent = impactInfo.collider?.GetComponent<HurtBox>()?.healthComponent;

            // Check the Health Component //
            if (healthComponent != null)
                this.OnLeaveValidated(impactInfo, impactInfo.collider, impactInfo.collider.GetComponent<HurtBox>(), healthComponent);

        }

        public void StayCollision(ProjectileImpactInfo impactInfo)
        {

            // Check if still Alive and destroy if needed //
            if (this.alive == false)
            {
                GameObject.Destroy(base.gameObject);
                return;
            }

            // Check the Panthera Object //
            if (this.ptraObj == null) return;

            // Get the Health Component //
            HealthComponent healthComponent = impactInfo.collider?.GetComponent<HurtBox>()?.healthComponent;

            // Check the Health Component //
            if (healthComponent != null)
                this.OnStayValidated(impactInfo, impactInfo.collider, impactInfo.collider.GetComponent<HurtBox>(), healthComponent);

        }

        public virtual void OnImpactValidated(ProjectileImpactInfo impactInfo, Collider collider, HurtBox hurtBox, HealthComponent healthComponent)
        {

        }

        public virtual void OnLeaveValidated(ProjectileImpactInfo impactInfo, Collider collider, HurtBox hurtBox, HealthComponent healthComponent)
        {

        }

        public virtual void OnStayValidated(ProjectileImpactInfo impactInfo, Collider collider, HurtBox hurtBox, HealthComponent healthComponent)
        {

        }

    }
}
