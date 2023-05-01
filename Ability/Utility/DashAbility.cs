using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Utility
{
    internal class DashAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.DashAbilityID;
            ability.name = "DASH_ABILITY_NAME";
            ability.desc = "DASH_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.hybrid;
            ability.icon = Assets.Dash;
            ability.unlockLevel = PantheraConfig.Dash_unlockLevel;
            ability.maxLevel = PantheraConfig.Dash_maxLevel;
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
