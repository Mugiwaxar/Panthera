using MonoMod.RuntimeDetour.HookGen;
using Panthera.GUI;
using Rewired;
using Rewired.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Panthera.Components
{

    public class KeyBind
    {
        public string name;
        public int actionID;
        public GameObject UIKeyboardButton;
        public GameObject UIMouseButton;
        public GameObject UIGamepadButton;
    }

    internal class KeysBinder
    {

        public static Player REPlayer;
        public static KeyboardMap REKeyboardMap;
        public static MouseMap REMouseMap;
        public static JoystickMap REGamepadMap;

        public static Dictionary<string, int> ActionList = new Dictionary<string, int>();
        public static InputMapper.Context CurrentContext;
        public static InputMapper CurrentInputMapper;
        public static KeysBinder CurrentMapping;

        public ConfigPanel configPanel;

        public static void Init()
        {
            // I don't really know what is this, but it works //
            var userDataInit = typeof(UserData).GetMethod(nameof(UserData.wVZZKoPFwEvodLvLcYNvVAPKpUj), BindingFlags.NonPublic | BindingFlags.Instance);
            HookEndpointManager.Add(userDataInit, KeysBinder.RegistersExtraInput);
        }

        public static void RegistersExtraInput(Action<UserData> orig, UserData self)
        {
            // Register all Inputs //
            ActionList.Add("Jump", PantheraConfig.JumpKey);
            ActionList.Add("Interact", PantheraConfig.InteractKey);
            ActionList.Add("Equipment", PantheraConfig.EquipmentKey);
            ActionList.Add("Sprint", PantheraConfig.SprintKey);
            ActionList.Add("Ping", PantheraConfig.PingKey);
            self.actions.Add(CreateRewiredAction(PantheraConfig.SwitchBarKey, "SwitchBar"));
            ActionList.Add("PrimarySkill", PantheraConfig.Skill1Key);
            ActionList.Add("SecondarySkill", PantheraConfig.Skill2Key);
            ActionList.Add("UtilitySkill", PantheraConfig.Skill3Key);
            ActionList.Add("SpecialSkill", PantheraConfig.Skill4Key);
            self.actions.Add(CreateRewiredAction(PantheraConfig.Skill5Key, "Skill5"));
            self.actions.Add(CreateRewiredAction(PantheraConfig.Skill6Key, "Skill6"));
            self.actions.Add(CreateRewiredAction(PantheraConfig.Skill7Key, "Skill7"));
            self.actions.Add(CreateRewiredAction(PantheraConfig.Skill8Key, "Skill8"));
            self.actions.Add(CreateRewiredAction(PantheraConfig.Skill9Key, "Skill9"));
            self.actions.Add(CreateRewiredAction(PantheraConfig.Skill10Key, "Skill10"));

            orig(self);

        }

        public static InputAction CreateRewiredAction(int id, string name)
        {
            // Create the new Rewired Action //
            InputAction action = new InputAction();
            action.id = id;
            action.name = name;
            action.type = InputActionType.Button;
            action.descriptiveName = name;
            action.behaviorId = 0;
            action.userAssignable = true;
            action.categoryId = 0;

            // Add the Action to the List //
            ActionList.Add(name, id);

            // Return the Action //
            return action;

        }

        public static void InitPlayer(Player player)
        {
            // Get the Rewired Maps //
            REPlayer = player;
            Player.ControllerHelper.MapHelper maps = REPlayer.controllers.maps;
            REKeyboardMap = (KeyboardMap)maps.GetFirstMapInCategory(ControllerType.Keyboard, 0, 0);
            REMouseMap = (MouseMap)maps.GetFirstMapInCategory(ControllerType.Mouse, 0, 0);
            REGamepadMap = (JoystickMap)maps.GetFirstMapInCategory(ControllerType.Joystick, 0, 0);
        }

        public static void SetAllDefaultKeyBinds()
        {
            // Switch Bar //
            if (REKeyboardMap.ContainsAction(130) == false)
                REKeyboardMap.CreateElementMap(130, Pole.Positive, KeyCode.LeftAlt, ModifierKeyFlags.None);
            if (REGamepadMap != null && REGamepadMap.ContainsAction(130) == false)
                REGamepadMap.CreateElementMap(130, Pole.Positive, 7, ControllerElementType.Button, AxisRange.Full, false);
            // Skill 5 //
            if (REKeyboardMap.ContainsAction(135) == false)
                REKeyboardMap.CreateElementMap(135, Pole.Positive, KeyCode.Alpha5, ModifierKeyFlags.None);
            if (REGamepadMap != null && REGamepadMap.ContainsAction(135) == false)
                REGamepadMap.CreateElementMap(135, Pole.Positive, 16, ControllerElementType.Button, AxisRange.Full, false);
            // Skill 6 //
            if (REKeyboardMap.ContainsAction(136) == false)
                REKeyboardMap.CreateElementMap(136, Pole.Positive, KeyCode.Alpha6, ModifierKeyFlags.None);
            if (REGamepadMap != null && REGamepadMap.ContainsAction(136) == false)
                REGamepadMap.CreateElementMap(136, Pole.Positive, 17, ControllerElementType.Button, AxisRange.Full, false);
            // Skill 7 //
            if (REKeyboardMap.ContainsAction(137) == false)
                REKeyboardMap.CreateElementMap(137, Pole.Positive, KeyCode.Alpha7, ModifierKeyFlags.None);
            if (REGamepadMap != null && REGamepadMap.ContainsAction(137) == false)
                REGamepadMap.CreateElementMap(137, Pole.Positive, 18, ControllerElementType.Button, AxisRange.Full, false);
            // Skill 8 //
            if (REKeyboardMap.ContainsAction(138) == false)
                REKeyboardMap.CreateElementMap(138, Pole.Positive, KeyCode.Alpha8, ModifierKeyFlags.None);
            if (REGamepadMap != null && REGamepadMap.ContainsAction(138) == false)
                REGamepadMap.CreateElementMap(138, Pole.Positive, 19, ControllerElementType.Button, AxisRange.Full, false);
            // Skill 9 //
            if (REKeyboardMap.ContainsAction(139) == false)
                REKeyboardMap.CreateElementMap(139, Pole.Positive, KeyCode.Alpha9, ModifierKeyFlags.None);
            // Skill 10 //
            if (REKeyboardMap.ContainsAction(1310) == false)
                REKeyboardMap.CreateElementMap(1310, Pole.Positive, KeyCode.Alpha0, ModifierKeyFlags.None);
        }

        public static void GamepadSetEnable(bool set)
        {
            // Disable all Gamepad Maps //
            foreach (JoystickMap map in REPlayer.controllers.maps.GetAllMaps(ControllerType.Joystick))
            {
                map.enabled = set;
            }
        }

        public static void StartMapping(ButtonWatcher buttonWatcher)
        {

            // Get and check the Action ID //
            int actionID = ConfigPanel.instance.getActionIdFromKeyBindObject(buttonWatcher.gameObject);
            if (actionID == null) return;

            // Get the Controllers //
            Player.ControllerHelper controllers = REPlayer.controllers;

            // Create the KeyBinder //
            CurrentMapping = new KeysBinder();
            CurrentMapping.configPanel = buttonWatcher.configPanel;

            // Disable the UI Gamepad Map //
            GamepadSetEnable(false);

            ControllerMap map = null;

            // Get the Controller Type //
            if (buttonWatcher.name.Contains("Keyboard"))
            {
                map = REKeyboardMap;
            }
            else if (buttonWatcher.name.Contains("Mouse"))
            {
                map = REMouseMap;
            }
            else if (buttonWatcher.name.Contains("Gamepad") && controllers.Joysticks.Count > 0)
            {
                map = REGamepadMap;
            }
            else
            {
                return;
            }

            // Check the Map //
            if (map == null) return;

            // Set the Text //
            ActionElementMap elementMap = map.GetFirstElementMapWithAction(actionID);
            buttonWatcher.configPanel.keyBindWindowText.text = elementMap != null ? elementMap.elementIdentifierName : "";

            // Create the Context //
            CurrentMapping.mapInput(actionID, map);

        }

        public void mapInput(int actionId, ControllerMap map)
        {

            // Get the old Action //
            ActionElementMap oldMapeElement = map.GetFirstElementMapWithAction(actionId);

            // Create the Context //
            CurrentContext = new InputMapper.Context()
            {
                actionElementMapToReplace = oldMapeElement,
                actionId = actionId,
                controllerMap = map,
                actionRange = AxisRange.Full
            };

            // Create the Mapper //
            CurrentInputMapper = new InputMapper();
            CurrentInputMapper.options = new InputMapper.Options
            {
                allowAxes = true,
                allowButtons = true,
                allowKeyboardKeysWithModifiers = true,
                allowKeyboardModifierKeyAsPrimary = true,
                checkForConflicts = false,
                checkForConflictsWithAllPlayers = false,
                checkForConflictsWithPlayerIds = Array.Empty<int>(),
                checkForConflictsWithSelf = false,
                checkForConflictsWithSystemPlayer = false,
                defaultActionWhenConflictFound = InputMapper.ConflictResponse.Add,
                holdDurationToMapKeyboardModifierKeyAsPrimary = 0.2f,
                ignoreMouseXAxis = true,
                ignoreMouseYAxis = true,
                timeout = float.PositiveInfinity,
            };

            // Create the Event //
            CurrentInputMapper.InputMappedEvent += this.onInputMapped;

            // Start the Mappper //
            CurrentInputMapper.Start(CurrentContext);

        }

        public void onInputMapped(InputMapper.InputMappedEventData obj)
        {

            // Stop the Unput Mapper //
            CurrentInputMapper.Clear();

            // Stop the Event //
            CurrentInputMapper.InputMappedEvent -= this.onInputMapped;

            // Set the Current Mapping to null //
            CurrentMapping = null;
            CurrentInputMapper = null;

            // Fill all Texts //
            ConfigPanel.instance.updateAllKeyBindTexts();

            // Close the Keys Bind Window //
            this.configPanel.keyBindWindow.SetActive(false);

        }

    }
}
