using Panthera.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Utility
{
    public class DeterminationAbility
    {
        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.DeterminationAbilityID;
            ability.name = "DETERMINATION_ABILITY_NAME";
            ability.desc = "DETERMINATION_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.DeterminationAbility;
            ability.unlockLevel = PantheraConfig.Determination_unlockLevel;
            ability.maxLevel = PantheraConfig.Determination_maxLevel;
            ability.requiredAbilities.Add(PantheraConfig.PrecisionAbilityID, 3);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }
    }
}
