using Panthera;
using Panthera.Abilities;
using Panthera.Abilities.Passives;
using Panthera.Abilities.Primaries;
using Panthera.Base;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Abilities.Passives
{
    public class MassiveHook : PantheraAbility
    {

        public MassiveHook()
        {
            base.name = Utils.PantheraTokens.Get("ability_MassiveHookName");
            base.abilityID = PantheraConfig.MassiveHook_AbilityID;
            base.type = AbilityType.passive;
            base.icon = PantheraAssets.MassiveHookAbility;
            base.maxLevel = PantheraConfig.MassiveHook_maxLevel;
            base.cooldown = 0;
            base.requiredAbility = PantheraConfig.ConvergenceHook_AbilityID;
            base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_MassiveHookDesc"));
            base.desc2 = null;
        }

    }
}
