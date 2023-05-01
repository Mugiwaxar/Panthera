using Panthera.Ability.Ruse;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.NetworkMessages;
using Panthera.Skills;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Passives
{
    class Stealth
    {

        public static void DoStealth(PantheraObj ptra)
        {
            if (ptra.stealthed == true) return;
            ptra.stealthed = true;
            new ServerSetBuffCount(ptra.gameObject, (int)Buff.StealthBuff.buffIndex, 1).Send(NetworkDestination.Server);
            Utils.Sound.playSound(Utils.Sound.Prowl, ptra.characterBody.gameObject);
            ptra.pantheraFX.SetStealthFX(true);
            ptra.characterBody.outOfCombat = true;
            ptra.characterBody.outOfDanger = true;
            ptra.characterBody.moveSpeed -= GetStealthMoveSpeedReduction(ptra);
            ptra.activePreset.getSkillByID(PantheraConfig.Prowl_SkillID).icon = Assets.ProwlActive;
        }

        public static void TookDamageUnstealth(PantheraObj ptra)
        {
            if (ptra.stealthed == false) return;
            float lastStealth = Time.time - ptra.prowlActivationTime;
            if (lastStealth < ptra.activePreset.prowl_unstealthDelay) return;
            UnStealth(ptra);
        }

        public static void DidDamageUnstealth(PantheraObj ptra)
        {
            if (ptra.stealthed == false) return;
            if (ptra.activePreset.prowl_unstealthDelay > 0 && ptra.unstealthCoroutine == null)
                ptra.unstealthCoroutine = ptra.StartCoroutine(Stealth.DelayUnstealth(ptra, ptra.activePreset.prowl_unstealthDelay));
            else if (ptra.activePreset.prowl_unstealthDelay > 0 && ptra.unstealthCoroutine != null)
                return;
            else
                UnStealth(ptra);
        }

        public static void UnStealth(PantheraObj ptra)
        {
            if (ptra.stealthed == false) return;
            ptra.stealthed = false;
            new ServerSetBuffCount(ptra.gameObject, (int)Buff.StealthBuff.buffIndex, 0).Send(NetworkDestination.Server);
            ptra.pantheraFX.SetStealthFX(false);
            ptra.characterBody.moveSpeed += GetStealthMoveSpeedReduction(ptra);
            ptra.skillLocator.startCooldown(PantheraConfig.Prowl_SkillID);
            ptra.activePreset.getSkillByID(PantheraConfig.Prowl_SkillID).icon = Assets.Prowl;
        }

        public static float GetStealthMoveSpeedReduction(PantheraObj ptra)
        {
            float speedReduction = PantheraConfig.Prowl_moveSpeedReduction;
            if (ptra.activePreset != null)
            {
                Preset preset = ptra.activePreset;
                int level = preset.getAbilityLevel(PantheraConfig.SilentPredatorAbilityID);
                if (level == 1)
                    speedReduction -= speedReduction * PantheraConfig.SilentPredator_reduction1;
                if (level == 2)
                    speedReduction -= speedReduction * PantheraConfig.SilentPredator_reduction2;
                if (level == 3)
                    speedReduction -= speedReduction * PantheraConfig.SilentPredator_reduction3;
            }
            return speedReduction;
        }

        public static IEnumerator DelayUnstealth(PantheraObj ptra, float delay)
        {

            float startingTime = Time.time;

            while (Time.time - startingTime < delay)
            {
                if (ptra.stealthed == false)
                {
                    ptra.unstealthCoroutine = null;
                    yield break;
                }
                yield return new WaitForSeconds(0.1f);
            }
            
            UnStealth(ptra);
            ptra.unstealthCoroutine = null;
        }

    }
}
