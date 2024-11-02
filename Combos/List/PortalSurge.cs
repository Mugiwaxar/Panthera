using Panthera.GUI;
using Panthera.Utils;

namespace Panthera.Combos.List
{
    public class PortalSurge : PantheraCombo
    {

        public PortalSurge()
        {
            ComboSkill comboSkill = new ComboSkill(Panthera.PantheraCharacter.CharacterSkills.GetSkillByID(PantheraConfig.PortalSurge_SkillID), KeysBinder.KeysEnum.Ability4);
            comboSkill.modifier = KeysBinder.KeysEnum.SpellsMode;
            comboSkillsList.Add(comboSkill);
            comboID = PantheraConfig.PortalSurge_CombosID;
            name = PantheraTokens.Get("combo_PortalSurgeName");
            visible = false;
        }

    }
}
