using Panthera;
using Panthera.Abilities;
using Panthera.Abilities.Actives;
using Panthera.Abilities.Primaries;
using Panthera.Base;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Abilities.Primaries
{
    public class Detection : PantheraAbility
    {

        public Detection()
        {
            name = Utils.PantheraTokens.Get("ability_DetectionName");
            abilityID = PantheraConfig.Detection_AbilityID;
            type = AbilityType.primary;
            icon = Assets.DetectionSkill;
            maxLevel = PantheraConfig.Detection_maxLevel;
            cooldown = PantheraConfig.Detection_cooldown;
            requiredAbility = PantheraConfig.Predator_AbilityID;
            desc1 = Utils.PantheraTokens.Get("ability_DetectionDesc");
            desc2 = null;
        }

    }
}
