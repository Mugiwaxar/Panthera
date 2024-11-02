using Panthera.Base;

namespace Panthera.Abilities.Passives
{
    public class SuperiorFlair : PantheraAbility
    {

        public SuperiorFlair()
        {
            base.name = Utils.PantheraTokens.Get("ability_SuperiorFlairName");
            base.abilityID = PantheraConfig.SuperiorFlair_AbilityID;
            base.type = AbilityType.passive;
            base.icon = PantheraAssets.SuperiorFlairAbility;
            base.maxLevel = PantheraConfig.SuperiorFlair_maxLevel;
            base.cooldown = 0;
            base.requiredAbility = PantheraConfig.Detection_AbilityID;
            base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_SuperiorFlairDesc"), PantheraConfig.SuperiorFlair_percent1 * 100);
            base.desc2 = null;
        }

        public override void updateDesc()
        {
            int level = Panthera.ProfileComponent.GetAbilityLevel(base.abilityID);
            if (level <= 1)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_SuperiorFlairDesc"), PantheraConfig.SuperiorFlair_percent1);
            else if (level == 2)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_SuperiorFlairDesc"), PantheraConfig.SuperiorFlair_percent2);
            else if (level == 3)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_SuperiorFlairDesc"), PantheraConfig.SuperiorFlair_percent3);
            else if (level == 4)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_SuperiorFlairDesc"), PantheraConfig.SuperiorFlair_percent4);
            else if (level == 5)
                base.desc1 = string.Format(Utils.PantheraTokens.Get("ability_SuperiorFlairDesc"), PantheraConfig.SuperiorFlair_percent5);
        }

    }
}
