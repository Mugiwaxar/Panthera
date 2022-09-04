using EntityStates;
using KinematicCharacterController;
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
        public float relativeDistance;
        public Vector3 LastPosition;

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

            // Tell the Server to attach the Component //
            if (NetworkServer.active == false) new ServerAttachHoldTargetComp(this.ptraObj.gameObject, this.gameObject, this.relativeDistance).Send(NetworkDestination.Server);

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

        }

        public void Update()
        {

            // Check the prey health //
            if (this.body != null && this.body.healthComponent == null || this.body.healthComponent.alive == false)
            {
                return;
            }

            // Stun the target //
            if (NetworkServer.active == true && this.stun != null && this.stun.targetStateMachine.state is StunState == false)
            {
                this.stun.SetStun(0.5f);
            }

            // Calcule the Position and rotation //
            Vector3 position = this.playerBody.corePosition + (this.playerDirection.forward * this.relativeDistance);
            Quaternion direction = this.playerModelTransform.rotation;

            // Move the Motor //
            if (this.motor)
            {
                this.motor.disableAirControlUntilCollision = true;
                this.motor.velocity = Vector3.zero;
                this.motor.rootMotion = Vector3.zero;
            }

            // Move the Kinematic Motor //
            if (this.kinMotor != null)
            {
                this.kinMotor.MoveCharacter(position);
                this.kinMotor.RotateCharacter(direction);
            }

            // Rotate the Character Direction //
            if (this.direction != null)
            {
                this.direction.forward = direction.eulerAngles;
            }

            // Move the Transform //
            if (this.transform != null)
            {
                this.transform.position = position;
                this.transform.rotation = direction;
            }
            

            // Move the Model Transform //
            if (this.modelTransform)
            {
                this.modelTransform.position = position;
                this.modelTransform.rotation = direction;
            }

            // Check if the Skill is still Active //
            MachineScript script = this.playerBody.GetComponent<PantheraSkillsMachine>()?.GetCurrentScript();
            if (NetworkClient.active == true && (script == null || script != this.skillScript))
            {

                GameObject.Destroy(this);
                if (NetworkServer.active == false)
                {
                    this.LastPosition = position;
                    new ServerDetachHoldTargetComp(base.gameObject).Send(NetworkDestination.Server);
                }
            }

        }

        public void OnDestroy()
        {

            // Tell the Server the last Position //
            if(NetworkClient.active == true && NetworkServer.active == false)
                new ServerHoldTargetLastPosition(base.gameObject, this.LastPosition).Send(NetworkDestination.Server);

            // Enable Model Locator //
            if (this.modelLocator) this.modelLocator.enabled = true;

            // Enable Direction //
            if (this.direction) this.direction.enabled = true;

            // Restore the layer //
            if (this.kinMotor != null)
            {
                this.kinMotor.CollidableLayers = this.previousLayerMask;
            }

            // Enable the collider //
            if (this.collider != null)
            {
                this.collider.enabled = true;
            }

        }

    }
}
