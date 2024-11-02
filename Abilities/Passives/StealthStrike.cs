using Panthera.Base;

namespace Panthera.Abilities.Actives
{
    public class StealthStrike : PantheraAbility
    {

        public StealthStrike()
        {
            name = Utils.PantheraTokens.Get("ability_StealthStrikeName");
            abilityID = PantheraConfig.StealthStrike_AbilityID;
            type = AbilityType.passive;
            icon = PantheraAssets.StealthStrikeAbility;
            maxLevel = PantheraConfig.StealthStrike_maxLevel;
            cooldown = 0;
            requiredAbility = PantheraConfig.GhostRip_AbilityID;
            desc1 = string.Format(Utils.PantheraTokens.Get("ability_StealthStrikeDesc"));
        }

    }
}
