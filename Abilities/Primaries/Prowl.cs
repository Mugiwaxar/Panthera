using Panthera.Base;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Abilities.Primaries
{
    public class Prowl : PantheraAbility
    {

        public Prowl()
        {
            base.name = Utils.PantheraTokens.Get("ability_ProwlName");
            base.abilityID = PantheraConfig.Prowl_AbilityID;
            base.type = AbilityType.primary;
            base.icon = PantheraAssets.ProwlSkill;
            base.maxLevel = PantheraConfig.Prowl_maxLevel;
            base.cooldown = PantheraConfig.Prowl_coolDown;
            base.requiredAbility = PantheraConfig.WindWalker_AbilityID;
            base.desc1 = Utils.PantheraTokens.Get("ability_ProwlDesc");
            base.desc2 = null;
        }

    }
}
