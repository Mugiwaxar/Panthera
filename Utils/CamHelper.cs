using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static RoR2.CameraTargetParams;

namespace Panthera.Utils
{
    internal class CamHelper
    {

		public enum AimType
		{
			Standard,
			ClawsStorm
		}


        public static CameraTargetParams pantheraCamParam;
		public static CameraParamsOverrideHandle lastCamHandle;

		// Used to apply all parameters after the camera data was changed //
		public static void applyParam(CharacterCameraParamsData data)

        {
			// Check if the camera parameters are not null //
			if (pantheraCamParam == null) return;

			// Create the request //
			CameraParamsOverrideRequest camRequest = new CameraParamsOverrideRequest();
			camRequest.cameraParamsData = data;

			// Send the Data //
			pantheraCamParam.RemoveParamsOverride(lastCamHandle);
			lastCamHandle = pantheraCamParam.AddParamsOverride(camRequest, 0.3f);

		}

		// Used to apply an Aim type //
		public static void applyAimType(AimType type)
        {
			// Default camera parameters //
			if (type == AimType.Standard)
			{
				pantheraCamParam.RemoveParamsOverride(lastCamHandle);
				return;
			}
			// Clawstorm camera parameters //
			if (type == AimType.ClawsStorm)
			{
				CharacterCameraParamsData data = pantheraCamParam.currentCameraParamsData;
				data.idealLocalCameraPos.value += new Vector3(0f, 1.5f, -5f);
				applyParam(data);
				return;
			}
		}

	}
}
