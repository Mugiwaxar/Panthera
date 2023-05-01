using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Utility
{
    internal class LeapAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.LeapAbilityID;
            ability.name = "LEAP_ABILITY_NAME";
            ability.desc = "LEAP_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.hybrid;
            ability.icon = Assets.Leap;
            ability.unlockLevel = PantheraConfig.Leap_unlockLevel;
            ability.maxLevel = PantheraConfig.Leap_maxLevel;
            ability.cooldown = PantheraConfig.Leap_cooldown1;
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
