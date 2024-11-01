using Panthera.Base;
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
using static UnityEngine.ParticleSystem;

namespace Panthera.NetworkMessages
{

    class ServerSetLeapTrailFX : INetMessage
    {

        GameObject player;
        bool setValue;

        public ServerSetLeapTrailFX()
        {

        }

        public ServerSetLeapTrailFX(GameObject player, bool setValue)
        {
            this.player = player;
            this.setValue = setValue;
        }

        public void OnReceived()
        {
            if (this.player == null) return;
            new ClientSetLeapTrailFX(this.player, this.setValue).Send(NetworkDestination.Clients);
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
            pantheraFX.leapTrailEmission.enabled = this.setValue;
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

    class ClientSetDashFX : INetMessage
    {

        GameObject player;
        bool setValue;

        public ClientSetDashFX()
        {

        }

        public ClientSetDashFX(GameObject player, bool setValue)
        {
            this.player = player;
            this.setValue = setValue;
        }

        public void OnReceived()
        {
            if (this.player == null) return;
            PantheraFX pantheraFX = this.player.GetComponent<PantheraFX>();
            if (pantheraFX == null) return;
            pantheraFX.dashEmission.enabled = this.setValue;
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

    class ServerSetStealthFX : INetMessage
    {

        GameObject player;
        bool setValue;

        public ServerSetStealthFX()
        {

        }

        public ServerSetStealthFX(GameObject player, bool setValue)
        {
            this.player = player;
            this.setValue = setValue;
        }

        public void OnReceived()
        {
            if (this.player == null) return;
            new ClientSetStealthFX(player, this.setValue).Send(NetworkDestination.Clients);
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

    class ClientSetStealthFX : INetMessage
    {

        GameObject player;
        bool setValue;

        public ClientSetStealthFX()
        {

        }

        public ClientSetStealthFX(GameObject player, bool setValue)
        {
            this.player = player;
            this.setValue = setValue;
        }

        public void OnReceived()
        {
            if (this.player == null) return;
            PantheraObj ptraObj = this.player.GetComponent<PantheraObj>();
            if (ptraObj == null) return;
            if (this.setValue == true) Utils.Functions.ToFadeMode(ptraObj.FindModelChild("Body").gameObject.GetComponent<SkinnedMeshRenderer>().material);
            else Utils.Functions.ToOpaqueMode(ptraObj.FindModelChild("Body").gameObject.GetComponent<SkinnedMeshRenderer>().material);
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

    class ServerSetFuryModeFX : INetMessage
    {

        GameObject player;
        bool setValue;

        public ServerSetFuryModeFX()
        {

        }

        public ServerSetFuryModeFX(GameObject player, bool setValue)
        {
            this.player = player;
            this.setValue = setValue;
        }

        public void OnReceived()
        {
            if (this.player == null) return;
            new ClientSetFuryModeFX(this.player, this.setValue).Send(NetworkDestination.Clients);
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

    class ClientSetFuryModeFX : INetMessage
    {

        GameObject player;
        bool setValue;

        public ClientSetFuryModeFX()
        {

        }

        public ClientSetFuryModeFX(GameObject player, bool setValue)
        {
            this.player = player;
            this.setValue = setValue;
        }

        public void OnReceived()
        {
            if (this.player == null) return;
            PantheraFX pantheraFX = this.player.GetComponent<PantheraFX>();
            if (pantheraFX == null) return;
            foreach (ParticleSystem ps in pantheraFX.furyAuraObj.GetComponentsInChildren<ParticleSystem>())
            {
                EmissionModule em = ps.emission;
                em.enabled = this.setValue;
            }
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

    class ServerSetGuardianModeFX : INetMessage
    {

        GameObject player;
        bool setValue;

        public ServerSetGuardianModeFX()
        {

        }

        public ServerSetGuardianModeFX(GameObject player, bool setValue)
        {
            this.player = player;
            this.setValue = setValue;
        }

        public void OnReceived()
        {
            if (this.player == null) return;
            new ClientSetGuardianModeFX(this.player, this.setValue).Send(NetworkDestination.Clients);
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

    class ClientSetGuardianModeFX : INetMessage
    {

        GameObject player;
        bool setValue;

        public ClientSetGuardianModeFX()
        {

        }

        public ClientSetGuardianModeFX(GameObject player, bool setValue)
        {
            this.player = player;
            this.setValue = setValue;
        }

        public void OnReceived()
        {
            if (this.player == null) return;
            PantheraFX pantheraFX = this.player.GetComponent<PantheraFX>();
            if (pantheraFX == null) return;
            foreach (ParticleSystem ps in pantheraFX.GuardianAuraObj.GetComponentsInChildren<ParticleSystem>())
            {
                EmissionModule em = ps.emission;
                em.enabled = this.setValue;
            }
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

    class ServerSetAmbitionModeFX : INetMessage
    {

        GameObject player;
        bool setValue;

        public ServerSetAmbitionModeFX()
        {

        }

        public ServerSetAmbitionModeFX(GameObject player, bool setValue)
        {
            this.player = player;
            this.setValue = setValue;
        }

        public void OnReceived()
        {
            if (this.player == null) return;
            new ClientSetAmbitionModeFX(this.player, this.setValue).Send(NetworkDestination.Clients);
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

    class ClientSetAmbitionModeFX : INetMessage
    {

        GameObject player;
        bool setValue;

        public ClientSetAmbitionModeFX()
        {

        }

        public ClientSetAmbitionModeFX(GameObject player, bool setValue)
        {
            this.player = player;
            this.setValue = setValue;
        }

        public void OnReceived()
        {
            if (this.player == null) return;
            PantheraFX pantheraFX = this.player.GetComponent<PantheraFX>();
            if (pantheraFX == null) return;
            foreach (ParticleSystem ps in pantheraFX.AmbitionAuraObj.GetComponentsInChildren<ParticleSystem>())
            {
                EmissionModule em = ps.emission;
                em.enabled = this.setValue;
            }
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
