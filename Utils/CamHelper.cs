using Panthera.BodyComponents;
using RoR2;
using RoR2.CameraModes;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static RoR2.CameraTargetParams;
using static UnityEngine.ParticleSystem.PlaybackState;

namespace Panthera.Utils
{
    public class CamHelper
    {

        public enum AimType
        {
            Standard,
            Death,
            ClawsStorm
        }

        // Used to apply all parameters after the camera data was changed //
        public static void ApplyParam(CharacterCameraParamsData data, PantheraObj ptraObj)

        {
            // Check if the camera parameters are not null //
            if (ptraObj.pantheraCamParam == null) return;

            // Create the request //
            CameraParamsOverrideRequest camRequest = new CameraParamsOverrideRequest();
            camRequest.cameraParamsData = data;

            // Send the Data //
            ptraObj.pantheraCamParam.RemoveParamsOverride(ptraObj.lastCamHandle);
            ptraObj.lastCamHandle = ptraObj.pantheraCamParam.AddParamsOverride(camRequest, 0.3f);

        }

        // Used to apply an Aim type //
        public static void ApplyAimType(AimType type, PantheraObj ptraObj)
        {
            // Default camera parameters //
            if (type == AimType.Standard)
            {
                CharacterCameraParamsData data = ptraObj.pantheraCamParam.currentCameraParamsData;
                data.idealLocalCameraPos.value = ptraObj.defaultCamPos;
                data.idealLocalCameraPos.alpha = 1;
                ApplyParam(data, ptraObj);
                return;
            }
            // Clawstorm camera parameters //
            if (type == AimType.Death)
            {
                CharacterCameraParamsData data = ptraObj.pantheraCamParam.currentCameraParamsData;
                data.idealLocalCameraPos.value = PantheraConfig.Death_cameraPos;
                data.idealLocalCameraPos.alpha = 1;
                ApplyParam(data, ptraObj);
                return;
            }
            // Clawstorm camera parameters //
            if (type == AimType.ClawsStorm)
            {
                CharacterCameraParamsData data = ptraObj.pantheraCamParam.currentCameraParamsData;
                data.idealLocalCameraPos.value = PantheraConfig.ClawsStorm_cameraPos;
                data.idealLocalCameraPos.alpha = 1;
                ApplyParam(data, ptraObj);
                return;
            }
        }

    }
}
