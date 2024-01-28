using Panthera.BodyComponents;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;
using UnityEngine;
using R2API.Networking;

namespace Panthera.NetworkMessages
{
    public class ServerStealthMessage : INetMessage
    {

        public GameObject character;
        public bool setValue;

        public ServerStealthMessage()
        {

        }

        public ServerStealthMessage(GameObject character, bool setValue)
        {
            this.character = character;
            this.setValue = setValue;
        }

        public void OnReceived()
        {
            if (this.character == null) return;
            PantheraObj ptraObj = this.character.GetComponent<PantheraObj>();
            if (ptraObj == null) return;
            ptraObj.stealthed = this.setValue;
            new ClientStealthMessage(this.character, this.setValue).Send(NetworkDestination.Clients);
            ptraObj.characterBody.RecalculateStats();
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(character);
            writer.Write(setValue);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.character = reader.ReadGameObject();
            this.setValue = reader.ReadBoolean();
        }

    }

    public class ClientStealthMessage : INetMessage
    {

        public GameObject character;
        public bool setValue;

        public ClientStealthMessage()
        {

        }

        public ClientStealthMessage(GameObject character, bool setValue)
        {
            this.character = character;
            this.setValue = setValue;
        }

        public void OnReceived()
        {
            if (this.character == null || Util.HasEffectiveAuthority(this.character) == true) return;
            PantheraObj ptraObj = this.character.GetComponent<PantheraObj>();
            if (ptraObj == null) return;
            ptraObj.stealthed = this.setValue;
            ptraObj.characterBody.RecalculateStats();
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(character);
            writer.Write(setValue);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.character = reader.ReadGameObject();
            this.setValue = reader.ReadBoolean();
        }

    }
}
