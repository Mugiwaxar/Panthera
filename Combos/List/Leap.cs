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
    public class Leap : PantheraCombo
    {

        public Leap()
        {
            ComboSkill comboSkill = new ComboSkill(Panthera.PantheraCharacter.characterSkills.getSkillByID(PantheraConfig.Leap_SkillID), KeysBinder.KeysEnum.Skill3);
            comboSkillsList.Add(comboSkill);
            comboID = PantheraConfig.Leap_CombosID;
            name = PantheraTokens.Get("combo_LeapName");
            visible = false;
        }

    }
}
