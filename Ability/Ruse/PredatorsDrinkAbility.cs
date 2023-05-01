using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Ruse
{
    internal class PredatorsDrinkAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.PredatorsDrinkAbilityID;
            ability.name = "PREDATORS_DRINK_ABILITY_NAME";
            ability.desc = "PREDATORS_DRINK_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.PredatorsDrinkAbility;
            ability.unlockLevel = PantheraConfig.PredatorsDrink_unlockLevel;
            ability.maxLevel = PantheraConfig.PredatorsDrink_maxLevel;
            ability.requiredAbilities.Add(PantheraConfig.SharpenedFrangsAbilityID, 1);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
