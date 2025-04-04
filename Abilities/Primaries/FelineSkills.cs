﻿using Panthera.Base;
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
            base.icon = PantheraAssets.FelineSkillsAbility;
            base.maxLevel = 1;
            base.cooldown = 0;
            base.requiredAbility = 0;
            base.desc1 = Utils.PantheraTokens.Get("ability_FelineSkillsDesc");
        }

    }
}
