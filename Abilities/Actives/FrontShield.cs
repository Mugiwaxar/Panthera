using Panthera.Base;
using System;

namespace Panthera.Abilities.Actives
{
    public class FrontShield : PantheraAbility
    {

        public FrontShield()
        {
            name = Utils.PantheraTokens.Get("ability_FrontShieldName");
            abilityID = PantheraConfig.FrontShield_AbilityID;
            type = AbilityType.active;
            icon = PantheraAssets.FrontShieldSkill;
            maxLevel = PantheraConfig.FrontShield_maxLevel;
            cooldown = PantheraConfig.FrontShield_cooldown;
            requiredAbility = PantheraConfig.RoarOfResilience_AbilityID;
            desc1 = String.Format(Utils.PantheraTokens.Get("ability_FrontShieldDesc"), PantheraConfig.FrontShield_maxShieldHealthPercent * 100, PantheraConfig.FrontShield_rechargeDelayAfterDamage, PantheraConfig.FrontShield_rechargeDelayAfterDestroyed);
            desc2 = null;
        }

    }
}
