using Panthera;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.NetworkMessages;
using Panthera.OldSkills;
using Panthera.Passives;
using Panthera.Skills.Passives;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using RoR2.Skills;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Panthera.Skills.Passives
{
    public class Detection
    {

        public static void EnableDetection(PantheraObj ptraObj)
        {

            // Enable the Detection Mode //
            ptraObj.detectionMode = true;

            // Play the Sound //
            Utils.Sound.playSound(Utils.Sound.DetectionEnable, ptraObj.gameObject);

            // Start the Cooldown //
            ptraObj.skillLocator.startCooldown(PantheraConfig.Detection_SkillID, 0.1f);

            // Set the Camera //
            Camera cam = Camera.main;
            cam.cullingMask = cam.cullingMask & ~(1 << 31);
            if (cam.GetComponent<PostProcessLayer>())
                cam.GetComponent<PostProcessLayer>().stopNaNPropagation = true;
            ptraObj.pantheraCam.gameObject.SetActive(true);

            // Start the Detection FX //
            ptraObj.StartCoroutine(EnableDetectionFX(ptraObj));

            // Change the Model Layer //
            ptraObj.characterModel.mainSkinnedMeshRenderer.gameObject.layer = PantheraConfig.Detection_layerIndex;

            // Recalculate Stats //
            ptraObj.characterBody.RecalculateStats();

        }

        public static void DisableDetection(PantheraObj ptraObj)
        {

            // Disable the Detection Mode //
            ptraObj.detectionMode = false;

            // Play the Sound //
            Utils.Sound.playSound(Utils.Sound.DetectionDisable, ptraObj.gameObject);

            // Start the Cooldown //
            //ptraObj.skillLocator.startCooldown(PantheraConfig.Detection_SkillID);

            // Set the normal Camera //
            Camera cam = Camera.main;
            cam.cullingMask = cam.cullingMask | 1 << 31;
            if (cam.GetComponent<PostProcessLayer>())
                cam.GetComponent<PostProcessLayer>().stopNaNPropagation = false;
            ptraObj.pantheraCam.gameObject.SetActive(false);

            // Disable the Detection FX //
            ptraObj.StartCoroutine(DisableDetectionFX(ptraObj));

            // Se the Model Layer back //
            ptraObj.characterModel.mainSkinnedMeshRenderer.gameObject.layer = ptraObj.origLayerIndex;

            // Recalculate Stats //
            ptraObj.characterBody.RecalculateStats();

        }

        public static IEnumerator EnableDetectionFX(PantheraObj ptraObj)
        {

            PostProcessVolume postProcess = ptraObj.pantheraPostProcess.GetComponent<PostProcessVolume>();
            postProcess.weight = 0;
            ptraObj.origPostProcess.SetActive(false);
            ptraObj.pantheraPostProcess.SetActive(true);

            float weight = 0;
            while (weight < 1)
            {
                weight += 0.05f;
                if (weight > 1) weight = 1;
                postProcess.weight = weight;
                yield return new WaitForSeconds(0.01f);
            }

            yield break;

        }

        public static IEnumerator DisableDetectionFX(PantheraObj ptraObj)
        {

            PostProcessVolume postProcess = ptraObj.pantheraPostProcess.GetComponent<PostProcessVolume>();
            postProcess.weight = 1;

            float weight = 1;
            while (weight > 0)
            {
                weight -= 0.05f;
                if (weight < 0) weight = 0;
                postProcess.weight = weight;
                yield return new WaitForSeconds(0.01f);
            }

            ptraObj.origPostProcess.SetActive(true);
            ptraObj.pantheraPostProcess.SetActive(false);

            yield break;

        }

        public static void ReScanBody(PantheraObj ptraObj)
        {
            List<CharacterBody> bodyLists = UnityEngine.Object.FindObjectsOfType<CharacterBody>().ToList();
            if (bodyLists == null || bodyLists.Count == 0) return;
            foreach (CharacterBody body in bodyLists)
            {
                if (body == ptraObj.characterBody) continue;
                XRayComponent component = body.GetComponent<XRayComponent>();
                if (component == null) component = body.gameObject.AddComponent<XRayComponent>();
                component.ptraObj = ptraObj;
                component.type = XRayComponent.XRayObjectType.Body;
            }
        }

        public static float GetDetectionMaxTime(PantheraObj ptraObj)
        {
            int level = ptraObj.getAbilityLevel(PantheraConfig.SuperiorFlair_AbilityID);
            float maxTime = PantheraConfig.Detection_maxTime;
            if (level == 1) maxTime = PantheraConfig.SuperiorFlair_percent1;
            else if (level == 2) maxTime = PantheraConfig.SuperiorFlair_percent2;
            else if (level == 3) maxTime = PantheraConfig.SuperiorFlair_percent3;
            else if (level == 4) maxTime = PantheraConfig.SuperiorFlair_percent4;
            else if (level == 5) maxTime = PantheraConfig.SuperiorFlair_percent5;
            return maxTime;
        }

    }
}
