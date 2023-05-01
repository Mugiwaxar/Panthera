using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Guardian
{
    internal class ResidualEnergyAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.ResidualEnergyAbilityID;
            ability.name = "RESIDUAL_ENERGY_ABILITY_NAME";
            ability.desc = "RESIDUAL_ENERGY_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.ResidualEnergyAbility;
            ability.unlockLevel = PantheraConfig.ResidualEnergy_unlockLevel;
            ability.maxLevel = PantheraConfig.ResidualEnergy_maxLevel;
            ability.requiredAbilities.Add(PantheraConfig.ShieldFocusAbilityID, 3);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
