using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Destruction
{
    internal class DestructionAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.DestructionAbilityID;
            ability.name = "DESTRUCTION_ABILITY_NAME";
            ability.desc = "DESTRUCTION_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.primary;
            ability.icon = Assets.DestructionAbility;
            ability.unlockLevel = 0;
            ability.maxLevel = 1;
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
