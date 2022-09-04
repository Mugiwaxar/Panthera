using Panthera.Machines;
using Panthera.MachineScripts;
using Panthera.Skills;
using Rewired;
using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Panthera.Components
{
    class PantheraInputBank : InputBankTest
    {

        public PantheraObj ptraObj;
        public Player networkUser;
        public PantheraSkillsMachine skillsMachine;
        public Dictionary<int, PantheraSkill> pressedSlot = new Dictionary<int, PantheraSkill>(); // List of pressed actions (key: SlotID, Value: ActionID)
        public bool switchBarPressed;

        public void DoInit()
        {
            this.ptraObj = this.GetComponent<PantheraObj>();
            this.networkUser = this.ptraObj.networkUser;
            this.skillsMachine = this.GetComponent<PantheraSkillsMachine>();
        }

        public void FixedUpdate()
        {

            bool cancelJump = false;
            bool cancelSprint = false;

            // Return if server //
            if (ptraObj.HasAuthority() == false) return;

            // Return if dead //
            if (ptraObj.healthComponent.alive == false) return;

            // Check the Switch Bar Key //
            if (IsKeyPressed(PantheraConfig.SwitchBarKey))
            {
                this.switchBarPressed = true;
            }
            else
            {
                this.switchBarPressed = false;
            }

            // Check if a Skill Button is pressed //
            this.pressedSlot.Clear();
            if (IsKeyPressed(PantheraConfig.Skill1Key)) this.pressedSlot[1] = this.ptraObj.activePreset.getPressedSkill(PantheraConfig.Skill1Key, this.switchBarPressed);
            if (IsKeyPressed(PantheraConfig.Skill2Key)) this.pressedSlot[2] = this.ptraObj.activePreset.getPressedSkill(PantheraConfig.Skill2Key, this.switchBarPressed);
            if (IsKeyPressed(PantheraConfig.Skill3Key)) this.pressedSlot[3] = this.ptraObj.activePreset.getPressedSkill(PantheraConfig.Skill3Key, this.switchBarPressed);
            if (IsKeyPressed(PantheraConfig.Skill4Key)) this.pressedSlot[4] = this.ptraObj.activePreset.getPressedSkill(PantheraConfig.Skill4Key, this.switchBarPressed);
            if (IsKeyPressed(PantheraConfig.Skill5Key)) this.pressedSlot[5] = this.ptraObj.activePreset.getPressedSkill(PantheraConfig.Skill5Key, this.switchBarPressed);
            if (IsKeyPressed(PantheraConfig.Skill6Key)) this.pressedSlot[6] = this.ptraObj.activePreset.getPressedSkill(PantheraConfig.Skill6Key, this.switchBarPressed);
            if (IsKeyPressed(PantheraConfig.Skill7Key)) this.pressedSlot[7] = this.ptraObj.activePreset.getPressedSkill(PantheraConfig.Skill7Key, this.switchBarPressed);
            if (IsKeyPressed(PantheraConfig.Skill8Key)) this.pressedSlot[8] = this.ptraObj.activePreset.getPressedSkill(PantheraConfig.Skill8Key, this.switchBarPressed);
            if (IsKeyPressed(PantheraConfig.Skill9Key)) this.pressedSlot[9] = this.ptraObj.activePreset.getPressedSkill(PantheraConfig.Skill9Key, this.switchBarPressed);
            if (IsKeyPressed(PantheraConfig.Skill10Key)) this.pressedSlot[10] = this.ptraObj.activePreset.getPressedSkill(PantheraConfig.Skill10Key, this.switchBarPressed);

            // Get the maximum priority Skill //
            PantheraSkill pressedSkill = null;
            int priority = -1;
            foreach (KeyValuePair<int, PantheraSkill> entry in this.pressedSlot)
            {
                if ((int)entry.Value.interruptPower > priority)
                {
                    pressedSkill = entry.Value;
                    priority = (int)entry.Value.interruptPower;
                }
            }

            // Check if a Skill can be used //
            if (pressedSkill != null)
            {
                MachineScript script = (MachineScript)Activator.CreateInstance(pressedSkill.associatedSkill, true);
                if (script.CanBeUsed(this.ptraObj))
                    this.skillsMachine.TryScript(script);
            }

            // Check the jump key //
            if (IsKeyDown(PantheraConfig.JumpKey) && cancelJump == false)
            {
                this.ptraObj.pantheraMotor.doJump = true;
            }

            // Check the sprint key //
            if (IsKeyDown(PantheraConfig.SprintKey) && cancelSprint == false)
            {
                this.ptraObj.pantheraMotor.startSprint = true;
            }

        }

        public bool isSkillPressed(int skillID)
        {
            foreach (KeyValuePair<int, PantheraSkill> entry in this.pressedSlot)
            {
                if (entry.Value.skillID == skillID)
                    return true;
            }
            return false;
        }

        public bool IsKeyDown(int key)
        {
            if (this.ptraObj.HasAuthority() == false) return false;
            return this.networkUser.GetButtonDown(key);
        }

        public bool IsKeyPressed(int key)
        {
            if (this.ptraObj.HasAuthority() == false) return false;
            return this.networkUser.GetButton(key);
        }

        public bool IsKeyReleased(int key)
        {
            if (this.ptraObj.HasAuthority() == false) return false;
            return this.networkUser.GetButtonUp(key);
        }

        public bool IsRightPressed()
        {
            if (this.networkUser.GetAxis(0) > 0) return true;
            return false;
        }

        public bool IsLeftPressed()
        {
            if (this.networkUser.GetAxis(0) < 0) return true;
            return false;
        }

        public bool IsUpPressed()
        {
            if (this.networkUser.GetAxis(1) > 0) return true;
            return false;
        }

        public bool IsDownPressed()
        {
            if (this.networkUser.GetAxis(0) < 0) return true;
            return false;
        }

        public bool IsDirectionKeyPressed()
        {
            if (IsRightPressed() || IsLeftPressed() || IsUpPressed() || IsDownPressed()) return true;
            return false;
        }


    }
}