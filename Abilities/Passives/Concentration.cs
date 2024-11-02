using Panthera.Base;

namespace Panthera.Abilities.Passives
{
    public class Concentration : PantheraAbility
    {

        public Concentration()
        {
            base.name = Utils.PantheraTokens.Get("ability_ConcentrationName");
            base.abilityID = PantheraConfig.Concentration_AbilityID;
            base.type = AbilityType.passive;
            base.icon = PantheraAssets.ConcentrationAbility;
            base.maxLevel = PantheraConfig.Concentration_maxLevel;
            base.cooldown = 0;
            base.requiredAbility = PantheraConfig.SixthSense_AbilityID;
            base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_ConcentrationDesc"));
            base.desc2 = null;
        }

    }
}
