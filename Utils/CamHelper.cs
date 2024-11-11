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
            ClawsStorm,
            Leap
        }

        // Used to apply an Aim type //
        public static void ApplyCameraType(AimType type, PantheraObj ptraObj, float transitionDuration = 0.3f)
        {
            // Default camera parameters //
            if (type == AimType.Standard)
            {
                Vector3 camPos = PantheraConfig.defaultCamPosition;
                Vector3 defaultCamPos = new Vector3(camPos.x, camPos.y, camPos.z * ptraObj.actualModelScale);
                CharacterCameraParamsData data = ptraObj.pantheraCamParam.cameraParams.data;
                data.idealLocalCameraPos.value = defaultCamPos;
                data.idealLocalCameraPos.alpha = 1;
                ptraObj.pantheraCamParam.cameraParams.data = data;
                if (ptraObj.cameraRigController != null)
                    ptraObj.cameraRigController.baseFov = PantheraConfig.defaultFOV;
                ApplyParam(data, ptraObj, transitionDuration);
                return;
            }
            // Clawstorm camera parameters //
            if (type == AimType.Death)
            {
                CharacterCameraParamsData data = ptraObj.pantheraCamParam.currentCameraParamsData;
                data.idealLocalCameraPos.value += PantheraConfig.Death_cameraPos;
                data.idealLocalCameraPos.alpha = 1;
                ptraObj.pantheraCamParam.cameraParams.data = data;
                ApplyParam(data, ptraObj, transitionDuration);
                return;
            }
            // Clawstorm camera parameters //
            if (type == AimType.ClawsStorm)
            {
                CharacterCameraParamsData data = ptraObj.pantheraCamParam.currentCameraParamsData;
                data.idealLocalCameraPos.value += PantheraConfig.ClawsStorm_cameraPos;
                data.idealLocalCameraPos.alpha = 1;
                ptraObj.pantheraCamParam.cameraParams.data = data;
                ApplyParam(data, ptraObj, transitionDuration);
                return;
            }
            // Leep camera parameters //
            if (type == AimType.Leap)
            {
                if (ptraObj.cameraRigController != null)
                    ptraObj.cameraRigController.baseFov = PantheraConfig.Leap_Fov;
                return;
            }
        }

        // Used to apply all parameters after the camera data was changed //
        public static void ApplyParam(CharacterCameraParamsData data, PantheraObj ptraObj, float transitionDuration)

        {
            // Check if the camera parameters are not null //
            if (ptraObj.pantheraCamParam == null) return;

            // Create the request //
            CameraParamsOverrideRequest camRequest = new CameraParamsOverrideRequest();
            camRequest.cameraParamsData = data;
            camRequest.priority = 1;

            // Send the Data //
            ptraObj.pantheraCamParam.RemoveParamsOverride(ptraObj.lastCamHandle);
            ptraObj.lastCamHandle = ptraObj.pantheraCamParam.AddParamsOverride(camRequest, transitionDuration);

        }

    }
}
