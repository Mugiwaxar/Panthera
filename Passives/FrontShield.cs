using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.NetworkMessages;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Panthera.Passives
{
    internal class FrontShield
    {

        public static void EnableFrontShield(PantheraObj obj)
        {
            if (obj.frontShield.active == true) return;
            obj.frontShield.SetActive(true);
            if (RoR2Application.isInMultiPlayer) new ServerSetFrontShield(obj.gameObject, true).Send(NetworkDestination.Server);
        }

        public static void DisableFrontShield(PantheraObj obj)
        {
            if (obj.frontShield.active == false) return;
            obj.frontShield.SetActive(false);
            if (RoR2Application.isInMultiPlayer) new ServerSetFrontShield(obj.gameObject, false).Send(NetworkDestination.Server);
        }

    }
}
