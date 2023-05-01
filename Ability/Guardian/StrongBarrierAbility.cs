using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Guardian
{
    internal class StrongBarrierAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.StrongBarrierAbilityID;
            ability.name = "STRONG_BARRIER_ABILITY_NAME";
            ability.desc = "STRONG_BARRIER_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.StrongBarrierAbility;
            ability.unlockLevel = PantheraConfig.StrongBarrier_unlockLevel;
            ability.maxLevel = PantheraConfig.StrongBarrier_maxLevel;
            ability.requiredAbilities.Add(PantheraConfig.ResidualEnergyAbilityID, 1);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
