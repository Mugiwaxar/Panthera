using Panthera;
using Panthera.Ability;
using Panthera.Ability.Guardian;
using Panthera.Ability.Utility;
using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Utility
{
    internal class ZoneHealAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.ZoneHealAbilityID;
            ability.name = "ZONE_HEAL_ABILITY_NAME";
            ability.desc = "ZONE_HEAL_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.active;
            ability.icon = Assets.ZoneHeal;
            ability.unlockLevel = PantheraConfig.ZoneHeal_unlockLevel;
            ability.maxLevel = PantheraConfig.ZoneHeal_maxLevel;
            ability.cooldown = PantheraConfig.ZoneHeal_cooldown1;
            ability.requiredPower = PantheraConfig.ZoneHeal_RequiredPower;
            ability.requiredAbilities.Add(PantheraConfig.PassivePowerAbilityID, 1);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
