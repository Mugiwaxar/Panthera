using Panthera.Base;

namespace Panthera.Abilities.Passives
{
    public class WindWalker : PantheraAbility
    {

        public WindWalker()
        {
            base.name = Utils.PantheraTokens.Get("ability_WindWalkerName");
            base.abilityID = PantheraConfig.WindWalker_AbilityID;
            base.type = AbilityType.passive;
            base.icon = PantheraAssets.WindWalkerAbility;
            base.maxLevel = PantheraConfig.WindWalker_maxLevel;
            base.cooldown = 0;
            base.requiredAbility = PantheraConfig.FelineSkills_AbilityID;
            base.desc1 = Utils.PantheraTokens.Get("ability_WindWalkerDesc");
            base.desc2 = null;
        }

    }
}
