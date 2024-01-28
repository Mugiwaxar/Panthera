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
    public class SwiftMoves : PantheraAbility
    {

        public SwiftMoves()
        {
            base.name = Utils.PantheraTokens.Get("ability_SwiftMovesName");
            base.abilityID = PantheraConfig.SwiftMoves_AbilityID;
            base.type = AbilityType.passive;
            base.icon = Assets.SwiftMovesAbility;
            base.maxLevel = PantheraConfig.SwiftMoves_maxLevel;
            base.cooldown = 0;
            base.requiredAbility = PantheraConfig.Prowl_AbilityID;
            base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_SwiftMovesDesc"), PantheraConfig.SwiftMoves_percent1 * 100);
            base.desc2 = null;
        }

        public override void updateDesc()
        {
            if (base.currentLevel <= 1)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_SwiftMovesDesc"), PantheraConfig.SwiftMoves_percent1 * 100);
            else if (base.currentLevel == 2)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_SwiftMovesDesc"), PantheraConfig.SwiftMoves_percent2 * 100);
            else if (base.currentLevel == 3)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_SwiftMovesDesc"), PantheraConfig.SwiftMoves_percent3 * 100);
        }

    }
}
