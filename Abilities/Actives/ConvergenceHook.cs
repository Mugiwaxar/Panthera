using Panthera.Base;
using System;

namespace Panthera.Abilities.Actives
{
    public class ConvergenceHook : PantheraAbility
    {

        public ConvergenceHook()
        {
            name = Utils.PantheraTokens.Get("ability_ConvergenceHookName");
            abilityID = PantheraConfig.ConvergenceHook_AbilityID;
            type = AbilityType.active;
            icon = PantheraAssets.ConvergenceHookSkill;
            maxLevel = PantheraConfig.ConvergenceHook_maxLevel;
            cooldown = PantheraConfig.ConvergenceHook_cooldown;
            requiredAbility = PantheraConfig.AirSlash_AbilityID;
            desc1 = String.Format(Utils.PantheraTokens.Get("ability_ConvergenceHookDesc"));
            desc2 = null;
        }

    }
}
