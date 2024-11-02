using Panthera.GUI;
using Panthera.Utils;

namespace Panthera.Combos.List
{
    public class Prowl : PantheraCombo
    {

        public Prowl()
        {
            ComboSkill comboSkill = new ComboSkill(Panthera.PantheraCharacter.CharacterSkills.GetSkillByID(PantheraConfig.Prowl_SkillID), KeysBinder.KeysEnum.Ability1);
            comboSkillsList.Add(comboSkill);
            comboID = PantheraConfig.Prowl_CombosID;
            name = PantheraTokens.Get("combo_ProwlName");
            visible = false;
        }

    }
}
