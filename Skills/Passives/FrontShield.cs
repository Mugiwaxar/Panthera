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
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Panthera.Skills.Passives;
using Panthera.Skills.Actives;

namespace Panthera.Skills.Passives
{
    public class FrontShield
    {

        public static void EnableFrontShield(PantheraObj ptraObj)
        {

            // Enable the Front Shield //
            ptraObj.frontShieldObj.SetActive(true);

            // Recalculate Stats //
            ptraObj.characterBody.RecalculateStats();

            // Send to the Server //
            if (RoR2Application.isInMultiPlayer) new ServerSetFrontShieldActive(ptraObj.gameObject, true).Send(NetworkDestination.Server);

        }

        public static void DisableFrontShield(PantheraObj ptraObj)
        {

            // Disable the Front Shield //
            ptraObj.frontShieldObj.SetActive(false);

            // UnDeploy //
            DeployShield(ptraObj, false);

            // Recalculate Stats //
            ptraObj.characterBody.RecalculateStats();

            // Send to the Server //
            if (RoR2Application.isInMultiPlayer) new ServerSetFrontShieldActive(ptraObj.gameObject, false).Send(NetworkDestination.Server);

        }

        public static void DeployShield(PantheraObj ptraObj,bool active)
        {

            // Set the Front Shield as deployed //
            ptraObj.frontShieldDeployed = active;

            // Send to the Server //
            if (RoR2Application.isInMultiPlayer) new ServerSetFrontShieldDeployed(ptraObj.gameObject, active).Send(NetworkDestination.Server);

            // Recalculate Stats //
            ptraObj.characterBody.RecalculateStats();

        }

        public static void DamageShield(PantheraObj ptraObj, float damage)
        {

            // Decrease the Front Shield //
            ptraObj.characterBody.frontShield -= damage;

            // Start the Timer //
            ptraObj.GetPassiveScript().lastShieldDamageTime = Time.time;

            // Check if the Shield must be destroyed //
            if (ptraObj.characterBody.frontShield <= 0)
                BreakShield(ptraObj);

        }

        public static void BreakShield(PantheraObj ptraObj)
        {

            // Start the Timer //
            ptraObj.GetPassiveScript().destroyedShieldTime = Time.time;

            // Play the Sound //
            Utils.Sound.playSound(Utils.Sound.FrontShieldBreak, ptraObj.gameObject);

            // Disable the Front Shield //
            ptraObj.frontShieldObj.SetActive(false);

            // UnDeploy //
            DeployShield(ptraObj, false);

            // Recalculate Stats //
            ptraObj.characterBody.RecalculateStats();

            // Send to the Server //
            if (RoR2Application.isInMultiPlayer) new ServerSetFrontShieldActive(ptraObj.gameObject, false).Send(NetworkDestination.Server);

        }

    }
}
