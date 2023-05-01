using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Destruction
{
    internal class TornadoAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.TornadoAbilityID;
            ability.name = "TORNADO_ABILITY_NAME";
            ability.desc = "TORNADO_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.TornadoAbility;
            ability.maxLevel = PantheraConfig.Tornado_maxLevel;
            ability.unlockLevel = PantheraConfig.Tornado_unlockLevel;
            ability.requiredAbilities.Add(PantheraConfig.ImprovedClawsStormAbilityID, 1);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
