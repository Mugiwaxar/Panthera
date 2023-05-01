using EntityStates;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.MachineScripts;
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

    class ClientChangePantheraScale : INetMessage
    {

        GameObject player;
        float scale;

        public ClientChangePantheraScale()
        {

        }

        public ClientChangePantheraScale(GameObject player, float scale)
        {
            this.player = player;
            this.scale = scale;
        }

        public void OnReceived()
        {
            PantheraObj ptraobj = this.player.GetComponent<PantheraObj>();
            if (ptraobj == null) return;
            Transform modelTransform = ptraobj.modelTransform;
            ptraobj.modelScale = scale;
            ptraobj.transform.localScale = new Vector3(scale, scale, scale);
            if (modelTransform == null) return;
            modelTransform.localScale = new Vector3(scale, scale, scale);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.player);
            writer.Write(this.scale);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.player = reader.ReadGameObject();
            this.scale = reader.ReadSingle();
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
            if (Util.HasEffectiveAuthority(this.creator) == true) return;
            GameObject effect = Utils.FXManager.CreateEffectInternal(this.creator, Utils.Prefabs.GetPrefab(this.assetPrefabID), this.origin, this.scale, this.parent, this.rotation, this.isModelTransform);
            Utils.FXManager.AddFX(this.ID, effect);
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

    class ClientDestroyEffect : INetMessage
    {

        public int ID;

        public ClientDestroyEffect()
        {

        }

        public ClientDestroyEffect(int ID)
        {
            this.ID = ID;
        }

        public void OnReceived()
        {
            Utils.FXManager.DestroyFX(this.ID);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.ID);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.ID = reader.ReadInt32();
        }

    }

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
            if (ptraObj == null || ptraObj.hasAuthority() == false) return;
            BigCatPassive bcp = ptraObj.getPassiveScript();
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

    class ClientPlayAnimation : INetMessage
    {

        public GameObject character;
        public string animationName;
        public float crossFadeTime;

        public ClientPlayAnimation()
        {

        }

        public ClientPlayAnimation(GameObject character, string animationName, float crossFadeTime)
        {
            this.character = character;
            this.animationName = animationName;
            this.crossFadeTime = crossFadeTime;
        }

        public void OnReceived()
        {
            if (this.character == null) return;
            if (Util.HasEffectiveAuthority(this.character) == true) return;
            PantheraObj ptraObj = this.character.GetComponent<PantheraObj>();
            if (ptraObj == null) return;
            Utils.Animation.PlayAnimation(ptraObj, this.animationName, this.crossFadeTime, false);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.character);
            writer.Write(this.animationName);
            writer.Write(this.crossFadeTime);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.character = reader.ReadGameObject();
            this.animationName = reader.ReadString();
            this.crossFadeTime = reader.ReadSingle();
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
            PantheraObj ptraObj = this.character.GetComponent<PantheraObj>();
            if (ptraObj == null) return;
            Utils.Animation.SetBoolean(ptraObj, this.paramName, this.setValue, false);
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
            PantheraObj ptraObj = this.character.GetComponent<PantheraObj>();
            if (ptraObj == null) return;
            Utils.Animation.SetFloat(ptraObj, this.paramName, this.value1, this.value2, this.value3, false);
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
        public bool ignoreAuthority;

        public ClientPlaySound()
        {

        }

        public ClientPlaySound(GameObject character, string soundName, bool ingloreAuthority = false)
        {
            this.character = character;
            this.soundName = soundName;
            this.ignoreAuthority = ingloreAuthority;
        }

        public ClientPlaySound(GameObject character, uint soundID, bool ingloreAuthority = false)
        {
            this.character = character;
            this.soundID = soundID;
            this.ignoreAuthority = ingloreAuthority;
        }

        public void OnReceived()
        {
            if (this.character == null) return;
            if (this.ignoreAuthority == false && Util.HasEffectiveAuthority(this.character) == true) return;
            if (this.soundName != "") Utils.Sound.playSound(this.soundName, this.character, false);
            else if (this.soundID != 0) Utils.Sound.playSound(this.soundID, this.character, false);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.character);
            writer.Write(this.soundName);
            writer.Write(this.soundID);
            writer.Write(this.ignoreAuthority);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.character = reader.ReadGameObject();
            this.soundName = reader.ReadString();
            this.soundID = reader.ReadUInt32();
            this.ignoreAuthority=reader.ReadBoolean();
        }

    }

    class ClientAddComboPoint :INetMessage
    {

        GameObject player;
        int number;

        public ClientAddComboPoint()
        {

        }

        public ClientAddComboPoint(GameObject player, int number)
        {
            this.player = player;
            this.number = number;
        }

        public void OnReceived()
        {
            if (Util.HasEffectiveAuthority(this.player) == false) return;
            PantheraObj ptraObj = this.player.GetComponent<PantheraObj>();
            if (ptraObj == null) return;
            ptraObj.characterBody.comboPoint += this.number;
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.player);
            writer.Write(this.number);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.player = reader.ReadGameObject();
            this.number = reader.ReadInt32();
        }
        
    }

}
