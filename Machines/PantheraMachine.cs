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

namespace Panthera.Machines
{
    class PantheraMachine : MonoBehaviour
    {

        public string mainScript;
        public string name;
        private MachineScript currentScript;
        private MachineScript nextScript;

        public GameObject player;
        public PantheraObj ptraObj;

        public virtual void Init()
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
            // Create a new script machine from the main script //
            if (this.currentScript == null && String.IsNullOrEmpty(this.mainScript) == false && Type.GetType(this.mainScript).IsSubclassOf(typeof(MachineScript)))
            {
                this.currentScript = (MachineScript) Activator.CreateInstance(Type.GetType(this.mainScript));
                this.currentScript.SetUpScript(this.player, this);
                return;
            }

            // Check if there are a next script //
            if (this.nextScript != null)
            {
                if (this.currentScript != null)
                {
                    this.currentScript.wasInterrupted = true;
                    if(this.currentScript.stateType == PantheraMachineState.HaveToUpdate) this.currentScript.Stop();
                    this.currentScript.stateType = PantheraMachineState.HaveToStop;
                }
                this.currentScript = this.nextScript;
                this.currentScript.SetUpScript(this.player, this);
                this.nextScript = null;
                return;
            }

            // Return if they are no current script //
            if (this.currentScript == null) return;

            // Start the script machine //
            if (this.currentScript.stateType == PantheraMachineState.HaveToStart)
            {
                this.currentScript.Start();
                this.currentScript.stateType = PantheraMachineState.HaveToUpdate;
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
                this.currentScript.Stop();
                this.currentScript.stateType = PantheraMachineState.HaveToDestroy;
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

        public virtual void TryScript(Type type)
        {
            if (type == null) return;
            TryScript(type.FullName);
        }
        public virtual void TryScript(string scriptType)
        {
            if (scriptType == null) return;
            SetCurrentScript((MachineScript)Activator.CreateInstance(Type.GetType(scriptType)));
        }
        public virtual void TryScript(MachineScript script)
        {
            if (script == null) return;
            SetCurrentScript(script);
        }

        public virtual void SetScript(Type type)
        {
            if (type == null) return;
            SetScript(type.FullName);
        }
        public virtual void SetScript(string scriptType)
        {
            if (scriptType == null) return;
            SetCurrentScript((MachineScript)Activator.CreateInstance(Type.GetType(scriptType)), true);
        }        
        public virtual void SetScript(MachineScript script)
        {
            if (script == null) return;
            SetCurrentScript(script, true);
        }

        private void SetCurrentScript(MachineScript script, bool forceInterupt = false)
        {
            if (script == null) return;
            if (forceInterupt == false && this.currentScript != null && this.currentScript.GetType() == script.GetType()) return;
            if (forceInterupt == false && this.currentScript != null && script.getSkillDef().interruptPower <= this.currentScript.getSkillDef().priority) return;
            this.nextScript = script;
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
