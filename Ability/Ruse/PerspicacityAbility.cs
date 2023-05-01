using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Ruse
{
    internal class PerspicacityAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.PerspicacityAbilityID;
            ability.name = "PERSPICACITY_ABILITY_NAME";
            ability.desc = "PERSPICACITY_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.PerspicacityAbility;
            ability.unlockLevel = PantheraConfig.Perspicacity_unlockLevel;
            ability.maxLevel = PantheraConfig.Perspicacity_maxLevel;
            ability.requiredAbilities.Add(PantheraConfig.SharpenedFrangsAbilityID, 1);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
