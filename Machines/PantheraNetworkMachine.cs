using Panthera.MachineScripts;
using System;

namespace Panthera.Machines
{
    class PantheraNetworkMachine : PantheraMachine
    {

        public override void Start()
        {
            base.Start();
            this.nextScript = Activator.CreateInstance<NetworkScript>();
        }

    }
}
