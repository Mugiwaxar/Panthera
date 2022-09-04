using Panthera.Components;
using Panthera.Passives;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Panthera.NetworkMessages
{
 
    class ClientSetShieldFX : INetMessage
    {

        GameObject player;
        bool setValue;

        public ClientSetShieldFX()
        {

        }

        public ClientSetShieldFX(GameObject player, bool setValue)
        {
            this.player = player;
            this.setValue = setValue;
        }

        public void OnReceived()
        {
            if (this.player == null) return;
            PantheraFX pantheraFX = this.player.GetComponent<PantheraFX>();
            if (pantheraFX == null) return;
            Shield.SetShieldState(pantheraFX, this.setValue, true);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.player);
            writer.Write(this.setValue);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.player = reader.ReadGameObject();
            this.setValue = reader.ReadBoolean();
        }
    }

    class ClientSetLeapTrailFX: INetMessage
    {

        GameObject player;
        bool setValue;

        public ClientSetLeapTrailFX()
        {

        }

        public ClientSetLeapTrailFX(GameObject player, bool setValue)
        {
            this.player = player;
            this.setValue = setValue;
        }

        public void OnReceived()
        {
            if (this.player == null) return;
            PantheraFX pantheraFX = this.player.GetComponent<PantheraFX>();
            if (pantheraFX == null) return;
            pantheraFX.ServerSetLeapTrailFX(this.setValue);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.player);
            writer.Write(this.setValue);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.player = reader.ReadGameObject();
            this.setValue = reader.ReadBoolean();
        }
    }

    class ClientSetSuperSprintFX : INetMessage
    {

        GameObject player;
        bool setValue;

        public ClientSetSuperSprintFX()
        {

        }

        public ClientSetSuperSprintFX(GameObject player, bool setValue)
        {
            this.player = player;
            this.setValue = setValue;
        }

        public void OnReceived()
        {
            if (this.player == null) return;
            PantheraFX pantheraFX = this.player.GetComponent<PantheraFX>();
            if (pantheraFX == null) return;
            pantheraFX.ServerSetSuperSprintFX(this.setValue);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.player);
            writer.Write(this.setValue);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.player = reader.ReadGameObject();
            this.setValue = reader.ReadBoolean();
        }
    }

    class ClientSetStealFX : INetMessage
    {

        GameObject player;
        bool setValue;

        public ClientSetStealFX()
        {

        }

        public ClientSetStealFX(GameObject player, bool setValue)
        {
            this.player = player;
            this.setValue = setValue;
        }

        public void OnReceived()
        {
            if (this.player == null) return;
            PantheraFX pantheraFX = this.player.GetComponent<PantheraFX>();
            if (pantheraFX == null) return;
            pantheraFX.ServerSetStealFX(this.setValue);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.player);
            writer.Write(this.setValue);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.player = reader.ReadGameObject();
            this.setValue = reader.ReadBoolean();
        }
    }

}
