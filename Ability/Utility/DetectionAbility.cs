using Panthera.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Utility
{
    public class DetectionAbility
    {
        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.DetectionAbilityID;
            ability.name = "DETECTION_SKILL_NAME";
            ability.desc = "DETECTION_SKILL_DESC";
            ability.type = PantheraAbility.AbilityType.active;
            ability.icon = Assets.Detection;
            ability.unlockLevel = PantheraConfig.Detection_unlockLevel;
            ability.maxLevel = PantheraConfig.Detection_maxLevel;
            ability.cooldown = PantheraConfig.Detection_coolDown;
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }
    }
}
