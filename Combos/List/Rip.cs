using Panthera.GUI;
using Panthera.Utils;

namespace Panthera.Combos.List
{
    public class Rip : PantheraCombo
    {

        public Rip()
        {
            ComboSkill comboSkill = new ComboSkill(Panthera.PantheraCharacter.CharacterSkills.GetSkillByID(PantheraConfig.Rip_SkillID), KeysBinder.KeysEnum.Skill1);
            comboSkillsList.Add(comboSkill);
            comboID = PantheraConfig.Rip_CombosID;
            name = PantheraTokens.Get("combo_RipName");
            visible = false;
        }

    }
}
