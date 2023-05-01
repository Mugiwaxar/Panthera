using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Destruction
{
    internal class BloodyRageAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.BloodyRageAbilityID;
            ability.name = "BLOODY_RAGE_ABILITY_NAME";
            ability.desc = "BLOODY_RAGE_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.BloodyRageAbility;
            ability.maxLevel = PantheraConfig.BloodyRage_maxLevel;
            ability.unlockLevel = PantheraConfig.BloodyRage_unlockLevel;
            ability.requiredAbilities.Add(PantheraConfig.TheRipperAbilityID, 1);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
