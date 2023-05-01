using BepInEx;
using MonoMod.RuntimeDetour.HookGen;
using Panthera.Base;
using Panthera.Components;
using Panthera.GUI;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.Skills;
using Panthera.Utils;
using R2API;
using R2API.Networking;
using R2API.Utils;
using RoR2;
using RoR2.CameraModes;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

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
            Utils.Hooks.RegisterHooks();
            Tokens.RegisterTokens();
            Assets.PopulateAssets();
            Utils.Sound.PopulateSounds();
            Character.RegisterCharacter();
            Skin.RegisterSkins();
            PantheraAbility.RegisterAbilities();
            PantheraSkill.RegisterSkills();
            Buff.RegisterBuffs();
            KeysBinder.Init();
            MessagesRegister.Register();
            Character.CalculMaxExperiencePerLevel();

            CreateDoppelganger();

            new Utils.ContentPacks().Initialize();

        }

        private void CreateDoppelganger()
        {

            GameObject newMaster = PrefabAPI.InstantiateClone(Resources.Load<GameObject>("Prefabs/CharacterMasters/CommandoMonsterMaster"), "PantheraMonster", true);
            newMaster.GetComponent<CharacterMaster>().bodyPrefab = Prefab.CharacterPrefab;

            masterPrefabs.Add(newMaster);
        }
    }
}