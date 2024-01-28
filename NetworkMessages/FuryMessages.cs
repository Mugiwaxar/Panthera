using Panthera.BodyComponents;
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

    public class ServerFuryMessage : INetMessage
    {

        public GameObject character;
        public bool setValue;

        public ServerFuryMessage()
        {

        }

        public ServerFuryMessage(GameObject character, bool setValue)
        {
            this.character = character;
            this.setValue = setValue;
        }

        public void OnReceived()
        {
            if (this.character == null) return;
            PantheraObj ptraObj = this.character.GetComponent<PantheraObj>();
            if (ptraObj == null) return;
            ptraObj.furyMode = this.setValue;
            new ClientFuryMessage(this.character, this.setValue).Send(NetworkDestination.Clients);
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

    public class ClientFuryMessage : INetMessage
    {

        public GameObject character;
        public bool setValue;

        public ClientFuryMessage()
        {

        }

        public ClientFuryMessage(GameObject character, bool setValue)
        {
            this.character = character;
            this.setValue = setValue;
        }

        public void OnReceived()
        {
            if (this.character == null || Util.HasEffectiveAuthority(this.character) == true) return;
            PantheraObj ptraObj = this.character.GetComponent<PantheraObj>();
            if (ptraObj == null) return;
            ptraObj.furyMode = this.setValue;
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
