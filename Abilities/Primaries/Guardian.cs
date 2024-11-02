using Panthera.Base;
using System;

namespace Panthera.Abilities.Primaries
{
    public class Guardian : PantheraAbility
    {

        public Guardian()
        {
            base.name = Utils.PantheraTokens.Get("ability_GuardianName");
            base.abilityID = PantheraConfig.Guardian_AbilityID;
            base.type = AbilityType.primary;
            base.icon = PantheraAssets.GuardianSkill;
            base.maxLevel = PantheraConfig.Guardian_maxLevel;
            base.cooldown = PantheraConfig.Guardian_cooldown;
            base.requiredAbility = PantheraConfig.EnchantedFur_AbilityID;
            base.desc1 = String.Format(Utils.PantheraTokens.Get("ability_GuardianDesc"), PantheraConfig.Guardian_increasedArmor * 100, PantheraConfig.Guardian_increasedHealthRegen * 100, (1 - PantheraConfig.Guardian_barrierDecayRatePercent) * 100);
            base.desc2 = String.Format(Utils.PantheraTokens.Get("ability_GuardianMasteryDesc"), (PantheraConfig.Guardian_masteryHealPercent * 100) + Panthera.ProfileComponent.getMastery());
            base.hasMastery = true;
        }

        public override void updateDesc()
        {
            base.desc2 = String.Format(Utils.PantheraTokens.Get("ability_GuardianMasteryDesc"), (PantheraConfig.Guardian_masteryHealPercent * 100) + Panthera.ProfileComponent.getMastery());
        }
    }
}
