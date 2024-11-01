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
    public class WardensVitality : PantheraAbility
    {

        public WardensVitality()
        {
            base.name = Utils.PantheraTokens.Get("ability_WardensVitalityName");
            base.abilityID = PantheraConfig.WardensVitality_AbilityID;
            base.type = AbilityType.passive;
            base.icon = PantheraAssets.WardensVitalityAbility;
            base.maxLevel = PantheraConfig.WardensVitality_maxLevel;
            base.cooldown = 0;
            base.requiredAbility = PantheraConfig.Guardian_AbilityID;
            base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_WardensVitalityDesc"), (PantheraConfig.WardensVitality_maxHealthPercent1 - 1) * 100, PantheraConfig.WardensVitality_BlockAdded1);
            base.desc2 = null;
        }

        public override void updateDesc()
        {
            int level = Panthera.ProfileComponent.GetAbilityLevel(base.abilityID);
            if (level <= 1)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_WardensVitalityDesc"), (PantheraConfig.WardensVitality_maxHealthPercent1 - 1) * 100, PantheraConfig.WardensVitality_BlockAdded1);
            else if (level == 2)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_WardensVitalityDesc"), (PantheraConfig.WardensVitality_maxHealthPercent2 - 1) * 100, PantheraConfig.WardensVitality_BlockAdded2);
            else if (level == 3)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_WardensVitalityDesc"), (PantheraConfig.WardensVitality_maxHealthPercent3 - 1) * 100, PantheraConfig.WardensVitality_BlockAdded3);
        }

    }
}
