using Panthera;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.NetworkMessages;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.ParticleSystem;

namespace Panthera.BodyComponents
{
    public class PantheraFX : MonoBehaviour
    {

        public PantheraObj ptraObj;
        //private GameObject shieldFX;
        //private Renderer shieldRenderer;
        public int leapTrailFXID;
        public ParticleSystem leapTrailParticles;
        public EmissionModule leapTrailEmission;
        public int dashFXID;
        public ParticleSystem dashParticles;
        public EmissionModule dashEmission;

        public void Init()
        {

            // Get the Panthera Object //
            ptraObj = GetComponent<PantheraObj>();

            // Don't need FX for the server //
            if (NetworkClient.active == false)
            {
                enabled = false;
                return;
            }

            // Create the shield //
            //this.shieldFX = Utils.Functions.SpawnEffect(base.gameObject, Assets.ShieldFX, this.ptraObj.modelTransform.position, 1, this.ptraObj.characterBody.gameObject, new Quaternion(), true);
            //this.shieldRenderer = this.shieldFX.GetComponentInChildren<Renderer>();
            //this.shieldRenderer.enabled = false;

            // Create the Leap Trail //
            leapTrailFXID = Utils.FXManager.SpawnEffect(gameObject, Assets.LeapTrailFX, ptraObj.modelTransform.position, 1, ptraObj.characterBody.gameObject, new Quaternion(), true);
            GameObject leapTrailEffect = Utils.FXManager.GetFX(leapTrailFXID);
            leapTrailParticles = leapTrailEffect.GetComponentInChildren<ParticleSystem>();
            leapTrailEmission = leapTrailParticles.emission;
            leapTrailEmission.enabled = false;

            // Create the Dash Trail //
            dashFXID = Utils.FXManager.SpawnEffect(gameObject, Assets.DashFX, ptraObj.modelTransform.position, 1, ptraObj.characterBody.gameObject, new Quaternion(), true);
            GameObject dashEffect = Utils.FXManager.GetFX(dashFXID);
            dashParticles = dashEffect.GetComponentInChildren<ParticleSystem>();
            dashEmission = dashParticles.emission;
            dashEmission.enabled = false;

        }

        public void FixedUpdate()
        {
            if (this.ptraObj.characterModel.invisibilityCount > 0)
            {
                this.ptraObj.modelTransform.Find("Body").gameObject.SetActive(false);
                this.ptraObj.modelTransform.Find("Arm").gameObject.SetActive(false);

            }
            else
            {
                this.ptraObj.modelTransform.Find("Body").gameObject.SetActive(true);
                this.ptraObj.modelTransform.Find("Arm").gameObject.SetActive(true);
            }
        }

        //public void SetShieldFX(bool state)
        //{
        //    if (NetworkClient.active == false || this.shieldRenderer.enabled == state) return;
        //    this.shieldRenderer.enabled = state;
        //    new ClientSetShieldFX(this.gameObject, state).Send(NetworkDestination.Clients);
        //}

        //public void ServerSetShieldFX(bool state)
        //{
        //    if (this.ptraObj.HasAuthority() == true) return;
        //    this.shieldRenderer.enabled = state;
        //}

        public void SetLeapTrailFX(bool state)
        {
            if (NetworkClient.active == false || leapTrailEmission.enabled == state) return;
            leapTrailEmission.enabled = state;
            new ClientSetLeapTrailFX(gameObject, state).Send(NetworkDestination.Clients);
        }

        public void SetDashFX(bool state)
        {
            if (NetworkClient.active == false || dashEmission.enabled == state) return;
            dashEmission.enabled = state;
            if (RoR2Application.isInMultiPlayer == true) new ClientSetDashFX(gameObject, state).Send(NetworkDestination.Clients);
        }

        public void SetStealthFX(bool state)
        {
            if (NetworkClient.active == false) return;
            new ClientSetStealthFX(gameObject, state).Send(NetworkDestination.Clients);
        }

        public void SetFadeLevel(float level)
        {
            if (ptraObj.stealthed == true)
                level = Math.Min(1f / 255f * 73f, level);
            SkinnedMeshRenderer redenrer = ptraObj.findModelChild("Body").gameObject.GetComponent<SkinnedMeshRenderer>();
            if (level >= 1)
            {
                Utils.Functions.ToOpaqueMode(redenrer.material);
                if (this.ptraObj.PantheraSkinIndex == 3) Utils.Functions.ToOpaqueMode(redenrer.materials[1]);
            }
            else
            {
                Utils.Functions.ToFadeMode(redenrer.material);
                if (this.ptraObj.PantheraSkinIndex == 3) Utils.Functions.ToFadeMode(redenrer.materials[1]);
                Color color = new Color(1, 1, 1, level);
                redenrer.material.SetColor("_Color", color);
                if (this.ptraObj.PantheraSkinIndex == 3) redenrer.materials[1].SetColor("_Color", color);
            }
        }

    }
}
