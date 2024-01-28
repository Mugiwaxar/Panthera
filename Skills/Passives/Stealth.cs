using Panthera;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.NetworkMessages;
using Panthera.Passives;
using Panthera.OldSkills;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Panthera.Skills.Passives;
using Panthera.MachineScripts;

namespace Panthera.Skills.Passives
{
    class Stealth
    {

        public static void DoStealth(PantheraObj ptraObj)
        {
            if (ptraObj.stealthed == true) return;
            if (ptraObj.unstealthCoroutine != null)
            {
                ptraObj.StopCoroutine(ptraObj.unstealthCoroutine);
                ptraObj.unstealthCoroutine = null;
            }
            new NetworkMessages.ServerStealthMessage(ptraObj.gameObject, true).Send(NetworkDestination.Server);
            ptraObj.stealthed = true;
            ptraObj.skillLocator.startCooldown(PantheraConfig.Prowl_SkillID, 1);
            Utils.Sound.playSound(Utils.Sound.Prowl, ptraObj.characterBody.gameObject);
            ptraObj.characterBody.outOfCombat = true;
            ptraObj.characterBody.outOfDanger = true;
            ptraObj.characterBody.RecalculateStats();
        }

        public static void UnStealth(PantheraObj ptraObj)
        {
            if (ptraObj.stealthed == false || ptraObj.unstealthCoroutine != null) return;
            float unStealthDelay = PantheraConfig.Prowl_unStealDelay;
            ptraObj.unstealthCoroutine = ptraObj.StartCoroutine(DelayUnstealth(ptraObj, unStealthDelay));
        }

        public static void TookDamageUnstealth(PantheraObj ptraObj)
        {
            if (ptraObj.stealthed == false) return;
            float lastStealth = Time.time - ptraObj.prowlActivationTime;
            //if (lastStealth < PantheraConfig.PrimalStalker_fixedTime1) return;
            UnStealth(ptraObj);
        }

        public static void DidDamageUnstealth(PantheraObj ptraObj)
        {
            if (ptraObj.stealthed == false) return;
            //if (PantheraConfig.PrimalStalker_fixedTime1 > 0 && ptraObj.unstealthCoroutine == null)
            //    ptraObj.unstealthCoroutine = ptraObj.StartCoroutine(DelayUnstealth(ptraObj, PantheraConfig.PrimalStalker_fixedTime1));
            //else if (PantheraConfig.PrimalStalker_fixedTime1 > 0 && ptraObj.unstealthCoroutine != null)
            //    return;
            else
                UnStealth(ptraObj);
        }

        public static IEnumerator DelayUnstealth(PantheraObj ptraObj, float delay)
        {

            yield return new WaitForSeconds(delay);

            new NetworkMessages.ServerStealthMessage(ptraObj.gameObject, false).Send(NetworkDestination.Server);
            ptraObj.stealthed = false;
            ptraObj.skillLocator.startCooldown(PantheraConfig.Prowl_SkillID);
            ptraObj.characterBody.RecalculateStats();
            ptraObj.StopCoroutine(ptraObj.unstealthCoroutine);
            ptraObj.unstealthCoroutine = null;
        }

    }
}
