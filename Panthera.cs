using BepInEx;
using MonoMod.RuntimeDetour.HookGen;
using Panthera.Abilities;
using Panthera.Base;
using Panthera.Components;
using Panthera.GUI;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.Utils;
using R2API;
using R2API.Networking;
using R2API.Utils;
using Rewired;
using Rewired.Utils;
using RoR2;
using RoR2.CameraModes;
using RoR2.UI;
using RoR2.UI.MainMenu;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Panthera
{

    // Loading dependencies //
    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("com.Dexy.Panthera", "P4N7H3R4", "0.0.1")]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    [R2APISubmoduleDependency(
        nameof(NetworkingAPI),
        nameof(DamageAPI)
        )]

    public class Panthera : BaseUnityPlugin
    {

        public static BaseUnityPlugin Instance;

        public static PantheraPanel PantheraPanelController;
        public static PantheraHUD PantheraHUD;
        public static Character PantheraCharacter;
        public static LocalUser FirstLocalUser;
        public static Player InputPlayer;
        public static UserProfile LoadedUserProfile;
        public static Camera UICamera
        {
            get
            {
                List<Camera> cameras = Camera.allCameras.ToList<Camera>();
                foreach (Camera camera in cameras)
                {
                    if (camera.name == "UI Camera")
                        return camera;
                }
                return null;
            }
        }

        public void Awake()
        {

            // Set the Instance //
            Panthera.Instance = this;

            // Init all Components //
            PantheraSaveSystem.Init();
            Utils.Hooks.RegisterHooks();
            Tokens.RegisterTokens();
            Assets.PopulateAssets();
            Utils.Sound.PopulateSounds();
            Prefab.RegisterCharacter();
            Skin.RegisterSkins();
            PantheraSkill.RegisterSkills();
            Buff.RegisterBuffs();
            MessagesRegister.Register();
            Prefab.CreateDoppelganger();

            // Init the Content Pack //
            new Utils.ContentPacks().Initialize();

        }

        public static void ProfileLoaded(Action<RoR2.UserProfile> orig, RoR2.UserProfile self)
        {

            // Call the Original Function //
            orig(self);

            // Create the Caracter //
            PantheraCharacter = new Character();
            PantheraCharacter.init();

            // Init the Save System //
            PantheraSaveSystem.saveFileName = self.fileName;
            PantheraSaveSystem.Load();

            // Get the Local User //
            FirstLocalUser = LocalUserManager.GetFirstLocalUser();

            // Get the input Player //
            InputPlayer = LocalUserManager.GetRewiredMainPlayer();

            // Get the User Profil //
            LoadedUserProfile = self;

            // Create the Config Panel //
            PantheraPanelController = Instance.gameObject.AddComponent<PantheraPanel>();

            // ------------------------------------------------- TO CHANGE --------------------------------------------------------- //
            // Init the Keys Binder //
            KeysBinder.InitPlayer();
            KeysBinder.SetAllDefaultKeyBinds();
            // --------------------------------------------------------------------------------------------------------------------- //

        }

        public void Update()
        {
            // Open/Close the Panthera Panel //
            if (InputPlayer != null && InputPlayer.GetButtonDown(PantheraConfig.Keys_OpenPantheraPanelActionCode))
            {
                if (PantheraPanelController.pantheraPanelGUI.active == false)
                {
                    PantheraPanelController.open();
                }
                else
                {
                    PantheraPanelController.close();
                }
            }

            //ReadOnlyCollection<TeamComponent> tl = RoR2.TeamComponent.GetTeamMembers(TeamIndex.Player);
            //if (tl != null)
            //{
            //    for (int i = 0; i < tl.Count; i++)
            //    {
            //        TeamComponent team = tl[i];
            //        string name = team.gameObject == null || team.gameObject.IsNullOrDestroyed() == true ? "Destroyed" : team.name;
            //        Utils.DebugInfo.addText("P" + i.ToString(), "P" + i.ToString() + ":" + name); ;
            //    }
            //}

        }

    }
}