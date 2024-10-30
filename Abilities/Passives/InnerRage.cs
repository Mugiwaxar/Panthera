using Panthera;
using Panthera.Abilities;
using Panthera.Abilities.Passives;
using Panthera.Abilities.Primaries;
using Panthera.Base;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Abilities.Passives
{
    public class InnerRage : PantheraAbility
    {

        public InnerRage()
        {
            base.name = Utils.PantheraTokens.Get("ability_InnerRageName");
            base.abilityID = PantheraConfig.InnerRage_AbilityID;
            base.type = AbilityType.passive;
            base.icon = PantheraAssets.InnerRageAbility;
            base.maxLevel = PantheraConfig.InnerRage_maxLevel;
            base.cooldown = 0;
            base.requiredAbility = PantheraConfig.EternalFury_AbilityID;
            base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_InnerRageDesc"), PantheraConfig.InnerRage_addedFuryPercent1 * 100);
            base.desc2 = String.Format(Utils.PantheraTokens.Get("ability_InnerRageMasteryDesc"), Panthera.ProfileComponent.getMastery(), PantheraConfig.InnerRage_enrageTime, PantheraConfig.InnerRage_enrageTimeFuryMode);
            base.hasMastery = true;
        }

        public override void updateDesc()
        {
            int level = Panthera.ProfileComponent.getAbilityLevel(base.abilityID);
            if (level <= 1)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_InnerRageDesc"), PantheraConfig.InnerRage_addedFuryPercent1 * 100);
            else if (level == 2)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_InnerRageDesc"), PantheraConfig.InnerRage_addedFuryPercent2 * 100);
            else if (level == 3)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_InnerRageDesc"), PantheraConfig.InnerRage_addedFuryPercent3 * 100);
            else if (level == 4)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_InnerRageDesc"), PantheraConfig.InnerRage_addedFuryPercent4 * 100);
            base.desc2 = String.Format(Utils.PantheraTokens.Get("ability_InnerRageMasteryDesc"), Panthera.ProfileComponent.getMastery(), PantheraConfig.InnerRage_enrageTime, PantheraConfig.InnerRage_enrageTimeFuryMode);
        }

    }
}
