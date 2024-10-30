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
    public class UntamedSpirit : PantheraAbility
    {

        public UntamedSpirit()
        {
            base.name = Utils.PantheraTokens.Get("ability_UntamedSpiritName");
            base.abilityID = PantheraConfig.UntamedSpirit_AbilityID;
            base.type = AbilityType.special;
            base.icon = PantheraAssets.UntamedSpiritAbility;
            base.maxLevel = 1;
            base.cooldown = 0;
            base.desc1 = String.Format(Utils.PantheraTokens.Get("ability_UntamedSpiritDesc"), PantheraConfig.UntamedSpitit_unlockLevel);
            base.desc2 = null;
        }

    }
}
