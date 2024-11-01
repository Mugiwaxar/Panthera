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
    public class InnateProtection : PantheraAbility
    {

        public InnateProtection()
        {
            base.name = Utils.PantheraTokens.Get("ability_InnateProtectionName");
            base.abilityID = PantheraConfig.InnateProtection_AbilityID;
            base.type = AbilityType.passive;
            base.icon = PantheraAssets.InnateProtectionAbility;
            base.maxLevel = PantheraConfig.InnateProtection_maxLevel;
            base.cooldown = 0;
            base.requiredAbility = PantheraConfig.Guardian_AbilityID;
            base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_InnateProtectionDesc"), PantheraConfig.InnateProtection_percent1 * 100);
            base.desc2 = null;
        }

        public override void updateDesc()
        {
            int level = Panthera.ProfileComponent.GetAbilityLevel(base.abilityID);
            if (level <= 1)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_InnateProtectionDesc"), PantheraConfig.InnateProtection_percent1 * 100);
            else if (level == 2)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_InnateProtectionDesc"), PantheraConfig.InnateProtection_percent2 * 100);
        }

    }
}
