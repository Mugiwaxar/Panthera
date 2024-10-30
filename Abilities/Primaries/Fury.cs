using Panthera.Base;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Abilities.Primaries
{
    public class Fury : PantheraAbility
    {

        public Fury()
        {
            base.name = Utils.PantheraTokens.Get("ability_FuryName");
            base.abilityID = PantheraConfig.Fury_AbilityID;
            base.type = AbilityType.primary;
            base.icon = PantheraAssets.FurySkill;
            base.maxLevel = PantheraConfig.Fury_maxLevel;
            base.cooldown = PantheraConfig.Fury_cooldown;
            base.requiredAbility = PantheraConfig.SharpenedFangs_AbilityID;
            base.desc1 = String.Format(Utils.PantheraTokens.Get("ability_FuryDesc"), PantheraConfig.Fury_increasedAttackSpeed * 100, PantheraConfig.Fury_increasedMoveSpeed * 100);
            base.desc2 = null;
        }

    }
}
