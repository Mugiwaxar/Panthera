using System;
using System.Collections.Generic;
using System.Text;
using Panthera;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using RoR2;
using UnityEngine.Networking;

namespace Panthera.Components
{

    public class PantheraMaster : NetworkBehaviour
    {

        public float savedFury = PantheraConfig.Fury_startingFury;
        public float savedPower = 0;
        public bool firstStarted = false;
        public Dictionary<int, float> savedCooldownList = new Dictionary<int, float>();

        public PantheraObj ptraObj
        {
            get
            {
                PantheraObj obj = this.GetComponent<CharacterMaster>()?.GetBodyObject()?.GetComponent<PantheraObj>();
                if (obj != null) return obj;
                return null;
            }
        }

        public void Start()
        {
            if (Util.HasEffectiveAuthority(base.gameObject) == true)
            {
                Preset.ActivePreset.applyAbilities();
                Character.AllowXP = true;
                PantheraConfig.readDefs();
            }
        }

    }
}
