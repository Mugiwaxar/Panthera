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
    public class Guardian : PantheraCombo
    {

        public Guardian()
        {
            ComboSkill comboSkill = new ComboSkill(Panthera.PantheraCharacter.CharacterSkills.GetSkillByID(PantheraConfig.Guardian_SkillID), KeysBinder.KeysEnum.Ability4);
            comboSkillsList.Add(comboSkill);
            comboID = PantheraConfig.Guardian_CombosID;
            name = PantheraTokens.Get("combo_GuardianName");
            visible = false;
        }

    }
}
