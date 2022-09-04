using Panthera.Components;
using R2API.Networking;
using R2API.Networking.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Panthera.NetworkMessages
{
    public class ServerAttachHoldTargetComp : INetMessage
    {

        GameObject player;
        GameObject target;
        float relativeDistance;

        public ServerAttachHoldTargetComp()
        {

        }

        public ServerAttachHoldTargetComp(GameObject player, GameObject target, float relativeDistance)
        {
            this.player = player;
            this.target = target;
            this.relativeDistance = relativeDistance;
        }

        public void OnReceived()
        {
            if (this.player == null && this.target == null) return;
            PantheraObj ptraObj = this.player.GetComponent<PantheraObj>();
            if (ptraObj == null) return;
            if (this.target.GetComponent<HoldTarget>() != null) return;
            HoldTarget comp = this.target.AddComponent<HoldTarget>();
            comp.ptraObj = ptraObj;
            comp.relativeDistance = this.relativeDistance;
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.player);
            writer.Write(this.target);
            writer.Write(this.relativeDistance);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.player = reader.ReadGameObject();
            this.target = reader.ReadGameObject();
            this.relativeDistance = reader.ReadSingle();
        }
    }

    public class ServerDetachHoldTargetComp : INetMessage
    {

        public GameObject target;

        public ServerDetachHoldTargetComp()
        {

        }

        public ServerDetachHoldTargetComp(GameObject target)
        {
            this.target = target;
        }

        public void OnReceived()
        {
            if (target == null) return;
            GameObject.DestroyImmediate(target.GetComponent<HoldTarget>());
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.target);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.target = reader.ReadGameObject();
        }

    }

    public class ServerHoldTargetLastPosition : INetMessage
    {

        public GameObject target;
        public Vector3 lastPosition;

        public ServerHoldTargetLastPosition()
        {

        }

        public ServerHoldTargetLastPosition(GameObject target, Vector3 lastPosition)
        {
            this.target = target;
            this.lastPosition = lastPosition;
        }

        public void OnReceived()
        {
            if (target == null) return;
            target.transform.position = this.lastPosition;
            Debug.LogWarning("Set last position");
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.target);
            writer.Write(this.lastPosition);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.target = reader.ReadGameObject();
            this.lastPosition = reader.ReadVector3();
        }

    }

}
