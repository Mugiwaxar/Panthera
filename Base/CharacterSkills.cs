using Panthera.MachineScripts;
using Panthera.Skills.Actives;
using System.Collections.Generic;
using UnityEngine;

namespace Panthera.Base
{
    public class CharacterSkills
    {
        // (int SkillID, MachineScript skill) Represent the Skill List //
        public Dictionary<int, MachineScript> SkillsList = new Dictionary<int, MachineScript>();

        public CharacterSkills()
        {
            this.AddSkillToList(PantheraConfig.Rip_SkillID, new Rip());
            this.AddSkillToList(PantheraConfig.AirCleave_SkillID, new AirCleave());
            this.AddSkillToList(PantheraConfig.Leap_SkillID, new Leap());
            this.AddSkillToList(PantheraConfig.MightyRoar_SkillID, new MightyRoar());
            this.AddSkillToList(PantheraConfig.Fury_SkillID, new Fury());
            this.AddSkillToList(PantheraConfig.Guardian_SkillID, new Guardian());
            this.AddSkillToList(PantheraConfig.Slash_SkillID, new Slash());
            this.AddSkillToList(PantheraConfig.Detection_SkillID, new Detection());
            this.AddSkillToList(PantheraConfig.Prowl_SkillID, new Prowl());
            this.AddSkillToList(PantheraConfig.Ambition_SkillID, new Ambition());
            this.AddSkillToList(PantheraConfig.AirSlash_SkillID, new AirSlash());
            this.AddSkillToList(PantheraConfig.FrontShield_SkillID, new FrontShield());
            this.AddSkillToList(PantheraConfig.ClawsStorm_SkillID, new ClawsStorm());
            this.AddSkillToList(PantheraConfig.ShieldBash_SkillID, new ShieldBash());
            this.AddSkillToList(PantheraConfig.ArcaneAnchor_SkillID, new ArcaneAnchor());
            this.AddSkillToList(PantheraConfig.ConvergenceHook_SkillID, new ConvergenceHook());
            this.AddSkillToList(PantheraConfig.PortalSurge_SkillID, new PortalSurge());
        }

        public void AddSkillToList(int ID, MachineScript skill)
        {
            if (this.SkillsList.ContainsKey(ID))
            {
                Debug.LogWarning("[Panthera -> Character.addSkillToList] The Skills List already contain a Skill with the ID: " + ID);
                return;
            }
            this.SkillsList.Add(ID, skill);
        }

        public MachineScript GetSkillByID(int ID) => this.SkillsList.ContainsKey(ID) ? this.SkillsList[ID] : null;

    }
}
