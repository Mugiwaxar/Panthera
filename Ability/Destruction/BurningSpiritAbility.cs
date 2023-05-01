using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Destruction
{
    internal class BurningSpiritAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.BurningSpiritAbilityID;
            ability.name = "BURNING_SPIRIT_ABILITY_NAME";
            ability.desc = "BURNING_SPIRIT_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.BurningSpiritAbility;
            ability.maxLevel = PantheraConfig.BurningSpirit_maxLevel;
            ability.unlockLevel = PantheraConfig.BurningSpirit_unlockLevel;
            ability.requiredAbilities.Add(PantheraConfig.TheRipperAbilityID, 1);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
