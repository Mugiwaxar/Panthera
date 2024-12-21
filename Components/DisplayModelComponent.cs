using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Components
{
    class DisplayModelComponent : MonoBehaviour
    {
        private void OnEnable()
        {
            base.transform.localPosition = new Vector3(0f, 0.8f, 0f);
            Utils.Sound.playSound(Utils.Sound.IntroRoar, base.gameObject, false);
        }
    }

}
