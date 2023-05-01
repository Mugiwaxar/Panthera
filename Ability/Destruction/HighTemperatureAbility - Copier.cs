using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Destruction
{
    internal class HighTemperatureAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.HighTemperatureAbilityID;
            ability.name = "HIGH_TEMPERATURE_ABILITY_NAME";
            ability.desc = "HIGH_TEMPERATURE_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.HighTemperatureAbility;
            ability.maxLevel = PantheraConfig.HighTemperature_maxLevel;
            ability.unlockLevel = PantheraConfig.HighTemperature_unlockLevel;
            ability.requiredAbilities.Add(PantheraConfig.IgnitionAbilityID, 1);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
