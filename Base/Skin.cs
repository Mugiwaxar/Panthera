using HarmonyLib;
using MonoMod.RuntimeDetour.HookGen;
using Panthera;
using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using RoR2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking.Match;
using static R2API.LoadoutAPI;
using static UnityEngine.ParticleSystem;

namespace Panthera.Base
{
    public class Skin
    {

        public static CharacterSelectSurvivorPreviewDisplayController PantheraCSSPreviewController;
        public static List<GameObject> allGameObjectActivations = new List<GameObject>();

        public static void RegisterSkins()
        {

            // Get the Prefabs //
            GameObject bodyPrefab = Prefab.CharacterPrefab;
            GameObject displayPrefab = Prefab.CharacterDisplayPrefab;

            // Get all Components //
            GameObject model = bodyPrefab.GetComponentInChildren<ModelLocator>().modelTransform.gameObject;
            CharacterModel characterModel = model.GetComponent<CharacterModel>();
            SkinnedMeshRenderer mainRenderer = characterModel.mainSkinnedMeshRenderer;
            ChildLocator childLocator = model.GetComponent<ChildLocator>();
            ChildLocator displayChildLocator = displayPrefab.GetComponent<ChildLocator>();
            CharacterModel.RendererInfo[] defaultRenderers = characterModel.baseRendererInfos;
            PantheraCSSPreviewController = displayPrefab.AddComponent<CharacterSelectSurvivorPreviewDisplayController>();
            PantheraCSSPreviewController.bodyPrefab = bodyPrefab;

            // Add the Skin Controler //
            ModelSkinController skinController = model.AddComponent<ModelSkinController>();

            // Create the SkinDefs List //
            List<SkinDef> skinDefs = new List<SkinDef>();

            // Create the Model1 SkinDef //
            SkinDef model1SkinDef = CreateSkinDef(PantheraTokens.Get("PANTHERA_MODEL_NAME_1"), PantheraAssets.Portrait1, defaultRenderers, mainRenderer, model);
            skinDefs.Add(model1SkinDef);

            // Save to the Skin Controller //
            skinController.skins = skinDefs.ToArray();

        }

        public static SkinDef CreateSkinDef(string skinName, Sprite skinIcon, CharacterModel.RendererInfo[] rendererInfos, SkinnedMeshRenderer mainRenderer, GameObject mainModel)
        {

            MethodInfo method = typeof(SkinDef).GetMethod(nameof(SkinDef.Awake), (BindingFlags)(-1));
            HookEndpointManager.Add(method, DoNothing);

            SkinDef skinDef = ScriptableObject.CreateInstance<SkinDef>();
            skinDef.baseSkins = Array.Empty<SkinDef>();
            skinDef.icon = skinIcon;
            skinDef.unlockableDef = null;
            skinDef.rootObject = mainModel;
            skinDef.rendererInfos = rendererInfos;
            skinDef.gameObjectActivations = new SkinDef.GameObjectActivation[0];
            skinDef.meshReplacements = new SkinDef.MeshReplacement[0];
            skinDef.projectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[0];
            skinDef.minionSkinReplacements = new SkinDef.MinionSkinReplacement[0];
            skinDef.nameToken = skinName;
            skinDef.name = skinName;

            HookEndpointManager.Remove(method, DoNothing);

            skinDef.Bake();
            return skinDef;
        }

        public static void DoNothing(Action<SkinDef> orig, SkinDef self)
        {

        }

        //public static int GetActualSkinIndex(NetworkUser nUser)
        //{
        //    BodyIndex bodyIndex = nUser.bodyIndexPreference;
        //    int index = 0;
        //    if (bodyIndex == null) return 0;
        //    Loadout.BodyLoadoutManager bodyLoadout = nUser?.networkLoadout?.loadout?.bodyLoadoutManager;
        //    if (bodyLoadout == null) return 0;
        //    index = (int)bodyLoadout.GetSkinIndex(bodyIndex);
        //    return index + 1;
        //}

    }
}
