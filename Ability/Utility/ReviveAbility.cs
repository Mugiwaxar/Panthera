using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Utility
{
    internal class ReviveAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.ReviveAbilityID;
            ability.name = "REVIVE_SKILL_NAME";
            ability.desc = "REVIVE_SKILL_DESC";
            ability.type = PantheraAbility.AbilityType.active;
            ability.icon = Assets.Revive;
            ability.unlockLevel = PantheraConfig.Revive_unlockLevel;
            ability.maxLevel = PantheraConfig.Revive_maxLevel;
            ability.requiredPower = PantheraConfig.Revive_powerRequired;
            ability.cooldown = PantheraConfig.Revive_cooldown;
            ability.requiredAbilities.Add(PantheraConfig.ZoneHealAbilityID, 1);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
