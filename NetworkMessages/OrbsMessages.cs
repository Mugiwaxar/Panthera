using R2API.Networking.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;
using UnityEngine;
using RoR2.Orbs;
using RoR2;
using Panthera.Orbs;

namespace Panthera.NetworkMessages
{
    class ServerSpawnGoldOrb : INetMessage
    {

        GameObject origin;
        GameObject target;
        int amount;

        public ServerSpawnGoldOrb()
        {

        }

        public ServerSpawnGoldOrb(GameObject origin, GameObject target, int amount)
        {
            this.origin = origin;
            this.target = target;
            this.amount = amount;
        }

        public void OnReceived()
        {
            if (this.origin == null || this.target == null) return;
            GoldOrb goldOrb = new GoldOrb();
            goldOrb.origin = this.origin.transform.position;
            goldOrb.target = this.target.GetComponent<CharacterBody>().mainHurtBox;
            goldOrb.goldAmount = (uint)this.amount;
            OrbManager.instance.AddOrb(goldOrb);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.origin);
            writer.Write(this.target);
            writer.Write(this.amount);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.origin = reader.ReadGameObject();
            this.target = reader.ReadGameObject();
            this.amount = reader.ReadInt32();
        }
    
    }

    class ServerSpawnShieldOrb : INetMessage
    {

        GameObject origin;
        GameObject target;
        float amount;

        public ServerSpawnShieldOrb()
        {

        }

        public ServerSpawnShieldOrb(GameObject origin, GameObject target, float amount)
        {
            this.origin = origin;
            this.target = target;
            this.amount = amount;
        }

        public void OnReceived()
        {
            if (this.origin == null || this.target == null) return;
            ShieldOrb shieldOrb = new ShieldOrb();
            shieldOrb.origin = this.origin.transform.position;
            shieldOrb.target = this.target.GetComponent<CharacterBody>().mainHurtBox;
            shieldOrb.shieldValue = this.amount;
            OrbManager.instance.AddOrb(shieldOrb);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.origin);
            writer.Write(this.target);
            writer.Write(this.amount);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.origin = reader.ReadGameObject();
            this.target = reader.ReadGameObject();
            this.amount = reader.ReadSingle();
        }

    }

}
