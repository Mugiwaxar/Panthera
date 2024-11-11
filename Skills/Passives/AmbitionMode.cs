using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.NetworkMessages;
using Panthera.Utils;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Skills.Passives
{
    public class AmbitionMode
    {

        public static void AmbitionOn(PantheraObj ptraObj)
        {

            // Enable Ambition Mode //
            ptraObj.ambitionMode = true;

            // Start the Timer //
            ptraObj.ambitionTimer = Time.time;

            // Send the Message //
            new NetworkMessages.ServerAmbitionMessage(ptraObj.gameObject, true).Send(NetworkDestination.Server);

            // Play the Sound //
            Utils.Sound.playSound(Utils.Sound.AmbitionOn, ptraObj.gameObject);

            // Start the Cooldown //
            ptraObj.skillLocator.startCooldown(PantheraConfig.Ambition_SkillID);

            // Spawn the Effect //
            FXManager.SpawnEffect(ptraObj.gameObject, PantheraAssets.AmbitionOnFX, ptraObj.modelTransform.position, ptraObj.actualModelScale, null, ptraObj.modelTransform.rotation, false);

            // Start the Aura FX //
            ptraObj.GetComponent<PantheraFX>().setAmbitionAuraFX(true);

        }

        public static void AmbitionOff(PantheraObj ptraObj)
        {

            // Disable Ambition Mode //
            ptraObj.ambitionMode = false;

            // Send the Message //
            new NetworkMessages.ServerAmbitionMessage(ptraObj.gameObject, false).Send(NetworkDestination.Server);

            // Stop the Aura FX //
            ptraObj.GetComponent<PantheraFX>().setAmbitionAuraFX(false);

        }

        public static void OnEnemyDie(PantheraObj ptraObj)
        {

            // Add a Cupidity Buff when an Enemy die //
            if (ptraObj.ambitionMode == true && ptraObj.characterBody.GetBuffCount(Buff.CupidityBuff) < PantheraConfig.Cupidity_maxStacks)
            {
                new ServerAddBuff(ptraObj.gameObject, ptraObj.gameObject, Buff.CupidityBuff).Send(NetworkDestination.Server);
                Utils.Sound.playSound(Utils.Sound.AmbitionBuff, ptraObj.gameObject, false);
            }

        }

        public static void CalculateAddedMoney(Action<RoR2.CharacterMaster, uint> orig, RoR2.CharacterMaster self, uint amount)
        {

            // Get the Body //
            CharacterBody body = self.GetBody();

            // Check if Panthera //
            if (body != null && body is PantheraBody)
            {
                // Get the Buff Count //
                int buffCount = body.GetBuffCount(Buff.CupidityBuff);

                // Apply the multiplier //
                if (buffCount > 0)
                {
                    float multiplier = buffCount * PantheraConfig.Cupidity_goldMultiplier;
                    float newAmount = Math.Max(amount * (1 + multiplier), 1);
                    amount = (uint)newAmount;
                }

            }

            // Call the orig Fonction //
            orig(self, amount);


        }

    }
}
