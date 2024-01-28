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
    public class RoarOfResilience : PantheraAbility
    {

        public RoarOfResilience()
        {
            name = Utils.PantheraTokens.Get("ability_RoarOfResilienceName");
            abilityID = PantheraConfig.RoarOfResilience_AbilityID;
            type = AbilityType.passive;
            icon = Assets.RoarOfResilienceAbility;
            maxLevel = PantheraConfig.RoarOfResilience_maxLevel;
            cooldown = 0;
            requiredAbility = PantheraConfig.Guardian_AbilityID;
            desc1 = Utils.PantheraTokens.Get("ability_RoarOfResilienceDesc");
        }

    }
}
