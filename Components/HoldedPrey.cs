using EntityStates;
using KinematicCharacterController;
using Panthera.NetworkMessages;
using Panthera.Skills;
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
    class HoldedPrey : MonoBehaviour
    {

        public float holdedPreyDistance;
        public float holdedPreyTime;
        public CharacterBody playerBody;
        public CharacterDirection playerDirection;
        public CharacterBody body;
        public CharacterMotor motor;
        public KinematicCharacterMotor controler;
        public CharacterDirection direction;
        public Transform transform;
        public Collider collider;
        public SetStateOnHurt stun;
        public LayerMask previousLayerMask;
        public Vector3 lastPosition = Vector3.zero;
        public Vector3 clientLastPosition = Vector3.zero;
        public bool released = false;

        public void Start()
        {
            // Save all needed variables //
            this.body = base.GetComponent<CharacterBody>();
            this.motor = base.GetComponent<CharacterMotor>();
            this.controler = base.GetComponent<KinematicCharacterMotor>();
            this.direction = base.GetComponent<CharacterDirection>();
            this.transform = this.body.gameObject.transform;
            this.collider = base.GetComponent<Collider>();
            this.stun = base.GetComponent<SetStateOnHurt>();

            // Change the layer //
            if (controler != null)
            {
                this.previousLayerMask = controler.CollidableLayers;
                controler.CollidableLayers = LayerIndex.fakeActor.intVal;
            }

            // Disable the collider //
            if (this.collider != null) this.collider.enabled = false;

            // Start the timer //
            this.holdedPreyTime = Time.time;
        }

        public void Update()
        {

            // Check the prey health //
            if (this.body != null && this.body.healthComponent == null || this.body.healthComponent.alive == false)
            {
                Destroy(this);
                return;
            }

            // Stun the prey //
            if (NetworkServer.active && this.stun != null && this.stun.targetStateMachine.state is StunState == false)
            {
                this.stun.SetStun(0.5f);
            }

            // Move the prey //
            Vector3 position = this.clientLastPosition;
            if (released == false) position = this.playerBody.corePosition + (this.playerDirection.forward * this.holdedPreyDistance);
            this.clientLastPosition = position;
            Quaternion direction = Quaternion.Euler(this.playerDirection.forward);
            if (this.motor != null)
            {
                this.motor.disableAirControlUntilCollision = true;
                this.motor.velocity = Vector3.zero;
                this.motor.rootMotion = Vector3.zero;
            }
            if (this.controler != null)
            {
                controler.SetPosition(position);
                if (this.direction != null) this.direction.forward = direction.eulerAngles;
            }
            else if (this.transform != null)
            {
                this.transform.localPosition = position;
                this.transform.localRotation = direction;
            }

            // Stop for timer //
            float totalDuration = Time.time - this.holdedPreyTime;
            if (totalDuration > PantheraConfig.Passive_maxPreyHoldingTime)
            {
                if (NetworkServer.active == true)
                {
                    Destroy(this);
                }
                else
                {
                    this.released = true;
                }
            }

        }

        public void OnDestroy()
        {
            // Set the last client position //
            if (NetworkServer.active == true && this.lastPosition != Vector3.zero)
            {
                if (this.controler != null)
                {
                    controler.SetPosition(lastPosition);
                }
                if (this.transform != null)
                {
                    this.transform.localPosition = lastPosition;
                }
            }

            // Restore the layer //
            if (this.controler != null)
            {
                this.controler.CollidableLayers = this.previousLayerMask;
            }

            // Enable the collider //
            if (this.collider != null)
            {
                this.collider.enabled = true;
            }

            // Remove the prey //
            playerBody.GetComponent<PantheraObj>().holdedPrey = null;

        }

    }

}
