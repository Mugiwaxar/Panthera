using Panthera.BodyComponents;
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

        public float tenacityTimer = 0;

        public override void Start()
        {
            if (NetworkServer.active == true) GlobalEventManager.onCharacterDeathGlobal += OnCharacterDieEventServer;
        }

        public override void Update()
        {

        }

        public override void FixedUpdate()
        {
            // Apply Tenacity Buffs //
            this.tenacityTimer = Time.time;
            int tenacityBuffCount = base.characterBody.GetBuffCount(Base.Buff.TenacityBuff);
            if (tenacityBuffCount > 0)
            {
                float barrierToAdd = base.characterBody.maxBarrier * PantheraConfig.Tenacity_blockAdded * tenacityBuffCount / 60;
                base.healthComponent.AddBarrier(barrierToAdd);
            }
        }

        public override void Stop()
        {
            GlobalEventManager.onCharacterDeathGlobal -= OnCharacterDieEventServer;
        }

        public void OnCharacterDieEventServer(DamageReport damageReport)
        {
            if (damageReport.attacker == null || damageReport.victim == null) return;
                new ClientCharacterDieEvent(damageReport.attacker, damageReport.victim.gameObject).Send(NetworkDestination.Clients);
        }

    }
}
