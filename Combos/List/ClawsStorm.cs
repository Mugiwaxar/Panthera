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
    public class ClawsStorm : PantheraCombo
    {

        public ClawsStorm()
        {
            ComboSkill comboSkill = new ComboSkill(Panthera.PantheraCharacter.CharacterSkills.GetSkillByID(PantheraConfig.ClawsStorm_SkillID), KeysBinder.KeysEnum.Skill2);
            comboSkillsList.Add(comboSkill);
            comboID = PantheraConfig.ClawsStorm_CombosID;
            name = PantheraTokens.Get("combo_ClawsStormName");
            visible = false;
            activated = false;
        }

    }
}
