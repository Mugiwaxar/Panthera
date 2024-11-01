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
    public class Ambition : PantheraCombo
    {

        public Ambition()
        {
            ComboSkill comboSkill1 = new ComboSkill(Panthera.PantheraCharacter.CharacterSkills.GetSkillByID(PantheraConfig.Ambition_SkillID), KeysBinder.KeysEnum.SpellsMode, KeysBinder.KeysEnum.Skill3);
            comboSkillsList.Add(comboSkill1);
            visible = false;
            comboID = PantheraConfig.Ambition_CombosID;
            name = PantheraTokens.Get("combo_AmbitionName");
        }

    }
}
