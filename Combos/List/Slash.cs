using Panthera.GUI;
using Panthera.Utils;

namespace Panthera.Combos.List
{
    public class Slash : PantheraCombo
    {

        public Slash()
        {
            ComboSkill comboSkill = new ComboSkill(Panthera.PantheraCharacter.CharacterSkills.GetSkillByID(PantheraConfig.Slash_SkillID), KeysBinder.KeysEnum.Skill2);
            comboSkillsList.Add(comboSkill);
            comboID = PantheraConfig.Slash_CombosID;
            name = PantheraTokens.Get("combo_SlashName");
            visible = false;
        }

    }
}
