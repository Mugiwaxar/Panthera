using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Ruse
{
    internal class ProwlAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.ProwlAbilityID;
            ability.name = "PROWL_SKILL_NAME";
            ability.desc = "PROWL_SKILL_DESC";
            ability.type = PantheraAbility.AbilityType.active;
            ability.icon = Assets.Prowl;
            ability.unlockLevel = PantheraConfig.Prowl_unlockLevel;
            ability.maxLevel = PantheraConfig.Prowl_maxLevel;
            ability.cooldown = PantheraConfig.Prowl_coolDown;
            ability.requiredAbilities.Add(PantheraConfig.RuseAbilityID, 1);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
