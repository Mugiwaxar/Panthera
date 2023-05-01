using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Guardian
{
    internal class ShieldBashAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.ShieldBashAbilityID;
            ability.name = "SHIELD_BASH_ABILITY_NAME";
            ability.desc = "SHIELD_BASH_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.hybrid;
            ability.icon = Assets.ShieldBash;
            ability.unlockLevel = PantheraConfig.ShieldBash_unlockLevel;
            ability.maxLevel = PantheraConfig.ShieldBash_maxLevel;
            ability.cooldown = PantheraConfig.ShieldBash_cooldown;
            ability.requiredEnergy = PantheraConfig.ShieldBash_requiredEnergy;
            ability.requiredAbilities.Add(PantheraConfig.ResidualEnergyAbilityID, 1);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
