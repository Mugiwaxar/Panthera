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
            ptraObj.frontShieldObj.SetActive(true);
            ptraObj.characterBody.RecalculateStats();
            if (RoR2Application.isInMultiPlayer) new ServerSetFrontShieldActive(ptraObj.gameObject, true).Send(NetworkDestination.Server);
        }

        public static void DisableFrontShield(PantheraObj ptraObj)
        {
            ptraObj.frontShieldObj.SetActive(false);
            DeployShield(ptraObj, false);
            ptraObj.characterBody.RecalculateStats();
            if (RoR2Application.isInMultiPlayer) new ServerSetFrontShieldActive(ptraObj.gameObject, false).Send(NetworkDestination.Server);
        }

        public static void DeployShield(PantheraObj ptraObj,bool active)
        {
            ptraObj.frontShieldDeployed = active;
            if (RoR2Application.isInMultiPlayer) new ServerSetFrontShieldDeployed(ptraObj.gameObject, active).Send(NetworkDestination.Server);
            ptraObj.characterBody.RecalculateStats();
        }

        public static void DamageShield(PantheraObj ptraObj, float damage)
        {
            ptraObj.characterBody.frontShield -= damage;
            ptraObj.getPassiveScript().lastShieldDamageTime = Time.time;
            if (ptraObj.characterBody.frontShield <= 0)
                BreakShield(ptraObj);
        }

        public static void BreakShield(PantheraObj ptraObj)
        {
            ptraObj.getPassiveScript().destroyedShieldTime = Time.time;
            Utils.Sound.playSound(Utils.Sound.FrontShieldBreak, ptraObj.gameObject);
            ptraObj.frontShieldObj.SetActive(false);
            DeployShield(ptraObj, false);
            ptraObj.characterBody.RecalculateStats();
            if (RoR2Application.isInMultiPlayer) new ServerSetFrontShieldActive(ptraObj.gameObject, false).Send(NetworkDestination.Server);
        }

    }
}
