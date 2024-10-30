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
    public class Predator : PantheraAbility
    {

        public Predator()
        {
            base.name = Utils.PantheraTokens.Get("ability_PredatorName");
            base.abilityID = PantheraConfig.Predator_AbilityID;
            base.type = AbilityType.passive;
            base.icon = PantheraAssets.PredatorAbility;
            base.maxLevel = PantheraConfig.Predator_maxLevel;
            base.cooldown = 0;
            base.requiredAbility = PantheraConfig.FelineSkills_AbilityID;
            base.desc1 = String.Format(Utils.PantheraTokens.Get("ability_PredatorDesc"), PantheraConfig.Predator_steakHealthAdded);
            base.desc2 = null;
        }

    }
}
