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
    public class GoldenStart : PantheraAbility
    {

        public GoldenStart()
        {
            name = Utils.PantheraTokens.Get("ability_GoldenStartName");
            abilityID = PantheraConfig.GoldenStart_AbilityID;
            type = AbilityType.passive;
            icon = PantheraAssets.GoldenStartAbility;
            maxLevel = PantheraConfig.GoldenStart_maxLevel;
            cooldown = 0;
            requiredAbility = PantheraConfig.Detection_AbilityID;
            desc1 = string.Format(Utils.PantheraTokens.Get("ability_GoldenStartDesc"), PantheraConfig.GoldenStart_addedGold);
        }

    }
}
