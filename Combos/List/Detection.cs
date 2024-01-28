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
    public class Detection : PantheraCombo
    {

        public Detection()
        {
            ComboSkill comboSkill = new ComboSkill(Panthera.PantheraCharacter.characterSkills.getSkillByID(PantheraConfig.Detection_SkillID), KeysBinder.KeysEnum.Ability3);
            comboSkillsList.Add(comboSkill);
            name = PantheraTokens.Get("combo_TrackingName");
            visible = false;
        }

    }
}
