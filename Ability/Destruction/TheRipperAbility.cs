using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Destruction
{
    internal class TheRipperAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.TheRipperAbilityID;
            ability.name = "THE_RIPPER_ABILITY_NAME";
            ability.desc = "THE_RIPPER_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.TheRipperAbility;
            ability.maxLevel = PantheraConfig.TheRipper_maxLevel;
            ability.unlockLevel = PantheraConfig.TheRipper_unlockLevel;
            ability.requiredAbilities.Add(PantheraConfig.ImprovedClawsStormAbilityID, 1);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
