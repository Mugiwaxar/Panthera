using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.MachineScripts;
using Panthera.Skills;
using Panthera.SkillsHybrid;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Panthera.NetworkMessages
{

    public class ClientSetFrontShield : INetMessage
    {

        GameObject character;
        bool set;

        public ClientSetFrontShield()
        {

        }

        public ClientSetFrontShield(GameObject character, bool set)
        {
            this.character = character;
            this.set = set;
        }

        public void OnReceived()
        {
            if (this.character == null || Util.HasEffectiveAuthority(this.character) == true) return;
            PantheraObj obj = this.character.GetComponent<PantheraObj>();
            if (obj == null) return;
            obj.frontShield.SetActive(this.set);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.character);
            writer.Write(this.set);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.character = reader.ReadGameObject();
            this.set = reader.ReadBoolean();
        }

    }


    public class ServerSetFrontShield : INetMessage
    {

        GameObject character;
        bool set;

        public ServerSetFrontShield()
        {

        }

        public ServerSetFrontShield(GameObject character, bool set)
        {
            this.character = character;
            this.set = set;
        }

        public void OnReceived()
        {
            if (this.character == null) return;
            PantheraObj obj = this.character.GetComponent<PantheraObj>();
            if (obj == null) return;
            obj.frontShield.SetActive(this.set);
            new ClientSetFrontShield(this.character, this.set).Send(NetworkDestination.Clients);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.character);
            writer.Write(this.set);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.character = reader.ReadGameObject();
            this.set = reader.ReadBoolean();
        }

    }

    public class ClientShieldDamage : INetMessage
    {

        GameObject character;
        float damage;

        public ClientShieldDamage()
        {

        }

        public ClientShieldDamage(GameObject character, float damage)
        {
            this.character = character;
            this.damage = damage;
        }

        public void OnReceived()
        {
            if (this.character == null || Util.HasEffectiveAuthority(this.character) == false) return;
            PantheraObj ptraObj = this.character.GetComponent<PantheraObj>();
            if (ptraObj == null) return;
            PantheraBody body = this.character.GetComponent<PantheraObj>().characterBody;
            if (body == null) return;
            if (ptraObj.getSkillMachine2SciptType() !=  typeof(ShieldBash))
                this.character.GetComponent<PantheraObj>().characterBody.shield -= this.damage * ptraObj.activePreset.frontShield_damageDecreaseMultiplier;
            BigCatPassive bcp = ptraObj.getPassiveScript();
            if (bcp == null) return;
            bcp.lastShieldDamageTime = Time.time;
            if (body.shield <= 0)
            {
                bcp.destroyedShieldTime = Time.time;
                Utils.Sound.playSound(Utils.Sound.FrontShieldBreak, this.character);
                ptraObj.frontShield.SetActive(false);
            }
            SkillsPassive.ShieldOfPower.ApplyAbility(ptraObj);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.character);
            writer.Write(this.damage);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.character = reader.ReadGameObject();
            this.damage = reader.ReadSingle();
        }

    }

    public class ServerSendFrontShield : INetMessage
    {

        GameObject character;
        float shield;

        public ServerSendFrontShield()
        {

        }

        public ServerSendFrontShield(GameObject character, float maxShield)
        {
            this.character = character;
            this.shield = maxShield;
        }

        public void OnReceived()
        {
            if (this.character == null) return;
            PantheraHealthComponent phc = this.character.GetComponent<PantheraHealthComponent>();
            if (phc == null) return;
            phc.frontShield = this.shield;
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.character);
            writer.Write(this.shield);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.character = reader.ReadGameObject();
            this.shield = reader.ReadSingle();
        }

    }

}
