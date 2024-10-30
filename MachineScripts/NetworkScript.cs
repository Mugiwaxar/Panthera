using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
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

        public float regenerationTimer = 0;
        public int regenFX = 0;

        public override void Start()
        {
            if (NetworkServer.active == true) GlobalEventManager.onCharacterDeathGlobal += OnCharacterDieEventServer;
        }

        public override void Update()
        {

        }

        public override void FixedUpdate()
        {

            // Return if not the Server //
            if (NetworkServer.active == false)
                return;

            // Apply Tenacity Buffs //
            int tenacityBuffCount = base.characterBody.GetBuffCount(Base.Buff.TenacityBuff);
            if (tenacityBuffCount > 0)
            {
                float barrierToAdd = base.characterBody.maxBarrier * PantheraConfig.Tenacity_blockAdded * tenacityBuffCount / 60;
                base.healthComponent.AddBarrier(barrierToAdd);
            }

            // Apply Regeneration Buffs //
            if (Time.time - this.regenerationTimer > PantheraConfig.Regeneration_time)
            {
                this.regenerationTimer = Time.time;
                int regenerationBuffCount = base.characterBody.GetBuffCount(Base.Buff.RegenerationBuff);
                if (regenerationBuffCount > 0)
                {
                    float healthToAdd = base.characterBody.maxHealth * PantheraConfig.Regeneration_percentHeal * regenerationBuffCount;
                    base.healthComponent.Heal(healthToAdd, default(ProcChainMask));
                    if (this.regenFX == 0)
                    {
                        this.regenFX = Utils.FXManager.SpawnEffect(base.gameObject, Base.PantheraAssets.RegenerationFX, base.modelTransform.position, 1, base.gameObject, base.modelTransform.rotation, true);
                        Utils.Sound.playSound(Utils.Sound.Regeneration, base.gameObject);
                    }
                }
                else if (this.regenFX != 0)
                {
                    Utils.FXManager.DestroyEffect(this.regenFX, 1);
                    this.regenFX = 0;
                }
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
