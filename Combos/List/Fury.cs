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
    public class Fury : PantheraCombo
    {

        public Fury()
        {
            ComboSkill comboSkill = new ComboSkill(Panthera.PantheraCharacter.characterSkills.getSkillByID(PantheraConfig.Fury_SkillID), KeysBinder.KeysEnum.Ability2);
            comboSkillsList.Add(comboSkill);
            comboID = PantheraConfig.Fury_CombosID;
            name = PantheraTokens.Get("combo_FuryName");
            visible = false;
        }

    }
}
