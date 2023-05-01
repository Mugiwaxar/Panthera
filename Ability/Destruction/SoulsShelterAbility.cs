using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Destruction
{
    internal class SoulsShelterAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.SoulsShelterAbilityID;
            ability.name = "SOULS_SHELTER_ABILITY_NAME";
            ability.desc = "SOULS_SHELTER_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.IgnitionAbility;
            ability.maxLevel = PantheraConfig.SoulsShelter_maxLevel;
            ability.unlockLevel = PantheraConfig.SoulsShelter_unlockLevel;
            ability.requiredAbilities.Add(PantheraConfig.CircularSawAbilityID, 1);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
