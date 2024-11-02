using Panthera.Base;
using Panthera.BodyComponents;
using R2API.Networking;
using R2API.Networking.Interfaces;
using System;
using System.Collections;
using UnityEngine;

namespace Panthera.Skills.Passives
{
    class Stealth
    {

        public static void DoStealth(PantheraObj ptraObj, bool startCooldown = true)
        {

            // Check if Stealthed //
            if (ptraObj.stealthed == true) return;

            // Check if delay unsteath //
            if (ptraObj.UnstealthCoroutine != null)
            {
                ptraObj.StopCoroutine(ptraObj.UnstealthCoroutine);
                ptraObj.UnstealthCoroutine = null;
            }

            // Send the Stealth Message //
            new NetworkMessages.ServerStealthMessage(ptraObj.gameObject, true).Send(NetworkDestination.Server);

            // Remove the Eclipse Buff //

            new NetworkMessages.ServerClearTimedBuffs(ptraObj.gameObject, Buff.EclipseBuff.index).Send(NetworkDestination.Server);
            // Set as Stealthed //
            ptraObj.stealthed = true;

            // Start the Stealth Cooldown //
            ptraObj.skillLocator.StartCooldown(PantheraConfig.Prowl_SkillID, 1);

            // Play the Sound //
            Utils.Sound.playSound(Utils.Sound.Prowl, ptraObj.characterBody.gameObject);

            // Set out of combat/danger //
            ptraObj.characterBody.outOfCombat = true;
            ptraObj.characterBody.outOfDanger = true;

            // Recalculate Body Stats //
            ptraObj.characterBody.RecalculateStats();

        }

        public static void UnStealth(PantheraObj ptraObj, float delay = 0)
        {

            // Check if Stealthed //
            if (ptraObj.stealthed == false || ptraObj.UnstealthCoroutine != null) return;

            // Start the Cooldown //
            ptraObj.skillLocator.StartCooldown(PantheraConfig.Prowl_SkillID);

            // Calcule the delay //
            float unStealthDelay = Math.Max(PantheraConfig.Prowl_unStealDelay, delay);

            // UnStealth //
            ptraObj.UnstealthCoroutine = ptraObj.StartCoroutine(DelayUnstealth(ptraObj, unStealthDelay));

        }

        public static void TookDamageUnstealth(PantheraObj ptraObj)
        {
            // Check if Stealthed //
            if (ptraObj.stealthed == false) return;

            // Check the Shadow Stalker Ability //
            int shadowStalkerLevel = ptraObj.GetAbilityLevel(PantheraConfig.ShadowStalker_AbilityID);

            // UnStealth //
            if (shadowStalkerLevel == 0)
                UnStealth(ptraObj);
            else if (shadowStalkerLevel == 1)
                UnStealth(ptraObj, PantheraConfig.ShadowStalker_duration1);
            else if (shadowStalkerLevel == 2)
                UnStealth(ptraObj, PantheraConfig.ShadowStalker_duration2);

        }

        public static void DidDamageUnstealth(PantheraObj ptraObj)
        {

            // Check if Stealthed //
            if (ptraObj.stealthed == false) return;

            // Check the Shadow Stalker Ability //
            int shadowStalkerLevel = ptraObj.GetAbilityLevel(PantheraConfig.ShadowStalker_AbilityID);

            // UnStealth //
            if (shadowStalkerLevel == 0)
                UnStealth(ptraObj);
            else if (shadowStalkerLevel == 1)
                UnStealth(ptraObj, PantheraConfig.ShadowStalker_duration1);
            else if (shadowStalkerLevel == 2)
                UnStealth(ptraObj, PantheraConfig.ShadowStalker_duration2);

        }

        public static IEnumerator DelayUnstealth(PantheraObj ptraObj, float delay)
        {

            // Wait the UnStealth delay //
            yield return new WaitForSeconds(delay);

            // Send the Message //
            new NetworkMessages.ServerStealthMessage(ptraObj.gameObject, false).Send(NetworkDestination.Server);

            // Set as not Stealthed //
            ptraObj.stealthed = false;

            // Recalculate Body Stats //
            ptraObj.characterBody.RecalculateStats();

            // Remove the Coroutine //
            ptraObj.StopCoroutine(ptraObj.UnstealthCoroutine);
            ptraObj.UnstealthCoroutine = null;

        }

    }
}
