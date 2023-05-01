using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Destruction
{
    internal class HellCatAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.HellCatAbilityID;
            ability.name = "HELL_CAT_ABILITY_NAME";
            ability.desc = "HELL_CAT_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.HellCatAbility;
            ability.maxLevel = PantheraConfig.HellCat_maxLevel;
            ability.unlockLevel = PantheraConfig.HellCat_unlockLevel;
            ability.requiredAbilities.Add(PantheraConfig.BurningSpiritAbilityID, 1);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
