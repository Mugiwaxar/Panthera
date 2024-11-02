using Panthera.GUI;
using Panthera.Utils;

namespace Panthera.Combos.List
{
    public class MightyRoar : PantheraCombo
    {

        public MightyRoar()
        {
            ComboSkill comboSkill = new ComboSkill(Panthera.PantheraCharacter.CharacterSkills.GetSkillByID(PantheraConfig.MightyRoar_SkillID), KeysBinder.KeysEnum.Skill4);
            comboSkillsList.Add(comboSkill);
            comboID = PantheraConfig.MightyRoar_CombosID;
            name = PantheraTokens.Get("combo_MightyRoarName");
            visible = false;
        }

    }
}
