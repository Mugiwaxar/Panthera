using Panthera.MachineScripts;
using System;
using System.Collections.Generic;
using System.Text;
using static RoR2.Skills.ComboSkillDef;

namespace Panthera.Combos
{
    public class PantheraCombo
    {

        public List<ComboSkill> comboSkillsList = new List<ComboSkill>();

        public string name = "Undefined";
        public int comboID = 0;
        public bool visible = true;
        public bool activated = true;

    }
}
