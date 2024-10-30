using EntityStates;
using Panthera.Utils;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Panthera.MachineScripts
{
    public class DeathScript : MachineScript
    {

        public bool bodyMarkedForDestructionServer;
        public Transform cachedModelTransform;
        public bool isBrittle;
        public bool isVoidDeath;
        public bool isPlayerDeath;
        public CameraTargetParams.AimRequest aimRequest;
        public float fallingStopwatch;
        public float restStopwatch;
        public float fixedAge;
        public bool shouldAutoDestroy = true;
        public bool wasOnGround;
        public float onGroundTime;
        public int deathEffectID = 0;
        public GameObject modelObj;

        public override void Start()
        {
            // Get Values //
            this.bodyMarkedForDestructionServer = false;
            this.cachedModelTransform = (base.modelLocator ? base.modelLocator.modelTransform : null);
            this.isBrittle = (base.characterBody && base.characterBody.isGlass);
            this.isVoidDeath = (base.healthComponent && (base.healthComponent.killingDamageType & DamageType.VoidDeath) > DamageType.Generic);
            this.isPlayerDeath = (base.characterBody.master && base.characterBody.master.GetComponent<PlayerCharacterMasterController>() != null);
            this.modelObj = base.modelTransform.gameObject;

            // Disable the Character Direction //
            base.characterDirection.enabled = false;

            // Recalculate the Body Stats //
            base.characterBody.RecalculateStats();

            // Check if dead by Void //
            if (this.isVoidDeath)
            {
                if (base.characterBody && base.isAuthority)
                {
                    EffectManager.SpawnEffect(GenericCharacterDeath.voidDeathEffect, new EffectData
                    {
                        origin = base.characterBody.corePosition,
                        scale = base.characterBody.bestFitRadius
                    }, true);
                }
                if (this.cachedModelTransform)
                {
                    EntityState.Destroy(this.cachedModelTransform.gameObject);
                    this.cachedModelTransform = null;
                }
            }

            // Create the Death Effect //
            if (this.isPlayerDeath && base.characterBody)
            {
                UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/TemporaryVisualEffects/PlayerDeathEffect"), base.characterBody.corePosition, Quaternion.identity).GetComponent<LocalCameraEffect>().targetCharacter = base.characterBody.gameObject;
            }


            if (this.cachedModelTransform)
            {

                // I don't know what is this //
                if (this.isBrittle)
                {
                    TemporaryOverlay temporaryOverlay = this.cachedModelTransform.gameObject.AddComponent<TemporaryOverlay>();
                    temporaryOverlay.duration = 0.5f;
                    temporaryOverlay.destroyObjectOnEnd = true;
                    temporaryOverlay.originalMaterial = LegacyResourcesAPI.Load<Material>("Materials/matShatteredGlass");
                    temporaryOverlay.destroyEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/BrittleDeath");
                    temporaryOverlay.destroyEffectChildString = "Chest";
                    temporaryOverlay.inspectorCharacterModel = this.cachedModelTransform.gameObject.GetComponent<CharacterModel>();
                    temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
                    temporaryOverlay.animateShaderAlpha = true;
                }

                // Change Camera Parameters - Not used //
                if (base.cameraTargetParams)
                {
                    ChildLocator component = this.cachedModelTransform.GetComponent<ChildLocator>();
                    if (component)
                    {
                        Transform transform = component.FindChild("Chest");
                        if (transform)
                        {
                            base.cameraTargetParams.cameraPivotTransform = transform;
                            this.aimRequest = base.cameraTargetParams.RequestAimType(CameraTargetParams.AimType.Aura);
                            base.cameraTargetParams.dontRaycastToPivot = true;
                        }
                    }
                }

            }

            // Death Functions //
            if (!this.isVoidDeath)
            {
                this.PlayDeathSound();
                this.PlayDeathAnimation(0.1f);
                this.CreateDeathEffects();
            }

        }

        public override void Update()
        {
            
        }

        public override void FixedUpdate()
        {

            // Update the Fixed Age //
            this.fixedAge += Time.fixedDeltaTime;

            if (NetworkClient.active == true)
            {
                // Start the on Ground Timer
                if (base.characterMotor.isGrounded == true && this.wasOnGround == false)
                {
                    this.wasOnGround = true;
                    this.onGroundTime = Time.time;
                }

                // Create the Death Effect //
                if ((Time.time - this.onGroundTime) > PantheraConfig.Death_effectStartTime && this.deathEffectID == 0)
                {
                    Vector3 position = new Vector3(base.characterBody.corePosition.x - 1.2f, base.characterBody.corePosition.y, base.characterBody.corePosition.z + 0.8f);
                    Utils.Sound.playSound(Utils.Sound.Dead1, this.modelObj);
                    this.deathEffectID = Utils.FXManager.SpawnEffect(base.gameObject, Base.PantheraAssets.DeadFX, position, base.pantheraObj.modelScale, base.characterBody.gameObject, new Quaternion(), true);
                    CamHelper.ApplyAimType(CamHelper.AimType.Death, base.pantheraObj);
                }
            }
            

            if (NetworkServer.active)
            {

                // Create variables //
                bool isOnGround = false;
                bool motorRest = true;

                // Check if Character Motor //
                if (base.characterMotor)
                {
                    isOnGround = base.characterMotor.isGrounded;
                    motorRest = base.characterMotor.atRest;
                }

                // Panthera has no Rigid Body Motor //
                //else if (base.rigidbodyMotor)
                //{
                //    isOnGround = false;
                //    motorRest = false;
                //}

                // Get the Stopwatchs //
                this.fallingStopwatch = (isOnGround ? 0f : (this.fallingStopwatch + Time.fixedDeltaTime));
                this.restStopwatch = ((!motorRest) ? 0f : (this.restStopwatch + Time.fixedDeltaTime));

                // Check if the Body should be destroyed //
                if (this.fixedAge >= PantheraConfig.Death_minimumTimeBeforeDestroying)
                {
                    if (this.bodyMarkedForDestructionServer == true)
                    {
                        this.OnPreDestroyBodyServer();
                        GameObject.Destroy(base.gameObject);
                        return;
                    }
                    if ((this.restStopwatch >= GenericCharacterDeath.bodyPreservationDuration || this.fallingStopwatch >= GenericCharacterDeath.maxFallDuration || this.fixedAge > GenericCharacterDeath.hardCutoffDuration) && this.shouldAutoDestroy)
                    {
                        this.DestroyBodyAsapServer();
                        return;
                    }
                }
            }
        }

        public override void Stop()
        {
            // Stop the Effect //
            if (NetworkClient.active == true)
            {
                Utils.Sound.playSound(Utils.Sound.Dead2, this.modelObj);
                Utils.FXManager.DestroyEffect(this.deathEffectID);
            }
            // Dispose the Camera //
            CameraTargetParams.AimRequest aimRequest = this.aimRequest;
            if (aimRequest != null)
            {
                aimRequest.Dispose();
            }
            //if (this.shouldAutoDestroy && this.fallingStopwatch >= GenericCharacterDeath.maxFallDuration)
            // Destroy the Model //
            if (this.shouldAutoDestroy)
            {
                this.DestroyModel();
            }
        }

        public void PlayDeathSound()
        {
            if (base.sfxLocator && base.sfxLocator.deathSound != "")
            {
                Util.PlaySound(base.sfxLocator.deathSound, base.gameObject);
            }
        }

        public void PlayDeathAnimation(float crossfadeDuration = 0.1f)
        {
            Animator modelAnimator = base.modelAnimator;
            if (modelAnimator)
            {
                modelAnimator.CrossFadeInFixedTime("Death", crossfadeDuration);
            }
        }

        public void CreateDeathEffects()
        {

        }

        public void OnPreDestroyBodyServer()
        {

        }

        public void DestroyBodyAsapServer()
        {
            this.bodyMarkedForDestructionServer = true;
        }

        public void DestroyModel()
        {
            if (this.cachedModelTransform)
            {
                GameObject.Destroy(this.cachedModelTransform.gameObject);
                this.cachedModelTransform = null;
            }
        }

    }
}
