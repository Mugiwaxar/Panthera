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
    class LeapDown : MonoBehaviour
    {

        public CharacterMotor motor;
        public CharacterDirection direction;
        public Transform modelTransform;
        public ModelLocator modelLocator;
        public CharacterBody playerBody;
        public CharacterDirection playerDirection;
        public float reachDistance;
        public KinematicCharacterMotor kinMotor;
        public SetStateOnHurt stun;

        public void Start()
        {

            // Find Components //
            this.motor = this.GetComponent<CharacterMotor>();
            this.direction = this.GetComponent<CharacterDirection>();
            this.modelLocator = this.GetComponent<ModelLocator>();
            this.kinMotor = base.GetComponent<KinematicCharacterMotor>();
            this.stun = base.GetComponent<SetStateOnHurt>();

            // Disable Direction //
            if (this.direction) this.direction.enabled = false;

            // Disable ModelLocator //
            if (this.modelLocator && this.modelLocator.modelTransform)
            {
                this.modelTransform = modelLocator.modelTransform;
                this.modelLocator.enabled = false;
            }

        }

        public void Update()
        {

            // Stun the target //
            if (NetworkServer.active == true && this.stun != null && this.stun.targetStateMachine.state is StunState == false)
            {
                this.stun.SetStun(0.5f);
            }

            // Calcule the Position //
            Vector3 position = this.playerBody.corePosition + (this.playerDirection.forward * this.reachDistance);

            // Move the Motor //
            if (this.motor)
            {
                this.motor.disableAirControlUntilCollision = true;
                this.motor.velocity = Vector3.zero;
                this.motor.rootMotion = Vector3.zero;

                this.motor.Motor.SetPosition(position, true);
            }

            // Move the Kinematic Motor //
            if (this.kinMotor != null)
            {
                this.kinMotor.MoveCharacter(position);
            }

            // Move the Transform //
            this.transform.position = position;

            // Move the Model Transform //
            if (this.modelTransform)
            {
                this.modelTransform.position = position;
            }

            // Check if the Skill is still Active //
            MachineScript script = this.playerBody.GetComponent<PantheraSkillsMachine>()?.GetCurrentScript();
            if (NetworkClient.active == true && !(script is Skills.Leap))
            {
                GameObject.Destroy(this);
                //if (NetworkServer.active == false) new ServerDetachLeapDownComponent(this.gameObject, position).Send(NetworkDestination.Server);
            }

        }

        public void OnDestroy()
        {

            // Enable Model Locator //
            if (this.modelLocator) this.modelLocator.enabled = true;

            // Enable Direction //
            if (this.direction) this.direction.enabled = true;

        }

    }
}
