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
    public class FeralBite : PantheraCombo
    {

        public FeralBite()
        {
            ComboSkill comboSkill1 = new ComboSkill(Panthera.PantheraCharacter.characterSkills.getSkillByID(PantheraConfig.Leap_SkillID), KeysBinder.KeysEnum.Skill3);
            ComboSkill comboSkill2 = new ComboSkill(Panthera.PantheraCharacter.characterSkills.getSkillByID(PantheraConfig.FeralBite_SkillID), KeysBinder.KeysEnum.Skill1);
            comboSkillsList.Add(comboSkill1);
            comboSkillsList.Add(comboSkill2);
            comboID = PantheraConfig.FeralBite_CombosID;
            name = PantheraTokens.Get("combo_FeralBiteName");
            activated = false;
        }

    }
}
