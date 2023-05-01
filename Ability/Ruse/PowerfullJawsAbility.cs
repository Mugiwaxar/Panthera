using EntityStates.LunarGolem;
using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Ruse
{
    internal class PowerfullJawsAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.PowerfullJawsAbilityID;
            ability.name = "POWERFULL_JAWS_ABILITY_NAME";
            ability.desc = "POWERFULL_JAWS_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.PowerfullJawsAbility;
            ability.unlockLevel = PantheraConfig.PowerfullJaws_RequiredLevel;
            ability.maxLevel = PantheraConfig.PowerfullJaws_maxLevel;
            ability.cooldown = PantheraConfig.PowerfullJaws_cooldown;
            ability.requiredAbilities.Add(PantheraConfig.SharpenedFrangsAbilityID, 5);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
