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

        public PantheraObj ptraObj;
        public CharacterBody body;
        public HealthComponent hc;
        public float bleedOutTime = 0;

        public void Start()
        {
            if (NetworkServer.active == false) base.enabled = false;
            this.body = this.GetComponent<CharacterBody>();
            this.hc = this.GetComponent<HealthComponent>();
        }

        public void FixedUpdate()
        {
            // Check the Bleed Out Buff //
            if (Time.time - this.bleedOutTime > PantheraConfig.BleedOut_damageTime)
            {
                // Save Time //
                this.bleedOutTime = Time.time;
                // Check if Debuff //
                int bleedOutCount = body.GetBuffCount(Buff.BleedOutDebuff.buffIndex);
                if (bleedOutCount > 0)
                {
                    this.hc.TakeDamage(Utils.Functions.CreateDotDamageInfo(Buff.BleedOutDebuff, this.ptraObj.gameObject, base.gameObject, this.ptraObj.characterBody.damage * Buff.BleedOutDebuff.damage * bleedOutCount));
                }
            }

        }


    }
}
