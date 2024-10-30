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
    public class ArcaneAnchor : PantheraCombo
    {

        public ArcaneAnchor()
        {
            ComboSkill comboSkill1 = new ComboSkill(Panthera.PantheraCharacter.characterSkills.getSkillByID(PantheraConfig.FrontShield_SkillID), KeysBinder.KeysEnum.Skill2);
            ComboSkill comboSkill2 = new ComboSkill(Panthera.PantheraCharacter.characterSkills.getSkillByID(PantheraConfig.ArcaneAnchor_SkillID), KeysBinder.KeysEnum.Skill2, KeysBinder.KeysEnum.Skill4);
            comboSkillsList.Add(comboSkill1);
            comboSkillsList.Add(comboSkill2);
            comboID = PantheraConfig.ArcaneAnchor_CombosID;
            name = PantheraTokens.Get("combo_ArcaneAnchorName");
            activated = true;
        }

    }
}
