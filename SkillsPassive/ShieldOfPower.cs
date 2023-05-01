using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.SkillsPassive
{
    internal class ShieldOfPower
    {

        public static void Create()
        {
            // Create the Skill //
            PantheraSkill skill = new PantheraSkill();
            skill.skillID = PantheraConfig.ShieldOfPower_SkillID;
            skill.name = "SHIELD_OF_POWER_ABILITY_NAME";
            skill.desc = "SHIELD_OF_POWER_ABILITY_DESC";
            skill.icon = Assets.ShieldOfPowerAbility;
            skill.iconPrefab = Assets.PassiveSkillPrefab;
            skill.type = PantheraSkill.SkillType.passive;
            skill.associatedSkill = typeof(ShieldOfPower);

            // Save this Skill //
            PantheraSkill.SkillDefsList.Add(skill.skillID, skill);
        }

        public static void ApplyAbility(PantheraObj ptraObj)
        {
            // Check the Ability Level //
            if (ptraObj.activePreset != null && ptraObj.activePreset.getAbilityLevel(PantheraConfig.ShieldOfPowerAbilityID) > 0)
            {
                ptraObj.characterBody.power += 1;
            }
        }

    }
}
