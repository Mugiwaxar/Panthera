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
    public class HeatWave : PantheraAbility
    {

        public HeatWave()
        {
            base.name = Utils.PantheraTokens.Get("ability_HeatWaveName");
            base.abilityID = PantheraConfig.HeatWave_AbilityID;
            base.type = AbilityType.passive;
            base.icon = PantheraAssets.HeatWaveAbility;
            base.maxLevel = PantheraConfig.HeatWave_maxLevel;
            base.cooldown = 0;
            base.requiredAbility = PantheraConfig.InfernalSwipe_AbilityID;
            base.desc1 = Utils.PantheraTokens.Get("ability_HeatWaveDesc");
            base.desc2 = null;
        }

    }
}
