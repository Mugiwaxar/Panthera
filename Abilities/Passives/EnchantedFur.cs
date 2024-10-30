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
    public class EnchantedFur : PantheraAbility
    {

        public EnchantedFur()
        {
            base.name = Utils.PantheraTokens.Get("ability_EnchantedFurName");
            base.abilityID = PantheraConfig.EnchantedFur_AbilityID;
            base.type = AbilityType.passive;
            base.icon = PantheraAssets.EnchantedFurAbility;
            base.maxLevel = PantheraConfig.EnchantedFur_maxLevel;
            base.cooldown = 0;
            base.requiredAbility = PantheraConfig.FelineSkills_AbilityID;
            base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_EnchantedFurDesc"), PantheraConfig.EnchantedFur_percent1 * 100);
            base.desc2 = null;
        }

        public override void updateDesc()
        {
            int level = Panthera.ProfileComponent.getAbilityLevel(base.abilityID);
            if (level <= 1)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_EnchantedFurDesc"), PantheraConfig.EnchantedFur_percent1 * 100);
            else if (level == 2)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_EnchantedFurDesc"), PantheraConfig.EnchantedFur_percent2 * 100);
            else if (level == 3)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_EnchantedFurDesc"), PantheraConfig.EnchantedFur_percent3 * 100);
        }

    }
}
