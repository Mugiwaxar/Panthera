using KinematicCharacterController;
using Panthera;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Base
{
    public class PantheraKinematicSystem : KinematicCharacterSystem
    {

        public static void SimulateHook1(Action<float> orig, float deltaTime)
        {
            for (int i = 0; i < PhysicsMovers.Count; i++)
            {
                PhysicsMovers[i].VelocityUpdate(deltaTime);
            }
            for (int j = 0; j < CharacterMotors.Count; j++)
            {
                if (CharacterMotors[j] is PantheraKinematicMotor)
                {
                    PantheraKinematicMotor motor = (PantheraKinematicMotor)CharacterMotors[j];
                    motor.UpdatePhase1(deltaTime);
                }
                else
                {
                    CharacterMotors[j].UpdatePhase1(deltaTime);
                }

            }
            for (int k = 0; k < PhysicsMovers.Count; k++)
            {
                PhysicsMovers[k].Transform.SetPositionAndRotation(PhysicsMovers[k].TransientPosition, PhysicsMovers[k].TransientRotation);
                PhysicsMovers[k].Rigidbody.position = PhysicsMovers[k].TransientPosition;
                PhysicsMovers[k].Rigidbody.rotation = PhysicsMovers[k].TransientRotation;
            }
            for (int l = 0; l < CharacterMotors.Count; l++)
            {
                if (CharacterMotors[l] is PantheraKinematicMotor)
                {
                    PantheraKinematicMotor motor = (PantheraKinematicMotor)CharacterMotors[l];
                    motor.UpdatePhase2(deltaTime);
                }
                else
                {
                    CharacterMotors[l].UpdatePhase2(deltaTime);
                }
                CharacterMotors[l].Transform.SetPositionAndRotation(CharacterMotors[l].TransientPosition, CharacterMotors[l].TransientRotation);
                CharacterMotors[l].Rigidbody.position = CharacterMotors[l].TransientPosition;
                CharacterMotors[l].Rigidbody.rotation = CharacterMotors[l].TransientRotation;
            }
        }

        public static void SimulateHook2(Action<float, KinematicCharacterMotor[], int, PhysicsMover[], int> orig, float deltaTime, KinematicCharacterMotor[] motors, int characterMotorsCount, PhysicsMover[] movers, int physicsMoversCount)
        {
            for (int i = 0; i < physicsMoversCount; i++)
            {
                movers[i].VelocityUpdate(deltaTime);
            }
            for (int j = 0; j < characterMotorsCount; j++)
            {
                if (CharacterMotors[j] is PantheraKinematicMotor)
                {
                    PantheraKinematicMotor motor = (PantheraKinematicMotor)CharacterMotors[j];
                    motor.UpdatePhase1(deltaTime);
                }
                else
                {
                    CharacterMotors[j].UpdatePhase1(deltaTime);
                }
            }
            for (int k = 0; k < physicsMoversCount; k++)
            {
                movers[k].Transform.SetPositionAndRotation(movers[k].TransientPosition, movers[k].TransientRotation);
                movers[k].Rigidbody.position = movers[k].TransientPosition;
                movers[k].Rigidbody.rotation = movers[k].TransientRotation;
            }
            for (int l = 0; l < characterMotorsCount; l++)
            {
                if (CharacterMotors[l] is PantheraKinematicMotor)
                {
                    PantheraKinematicMotor motor = (PantheraKinematicMotor)CharacterMotors[l];
                    motor.UpdatePhase2(deltaTime);
                }
                else
                {
                    CharacterMotors[l].UpdatePhase2(deltaTime);
                }
                motors[l].Transform.SetPositionAndRotation(motors[l].TransientPosition, motors[l].TransientRotation);
                motors[l].Rigidbody.position = motors[l].TransientPosition;
                motors[l].Rigidbody.rotation = motors[l].TransientRotation;
            }
        }

    }
}
