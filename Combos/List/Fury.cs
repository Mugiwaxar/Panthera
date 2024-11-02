using Panthera.GUI;
using Panthera.Utils;

namespace Panthera.Combos.List
{
    public class Fury : PantheraCombo
    {

        public Fury()
        {
            ComboSkill comboSkill = new ComboSkill(Panthera.PantheraCharacter.CharacterSkills.GetSkillByID(PantheraConfig.Fury_SkillID), KeysBinder.KeysEnum.Ability2);
            comboSkillsList.Add(comboSkill);
            comboID = PantheraConfig.Fury_CombosID;
            name = PantheraTokens.Get("combo_FuryName");
            visible = false;
        }

    }
}
