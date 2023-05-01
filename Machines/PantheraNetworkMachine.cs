using Panthera.MachineScripts;
using System;
using System.Collections.Generic;
using System.Text;

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
