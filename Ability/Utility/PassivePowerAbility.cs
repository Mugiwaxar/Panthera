using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Utility
{
    public class PassivePowerAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.PassivePowerAbilityID;
            ability.name = "PASSIVE_POWER_ABILITY_NAME";
            ability.desc = "PASSIVE_POWER_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.PassivePowerAbility;
            ability.unlockLevel = PantheraConfig.PassivePower_unlockLevel;
            ability.maxLevel = PantheraConfig.PassivePower_maxLevel;
            ability.requiredAbilities.Add(PantheraConfig.RegenerationAbilityID, 1);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
