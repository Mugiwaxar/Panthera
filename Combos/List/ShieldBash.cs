using Panthera.GUI;
using Panthera.Utils;

namespace Panthera.Combos.List
{
    public class ShieldBash : PantheraCombo
    {

        public ShieldBash()
        {
            ComboSkill comboSkill1 = new ComboSkill(Panthera.PantheraCharacter.CharacterSkills.GetSkillByID(PantheraConfig.FrontShield_SkillID), KeysBinder.KeysEnum.Skill2);
            ComboSkill comboSkill2 = new ComboSkill(Panthera.PantheraCharacter.CharacterSkills.GetSkillByID(PantheraConfig.ShieldBash_SkillID), KeysBinder.KeysEnum.Skill1, KeysBinder.KeysEnum.Skill2);
            comboSkillsList.Add(comboSkill1);
            comboSkillsList.Add(comboSkill2);
            comboID = PantheraConfig.ShieldBash_CombosID;
            name = PantheraTokens.Get("combo_ShieldBashName");
            activated = true;
        }

    }
}
