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
    public class ImprovedShield : PantheraAbility
    {

        public ImprovedShield()
        {
            base.name = Utils.PantheraTokens.Get("ability_ImprovedShieldName");
            base.abilityID = PantheraConfig.ImprovedShield_AbilityID;
            base.type = AbilityType.passive;
            base.icon = Assets.ImprovedShieldAbility;
            base.maxLevel = PantheraConfig.ImprovedShield_maxLevel;
            base.cooldown = 0;
            base.requiredAbility = PantheraConfig.FrontShield_AbilityID;
            base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_ImprovedShieldDesc"), PantheraConfig.ImprovedShield_addedPercent1 * 100, PantheraConfig.ImprovedShield_RemovedRechargeDelay1);
            base.desc2 = null;
        }

        public override void updateDesc()
        {
            if (base.currentLevel <= 1)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_ImprovedShieldDesc"), PantheraConfig.ImprovedShield_addedPercent1 * 100, PantheraConfig.ImprovedShield_RemovedRechargeDelay1);
            else if (base.currentLevel == 2)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_ImprovedShieldDesc"), PantheraConfig.ImprovedShield_addedPercent2 * 100, PantheraConfig.ImprovedShield_RemovedRechargeDelay2);
            else if (base.currentLevel == 3)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_ImprovedShieldDesc"), PantheraConfig.ImprovedShield_addedPercent3 * 100, PantheraConfig.ImprovedShield_RemovedRechargeDelay3);
        }

    }
}
