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
    public class PortalSurge : PantheraAbility
    {

        public PortalSurge()
        {
            name = Utils.PantheraTokens.Get("ability_PortalSurgeName");
            abilityID = PantheraConfig.PortalSurge_AbilityID;
            type = AbilityType.special;
            icon = PantheraAssets.PortalSurgeSkill;
            maxLevel = 1;
            cooldown = PantheraConfig.PortalSurge_cooldown;
            desc1 = String.Format(Utils.PantheraTokens.Get("ability_PortalSurgeDesc"), PantheraConfig.PortalSurge_unlockLevel, PantheraConfig.PortalSurge_requiredIngameLevel, PantheraConfig.PortalSurge_lunarCost);
            desc2 = null;
        }

    }
}
