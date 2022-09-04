using Panthera.Components;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Panthera.NetworkMessages
{

    class ServerKeepThePrey : INetMessage
    {

        public GameObject player;
        public GameObject target;
        public float distance;

        public ServerKeepThePrey()
        {

        }

        public ServerKeepThePrey(GameObject player, GameObject target, float distance)
        {
            this.player = player;
            this.target = target;
            this.distance = distance;
        }

        public void OnReceived()
        {
            if (this.player == null || this.target == null) return;
            PantheraObj ptraObj = this.player.GetComponent<PantheraObj>();
            CharacterBody body = this.player.GetComponent<CharacterBody>();
            CharacterBody targetBody = this.target.GetComponent<CharacterBody>();
            CharacterDirection direction = this.player.GetComponent<CharacterDirection>();
            if (ptraObj == null || body == null || direction == null) return;
            ptraObj.holdedPrey = targetBody;
            HoldedPrey holdedPrey = this.target.AddComponent<HoldedPrey>();
            holdedPrey.holdedPreyDistance = this.distance;
            holdedPrey.playerBody = body;
            holdedPrey.playerDirection = direction;
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.player);
            writer.Write(this.target);
            writer.Write(this.distance);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.player = reader.ReadGameObject();
            this.target = reader.ReadGameObject();
            this.distance = reader.ReadSingle();
        }

    }

    class ServerReleasePrey : INetMessage
    {

        public GameObject player;
        public GameObject target;
        public Vector3 position;

        public ServerReleasePrey()
        {

        }

        public ServerReleasePrey(GameObject player, GameObject target, Vector3 position)
        {
            this.player = player;
            this.target = target;
            this.position = position;
        }

        public void OnReceived()
        {
            if (this.player == null || this.target == null) return;
            PantheraObj ptraObj = this.player.GetComponent<PantheraObj>();
            HoldedPrey holdedPrey = this.target.GetComponent<HoldedPrey>();
            if (ptraObj == null || holdedPrey == null) return;
            GameObject.DestroyImmediate(holdedPrey);
            ptraObj.holdedPrey = null;
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.player);
            writer.Write(this.target);
            writer.Write(this.position);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.player = reader.ReadGameObject();
            this.target = reader.ReadGameObject();
            this.position = reader.ReadVector3();
        }

    }
}
