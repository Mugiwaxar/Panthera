using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Panthera.NetworkMessages
{

    class ServerSpawnEffect : INetMessage
    {

        public int ID;
        public GameObject creator;
        public int assetPrefabID;
        public Vector3 origin;
        public float scale;
        public GameObject parent;
        public Quaternion rotation;
        public bool isModelTransform;

        public ServerSpawnEffect()
        {

        }

        public ServerSpawnEffect(int ID, GameObject creator, int assetPrefab, Vector3 origin, float scale, GameObject parent, Quaternion rotation, bool isModelTransform)
        {
            this.ID = ID;
            this.creator = creator;
            this.assetPrefabID = assetPrefab;
            this.origin = origin;
            this.scale = scale;
            this.parent = parent;
            this.rotation = rotation;
            this.isModelTransform = isModelTransform;
        }

        public void OnReceived()
        {
            if (this.creator == null) return;
            GameObject effect = Utils.FXManager.CreateEffectInternal(this.creator, Utils.Prefabs.GetPrefab(this.assetPrefabID), this.origin, this.scale, this.parent, this.rotation, this.isModelTransform);
            Utils.FXManager.AddEffectToList(this.ID, effect);
            new ClientSpawnEffect(ID, creator, this.assetPrefabID, origin, scale, parent, rotation, isModelTransform).Send(NetworkDestination.Clients);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.ID);
            writer.Write(this.creator);
            writer.Write(this.assetPrefabID);
            writer.Write(this.origin);
            writer.Write(this.scale);
            writer.Write(this.parent);
            writer.Write(this.rotation);
            writer.Write(this.isModelTransform);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.ID = reader.ReadInt32();
            this.creator = reader.ReadGameObject();
            this.assetPrefabID = reader.ReadInt32();
            this.origin = reader.ReadVector3();
            this.scale = reader.ReadSingle();
            this.parent = reader.ReadGameObject();
            this.rotation = reader.ReadQuaternion();
            this.isModelTransform = reader.ReadBoolean();
        }

    }

    class ClientSpawnEffect : INetMessage
    {

        public int ID;
        public GameObject creator;
        public int assetPrefabID;
        public Vector3 origin;
        public float scale;
        public GameObject parent;
        public Quaternion rotation;
        public bool isModelTransform;

        public ClientSpawnEffect()
        {

        }

        public ClientSpawnEffect(int ID, GameObject creator, int assetPrefab, Vector3 origin, float scale, GameObject parent, Quaternion rotation, bool isModelTransform)
        {
            this.ID = ID;
            this.creator = creator;
            this.assetPrefabID = assetPrefab;
            this.origin = origin;
            this.scale = scale;
            this.parent = parent;
            this.rotation = rotation;
            this.isModelTransform = isModelTransform;
        }

        public void OnReceived()
        {
            if (this.creator == null) return;
            if (Utils.FXManager.GetEffect(this.ID) != null) return;
            GameObject effect = Utils.FXManager.CreateEffectInternal(this.creator, Utils.Prefabs.GetPrefab(this.assetPrefabID), this.origin, this.scale, this.parent, this.rotation, this.isModelTransform);
            Utils.FXManager.AddEffectToList(this.ID, effect);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.ID);
            writer.Write(this.creator);
            writer.Write(this.assetPrefabID);
            writer.Write(this.origin);
            writer.Write(this.scale);
            writer.Write(this.parent);
            writer.Write(this.rotation);
            writer.Write(this.isModelTransform);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.ID = reader.ReadInt32();
            this.creator = reader.ReadGameObject();
            this.assetPrefabID = reader.ReadInt32();
            this.origin = reader.ReadVector3();
            this.scale = reader.ReadSingle();
            this.parent = reader.ReadGameObject();
            this.rotation = reader.ReadQuaternion();
            this.isModelTransform = reader.ReadBoolean();
        }

    }

    class ServerDestroyEffect : INetMessage
    {

        public int ID;
        public float delay;

        public ServerDestroyEffect()
        {

        }

        public ServerDestroyEffect(int ID, float delay)
        {
            this.ID = ID;
            this.delay = delay;
        }

        public void OnReceived()
        {
            Utils.FXManager.DestroyEffectInternal(this.ID, this.delay);
            new ClientDestroyEffect(this.ID, this.delay).Send(NetworkDestination.Clients);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.ID);
            writer.Write(this.delay);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.ID = reader.ReadInt32();
            this.delay = reader.ReadSingle();
        }

    }

    class ClientDestroyEffect : INetMessage
    {

        public int ID;
        public float delay;

        public ClientDestroyEffect()
        {

        }

        public ClientDestroyEffect(int ID, float delay)
        {
            this.ID = ID;
            this.delay = delay;
        }

        public void OnReceived()
        {
            Utils.FXManager.DestroyEffectInternal(this.ID, this.delay);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.ID);
            writer.Write(this.delay);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.ID = reader.ReadInt32();
            this.delay = reader.ReadSingle();
        }

    }

}