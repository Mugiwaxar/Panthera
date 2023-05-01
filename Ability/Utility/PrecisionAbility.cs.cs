using Panthera.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Utility
{
    public class PrecisionAbility
    {
        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.PrecisionAbilityID;
            ability.name = "PRECISION_ABILITY_NAME";
            ability.desc = "PRECISION_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.PrecisionAbility;
            ability.unlockLevel = PantheraConfig.Precision_unlockLevel;
            ability.maxLevel = PantheraConfig.Precision_maxLevel;
            ability.requiredAbilities.Add(PantheraConfig.ConcentrationAbilityID, 1);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }
    }
}
