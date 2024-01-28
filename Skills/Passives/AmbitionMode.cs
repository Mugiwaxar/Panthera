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
            ptraObj.ambitionMode = true;
            ptraObj.ambitionTimer = Time.time;
            new NetworkMessages.ServerAmbitionMessage(ptraObj.gameObject, true).Send(NetworkDestination.Server);
            Utils.Sound.playSound(Utils.Sound.AmbitionOn, ptraObj.gameObject);
            ptraObj.skillLocator.startCooldown(PantheraConfig.Ambition_SkillID, 1);
            FXManager.SpawnEffect(ptraObj.gameObject, Assets.AmbitionOnFX, ptraObj.modelTransform.position, ptraObj.modelScale, null, ptraObj.modelTransform.rotation, false);
            ptraObj.GetComponent<PantheraFX>().setAmbitionAuraFX(true);
        }

        public static void AmbitionOff(PantheraObj ptraObj)
        {
            ptraObj.ambitionMode = false;
            ptraObj.skillLocator.startCooldown(PantheraConfig.Ambition_SkillID);
            new NetworkMessages.ServerAmbitionMessage(ptraObj.gameObject, false).Send(NetworkDestination.Server);
            ptraObj.skillLocator.startCooldown(PantheraConfig.Ambition_SkillID);
            ptraObj.GetComponent<PantheraFX>().setAmbitionAuraFX(false);
        }

        public static void OnEnemyDie(PantheraObj ptraObj)
        {
            if (ptraObj.ambitionMode == true && ptraObj.characterBody.GetBuffCount(Buff.CupidityBuff) < PantheraConfig.Cupidity_maxStacks)
            {
                new ServerAddBuff(ptraObj.gameObject, ptraObj.gameObject, Buff.CupidityBuff).Send(NetworkDestination.Server);
                Utils.Sound.playSound(Utils.Sound.AmbitionBuff, ptraObj.gameObject, false);
            }
        }

        public static void CalculateAddedMoney(Action<RoR2.CharacterMaster, uint> orig, RoR2.CharacterMaster self, uint amount)
        {
            CharacterBody body = self.GetBody();
            if (body != null && body is PantheraBody)
            {
                int buffCount = body.GetBuffCount(Buff.CupidityBuff);
                if (buffCount > 0)
                {
                    float multiplier = buffCount * PantheraConfig.Cupidity_goldMultiplier;
                    float newAmount = Math.Max(amount * (1 + multiplier), 1);
                    amount = (uint)newAmount;
                }
            }
            orig(self, amount);
        }

    }
}
