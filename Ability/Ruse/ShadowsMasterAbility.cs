using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Ruse
{
    internal class ShadowsMasterAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.ShadowsMasterAbilityID;
            ability.name = "SHADOWS_MASTER_ABILITY_NAME";
            ability.desc = "SHADOWS_MASTER_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.ShadowMasterAbility;
            ability.unlockLevel = PantheraConfig.ShadowsMaster_unlockLevel;
            ability.maxLevel = PantheraConfig.ShadowsMaster_maxLevel;
            ability.requiredAbilities.Add(PantheraConfig.ProwlAbilityID, 1);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
