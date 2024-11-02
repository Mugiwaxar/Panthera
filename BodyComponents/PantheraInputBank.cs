using Panthera.Machines;
using Rewired;
using RoR2;
using static Panthera.GUI.KeysBinder;

namespace Panthera.BodyComponents
{
    public class PantheraInputBank : InputBankTest
    {

        public PantheraObj ptraObj;
        public PantheraComboComponent comboComponent;
        public Player rewirePlayer => Panthera.InputPlayer;
        public PantheraSkillsMachine skillsMachine1;
        public PantheraSkillsMachine skillsMachine2;
        //public Dictionary<int, PantheraSkill> pressedSlot = new Dictionary<int, PantheraSkill>(); // List of pressed actions (key: SlotID, Value: ActionID)
        //public bool switchBarPressed;

        public KeysEnum keysPressedList;

        public void DoInit()
        {
            this.ptraObj = base.GetComponent<PantheraObj>();
            this.comboComponent = this.ptraObj.comboComponent;
            this.skillsMachine1 = base.GetComponent<PantheraSkillsMachine>();
            this.skillsMachine2 = base.GetComponents<PantheraSkillsMachine>()[1];
        }

        public void Update()
        {
            // Return if server //
            if (ptraObj.HasAuthority() == false)
                return;

            // Clear the Keys Lists //
            this.keysPressedList = KeysEnum.None;

            // Return if dead //
            if (ptraObj.healthComponent.alive == false)
                return;

            // Return if sleeping //
            if (ptraObj.GetPassiveScript() == null || ptraObj.GetPassiveScript().isSleeping == true)
                return;

            // Check all Buttons //
            if (IsUpPressed()) this.keysPressedList = KeysEnum.Forward;
            if (IsDownPressed()) this.keysPressedList |= KeysEnum.Backward;
            if (IsLeftPressed()) this.keysPressedList |= KeysEnum.Left;
            if (IsRightPressed()) this.keysPressedList |= KeysEnum.Right;

            /*
            if (IsKeyPressed(PantheraConfig.InteractKey)) this.keysPressedList.Add(KeysEnum.Interact);
            if (IsKeyPressed(PantheraConfig.EquipmentKey)) this.keysPressedList.Add(KeysEnum.Equipment);
            if (IsKeyPressed(PantheraConfig.SprintKey)) this.keysPressedList.Add(KeysEnum.Sprint);
            if (IsKeyPressed(PantheraConfig.InfoKey)) this.keysPressedList.Add(KeysEnum.Info);
            if (IsKeyPressed(PantheraConfig.PingKey)) this.keysPressedList.Add(KeysEnum.Ping);
            if (IsKeyPressed(PantheraConfig.JumpKey)) this.keysPressedList.Add(KeysEnum.Jump);*/
            if (IsKeyPressed(PantheraConfig.Skill1Key)) this.keysPressedList |= KeysEnum.Skill1;
            if (IsKeyPressed(PantheraConfig.Skill2Key)) this.keysPressedList |= KeysEnum.Skill2;
            if (IsKeyPressed(PantheraConfig.Skill3Key)) this.keysPressedList |= KeysEnum.Skill3;
            if (IsKeyPressed(PantheraConfig.Skill4Key)) this.keysPressedList |= KeysEnum.Skill4;
            if (IsKeyPressed(PantheraConfig.Keys_Ability1ActionCode)) this.keysPressedList |= KeysEnum.Ability1;
            if (IsKeyPressed(PantheraConfig.Keys_Ability2ActionCode)) this.keysPressedList |= KeysEnum.Ability2;
            if (IsKeyPressed(PantheraConfig.Keys_Ability3ActionCode)) this.keysPressedList |= KeysEnum.Ability3;
            if (IsKeyPressed(PantheraConfig.Keys_Ability4ActionCode)) this.keysPressedList |= KeysEnum.Ability4;
            if (IsKeyPressed(PantheraConfig.Keys_SpellsModeActionCode)) this.keysPressedList |= KeysEnum.SpellsMode;
            /*
            if (IsKeyDown(PantheraConfig.InteractKey)) this.keysDownList |= KeysEnum.Interact;
            if (IsKeyDown(PantheraConfig.EquipmentKey)) this.keysDownList |= KeysEnum.Equipment;
            if (IsKeyDown(PantheraConfig.SprintKey)) this.keysDownList |= KeysEnum.Sprint;
            if (IsKeyDown(PantheraConfig.InfoKey)) this.keysDownList |= KeysEnum.Info;
            if (IsKeyDown(PantheraConfig.PingKey)) this.keysDownList |= KeysEnum.Ping;
            if (IsKeyDown(PantheraConfig.JumpKey)) this.keysDownList |= KeysEnum.Jump;
            if (IsKeyDown(PantheraConfig.Skill1Key)) this.keysDownList |= KeysEnum.Skill1;
            if (IsKeyDown(PantheraConfig.Skill2Key)) this.keysDownList |= KeysEnum.Skill2;
            if (IsKeyDown(PantheraConfig.Skill3Key)) this.keysDownList |= KeysEnum.Skill3;
            if (IsKeyDown(PantheraConfig.Skill4Key)) this.keysDownList |= KeysEnum.Skill4;
            if (IsKeyDown(PantheraConfig.Keys_Ability1ActionCode)) this.keysDownList |= KeysEnum.Ability1;
            if (IsKeyDown(PantheraConfig.Keys_Ability2ActionCode)) this.keysDownList |= KeysEnum.Ability2;
            if (IsKeyDown(PantheraConfig.Keys_Ability3ActionCode)) this.keysDownList |= KeysEnum.Ability3;
            if (IsKeyDown(PantheraConfig.Keys_Ability4ActionCode)) this.keysDownList |= KeysEnum.Ability4;
            if (IsKeyDown(PantheraConfig.Keys_SpellsModeActionCode)) this.keysDownList |= KeysEnum.SpellsMode;*/

            // Check the Interact Key //
            ptraObj.interactPressed = IsKeyDown(PantheraConfig.InteractKey);

            // Check the Jump key //
            if (IsKeyDown(PantheraConfig.JumpKey))
            {
                ptraObj.jumpPressed = true;
                ptraObj.pantheraMotor.doJump = true;
            }
            else if (ptraObj.jumpPressed && IsKeyReleased(PantheraConfig.JumpKey))
            {
                ptraObj.jumpPressed = false;
            }

            // Check the Sprint Key //
            if (IsKeyDown(PantheraConfig.SprintKey))
            {
                ptraObj.sprintPressed = true;
                ptraObj.pantheraMotor.isSprinting = true;
            }
            else if (ptraObj.sprintPressed && IsKeyReleased(PantheraConfig.SprintKey))
            {
                ptraObj.sprintPressed = false;
            }

            // Check if Keys are Pressed //
            if (this.keysPressedList == KeysEnum.None)
                return;

            // Try to launch a Skill //
            this.ptraObj.comboComponent.TryLaunchSkill(this.keysPressedList);

        }

        private bool IsAnyButtonChanged() => rewirePlayer.GetAnyButton() || rewirePlayer.GetAnyButtonDown();

        private bool IsKeyDown(int key) => rewirePlayer.GetButtonDown(key);

        private bool IsKeyPressed(int key) => rewirePlayer.GetButton(key);

        private bool IsKeyReleased(int key) => rewirePlayer.GetButtonUp(key);

        private bool IsKeyDoublePressed(int key) => rewirePlayer.GetButtonDoublePressUp(key);

        private bool IsRightPressed() => rewirePlayer.GetAxis(0) > 0.9f;

        private bool IsLeftPressed() => rewirePlayer.GetAxis(0) < -0.9f;

        private bool IsUpPressed() => rewirePlayer.GetAxis(1) > 0.9f;

        private bool IsDownPressed() => rewirePlayer.GetAxis(1) < -0.9f;

        private bool IsDirectionKeyPressed() => IsRightPressed() || IsLeftPressed() || IsUpPressed() || IsDownPressed();


    }
}