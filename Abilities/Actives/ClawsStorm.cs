using Panthera.Base;
using System;

namespace Panthera.Abilities.Actives
{
    public class ClawsStorm : PantheraAbility
    {

        public ClawsStorm()
        {
            name = Utils.PantheraTokens.Get("ability_ClawsStormName");
            abilityID = PantheraConfig.ClawsStorm_AbilityID;
            type = AbilityType.active;
            icon = PantheraAssets.ClawStormSkill;
            maxLevel = PantheraConfig.ClawsStorm_maxLevel;
            cooldown = PantheraConfig.ClawsStorm_cooldown;
            requiredAbility = PantheraConfig.ClawsSharpening_AbilityID;
            desc1 = String.Format(Utils.PantheraTokens.Get("ability_ClawsStormDesc"), PantheraConfig.clawsStorm_damageMultiplier * 100, PantheraConfig.ClawsStorm_firedDelay, PantheraConfig.ClawsStorm_continuousConsumedFury);
            desc2 = null;
        }

    }
}
