using EntityStates;
using Panthera.BodyComponents;
using Panthera.NetworkMessages;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Utils
{
    public class Animation
    {

        public static void PlayAnimation(PantheraObj ptraObj, string animName, float crossFadeTime = 0, bool transmit = true)
        {
            if (ptraObj == null) return;
            Animator animator = ptraObj.modelAnimator;
            if (animator == null) return;
            animator.speed = 1f;
            animator.Update(0f);
            if (crossFadeTime > 0)
                animator.CrossFadeInFixedTime(animName, crossFadeTime, -1, 0);
            else
                animator.PlayInFixedTime(animName, -1, 0);
            if (RoR2Application.isInMultiPlayer == true && transmit == true)
                new ServerPlayAnimation(ptraObj.gameObject, animName, crossFadeTime).Send(NetworkDestination.Server);
        }

        public static void SetBoolean(PantheraObj ptraObj, string paramName, bool setValue, bool transmit = true)
        {
            if (ptraObj == null) return;
            Animator animator = ptraObj.modelAnimator;
            if (animator == null) return;
            animator.SetBool(paramName, setValue);
            if (RoR2Application.isInMultiPlayer == true && transmit == true)
                new ServerSetAnimatorBoolean(ptraObj.gameObject, paramName, setValue).Send(NetworkDestination.Server);
        }

        public static void SetFloat(PantheraObj ptraObj, string paramName, float value1 = 0, float value2 = 0, float value3 = 0, bool transmit = true)
        {
            if (ptraObj == null) return;
            Animator animator = ptraObj.modelAnimator;
            if (animator == null) return;
            animator.SetFloat(paramName, value1, value2, value3);
            if (RoR2Application.isInMultiPlayer == true && transmit == true)
                new ServerSetAnimatorFloat(ptraObj.gameObject, paramName, value1, value2, value3).Send(NetworkDestination.Server);
        }

    }
}
