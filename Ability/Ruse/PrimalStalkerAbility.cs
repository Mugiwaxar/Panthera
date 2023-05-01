using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Ruse
{
    internal class PrimalStalkerAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.PrimalStalkerAbilityID;
            ability.name = "PRIMAL_STALKER_ABILITY_NAME";
            ability.desc = "PRIMAL_STALKER_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.PrimalStalkerAbility;
            ability.unlockLevel = PantheraConfig.PrimalStalker_unlockLevel;
            ability.maxLevel = PantheraConfig.PrimalStalker_maxLevel;
            ability.requiredAbilities.Add(PantheraConfig.ShadowsMasterAbilityID, 3);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
