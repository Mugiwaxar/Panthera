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
    public class Tornado : PantheraAbility
    {

        public Tornado()
        {
            base.name = Utils.PantheraTokens.Get("ability_TornadoName");
            base.abilityID = PantheraConfig.Tornado_AbilityID;
            base.type = AbilityType.passive;
            base.icon = PantheraAssets.TornadoAbility;
            base.maxLevel = PantheraConfig.Tornado_maxLevel;
            base.cooldown = 0;
            base.requiredAbility = PantheraConfig.ClawsStorm_AbilityID;
            base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_TornadoDesc"), PantheraConfig.Tornado_damagePercent1 * 100, PantheraConfig.Tornado_resistPercent1 * 100);
            base.desc2 = null;
        }

        public override void updateDesc()
        {
            int level = Panthera.ProfileComponent.getAbilityLevel(base.abilityID);
            if (level <= 1)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_TornadoDesc"), PantheraConfig.Tornado_damagePercent1 * 100, PantheraConfig.Tornado_resistPercent1 * 100);
            else if (level == 2)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_TornadoDesc"), PantheraConfig.Tornado_damagePercent2 * 100, PantheraConfig.Tornado_resistPercent2 * 100);
            else if (level == 3)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_TornadoDesc"), PantheraConfig.Tornado_damagePercent3 * 100, PantheraConfig.Tornado_resistPercent3 * 100);
        }

    }
}
