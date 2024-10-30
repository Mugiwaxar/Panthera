using Panthera;
using Panthera.Abilities;
using Panthera.Abilities.Actives;
using Panthera.Abilities.Passives;
using Panthera.Abilities.Primaries;
using Panthera.Base;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Abilities.Actives
{
    public class ClawsSharpening : PantheraAbility
    {

        public ClawsSharpening()
        {
            name = Utils.PantheraTokens.Get("ability_ClawsSharpeningName");
            abilityID = PantheraConfig.ClawsSharpening_AbilityID;
            type = AbilityType.passive;
            icon = PantheraAssets.ClawsSharpeningAbility;
            maxLevel = PantheraConfig.ClawsSharpening_maxLevel;
            cooldown = 0;
            requiredAbility = PantheraConfig.Fury_AbilityID;
            desc1 = Utils.PantheraTokens.Get("ability_ClawsSharpeningDesc");
        }

    }
}
