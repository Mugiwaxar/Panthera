using Panthera.Base;
using Panthera.Utils;
using Rewired;
using RoR2.UI;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Panthera.GUI
{
    public class PantheraPanel : MonoBehaviour
    {

        public static PantheraPanel instance;

        public CharacterSelectController origGUI;
        public GameObject canva;
        public GameObject leftPanel;
        public GameObject rightPanel;

        public Player player;
        public GameObject configPanelGUI;
        public Transform dragArea;
        public bool guiOpened = false;

        public static void PantheraPanelHook(Action<RoR2.UI.CharacterSelectController> orig, RoR2.UI.CharacterSelectController self)
        {

            // Use the original function //
            orig(self);

            if (NetworkClient.active == false)
            {
                return;
            }

            // Get the Character Selection Canva //
            GameObject canva = self.transform.Find("SafeArea").gameObject;

            // Add the Config Panel to the Canva //
            instance = canva.AddComponent<PantheraPanel>();
            instance.origGUI = self;
            instance.canva = canva;

            // Get the Character Panel //
            Transform characterPanel = instance.canva.transform.Find("LeftHandPanel (Layer: Main)");
            instance.leftPanel = characterPanel.gameObject;
            Transform utilsPanel = instance.canva.transform.Find("RightHandPanel");
            instance.rightPanel = utilsPanel.gameObject;

        }

        public void Start()
        {

            // Set KeysBinder REPlayer and REMaps //
            this.player = this.origGUI.localUser.inputPlayer;
            KeysBinder.InitPlayer(this.player);
            KeysBinder.SetAllDefaultKeyBinds();

            // Get the RE Player //
            this.player = this.origGUI.localUser.inputPlayer;

            // Set the Open Panel KeyBind //


            // Create the Config Panel //
            this.configPanelGUI = UnityEngine.Object.Instantiate<GameObject>(Assets.ConfigPanelPrefab, canva.transform, false);
            this.dragArea = this.configPanelGUI.transform.Find("DragArea");
            this.configPanelGUI.SetActive(false);

        }

        public void Update()
        {

            // Get the Selected survivor //
            var charName = origGUI.survivorName.text;

            // Check if this is P4N7H3R4 //
            if (charName == "P4N7H3R4")
            {
                if (this.player.GetButton(PantheraConfig.Keys_OpenPantheraPanelActionCode) == true && this.guiOpened == false)
                {
                    // Enable the Config Panel //
                    this.configPanelGUI.SetActive(true);
                    // Enable the Drag Area //
                    this.dragArea.gameObject.SetActive(true);
                    // Disable the Panels because Hightlights cause bugs //
                    this.leftPanel.SetActive(false);
                    this.rightPanel.SetActive(false);
                    // Disable the Gamepad //
                    KeysBinder.GamepadSetEnable(false);
                    // Play the Sound //
                    Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject);
                }
                else if (this.player.GetButtonDown(PantheraConfig.Keys_OpenPantheraPanelActionCode) == true && this.guiOpened == true)
                {
                    // Disable the Config Panel //
                    this.configPanelGUI.SetActive(false);
                    // Disable the Drag Area //
                    this.dragArea.gameObject.SetActive(false);
                    // Enable the orig Panels //
                    this.leftPanel.SetActive(true);
                    this.rightPanel.SetActive(true);
                    // Enable the Gamepad //
                    KeysBinder.GamepadSetEnable(true);
                    // Play the Sound //
                    Utils.Sound.playSound(Utils.Sound.Click1, this.gameObject);
                }
            }

        }

























    }
}
