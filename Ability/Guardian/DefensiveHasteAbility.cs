using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Guardian
{
    internal class DefensiveHasteAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.DefensiveHasteAbilityID;
            ability.name = "DEFENSIVE_HASTE_ABILITY_NAME";
            ability.desc = "DEFENSIVE_HASTE_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.DefensiveHasteAbility;
            ability.maxLevel = PantheraConfig.DefensiveHaste_maxLevel;
            ability.unlockLevel = PantheraConfig.DefensiveHaste_unlockLevel;
            ability.requiredAbilities.Add(PantheraConfig.ShieldFocusAbilityID, 3);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
