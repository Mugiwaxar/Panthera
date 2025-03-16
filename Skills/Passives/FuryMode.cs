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

            // Enable the Fury Mode //
            ptraObj.furyMode = true;

            // Send to the Server //
            new NetworkMessages.ServerFuryMessage(ptraObj.gameObject, true).Send(NetworkDestination.Server);

            // Disable the Gardian Mode //
            if (ptraObj.guardianMode == true)
                Skills.Passives.GuardianMode.GuardianOff(ptraObj);

            // Play the sound //
            Utils.Sound.playSound(Utils.Sound.FuryOn, ptraObj.gameObject);

            // Create the FX //
            FXManager.SpawnEffect(ptraObj.gameObject, PantheraAssets.FuryOnFX, ptraObj.modelTransform.position, ptraObj.actualModelScale, null, ptraObj.modelTransform.rotation, false);

            // Start the Cooldown //
            ptraObj.skillLocator.startCooldown(PantheraConfig.Fury_SkillID, 1);

            // Change the Skills //
            if (ptraObj.profileComponent.getAbilityLevel(PantheraConfig.ClawsStorm_AbilityID) > 0)
            {
                ptraObj.profileComponent.disableSkill(PantheraConfig.Slash_SkillID, true);
                ptraObj.profileComponent.disableSkill(PantheraConfig.ClawsStorm_SkillID, false);
            }

            // Start the Aura FX //
            ptraObj.GetComponent<PantheraFX>().setFuryAuraFX(true);

            // Recalculate Stats //
            ptraObj.characterBody.RecalculateStats();

        }

        public static void FuryOff(PantheraObj ptraObj)
        {

            // Disable the Fury Mode //
            ptraObj.furyMode = false;

            // Send to the Server //
            new NetworkMessages.ServerFuryMessage(ptraObj.gameObject, false).Send(NetworkDestination.Server);

            // Start the Cooldown //
            ptraObj.skillLocator.startCooldown(PantheraConfig.Fury_SkillID);

            // Set back the Skills //
            ptraObj.profileComponent.disableSkill(PantheraConfig.Slash_SkillID, false);
            ptraObj.profileComponent.disableSkill(PantheraConfig.ClawsStorm_SkillID, true);

            // Stop the Aura FX //
            ptraObj.GetComponent<PantheraFX>().setFuryAuraFX(false);

            // Recalculate Stats //
            ptraObj.characterBody.RecalculateStats();

        }

    }
}
