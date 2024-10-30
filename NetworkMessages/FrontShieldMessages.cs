using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.MachineScripts;
using Panthera.Passives;
using Panthera.OldSkills;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Panthera.Skills.Passives;

namespace Panthera.NetworkMessages
{

    public class ServerSetFrontShieldActive : INetMessage
    {

        GameObject character;
        bool set;

        public ServerSetFrontShieldActive()
        {

        }

        public ServerSetFrontShieldActive(GameObject character, bool set)
        {
            this.character = character;
            this.set = set;
        }

        public void OnReceived()
        {
            if (this.character == null) return;
            PantheraObj ptraObj = this.character.GetComponent<PantheraObj>();
            if (ptraObj == null) return;
            if (this.set == true)
                ptraObj.frontShieldObj.SetActive(true);
            else if (this.set == false && ptraObj.frontShieldObj != null)
                ptraObj.frontShieldObj.SetActive(false);
            ptraObj.characterBody.RecalculateStats();
            new ClientSetFrontShieldActive(this.character, this.set).Send(NetworkDestination.Clients);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.character);
            writer.Write(this.set);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.character = reader.ReadGameObject();
            this.set = reader.ReadBoolean();
        }

    }

    public class ClientSetFrontShieldActive : INetMessage
    {

        GameObject character;
        bool set;

        public ClientSetFrontShieldActive()
        {

        }

        public ClientSetFrontShieldActive(GameObject character, bool set)
        {
            this.character = character;
            this.set = set;
        }

        public void OnReceived()
        {
            if (this.character == null || Util.HasEffectiveAuthority(this.character) == true) return;
            PantheraObj ptraObj = this.character.GetComponent<PantheraObj>();
            if (ptraObj == null) return;
            if (this.set == true)
                ptraObj.frontShieldObj.SetActive(true);
            else if (this.set == false && ptraObj.frontShieldObj != null)
                ptraObj.frontShieldObj.SetActive(false);
            ptraObj.frontShieldObj.layer = LayerIndex.entityPrecise.intVal;
            ptraObj.frontShieldObj.transform.FindChild("WorldHitBox").gameObject.layer = LayerIndex.world.intVal;
            ptraObj.characterBody.RecalculateStats();
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.character);
            writer.Write(this.set);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.character = reader.ReadGameObject();
            this.set = reader.ReadBoolean();
        }

    }

    public class ServerSetFrontShieldDeployed : INetMessage
    {

        GameObject character;
        bool set;

        public ServerSetFrontShieldDeployed()
        {

        }

        public ServerSetFrontShieldDeployed(GameObject character, bool set)
        {
            this.character = character;
            this.set = set;
        }

        public void OnReceived()
        {
            if (this.character == null) return;
            PantheraObj ptraObj = this.character.GetComponent<PantheraObj>();
            if (ptraObj == null) return;
            ptraObj.frontShieldDeployed = this.set;
            new ClientSetFrontShieldDeployed(this.character, this.set).Send(NetworkDestination.Clients);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.character);
            writer.Write(this.set);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.character = reader.ReadGameObject();
            this.set = reader.ReadBoolean();
        }

    }

    public class ClientSetFrontShieldDeployed : INetMessage
    {

        GameObject character;
        bool set;

        public ClientSetFrontShieldDeployed()
        {

        }

        public ClientSetFrontShieldDeployed(GameObject character, bool set)
        {
            this.character = character;
            this.set = set;
        }

        public void OnReceived()
        {
            if (this.character == null) return;
            PantheraObj ptraObj = this.character.GetComponent<PantheraObj>();
            if (ptraObj == null) return;
            ptraObj.frontShieldDeployed = this.set;
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.character);
            writer.Write(this.set);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.character = reader.ReadGameObject();
            this.set = reader.ReadBoolean();
        }

    }

    public class ClientDamageShield : INetMessage
    {

        GameObject player;
        float damage;

        public ClientDamageShield()
        {

        }

        public ClientDamageShield(GameObject character, float damage)
        {
            this.player = character;
            this.damage = damage;
        }

        public void OnReceived()
        {
            if (this.player == null || Util.HasEffectiveAuthority(this.player) == false) return;
            PantheraObj ptraObj = this.player.GetComponent<PantheraObj>();
            if (ptraObj == null) return;
            Skills.Passives.FrontShield.DamageShield(ptraObj, this.damage);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.player);
            writer.Write(this.damage);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.player = reader.ReadGameObject();
            this.damage = reader.ReadSingle();
        }

    }

    public class ServerSetFrontShieldAmount : INetMessage
    {

        GameObject character;
        float shield;

        public ServerSetFrontShieldAmount()
        {

        }

        public ServerSetFrontShieldAmount(GameObject character, float shield)
        {
            this.character = character;
            this.shield = shield;
        }

        public void OnReceived()
        {
            if (this.character == null) return;
            PantheraBody body = character.GetComponent<PantheraBody>();
            if (body == null) return;
            body.frontShield = shield;
            new ClientSetFrontShieldAmount(this.character, this.shield).Send(NetworkDestination.Clients);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.character);
            writer.Write(this.shield);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.character = reader.ReadGameObject();
            this.shield = reader.ReadSingle();
        }

    }

    public class ClientSetFrontShieldAmount : INetMessage
    {

        GameObject character;
        float shield;

        public ClientSetFrontShieldAmount()
        {

        }

        public ClientSetFrontShieldAmount(GameObject character, float shield)
        {
            this.character = character;
            this.shield = shield;
        }

        public void OnReceived()
        {
            if (this.character == null || Util.HasEffectiveAuthority(this.character) == true) return;
            PantheraBody body = character.GetComponent<PantheraBody>();
            if (body == null) return;
            body.frontShield = shield;
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.character);
            writer.Write(this.shield);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.character = reader.ReadGameObject();
            this.shield = reader.ReadSingle();
        }

    }

}
