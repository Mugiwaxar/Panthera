using Panthera.Base;
using System;

namespace Panthera.Abilities.Actives
{
    public class GhostRip : PantheraAbility
    {

        public GhostRip()
        {
            name = Utils.PantheraTokens.Get("ability_GhostRipName");
            abilityID = PantheraConfig.GhostRip_AbilityID;
            type = AbilityType.active;
            icon = PantheraAssets.GhostRipSkill;
            maxLevel = PantheraConfig.GhostRip_maxLevel;
            cooldown = PantheraConfig.GhostRip_cooldown;
            requiredAbility = PantheraConfig.Prowl_AbilityID;
            desc1 = String.Format(Utils.PantheraTokens.Get("ability_GhostRipDesc"), PantheraConfig.GhostRip_damageMultiplier * 100, PantheraConfig.GhostRip_stunDuration, PantheraConfig.GhostRip_critMultiplier) + String.Format(Utils.PantheraTokens.Get("skill_GhostRipFuryDesc"), PantheraConfig.GhostRip_furyAdded);
            desc2 = null;
        }

    }
}
