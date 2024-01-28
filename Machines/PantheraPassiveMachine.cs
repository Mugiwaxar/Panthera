using Panthera.MachineScripts;
using System;
using System.Collections.Generic;
using System.Text;

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
