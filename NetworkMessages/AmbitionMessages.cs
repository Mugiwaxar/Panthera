using Panthera.BodyComponents;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace Panthera.NetworkMessages
{

    public class ServerAmbitionMessage : INetMessage
    {

        public GameObject character;
        public bool setValue;

        public ServerAmbitionMessage()
        {

        }

        public ServerAmbitionMessage(GameObject character, bool setValue)
        {
            this.character = character;
            this.setValue = setValue;
        }

        public void OnReceived()
        {
            if (this.character == null) return;
            PantheraObj ptraObj = this.character.GetComponent<PantheraObj>();
            if (ptraObj == null) return;
            ptraObj.ambitionMode = this.setValue;
            new ClientAmbitionMessage(this.character, this.setValue).Send(NetworkDestination.Clients);
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

    public class ClientAmbitionMessage : INetMessage
    {

        public GameObject character;
        public bool setValue;

        public ClientAmbitionMessage()
        {

        }

        public ClientAmbitionMessage(GameObject character, bool setValue)
        {
            this.character = character;
            this.setValue = setValue;
        }

        public void OnReceived()
        {
            if (this.character == null || Util.HasEffectiveAuthority(this.character) == true) return;
            PantheraObj ptraObj = this.character.GetComponent<PantheraObj>();
            if (ptraObj == null) return;
            ptraObj.ambitionMode = this.setValue;
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
