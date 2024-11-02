using Panthera.Base;

namespace Panthera.Abilities.Passives
{
    public class CryoLeap : PantheraAbility
    {

        public CryoLeap()
        {
            base.name = Utils.PantheraTokens.Get("ability_CryoLeapName");
            base.abilityID = PantheraConfig.CryoLeap_AbilityID;
            base.type = AbilityType.passive;
            base.icon = PantheraAssets.CryoLeapAbility;
            base.maxLevel = PantheraConfig.CryoLeap_maxLevel;
            base.cooldown = 0;
            base.requiredAbility = PantheraConfig.RelentlessStalker_AbilityID;
            base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_CryoLeapDesc"), PantheraConfig.CryoLeap_duration1);
            base.desc2 = null;
        }

        public override void updateDesc()
        {
            int level = Panthera.ProfileComponent.GetAbilityLevel(base.abilityID);
            if (level <= 1)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_CryoLeapDesc"), PantheraConfig.CryoLeap_duration1);
            else if (level == 2)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_CryoLeapDesc"), PantheraConfig.CryoLeap_duration2);
            else if (level == 3)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_CryoLeapDesc"), PantheraConfig.CryoLeap_duration3);
        }

    }
}
