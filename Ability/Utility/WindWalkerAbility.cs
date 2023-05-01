using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Utility
{
    internal class WindWalkerAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.WindWalkerAbilityID;
            ability.name = "WIND_WALKER_ABILITY_NAME";
            ability.desc = "WIND_WALKER_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.WindWalkerAbility;
            ability.unlockLevel = PantheraConfig.WindWalker_unlockLevel;
            ability.maxLevel = PantheraConfig.WindWalker_maxLevel;
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
