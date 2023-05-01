using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Destruction
{
    internal class IgnitionAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.IgnitionAbilityID;
            ability.name = "IGNITION_ABILITY_NAME";
            ability.desc = "IGNITION_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.IgnitionAbility;
            ability.maxLevel = PantheraConfig.Ignition_maxLevel;
            ability.unlockLevel = PantheraConfig.Ignition_unlockLevel;
            ability.requiredAbilities.Add(PantheraConfig.CircularSawAbilityID, 1);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
