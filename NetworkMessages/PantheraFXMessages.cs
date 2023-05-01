using Panthera.Base;
using Panthera.BodyComponents;
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

    //class ClientSetShieldFX : INetMessage
    //{

    //    GameObject player;
    //    bool setValue;

    //    public ClientSetShieldFX()
    //    {

    //    }

    //    public ClientSetShieldFX(GameObject player, bool setValue)
    //    {
    //        this.player = player;
    //        this.setValue = setValue;
    //    }

    //    public void OnReceived()
    //    {
    //        if (this.player == null) return;
    //        PantheraFX pantheraFX = this.player.GetComponent<PantheraFX>();
    //        if (pantheraFX == null) return;
    //        Shield.SetShieldState(pantheraFX, this.setValue, true);
    //    }

    //    public void Serialize(NetworkWriter writer)
    //    {
    //        writer.Write(this.player);
    //        writer.Write(this.setValue);
    //    }

    //    public void Deserialize(NetworkReader reader)
    //    {
    //        this.player = reader.ReadGameObject();
    //        this.setValue = reader.ReadBoolean();
    //    }
    //}

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
            if (this.player == null || Util.HasEffectiveAuthority(this.player)) return;
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
            if (this.player == null || Util.HasEffectiveAuthority(this.player)) return;
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
            if (this.player == null || Util.HasEffectiveAuthority(this.player)) return;
            PantheraObj ptraObj = this.player.GetComponent<PantheraObj>();
            if (ptraObj == null) return;
            if (this.setValue == true) Utils.Functions.ToFadeMode(ptraObj.findModelChild("Body").gameObject.GetComponent<SkinnedMeshRenderer>().material);
            else Utils.Functions.ToOpaqueMode(ptraObj.findModelChild("Body").gameObject.GetComponent<SkinnedMeshRenderer>().material);
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
