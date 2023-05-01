using Panthera.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Utility
{
    public class ConcentrationAbility
    {
        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.ConcentrationAbilityID;
            ability.name = "CONCENTRATION_ABILITY_NAME";
            ability.desc = "CONCENTRATION_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.ConcentrationAbility;
            ability.unlockLevel = PantheraConfig.Concentration_unlockLevel;
            ability.maxLevel = PantheraConfig.Concentration_maxLevel;
            ability.requiredAbilities.Add(PantheraConfig.PrescienceAbilityID, 1);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }
    }
}
