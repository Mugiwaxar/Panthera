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
    public class AirSlash : PantheraAbility
    {

        public AirSlash()
        {
            name = Utils.PantheraTokens.Get("ability_AirSlashName");
            abilityID = PantheraConfig.AirSlash_AbilityID;
            type = AbilityType.active;
            icon = PantheraAssets.AirSlashSkill;
            maxLevel = PantheraConfig.AirSlash_maxLevel;
            cooldown = PantheraConfig.AirSlash_cooldown;
            requiredAbility = PantheraConfig.Tornado_AbilityID;
            desc1 = String.Format(Utils.PantheraTokens.Get("ability_AirSlashDesc"), PantheraConfig.AirSlash_atkDamageMultiplier * 100) + String.Format(Utils.PantheraTokens.Get("Ability_AirSlashFuryDesc"), PantheraConfig.AirSlash_furyAdded);
            desc2 = null;
        }

    }
}
