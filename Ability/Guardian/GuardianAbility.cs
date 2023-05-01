using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Destruction
{
    internal class GuardianAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.GuardianAbilityID;
            ability.name = "GUARDIAN_ABILITY_NAME";
            ability.desc = "GUARDIAN_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.primary;
            ability.icon = Assets.GuardianAbility;
            ability.unlockLevel = 0;
            ability.maxLevel = 1;
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
