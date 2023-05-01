using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Utils
{
    internal class Prefabs
    {
        // (PrefabID, Prefab Object) The list of all Prefabs used for the mod, this will help for networking //
        public static Dictionary<int, GameObject> PrefabsList = new Dictionary<int, GameObject>();

        // Index are automaticaly assigned //
        public static int index = 1;

        public static void AddPrefab(GameObject prefab)
        {
            PrefabsList.Add(index, prefab);
            index++;
        }

        public static GameObject GetPrefab(int index)
        {
            if (PrefabsList.ContainsKey(index)) return PrefabsList[index];
            return null;
        }

        public static int GetIndex(GameObject prefab)
        {
            foreach (KeyValuePair<int, GameObject> entry in PrefabsList)
            {
                if (entry.Value == prefab)
                    return entry.Key;
            }
            return 0;
        }

    }
}
