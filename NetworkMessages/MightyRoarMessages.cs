using Panthera.MachineScripts;
using Panthera.Skills;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Panthera.NetworkMessages
{

    class ServerDoMightyRoar : INetMessage
    {

        GameObject player;

        public ServerDoMightyRoar()
        {

        }

        public ServerDoMightyRoar(GameObject player)
        {
            this.player = player;
        }

        public void OnReceived()
        {

            if (this.player == null) return;

            Collider[] colliders = Physics.OverlapSphere(player.transform.position, PantheraConfig.MightyRoar_distance, LayerIndex.entityPrecise.mask.value);
            foreach (Collider collider in colliders)
            {
                HurtBox hb = collider.GetComponent<HurtBox>();
                if (hb == null) continue;
                HealthComponent hc = hb.healthComponent;
                if (hc == null) continue;
                if (hc.gameObject == player) continue;
                SetStateOnHurt state = hc.GetComponent<SetStateOnHurt>();
                if (state == null) continue;
                state.SetStun(PantheraConfig.MightyRoar_stunDuration);
            }
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(player);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.player = reader.ReadGameObject();
        }
    }

}
