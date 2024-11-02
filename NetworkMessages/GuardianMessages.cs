using Panthera.BodyComponents;
using R2API.Networking.Interfaces;
using RoR2;
using UnityEngine.Networking;
using UnityEngine;
using R2API.Networking;

namespace Panthera.NetworkMessages
{
    public class ServerGuardianMessage : INetMessage
    {

        public GameObject character;
        public bool setValue;

        public ServerGuardianMessage()
        {

        }

        public ServerGuardianMessage(GameObject character, bool setValue)
        {
            this.character = character;
            this.setValue = setValue;
        }

        public void OnReceived()
        {
            if (this.character == null) return;
            PantheraObj ptraObj = this.character.GetComponent<PantheraObj>();
            if (ptraObj == null) return;
            ptraObj.guardianMode = this.setValue;
            Skills.Passives.FrontShield.DisableFrontShield(ptraObj);
            new ClientGuardianMessage(this.character, this.setValue).Send(NetworkDestination.Clients);
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

    public class ClientGuardianMessage : INetMessage
    {

        public GameObject character;
        public bool setValue;

        public ClientGuardianMessage()
        {

        }

        public ClientGuardianMessage(GameObject character, bool setValue)
        {
            this.character = character;
            this.setValue = setValue;
        }

        public void OnReceived()
        {
            if (this.character == null || Util.HasEffectiveAuthority(this.character) == true) return;
            PantheraObj ptraObj = this.character.GetComponent<PantheraObj>();
            if (ptraObj == null) return;
            ptraObj.guardianMode = this.setValue;
            Skills.Passives.FrontShield.DisableFrontShield(ptraObj);
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
