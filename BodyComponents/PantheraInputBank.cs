using Panthera;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.Machines;
using Panthera.MachineScripts;
using Panthera.Skills;
using Panthera.SkillsHybrid;
using Rewired;
using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Panthera.BodyComponents
{
    public class PantheraInputBank : InputBankTest
    {

        public PantheraObj ptraObj;
        public Player rewirePlayer;
        public PantheraSkillsMachine skillsMachine1;
        public PantheraSkillsMachine skillsMachine2;
        public Dictionary<int, PantheraSkill> pressedSlot = new Dictionary<int, PantheraSkill>(); // List of pressed actions (key: SlotID, Value: ActionID)
        public bool switchBarPressed;

        public void DoInit()
        {
            this.ptraObj = base.GetComponent<PantheraObj>();
            this.skillsMachine1 = base.GetComponent<PantheraSkillsMachine>();
            this.skillsMachine2 = base.GetComponents<PantheraSkillsMachine>()[1];
        }

        public void FixedUpdate()
        {

            bool cancelJump = false;
            bool cancelSprint = false;

            // Return if server //
            if (ptraObj.hasAuthority() == false) return;

            // Return if dead //
            if (ptraObj.healthComponent.alive == false) return;

            // Return if sleeping //
            if (ptraObj.getPassiveScript() == null || ptraObj.getPassiveScript().isSleeping == true) return;

            // Check the Switch Bar Key //
            if (IsKeyPressed(PantheraConfig.SwitchBarKey))
            {
                switchBarPressed = true;
            }
            else
            {
                switchBarPressed = false;
            }

            // Check if a Skill Button is pressed //
            pressedSlot.Clear();
            if (IsKeyPressed(PantheraConfig.Skill1Key)) pressedSlot[1] = ptraObj.activePreset.getPressedSkill(PantheraConfig.Skill1Key, switchBarPressed);
            if (IsKeyPressed(PantheraConfig.Skill2Key)) pressedSlot[2] = ptraObj.activePreset.getPressedSkill(PantheraConfig.Skill2Key, switchBarPressed);
            if (IsKeyPressed(PantheraConfig.Skill3Key)) pressedSlot[3] = ptraObj.activePreset.getPressedSkill(PantheraConfig.Skill3Key, switchBarPressed);
            if (IsKeyPressed(PantheraConfig.Skill4Key)) pressedSlot[4] = ptraObj.activePreset.getPressedSkill(PantheraConfig.Skill4Key, switchBarPressed);
            if (IsKeyPressed(PantheraConfig.Skill5Key)) pressedSlot[5] = ptraObj.activePreset.getPressedSkill(PantheraConfig.Skill5Key, switchBarPressed);
            if (IsKeyPressed(PantheraConfig.Skill6Key)) pressedSlot[6] = ptraObj.activePreset.getPressedSkill(PantheraConfig.Skill6Key, switchBarPressed);
            if (IsKeyPressed(PantheraConfig.Skill7Key)) pressedSlot[7] = ptraObj.activePreset.getPressedSkill(PantheraConfig.Skill7Key, switchBarPressed);
            if (IsKeyPressed(PantheraConfig.Skill8Key)) pressedSlot[8] = ptraObj.activePreset.getPressedSkill(PantheraConfig.Skill8Key, switchBarPressed);
            if (IsKeyPressed(PantheraConfig.Skill9Key)) pressedSlot[9] = ptraObj.activePreset.getPressedSkill(PantheraConfig.Skill9Key, switchBarPressed);
            if (IsKeyPressed(PantheraConfig.Skill10Key)) pressedSlot[10] = ptraObj.activePreset.getPressedSkill(PantheraConfig.Skill10Key, switchBarPressed);
            // Get the maximum priority Skill //
            PantheraSkill pressedSkill = null;
            int priority = -1;
            foreach (KeyValuePair<int, PantheraSkill> entry in pressedSlot)
            {
                if (entry.Value != null && entry.Value.interruptPower != null && (int)entry.Value.interruptPower > priority)
                {
                    pressedSkill = entry.Value;
                    priority = (int)entry.Value.interruptPower;
                }
            }

            // Check if a Skill can be used //
            if (pressedSkill != null)
            {
                MachineScript script = (MachineScript)Activator.CreateInstance(pressedSkill.associatedSkill, true);
                if (script.CanBeUsed(ptraObj))
                {
                    if (pressedSkill.skillMachine == 1)
                        skillsMachine1.TryScript(script);
                    else if (pressedSkill.skillMachine == 2)
                        skillsMachine2.TryScript(script);
                }
            }

            // Check the jump key //
            if (IsKeyDoublePressed(PantheraConfig.JumpKey) && ptraObj.activePreset.getAbilityLevel(PantheraConfig.LeapAbilityID) > 0)
            {
                if (IsKeyPressed(PantheraConfig.InteractKey) && ptraObj.activePreset.getAbilityLevel(PantheraConfig.SaveMyFriendAbilityID) > 0)
                {
                    MachineScript script = (MachineScript)Activator.CreateInstance(typeof(SaveMyFriend), true);
                    if (script.CanBeUsed(ptraObj))
                        skillsMachine2.TryScript(script);
                }
                else
                {
                    MachineScript script = (MachineScript)Activator.CreateInstance(typeof(Leap), true);
                    if (script.CanBeUsed(ptraObj))
                        skillsMachine2.TryScript(script);
                }
            }
            else if (IsKeyDown(PantheraConfig.JumpKey) && cancelJump == false)
            {
                ptraObj.pantheraMotor.doJump = true;
            }

            // Check the sprint key //
            if (IsKeyDown(PantheraConfig.SprintKey) && cancelSprint == false)
            {
                if (characterBody.isSprinting && ptraObj.activePreset.getAbilityLevel(PantheraConfig.DashAbilityID) > 0)
                {
                    if (ptraObj.dashing == true)
                    {
                        Passives.Dash.StopDash(ptraObj);
                    }
                    else
                    {
                        MachineScript script = (MachineScript)Activator.CreateInstance(typeof(Dash), true);
                        if (script.CanBeUsed(ptraObj))
                            skillsMachine2.TryScript(script);
                    }
                }
                else
                {
                    ptraObj.pantheraMotor.startSprint = true;
                }
            }

            // Check the Interact Key //
            if (IsKeyPressed(PantheraConfig.InteractKey))
                ptraObj.interactPressed = true;
            else
                ptraObj.interactPressed = false;

        }

        public bool isSkillPressed(int skillID)
        {
            foreach (KeyValuePair<int, PantheraSkill> entry in pressedSlot)
            {
                if (entry.Value != null && entry.Value.skillID == skillID)
                    return true;
            }
            return false;
        }

        public bool IsKeyDown(int key)
        {
            if (ptraObj.hasAuthority() == false) return false;
            return rewirePlayer.GetButtonDown(key);
        }

        public bool IsKeyPressed(int key)
        {
            if (ptraObj.hasAuthority() == false) return false;
            return rewirePlayer.GetButton(key);
        }

        public bool IsKeyReleased(int key)
        {
            if (ptraObj.hasAuthority() == false) return false;
            return rewirePlayer.GetButtonUp(key);
        }

        public bool IsKeyDoublePressed(int key)
        {
            if (ptraObj.hasAuthority() == false) return false;
            return rewirePlayer.GetButtonDoublePressUp(key);
        }

        public bool IsRightPressed()
        {
            if (rewirePlayer.GetAxis(0) > 0) return true;
            return false;
        }

        public bool IsLeftPressed()
        {
            if (rewirePlayer.GetAxis(0) < 0) return true;
            return false;
        }

        public bool IsUpPressed()
        {
            if (rewirePlayer.GetAxis(1) > 0) return true;
            return false;
        }

        public bool IsDownPressed()
        {
            if (rewirePlayer.GetAxis(0) < 0) return true;
            return false;
        }

        public bool IsDirectionKeyPressed()
        {
            if (IsRightPressed() || IsLeftPressed() || IsUpPressed() || IsDownPressed()) return true;
            return false;
        }


    }
}