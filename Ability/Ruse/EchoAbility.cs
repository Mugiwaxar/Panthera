using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Ruse
{
    internal class EchoAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.EchoAbilityID;
            ability.name = "ECHO_ABILITY_NAME";
            ability.desc = "ECHO_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.EchoAbility;
            ability.unlockLevel = PantheraConfig.Echo_unlockLevel;
            ability.maxLevel = PantheraConfig.Echo_maxLevel;
            ability.requiredAbilities.Add(PantheraConfig.MightyRoarAbilityID, 1);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
