using R2API.Networking;
using RoR2.Audio;
using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Panthera.NetworkMessages;
using R2API.Networking.Interfaces;

namespace Panthera.Utils
{
    public class FXManager
    {

        // (int EffectID, GameObject Effect) This list represent all current spawned Effects linked with its ID //
        public static Dictionary<int, GameObject> EffectsList = new Dictionary<int, GameObject>();

        public static int GetID()
        {
            return ((int)(Time.time * 100)) * 1000 + UnityEngine.Random.Range(1, 1000);
        }

        public static void CleanList()
        {
            foreach (KeyValuePair<int, GameObject> entry in EffectsList.ToArray())
            {
                if (entry.Value == null) EffectsList.Remove(entry.Key);
            }
        }

        public static void AddFX(int ID, GameObject effect)
        {
            CleanList();
            if (EffectsList.ContainsKey(ID)) EffectsList[ID] = effect;
            else EffectsList.Add(ID, effect);
        }

        public static GameObject GetFX(int ID)
        {
            CleanList();
            if (EffectsList.ContainsKey(ID)) return EffectsList[ID];
            return null;
        }

        public static void DestroyFX(int ID)
        {
            CleanList();
            if (EffectsList.ContainsKey(ID))
            {
                GameObject.Destroy(EffectsList[ID]);
                EffectsList.Remove(ID);
            }
        }

        public static GameObject CreateEffectInternal(GameObject creator, GameObject prefab, Vector3 origin, float scale = 1, GameObject parent = null, Quaternion rotation = new Quaternion(), bool isModelTransform = false)
        {

            // If the Parent must be the Model Transform //
            if (isModelTransform == true)
                parent = parent.GetComponent<ModelLocator>().modelTransform.gameObject;

            // Create the effect data //
            EffectData effectData = new EffectData();
            effectData.origin = origin;
            effectData.scale = scale;
            effectData.rotation = rotation;

            // Find the effect index //
            EffectIndex effectIndex = EffectCatalog.FindEffectIndexFromPrefab(prefab);

            // Start the sound //
            if (effectData.networkSoundEventIndex != NetworkSoundEventIndex.Invalid)
            {
                PointSoundManager.EmitSoundLocal(NetworkSoundEventCatalog.GetAkIdFromNetworkSoundEventIndex(effectData.networkSoundEventIndex), effectData.origin);
            }

            // Get the effect def //
            EffectDef effectDef = EffectCatalog.GetEffectDef(effectIndex);
            if (effectDef == null)
            {
                return null;
            }

            // Play the sound attached to the effect def //
            string spawnSoundEventName = effectDef.spawnSoundEventName;
            if (!string.IsNullOrEmpty(spawnSoundEventName))
            {
                PointSoundManager.EmitSoundLocal((AkEventIdArg)spawnSoundEventName, origin);
            }

            // Play the sound related to the surface where the effect is spawned //
            SurfaceDef surfaceDef = SurfaceDefCatalog.GetSurfaceDef(effectData.surfaceDefIndex);
            if (surfaceDef != null)
            {
                string impactSoundString = surfaceDef.impactSoundString;
                if (!string.IsNullOrEmpty(impactSoundString))
                {
                    PointSoundManager.EmitSoundLocal((AkEventIdArg)impactSoundString, effectData.origin);
                }
            }

            // Check if the effect can be spawned //
            if (!VFXBudget.CanAffordSpawn(effectDef.prefabVfxAttributes))
            {
                return null;
            }

            // Check the culling method ? //
            if (effectDef.cullMethod != null && !effectDef.cullMethod(effectData))
            {
                return null;
            }

            // Clone the effect data //
            EffectData effectData2 = effectData.Clone();

            // Instantiate the effect //
            GameObject effectObject = GameObject.Instantiate<GameObject>(effectDef.prefab, effectData2.origin, effectData2.rotation);

            EffectComponent component = effectObject.GetComponent<EffectComponent>();

            // Set the effect data of the component with the clone //
            if (component)
            {
                component.effectData = effectData2.Clone();
            }

            // Set the parrent //
            if (parent != null) effectObject.transform.parent = parent.transform;

            // Set the Position/Rotation/Scale //
            effectObject.transform.position = origin;
            effectObject.transform.rotation = rotation;
            effectObject.transform.localScale = new Vector3(scale, scale, scale);

            // Return the effect object //
            return effectObject;

        }

        public static int SpawnEffect(GameObject creator, GameObject prefab, Vector3 origin, float scale = 1, GameObject parent = null, Quaternion rotation = new Quaternion(), bool isModelTransform = false, bool emit = true)
        {

            // The effect can only be created by the client //
            if (NetworkClient.active == false) return 0;

            // Create the Effect //
            int ID = GetID();
            GameObject effect = CreateEffectInternal(creator, prefab, origin, scale, parent, rotation, isModelTransform);

            // Save the effect into the List //
            AddFX(ID, effect);

            // Check if Multiplayer //
            if (RoR2Application.isInMultiPlayer == true && emit == true)
                new ClientSpawnEffect(ID, creator, Utils.Prefabs.GetIndex(prefab), origin, scale, parent, rotation, isModelTransform).Send(NetworkDestination.Clients);

            return ID;

        }

    }
}
