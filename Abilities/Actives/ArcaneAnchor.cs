using Panthera;
using Panthera.Abilities;
using Panthera.Abilities.Actives;
using Panthera.Abilities.Primaries;
using Panthera.Base;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Abilities.Actives
{
    public class ArcaneAnchor : PantheraAbility
    {

        public ArcaneAnchor()
        {
            name = Utils.PantheraTokens.Get("ability_ArcaneAnchorName");
            abilityID = PantheraConfig.ArcaneAnchor_AbilityID;
            type = AbilityType.active;
            icon = Assets.ArcaneAnchorSkill;
            maxLevel = PantheraConfig.ArcaneAnchor_maxLevel;
            cooldown = PantheraConfig.ArcaneAnchor_cooldown;
            requiredAbility = PantheraConfig.ShieldBash_AbilityID;
            desc1 = String.Format(Utils.PantheraTokens.Get("ability_ArcaneAnchorDesc"));
            desc2 = null;
        }

    }
}
