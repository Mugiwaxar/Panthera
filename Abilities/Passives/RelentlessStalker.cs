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
    public class RelentlessStalker : PantheraAbility
    {

        public RelentlessStalker()
        {
            base.name = Utils.PantheraTokens.Get("ability_RelentlessStalkerName");
            base.abilityID = PantheraConfig.RelentlessStalker_AbilityID;
            base.type = AbilityType.passive;
            base.icon = PantheraAssets.RelentlessStalkerAbility;
            base.maxLevel = PantheraConfig.RelentlessStalker_maxLevel;
            base.cooldown = 0;
            base.requiredAbility = PantheraConfig.Prowl_AbilityID;
            base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_RelentlessStalkerDesc"), PantheraConfig.RelentlessStalker_CooldownReduction);
            base.desc2 = null;
        }

    }
}
