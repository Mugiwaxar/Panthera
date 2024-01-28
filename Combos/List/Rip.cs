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
    public class Rip : PantheraCombo
    {

        public Rip()
        {
            ComboSkill comboSkill = new ComboSkill(Panthera.PantheraCharacter.characterSkills.getSkillByID(PantheraConfig.Rip_SkillID), KeysBinder.KeysEnum.Skill1);
            comboSkillsList.Add(comboSkill);
            name = PantheraTokens.Get("combo_RipName");
            visible = false;
        }

    }
}
