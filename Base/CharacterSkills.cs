    using Panthera.MachineScripts;
using Panthera.Skills.Actives;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Panthera.Base
{
    public class CharacterSkills
    {

        // (int SkillID, MachineScript skill) Represent the Skill List //
        public Dictionary<int, MachineScript> SkillsList = new Dictionary<int, MachineScript>();

        public CharacterSkills()
        {
            this.addSkillToList(PantheraConfig.Rip_SkillID, new Rip());
            this.addSkillToList(PantheraConfig.AirCleave_SkillID, new AirCleave());
            this.addSkillToList(PantheraConfig.Leap_SkillID, new Leap());
            this.addSkillToList(PantheraConfig.MightyRoar_SkillID, new MightyRoar());
            this.addSkillToList(PantheraConfig.Fury_SkillID, new Fury());
            this.addSkillToList(PantheraConfig.Guardian_SkillID, new Guardian());
            this.addSkillToList(PantheraConfig.Slash_SkillID, new Slash());
            this.addSkillToList(PantheraConfig.Detection_SkillID, new Detection());
            this.addSkillToList(PantheraConfig.Prowl_SkillID, new Prowl());
            this.addSkillToList(PantheraConfig.Ambition_SkillID, new Ambition());
            this.addSkillToList(PantheraConfig.AirSlash_SkillID, new AirSlash());
            this.addSkillToList(PantheraConfig.FrontShield_SkillID, new FrontShield());
            this.addSkillToList(PantheraConfig.ClawsStorm_SkillID, new ClawsStorm());
            this.addSkillToList(PantheraConfig.ShieldBash_SkillID, new ShieldBash());
            this.addSkillToList(PantheraConfig.ArcaneAnchor_SkillID, new ArcaneAnchor());
            this.addSkillToList(PantheraConfig.ConvergenceHook_SkillID, new ConvergenceHook());
        }

        public void addSkillToList(int ID, MachineScript skill)
        {
            if (this.SkillsList.ContainsKey(ID))
            {
                Debug.LogWarning("[Panthera -> Character.addSkillToList] The Skills List already contain a Skill with the ID: " + ID);
                return;
            }
            this.SkillsList.Add(ID, skill);
        }

        public MachineScript getSkillByID(int ID)
        {
            if (this.SkillsList.ContainsKey(ID))
                return this.SkillsList[ID];
            return null;
        }

    }
}
