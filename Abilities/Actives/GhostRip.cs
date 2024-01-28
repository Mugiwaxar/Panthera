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
    public class GhostRip : PantheraAbility
    {

        public GhostRip()
        {
            name = Utils.PantheraTokens.Get("ability_GhostRipName");
            abilityID = PantheraConfig.GhostRip_AbilityID;
            type = AbilityType.active;
            icon = Assets.GhostRipSkill;
            maxLevel = PantheraConfig.GhostRip_maxLevel;
            cooldown = PantheraConfig.GhostRip_cooldown;
            requiredAbility = PantheraConfig.Prowl_AbilityID;
            desc1 = String.Format(Utils.PantheraTokens.Get("ability_GhostRipDesc"), PantheraConfig.GhostRip_DamageMultiplier * 100, PantheraConfig.GhostRip_stunDuration) + String.Format(Utils.PantheraTokens.Get("skill_GhostRipFuryDesc"), PantheraConfig.GhostRip_furyAdded);
            desc2 = null;
        }

    }
}
