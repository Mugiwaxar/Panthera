using Panthera.Base;

namespace Panthera.Abilities.Actives
{
    public class ClawsSharpening : PantheraAbility
    {

        public ClawsSharpening()
        {
            name = Utils.PantheraTokens.Get("ability_ClawsSharpeningName");
            abilityID = PantheraConfig.ClawsSharpening_AbilityID;
            type = AbilityType.passive;
            icon = PantheraAssets.ClawsSharpeningAbility;
            maxLevel = PantheraConfig.ClawsSharpening_maxLevel;
            cooldown = 0;
            requiredAbility = PantheraConfig.Fury_AbilityID;
            desc1 = Utils.PantheraTokens.Get("ability_ClawsSharpeningDesc");
        }

    }
}
