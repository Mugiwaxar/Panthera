using Panthera;
using Panthera.Combos;
using Panthera.Combos.List;
using Panthera.GUI;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Combos.List
{
    public class MassiveHook : PantheraCombo
    {

        public MassiveHook()
        {
            ComboSkill comboSkill1 = new ComboSkill(Panthera.PantheraCharacter.characterSkills.getSkillByID(PantheraConfig.Rip_SkillID), KeysBinder.KeysEnum.Skill1);
            ComboSkill comboSkill2 = new ComboSkill(Panthera.PantheraCharacter.characterSkills.getSkillByID(PantheraConfig.AirSlash_SkillID), KeysBinder.KeysEnum.Skill1, KeysBinder.KeysEnum.Skill2);
            ComboSkill comboSkill3 = new ComboSkill(Panthera.PantheraCharacter.characterSkills.getSkillByID(PantheraConfig.ConvergenceHook_SkillID), KeysBinder.KeysEnum.Skill2, 0, KeysBinder.KeysEnum.Backward);
            comboSkillsList.Add(comboSkill1);
            comboSkillsList.Add(comboSkill2);
            comboSkillsList.Add(comboSkill3);
            name = PantheraTokens.Get("combo_MassiveHook");
        }

    }
}
