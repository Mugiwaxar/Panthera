using Panthera;
using Panthera.Abilities;
using Panthera.Abilities.Actives;
using Panthera.Abilities.Primaries;
using Panthera.Base;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Abilities.Actives
{
    public class GoldenRip : PantheraAbility
    {

        public GoldenRip()
        {
            name = Utils.PantheraTokens.Get("ability_GoldenRipName");
            abilityID = PantheraConfig.GoldenRip_AbilityID;
            type = AbilityType.active;
            icon = PantheraAssets.GoldenRipSkill;
            maxLevel = PantheraConfig.GoldenRip_maxLevel;
            cooldown = PantheraConfig.GoldenRip_cooldown;
            requiredAbility = PantheraConfig.Ambition_AbilityID;
            desc1 = String.Format(Utils.PantheraTokens.Get("ability_GoldenRipDesc"), PantheraConfig.GoldenRip_DamageMultiplier * 100, PantheraConfig.GoldenRip_addedCoin1) + String.Format(Utils.PantheraTokens.Get("skill_GoldenRipFuryDesc"), PantheraConfig.GoldenRip_furyAdded);
            desc2 = null;
        }

        public override void updateDesc()
        {
            int level = Panthera.ProfileComponent.GetAbilityLevel(base.abilityID);
            if (level <= 1)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_GoldenRipDesc"), PantheraConfig.GoldenRip_DamageMultiplier * 100, PantheraConfig.GoldenRip_addedCoin1) + String.Format(Utils.PantheraTokens.Get("skill_GoldenRipFuryDesc"), PantheraConfig.GoldenRip_furyAdded);
            else if (level == 2)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_GoldenRipDesc"), PantheraConfig.GoldenRip_DamageMultiplier * 100, PantheraConfig.GoldenRip_addedCoin2) + String.Format(Utils.PantheraTokens.Get("skill_GoldenRipFuryDesc"), PantheraConfig.GoldenRip_furyAdded);
            else if (level == 3)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_GoldenRipDesc"), PantheraConfig.GoldenRip_DamageMultiplier * 100, PantheraConfig.GoldenRip_addedCoin3) + String.Format(Utils.PantheraTokens.Get("skill_GoldenRipFuryDesc"), PantheraConfig.GoldenRip_furyAdded);
        }

    }
}
