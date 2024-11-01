using MonoMod.RuntimeDetour.HookGen;
using Panthera;
using Panthera.Components;
using Panthera.GUI;
using Panthera.GUI.Tabs;
using Rewired;
using Rewired.Data;
using RoR2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Rewired.InputMapper;
using static Rewired.Platforms.Custom.CustomInputSource;

namespace Panthera.GUI
{

    public class KeyBind
    {
        public string name;
        public int actionID;
        public AxisRange axisRange;
        public GameObject UIKeyboardButton;
        public GameObject UIMouseButton;
        public GameObject UIGamepadButton;
        public ControllerType controllerType;
    }

    public class KeysBinder
    {

        public static KeyboardMap REKeyboardMap
        {
            get
            {
                return (KeyboardMap)Panthera.InputPlayer.controllers.maps.GetFirstMapInCategory(ControllerType.Keyboard, 0, 0);
            }
        }
        public static MouseMap REMouseMap
        {
            get
            {
                return (MouseMap)Panthera.InputPlayer.controllers.maps.GetFirstMapInCategory(ControllerType.Mouse, 0, 0);
            }
        }
        public static JoystickMap REJoystickMap
        {
            get
            {
                if (Panthera.InputPlayer.controllers.maps.GetFirstMapInCategory(ControllerType.Joystick, 0, 0) != null)
                    return (JoystickMap)Panthera.InputPlayer.controllers.maps.GetFirstMapInCategory(ControllerType.Joystick, 0, 0);
                return null;
            }
        }

        // (Action Name, Action ID) Represent the List of all Action Name and ID //
        public static Dictionary<string, int> ActionList = new Dictionary<string, int>();

        // (Action Name, Ingame Action Name) Represent the List of all Action Name and the Ingame Name //
        public static Dictionary<string, string> ActionNameToGameName = new Dictionary<string, string>();

        public static InputMapper.Context CurrentContext;
        public static InputMapper InputMapper;
        public static KeysBinder CurrentMapping;

        public PantheraPanel pantheraPanel;

        public static void ApplyUserProfileBindingstoRewiredController(Rewired.Controller controller)
        {
            if (Panthera.FirstLocalUser == null)
            {
                return;
            }
            ControllerMap controllerMap = null;
            switch (controller.type)
            {
                case ControllerType.Keyboard:
                    controllerMap = Panthera.FirstLocalUser.userProfile.keyboardMap;
                    break;
                case ControllerType.Mouse:
                    controllerMap = Panthera.FirstLocalUser.userProfile.mouseMap;
                    break;
                case ControllerType.Joystick:
                    controllerMap = Panthera.FirstLocalUser.userProfile.joystickMap;
                    break;
            }
            if (controllerMap != null)
            {
                Panthera.FirstLocalUser.inputPlayer.controllers.maps.AddMap(controller, controllerMap);
            }
        }
        public static void RegistersExtraInput(Action<UserData> orig, UserData self)
        {
            // Clear the Action List //
            ActionList.Clear();

            // Register all Inputs //
            ActionList.Add(PantheraConfig.InteractKeyName, PantheraConfig.InteractKey);
            ActionList.Add(PantheraConfig.EquipmentKeyName, PantheraConfig.EquipmentKey);
            ActionList.Add(PantheraConfig.SprintKeyName, PantheraConfig.SprintKey);
            ActionList.Add(PantheraConfig.InfoKeyName, PantheraConfig.InfoKey);
            ActionList.Add(PantheraConfig.PingKeyName, PantheraConfig.PingKey);
            ActionList.Add(PantheraConfig.ForwardKeyName, PantheraConfig.ForwardBackwardKey);
            //ActionList.Add(PantheraConfig.BackwardKeyName, PantheraConfig.ForwardBackwardKey); // The same as Forward
            ActionList.Add(PantheraConfig.LeftKeyName, PantheraConfig.LeftRightKey);
            //ActionList.Add(PantheraConfig.RightKeyName, PantheraConfig.LeftRightKey); // The same as Left
            ActionList.Add(PantheraConfig.JumpKeyName, PantheraConfig.JumpKey);
            ActionList.Add(PantheraConfig.Skill1KeyName, PantheraConfig.Skill1Key);
            ActionList.Add(PantheraConfig.Skill2KeyName, PantheraConfig.Skill2Key);
            ActionList.Add(PantheraConfig.Skill3KeyName, PantheraConfig.Skill3Key);
            ActionList.Add(PantheraConfig.Skill4KeyName, PantheraConfig.Skill4Key);
            self.actions.Add(CreateRewiredAction(PantheraConfig.Keys_OpenPantheraPanelActionCode, PantheraConfig.Keys_OpenPantheraPanelActionName, PantheraConfig.Keys_OpenPantheraPanelActionDesc));
            self.actions.Add(CreateRewiredAction(PantheraConfig.Keys_Ability1ActionCode, PantheraConfig.Keys_Ability1ActionName, PantheraConfig.Keys_Ability1ActionDesc));
            self.actions.Add(CreateRewiredAction(PantheraConfig.Keys_Ability2ActionCode, PantheraConfig.Keys_Ability2ActionName, PantheraConfig.Keys_Ability2ActionDesc));
            self.actions.Add(CreateRewiredAction(PantheraConfig.Keys_Ability3ActionCode, PantheraConfig.Keys_Ability3ActionName, PantheraConfig.Keys_Ability3ActionDesc));
            self.actions.Add(CreateRewiredAction(PantheraConfig.Keys_Ability4ActionCode, PantheraConfig.Keys_Ability4ActionName, PantheraConfig.Keys_Ability4ActionDesc));
            self.actions.Add(CreateRewiredAction(PantheraConfig.Keys_SpellsModeActionCode, PantheraConfig.Keys_SpellsModeActionName, PantheraConfig.Keys_SpellsModeActionDesc));

            orig(self);

        }

        public static InputAction CreateRewiredAction(int id, string name, string desc)
        {
            // Create the new Rewired Action //
            InputAction action = new InputAction();
            action.id = id;
            action.name = name;
            action.type = InputActionType.Button;
            action.descriptiveName = desc;
            action.behaviorId = 0;
            action.userAssignable = true;
            action.categoryId = 0;

            // Add the Action to the List //
            ActionList.Add(name, id);

            // Return the Action //
            return action;

        }

        public static void InitPlayer()
        {
            // Create the Mapper //
            InputMapper = new InputMapper();
            InputMapper.options = new InputMapper.Options
            {
                allowAxes = true,
                allowButtons = true,
                allowKeyboardKeysWithModifiers = true,
                allowKeyboardModifierKeyAsPrimary = true,
                checkForConflicts = true,
                checkForConflictsWithAllPlayers = false,
                checkForConflictsWithPlayerIds = Array.Empty<int>(),
                checkForConflictsWithSelf = true,
                checkForConflictsWithSystemPlayer = false,
                defaultActionWhenConflictFound = InputMapper.ConflictResponse.Add,
                holdDurationToMapKeyboardModifierKeyAsPrimary = 0.2f,
                ignoreMouseXAxis = true,
                ignoreMouseYAxis = true,
                timeout = float.PositiveInfinity,
            };

            // Create the Mapper Event //
            InputMapper.InputMappedEvent += onInputMapped;
            InputMapper.ConflictFoundEvent += onConflictFound;

            // Clear the ActionNameToGameName Map //
            ActionNameToGameName.Clear();

            // Create the Ingame Action Name //
            ActionNameToGameName.Add("Interact", PantheraConfig.InteractKeyName);
            ActionNameToGameName.Add("Equipment", PantheraConfig.EquipmentKeyName);
            ActionNameToGameName.Add("Sprint", PantheraConfig.SprintKeyName);
            ActionNameToGameName.Add("Info", PantheraConfig.InfoKeyName);
            ActionNameToGameName.Add("Ping", PantheraConfig.PingKeyName);
            ActionNameToGameName.Add("Forward", PantheraConfig.ForwardKeyName);
            ActionNameToGameName.Add("Backward", PantheraConfig.BackwardKeyName);
            ActionNameToGameName.Add("Left", PantheraConfig.LeftKeyName);
            ActionNameToGameName.Add("Right", PantheraConfig.RightKeyName);
            ActionNameToGameName.Add("Jump", PantheraConfig.JumpKeyName);
            ActionNameToGameName.Add("Skill1", PantheraConfig.Skill1KeyName);
            ActionNameToGameName.Add("Skill2", PantheraConfig.Skill2KeyName);
            ActionNameToGameName.Add("Skill3", PantheraConfig.Skill3KeyName);
            ActionNameToGameName.Add("Skill4", PantheraConfig.Skill4KeyName);
            ActionNameToGameName.Add("Ability1", PantheraConfig.Keys_Ability1ActionName);
            ActionNameToGameName.Add("Ability2", PantheraConfig.Keys_Ability2ActionName);
            ActionNameToGameName.Add("Ability3", PantheraConfig.Keys_Ability3ActionName);
            ActionNameToGameName.Add("Ability4", PantheraConfig.Keys_Ability4ActionName);
            ActionNameToGameName.Add("Spells", PantheraConfig.Keys_SpellsModeActionName);

            // Create the Connected Controller Event //
            ReInput.ControllerConnectedEvent += OnControllerConnected;

        }

        public static void SetAllDefaultKeyBinds()
        {
            // Open Panthera Panel //
            if (REKeyboardMap.ContainsAction(PantheraConfig.Keys_OpenPantheraPanelActionCode) == false)
                REKeyboardMap.CreateElementMap(PantheraConfig.Keys_OpenPantheraPanelActionCode, Pole.Positive, PantheraConfig.Keys_OpenPantheraPanelDefaultKey, ModifierKeyFlags.None);
            // Ability 1 //
            if (REKeyboardMap.ContainsAction(PantheraConfig.Keys_Ability1ActionCode) == false)
                REKeyboardMap.CreateElementMap(PantheraConfig.Keys_Ability1ActionCode, Pole.Positive, PantheraConfig.Keys_Ability1DefaultKey, ModifierKeyFlags.None);
            // Ability 2 //
            if (REKeyboardMap.ContainsAction(PantheraConfig.Keys_Ability2ActionCode) == false)
                REKeyboardMap.CreateElementMap(PantheraConfig.Keys_Ability2ActionCode, Pole.Positive, PantheraConfig.Keys_Ability2DefaultKey, ModifierKeyFlags.None);
            // Ability 3 //
            if (REKeyboardMap.ContainsAction(PantheraConfig.Keys_Ability3ActionCode) == false)
                REKeyboardMap.CreateElementMap(PantheraConfig.Keys_Ability3ActionCode, Pole.Positive, PantheraConfig.Keys_Ability3DefaultKey, ModifierKeyFlags.None);
            // Ability 4 //
            if (REKeyboardMap.ContainsAction(PantheraConfig.Keys_Ability4ActionCode) == false)
                REKeyboardMap.CreateElementMap(PantheraConfig.Keys_Ability4ActionCode, Pole.Positive, PantheraConfig.Keys_Ability4DefaultKey, ModifierKeyFlags.None);
            // Spells Mode //
            if (REKeyboardMap.ContainsAction(PantheraConfig.Keys_SpellsModeActionCode) == false)
                REKeyboardMap.CreateElementMap(PantheraConfig.Keys_SpellsModeActionCode, Pole.Positive, PantheraConfig.Keys_SpellsModeDefaultKey, ModifierKeyFlags.None);

            // Check if Controller Map //
            if (REJoystickMap != null)
            {
                // Ability 1 //
                if (REJoystickMap.ContainsAction(PantheraConfig.Keys_Ability1ActionCode) == false)
                    REJoystickMap.CreateElementMap(PantheraConfig.Keys_Ability1ActionCode, Pole.Positive, PantheraConfig.Keys_Ability1DefaultJoystickIdentifierID, ControllerElementType.Button, AxisRange.Full, false);
                // Ability 2 //
                if (REJoystickMap.ContainsAction(PantheraConfig.Keys_Ability2ActionCode) == false)
                    REJoystickMap.CreateElementMap(PantheraConfig.Keys_Ability2ActionCode, Pole.Positive, PantheraConfig.Keys_Ability2DefaultJoystickIdentifierID, ControllerElementType.Button, AxisRange.Full, false);
                // Ability 3 //
                if (REJoystickMap.ContainsAction(PantheraConfig.Keys_Ability3ActionCode) == false)
                    REJoystickMap.CreateElementMap(PantheraConfig.Keys_Ability3ActionCode, Pole.Positive, PantheraConfig.Keys_Ability3DefaultJoystickIdentifierID, ControllerElementType.Button, AxisRange.Full, false);
                // Ability 4 //
                if (REJoystickMap.ContainsAction(PantheraConfig.Keys_Ability4ActionCode) == false)
                    REJoystickMap.CreateElementMap(PantheraConfig.Keys_Ability4ActionCode, Pole.Positive, PantheraConfig.Keys_Ability4DefaultJoystickIdentifierID, ControllerElementType.Button, AxisRange.Full, false);
                // Spell Mode //
                if (REJoystickMap.ContainsAction(PantheraConfig.Keys_SpellsModeActionCode) == false)
                    REJoystickMap.CreateElementMap(PantheraConfig.Keys_SpellsModeActionCode, Pole.Positive, PantheraConfig.Keys_SpellModeDefaultJoystickIdentifierID, ControllerElementType.Button, AxisRange.Full, false);
            }

        }

        public static void OnControllerConnected(ControllerStatusChangedEventArgs args)
        {
            // Set all Default Key Binds //
            SetAllDefaultKeyBinds();
        }

        public static void GamepadSetEnable(bool set)
        {
            // Disable all Gamepad Maps //
            if (REJoystickMap != null)
                REJoystickMap.enabled = set;
        }

        public static ActionElementMap GetElementMapFromKeyBind(KeyBind keyBind, ControllerType type)
        {

            // Create the Variables //
            ActionElementMap returnedElementMap = null;
            List<ActionElementMap> elementsList = null;

            // Get the Elements List //
            if (type == ControllerType.Keyboard)
                elementsList = REKeyboardMap?.GetElementMapsWithAction(keyBind.actionID)?.ToList();
            else if (type == ControllerType.Mouse)
                elementsList = REMouseMap?.GetElementMapsWithAction(keyBind.actionID)?.ToList();
            else if (type == ControllerType.Joystick)
                elementsList = REJoystickMap?.GetElementMapsWithAction(keyBind.actionID)?.ToList();

            if (elementsList == null) return null;

            if (keyBind.name == PantheraConfig.ForwardKeyName || keyBind.name == PantheraConfig.BackwardKeyName || keyBind.name == PantheraConfig.RightKeyName || keyBind.name == PantheraConfig.LeftKeyName)
            {
                foreach (ActionElementMap elementMap in elementsList)
                {
                    if (elementMap.actionDescriptiveName == PantheraConfig.ForwardKeyName && keyBind.name == PantheraConfig.ForwardKeyName)
                        returnedElementMap = elementMap;
                    else if (elementMap.actionDescriptiveName == PantheraConfig.RightKeyName && keyBind.name == PantheraConfig.RightKeyName)
                        returnedElementMap = elementMap;
                    else if (elementMap.actionDescriptiveName == PantheraConfig.BackwardKeyName && keyBind.name == PantheraConfig.BackwardKeyName)
                        returnedElementMap = elementMap;
                    else if (elementMap.actionDescriptiveName == PantheraConfig.LeftKeyName && keyBind.name == PantheraConfig.LeftKeyName)
                        returnedElementMap = elementMap;
                }
            }
            else
            {
                if (elementsList.Count > 0)
                    returnedElementMap = elementsList.First();
            }

            // Return the Element //
            return returnedElementMap;

        }

        public static void ResetAllKeysBind()
        {

            if (Panthera.InputPlayer == null)
            {
                return;
            }
            if (Panthera.FirstLocalUser.userProfile != null)
            {
                Panthera.InputPlayer.controllers.maps.ClearAllMaps(false);
                foreach (Rewired.Controller controller in Panthera.InputPlayer.controllers.Controllers)
                {
                    Panthera.InputPlayer.controllers.maps.LoadMap(controller.type, controller.id, 2, 0);
                    Panthera.FirstLocalUser.userProfile.keyboardMap = new KeyboardMap(DefaultControllerMaps.defaultKeyboardMap);
                    Panthera.FirstLocalUser.userProfile.mouseMap = new MouseMap(DefaultControllerMaps.defaultMouseMap);
                    Panthera.FirstLocalUser.userProfile.joystickMap = new JoystickMap(DefaultControllerMaps.DefaultJoystickMap);
                    ApplyUserProfileBindingstoRewiredController(controller);
                }
                SetAllDefaultKeyBinds();
                Panthera.InputPlayer.controllers.maps.SetAllMapsEnabled(true);
            }

            // Save the Profil //
            Panthera.LoadedUserProfile.RequestEventualSave();

        }

        public static void StartMapping(string buttonName, KeyBind keyBind, KeysBindTab keyBindTab)
        {

            // Get the Controllers //
            Player.ControllerHelper controllers = Panthera.InputPlayer.controllers;

            // Disable the UI Gamepad Map //
            GamepadSetEnable(false);

            // Create the Variable //
            ControllerMap map = null;
            ActionElementMap elementMap = null;

            // Get the Controller Type //
            if (buttonName.Contains("Keyboard"))
            {
                map = REKeyboardMap;
                elementMap = KeysBinder.GetElementMapFromKeyBind(keyBind, ControllerType.Keyboard);
            }
            else if (buttonName.Contains("Mouse"))
            {
                map = REMouseMap;
                elementMap = KeysBinder.GetElementMapFromKeyBind(keyBind, ControllerType.Mouse);
            }
            else if (buttonName.Contains("Gamepad") && controllers.Joysticks.Count > 0)
            {
                map = REJoystickMap;
                elementMap = KeysBinder.GetElementMapFromKeyBind(keyBind, ControllerType.Joystick);
            }
            else
            {
                keyBindTab.keysBindWindow.active = false;
                return;
            }

            // Check the Map //
            if (map == null)
            {
                keyBindTab.keysBindWindow.active = false;
                return;
            }

            // Set the Text //
            keyBindTab.keysBindWindowText.text = elementMap != null ? elementMap.elementIdentifierName : "";

            // Create the Context //
            MapInput(keyBind, map, elementMap);

        }

        public static void MapInput(KeyBind keyBind, ControllerMap map, ActionElementMap oldElementMap)
        {

            // Create the Context //
            CurrentContext = new InputMapper.Context()
            {
                actionElementMapToReplace = oldElementMap,
                actionId = keyBind.actionID,
                controllerMap = map,
                actionRange = keyBind.axisRange
            };

            // Start the Mappper //
            InputMapper.Start(CurrentContext);

        }

        public static void onInputMapped(InputMapper.InputMappedEventData obj)
        {

            // Stop the Unput Mapper //
            InputMapper.Stop();

            // Set the Current Mapping to null //
            CurrentMapping = null;

            // Fill all Texts //
            //ConfigPanel.instance.updateAllKeyBindTexts();

            // Close the Keys Bind Window //
            Panthera.PantheraPanelController.keysBindTab.keysBindWindow.active = false;

            // Save the Profil //
            Panthera.LoadedUserProfile.RequestEventualSave();

            // Refresh the GUI //
            //for (int j = 0; j < InputBindingDisplayController.instances.Count; j++)
            //{
            //    InputBindingDisplayController.instances[j].Refresh(true);
            //}

        }

        public static void onConflictFound(InputMapper.ConflictFoundEventData data)
        {
            // Check if must Replace //
            if (data.isProtected == false)
                data.responseCallback(InputMapper.ConflictResponse.Replace);
            else
                data.responseCallback(InputMapper.ConflictResponse.Add);
        }
        /*
        public enum KeysEnum
        {
            Interact = 5,
            Equipment = 6,
            Sprint = 18,
            Info = 19,
            Ping = 28,
            Forward = 1001,
            Backward = 1002,
            Left = 1003,
            Right = 1004,
            Jump = 4,
            Skill1 = 7,
            Skill2 = 8,
            Skill3 = 9,
            Skill4 = 10,
            Ability1 = 1301,
            Ability2 = 1302,
            Ability3 = 1303,
            Ability4 = 1304,
            SpellsMode = 1310
        }
        */

        [Flags]
        public enum KeysEnum : ushort
        {
            None = 0,

            Forward = 1 << 0,
            Backward = 1 << 1,
            Left = 1 << 2,
            Right = 1 << 3,

            Skill1 = 1 << 4,
            Skill2 = 1 << 5,
            Skill3 = 1 << 6,
            Skill4 = 1 << 7,

            Ability1 = 1 << 8,
            Ability2 = 1 << 9,
            Ability3 = 1 << 10,
            Ability4 = 1 << 11,

            SpellsMode = 1 << 12,
            Jump = 1 << 13,
            Sprint = 1 << 14,
            /*
            Interact = 1 << 15,
            Equipment = 1 << 16,
            Info = 1 << 17,
            Ping = 1 << 18,*/
        }

    }
}
