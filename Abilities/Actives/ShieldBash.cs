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
    public class ShieldBash : PantheraAbility
    {

        public ShieldBash()
        {
            name = Utils.PantheraTokens.Get("ability_ShieldBashName");
            abilityID = PantheraConfig.ShieldBash_AbilityID;
            type = AbilityType.active;
            icon = PantheraAssets.ShieldBashSkill;
            maxLevel = PantheraConfig.ShieldBash_maxLevel;
            cooldown = PantheraConfig.ShieldBash_cooldown;
            requiredAbility = PantheraConfig.FrontShield_AbilityID;
            desc1 = String.Format(Utils.PantheraTokens.Get("ability_ShieldBashDesc"), PantheraConfig.ShieldBash_damageMultiplier * 100, PantheraConfig.ShieldBash_stunDuration);
            desc2 = null;
        }

    }
}
