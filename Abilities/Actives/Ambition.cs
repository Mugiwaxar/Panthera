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
    public class Ambition : PantheraAbility
    {

        public Ambition()
        {
            name = Utils.PantheraTokens.Get("ability_AmbitionName");
            abilityID = PantheraConfig.Ambition_AbilityID;
            type = AbilityType.active;
            icon = Assets.AmbitionSkill;
            maxLevel = PantheraConfig.Ambition_maxLevel;
            cooldown = PantheraConfig.Ambition_cooldown;
            requiredAbility = PantheraConfig.GoldenStart_AbilityID;
            desc1 = String.Format(Utils.PantheraTokens.Get("ability_AmbitionDesc1"), PantheraConfig.Ambition_buffDuration);
        }

    }
}
