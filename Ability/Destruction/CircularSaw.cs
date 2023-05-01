using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Destruction
{
    internal class CircularSawAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.CircularSawAbilityID;
            ability.name = "CIRCULAR_SAW_ABILITY_NAME";
            ability.desc = "CIRCULAR_SAW_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.CircularSawAbility;
            ability.maxLevel = PantheraConfig.CircualSaw_maxLevel;
            ability.unlockLevel = PantheraConfig.CircualSaw_unlockLevel;
            ability.requiredAbilities.Add(PantheraConfig.SlashAbilityID, 1);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
