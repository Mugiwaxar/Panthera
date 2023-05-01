using Panthera.Base;
using Panthera.Components;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.SkillsPassive
{
    internal class TheRipper
    {

        public static void Create()
        {
            // Create the Skill //
            PantheraSkill skill = new PantheraSkill();
            skill.skillID = PantheraConfig.TheRipper_SkillID;
            skill.name = "THE_RIPPER_SKILL_NAME";
            skill.desc = "THE_RIPPER_SKILL_DESC";
            skill.icon = Assets.TheRipperAbility;
            skill.iconPrefab = Assets.PassiveSkillPrefab;
            skill.type = PantheraSkill.SkillType.passive;
            skill.associatedSkill = typeof(TheRipper);

            // Save this Skill //
            PantheraSkill.SkillDefsList.Add(skill.skillID, skill);
        }

    }
}
