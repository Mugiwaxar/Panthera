using Panthera.Combos;
using Panthera.MachineScripts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using static Panthera.GUI.KeysBinder;

namespace Panthera.BodyComponents
{
    public class PantheraComboComponent : NetworkBehaviour
    {

        // (int Skill ID, ComboSkill comboSkill) Represent all Skills of the actual Combo //
        public List<ComboSkill> actualCombosList = new List<ComboSkill>();

        public PantheraObj ptraObj;

        public bool machinesIddle;
        public float comboTimer;
        public float comboFailTimerFrame;
        public float comboMaxTime = PantheraConfig.Combos_maxTime;
        public ComboSkill lastFailedSkill;

        public void FixedUpdate()
        {

            // Check the Authority //
            if (ptraObj.HasAuthority() == false) return;

            // Start the Combo Timer if the Skill Machines are iddle //
            if (this.machinesIddle == false && this.ptraObj.skillsMachine1.currentScript == null && this.ptraObj.skillsMachine2.currentScript == null)
            {
                this.machinesIddle = true;
                this.comboTimer = Time.time;
            }
            else if (this.machinesIddle == true && (this.ptraObj.skillsMachine1.currentScript != null || this.ptraObj.skillsMachine2.currentScript != null))
            {
                this.machinesIddle = false;
            }

            // Stop the Combo if time passed //
            if (Time.time - this.comboTimer > this.comboMaxTime && this.machinesIddle == true)
            {
                this.actualCombosList.Clear();
                this.comboMaxTime = PantheraConfig.Combos_maxTime;
            }

            // Check the last failed Skill //
            if (this.lastFailedSkill != null && Time.time - this.comboFailTimerFrame > PantheraConfig.Combos_failTimeFrame)
                this.lastFailedSkill = null;

        }

        public void TryLaunchSkill(KeysEnum keys)
        {

            // Create the New Combo Boolean //
            bool newCombo = false;

            // Try to find a Skill //
            ComboSkill comboSkill = this.GetSkill(this.actualCombosList, keys);

            // If no skill found, try with a new Combo //
            if (comboSkill == null)
            {
                newCombo = true;
                comboSkill = this.GetSkill(new List<ComboSkill>(), keys);
            }

            // Check the Skill //
            if (comboSkill == null || this.ptraObj.IsSkillUnlocked(comboSkill.skill.skillID) == false)
                return;

            // Get the Skill //
            MachineScript skill = comboSkill.skill;

            // Check if the Skill can be processed by the Machine //
            if (Machines.PantheraMachine.CanBeProcessed(this.ptraObj, skill) == false)
                return;

            // Check if the Skill can be Used //
            if (skill.CanBeUsed(this.ptraObj) == false)
            {
                this.comboFailTimerFrame = Time.time;
                this.lastFailedSkill = comboSkill;
            }
            else
            {
                // Execute the Skill //
                if (skill.machineToUse == 1)
                    this.ptraObj.skillsMachine1.TryScript((MachineScript)skill.Clone());
                else if (skill.machineToUse == 2)
                    this.ptraObj.skillsMachine2.TryScript((MachineScript)skill.Clone());
                // Stop the Stealth //
                if (skill.removeStealth == true)
                    Skills.Passives.Stealth.DidDamageUnstealth(this.ptraObj);
                // Check if new Combo //
                if (newCombo == true)
                    this.actualCombosList.Clear();
                // Add the Skill to the Actual Combos List //
                this.actualCombosList.Add(comboSkill);
                // Set the ComboMaxTime //
                this.comboMaxTime = skill.comboMaxTime;
                // Set Machines running //
                this.machinesIddle = false;
            }


        }

        private ComboSkill GetSkill(List<ComboSkill> actualCombosList, KeysEnum keys)
        {
            // Create the filtered List //
            Dictionary<int, PantheraCombo> filteredCombosList = this.ptraObj.CharacterCombos.CombosList;

            // Get the actual Combo number //
            int comboNumber = actualCombosList.Count;

            // Check the ComboNumber and filter the List //
            if (comboNumber > 0)
                filteredCombosList = this.FilterCompatibleCombos(filteredCombosList, actualCombosList);

            // Try to get the Skill with the Direction //
            // Mask to get the 4 direction keys
            var modifierMask = KeysEnum.Forward | KeysEnum.Backward | KeysEnum.Left | KeysEnum.Right | KeysEnum.SpellsMode;
            var modifiers = keys & modifierMask;

            // reverse mask to remove modifier keys
            keys &= ~modifierMask;

            return this.GetNextSkill(filteredCombosList, keys, modifiers, comboNumber);
        }

        private ComboSkill GetNextSkill(Dictionary<int, PantheraCombo> filteredCombosList, KeysEnum keys, KeysEnum modifiers, int comboNumber)
        {
            // Itinerate the Combos List //
            foreach (var pair in filteredCombosList)
            {
                // Check if the Combo is locked //
                if (this.ptraObj.IsComboUnlocked(pair.Value.comboID) == false)
                    continue;

                // Check if the Combo is activated //
                if (this.ptraObj.activatedComboList[pair.Key] == false)
                    continue;

                // Get Combo skill //
                var comboSkill = pair.Value.comboSkillsList.ElementAtOrDefault(comboNumber);
                if (comboSkill is null)
                    continue;

                // Check if the Direction Key is the same //
                if (comboSkill.modifier != KeysEnum.None && !modifiers.HasFlag(comboSkill.modifier))
                    continue;

                //filter down to only spells/non-spells
                if ((comboSkill.modifier == KeysEnum.SpellsMode) != modifiers.HasFlag(KeysEnum.SpellsMode))
                    continue;

                // Check the Keys //
                if (keys.HasFlag(comboSkill.keyA) && (comboSkill.keyB == KeysEnum.None || keys.HasFlag(comboSkill.keyB)))
                    return comboSkill;
            }

            // Return null //
            return null;
        }

        public Dictionary<int, PantheraCombo> FilterCompatibleCombos(Dictionary<int, PantheraCombo> allCombosList, List<ComboSkill> actualList)
        {
            // Create the new List //
            Dictionary<int, PantheraCombo> tmpCombosList = new Dictionary<int, PantheraCombo>();
            // Check all Combos //
            foreach (KeyValuePair<int, PantheraCombo> pair in allCombosList)
            {
                // Check the Combo //
                if (CompareCombosList(actualList, pair.Value.comboSkillsList) == true)
                    tmpCombosList.Add(pair.Key, pair.Value);
            }
            // Return the List //
            return tmpCombosList;
        }

        public bool CompareCombosList(List<ComboSkill> list1, List<ComboSkill> list2)
        {
            // Check the Lists size //
            if (list1.Count > list2.Count)
                return false;

            // Itinerate the List 1 //
            for (int i = 0; i < list1.Count; i++ )
            {
                // Check the Skill //
                if (list1[i].skill.skillID != list2[i].skill.skillID)
                    return false;
            }

            return true;

        }

    }
}
