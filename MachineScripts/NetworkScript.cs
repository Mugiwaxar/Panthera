using Panthera.NetworkMessages;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Panthera.MachineScripts
{
    class NetworkScript : MachineScript
    {

        public override void Start()
        {
            if (NetworkServer.active == true && NetworkClient.active == false) GlobalEventManager.onCharacterDeathGlobal += OnCharacterDieEvent;
        }

        public override void Update()
        {

        }

        public override void FixedUpdate()
        {

        }

        public override void Stop()
        {
            if (NetworkServer.active == true && NetworkClient.active == false) GlobalEventManager.onCharacterDeathGlobal -= OnCharacterDieEvent;
        }

        public void OnCharacterDieEvent(DamageReport damageReport)
        {
            if (damageReport.attacker == null || damageReport.victim == null) return;
            new ClientCharacterDieEvent(damageReport.attacker, damageReport.victim.gameObject).Send(NetworkDestination.Clients);
        }

    }
}
