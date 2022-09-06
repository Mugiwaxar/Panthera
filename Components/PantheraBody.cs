using Panthera.Components;
using Panthera.GUI;
using Panthera.NetworkMessages;
using Panthera.Skills;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Panthera.Components
{

    class PantheraBody : CharacterBody
    {

		public PantheraObj ptraObj;

        public float _energy;
        public float _power;
        public float _fury;
        public float _comboPoint;
        public float _shield;
		public float energy
		{ 
			set
            {
				this._energy = value;
				if (this._energy > ptraObj.activePreset.maxEnergy)
					this._energy = ptraObj.activePreset.maxEnergy;
				if (this._energy < 0)
					this._energy = 0;

            }
			get
            {
				return this._energy;
            }
		}
		public float power
		{
			set
			{
				this._power = value;
				if (this._power > ptraObj.activePreset.maxPower)
					this._power = ptraObj.activePreset.maxPower;
				if (this._power < 0)
					this._power = 0;

			}
			get
			{
				return this._power;
			}
		}
		public float fury
		{
			set
			{
				this._fury = value;
				if (this._fury > ptraObj.activePreset.maxFury)
					this._fury = ptraObj.activePreset.maxFury;
				if (this._fury < 0)
					this._fury = 0;

			}
			get
			{
				return this._fury;
			}
		}
		public float comboPoint
		{
			set
			{
				this._comboPoint = value;
				if (this._comboPoint > ptraObj.activePreset.maxComboPoint)
					this._comboPoint = ptraObj.activePreset.maxComboPoint;
				if (this._comboPoint < 0)
					this._comboPoint = 0;

			}
			get
			{
				return this._comboPoint;
			}
		}
		public float shield
		{
			set
			{
				this._shield = value;
				if (this._shield > ptraObj.activePreset.maxShield)
					this._shield = ptraObj.activePreset.maxShield;
				if (this._shield < 0)
					this._shield = 0;
                if (NetworkServer.active == false) new ServerSendFrontShield(this.gameObject, this.shield).Send(NetworkDestination.Server);
            }
			get
			{
				if (this._shield > ptraObj.activePreset.maxShield)
					this._shield = ptraObj.activePreset.maxShield;

                return this._shield;
			}
		}

		public void DoInit()
		{
			this.ptraObj = base.GetComponent<PantheraObj>();
			if (NetworkClient.active == false) return;
			this.energy = ptraObj.activePreset.maxEnergy;
			this.power = 0;
			this.fury = 0;
			this.comboPoint = 0;
			this.shield = ptraObj.activePreset.maxShield;
		}

		public VisibilityLevel GetVisibilityLevel(On.RoR2.CharacterBody.orig_GetVisibilityLevel_TeamIndex orig, CharacterBody self, TeamIndex observerTeam)
        {
			if (self is PantheraBody == false) return orig(self, observerTeam);
			if (this.ptraObj == null) return VisibilityLevel.Visible;
			if (this.HasBuff(Buff.stealBuff) == true)
            {
				return VisibilityLevel.Invisible;
			}
			if (this.hasCloakBuff == false)
			{
				return VisibilityLevel.Visible;
			}
			if (this.teamComponent.teamIndex != observerTeam)
			{
				return VisibilityLevel.Cloaked;
			}
			return VisibilityLevel.Revealed;
		}

    }
}
