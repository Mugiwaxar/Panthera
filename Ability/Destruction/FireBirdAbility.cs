using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Destruction
{
    internal class FireBirdAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.FireBirdAbilityID;
            ability.name = "FIRE_BIRD_SKILL_NAME";
            ability.desc = "FIRE_BIRD_SKILL_DESC";
            ability.type = PantheraAbility.AbilityType.active;
            ability.icon = Assets.FireBird;
            ability.maxLevel = PantheraConfig.FireBird_maxLevel;
            ability.unlockLevel = PantheraConfig.FireBird_unlockLevel;
            ability.requiredFury = PantheraConfig.FireBird_furyRequired;
            ability.cooldown = PantheraConfig.FireBird_Cooldown;
            ability.requiredAbilities.Add(PantheraConfig.IgnitionAbilityID, 3);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
