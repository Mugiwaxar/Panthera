﻿using Panthera.Base;
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
    public class GuardianMode
    {

        public static void GuardianOn(PantheraObj ptraObj)
        {

            // Enable the Gardian Mode //
            ptraObj.guardianMode = true;

            // Send to the Server //
            new NetworkMessages.ServerGuardianMessage(ptraObj.gameObject, true).Send(NetworkDestination.Server);

            // Apply the Mastery //
            if (ptraObj.profileComponent.isMastery(PantheraConfig.Guardian_AbilityID) == true)
            {
                float healPercent = PantheraConfig.Guardian_masteryHealPercent + (ptraObj.characterBody.mastery / 100);
                float healAmount = ptraObj.characterBody.maxHealth * healPercent;
                new NetworkMessages.ServerHeal(ptraObj.gameObject, healAmount).Send(NetworkDestination.Server);
            }

            // Apply the Savage Regeneration Ability //
            if (ptraObj.profileComponent.getAbilityLevel(PantheraConfig.SavageRevitalization_AbilityID) > 0)
            {
                int regenStackCount = ptraObj.characterBody.GetBuffCount(Base.Buff.RegenerationBuff);
                int buffToRemove = Math.Min(PantheraConfig.SavageRevitalization_addedStack, regenStackCount);
                new ServerRemoveBuff(ptraObj.gameObject, Base.Buff.RegenerationBuff.index, buffToRemove, true).Send(NetworkDestination.Server);
                new ServerAddBuff(ptraObj.gameObject, ptraObj.gameObject, Base.Buff.RegenerationBuff, PantheraConfig.SavageRevitalization_addedStack, PantheraConfig.SavageRevitalization_buffTime).Send(NetworkDestination.Server);
            }

            // Apply the Warden Vitality Ability //
            int wardensVitalityLevel = ptraObj.profileComponent.getAbilityLevel(PantheraConfig.WardensVitality_AbilityID);
            if (wardensVitalityLevel > 0)
            {
                int addedPoints = 0;
                if (wardensVitalityLevel == 1) addedPoints = PantheraConfig.WardensVitality_BlockAdded1;
                else if (wardensVitalityLevel == 2) addedPoints = PantheraConfig.WardensVitality_BlockAdded2;
                else if (wardensVitalityLevel == 3) addedPoints = PantheraConfig.WardensVitality_BlockAdded3;
                ptraObj.characterBody.block = Math.Max(ptraObj.characterBody.block, addedPoints);
            }

            // Stop the Fury Mode //
            if (ptraObj.furyMode == true)
                Skills.Passives.FuryMode.FuryOff(ptraObj);

            // Play the Sound //
            Utils.Sound.playSound(Utils.Sound.GardianOn, ptraObj.gameObject);

            // Start the Cooldown //
            ptraObj.skillLocator.startCooldown(PantheraConfig.Guardian_SkillID, 1);

            // Create the FX //
            FXManager.SpawnEffect(ptraObj.gameObject, PantheraAssets.GuardianOnFX, ptraObj.modelTransform.position, 1, ptraObj.gameObject, ptraObj.modelTransform.rotation, true);

            // Change the Skills //
            if (ptraObj.profileComponent.getAbilityLevel(PantheraConfig.FrontShield_AbilityID) > 0)
            {
                ptraObj.profileComponent.disableSkill(PantheraConfig.Slash_SkillID, true);
                ptraObj.profileComponent.disableSkill(PantheraConfig.FrontShield_SkillID, false);
            }

            // Start the Aura FX //
            ptraObj.GetComponent<PantheraFX>().setGuardianAuraFX(true);

            // Recalculate Stats //
            ptraObj.characterBody.RecalculateStats();

        }

        public static void GuardianOff(PantheraObj ptraObj)
        {

            // Disable the Gardian Mode //
            ptraObj.guardianMode = false;

            // Send to the Server //
            new NetworkMessages.ServerGuardianMessage(ptraObj.gameObject, false).Send(NetworkDestination.Server);

            // Play the Sound //
            Utils.Sound.playSound(Utils.Sound.GardianOff, ptraObj.gameObject);

            // Disable the Front Shield //
            FrontShield.DisableFrontShield(ptraObj);

            // Start the Cooldown //
            ptraObj.skillLocator.startCooldown(PantheraConfig.Guardian_SkillID);

            // Set back the Skills //
            ptraObj.profileComponent.disableSkill(PantheraConfig.Slash_SkillID, false);
            ptraObj.profileComponent.disableSkill(PantheraConfig.FrontShield_SkillID, true);

            // Stop the Aura FX //
            ptraObj.GetComponent<PantheraFX>().setGuardianAuraFX(false);

            // Recalculate Stats //
            ptraObj.characterBody.RecalculateStats();

        }

    }
}
