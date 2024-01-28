using HarmonyLib;
using Panthera;
using Panthera.Base;
using Panthera.Components;
using Panthera.Machines;
using Panthera.MachineScripts;
using Panthera.Passives;
using Panthera.OldSkills;
using Panthera.Skills;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Base
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

        public static void RegisterSkills()
        {
            //SkillDefsList.Clear();
            //OldSkills.Rip.Create();
            //OldSkills.AirCleave.Create();
            //OldSkills.Leap.Create();
            //OldSkills.MightyRoar.Create();
            //OldSkills.ClawsStorm.Create();
            //OldSkills.FrontShield.Create();
            //OldSkills.Prowl.Create();
            //OldSkills.FuriousBite.Create();
            //OldSkills.Dash.Create();
            //OldSkills.ShieldBash.Create();
            //OldSkills.ZoneHeal.Create();
            //OldPassives.WindWalker.Create();
            //OldPassives.TheRipper.Create();
            //OldSkills.SaveMyFriend.Create();
            //OldPassives.ShieldOfPower.Create();
            //OldPassives.BurningSpirit.Create();
            //OldSkills.Slash.Create();
            //OldSkills.FireBird.Create();
            //OldPassives.PassivePower.Create();
            //OldSkills.Revive.Create();
            //OldSkills.Detection.Create();
            //OldPassives.Regeneration.Create();
        }

        public static PantheraSkill GetSkillDef(int skillID)
        {
            if (SkillDefsList.ContainsKey(skillID)) return SkillDefsList[skillID];
            return null;
        }

        public PantheraSkill clone()
        {
            return (PantheraSkill) MemberwiseClone();
        }
    }
}
