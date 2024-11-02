using Panthera.BodyComponents;
using Panthera.MachineScripts;
using UnityEngine;

namespace Panthera.Machines
{
    public class PantheraMachine : MonoBehaviour
    {
        public new string name;
        public MachineScript currentScript;
        public MachineScript nextScript;

        public GameObject player;
        public PantheraObj ptraObj;

        public virtual void Start()
        {
            this.player = this.gameObject;
            this.ptraObj = player.GetComponent<PantheraObj>();
        }

        public virtual void Update()
        {
            // Update the script machine //
            if (currentScript != null && currentScript.stateType == PantheraMachineState.HaveToUpdate)
            {
                currentScript.Update();
            }
        }

        public virtual void FixedUpdate()
        {

            // Check if there are a next script //
            if (this.nextScript != null)
            {
                if (this.currentScript != null && this.currentScript.stateType == PantheraMachineState.HaveToUpdate)
                {
                    this.currentScript.wasInterrupted = true;
                    this.currentScript.stateType = PantheraMachineState.HaveToStop;
                }
                else
                {
                    this.currentScript = this.nextScript;
                    this.currentScript.SetUpScript(player, this);
                    this.nextScript = null;
                }
            }

            // Return if they are no current script //
            if (this.currentScript == null) return;

            // Start the script machine //
            if (this.currentScript.stateType == PantheraMachineState.HaveToStart)
            {
                this.currentScript.stateType = PantheraMachineState.HaveToUpdate;
                this.currentScript.Start();
                return;
            }

            // Update the script machine //
            if (this.currentScript.stateType == PantheraMachineState.HaveToUpdate)
            {
                this.currentScript.FixedUpdate();
                return;
            }

            // Stop the script machine //
            if (this.currentScript.stateType == PantheraMachineState.HaveToStop)
            {
                this.currentScript.stateType = PantheraMachineState.HaveToDestroy;
                this.currentScript.Stop();
                return;
            }

            // Destroy the script //
            if (this.currentScript.stateType == PantheraMachineState.HaveToDestroy)
            {
                this.currentScript = null;
                return;
            }

        }

        public virtual MachineScript GetCurrentScript()
        {
            return this.currentScript;
        }

        public virtual void EndScript()
        {
            if (this.currentScript == null) return;
            this.currentScript.stateType = PantheraMachineState.HaveToStop;
        }

        public static bool CanBeProcessed(PantheraObj ptraObj, MachineScript script)
        {
            if (script.machineToUse == 1 && ptraObj.skillsMachine1.CheckScript(script) == true)
                return true;
            if (script.machineToUse == 2 && ptraObj.skillsMachine2.CheckScript(script) == true)
                return true;
            return false;
        }

        public virtual bool CheckScript(MachineScript script)
        {
            if (script == null) return false;
            if (this.currentScript != null && this.currentScript.GetType() == script.GetType()) return false;
            if (this.currentScript != null && script.interruptPower <= this.currentScript.priority) return false;
            if (this.nextScript != null && script.interruptPower <= this.nextScript.priority) return false;
            return true;
        }

        public virtual bool TryScript(MachineScript script)
        {
            if (script == null) return false;
            if (SetCurrentScript(script) == true)
                return true;
            else
                return false;
        }
       
        public virtual bool SetScript(MachineScript script)
        {
            if (script == null) return false;
            if (SetCurrentScript(script, true) == true)
                return true;
            else
                return false;
        }

        private bool SetCurrentScript(MachineScript script, bool forceInterupt = false)
        {
            if (script == null) return false;
            if (forceInterupt == false && this.currentScript != null && this.currentScript.GetType() == script.GetType()) return false;
            if (forceInterupt == false && this.currentScript != null && script.interruptPower <= this.currentScript.priority) return false;
            this.nextScript = script;
            return true;
        }

        public void OnDestroy()
        {
            if (this.currentScript != null)
            {
                this.currentScript.Stop();
            }
        }

    }

    public enum PantheraMachineState : int
    {
        HaveToStart = 0,
        HaveToUpdate = 1,
        HaveToStop = 2,
        HaveToDestroy = 3
    }

}
