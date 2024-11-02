using Panthera.MachineScripts;
using static Panthera.GUI.KeysBinder;

namespace Panthera.Combos
{
    public class ComboSkill
    {

        public MachineScript skill;
        public KeysEnum keyA;
        public KeysEnum keyB;
        public KeysEnum modifier;
        public bool biggerIcon = false;

        public ComboSkill (MachineScript skill, KeysEnum keyA, KeysEnum keyB = 0, KeysEnum modifier = 0)
        {
            this.skill = skill;
            this.keyA = keyA;
            this.keyB = keyB;
            this.modifier = modifier;
        }

    }
}
