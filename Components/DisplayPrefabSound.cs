using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Components
{
    class DisplayPrefabSound : MonoBehaviour
    {
        private void OnEnable()
        {
            Utils.Sound.playSound(Utils.Sound.IntroRoar, base.gameObject, false);
        }
    }

}
