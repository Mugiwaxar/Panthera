using EntityStates;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.MachineScripts;
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

    class ClientSyncProfile : INetMessage
    {

        public GameObject player;
        public Dictionary<String, int> dico = new Dictionary<String, int>();

        public ClientSyncProfile()
        {

        }

        public ClientSyncProfile(GameObject player, Dictionary<string, int> dico)
        {
            this.player = player;
            this.dico = dico;
        }

        public void OnReceived()
        {
            if (this.player == null) return;
            PantheraObj ptraObj = player.GetComponent<PantheraObj>();
            if (ptraObj == null)
                return;
            if (ptraObj.HasAuthority() == true)
            {
                ptraObj.characterBody.RecalculateStats();
                return;
            }
            ProfileComponent profileComp = ptraObj.profileComponent;
            if (profileComp == null)
            {
                profileComp = this.player.AddComponent<ProfileComponent>();
                ptraObj.profileComponent = profileComp;
            }
            profileComp.deserialize(this.dico);
            ptraObj.characterBody.RecalculateStats();
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.player);
            writer.Write(this.dico.Count);
            foreach (KeyValuePair<String, int> entry in this.dico)
            {
                writer.Write(entry.Key);
                writer.Write(entry.Value);
            }

        }

        public void Deserialize(NetworkReader reader)
        {
            this.dico.Clear();
            this.player = reader.ReadGameObject();
            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                string key = reader.ReadString();
                int value = reader.ReadInt32();
                this.dico.Add(key, value);
            }
        }

    }

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
            PantheraObj ptraObj = this.player.GetComponent<PantheraObj>();
            if (ptraObj == null || ptraObj.HasAuthority() == true) return;
            Transform modelTransform = ptraObj.modelTransform;
            ptraObj.modelScale = scale;
            ptraObj.transform.localScale = new Vector3(scale, scale, scale);
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
            bcp.OnCharacterDieEventClient(this.attacker, this.victime);
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

    class ServerPlayAnimation : INetMessage
    {

        public GameObject character;
        public string animationName;
        public float crossFadeTime;

        public ServerPlayAnimation()
        {

        }

        public ServerPlayAnimation(GameObject character, string animationName, float crossFadeTime)
        {
            this.character = character;
            this.animationName = animationName;
            this.crossFadeTime = crossFadeTime;
        }

        public void OnReceived()
        {
            if (this.character == null) return;
            new ClientPlayAnimation(this.character, this.animationName, this.crossFadeTime).Send(NetworkDestination.Clients);
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

    class ServerSetAnimatorBoolean : INetMessage
    {

        public GameObject character;
        public string paramName;
        public bool setValue;

        public ServerSetAnimatorBoolean()
        {

        }

        public ServerSetAnimatorBoolean(GameObject character, string paramName, bool setValue)
        {
            this.character = character;
            this.paramName = paramName;
            this.setValue = setValue;
        }

        public void OnReceived()
        {
            if (this.character == null) return;
            new ClientSetAnimatorBoolean(this.character, this.paramName, this.setValue).Send(NetworkDestination.Clients);
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

    class ServerSetAnimatorFloat : INetMessage
    {

        public GameObject character;
        public string paramName;
        public float value1;
        public float value2;
        public float value3;

        public ServerSetAnimatorFloat()
        {

        }

        public ServerSetAnimatorFloat(GameObject character, string paramName, float value1, float value2, float value3)
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
            new ClientSetAnimatorFloat(this.character, this.paramName, this.value1, this.value2, this.value3).Send(NetworkDestination.Clients);
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

    class ServerPlaySound : INetMessage
    {

        public GameObject character;
        public string soundName;
        public bool ignoreAuthority;

        public ServerPlaySound()
        {

        }

        public ServerPlaySound(GameObject character, string soundName, bool ignoreAuthority = false)
        {
            this.character = character;
            this.soundName = soundName;
            this.ignoreAuthority = ignoreAuthority;
        }

        public void OnReceived()
        {
            new ClientPlaySound(this.character, this.soundName, this.ignoreAuthority).Send(NetworkDestination.Clients);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.character);
            writer.Write(this.soundName);
            writer.Write(this.ignoreAuthority);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.character = reader.ReadGameObject();
            this.soundName = reader.ReadString();
            this.ignoreAuthority = reader.ReadBoolean();
        }

    }

    class ClientPlaySound : INetMessage
    {

        public GameObject character;
        public string soundName;
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

        public void OnReceived()
        {
            if (this.character == null) return;
            if (this.ignoreAuthority == false && Util.HasEffectiveAuthority(this.character) == true) return;
            if (this.soundName != "") Utils.Sound.playSound(this.soundName, this.character, false);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.character);
            writer.Write(this.soundName);
            writer.Write(this.ignoreAuthority);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.character = reader.ReadGameObject();
            this.soundName = reader.ReadString();
            this.ignoreAuthority = reader.ReadBoolean();
        }

    }

    class ClientAddFury : INetMessage
    {

        public GameObject player;
        public float amount;

        public ClientAddFury()
        {

        }

        public ClientAddFury(GameObject player, float amount)
        {
            this.player = player;
            this.amount = amount;
        }

        public void OnReceived()
        {
            if (this.player == null || Util.HasEffectiveAuthority(this.player) == false) return;
            PantheraBody body = this.player.GetComponent<PantheraBody>();
            if (body == null) return;
            body.fury += amount;
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.player);
            writer.Write(this.amount);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.player = reader.ReadGameObject();
            this.amount = reader.ReadSingle();
        }

    }

    class ClientAddShield : INetMessage
    {

        public GameObject player;
        public float amount;

        public ClientAddShield()
        {

        }

        public ClientAddShield(GameObject player, float amount)
        {
            this.player = player;
            this.amount = amount;
        }

        public void OnReceived()
        {
            if (this.player == null || Util.HasEffectiveAuthority(this.player) == false) return;
            PantheraBody body = this.player.GetComponent<PantheraBody>();
            if (body == null) return;
            body.frontShield += amount;
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.player);
            writer.Write(this.amount);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.player = reader.ReadGameObject();
            this.amount = reader.ReadSingle();
        }

    }


    class ClientSetBodyVelocity : INetMessage
    {

        public GameObject target;
        public Vector3 velocity;

        public ClientSetBodyVelocity()
        {

        }

        public ClientSetBodyVelocity(GameObject target, Vector3 velocity)
        {
            this.target = target;
            this.velocity = velocity;
        }

        public void OnReceived()
        {
            if (this.target == null) return;
            CharacterMotor motor = this.target.GetComponent<CharacterMotor>();
            if (motor == null) return;
            motor.velocity = velocity;
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.target);
            writer.Write(this.velocity);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.target = reader.ReadGameObject();
            this.velocity = reader.ReadVector3();
        }

    }

    public class ClientSetPortalSurge : INetMessage
    {

        public GameObject target;
        public GameObject portal;
        public bool set;

        public ClientSetPortalSurge()
        {

        }

        public ClientSetPortalSurge(GameObject target, GameObject portal, bool set)
        {
            this.target = target;
            this.portal = portal;
            this.set = set;
        }

        public void OnReceived()
        {
            Base.PantheraCombatDirector.SetSurged(this.portal, this.set);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.target);
            writer.Write(this.portal);
            writer.Write(this.set);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.target = reader.ReadGameObject();
            this.portal = reader.ReadGameObject();
            this.set = reader.ReadBoolean();
        }

    }

}
