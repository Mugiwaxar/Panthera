using Panthera.Base;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Abilities.Primaries
{
    public class FelineSkills : PantheraAbility
    {

        public FelineSkills()
        {
            base.name = Utils.PantheraTokens.Get("ability_FelineSkillsName");
            base.abilityID = PantheraConfig.FelineSkills_AbilityID;
            base.type = AbilityType.primary;
            base.icon = Assets.FelineSkillsAbility;
            base.maxLevel = 0;
            base.cooldown = 0;
            base.requiredAbility = 0;
            base.desc1 = String.Format(Utils.PantheraTokens.Get("ability_FelineSkillsDesc"));
            base.desc2 = null;
        }

    }
}
