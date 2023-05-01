using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Destruction
{
    internal class ImprovedClawsStormAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.ImprovedClawsStormAbilityID;
            ability.name = "IMPROVED_CLAWS_STORM_ABILITY_NAME";
            ability.desc = "IMPROVED_CLAWS_STORM_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.ClawStorm;
            ability.maxLevel = PantheraConfig.ImprovedClawsStorm_maxLevel;
            ability.requiredAbilities.Add(PantheraConfig.DestructionAbilityID, 1);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
