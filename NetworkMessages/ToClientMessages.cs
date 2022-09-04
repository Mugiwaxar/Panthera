using EntityStates;
using Panthera.Skills;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Panthera.NetworkMessages
{
    class ClientCharacterDieEvent : INetMessage
    {

        public GameObject attacker;
        public GameObject victime;

        public ClientCharacterDieEvent()
        {

        }

        public ClientCharacterDieEvent(GameObject attacker, GameObject victime)
        {
            this.attacker = attacker;
            this.victime = victime;
        }

        public void OnReceived()
        {
            if (this.attacker == null || this.victime == null) return;
            PantheraObj ptraObj = this.attacker.GetComponent<PantheraObj>();
            if (ptraObj == null || ptraObj.HasAuthority() == false) return;
            BigCatPassive bcp = ptraObj.GetPassiveScript();
            if (bcp == null) return;
            bcp.OnCharacterDie(this.attacker, this.victime);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.attacker);
            writer.Write(this.victime);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.attacker = reader.ReadGameObject();
            this.victime = reader.ReadGameObject();
        }

    }

    class ClientSpawnEffect : INetMessage
    {

        public GameObject creator;
        public GameObject assetPrefab;
        public Vector3 origin;
        public float scale;
        public GameObject parent;
        public Quaternion rotation;

        public ClientSpawnEffect()
        {

        }

        public ClientSpawnEffect(GameObject creator, GameObject assetPrefab, Vector3 origin, float scale, GameObject parent, Quaternion rotation)
        {
            this.creator = creator;
            this.assetPrefab = assetPrefab;
            this.origin = origin;
            this.scale = scale;
            this.parent = parent;
            this.rotation = rotation;
        }

        public void OnReceived()
        {
            if (this.creator == null || this.assetPrefab == null) return;
            if (Util.HasEffectiveAuthority(this.creator) == true) return;
            Utils.Functions.SpawnEffect(creator, assetPrefab, origin, scale, parent, rotation, false);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.creator);
            writer.Write(this.assetPrefab);
            writer.Write(this.origin);
            writer.Write(this.scale);
            if (this.parent != null) writer.Write(this.parent);
            writer.Write(this.rotation);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.creator = reader.ReadGameObject();
            this.assetPrefab = reader.ReadGameObject();
            this.origin = reader.ReadVector3();
            this.scale = reader.ReadSingle();
            this.parent = reader.ReadGameObject();
            this.rotation = reader.ReadQuaternion();
        }

    }

    class ClientPlayAnimation : INetMessage
    {

        public GameObject character;
        public string layerName;
        public string animationName;

        public ClientPlayAnimation()
        {

        }

        public ClientPlayAnimation(GameObject character, string layerName, string animationName)
        {
            this.character = character;
            this.layerName = layerName;
            this.animationName = animationName;
        }

        public void OnReceived()
        {
            if (this.character == null) return;
            if (Util.HasEffectiveAuthority(this.character) == true) return;
            Utils.Functions.PlayAnimation(character, layerName, animationName, false);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.character);
            writer.Write(this.layerName);
            writer.Write(this.animationName);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.character = reader.ReadGameObject();
            this.layerName = reader.ReadString();
            this.animationName = reader.ReadString();
        }
    }

    class ClientSetAnimatorBoolean : INetMessage
    {

        public GameObject character;
        public string paramName;
        public bool setValue;

        public ClientSetAnimatorBoolean()
        {

        }

        public ClientSetAnimatorBoolean (GameObject character, string paramName, bool setValue)
        {
            this.character = character;
            this.paramName = paramName;
            this.setValue = setValue;
        }

        public void OnReceived()
        {
            if (this.character == null) return;
            if (Util.HasEffectiveAuthority(this.character) == true) return;
            Utils.Functions.SetAnimatorBoolean(this.character, this.paramName, this.setValue, false);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.character);
            writer.Write(this.paramName);
            writer.Write(this.setValue);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.character = reader.ReadGameObject();
            this.paramName = reader.ReadString();
            this.setValue = reader.ReadBoolean();
        }

    }

    class ClientSetAnimatorFloat : INetMessage
    {

        public GameObject character;
        public string paramName;
        public float value1;
        public float value2;
        public float value3;

        public ClientSetAnimatorFloat()
        {

        }

        public ClientSetAnimatorFloat(GameObject character, string paramName, float value1, float value2, float value3)
        {
            this.character = character;
            this.paramName = paramName;
            this.value1 = value1;
            this.value2 = value2;
            this.value3 = value3;
        }

        public void OnReceived()
        {
            if (this.character == null) return;
            if (Util.HasEffectiveAuthority(this.character) == true) return;
            Utils.Functions.SetAnimatorFloat(this.character, this.paramName, this.value1, this.value2, this.value3, false);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.character);
            writer.Write(this.paramName);
            writer.Write(this.value1);
            writer.Write(this.value2);
            writer.Write(this.value3);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.character = reader.ReadGameObject();
            this.paramName = reader.ReadString();
            this.value1 = reader.ReadSingle();
            this.value2 = reader.ReadSingle();
            this.value3 = reader.ReadSingle();
        }

    }

    class ClientPlaySound : INetMessage
    {

        public GameObject character;
        public string soundName = "";
        public uint soundID = 0;

        public ClientPlaySound()
        {

        }

        public ClientPlaySound(GameObject character, string soundName)
        {
            this.character = character;
            this.soundName = soundName;
        }

        public ClientPlaySound(GameObject character, uint soundID)
        {
            this.character = character;
            this.soundID = soundID;
        }

        public void OnReceived()
        {
            if (this.character == null) return;
            if (Util.HasEffectiveAuthority(this.character) == true) return;
            if (this.soundName != "") Utils.Sound.playSound(this.soundName, this.character, false);
            else if (this.soundID != 0) Utils.Sound.playSound(this.soundID, this.character, false);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.character);
            writer.Write(this.soundName);
            writer.Write(this.soundID);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.character = reader.ReadGameObject();
            this.soundName = reader.ReadString();
            this.soundID = reader.ReadUInt32();
        }

    }

}
