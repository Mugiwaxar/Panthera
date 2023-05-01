using Panthera.MachineScripts;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

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
