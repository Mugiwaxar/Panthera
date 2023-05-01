using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Ruse
{
    internal class PiercingWavesAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.PiercingWavesAbilityID;
            ability.name = "PIERCING_WAVES_ABILITY_NAME";
            ability.desc = "PIERCING_WAVES_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.PiercingWavesAbility;
            ability.unlockLevel = PantheraConfig.PiercingWaves_unlockLevel;
            ability.maxLevel = PantheraConfig.PiercingWaves_maxLevel;
            ability.requiredAbilities.Add(PantheraConfig.EchoAbilityID, 3);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
