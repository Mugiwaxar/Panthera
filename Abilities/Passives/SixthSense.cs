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
    public class SixthSense : PantheraAbility
    {

        public SixthSense()
        {
            base.name = Utils.PantheraTokens.Get("ability_SixthSenseName");
            base.abilityID = PantheraConfig.SixthSense_AbilityID;
            base.type = AbilityType.passive;
            base.icon = PantheraAssets.SixthSenseAbility;
            base.maxLevel = PantheraConfig.SixthSense_maxLevel;
            base.cooldown = 0;
            base.requiredAbility = PantheraConfig.Detection_AbilityID;
            base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_SixthSenseDesc"));
            base.desc2 = null;
        }

    }
}
