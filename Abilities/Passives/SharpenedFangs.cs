using Panthera.Base;

namespace Panthera.Abilities.Passives
{
    public class SharpenedFangs : PantheraAbility
    {

        public SharpenedFangs()
        {
            base.name = Utils.PantheraTokens.Get("ability_SharpenedFangsName");
            base.abilityID = PantheraConfig.SharpenedFangs_AbilityID;
            base.type = AbilityType.passive;
            base.icon = PantheraAssets.SharpenedFangsAbility;
            base.maxLevel = PantheraConfig.SharpenedFangs_maxLevel;
            base.cooldown = 0;
            base.requiredAbility = PantheraConfig.FelineSkills_AbilityID;
            base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_SharpenedFangsDesc"), PantheraConfig.SharpenedFangs_damagePercent1 * 100);
            base.desc2 = null;
        }

        public override void updateDesc()
        {
            int level = Panthera.ProfileComponent.GetAbilityLevel(base.abilityID);
            if (level <= 1)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_SharpenedFangsDesc"), PantheraConfig.SharpenedFangs_damagePercent1 * 100);
            else if (level == 2)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_SharpenedFangsDesc"), PantheraConfig.SharpenedFangs_damagePercent2 * 100);
            else if (level == 3)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_SharpenedFangsDesc"), PantheraConfig.SharpenedFangs_damagePercent3 * 100);
        }

    }
}
