using BepInEx;
using Panthera.Base;
using Panthera.Components;
using Panthera.GUI;
using Panthera.NetworkMessages;
using Panthera.Utils;
using R2API.Utils;
using Rewired;
using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Panthera
{
    [BepInPlugin("com.Dexy.Panthera", "P4N7H3R4", "0.0.1")]
    [BepInDependency("com.bepis.r2api")]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    public class Panthera : BaseUnityPlugin
    {

        public static BaseUnityPlugin Instance;

        public static ProfileComponent ProfileComponent;
        public static PantheraPanel PantheraPanelController;
        public static PantheraHUD PantheraHUD;
        public static Character PantheraCharacter;
        public static LocalUser FirstLocalUser;
        public static Player InputPlayer;
        public static UserProfile LoadedUserProfile;
        public static SurgeComponent surgeComponent;
        public static Xoroshiro128Plus ptraRNG = new Xoroshiro128Plus((ulong)Time.time);
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
            PantheraAssets.PopulateAssets();
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

            // Add the Profile Component //
            ProfileComponent = Instance.gameObject.GetComponent<ProfileComponent>();
            if (ProfileComponent != null)
                GameObject.DestroyImmediate(ProfileComponent);
            ProfileComponent = Instance.gameObject.AddComponent<ProfileComponent>();
                

            // Create the Caracter //
            PantheraCharacter = new Character();
            PantheraCharacter.init();
            PantheraCharacter.CreateMasteryBossList();

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
            PantheraPanelController = Instance.gameObject.GetComponent<PantheraPanel>();
            if (PantheraPanelController != null)
                GameObject.DestroyImmediate(PantheraPanelController);
            PantheraPanelController = Instance.gameObject.AddComponent<PantheraPanel>();

            // ------------------------------------------------- TO CHANGE --------------------------------------------------------- //
            // Init the Keys Binder //
            KeysBinder.InitPlayer();
            KeysBinder.SetAllDefaultKeyBinds();
            // --------------------------------------------------------------------------------------------------------------------- //
            //PantheraCharacter.lunarCoin = 100;
            //Debug.LogWarning("Added Lunar Coins");
            // --------------------------------------------------------------------------------------------------------------------- //

        }

        public void Update()
        {
            // Open/Close the Panthera Panel //
            if (InputPlayer?.GetButtonDown(PantheraConfig.Keys_OpenPantheraPanelActionCode) == true)
            {
                if (!PantheraPanelController.pantheraPanelGUI.activeInHierarchy)
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