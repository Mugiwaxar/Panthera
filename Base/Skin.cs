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

            // Create the Model2 SkinDef //
            SkinDef model2SkinDef = CreateSkinDef(PantheraTokens.Get("PANTHERA_MODEL_NAME_2"), PantheraAssets.Portrait2, defaultRenderers, mainRenderer, model);
            skinDefs.Add(model2SkinDef);

            // Create the Model3 SkinDef //
            SkinDef model3SkinDef = CreateSkinDef(PantheraTokens.Get("PANTHERA_MODEL_NAME_3"), PantheraAssets.Portrait3, defaultRenderers, mainRenderer, model);
            skinDefs.Add(model3SkinDef);

            // Create the Model4 SkinDef //
            SkinDef model4SkinDef = CreateSkinDef(PantheraTokens.Get("PANTHERA_MODEL_NAME_4"), PantheraAssets.Portrait4, defaultRenderers, mainRenderer, model);
            skinDefs.Add(model4SkinDef);

            // Save to the Skin Controller //
            skinController.skins = skinDefs.ToArray();

        }

        public static void SkinChangeHook(Action<RoR2.SurvivorMannequins.SurvivorMannequinSlotController> orig, RoR2.SurvivorMannequins.SurvivorMannequinSlotController self)
        {

            // Call the Original Function //
            orig(self);

            // Check if Panthera //
            if (self.currentSurvivorDef == null || self.currentSurvivorDef.bodyPrefab == null || self.currentSurvivorDef.bodyPrefab.name != "PantheraBody") return;

            // Get the Charater Display Model //
            CharacterModel displayModel = self.mannequinInstanceTransform.GetComponentInChildren<CharacterModel>();

            // Get the Skin Index //
            BodyIndex bodyIndexFromSurvivorIndex = SurvivorCatalog.GetBodyIndexFromSurvivorIndex(self.currentSurvivorDef.survivorIndex);
            int skinIndex = (int)self.currentLoadout.bodyLoadoutManager.GetSkinIndex(bodyIndexFromSurvivorIndex);

            // Get the Child Locator //
            ChildLocator childLocator = displayModel.childLocator;

            // Change the Model //
            if (skinIndex == 0) ChangedToDisplayModel1(childLocator);
            else if (skinIndex == 1) ChangedToDisplayModel2(childLocator);
            else if (skinIndex == 2) ChangedToDisplayModel3(childLocator);
            else if (skinIndex == 3) ChangedToDisplayModel4(childLocator);

        }

        public static void ChangedToDisplayModel1(ChildLocator displayChildLocator)
        {

            // Set Display Model Objects //
            displayChildLocator.FindChild("Model1ClawLeft").gameObject.SetActive(true);
            displayChildLocator.FindChild("Model1ClawRight").gameObject.SetActive(true);
            displayChildLocator.FindChild("Model1HeadEffect").gameObject.SetActive(true);
            displayChildLocator.FindChild("Model2ClawLeft").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model2ClawRight").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model2Eye1").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model2Eye2").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model2Head1").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model2Head2").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model2Head3").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model2Head4").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model2Tail1").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model2Tail2").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model2Tail3").gameObject.SetActive(false);
            displayChildLocator.FindChild("RedXIIIFire").gameObject.SetActive(false);

            // Change Display Mesh and Materials //
            SkinnedMeshRenderer displaySmr = displayChildLocator.FindChild("Body").GetComponent<SkinnedMeshRenderer>();
            Material[] displayMaterials = new Material[] { PantheraAssets.Body1Mat1, PantheraAssets.Body1Mat2 };
            displaySmr.sharedMaterials = displayMaterials;
            displaySmr.sharedMesh = PantheraAssets.BodyMesh1;

            // Remove Fade mode //
            foreach (Material mat in displayMaterials)
            {
                Utils.Functions.ToOpaqueMode(mat);
            }

        }

        public static void ChangedToDisplayModel2(ChildLocator displayChildLocator)
        {

            // Set Display Model Objects //
            displayChildLocator.FindChild("Model1ClawLeft").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model1ClawRight").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model1HeadEffect").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model2ClawLeft").gameObject.SetActive(true);
            displayChildLocator.FindChild("Model2ClawRight").gameObject.SetActive(true);
            displayChildLocator.FindChild("Model2Eye1").gameObject.SetActive(true);
            displayChildLocator.FindChild("Model2Eye2").gameObject.SetActive(true);
            displayChildLocator.FindChild("Model2Head1").gameObject.SetActive(true);
            displayChildLocator.FindChild("Model2Head2").gameObject.SetActive(true);
            displayChildLocator.FindChild("Model2Head3").gameObject.SetActive(true);
            displayChildLocator.FindChild("Model2Head4").gameObject.SetActive(true);
            displayChildLocator.FindChild("Model2Tail1").gameObject.SetActive(true);
            displayChildLocator.FindChild("Model2Tail2").gameObject.SetActive(true);
            displayChildLocator.FindChild("Model2Tail3").gameObject.SetActive(true);
            displayChildLocator.FindChild("RedXIIIFire").gameObject.SetActive(false);


            // Change Display Mesh and Materials //
            SkinnedMeshRenderer displaySmr = displayChildLocator.FindChild("Body").GetComponent<SkinnedMeshRenderer>();
            Material[] displayMaterials = new Material[] { PantheraAssets.Body2Mat1, PantheraAssets.Body2Mat2 };
            displaySmr.sharedMaterials = displayMaterials;
            displaySmr.sharedMesh = PantheraAssets.BodyMesh2;

            // Remove Fade mode //
            foreach (Material mat in displayMaterials)
            {
                Utils.Functions.ToOpaqueMode(mat);
            }

        }

        public static void ChangedToDisplayModel3(ChildLocator displayChildLocator)
        {

            // Set Display Model Objects //
            displayChildLocator.FindChild("Model1ClawLeft").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model1ClawRight").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model1HeadEffect").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model2ClawLeft").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model2ClawRight").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model2Eye1").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model2Eye2").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model2Head1").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model2Head2").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model2Head3").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model2Head4").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model2Tail1").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model2Tail2").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model2Tail3").gameObject.SetActive(false);
            displayChildLocator.FindChild("RedXIIIFire").gameObject.SetActive(false);


            // Change Display Mesh and Materials //
            SkinnedMeshRenderer displaySmr = displayChildLocator.FindChild("Body").GetComponent<SkinnedMeshRenderer>();
            Material[] displayMaterials = new Material[] { PantheraAssets.Body3Mat1, PantheraAssets.Body3Mat2 };
            displaySmr.sharedMaterials = displayMaterials;
            displaySmr.sharedMesh = PantheraAssets.BodyMesh3;

            // Remove Fade mode //
            foreach (Material mat in displayMaterials)
            {
                Utils.Functions.ToOpaqueMode(mat);
            }

        }

        public static void ChangedToDisplayModel4(ChildLocator displayChildLocator)
        {

            // Set Display Model Objects //
            displayChildLocator.FindChild("Model1ClawLeft").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model1ClawRight").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model1HeadEffect").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model2ClawLeft").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model2ClawRight").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model2Eye1").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model2Eye2").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model2Head1").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model2Head2").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model2Head3").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model2Head4").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model2Tail1").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model2Tail2").gameObject.SetActive(false);
            displayChildLocator.FindChild("Model2Tail3").gameObject.SetActive(false);
            displayChildLocator.FindChild("RedXIIIFire").gameObject.SetActive(true);


            // Change Display Mesh and Materials //
            SkinnedMeshRenderer displaySmr = displayChildLocator.FindChild("Body").GetComponent<SkinnedMeshRenderer>();
            Material[] displayMaterials = new Material[] { PantheraAssets.Body4Mat1, PantheraAssets.Body4Mat2, PantheraAssets.Body4Mat3, PantheraAssets.Body4Mat4, PantheraAssets.Body4Mat5, PantheraAssets.Body4Mat6, PantheraAssets.Body4Mat7,
                PantheraAssets.Body4Mat8, PantheraAssets.Body4Mat9, PantheraAssets.Body4Mat10, PantheraAssets.Body4Mat11, PantheraAssets.Body4Mat12, PantheraAssets.Body4Mat13 };
            displaySmr.sharedMaterials = displayMaterials;
            displaySmr.sharedMesh = PantheraAssets.BodyMesh4;

            // Remove Fade mode //
            foreach (Material mat in displayMaterials)
            {
                Utils.Functions.ToOpaqueMode(mat);
            }

        }

        public static void ChangedToMainModel1(ChildLocator childLocator)
        {

            // Set Model Objects //
            childLocator.FindChild("Model1ClawLeft").gameObject.SetActive(true);
            childLocator.FindChild("Model1ClawRight").gameObject.SetActive(true);
            childLocator.FindChild("Model1HeadEffect").gameObject.SetActive(true);
            childLocator.FindChild("Model2ClawLeft").gameObject.SetActive(false);
            childLocator.FindChild("Model2ClawRight").gameObject.SetActive(false);
            childLocator.FindChild("Model2Eye1").gameObject.SetActive(false);
            childLocator.FindChild("Model2Eye2").gameObject.SetActive(false);
            childLocator.FindChild("Model2Head1").gameObject.SetActive(false);
            childLocator.FindChild("Model2Head2").gameObject.SetActive(false);
            childLocator.FindChild("Model2Head3").gameObject.SetActive(false);
            childLocator.FindChild("Model2Head4").gameObject.SetActive(false);
            childLocator.FindChild("Model2Tail1").gameObject.SetActive(false);
            childLocator.FindChild("Model2Tail2").gameObject.SetActive(false);
            childLocator.FindChild("Model2Tail3").gameObject.SetActive(false);
            childLocator.FindChild("RedXIIIFire").gameObject.SetActive(false);

            // Change Mesh and Materials //
            SkinnedMeshRenderer smr = childLocator.FindChild("Body").GetComponent<SkinnedMeshRenderer>();
            Material[] materials = new Material[] { PantheraAssets.Body1Mat1, PantheraAssets.Body1Mat2 };
            smr.sharedMaterials = materials;
            smr.sharedMesh = PantheraAssets.BodyMesh1;

        }

        public static void ChangedToMainModel2(ChildLocator childLocator)
        {

            // Set Model Objects //
            childLocator.FindChild("Model1ClawLeft").gameObject.SetActive(false);
            childLocator.FindChild("Model1ClawRight").gameObject.SetActive(false);
            childLocator.FindChild("Model1HeadEffect").gameObject.SetActive(false);
            childLocator.FindChild("Model2ClawLeft").gameObject.SetActive(true);
            childLocator.FindChild("Model2ClawRight").gameObject.SetActive(true);
            childLocator.FindChild("Model2Eye1").gameObject.SetActive(true);
            childLocator.FindChild("Model2Eye2").gameObject.SetActive(true);
            childLocator.FindChild("Model2Head1").gameObject.SetActive(true);
            childLocator.FindChild("Model2Head2").gameObject.SetActive(true);
            childLocator.FindChild("Model2Head3").gameObject.SetActive(true);
            childLocator.FindChild("Model2Head4").gameObject.SetActive(true);
            childLocator.FindChild("Model2Tail1").gameObject.SetActive(true);
            childLocator.FindChild("Model2Tail2").gameObject.SetActive(true);
            childLocator.FindChild("Model2Tail3").gameObject.SetActive(true);
            childLocator.FindChild("RedXIIIFire").gameObject.SetActive(false);

            // Change Mesh and Materials //
            SkinnedMeshRenderer smr = childLocator.FindChild("Body").GetComponent<SkinnedMeshRenderer>();
            Material[] materials = new Material[] { PantheraAssets.Body2Mat1, PantheraAssets.Body2Mat2 };
            smr.sharedMaterials = materials;
            smr.sharedMesh = PantheraAssets.BodyMesh2;

        }

        public static void ChangedToMainModel3(ChildLocator childLocator)
        {

            // Set Model Objects //
            childLocator.FindChild("Model1ClawLeft").gameObject.SetActive(false);
            childLocator.FindChild("Model1ClawRight").gameObject.SetActive(false);
            childLocator.FindChild("Model1HeadEffect").gameObject.SetActive(false);
            childLocator.FindChild("Model2ClawLeft").gameObject.SetActive(false);
            childLocator.FindChild("Model2ClawRight").gameObject.SetActive(false);
            childLocator.FindChild("Model2Eye1").gameObject.SetActive(false);
            childLocator.FindChild("Model2Eye2").gameObject.SetActive(false);
            childLocator.FindChild("Model2Head1").gameObject.SetActive(false);
            childLocator.FindChild("Model2Head2").gameObject.SetActive(false);
            childLocator.FindChild("Model2Head3").gameObject.SetActive(false);
            childLocator.FindChild("Model2Head4").gameObject.SetActive(false);
            childLocator.FindChild("Model2Tail1").gameObject.SetActive(false);
            childLocator.FindChild("Model2Tail2").gameObject.SetActive(false);
            childLocator.FindChild("Model2Tail3").gameObject.SetActive(false);
            childLocator.FindChild("RedXIIIFire").gameObject.SetActive(false);

            // Change Mesh and Materials //
            SkinnedMeshRenderer smr = childLocator.FindChild("Body").GetComponent<SkinnedMeshRenderer>();
            Material[] materials = new Material[] { PantheraAssets.Body3Mat1, PantheraAssets.Body3Mat2 };
            smr.sharedMaterials = materials;
            smr.sharedMesh = PantheraAssets.BodyMesh3;

        }

        public static void ChangedToMainModel4(ChildLocator childLocator)
        {

            // Set Model Objects //
            childLocator.FindChild("Model1ClawLeft").gameObject.SetActive(false);
            childLocator.FindChild("Model1ClawRight").gameObject.SetActive(false);
            childLocator.FindChild("Model1HeadEffect").gameObject.SetActive(false);
            childLocator.FindChild("Model2ClawLeft").gameObject.SetActive(false);
            childLocator.FindChild("Model2ClawRight").gameObject.SetActive(false);
            childLocator.FindChild("Model2Eye1").gameObject.SetActive(false);
            childLocator.FindChild("Model2Eye2").gameObject.SetActive(false);
            childLocator.FindChild("Model2Head1").gameObject.SetActive(false);
            childLocator.FindChild("Model2Head2").gameObject.SetActive(false);
            childLocator.FindChild("Model2Head3").gameObject.SetActive(false);
            childLocator.FindChild("Model2Head4").gameObject.SetActive(false);
            childLocator.FindChild("Model2Tail1").gameObject.SetActive(false);
            childLocator.FindChild("Model2Tail2").gameObject.SetActive(false);
            childLocator.FindChild("Model2Tail3").gameObject.SetActive(false);
            childLocator.FindChild("RedXIIIFire").gameObject.SetActive(true);

            // Change Mesh and Materials //
            SkinnedMeshRenderer smr = childLocator.FindChild("Body").GetComponent<SkinnedMeshRenderer>();
            Material[] materials = new Material[] { PantheraAssets.Body4Mat1, PantheraAssets.Body4Mat2, PantheraAssets.Body4Mat3, PantheraAssets.Body4Mat4, PantheraAssets.Body4Mat5, PantheraAssets.Body4Mat6, PantheraAssets.Body4Mat7,
                PantheraAssets.Body4Mat8, PantheraAssets.Body4Mat9, PantheraAssets.Body4Mat10, PantheraAssets.Body4Mat11, PantheraAssets.Body4Mat12, PantheraAssets.Body4Mat13 };
            smr.sharedMaterials = materials;
            smr.sharedMesh = PantheraAssets.BodyMesh4;

        }


        public static SkinDef CreateSkinDef(string skinName, Sprite skinIcon, CharacterModel.RendererInfo[] rendererInfos, SkinnedMeshRenderer mainRenderer, GameObject mainModel)
        {
            SkinDefInfo skinDefInfo = new SkinDefInfo
            {
                BaseSkins = Array.Empty<SkinDef>(),
                GameObjectActivations = new SkinDef.GameObjectActivation[0],
                Icon = skinIcon,
                MeshReplacements = new SkinDef.MeshReplacement[0],
                MinionSkinReplacements = new SkinDef.MinionSkinReplacement[0],
                Name = skinName,
                NameToken = skinName,
                ProjectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[0],
                RendererInfos = rendererInfos,
                RootObject = mainModel,
                UnlockableDef = null
            };

            MethodInfo method = typeof(SkinDef).GetMethod(nameof(SkinDef.Awake), (BindingFlags)(-1));
            HookEndpointManager.Add(method, DoNothing);

            SkinDef skinDef = ScriptableObject.CreateInstance<SkinDef>();
            skinDef.baseSkins = skinDefInfo.BaseSkins;
            skinDef.icon = skinDefInfo.Icon;
            skinDef.unlockableDef = skinDefInfo.UnlockableDef;
            skinDef.rootObject = skinDefInfo.RootObject;
            skinDef.rendererInfos = skinDefInfo.RendererInfos;
            skinDef.gameObjectActivations = skinDefInfo.GameObjectActivations;
            skinDef.meshReplacements = skinDefInfo.MeshReplacements;
            skinDef.projectileGhostReplacements = skinDefInfo.ProjectileGhostReplacements;
            skinDef.minionSkinReplacements = skinDefInfo.MinionSkinReplacements;
            skinDef.nameToken = skinDefInfo.NameToken;
            skinDef.name = skinDefInfo.Name;

            HookEndpointManager.Remove(method, DoNothing);

            return skinDef;
        }

        public static void DoNothing(Action<SkinDef> orig, SkinDef self)
        {

        }

        public static int GetActualSkinIndex(NetworkUser nUser)
        {
            BodyIndex bodyIndex = nUser.bodyIndexPreference;
            int index = 0;
            if (bodyIndex == null) return 0;
            Loadout.BodyLoadoutManager bodyLoadout = nUser?.networkLoadout?.loadout?.bodyLoadoutManager;
            if (bodyLoadout == null) return 0;
            index = (int)bodyLoadout.GetSkinIndex(bodyIndex);
            return index + 1;
        }

    }
}
