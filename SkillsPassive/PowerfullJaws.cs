using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.MachineScripts;
using RoR2;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.SkillsPassive
{
    internal class PowerfullJaws
    {

        public static void Create()
        {
            // Create the Skill //
            PantheraSkill skill = new PantheraSkill();
            skill.skillID = PantheraConfig.PowerfullJaws_SkillID;
            skill.name = "POWERFULL_JAWS_ABILITY_NAME";
            skill.desc = "POWERFULL_JAWS_ABILITY_DESC";
            skill.icon = Assets.PowerfullJawsAbility;
            skill.iconPrefab = Assets.PassiveSkillPrefab;
            skill.type = PantheraSkill.SkillType.passive;
            skill.cooldown = PantheraConfig.PowerfullJaws_cooldown;
            skill.associatedSkill = typeof(PowerfullJaws);

            // Save this Skill //
            PantheraSkill.SkillDefsList.Add(skill.skillID, skill);
        }

        public static bool CanBeUsed(PantheraObj ptraObj)
        {
            if (ptraObj.skillLocator.getCooldownInSecond(PantheraConfig.PowerfullJaws_SkillID) > 0) return false;
            return true;
        }

        public static void Use(PantheraObj ptraObj)
        {
            ptraObj.skillLocator.startCooldown(PantheraConfig.PowerfullJaws_SkillID);
        }

    }
}
