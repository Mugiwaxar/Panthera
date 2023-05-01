using Panthera.NetworkMessages;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Panthera.Components
{
    internal class ZoneHealComponent : MonoBehaviour
    {

        public float startingTime;
        public float lastHealTime;
        public float duration;
        public float healRate;
        public float healPercentAmount;

        public void Start()
        {

            // Set the Start Time //
            this.startingTime = Time.time;

            // Tell the server to add the Component //
            if (Utils.Functions.IsMultiplayer()) new ServerZoneHealTargetComp(base.gameObject, this.duration, this.healRate, this.healPercentAmount).Send(NetworkDestination.Server);

        }

        public void FixedUpdate()
        {

            // Check if Stop //
            float totalDuration = Time.time - this.startingTime;
            if (totalDuration > this.duration)
            {
                GameObject.Destroy(this.gameObject);
                return;
            }

            // Check if must Heal //
            float lastHeal = Time.time - this.lastHealTime;
            if (NetworkServer.active == true && lastHeal > this.healRate)
            {
                this.lastHealTime = Time.time;
                this.doHeal();
            }

        }

        public void doHeal()
        {
            List<GameObject> alliesHealed = new List<GameObject>();
            Collider[] colliders = Physics.OverlapSphere(base.transform.position, PantheraConfig.ZoneHeal_radius, LayerIndex.entityPrecise.mask.value);
            foreach (Collider collider in colliders)
            {
                HurtBox hb = collider.GetComponent<HurtBox>();
                if (hb == null) continue;
                HealthComponent hc = hb.healthComponent;
                if (hc == null || hc.body == null) continue;
                if (alliesHealed.Contains(hc.gameObject)) continue;
                alliesHealed.Add(hc.gameObject);
                TeamComponent tc = hc?.body?.teamComponent;
                if (tc == null || tc.teamIndex != TeamIndex.Player) continue;
                float maxHeal = hc.body.maxHealth;
                float heal = maxHeal * healPercentAmount;
                hc.Heal(heal, default(ProcChainMask));
                Utils.Sound.playSound(Utils.Sound.ZoneHeal, hc.gameObject);
                Utils.FXManager.SpawnEffect(hc.gameObject, Base.Assets.FlashHealFX, hc.body.footPosition, 1, hc.gameObject);
            }
        }

    }
}
