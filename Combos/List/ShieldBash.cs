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
    public class ShieldBash : PantheraCombo
    {

        public ShieldBash()
        {
            ComboSkill comboSkill1 = new ComboSkill(Panthera.PantheraCharacter.characterSkills.getSkillByID(PantheraConfig.FrontShield_SkillID), KeysBinder.KeysEnum.Skill2);
            ComboSkill comboSkill2 = new ComboSkill(Panthera.PantheraCharacter.characterSkills.getSkillByID(PantheraConfig.ShieldBash_SkillID), KeysBinder.KeysEnum.Skill1, KeysBinder.KeysEnum.Skill2);
            comboSkillsList.Add(comboSkill1);
            comboSkillsList.Add(comboSkill2);
            name = PantheraTokens.Get("combo_ShieldBashName");
            activated = true;
        }

    }
}
