using Panthera;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.NetworkMessages;
using Panthera.Passives;
using Panthera.OldSkills;
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
            ptraObj.detectionMode = true;
            Utils.Sound.playSound(Utils.Sound.DetectionEnable, ptraObj.gameObject);
            ptraObj.skillLocator.startCooldown(PantheraConfig.Detection_SkillID, 1);
            Camera cam = Camera.main;
            cam.cullingMask = cam.cullingMask & ~(1 << 31);
            if (cam.GetComponent<PostProcessLayer>())
                cam.GetComponent<PostProcessLayer>().stopNaNPropagation = true;
            ptraObj.pantheraCam.gameObject.SetActive(true);
            ptraObj.StartCoroutine(EnableDetectionFX(ptraObj));
            ptraObj.characterModel.mainSkinnedMeshRenderer.gameObject.layer = PantheraConfig.Detection_layerIndex;
            ptraObj.characterBody.RecalculateStats();
        }

        public static void DisableDetection(PantheraObj ptraObj)
        {
            ptraObj.detectionMode = false;
            Utils.Sound.playSound(Utils.Sound.DetectionDisable, ptraObj.gameObject);
            ptraObj.skillLocator.startCooldown(PantheraConfig.Detection_SkillID);
            Camera cam = Camera.main;
            cam.cullingMask = cam.cullingMask | 1 << 31;
            if (cam.GetComponent<PostProcessLayer>())
                cam.GetComponent<PostProcessLayer>().stopNaNPropagation = false;
            ptraObj.pantheraCam.gameObject.SetActive(false);
            ptraObj.StartCoroutine(DisableDetectionFX(ptraObj));
            ptraObj.characterModel.mainSkinnedMeshRenderer.gameObject.layer = ptraObj.origLayerIndex;
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

        public static void ReScanBody(PantheraObj ptra)
        {
            List<CharacterBody> bodyLists = UnityEngine.Object.FindObjectsOfType<CharacterBody>().ToList();
            if (bodyLists == null || bodyLists.Count == 0) return;
            foreach (CharacterBody body in bodyLists)
            {
                if (body == ptra.characterBody) continue;
                XRayComponent component = body.GetComponent<XRayComponent>();
                if (component == null) component = body.gameObject.AddComponent<XRayComponent>();
                component.ptraObj = ptra;
                component.type = XRayComponent.XRayObjectType.Body;
            }
        }

    }
}
