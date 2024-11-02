using Panthera.MachineScripts;
using System;

namespace Panthera.Machines
{
    class PantheraMainMachine : PantheraMachine
    {

        public override void Start()
        {
            base.Start();
            this.nextScript = Activator.CreateInstance<MainScript>();
        }

    }
}
