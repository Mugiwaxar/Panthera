using Panthera.GUI;
using Panthera.Utils;

namespace Panthera.Combos.List
{
    public class MassiveHook : PantheraCombo
    {

        public MassiveHook()
        {
            ComboSkill comboSkill1 = new ComboSkill(Panthera.PantheraCharacter.CharacterSkills.GetSkillByID(PantheraConfig.Rip_SkillID), KeysBinder.KeysEnum.Skill1);
            ComboSkill comboSkill2 = new ComboSkill(Panthera.PantheraCharacter.CharacterSkills.GetSkillByID(PantheraConfig.AirSlash_SkillID), KeysBinder.KeysEnum.Skill1, KeysBinder.KeysEnum.Skill2);
            ComboSkill comboSkill3 = new ComboSkill(Panthera.PantheraCharacter.CharacterSkills.GetSkillByID(PantheraConfig.ConvergenceHook_SkillID), KeysBinder.KeysEnum.Skill2, 0, KeysBinder.KeysEnum.Backward);
            comboSkillsList.Add(comboSkill1);
            comboSkillsList.Add(comboSkill2);
            comboSkillsList.Add(comboSkill3);
            comboID = PantheraConfig.MassiveHook_CombosID;
            name = PantheraTokens.Get("combo_MassiveHookName");
        }

    }
}
