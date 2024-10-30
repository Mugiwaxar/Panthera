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
    public class Prowl : PantheraCombo
    {

        public Prowl()
        {
            ComboSkill comboSkill = new ComboSkill(Panthera.PantheraCharacter.characterSkills.getSkillByID(PantheraConfig.Prowl_SkillID), KeysBinder.KeysEnum.Ability1);
            comboSkillsList.Add(comboSkill);
            comboID = PantheraConfig.Prowl_CombosID;
            name = PantheraTokens.Get("combo_ProwlName");
            visible = false;
        }

    }
}
