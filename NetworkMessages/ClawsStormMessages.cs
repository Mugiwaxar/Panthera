using Panthera.BodyComponents;
using R2API.Networking;
using R2API.Networking.Interfaces;
using UnityEngine.Networking;
using UnityEngine;

namespace Panthera.NetworkMessages
{

    public class ServerSetClawsStormMessage : INetMessage
    {
        public GameObject character;
        public bool setValue;
        public int invisibilityCount;

        public ServerSetClawsStormMessage()
        {

        }

        public ServerSetClawsStormMessage(GameObject character, bool setValue, int invisibilityCount)
        {
            this.character = character;
            this.setValue = setValue;
            this.invisibilityCount = invisibilityCount;
        }

        public void OnReceived()
        {
            if (this.character == null) return;
            PantheraObj ptraObj = this.character.GetComponent<PantheraObj>();
            if (ptraObj == null) return;
            ptraObj.clawsStormActivated = this.setValue;
            ptraObj.characterModel.invisibilityCount = this.invisibilityCount;
            new ClientSetClawsStormMessage(this.character, this.setValue, this.invisibilityCount).Send(NetworkDestination.Clients);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.character);
            writer.Write(this.setValue);
            writer.Write(this.invisibilityCount);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.character = reader.ReadGameObject();
            this.setValue = reader.ReadBoolean();
            this.invisibilityCount = reader.ReadInt32();
        }

    }

    public class ClientSetClawsStormMessage : INetMessage
    {
        public GameObject character;
        public bool setValue;
        public int invisibilityCount;

        public ClientSetClawsStormMessage()
        {

        }

        public ClientSetClawsStormMessage(GameObject character, bool setValue, int invisibilityCount)
        {
            this.character = character;
            this.setValue = setValue;
            this.invisibilityCount = invisibilityCount;
        }

        public void OnReceived()
        {
            if (this.character == null) return;
            PantheraObj ptraObj = this.character.GetComponent<PantheraObj>();
            if (ptraObj == null) return;
            ptraObj.clawsStormActivated = this.setValue;
            ptraObj.characterModel.invisibilityCount = this.invisibilityCount;
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(character);
            writer.Write(setValue);
            writer.Write(invisibilityCount);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.character = reader.ReadGameObject();
            this.setValue = reader.ReadBoolean();
            this.invisibilityCount = reader.ReadInt32();
        }
    }

}
