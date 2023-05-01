using Panthera;
using Panthera.Ability.Destruction;
using Panthera.Ability.Guardian;
using Panthera.Ability.Ruse;
using Panthera.Ability.Utility;
using Panthera.Base;
using Panthera.Components;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Base
{
    public class PantheraAbility
    {

        public enum AbilityType
        {
            passive,
            active,
            primary,
            hybrid
        }

        // (int AbilityID 1 - ??, PantheraAbility Object) Represent the list of all Panthera Ability Definitions //
        public static Dictionary<int, PantheraAbility> AbilitytiesDefsList = new Dictionary<int, PantheraAbility>();

        public string name;
        public int abilityID;
        public string desc;
        public AbilityType type;
        public Sprite icon;
        public int maxLevel;
        public int _unlockLevel;
        public int unlockLevel
        {
            get
            {
                if (type == AbilityType.primary)
                {
                    int count = GetPrimaryCount();
                    if (count <= 0)
                        return _unlockLevel;
                    else
                        return PantheraConfig.LevelsRequiredForMultipleSkillsTree * count;
                }
                else
                {
                    return _unlockLevel;
                }
            }
            set
            {
                _unlockLevel = value;
            }
        }
        public float cooldown;
        public int requiredEnergy = 0;
        public int requiredPower = 0;
        public int requiredFury = 0;
        public int requiredCombo = 0;
        public Dictionary<int, int> requiredAbilities = new Dictionary<int, int>(); // <AbilityID, AbilityLevel //

        public static void RegisterAbilities()
        {
            DestructionAbility.RegisterAbility();
            GuardianAbility.RegisterAbility();
            RuseAbility.RegisterAbility();
            LeapAbility.RegisterAbility();
            MyghtyRoarAbility.RegisterAbility();
            ImprovedClawsStormAbility.RegisterAbility();
            HealStormAbility.RegisterAbility();
            TornadoAbility.RegisterAbility();
            SharpenedFangsAbility.RegisterAbility();
            PowerfullJawsAbility.RegisterAbility();
            PredatorsDrinkAbility.RegisterAbility();
            ProwlAbility.RegisterAbility();
            ShadowsMasterAbility.RegisterAbility();
            SilentPredatorAbility.RegisterAbility();
            PrimalStalkerAbility.RegisterAbility();
            ShieldFocusAbility.RegisterAbility();
            ResidualEnergyAbility.RegisterAbility();
            DefensiveHasteAbility.RegisterAbility();
            DashAbility.RegisterAbility();
            ShieldBashAbility.RegisterAbility();
            KineticAbsorbtionAbility.RegisterAbility();
            ZoneHealAbility.RegisterAbility();
            WindWalkerAbility.RegisterAbility();
            PerspicacityAbility.RegisterAbility();
            HealingCleaveAbility.RegisterAbility();
            TheRipperAbility.RegisterAbility();
            InstinctiveResistanceAbility.RegisterAbility();
            BloodyRageAbility.RegisterAbility();
            GodOfReapersAbility.RegisterAbility();
            SaveeMyFriendAbility.RegisterAbility();
            ShieldOfPowerAbility.RegisterAbility();
            GhostRipAbility.RegisterAbility();
            EchoAbility.RegisterAbility();
            PiercingWavesAbility.RegisterAbility();
            BurningSpiritAbility.RegisterAbility();
            HellCatAbility.RegisterAbility();
            SlashAbility.RegisterAbility();
            CircularSawAbility.RegisterAbility();
            IgnitionAbility.RegisterAbility();
            SoulsShelterAbility.RegisterAbility();
            TheSlashPerAbility.RegisterAbility();
            HighTemperatureAbility.RegisterAbility();
            SacredFlamesAbility.RegisterAbility();
            FireBirdAbility.RegisterAbility();
            AngryBirdAbility.RegisterAbility();
            PassivePowerAbility.RegisterAbility();
            ReviveAbility.RegisterAbility();
            DetectionAbility.RegisterAbility();
            PrescienceAbility.RegisterAbility();
            ConcentrationAbility.RegisterAbility();
            PrecisionAbility.RegisterAbility();
            DeterminationAbility.RegisterAbility();
            RegenerationAbility.RegisterAbility();
            StrongBarrierAbility.RegisterAbility();
        }

        public static bool CanBeUpgraded(PantheraAbility ability)
        {
            // Check if there are enough Points available //
            if (Character.AvailablePoint < 1) return false;
            // Check if the Ability can be updated //
            if (Preset.SelectedPreset.getAbilityLevel(ability.abilityID) >= ability.maxLevel) return false;
            // Check the Unlock Level //
            if (Character.CharacterLevel < ability.unlockLevel) return false;
            // Check if the Required Abilities are unlocked //
            foreach (KeyValuePair<int, int> entry in ability.requiredAbilities)
            {
                if (Preset.SelectedPreset.getAbilityLevel(entry.Key) < entry.Value)
                    return false;
            }
            return true;
        }

        public static int GetPrimaryCount()
        {
            int count = 0;
            foreach (int key in Preset.SelectedPreset.unlockedAbilitiesList.Keys)
            {
                PantheraAbility ability = AbilitytiesDefsList[key];
                if (ability.type == AbilityType.primary)
                    count = count + 1;
            }
            return count;
        }

    }
}
