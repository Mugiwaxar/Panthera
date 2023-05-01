using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Destruction
{
    internal class SlashAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.SlashAbilityID;
            ability.name = "SLASH_SKILL_NAME";
            ability.desc = "SLASH_SKILL_DESC";
            ability.type = PantheraAbility.AbilityType.active;
            ability.icon = Assets.Slash;
            ability.maxLevel = PantheraConfig.Slash_maxLevel;
            ability.unlockLevel = PantheraConfig.Slash_unlockLevel;
            ability.requiredEnergy = PantheraConfig.Slash_energyRequired;
            ability.cooldown = PantheraConfig.Slash_Cooldown;
            ability.requiredAbilities.Add(PantheraConfig.DestructionAbilityID, 1);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
