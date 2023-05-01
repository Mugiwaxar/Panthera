using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Destruction
{
    internal class TheSlashPerAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.TheSlashPerAbilityID;
            ability.name = "THE_SLASHPER_ABILITY_NAME";
            ability.desc = "THE_SLASHPER_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.TheSlashPerAbility;
            ability.maxLevel = PantheraConfig.TheSlashPer_maxLevel;
            ability.unlockLevel = PantheraConfig.TheSlashPer_unlockLevel;
            ability.requiredAbilities.Add(PantheraConfig.CircularSawAbilityID, 1);
            ability.requiredAbilities.Add(PantheraConfig.TheRipperAbilityID, 1);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
