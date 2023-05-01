using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using R2API.Networking.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Panthera.NetworkMessages
{
    //class ServerSetRaySlashCharge : INetMessage
    //{

    //    public GameObject player;
    //    public bool setValue;

    //    public ServerSetRaySlashCharge()
    //    {

    //    }

    //    public ServerSetRaySlashCharge(GameObject player, bool setValue)
    //    {
    //        this.player = player;
    //        this.setValue = setValue;
    //    }


    //    public void OnReceived()
    //    {
    //        if (this.player == null) return;
    //        PantheraObj ptraObj = this.player.GetComponent<PantheraObj>();
    //        if (ptraObj == null) return;
    //        ptraObj.onRaySlashCharge = this.setValue;
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
}
