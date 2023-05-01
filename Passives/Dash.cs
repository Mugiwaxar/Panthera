using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Passives
{
    class Dash
    {
        public static void StartDash(PantheraObj ptraObj)
        {
            if (ptraObj.dashing == true) return;
            ptraObj.dashing = true;
            ptraObj.pantheraMotor.startSprint = true;
            Utils.Sound.playSound(Utils.Sound.Dash, ptraObj.gameObject);
            ptraObj.pantheraFX.SetDashFX(true);
        }

        public static void StopDash(PantheraObj ptraObj)
        {
            if (ptraObj.dashing == false) return;
            ptraObj.dashing = false;
            ptraObj.pantheraFX.SetDashFX(false);
            ptraObj.skillLocator.startCooldown(PantheraConfig.Dash_SkillID);
        }

    }
}
