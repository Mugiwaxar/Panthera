using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Ruse
{
    internal class RuseAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.RuseAbilityID;
            ability.name = Tokens.RuseAbilityName;
            ability.desc = Tokens.RuseAbilityDesc;
            ability.type = PantheraAbility.AbilityType.primary;
            ability.icon = Assets.RuseAbility;
            ability.unlockLevel = 0;
            ability.maxLevel = 1;
            ability.cooldown = 0;
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
