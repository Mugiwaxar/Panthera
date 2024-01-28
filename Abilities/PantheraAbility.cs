using Panthera;
using Panthera.Abilities;
using Panthera.Base;
using Panthera.Components;
using Panthera.NetworkMessages;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Abilities
{
    public class PantheraAbility
    {

        public enum AbilityType
        {
            primary,
            passive,
            active
        }

        public string name;
        public int abilityID;
        public AbilityType type;
        public string desc1;
        public string desc2;
        public Sprite icon;
        public int maxLevel;
        public float cooldown;
        public int requiredAbility;
        public GameObject associatedGUIObj;

        public int currentLevel
        {
            get
            {
                return Panthera.PantheraCharacter.characterAbilities.getAbilityLevel(this.abilityID);
            }
        }
        public bool unlocked
        {
            get
            {
                if (Panthera.PantheraCharacter.characterAbilities.unlockedAbilitiesList.ContainsKey(this.abilityID))
                    return true;
                return false;
            }
        }
        public bool unlockable
        {
            get
            {
            if (Panthera.PantheraCharacter.characterAbilities.getAbilityLevel(this.requiredAbility) >= Panthera.PantheraCharacter.characterAbilities.getAbilityByID(this.requiredAbility).maxLevel)
                return true;
            return false;
            }
        }

        public bool canBeUpgraded()
        {
            // Check if there are enough Points available //
            if (Panthera.PantheraCharacter.skillPointsLeft < 1) return false;
            // Check if the Ability can be upgraded //
            if (Panthera.PantheraCharacter.characterAbilities.getAbilityLevel(this.abilityID) >= this.maxLevel) return false;
            // Check if the Required Abilities are unlocked //
            if (this.unlockable == false)
                return false;
            return true;
        }

        public void upgrade()
        {
            if (this.canBeUpgraded() == false)
                return;
            if (Panthera.PantheraCharacter.characterAbilities.unlockedAbilitiesList.ContainsKey(this.abilityID))
                Panthera.PantheraCharacter.characterAbilities.unlockedAbilitiesList[this.abilityID]++;
            else
                Panthera.PantheraCharacter.characterAbilities.unlockedAbilitiesList[this.abilityID] = 1;
            Utils.PantheraSaveSystem.Save();
        }

        public virtual void updateDesc()
        {

        }

    }
}
