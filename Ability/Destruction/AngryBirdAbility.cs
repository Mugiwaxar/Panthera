using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Destruction
{
    internal class AngryBirdAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.AngryBirdAbilityID;
            ability.name = "ANGRY_BIRD_ABILITY_NAME";
            ability.desc = "ANGRY_BIRD_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.AngryBirdAbility;
            ability.maxLevel = PantheraConfig.AngryBird_maxLevel;
            ability.unlockLevel = PantheraConfig.AngryBird_unlockLevel;
            ability.requiredAbilities.Add(PantheraConfig.FireBirdAbilityID, 1);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
