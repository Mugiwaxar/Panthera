using Panthera.Base;
using System;

namespace Panthera.Abilities.Passives
{
    public class Furrify : PantheraAbility
    {

        public Furrify()
        {
            base.name = Utils.PantheraTokens.Get("ability_FurrifyName");
            base.abilityID = PantheraConfig.Furrify_AbilityID;
            base.type = AbilityType.passive;
            base.icon = PantheraAssets.FurrifyAbility;
            base.maxLevel = PantheraConfig.Furrify_maxLevel;
            base.cooldown = 0;
            base.requiredAbility = PantheraConfig.InnateProtection_AbilityID;
            base.desc1 = String.Format(Utils.PantheraTokens.Get("ability_FurrifyDesc"), PantheraConfig.Furrify_percent * 100);
            base.desc2 = null;
        }

    }
}
