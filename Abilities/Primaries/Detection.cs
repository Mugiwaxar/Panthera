using Panthera.Base;
using System;

namespace Panthera.Abilities.Primaries
{
    public class Detection : PantheraAbility
    {

        public Detection()
        {
            name = Utils.PantheraTokens.Get("ability_DetectionName");
            abilityID = PantheraConfig.Detection_AbilityID;
            type = AbilityType.primary;
            icon = PantheraAssets.DetectionSkill;
            maxLevel = PantheraConfig.Detection_maxLevel;
            cooldown = PantheraConfig.Detection_maxTime;
            requiredAbility = PantheraConfig.Predator_AbilityID;
            desc1 = String.Format(Utils.PantheraTokens.Get("ability_DetectionDesc"), PantheraConfig.Detection_maxTime);
            desc2 = null;
        }

    }
}
