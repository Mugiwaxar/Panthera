using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Ruse
{
    internal class MyghtyRoarAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.MightyRoarAbilityID;
            ability.name = "MIGHTY_ROAR_SKILL_NAME";
            ability.desc = "MIGHTY_ROAR_SKILL_DESC";
            ability.type = PantheraAbility.AbilityType.active;
            ability.icon = Assets.MightyRoar;
            ability.unlockLevel = PantheraConfig.MightyRoar_unlockLevel;
            ability.maxLevel = PantheraConfig.MightyRoar_maxLevel;
            ability.cooldown = PantheraConfig.MightyRoar_cooldown;
            ability.requiredAbilities.Add(PantheraConfig.ProwlAbilityID, 1);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
