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

    class ClientSyncCharacter : INetMessage
    {

        public GameObject character;
        public Dictionary<int, int> unlockedAbilitiesListNetwork = new Dictionary<int, int>();

        public float enduranceNetwork;
        public float forceNetwork;
        public float agilityNetwork;
        public float swiftnessNetwork;
        public float dexterityNetwork;
        public bool firstSync;

        public ClientSyncCharacter()
        {

        }

        public ClientSyncCharacter(GameObject character, Dictionary<int, int> unlockedAbilitiesList, float endurance, float force, float agility, float swiftness, float dexterity, bool firstSync)
        {
            this.character = character;
            this.unlockedAbilitiesListNetwork = unlockedAbilitiesList;
            this.enduranceNetwork = endurance;
            this.forceNetwork = force;
            this.agilityNetwork = agility;
            this.swiftnessNetwork = swiftness;
            this.dexterityNetwork = dexterity;
            this.firstSync = firstSync;
        }

        public void OnReceived()
        {
            if (this.character == null) return;
            PantheraObj ptraObj = character.GetComponent<PantheraObj>();
            if (ptraObj == null) return;
            ptraObj.unlockedAbilitiesListObjSyncCopy = new Dictionary<int, int>(this.unlockedAbilitiesListNetwork);
            PantheraBody ptraBody = ptraObj.characterBody;
            if (ptraBody == null) return;
            ptraBody.enduranceCopy = enduranceNetwork;
            ptraBody.forceCopy = forceNetwork;
            ptraBody.agilityCopy = agilityNetwork;
            ptraBody.swiftnessCopy = swiftnessNetwork;
            ptraBody.dexterityCopy = dexterityNetwork;
            ptraBody.RecalculateStats();
            if (this.firstSync == true)
                ptraObj.healthComponent.health = ptraBody.maxHealth;
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.character);
            writer.Write(this.unlockedAbilitiesListNetwork.Count);
            foreach (KeyValuePair<int, int> entry in this.unlockedAbilitiesListNetwork)
            {
                writer.Write(entry.Key);
                writer.Write(entry.Value);
            }
            writer.Write(this.enduranceNetwork);
            writer.Write(this.forceNetwork);
            writer.Write(this.agilityNetwork);
            writer.Write(this.swiftnessNetwork);
            writer.Write(this.dexterityNetwork);
            writer.Write(this.firstSync);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.unlockedAbilitiesListNetwork.Clear();
            this.character = reader.ReadGameObject();
            this.unlockedAbilitiesListNetwork = new Dictionary<int, int>();
            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                int key = reader.ReadInt32();
                int value = reader.ReadInt32();
                this.unlockedAbilitiesListNetwork[key] = value;
            }
            this.enduranceNetwork = reader.ReadSingle();
            this.forceNetwork = reader.ReadSingle();
            this.agilityNetwork = reader.ReadSingle();
            this.swiftnessNetwork = reader.ReadSingle();
            this.dexterityNetwork = reader.ReadSingle();
            this.firstSync = reader.ReadBoolean();
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

        public ServerPlaySound(GameObject character, string soundName, bool ingloreAuthority = false)
        {
            this.character = character;
            this.soundName = soundName;
            this.ignoreAuthority = ingloreAuthority;
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
        public int amount;

        public ClientAddFury()
        {

        }

        public ClientAddFury(GameObject player, int amount)
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
            this.amount = reader.ReadInt32();
        }

    }

    //class ClientAddComboPoint :INetMessage
    //{

    //    GameObject player;
    //    int number;

    //    public ClientAddComboPoint()
    //    {

    //    }

    //    public ClientAddComboPoint(GameObject player, int number)
    //    {
    //        this.player = player;
    //        this.number = number;
    //    }

    //    public void OnReceived()
    //    {
    //        if (Util.HasEffectiveAuthority(this.player) == false) return;
    //        PantheraObj ptraObj = this.player.GetComponent<PantheraObj>();
    //        if (ptraObj == null) return;
    //        ptraObj.characterBody.comboPoint += this.number;
    //    }

    //    public void Serialize(NetworkWriter writer)
    //    {
    //        writer.Write(this.player);
    //        writer.Write(this.number);
    //    }

    //    public void Deserialize(NetworkReader reader)
    //    {
    //        this.player = reader.ReadGameObject();
    //        this.number = reader.ReadInt32();
    //    }
        
    //}

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

}
