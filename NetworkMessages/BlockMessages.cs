using Panthera.BodyComponents;
using R2API.Networking.Interfaces;
using R2API.Networking;
using UnityEngine.Networking;
using UnityEngine;
using RoR2;

namespace Panthera.NetworkMessages
{

    public class ServerSetBlockAmount : INetMessage
    {

        GameObject character;
        float block;

        public ServerSetBlockAmount()
        {

        }

        public ServerSetBlockAmount(GameObject character, float block)
        {
            this.character = character;
            this.block = block;
        }

        public void OnReceived()
        {
            if (this.character == null) return;
            PantheraBody body = character.GetComponent<PantheraBody>();
            if (body == null) return;
            body.block = block;
            new ClientSetBlockAmount(this.character, this.block).Send(NetworkDestination.Clients);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.character);
            writer.Write(this.block);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.character = reader.ReadGameObject();
            this.block = reader.ReadSingle();
        }

    }

    public class ClientSetBlockAmount : INetMessage
    {

        GameObject character;
        float block;

        public ClientSetBlockAmount()
        {

        }

        public ClientSetBlockAmount(GameObject character, float block)
        {
            this.character = character;
            this.block = block;
        }

        public void OnReceived()
        {
            if (this.character == null || Util.HasEffectiveAuthority(this.character) == true) return;
            PantheraBody body = character.GetComponent<PantheraBody>();
            if (body == null) return;
            body.block = block;
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.character);
            writer.Write(this.block);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.character = reader.ReadGameObject();
            this.block = reader.ReadSingle();
        }

    }

    public class ClientBlockUsed : INetMessage
    {

        GameObject character;

        public ClientBlockUsed()
        {

        }

        public ClientBlockUsed(GameObject character)
        {
            this.character = character;
        }

        public void OnReceived()
        {
            if (this.character == null || Util.HasEffectiveAuthority(this.character) == false) return;
            PantheraBody body = character.GetComponent<PantheraBody>();
            if (body == null) return;
            body.block--;
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.character);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.character = reader.ReadGameObject();
        }

    }

}
