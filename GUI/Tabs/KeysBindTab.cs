using Panthera.Base;
using Rewired;
using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Panthera.GUI.Tabs
{
    public class KeysBindTab
    {

        public PantheraPanel pantheraPanel;
        public GameObject tabObj;

        // (Action name, KeyBind Object) Represent every line of Keys Bind (Keyboard + Mouse + Gamepad) inside the Keys Bind Tab //
        public Dictionary<string, KeyBind> keysBindList = new Dictionary<string, KeyBind>();

        public GameObject keysBindWindow;
        public TextMeshProUGUI keysBindWindowText;
        public GameObject keysBindResetWindow;

        public KeysBindTab(PantheraPanel pantheraPanel)
        {

            // Set the Panthera Panel //
            this.pantheraPanel = pantheraPanel;

            // Find the Overview Tab Game Object //
            this.tabObj = pantheraPanel.pantheraPanelGUI.transform.Find("MainPanel/TabContents/TabContentKeysBind").gameObject;

            // Create the Key Bind Window //
            this.keysBindWindow = UnityEngine.Object.Instantiate<GameObject>(PantheraAssets.KeyBindWindowPrefab, this.pantheraPanel.pantheraCanvas.transform);
            this.keysBindWindow.SetActive(false);
            this.keysBindWindowText = this.keysBindWindow.transform.Find("Content").Find("KeysBind").Find("Text").GetComponent<TextMeshProUGUI>();
            ButtonWatcher buttonWatcher1 = this.keysBindWindow.transform.Find("Content").Find("KeysBindRemoveButton").gameObject.AddComponent<ButtonWatcher>();
            ButtonWatcher buttonWatcher2 = this.keysBindWindow.transform.Find("Content").Find("KeysBindCancelButton").gameObject.AddComponent<ButtonWatcher>();
            buttonWatcher1.pantheraPanel = this.pantheraPanel;
            buttonWatcher2.pantheraPanel = this.pantheraPanel;

            // Create the Reset Key Bind Window //
            this.keysBindResetWindow = UnityEngine.Object.Instantiate<GameObject>(PantheraAssets.ResetKeyBindWindowPrefab, this.pantheraPanel.pantheraCanvas.transform);
            this.keysBindResetWindow.SetActive(false);
            ButtonWatcher buttonWatcher3 = this.keysBindResetWindow.transform.Find("Content").Find("CancelResetKeysBindButton").gameObject.AddComponent<ButtonWatcher>();
            ButtonWatcher buttonWatcher4 = this.keysBindResetWindow.transform.Find("Content").Find("ResetResetKeysBindButton").gameObject.AddComponent<ButtonWatcher>();
            buttonWatcher3.pantheraPanel = this.pantheraPanel;
            buttonWatcher4.pantheraPanel = this.pantheraPanel;

            // Create a List of all Keys Bind //
            this.registerAllKeysBind();

        }

        public void registerAllKeysBind()
        {
            // Look for all Keys Bind //
            foreach (Transform transform in this.tabObj.transform.Find("Content"))
            {
                // Exclude the Titles Layout //
                if (transform.name.Contains("Title")) continue;

                // Create the Key Bind Object //
                KeyBind keyBind = new KeyBind();

                // Get the Name //
                keyBind.name = transform.name.Replace("Layout", "");

                // Get the Ingame Action Name //
                keyBind.name = KeysBinder.ActionNameToGameName[keyBind.name];

                // Get the Action ID //
                int actionID = -1;
                if (keyBind.name == PantheraConfig.ForwardKeyName)
                    actionID = 1;
                else if (keyBind.name == PantheraConfig.BackwardKeyName)
                    actionID = 1;
                else if (keyBind.name == PantheraConfig.RightKeyName)
                    actionID = 0;
                else if (keyBind.name == PantheraConfig.LeftKeyName)
                    actionID = 0;
                else if (KeysBinder.ActionList.ContainsKey(keyBind.name) == true)
                    actionID = KeysBinder.ActionList[keyBind.name];

                // Check the Action ID //
                if (actionID < 0) continue;

                // Set the Key Bind Action ID //
                keyBind.actionID = actionID;

                // Get the Axis Range //
                if (keyBind.name == PantheraConfig.BackwardKeyName || keyBind.name == PantheraConfig.LeftKeyName)
                    keyBind.axisRange = AxisRange.Negative;
                else
                    keyBind.axisRange = AxisRange.Positive;

                // Gets Buttons //
                keyBind.UIKeyboardButton = transform.GetChild(2).gameObject;
                keyBind.UIMouseButton = transform.GetChild(3).gameObject;
                keyBind.UIGamepadButton = transform.GetChild(4).gameObject;

                // Set the Button Watcher Component //
                keyBind.UIKeyboardButton.GetComponent<ButtonWatcher>().keyBindObj = keyBind;
                keyBind.UIMouseButton.GetComponent<ButtonWatcher>().keyBindObj = keyBind;
                if (keyBind.UIGamepadButton.name != "KBButtonForwardGamepad" && keyBind.UIGamepadButton.name != "KBButtonBackwardGamepad" && keyBind.UIGamepadButton.name != "KBButtonLeftGamepad" && keyBind.UIGamepadButton.name != "KBButtonRightGamepad")
                    keyBind.UIGamepadButton.GetComponent<ButtonWatcher>().keyBindObj = keyBind;
                else
                    keyBind.UIGamepadButton.GetComponent<ButtonWatcher>().enabled = false;

                // Add to the List //
                this.keysBindList.Add(keyBind.name, keyBind);

            }
        }

        public void update()
        {
            // Update all Keys Bind Text //
            this.updateAllKeyBindTexts();
        }

        public void enable()
        {
            this.tabObj.SetActive(true);
        }

        public void disable()
        {
            this.tabObj.SetActive(false);
        }

        public void updateAllKeyBindTexts()
        {

            // Itinerate all KeyBinds //
            foreach (KeyBind keyBind in this.keysBindList.Values)
            {

                // Get all Text Elements //
                TextMeshProUGUI keyboardText = keyBind.UIKeyboardButton.transform.Find("Text").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI mouseText = keyBind.UIMouseButton.transform.Find("Text").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI joystickText = keyBind.UIGamepadButton.transform.Find("Text").GetComponent<TextMeshProUGUI>();

                // Get all Element Maps //
                ActionElementMap keyboardElement = KeysBinder.GetElementMapFromKeyBind(keyBind, ControllerType.Keyboard);
                ActionElementMap mouseElement = KeysBinder.GetElementMapFromKeyBind(keyBind, ControllerType.Mouse);
                ActionElementMap joystickElement = KeysBinder.GetElementMapFromKeyBind(keyBind, ControllerType.Joystick);

                // Fill the Keyboard Key //
                if (keyboardElement != null)
                    keyboardText.text = keyboardElement.elementIdentifierName;
                else
                    keyboardText.text = null;
                // Fill the Mouse Key //
                if (mouseElement != null)
                    mouseText.text = mouseElement.elementIdentifierName;
                else
                    mouseText.text = null;
                // Fill the Gamepad Key //
                if (joystickElement != null)
                    joystickText.text = joystickElement.elementIdentifierName;
                else if (KeysBinder.REJoystickMap == null)
                    joystickText.text = "No Controller";
                else if (keyBind.name == PantheraConfig.ForwardKeyName || keyBind.name == PantheraConfig.BackwardKeyName || keyBind.name == PantheraConfig.RightKeyName || keyBind.name == PantheraConfig.LeftKeyName)
                    joystickText.text = "Left Joystick";
                else if (joystickElement == null)
                    joystickText.text = null;
            }

        }

    }
}
