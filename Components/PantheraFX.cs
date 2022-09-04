using Panthera.NetworkMessages;
using R2API.Networking;
using R2API.Networking.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.ParticleSystem;

namespace Panthera.Components
{
    class PantheraFX : MonoBehaviour
    {

        public PantheraObj ptraObj;
        private GameObject shieldFX;
        private Renderer shieldRenderer;
        private GameObject leapTrailFX;
        private ParticleSystem leapTrailParticles;
        private EmissionModule leapTrailEmission;
        private GameObject superSprintFX;
        private ParticleSystem superSprintParticles;
        private EmissionModule superSprintEmission;

        public void Init()
        {

            // Get the Panthera Object //
            this.ptraObj = base.GetComponent<PantheraObj>();

            // Don't need FX for the server //
            if (NetworkClient.active == false)
            {
                this.enabled = false;
                return;
            }

            // Spawn the shield //
            this.shieldFX = Utils.Functions.SpawnEffect(base.gameObject, Assets.ShieldFX, this.ptraObj.modelTransform.position, 1, this.ptraObj.modelTransform.gameObject);
            this.shieldRenderer = this.shieldFX.GetComponentInChildren<Renderer>();
            this.shieldRenderer.enabled = false;

            // Spawn the leap trail //
            this.leapTrailFX = Utils.Functions.SpawnEffect(base.gameObject, Assets.LeapTrailFX, this.ptraObj.modelTransform.position, 1, this.ptraObj.modelTransform.gameObject);
            this.leapTrailParticles = this.leapTrailFX.GetComponentInChildren<ParticleSystem>();
            this.leapTrailEmission = this.leapTrailParticles.emission;
            this.leapTrailEmission.enabled = false;
            
            // Spawn the super sprint trail //
            this.superSprintFX = Utils.Functions.SpawnEffect(base.gameObject, Assets.SuperSprintFX, this.ptraObj.modelTransform.position, 1, this.ptraObj.modelTransform.gameObject);
            this.superSprintParticles = this.superSprintFX.GetComponentInChildren<ParticleSystem>();
            this.superSprintEmission = this.superSprintParticles.emission;
            this.superSprintEmission.enabled = false;

        }

        public void SetShieldFX(bool state)
        {
            if (NetworkClient.active == false || this.shieldRenderer.enabled == state) return;
            this.shieldRenderer.enabled = state;
            new ClientSetShieldFX(this.gameObject, state).Send(NetworkDestination.Clients);
        }

        public void ServerSetShieldFX(bool state)
        {
            if (this.ptraObj.HasAuthority() == true) return;
            this.shieldRenderer.enabled = state;
        }

        public void SetLeapTrailFX(bool state)
        {
            if (NetworkClient.active == false || this.leapTrailEmission.enabled == state) return;
            this.leapTrailEmission.enabled = state;
            new ClientSetLeapTrailFX(this.gameObject, state).Send(NetworkDestination.Clients);
        }

        public void ServerSetLeapTrailFX(bool state)
        {
            if (this.ptraObj.HasAuthority() == true) return;
            this.leapTrailEmission.enabled = state;
        }

        public void SetSuperSprintFX(bool state)
        {
            if (NetworkClient.active == false || this.superSprintEmission.enabled == state) return;
            this.superSprintEmission.enabled = state;
            new ClientSetSuperSprintFX(this.gameObject, state).Send(NetworkDestination.Clients);
        }

        public void ServerSetSuperSprintFX(bool state)
        {
            if (this.ptraObj.HasAuthority() == true) return;
            this.superSprintEmission.enabled = state;
        }

        public void SetStealFX(bool state)
        {
            if (NetworkClient.active == false) return;
            if (state == true) this.ptraObj.FindModelChild("Body").gameObject.GetComponent<SkinnedMeshRenderer>().material = Assets.StealMaterial;
            else this.ptraObj.FindModelChild("Body").gameObject.GetComponent<SkinnedMeshRenderer>().material = Assets.NormalMaterial;
            new ClientSetStealFX(this.gameObject, state).Send(NetworkDestination.Clients);
        }

        public void ServerSetStealFX(bool state)
        {
            if (this.ptraObj.HasAuthority() == true) return;
            if (state == true) this.ptraObj.FindModelChild("Body").gameObject.GetComponent<SkinnedMeshRenderer>().material = Assets.StealMaterial;
            else this.ptraObj.FindModelChild("Body").gameObject.GetComponent<SkinnedMeshRenderer>().material = Assets.NormalMaterial;
        }

        public void SetFadeLevel(float level)
        {
            if (this.ptraObj.GetPassiveScript().stealed == true)
                level = 1f / 255f * 73f;
            SkinnedMeshRenderer redenrer = this.ptraObj.FindModelChild("Body").gameObject.GetComponent<SkinnedMeshRenderer>();
            if (level >= 1)
            {
                redenrer.material = Assets.NormalMaterial;
            }
            else
            {
                redenrer.material = this.ptraObj.FindModelChild("Body").gameObject.GetComponent<SkinnedMeshRenderer>().material = Assets.StealMaterial;
                Color color = new Color(1, 1, 1, level);
                redenrer.material.SetColor("_Color", color);
            }
        }

    }
}
