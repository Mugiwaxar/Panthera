using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Destruction
{
    internal class SacredFlamesAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.SacredFlamesAbilityID;
            ability.name = "SACRED_FLAMES_ABILITY_NAME";
            ability.desc = "SACRED_FLAMES_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.SacredFlamesAbility;
            ability.maxLevel = PantheraConfig.SacredFlames_maxLevel;
            ability.unlockLevel = PantheraConfig.SacredFlames_unlockLevel;
            ability.requiredAbilities.Add(PantheraConfig.IgnitionAbilityID, 3);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
