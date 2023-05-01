using Panthera.Base;
using Panthera.Components;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Ability.Utility
{
    internal class SaveeMyFriendAbility
    {

        public static void RegisterAbility()
        {
            PantheraAbility ability = new PantheraAbility();
            ability.abilityID = PantheraConfig.SaveMyFriendAbilityID;
            ability.name = "SAVE_MY_FRIEND_ABILITY_NAME";
            ability.desc = "SAVE_MY_FRIEND_ABILITY_DESC";
            ability.type = PantheraAbility.AbilityType.hybrid;
            ability.icon = Assets.SaveMyFriendAbility;
            ability.unlockLevel = PantheraConfig.SaveMyFriend_unlockLevel;
            ability.maxLevel = PantheraConfig.SaveMyFriend_maxLevel;
            ability.cooldown = PantheraConfig.SaveMyFriend_cooldown;
            ability.requiredAbilities.Add(PantheraConfig.LeapAbilityID, 3);
            PantheraAbility.AbilitytiesDefsList.Add(ability.abilityID, ability);
        }

    }
}
