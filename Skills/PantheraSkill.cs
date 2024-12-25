using HarmonyLib;
using Panthera;
using Panthera.Base;
using Panthera.Components;
using Panthera.Machines;
using Panthera.MachineScripts;
using Panthera.OldSkills;
using Panthera.Passives;
using Panthera.Skills;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Skills
{
    public class PantheraSkill
    {

        public enum SkillType
        {
            passive,
            active,
            hybrid
        }

        // (int SkillID 1 - ??, PantheraSkill Object) Represent the list of all Panthera Skill Definitions //
        public static Dictionary<int, PantheraSkill> SkillDefsList = new Dictionary<int, PantheraSkill>();

        public int skillID;
        public string name;
        public string desc;
        public Sprite icon;
        public GameObject iconPrefab;
        public SkillType type;
        public Type associatedSkill;
        public ScriptPriority priority = ScriptPriority.NoPriority;
        public ScriptPriority interruptPower = ScriptPriority.NoPriority;
        public float cooldown = 0;
        public int requiredEnergy = 0;
        public int requiredPower = 0;
        public int requiredFury = 0;
        public int requiredCombo = 0;
        public int skillMachine = 1;

        public static PantheraSkill GetSkillDef(int skillID)
        {
            if (SkillDefsList.ContainsKey(skillID)) return SkillDefsList[skillID];
            return null;
        }

        public PantheraSkill clone()
        {
            return (PantheraSkill)MemberwiseClone();
        }
    }
}
