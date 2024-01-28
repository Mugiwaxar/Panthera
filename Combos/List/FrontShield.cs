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
    public class FrontShield : PantheraCombo
    {

        public FrontShield()
        {
            ComboSkill comboSkill = new ComboSkill(Panthera.PantheraCharacter.characterSkills.getSkillByID(PantheraConfig.FrontShield_SkillID), KeysBinder.KeysEnum.Skill2);
            comboSkillsList.Add(comboSkill);
            name = PantheraTokens.Get("combo_FrontShieldName");
            visible = false;
            activated = false;
        }

    }
}
