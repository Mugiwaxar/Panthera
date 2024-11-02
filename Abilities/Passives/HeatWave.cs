using Panthera.Base;

namespace Panthera.Abilities.Passives
{
    public class HeatWave : PantheraAbility
    {

        public HeatWave()
        {
            base.name = Utils.PantheraTokens.Get("ability_HeatWaveName");
            base.abilityID = PantheraConfig.HeatWave_AbilityID;
            base.type = AbilityType.passive;
            base.icon = PantheraAssets.HeatWaveAbility;
            base.maxLevel = PantheraConfig.HeatWave_maxLevel;
            base.cooldown = 0;
            base.requiredAbility = PantheraConfig.InfernalSwipe_AbilityID;
            base.desc1 = Utils.PantheraTokens.Get("ability_HeatWaveDesc");
            base.desc2 = null;
        }

    }
}
