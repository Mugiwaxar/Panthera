using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Ruse
{
    internal class SilentPredatorAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.SilentPredatorAbilityID;
            ability.name = "SILENT_PREDATOR_ABILITY_NAME";
            ability.desc = "SILENT_PREDATOR_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.SilentPredatorAbility;
            ability.unlockLevel = PantheraConfig.SilentPredator_unlockLevel;
            ability.maxLevel = PantheraConfig.SilentPredator_maxLevel;
            ability.requiredAbilities.Add(PantheraConfig.ShadowsMasterAbilityID, 1);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
