using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Guardian
{
    internal class ShieldFocusAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.ShieldFocusAbilityID;
            ability.name = "SHIELD_FOCUS_ABILITY_NAME";
            ability.desc = "SHIELD_FOCUS_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.FrontShield;
            ability.maxLevel = PantheraConfig.ShieldFocus_maxLevel;
            ability.requiredAbilities.Add(PantheraConfig.GuardianAbilityID, 1);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
