using Panthera.Base;

namespace Panthera.Abilities.Actives
{
    public class RoarOfResilience : PantheraAbility
    {

        public RoarOfResilience()
        {
            name = Utils.PantheraTokens.Get("ability_RoarOfResilienceName");
            abilityID = PantheraConfig.RoarOfResilience_AbilityID;
            type = AbilityType.passive;
            icon = PantheraAssets.RoarOfResilienceAbility;
            maxLevel = PantheraConfig.RoarOfResilience_maxLevel;
            cooldown = 0;
            requiredAbility = PantheraConfig.Guardian_AbilityID;
            desc1 = Utils.PantheraTokens.Get("ability_RoarOfResilienceDesc");
        }

    }
}
