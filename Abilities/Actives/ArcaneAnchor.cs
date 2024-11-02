using Panthera.Base;
using System;

namespace Panthera.Abilities.Actives
{
    public class ArcaneAnchor : PantheraAbility
    {

        public ArcaneAnchor()
        {
            name = Utils.PantheraTokens.Get("ability_ArcaneAnchorName");
            abilityID = PantheraConfig.ArcaneAnchor_AbilityID;
            type = AbilityType.active;
            icon = PantheraAssets.ArcaneAnchorSkill;
            maxLevel = PantheraConfig.ArcaneAnchor_maxLevel;
            cooldown = PantheraConfig.ArcaneAnchor_cooldown;
            requiredAbility = PantheraConfig.ImprovedShield_AbilityID;
            desc1 = String.Format(Utils.PantheraTokens.Get("ability_ArcaneAnchorDesc"));
            desc2 = null;
        }

    }
}
