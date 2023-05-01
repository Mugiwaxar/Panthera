using Panthera.BodyComponents;
using Panthera.Components;
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

    public class ServerZoneHealTargetComp : INetMessage
    {

        GameObject FXObject;
        float duration;
        float healRate;
        float healPercentAmount;

        public ServerZoneHealTargetComp()
        {

        }

        public ServerZoneHealTargetComp(GameObject FXObject, float duration, float healRate, float healPercentAmount)
        {
            this.FXObject = FXObject;
            this.duration = duration;
            this.healRate = healRate;
            this.healPercentAmount = healPercentAmount;
        }

        public void OnReceived()
        {
            if (this.FXObject == null) return;
            if (this.FXObject.GetComponent<ZoneHealComponent>() == null)
            {
                ZoneHealComponent comp = this.FXObject.AddComponent<ZoneHealComponent>();
                comp.duration = this.duration;
                comp.healRate = this.healRate;
                comp.healPercentAmount = this.healPercentAmount;
            }
            new ClientAttachZoneHealComp(this.FXObject, this.duration, this.healRate, this.healPercentAmount).Send(NetworkDestination.Clients);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.FXObject);
            writer.Write(this.duration);
            writer.Write(this.healRate);
            writer.Write(this.healPercentAmount);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.FXObject = reader.ReadGameObject();
            this.duration = reader.ReadSingle();
            this.healRate = reader.ReadSingle();
            this.healPercentAmount = reader.ReadSingle();
        }
    }

    public class ClientAttachZoneHealComp : INetMessage
    {

        GameObject FXObject;
        float duration;
        float healRate;
        float healPercentAmount;

        public ClientAttachZoneHealComp()
        {

        }

        public ClientAttachZoneHealComp(GameObject FXObject, float duration, float healRate, float healPercentAmount)
        {
            this.FXObject = FXObject;
            this.duration = duration;
            this.healRate = healRate;
            this.healPercentAmount = healPercentAmount;
        }

        public void OnReceived()
        {
            if (this.FXObject == null) return;
            if (this.FXObject.GetComponent<ZoneHealComponent>() == null)
            {
                ZoneHealComponent comp = this.FXObject.AddComponent<ZoneHealComponent>();
                comp.duration = this.duration;
                comp.healRate = this.healRate;
                comp.healPercentAmount = this.healPercentAmount;
            }
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.FXObject);
            writer.Write(this.duration);
            writer.Write(this.healRate);
            writer.Write(this.healPercentAmount);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.FXObject = reader.ReadGameObject();
            this.duration = reader.ReadSingle();
            this.healRate = reader.ReadSingle();
            this.healPercentAmount = reader.ReadSingle();
        }
    }

}
