using Panthera.Base;
using Panthera.Components;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.SkillsPassive
{
    internal class BurningSpirit
    {

        public static void Create()
        {
            // Create the Skill //
            PantheraSkill skill = new PantheraSkill();
            skill.skillID = PantheraConfig.BurningSpirit_SkillID;
            skill.name = "BURNING_SPIRIT_ABILITY_NAME";
            skill.desc = "BURNING_SPIRIT_ABILITY_DESC";
            skill.icon = Assets.BurningSpiritAbility;
            skill.iconPrefab = Assets.PassiveSkillPrefab;
            skill.type = PantheraSkill.SkillType.passive;
            skill.associatedSkill = typeof(BurningSpirit);

            // Save this Skill //
            PantheraSkill.SkillDefsList.Add(skill.skillID, skill);
        }

    }
}
