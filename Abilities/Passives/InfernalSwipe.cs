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
    public class InfernalSwipe : PantheraAbility
    {

        public InfernalSwipe()
        {
            base.name = Utils.PantheraTokens.Get("ability_InfernalSwipeName");
            base.abilityID = PantheraConfig.InfernalSwipe_AbilityID;
            base.type = AbilityType.passive;
            base.icon = PantheraAssets.InfernalSwipeAbility;
            base.maxLevel = PantheraConfig.InfernalSwipe_maxLevel;
            base.cooldown = 0;
            base.requiredAbility = PantheraConfig.Fury_AbilityID;
            base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_InfernalSwipeDesc"), PantheraConfig.InfernalSwipe_damagePercent1 * 100, PantheraConfig.InfernalSwipe_ingnitionChance1 * 100);
            base.desc2 = null;
        }

        public override void updateDesc()
        {
            int level = Panthera.ProfileComponent.getAbilityLevel(base.abilityID);
            if (level <= 1)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_InfernalSwipeDesc"), PantheraConfig.InfernalSwipe_damagePercent1 * 100, PantheraConfig.InfernalSwipe_ingnitionChance1 * 100);
            else if (level == 2)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_InfernalSwipeDesc"), PantheraConfig.InfernalSwipe_damagePercent2 * 100, PantheraConfig.InfernalSwipe_ingnitionChance2 * 100);
        }

    }
}
