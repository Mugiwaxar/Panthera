﻿using Panthera;
using Panthera.Combos;
using Panthera.Combos.List;
using Panthera.GUI;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Combos.List
{
    public class PortalSurge : PantheraCombo
    {

        public PortalSurge()
        {
            ComboSkill comboSkill = new ComboSkill(Panthera.PantheraCharacter.characterSkills.getSkillByID(PantheraConfig.PortalSurge_SkillID), KeysBinder.KeysEnum.SpellsMode, KeysBinder.KeysEnum.Ability4);
            comboSkillsList.Add(comboSkill);
            comboID = PantheraConfig.PortalSurge_CombosID;
            name = PantheraTokens.Get("combo_PortalSurgeName");
            visible = false;
        }

    }
}
