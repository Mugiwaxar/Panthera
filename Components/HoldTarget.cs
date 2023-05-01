using EntityStates;
using KinematicCharacterController;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.Machines;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Panthera.Components
{
    internal class HoldTarget : MonoBehaviour
    {

        public float startTime;
        public MachineScript skillScript;
        public CharacterBody body;
        public CharacterMotor motor;
        public CharacterDirection direction;
        public Transform modelTransform;
        public ModelLocator modelLocator;
        public KinematicCharacterMotor kinMotor;
        public Collider collider;
        public SetStateOnHurt stun;
        public LayerMask previousLayerMask;
        public PantheraObj ptraObj;
        public CharacterBody playerBody;
        public CharacterDirection playerDirection;
        public Transform playerModelTransform;
        public Transform playerMouthTransform;
        public float relativeDistance;
        public Vector3 relativeModelDistance;
        public Vector3 lastPosition;
        public bool destroying;
        public float startDestroyingTime;
        public bool wasElite;
        public bool isAlly;
        public int healFX;
        public float lastHealTime;

        public void Start()
        {

            // Find Components //
            this.body = base.GetComponent<CharacterBody>();
            this.motor = this.GetComponent<CharacterMotor>();
            this.direction = this.GetComponent<CharacterDirection>();
            this.modelLocator = this.GetComponent<ModelLocator>();
            this.kinMotor = base.GetComponent<KinematicCharacterMotor>();
            this.collider = base.GetComponent<Collider>();
            this.stun = base.GetComponent<SetStateOnHurt>();

            this.playerBody = this.ptraObj.characterBody;
            this.playerDirection = this.ptraObj.direction;
            this.playerModelTransform = this.ptraObj.modelTransform;
            this.playerMouthTransform = this.ptraObj.findModelChild("Mouth");

            // Set the Start Time //
            this.startTime = Time.time;

            // Tell the Server to attach the Component //
            if (Utils.Functions.IsMultiplayer() && this.ptraObj.hasAuthority() == true) new ServerAttachHoldTargetComp(this.ptraObj.gameObject, this.gameObject, this.relativeDistance, this.isAlly).Send(NetworkDestination.Server);

            // Check Elite //
            if (this.body != null && this.body.isElite)
            {
                // Save elite state //
                wasElite = this.body.isElite;

                // Set elite to false //
                this.body.isElite = false;
            }

            // Disable Direction //
            if (this.direction)
            {
                this.direction.enabled = false;
            }

            // Disable ModelLocator //
            if (this.modelLocator && this.modelLocator.modelTransform)
            {
                this.modelTransform = modelLocator.modelTransform;
                this.modelLocator.enabled = false;
            }

            // Disable the collider //
            if (this.collider != null)
            {
                this.collider.enabled = false;
            }

            // Change the layer //
            if (kinMotor != null)
            {
                this.previousLayerMask = kinMotor.CollidableLayers;
                kinMotor.CollidableLayers = LayerIndex.fakeActor.intVal;
            }

            // Calcule the Relative Core Distance //
            this.relativeModelDistance = this.modelTransform.position - this.transform.position;

            // Start the Heal FX //
            if (this.isAlly == true && Utils.Functions.IsServer() == false && this.ptraObj.hasAuthority() == true)
            {
                this.healFX = Utils.FXManager.SpawnEffect(base.gameObject, Base.Assets.LoopHealFX, this.body.corePosition, 1, base.gameObject, new Quaternion(), false, false);
            }

        }

        public void LateUpdate()
        {

            // Check the Target //
            if (base.gameObject == null || this.body == null || this.body.healthComponent == null || this.body.healthComponent.alive == false)
            {
                GameObject.Destroy(this);
                return;
            }

            // Check if must be destroyed //
            if (this.destroying == true)
            {
                // Check if this should be destroyed //
                float destroyingTime = Time.time - this.startDestroyingTime;
                if (destroyingTime > 1)
                    GameObject.DestroyImmediate(this);
            }

            // Stun the target //
            if (this.stun != null && this.stun.targetStateMachine != null && this.stun.targetStateMachine.state is StunState == false && NetworkServer.active == true)
            {
                this.stun.SetStun(0.5f);
            }

            // Active the Barrier //
            if (this.isAlly == true && NetworkServer.active == true)
            {
                this.body.healthComponent.barrier = this.body.maxBarrier;
            }

            // Heal Player //
            float lastHeal = Time.time - this.lastHealTime;
            if (this.isAlly == true && lastHeal > PantheraConfig.SaveMyFriend_healInterval && NetworkServer.active == true)
            {
                this.lastHealTime = Time.time;
                float healAmount = this.playerBody.maxHealth * PantheraConfig.SaveMyFriend_healPercent;
                body.healthComponent.Heal(healAmount, default(ProcChainMask));
                Utils.Sound.playSound(Utils.Sound.ZoneHeal, base.gameObject);
            }

            // Calcule the Position and rotation //
            Vector3 position = this.playerMouthTransform.position;
            if (this.destroying == true) position = this.lastPosition;
            Quaternion rotation = this.playerModelTransform.rotation;
            if (this.destroying == true) rotation = this.modelTransform.rotation;

            // Move the Motor //
            if (this.motor)
            {
                this.motor.disableAirControlUntilCollision = true;
                this.motor.velocity = Vector3.zero;
                this.motor.rootMotion = Vector3.zero;
            }

            // Move the Kinematic Motor or the Transform //
            if (this.kinMotor != null)
            {
                this.kinMotor.SetPosition(position);
            }
            else if (this.transform != null)
            {
                this.transform.position = position;
            }

            // Move the Model Transform //
            if (this.modelTransform)
            {
                this.modelTransform.position = position + this.relativeModelDistance;
                this.modelTransform.rotation = rotation;
            }

        }

        public void SetToDestroy()
        {
            // Check if the Target still exist //
            if (base.gameObject == null)
                return;
            // Set the Component as destroying //
            this.lastPosition = this.transform.position;
            if (Utils.Functions.IsMultiplayer() == true)
            {
                new ServerDetachHoldTargetComp(this.playerBody.gameObject, base.gameObject).Send(NetworkDestination.Server);
                new ServerHoldTargetLastPosition(base.gameObject, this.lastPosition).Send(NetworkDestination.Server);
            }
            this.startDestroyingTime = Time.time;
            this.destroying = true;
        }

        public void OnDestroy()
        {

            // Enable Model Locator //
            if (this.modelLocator) this.modelLocator.enabled = true;

            // Enable Direction //
            if (this.direction) this.direction.enabled = true;

            // Restore the layer //
            if (this.kinMotor != null)
            {
                this.kinMotor.CollidableLayers = this.previousLayerMask;
            }

            // Restore Elite //
            if (this.wasElite == true && this.body != null)
            {
                this.body.isElite = true;
            }

            // Enable the collider //
            if (this.collider != null)
            {
                this.collider.enabled = true;
            }

            // Stop the FX //
            if (this.isAlly == true && Utils.Functions.IsServer() == false && this.ptraObj.hasAuthority() == true)
            {
                Utils.FXManager.DestroyFX(this.healFX);
            }

        }

    }
}
