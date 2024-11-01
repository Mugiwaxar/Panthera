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
    public class AirSlash : PantheraCombo
    {

        public AirSlash()
        {
            ComboSkill comboSkill1 = new ComboSkill(Panthera.PantheraCharacter.CharacterSkills.GetSkillByID(PantheraConfig.Rip_SkillID), KeysBinder.KeysEnum.Skill1);
            ComboSkill comboSkill2 = new ComboSkill(Panthera.PantheraCharacter.CharacterSkills.GetSkillByID(PantheraConfig.AirSlash_SkillID), KeysBinder.KeysEnum.Skill1, KeysBinder.KeysEnum.Skill2);
            comboSkillsList.Add(comboSkill1);
            comboSkillsList.Add(comboSkill2);
            comboID = PantheraConfig.AirSlash_CombosID;
            name = PantheraTokens.Get("combo_AirSlashName");
        }

    }
}
