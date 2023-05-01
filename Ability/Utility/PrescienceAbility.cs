using Panthera.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Utility
{
    public class PrescienceAbility
    {
        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.PrescienceAbilityID;
            ability.name = "PRESCIENCE_ABILITY_NAME";
            ability.desc = "PRESCIENCE_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.PrescienceAbility;
            ability.unlockLevel = PantheraConfig.Prescience_unlockLevel;
            ability.maxLevel = PantheraConfig.Prescience_maxLevel;
            ability.requiredAbilities.Add(PantheraConfig.DetectionAbilityID, 1);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }
    }
}
