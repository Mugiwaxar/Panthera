using System;
using BepInEx;
using UnityEngine;
using RoR2;
using R2API;
using R2API.Utils;
using System.Collections.Generic;
using Panthera.Skills;
using Panthera.Utils;
using R2API.Networking;
using Panthera.Components;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.GUI;
using RoR2.CameraModes;
using System.Reflection;
using MonoMod.RuntimeDetour.HookGen;

// Creating the Panthera Namespace //
namespace Panthera
{

    // Loading dependencies //
    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("com.Dexy.Panthera", "P4N7H3R4", "0.0.1")]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    [R2APISubmoduleDependency(
        nameof(PrefabAPI),
        nameof(LoadoutAPI),
        nameof(LanguageAPI),
        nameof(SoundAPI),
        nameof(NetworkingAPI),
        nameof(DamageAPI)
        )]

    // Creating base classe Panthera //
    public class Panthera : BaseUnityPlugin
    {

        // Mod Instances //
        public static BaseUnityPlugin Instance;
        internal static List<GameObject> masterPrefabs = new List<GameObject>();

        // Starting method //
        public void Awake()
        {

            Panthera.Instance = this;

            PantheraSaveSystem.Init();
            Tokens.RegisterTokens();
            Assets.PopulateAssets();
            Utils.Sound.PopulateSounds();
            Prefab.CreatePrefab();
            Character.RegisterCharacter();
            Prefab.RegisterSkills();
            PantheraSkill.RegisterSkills();
            PantheraAbility.RegisterAbilities();
            Buff.RegisterBuffs();
            ConfigPanel.Init();
            PantheraHUD.Init();
            KeysBinder.Init();
            MessagesRegister.Register();

            CreateDoppelganger();

            PantheraHealthComponent.barrierDamageType = DamageAPI.ReserveDamageType();
            On.RoR2.DamageInfo.ModifyDamageInfo += PantheraHealthComponent.ModifyDamageInfo;

            new Utils.ContentPacks().Initialize();

        }

        private void CreateDoppelganger()
        {

            GameObject newMaster = PrefabAPI.InstantiateClone(Resources.Load<GameObject>("Prefabs/CharacterMasters/CommandoMonsterMaster"), "PantheraMonster", true);
            newMaster.GetComponent<CharacterMaster>().bodyPrefab = Prefab.characterPrefab;

            masterPrefabs.Add(newMaster);
        }
    }
}