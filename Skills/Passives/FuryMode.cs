using Panthera.Base;
using Panthera.BodyComponents;
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
    public class FuryMode
    {

        public static void FuryOn(PantheraObj ptraObj)
        {
            ptraObj.furyMode = true;
            new NetworkMessages.ServerFuryMessage(ptraObj.gameObject, true).Send(NetworkDestination.Server);
            if (ptraObj.guardianMode == true)
                Skills.Passives.GuardianMode.GuardianOff(ptraObj);
            Utils.Sound.playSound(Utils.Sound.FuryOn, ptraObj.gameObject);
            FXManager.SpawnEffect(ptraObj.gameObject, Assets.FuryOnFX, ptraObj.modelTransform.position, ptraObj.modelScale, null, ptraObj.modelTransform.rotation, false);
            ptraObj.skillLocator.startCooldown(PantheraConfig.Fury_SkillID, 1);
            if (ptraObj.getAbilityLevel(PantheraConfig.ClawsStorm_AbilityID) > 0)
            {
                ptraObj.activatedComboList[PantheraConfig.Slash_CombosID] = false;
                ptraObj.activatedComboList[PantheraConfig.ClawsStorm_CombosID] = true;
            }
            ptraObj.GetComponent<PantheraFX>().setFuryAuraFX(true);
            ptraObj.characterBody.RecalculateStats();
        }

        public static void FuryOff(PantheraObj ptraObj)
        {
            ptraObj.furyMode = false;
            new NetworkMessages.ServerFuryMessage(ptraObj.gameObject, false).Send(NetworkDestination.Server);
            ptraObj.skillLocator.startCooldown(PantheraConfig.Fury_SkillID);
            ptraObj.activatedComboList[PantheraConfig.Slash_CombosID] = true;
            ptraObj.activatedComboList[PantheraConfig.ClawsStorm_CombosID] = false;
            ptraObj.GetComponent<PantheraFX>().setFuryAuraFX(false);
            ptraObj.characterBody.RecalculateStats();
        }

    }
}
