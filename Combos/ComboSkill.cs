using Panthera.MachineScripts;
using System;
using System.Collections.Generic;
using System.Text;
using static Panthera.GUI.KeysBinder;

namespace Panthera.Combos
{
    public class ComboSkill
    {

        public MachineScript skill;
        public KeysEnum keyA;
        public KeysEnum keyB;
        public KeysEnum direction;
        public bool biggerIcon = false;

        public ComboSkill (MachineScript skill, KeysEnum keyA, KeysEnum keyB = 0, KeysEnum direction = 0)
        {
            this.skill = skill;
            this.keyA = keyA;
            this.keyB = keyB;
            this.direction = direction;
        }

    }
}
