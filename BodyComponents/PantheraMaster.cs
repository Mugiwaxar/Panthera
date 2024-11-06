using Panthera;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;

namespace Panthera.BodyComponents
{

    public class PantheraMaster : NetworkBehaviour
    {

        public float savedFury = 0;
        public bool firstStarted = false;
        public Dictionary<int, RechargeSkill> savedCooldownList;

        public PantheraObj ptraObj
        {
            get
            {
                PantheraObj obj = GetComponent<CharacterMaster>()?.GetBodyObject()?.GetComponent<PantheraObj>();
                if (obj != null) return obj;
                return null;
            }
        }

        public void Start()
        {
            if (Util.HasEffectiveAuthority(gameObject) == true)
            {
                PantheraObj.readDefs();
            }
        }

    }
}
