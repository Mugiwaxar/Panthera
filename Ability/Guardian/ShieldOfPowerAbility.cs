using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Guardian
{
    internal class ShieldOfPowerAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.ShieldOfPowerAbilityID;
            ability.name = "SHIELD_OF_POWER_ABILITY_NAME";
            ability.desc = "SHIELD_OF_POWER_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.ShieldOfPowerAbility;
            ability.maxLevel = PantheraConfig.ShieldOfPower_maxLevel;
            ability.unlockLevel = PantheraConfig.ShieldOfPower_unlockLevel;
            ability.requiredAbilities.Add(PantheraConfig.ShieldFocusAbilityID, 5);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
