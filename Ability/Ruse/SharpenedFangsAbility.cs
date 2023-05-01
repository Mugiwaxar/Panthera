using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Ruse
{
    internal class SharpenedFangsAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.SharpenedFrangsAbilityID;
            ability.name = "SHARPENED_FANGS_ABILITY_NAME";
            ability.desc = "SHARPENED_FANGS_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.FuriousBite;
            ability.unlockLevel = 0;
            ability.maxLevel = PantheraConfig.SharpenedFangs_maxLevel;
            ability.requiredAbilities.Add(PantheraConfig.RuseAbilityID, 1);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
