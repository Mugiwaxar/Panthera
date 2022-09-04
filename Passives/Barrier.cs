using Panthera.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Passives
{
    class Barrier
    {

        public static void ApplyBarrier(PantheraHealthComponent comp, float damage)
        {
            comp.AddBarrier(damage * PantheraConfig.Passive_barriereMultiplier);
        }

    }
}
