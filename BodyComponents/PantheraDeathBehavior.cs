using Panthera;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.Machines;
using Panthera.MachineScripts;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.BodyComponents
{
    public class PantheraDeathBehavior : CharacterDeathBehavior
    {

        public PantheraObj ptraObj;
        public PantheraMachine deathMachine;

        public static void CharacterDeathBehaviorHook(Action<CharacterDeathBehavior> orig, CharacterDeathBehavior self)
        {

            // Check if Panthera //
            PantheraDeathBehavior deathBehavior = self as PantheraDeathBehavior;
            if (deathBehavior == null)
            {
                orig(self);
                return;
            }

            deathBehavior.OnDeath();

        }

        public void OnDeath()
        {

            // Stop all Scripts //
            ptraObj.stopAllScripts();

            // Start the death Machine //
            deathMachine.enabled = true;
            deathMachine.SetScript(new DeathScript());

            //if (Util.HasEffectiveAuthority(gameObject))
            //{
            //    deathMachine.SetScript(new DeathScript());
            //    //if (this.deathStateMachine)
            //    //{
            //    //    this.deathStateMachine.SetNextState(EntityStateCatalog.InstantiateState(this.deathState));
            //    //}
            //    //EntityStateMachine[] array = this.idleStateMachine;
            //    //for (int i = 0; i < array.Length; i++)
            //    //{
            //    //    array[i].SetNextState(new Idle());
            //    //}
            //}
            //else
            //{
            //    // Delete Panthera //
            //    Destroy(ptraObj?.gameObject);
            //    Destroy(ptraObj?.modelTransform?.gameObject);
            //}

            // Change the Player Layer //
            gameObject.layer = LayerIndex.debris.intVal;
            CharacterMotor component = GetComponent<CharacterMotor>();
            if (component)
            {
                component.Motor.RebuildCollidableLayers();
            }

            // Set Character ILifeBehavior? to Death //
            ILifeBehavior[] components = GetComponents<ILifeBehavior>();
            for (int i = 0; i < components.Length; i++)
            {
                components[i].OnDeathStart();
            }

            // Set Model ILifeBehavior? to Death //
            ModelLocator component2 = GetComponent<ModelLocator>();
            if (component2)
            {
                Transform modelTransform = component2.modelTransform;
                if (modelTransform)
                {
                    components = modelTransform.GetComponents<ILifeBehavior>();
                    for (int i = 0; i < components.Length; i++)
                    {
                        components[i].OnDeathStart();
                    }
                }
            }

        }

    }
}
