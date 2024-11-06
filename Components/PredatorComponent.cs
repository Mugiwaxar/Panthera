using Panthera.Base;
using Panthera.BodyComponents;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Panthera.Components
{
    public class PredatorComponent : MonoBehaviour
    {

        public CharacterBody body;
        public HealthComponent hc;
        public bool damaged = false; // Local
        public PantheraObj lastHit; // Server
        public float bleedOutTime = 0; // Server
        public float IgnitionTime = 0; // Server
        public float lastStealthStrikeTime = 0; // Local

        public void Start()
        {
            if (NetworkServer.active == false) base.enabled = false;
            this.body = this.GetComponent<CharacterBody>();
            this.hc = this.GetComponent<HealthComponent>();
        }

        public void FixedUpdate()
        {

            // Check the Bleed Out DeBuff //
            if (Time.time - this.bleedOutTime > PantheraConfig.BleedOut_damageTime && this.lastHit != null)
            {
                // Save Time //
                this.bleedOutTime = Time.time;
                // Check if Debuff //
                int bleedOutCount = body.GetBuffCount(Buff.BleedOutDebuff.buffIndex);
                if (bleedOutCount > 0)
                {
                    this.hc.TakeDamage(Utils.Functions.CreateDotDamageInfo(Buff.BleedOutDebuff, this.lastHit.gameObject, base.gameObject, this.lastHit.characterBody.damage * Buff.BleedOutDebuff.damage * bleedOutCount, DamageColorIndex.Bleed));
                }
            }

            // Check the Ignition DeBuff //
            if (Time.time - this.IgnitionTime > PantheraConfig.Ignition_damageTime && this.lastHit != null)
            {
                // Save Time //
                this.IgnitionTime = Time.time;
                // Check if Debuff //
                int IgnitionCount = body.GetBuffCount(Buff.IgnitionDebuff.buffIndex);
                if (IgnitionCount > 0)
                {
                    this.hc.TakeDamage(Utils.Functions.CreateDotDamageInfo(Buff.IgnitionDebuff, this.lastHit.gameObject, base.gameObject, this.lastHit.characterBody.damage * Buff.IgnitionDebuff.damage * IgnitionCount, DamageColorIndex.WeakPoint));
                }
            }

        }


    }
}
