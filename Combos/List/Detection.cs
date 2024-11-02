using Panthera.GUI;
using Panthera.Utils;

namespace Panthera.Combos.List
{
    public class Detection : PantheraCombo
    {

        public Detection()
        {
            ComboSkill comboSkill = new ComboSkill(Panthera.PantheraCharacter.CharacterSkills.GetSkillByID(PantheraConfig.Detection_SkillID), KeysBinder.KeysEnum.Ability3);
            comboSkillsList.Add(comboSkill);
            comboID = PantheraConfig.Detection_CombosID;
            name = PantheraTokens.Get("combo_TrackingName");
            visible = false;
        }

    }
}
