using Panthera.MachineScripts;
using Panthera.Skills.Actives;
using System.Collections.Generic;

namespace Panthera.Base
{
    public class CharacterSkills
    {

        // (int SkillID, MachineScript skill) Represent the Skill List //
        public List<MachineScript> SkillsList { get; set; } = [];

        public CharacterSkills()
        {
            PantheraConfig.Rip_SkillID = this.AddSkillToList(new Rip());
            PantheraConfig.AirCleave_SkillID = this.AddSkillToList(new AirCleave());
            PantheraConfig.Leap_SkillID = this.AddSkillToList(new Leap());
            PantheraConfig.MightyRoar_SkillID = this.AddSkillToList(new MightyRoar());
            PantheraConfig.Fury_SkillID = this.AddSkillToList(new Fury());
            PantheraConfig.Guardian_SkillID = this.AddSkillToList(new Guardian());
            PantheraConfig.Slash_SkillID = this.AddSkillToList(new Slash());
            PantheraConfig.Detection_SkillID = this.AddSkillToList(new Detection());
            PantheraConfig.Prowl_SkillID = this.AddSkillToList(new Prowl());
            PantheraConfig.Ambition_SkillID = this.AddSkillToList(new Ambition());
            PantheraConfig.AirSlash_SkillID = this.AddSkillToList(new AirSlash());
            PantheraConfig.FrontShield_SkillID = this.AddSkillToList(new FrontShield());
            PantheraConfig.ClawsStorm_SkillID = this.AddSkillToList(new ClawsStorm());
            PantheraConfig.ShieldBash_SkillID = this.AddSkillToList(new ShieldBash());
            PantheraConfig.ArcaneAnchor_SkillID = this.AddSkillToList(new ArcaneAnchor());
            PantheraConfig.ConvergenceHook_SkillID = this.AddSkillToList(new ConvergenceHook());
            PantheraConfig.PortalSurge_SkillID = this.AddSkillToList(new PortalSurge());
        }

        public int AddSkillToList(MachineScript skill)
        {
            this.SkillsList.Add(skill);
            return this.SkillsList.IndexOf(skill);
        }

        public MachineScript GetSkillByID(int ID) => this.SkillsList[ID];

    }
}
