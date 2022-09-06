using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Utility
{
    internal class ImprovedLeapAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.ImprovedLeapAbilityID;
            ability.name = Tokens.ImprovedLeapAbilityName;
            ability.desc = Tokens.ImprovedLeapAbilityDesc;
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.Leap;
            ability.unlockLevel = 5;
            ability.maxLevel = 3;
            ability.cooldown = 0;
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
