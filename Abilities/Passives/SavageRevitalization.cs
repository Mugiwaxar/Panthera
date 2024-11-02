using Panthera.Base;
using System;

namespace Panthera.Abilities.Passives
{
    public class SavageRevitalization : PantheraAbility
    {

        public SavageRevitalization()
        {
            base.name = Utils.PantheraTokens.Get("ability_SavageRevitalizationName");
            base.abilityID = PantheraConfig.SavageRevitalization_AbilityID;
            base.type = AbilityType.passive;
            base.icon = PantheraAssets.SavageRevitalizationAbility;
            base.maxLevel = PantheraConfig.SavageRevitalization_maxLevel;
            base.cooldown = 0;
            base.requiredAbility = PantheraConfig.WardensVitality_AbilityID;
            base.desc1 = String.Format(Utils.PantheraTokens.Get("ability_SavageRevitalizationDesc"), PantheraConfig.SavageRevitalization_addedStack, PantheraConfig.SavageRevitalization_buffTime);
            base.desc2 = String.Format(Utils.PantheraTokens.Get("ability_SavageRevitalizationMasteryDesc"), Panthera.ProfileComponent.getMastery() / 2, PantheraConfig.SavageRevitalization_MasteryBuffTime);
            base.hasMastery = true;
        }

        public override void updateDesc()
        {
            base.desc2 = String.Format(Utils.PantheraTokens.Get("ability_SavageRevitalizationMasteryDesc"), Panthera.ProfileComponent.getMastery() / 2, PantheraConfig.SavageRevitalization_MasteryBuffTime);
        }

    }
}
