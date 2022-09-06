using Panthera.Ability.Destruction;
using Panthera.Ability.Ruse;
using Panthera.Ability.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Components
{
    internal class PantheraAbility
    {

        public enum AbilityType
        {
            passive,
            active,
            primary
        }

        // (int AbilityID 1 - ??, PantheraAbility Object) Represent the list of all Panthera Ability Definitions //
        public static Dictionary<int, PantheraAbility> AbilitytiesDefsList = new Dictionary<int, PantheraAbility>();

        public string name;
        public int abilityID;
        public string desc;
        public AbilityType type;
        public Sprite icon;
        public int maxLevel;
        public int _requiredPoint;
        public int requiredPoint
        {
            get
            {
                if(this.type == AbilityType.primary)
                {
                    int count = GetPrimaryCount();
                    if (count <= 0)
                        return _requiredPoint;
                    else
                        return PantheraConfig.PointsRequiredForMultipleSkillsTree * count;
                }
                else
                {
                    return _requiredPoint;
                }
            }
            set
            {
                this._requiredPoint = value;
            }
        }
        public int unlockLevel;
        public int cooldown;
        public int requiredEnergy = 0;
        public int requiredPower = 0;
        public int requiredFury = 0;
        public int requiredCombo = 0;
        public Dictionary<int, int> requiredSkills = new Dictionary<int, int>(); // <SkillID, SkillLevel //

        public static void RegisterAbilities()
        {
            DestructionAbility.RegisterAbility();
            GuardianAbility.RegisterAbility();
            RuseAbility.RegisterAbility();
            ImprovedLeapAbility.RegisterAbility();
        }

        public static bool CanBeUpgraded(PantheraAbility ability)
        {
            // Check if there are enough Points available //
            if (Character.AvailablePoint < 1) return false;
            // Check if the Ability can be updated //
            if (Preset.SelectedPreset.getAbilityLevel(ability.abilityID) >= ability.maxLevel) return false;
            // Check the Required Points //
            if (Character.SpentPoint < ability.requiredPoint) return false;
            // Check if the Required Abilities are unlocked //
            foreach(KeyValuePair<int, int> entry in ability.requiredSkills)
            {
                if (Preset.SelectedPreset.getAbilityLevel(entry.Key) < entry.Value)
                    return false;
            }
            return true;
        }

        public static int GetPrimaryCount()
        {
            int count = 0;
            foreach(int key in Preset.SelectedPreset.unlockedAbilitiesList.Keys)
            {
                PantheraAbility ability = AbilitytiesDefsList[key];
                if (ability.type == AbilityType.primary)
                    count = count + 1;
            }
            return count;
        }

    }
}
