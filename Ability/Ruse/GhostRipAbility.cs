using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Ruse
{
    internal class GhostRipAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.GhostRipAbilityID;
            ability.name = "GHOST_RIP_ABILITY_NAME";
            ability.desc = "GHOST_RIP_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.GhostRipAbility;
            ability.unlockLevel = PantheraConfig.GhostRip_unlockLevel;
            ability.maxLevel = PantheraConfig.GhostRip_maxLevel;
            ability.requiredAbilities.Add(PantheraConfig.ShadowsMasterAbilityID, 1);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
