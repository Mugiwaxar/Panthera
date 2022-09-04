using Panthera.Components;
using Panthera.Skills;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Passives
{
    class NineLives
    {

        public static void ApplyNineLives(PantheraHealthComponent comp)
        {
            comp.barrier = comp.body.maxHealth;
            comp.body.RemoveBuff(Buff.nineLives);
            comp.Networkhealth = comp.fullHealth * PantheraConfig.healthPercentAfterNineLivesActivated;
            comp.ptraObj.GetPassiveScript().lastNineLivesTime = Time.time;
            //Utils.Functions.SpawnEffect(
            //    comp.gameObject, Assets.NineLivesFX,
            //    comp.body.corePosition, PantheraConfig.Model_generalScale,
            //    null,
            //    Util.QuaternionSafeLookRotation(comp.characterDirection.forward)
            //    );
        }

    }
}
