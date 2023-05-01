using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Guardian
{
    internal class KineticAbsorbtionAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.KineticAbsorbtionAbilityID;
            ability.name = "KINEMATIC_ABSORBTION_ABILITY_NAME";
            ability.desc = "KINEMATIC_ABSORBTION_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.passive;
            ability.icon = Assets.KineticAbsorbtionAbility;
            ability.maxLevel = PantheraConfig.KineticAbsorbtion_maxLevel;
            ability.unlockLevel = PantheraConfig.KineticAbsorbtion_unlockLevel;
            ability.requiredAbilities.Add(PantheraConfig.ShieldBashAbilityID, 1);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
