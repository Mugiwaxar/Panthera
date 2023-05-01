using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.SkillsPassive
{
    internal class Regeneration
    {

        public static void Create()
        {
            // Create the Skill //
            PantheraSkill skill = new PantheraSkill();
            skill.skillID = PantheraConfig.Regeneration_SkillID;
            skill.name = "REGENERATION_SKILL_NAME";
            skill.desc = "REGENERATION_SKILL_DESC";
            skill.icon = Assets.RegenerationAbility;
            skill.iconPrefab = Assets.PassiveSkillPrefab;
            skill.type = PantheraSkill.SkillType.passive;
            skill.associatedSkill = typeof(Regeneration);

            // Save this Skill //
            PantheraSkill.SkillDefsList.Add(skill.skillID, skill);
        }

    }
}
