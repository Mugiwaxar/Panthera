using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Utility
{
    public class RegenerationAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.RegenerationAbilityID;
            ability.name = "REGENERATION_ABILITY_NAME";
            ability.desc = "REGENERATION_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.RegenerationAbility;
            ability.unlockLevel = PantheraConfig.Regeneration_unlockLevel;
            ability.maxLevel = PantheraConfig.Regeneration_maxLevel;
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
