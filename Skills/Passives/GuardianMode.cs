using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Utils;
using R2API.Networking.Interfaces;
using R2API.Networking;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Skills.Passives
{
    public class GuardianMode
    {

        public static void GuardianOn(PantheraObj ptraObj)
        {
            ptraObj.guardianMode = true;
            new NetworkMessages.ServerGuardianMessage(ptraObj.gameObject, true).Send(NetworkDestination.Server);
            if (ptraObj.furyMode == true)
                Skills.Passives.FuryMode.FuryOff(ptraObj);
            Utils.Sound.playSound(Utils.Sound.GardianOn, ptraObj.gameObject);
            ptraObj.skillLocator.startCooldown(PantheraConfig.Guardian_SkillID, 1);
            FXManager.SpawnEffect(ptraObj.gameObject, Assets.GuardianOnFX, ptraObj.modelTransform.position, 1, ptraObj.gameObject, ptraObj.modelTransform.rotation, true);
            if (ptraObj.getAbilityLevel(PantheraConfig.FrontShield_AbilityID) > 0)
            {
                ptraObj.activatedComboList[PantheraConfig.Slash_CombosID] = false;
                ptraObj.activatedComboList[PantheraConfig.FrontShield_CombosID] = true;
            }
            ptraObj.GetComponent<PantheraFX>().setGuardianAuraFX(true);
            ptraObj.characterBody.RecalculateStats();
        }

        public static void GuardianOff(PantheraObj ptraObj)
        {
            ptraObj.guardianMode = false;
            new NetworkMessages.ServerGuardianMessage(ptraObj.gameObject, false).Send(NetworkDestination.Server);
            Utils.Sound.playSound(Utils.Sound.GardianOff, ptraObj.gameObject);
            FrontShield.DisableFrontShield(ptraObj);
            ptraObj.skillLocator.startCooldown(PantheraConfig.Guardian_SkillID);
            ptraObj.activatedComboList[PantheraConfig.Slash_CombosID] = true;
            ptraObj.activatedComboList[PantheraConfig.FrontShield_CombosID] = false;
            ptraObj.GetComponent<PantheraFX>().setGuardianAuraFX(false);
            ptraObj.characterBody.RecalculateStats();
        }

    }
}
