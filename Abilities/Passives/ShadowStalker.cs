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
    public class ShadowStalker : PantheraAbility
    {

        public ShadowStalker()
        {
            base.name = Utils.PantheraTokens.Get("ability_ShadowStalkerName");
            base.abilityID = PantheraConfig.ShadowStalker_AbilityID;
            base.type = AbilityType.passive;
            base.icon = PantheraAssets.ShadowStalkerAbility;
            base.maxLevel = PantheraConfig.ShadowStalker_maxLevel;
            base.cooldown = 0;
            base.requiredAbility = PantheraConfig.SwiftMoves_AbilityID;
            base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_ShadowStalkerDesc"), PantheraConfig.ShadowStalker_duration1);
            base.desc2 = null;
        }

        public override void updateDesc()
        {
            int level = Panthera.ProfileComponent.getAbilityLevel(base.abilityID);
            if (level <= 1)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_ShadowStalkerDesc"), PantheraConfig.ShadowStalker_duration1);
            else if (level == 2)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_ShadowStalkerDesc"), PantheraConfig.ShadowStalker_duration2);
        }

    }
}
