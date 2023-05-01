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
    internal class HealingCleaveAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.HealingCleaveAbilityID;
            ability.name = "HEALING_CLEAVE_ABILITY_NAME";
            ability.desc = "HEALING_CLEAVE_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.HealingCleaveAbility;
            ability.unlockLevel = PantheraConfig.HealingCleave_unlockLevel;
            ability.maxLevel = PantheraConfig.HealingCleave_maxLevel;
            //ability.requiredAbilities.Add(PantheraConfig.ShieldFocusAbilityID, 1);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
