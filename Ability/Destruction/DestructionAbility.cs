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
            ability.name = Tokens.DestructionAbilityName;
            ability.desc = Tokens.DestructionAbilityDesc;
            ability.type = PantheraAbility.AbilityType.primary;
            ability.icon = Assets.DestructionAbility;
            ability.unlockLevel = 0;
            ability.maxLevel = 1;
            ability.cooldown = 0;
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
