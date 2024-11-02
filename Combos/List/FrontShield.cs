using Panthera.GUI;
using Panthera.Utils;

namespace Panthera.Combos.List
{
    public class FrontShield : PantheraCombo
    {

        public FrontShield()
        {
            ComboSkill comboSkill = new ComboSkill(Panthera.PantheraCharacter.CharacterSkills.GetSkillByID(PantheraConfig.FrontShield_SkillID), KeysBinder.KeysEnum.Skill2);
            comboSkillsList.Add(comboSkill);
            comboID = PantheraConfig.FrontShield_CombosID;
            name = PantheraTokens.Get("combo_FrontShieldName");
            visible = false;
            activated = false;
        }

    }
}
