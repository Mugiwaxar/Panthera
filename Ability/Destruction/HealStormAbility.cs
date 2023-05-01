using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Destruction
{
    internal class HealStormAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.HealingStormAbilityID;
            ability.name = "HEALING_STORM_ABILITY_NAME";
            ability.desc = "HEALING_STORM_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.HealingStormAbility;
            ability.maxLevel = PantheraConfig.HealingStorm_maxLevel;
            ability.unlockLevel = PantheraConfig.HealingStorm_unlockLevel;
            ability.requiredAbilities.Add(PantheraConfig.ImprovedClawsStormAbilityID, 1);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
