using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Destruction
{
    internal class InstinctiveResistanceAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.InstinctiveResistanceAbilityID;
            ability.name = "INSTINCTIVE_RESISTANCE_ABILITY_NAME";
            ability.desc = "INSTINCTIVE_RESISTANCE_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.InstinctiveResistanceAbility;
            ability.maxLevel = PantheraConfig.InstinctiveResistance_maxLevel;
            ability.unlockLevel = PantheraConfig.InstinctiveResistance_unlockLevel;
            ability.requiredAbilities.Add(PantheraConfig.TheRipperAbilityID, 1);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
