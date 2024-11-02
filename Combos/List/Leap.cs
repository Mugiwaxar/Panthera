using Panthera.GUI;
using Panthera.Utils;

namespace Panthera.Combos.List
{
    public class Leap : PantheraCombo
    {

        public Leap()
        {
            ComboSkill comboSkill = new ComboSkill(Panthera.PantheraCharacter.CharacterSkills.GetSkillByID(PantheraConfig.Leap_SkillID), KeysBinder.KeysEnum.Skill3);
            comboSkillsList.Add(comboSkill);
            comboID = PantheraConfig.Leap_CombosID;
            name = PantheraTokens.Get("combo_LeapName");
            visible = false;
        }

    }
}
