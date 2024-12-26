using Panthera;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Combos;
using Panthera.Components;
using Panthera.Machines;
using Panthera.MachineScripts;
using Rewired;
using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using static Panthera.GUI.KeysBinder;

namespace Panthera.BodyComponents
{
    public class PantheraInputBank : InputBankTest
    {

        public PantheraObj ptraObj;
        public PantheraComboComponent comboComponent;
        public Player rewirePlayer
        {
            get
            {
                return Panthera.InputPlayer;
            }
        }
        //public PantheraSkillsMachine skillsMachine1;
        //public PantheraSkillsMachine skillsMachine2;
        //public Dictionary<int, PantheraSkill> pressedSlot = new Dictionary<int, PantheraSkill>(); // List of pressed actions (key: SlotID, Value: ActionID)
        //public bool switchBarPressed;

        public KeysEnum keysPressed;
        //public List<KeysEnum> keysDownList = new List<KeysEnum>();
        //public KeysEnum directionKeyPressed = 0;

        public void DoInit()
        {
            this.ptraObj = base.GetComponent<PantheraObj>();
            this.comboComponent = this.ptraObj.comboComponent;
            //this.skillsMachine1 = base.GetComponent<PantheraSkillsMachine>();
            //this.skillsMachine2 = base.GetComponents<PantheraSkillsMachine>()[1];
        }

        public void Update()
        {

            // Return if server //
            if (ptraObj.hasAuthority() == false) return;

            // Clear the Keys Lists //
            //this.keysPressedList.Clear();
            //this.keysDownList.Clear();
            //this.directionKeyPressed = 0;
            this.keysPressed = KeysEnum.None;


            // Return if dead //
            if (ptraObj.healthComponent.alive == false) return;

            // Return if sleeping //
            if (ptraObj.getPassiveScript() == null || ptraObj.getPassiveScript().isSleeping == true) return;

            // Return of the Panthera Panel is open //
            if (Panthera.PantheraPanelController.pantheraPanelGUI.active == true) return;

            // Return if no Button was pressed //
            if (this.isAnyButtonChanged() == false) return;

            // Check all Buttons //
            if (IsUpPressed()) this.keysPressed |= KeysEnum.Forward;
            if (IsDownPressed()) this.keysPressed |= KeysEnum.Backward;
            if (IsLeftPressed()) this.keysPressed |= KeysEnum.Left;
            if (IsRightPressed()) this.keysPressed |= KeysEnum.Right;

            if (IsKeyPressed(PantheraConfig.InteractKey)) this.keysPressed |= KeysEnum.Interact;
            if (IsKeyPressed(PantheraConfig.EquipmentKey)) this.keysPressed |= KeysEnum.Equipment;
            if (IsKeyPressed(PantheraConfig.SprintKey)) this.keysPressed |= KeysEnum.Sprint;
            if (IsKeyPressed(PantheraConfig.InfoKey)) this.keysPressed |= KeysEnum.Info;
            if (IsKeyPressed(PantheraConfig.PingKey)) this.keysPressed |= KeysEnum.Ping;
            if (IsKeyPressed(PantheraConfig.JumpKey)) this.keysPressed |= KeysEnum.Jump;
            if (IsKeyPressed(PantheraConfig.Skill1Key)) this.keysPressed |= KeysEnum.Skill1;
            if (IsKeyPressed(PantheraConfig.Skill2Key)) this.keysPressed |= KeysEnum.Skill2;
            if (IsKeyPressed(PantheraConfig.Skill3Key)) this.keysPressed |= KeysEnum.Skill3;
            if (IsKeyPressed(PantheraConfig.Skill4Key)) this.keysPressed |= KeysEnum.Skill4;
            if (IsKeyPressed(PantheraConfig.Keys_Ability1ActionCode)) this.keysPressed |= KeysEnum.Ability1;
            if (IsKeyPressed(PantheraConfig.Keys_Ability2ActionCode)) this.keysPressed |= KeysEnum.Ability2;
            if (IsKeyPressed(PantheraConfig.Keys_Ability3ActionCode)) this.keysPressed |= KeysEnum.Ability3;
            if (IsKeyPressed(PantheraConfig.Keys_Ability4ActionCode)) this.keysPressed |= KeysEnum.Ability4;
            if (IsKeyPressed(PantheraConfig.Keys_SpellsModeActionCode)) this.keysPressed |= KeysEnum.SpellsMode;

            //if (IsKeyDown(PantheraConfig.InteractKey)) this.keysDownList.Add(KeysEnum.Interact);
            //if (IsKeyDown(PantheraConfig.EquipmentKey)) this.keysDownList.Add(KeysEnum.Equipment);
            //if (IsKeyDown(PantheraConfig.SprintKey)) this.keysDownList.Add(KeysEnum.Sprint);
            //if (IsKeyDown(PantheraConfig.InfoKey)) this.keysDownList.Add(KeysEnum.Info);
            //if (IsKeyDown(PantheraConfig.PingKey)) this.keysDownList.Add(KeysEnum.Ping);
            //if (IsKeyDown(PantheraConfig.JumpKey)) this.keysDownList.Add(KeysEnum.Jump);
            //if (IsKeyDown(PantheraConfig.Skill1Key)) this.keysDownList.Add(KeysEnum.Skill1);
            //if (IsKeyDown(PantheraConfig.Skill2Key)) this.keysDownList.Add(KeysEnum.Skill2);
            //if (IsKeyDown(PantheraConfig.Skill3Key)) this.keysDownList.Add(KeysEnum.Skill3);
            //if (IsKeyDown(PantheraConfig.Skill4Key)) this.keysDownList.Add(KeysEnum.Skill4);
            //if (IsKeyDown(PantheraConfig.Keys_Ability1ActionCode)) this.keysDownList.Add(KeysEnum.Ability1);
            //if (IsKeyDown(PantheraConfig.Keys_Ability2ActionCode)) this.keysDownList.Add(KeysEnum.Ability2);
            //if (IsKeyDown(PantheraConfig.Keys_Ability3ActionCode)) this.keysDownList.Add(KeysEnum.Ability3);
            //if (IsKeyDown(PantheraConfig.Keys_Ability4ActionCode)) this.keysDownList.Add(KeysEnum.Ability4);
            //if (IsKeyDown(PantheraConfig.Keys_SpellsModeActionCode)) this.keysDownList.Add(KeysEnum.SpellsMode);

            // Check the Interact Key //
            if (IsKeyDown(PantheraConfig.InteractKey))
                ptraObj.interactPressed = true;
            else
                ptraObj.interactPressed = false;

            // Check the Jump key //
            if (IsKeyDown(PantheraConfig.JumpKey))
            {
                ptraObj.jumpPressed = true;
                ptraObj.pantheraMotor.doJump = true;
            }
            else
            {
                ptraObj.jumpPressed = false;
            }

            // Check the Sprint Key //
            if (IsKeyDown(PantheraConfig.SprintKey))
            {
                ptraObj.sprintPressed = true;
                ptraObj.pantheraMotor.isSprinting = true;
            }
            else
            {
                ptraObj.sprintPressed = false;
            }

            // Check if Keys are Pressed //
            if (this.keysPressed == KeysEnum.None)
                return;

            // Try to launch a Skill //
            KeysEnum checkMask = KeysEnum.Skill1 | KeysEnum.Skill2 | KeysEnum.Skill3 | KeysEnum.Skill4 |KeysEnum.Ability1 | KeysEnum.Ability2 | KeysEnum.Ability3 | KeysEnum.Ability4;
            if ((keysPressed & checkMask) != KeysEnum.None)
                this.ptraObj.comboComponent.tryLaunchSkill(this.keysPressed);

        }

        private bool isAnyButtonChanged()
        {
            if (rewirePlayer.GetAnyButton() == true || rewirePlayer.GetAnyButtonDown() == true) return true;
            return false;
        }

        private bool IsKeyDown(int key)
        {
            return rewirePlayer.GetButtonDown(key);
        }

        private bool IsKeyPressed(int key)
        {
            return rewirePlayer.GetButton(key);
        }

        private bool IsKeyReleased(int key)
        {
            return rewirePlayer.GetButtonUp(key);
        }

        private bool IsKeyDoublePressed(int key)
        {
            return rewirePlayer.GetButtonDoublePressUp(key);
        }

        private bool IsRightPressed()
        {
            if (rewirePlayer.GetAxis(0) > 0.9f) return true;
            return false;
        }

        private bool IsLeftPressed()
        {
            if (rewirePlayer.GetAxis(0) < -0.9f) return true;
            return false;
        }

        private bool IsUpPressed()
        {
            if (rewirePlayer.GetAxis(1) > 0.9f) return true;
            return false;
        }

        private bool IsDownPressed()
        {
            if (rewirePlayer.GetAxis(1) < -0.9f) return true;
            return false;
        }

        private bool IsDirectionKeyPressed()
        {
            if (IsRightPressed() || IsLeftPressed() || IsUpPressed() || IsDownPressed()) return true;
            return false;
        }


    }
}