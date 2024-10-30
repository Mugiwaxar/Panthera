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
    public class KineticResorption : PantheraAbility
    {

        public KineticResorption()
        {
            base.name = Utils.PantheraTokens.Get("ability_KineticResorptionName");
            base.abilityID = PantheraConfig.KineticResorption_AbilityID;
            base.type = AbilityType.passive;
            base.icon = PantheraAssets.KineticResorptionAbility;
            base.maxLevel = PantheraConfig.KineticResorption_maxLevel;
            base.cooldown = 0;
            base.requiredAbility = PantheraConfig.ShieldBash_AbilityID;
            base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_KineticResorptionDesc"), PantheraConfig.KineticResorption_regenPercent1 * 100);
            base.desc2 = null;
        }

        public override void updateDesc()
        {
            int level = Panthera.ProfileComponent.getAbilityLevel(base.abilityID);
            if (level <= 1)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_KineticResorptionDesc"), PantheraConfig.KineticResorption_regenPercent1 * 100);
            else if (level == 2)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_KineticResorptionDesc"), PantheraConfig.KineticResorption_regenPercent2 * 100);
            else if (level == 3)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_KineticResorptionDesc"), PantheraConfig.KineticResorption_regenPercent3 * 100);
        }

    }
}
