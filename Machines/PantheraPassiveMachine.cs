using Panthera.MachineScripts;
using System;

namespace Panthera.Machines
{
    class PantheraPassiveMachine : PantheraMachine
    {

        public override void Start()
        {
            base.Start();
            this.nextScript = Activator.CreateInstance<BigCatPassive>();
        }

    }
}
