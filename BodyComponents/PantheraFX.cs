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
        public int furyAuraFXID;
        public GameObject furyAuraObj;
        public int GuardianAuraFXID;
        public GameObject GuardianAuraObj;
        public int AmbitionAuraFXID;
        public GameObject AmbitionAuraObj;
        public Renderer renderer;

        public void DoInit()
        {

            // Get the Panthera Object //
            ptraObj = GetComponent<PantheraObj>();

            // Don't need FX for the server //
            if (NetworkClient.active == false)
            {
                base.enabled = false;
                return;
            }

            // Create the shield //
            //this.shieldFX = Utils.Functions.SpawnEffect(base.gameObject, PantheraAssets.ShieldFX, this.ptraObj.modelTransform.position, 1, this.ptraObj.characterBody.gameObject, new Quaternion(), true);
            //this.shieldRenderer = this.shieldFX.GetComponentInChildren<Renderer>();
            //this.shieldRenderer.enabled = false;

            // Create the Leap Trail //
            this.leapTrailFXID = Utils.FXManager.SpawnEffect(base.gameObject, PantheraAssets.LeapTrailFX, ptraObj.modelTransform.position, ptraObj.actualModelScale, ptraObj.characterBody.gameObject, new Quaternion(), true, false);
            GameObject leapTrailEffect = Utils.FXManager.GetEffect(leapTrailFXID);
            this.leapTrailParticles = leapTrailEffect.GetComponentInChildren<ParticleSystem>();
            this.leapTrailEmission = leapTrailParticles.emission;
            this.leapTrailEmission.enabled = false;

            // Create the Dash Trail //
            //this.dashFXID = Utils.FXManager.SpawnEffect(base.gameObject, PantheraAssets.DashFX, ptraObj.modelTransform.position, ptraObj.modelScale, ptraObj.characterBody.gameObject, new Quaternion(), true, false);
            //GameObject dashEffect = Utils.FXManager.GetEffect(dashFXID);
            //this.dashParticles = dashEffect.GetComponentInChildren<ParticleSystem>();
            //this.dashEmission = dashParticles.emission;
            //this.dashEmission.enabled = false;

            // Create the Fury Aura //
            this.furyAuraFXID = Utils.FXManager.SpawnEffect(base.gameObject, PantheraAssets.FuryAuraFX, ptraObj.modelTransform.position, 1, ptraObj.characterBody.gameObject, new Quaternion(), true, false);
            this.furyAuraObj = Utils.FXManager.GetEffect(this.furyAuraFXID);
            foreach (ParticleSystem ps in this.furyAuraObj.GetComponentsInChildren<ParticleSystem>())
            {
                EmissionModule em = ps.emission;
                em.enabled = false;
            }

            // Create the Guardian Aura //
            this.GuardianAuraFXID = Utils.FXManager.SpawnEffect(base.gameObject, PantheraAssets.GuardianAuraFX, ptraObj.modelTransform.position, 1, ptraObj.characterBody.gameObject, new Quaternion(), true, false);
            this.GuardianAuraObj = Utils.FXManager.GetEffect(this.GuardianAuraFXID);
            foreach (ParticleSystem ps in this.GuardianAuraObj.GetComponentsInChildren<ParticleSystem>())
            {
                EmissionModule em = ps.emission;
                em.enabled = false;
            }

            // Create the Ambition Aura //
            this.AmbitionAuraFXID = Utils.FXManager.SpawnEffect(base.gameObject, PantheraAssets.AmbitionAuraFX, ptraObj.modelTransform.position, 1, ptraObj.characterBody.gameObject, new Quaternion(), true, false);
            this.AmbitionAuraObj = Utils.FXManager.GetEffect(this.AmbitionAuraFXID);
            foreach (ParticleSystem ps in this.AmbitionAuraObj.GetComponentsInChildren<ParticleSystem>())
            {
                EmissionModule em = ps.emission;
                em.enabled = false;
            }

        }

        public void FixedUpdate()
        {

        }

        public void setLeapTrailFX(bool state)
        {
            if (NetworkClient.active == false) return;
            new ServerSetLeapTrailFX(base.gameObject, state).Send(NetworkDestination.Server);
        }

        public void setDashFX(bool state)
        {
            if (NetworkClient.active == false) return;
            new ClientSetDashFX(base.gameObject, state).Send(NetworkDestination.Clients);
        }

        public void setStealthFX(bool state)
        {
            if (NetworkClient.active == false) return;
            new ServerSetStealthFX(base.gameObject, state).Send(NetworkDestination.Server);
        }

        public void setFuryAuraFX(bool state)
        {
            if (NetworkClient.active == false) return;
            new ServerSetFuryModeFX(base.gameObject, state).Send(NetworkDestination.Server);
        }

        public void setGuardianAuraFX(bool state)
        {
            if (NetworkClient.active == false) return;
            new ServerSetGuardianModeFX(base.gameObject, state).Send(NetworkDestination.Server);
        }

        public void setAmbitionAuraFX(bool state)
        {
            if (NetworkClient.active == false) return;
            new ServerSetAmbitionModeFX(base.gameObject, state).Send(NetworkDestination.Server);
        }

        public static void UpdateRendererMaterialsHook(Action<RoR2.CharacterModel, Renderer, Material, bool> orig, RoR2.CharacterModel self, Renderer renderer, Material defaultMaterial, bool ignoreOverlays)
        {
            if (self.visibility == VisibilityLevel.Visible)
                renderer.material = defaultMaterial;
            else
                renderer.material = PantheraAssets.skin1MatCloaked;
        }

    }
}
