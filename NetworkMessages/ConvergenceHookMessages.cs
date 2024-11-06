using Panthera.BodyComponents;
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

    public class ClientAddConvergenceHookComp : INetMessage
    {

        GameObject player;
        GameObject enemy;
        bool massive = false;

        public ClientAddConvergenceHookComp()
        {

        }

        public ClientAddConvergenceHookComp(GameObject player, GameObject enemy, bool massive = false)
        {
            this.player = player;
            this.enemy = enemy;
            this.massive = massive;
        }

        public void OnReceived()
        {
            if (this.player == null) return;
            PantheraObj ptraObj = this.player.GetComponent<PantheraObj>();
            if (ptraObj == null) return;
            ConvergenceHookComp comp = this.enemy.AddComponent<ConvergenceHookComp>();
            comp.ptraObj = ptraObj;
            comp.massive = this.massive;
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.player);
            writer.Write(this.enemy);
            writer.Write(this.massive);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.player = reader.ReadGameObject();
            this.enemy = reader.ReadGameObject();
            this.massive = reader.ReadBoolean();
        }

    }

    public class ServerActivateConvergenceHookComp : INetMessage
    {

        GameObject player;

        public ServerActivateConvergenceHookComp()
        {

        }

        public ServerActivateConvergenceHookComp(GameObject player)
        {
            this.player = player;
        }

        public void OnReceived()
        {
            if (this.player == null) return;
            PantheraObj ptraObj = this.player.GetComponent<PantheraObj>();
            if (ptraObj == null) return;
            foreach (ConvergenceHookComp comp in ptraObj.convergenceCompsList)
            {
                comp.activated = true;
            }
            new ClientActivateConvergenceHookComp(this.player).Send(NetworkDestination.Clients);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.player);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.player = reader.ReadGameObject();
        }

    }

    public class ClientActivateConvergenceHookComp : INetMessage
    {

        GameObject player;

        public ClientActivateConvergenceHookComp()
        {

        }

        public ClientActivateConvergenceHookComp(GameObject player)
        {
            this.player = player;
        }

        public void OnReceived()
        {
            if (this.player == null) return;
            PantheraObj ptraObj = this.player.GetComponent<PantheraObj>();
            if (ptraObj == null) return;
            foreach (ConvergenceHookComp comp in ptraObj.convergenceCompsList)
            {
                comp.activated = true;
            }
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.player);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.player = reader.ReadGameObject();
        }

    }

}
