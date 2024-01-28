using Panthera;
using Panthera.Abilities;
using Panthera.Abilities.Actives;
using Panthera.Abilities.Primaries;
using Panthera.Base;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Abilities.Actives
{
    public class ConvergenceHook : PantheraAbility
    {

        public ConvergenceHook()
        {
            name = Utils.PantheraTokens.Get("ability_ConvergenceHookName");
            abilityID = PantheraConfig.ConvergenceHook_AbilityID;
            type = AbilityType.active;
            icon = Assets.ConvergenceHookSkill;
            maxLevel = PantheraConfig.ConvergenceHook_maxLevel;
            cooldown = PantheraConfig.ConvergenceHook_cooldown;
            requiredAbility = PantheraConfig.AirSlash_AbilityID;
            desc1 = String.Format(Utils.PantheraTokens.Get("ability_ConvergenceHookDesc"));
            desc2 = null;
        }

    }
}
