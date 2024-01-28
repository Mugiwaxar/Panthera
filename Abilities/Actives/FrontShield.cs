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
    public class FrontShield : PantheraAbility
    {

        public FrontShield()
        {
            name = Utils.PantheraTokens.Get("ability_FrontShieldName");
            abilityID = PantheraConfig.FrontShield_AbilityID;
            type = AbilityType.active;
            icon = Assets.FrontShieldSkill;
            maxLevel = PantheraConfig.FrontShield_maxLevel;
            cooldown = PantheraConfig.FrontShield_cooldown;
            requiredAbility = PantheraConfig.Guardian_AbilityID;
            desc1 = String.Format(Utils.PantheraTokens.Get("ability_FrontShieldDesc"), PantheraConfig.FrontShield_maxShieldHealthPercent * 100, PantheraConfig.FrontShield_rechargeDelayAfterDamage, PantheraConfig.FrontShield_rechargeDelayAfterDestroyed);
            desc2 = null;
        }

    }
}
