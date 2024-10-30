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
    public class EternalFury : PantheraAbility
    {

        public EternalFury()
        {
            base.name = Utils.PantheraTokens.Get("ability_EternalFuryName");
            base.abilityID = PantheraConfig.EternalFury_AbilityID;
            base.type = AbilityType.passive;
            base.icon = PantheraAssets.EternalFuryAbility;
            base.maxLevel = PantheraConfig.EternalFury_maxLevel;
            base.cooldown = 0;
            base.requiredAbility = PantheraConfig.Fury_AbilityID;
            base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_EternalFuryDesc"), PantheraConfig.EternalFury_reductionPercent1 * 100, PantheraConfig.EternalFury_startPercent1 * 100);
            base.desc2 = null;
        }

        public override void updateDesc()
        {
            int level = Panthera.ProfileComponent.getAbilityLevel(base.abilityID);
            if (level <= 1)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_EternalFuryDesc"), PantheraConfig.EternalFury_reductionPercent1 * 100, PantheraConfig.EternalFury_startPercent1 * 100);
            else if (level == 2)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_EternalFuryDesc"), PantheraConfig.EternalFury_reductionPercent2 * 100, PantheraConfig.EternalFury_startPercent2 * 100);
            else if (level == 3)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_EternalFuryDesc"), PantheraConfig.EternalFury_reductionPercent3 * 100, PantheraConfig.EternalFury_startPercent3 * 100);
        }

    }
}
