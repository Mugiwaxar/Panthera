using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Destruction
{
    internal class GodOfReapersAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.GodOfReapersAbilityID;
            ability.name = "GOD_OF_REAPERS_ABILITY_NAME";
            ability.desc = "GOD_OF_REAPERS_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.GodOfReapersAbility;
            ability.maxLevel = PantheraConfig.GodOfReapers_maxLevel;
            ability.unlockLevel = PantheraConfig.GodOfReapers_unlockLevel;
            ability.requiredAbilities.Add(PantheraConfig.TheRipperAbilityID, 1);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
