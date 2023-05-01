using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using R2API;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using static Panthera.Base.PantheraSkill;
using static Rewired.UI.ControlMapper.ControlMapper;
using static RoR2.DotController;

namespace Panthera.NetworkMessages
{

    class ServerSyncPreset : INetMessage
    {

        public GameObject character;
        public Dictionary<int, int> abilitiesList;

        public ServerSyncPreset()
        {

        }

        public ServerSyncPreset(GameObject character, Dictionary<int, int> abilitiesList)
        {
            this.character = character;
            this.abilitiesList = abilitiesList;
        }

        public void OnReceived()
        {
            if (this.character == null) return;
            PantheraObj ptraObj = character.GetComponent<PantheraObj>();
            if (ptraObj == null) return;
            ptraObj.activePreset = new Preset();
            ptraObj.activePreset.ptraObj = ptraObj;
            ptraObj.activePreset.unlockedAbilitiesList = abilitiesList;
            ptraObj.applyStats();
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.character);
            writer.Write(abilitiesList.Count);
            foreach (KeyValuePair<int, int> entry in abilitiesList)
            {
                writer.Write(entry.Key);
                writer.Write(entry.Value);
            }
        }

        public void Deserialize(NetworkReader reader)
        {
            this.character = reader.ReadGameObject();
            abilitiesList = new Dictionary<int, int>();
            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                int key = reader.ReadInt32();
                int value = reader.ReadInt32();
                abilitiesList[key] = value;
            }
        }

    }

    class ServerChangePantheraScale : INetMessage
    {

        GameObject player;
        float scale;

        public ServerChangePantheraScale()
        {

        }

        public ServerChangePantheraScale(GameObject player, float scale)
        {
            this.player = player;
            this.scale = scale;
        }

        public void OnReceived()
        {
            PantheraObj ptraobj = this.player.GetComponent<PantheraObj>();
            if (ptraobj == null) return;
            Transform modelTransform = ptraobj.modelTransform;
            ptraobj.modelScale = scale;
            ptraobj.transform.localScale = new Vector3(scale, scale, scale);
            if (modelTransform == null) return;
            modelTransform.localScale = new Vector3(scale, scale, scale);
            new ClientChangePantheraScale(this.player, scale).Send(NetworkDestination.Clients);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.player);
            writer.Write(this.scale);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.player = reader.ReadGameObject();
            this.scale = reader.ReadSingle();
        }

    }

    class ServerAddBuff : INetMessage
    {

        public GameObject player;
        public int buffId;
        public float duration;

        public ServerAddBuff()
        {

        }

        public ServerAddBuff(GameObject player, int buffId, float duration = 0)
        {
            this.player = player;
            this.buffId = buffId;
            this.duration = duration;
        }

        public void OnReceived()
        {
            if (this.player == null) return;
            CharacterBody body = player.GetComponent<CharacterBody>();
            if (body == null) return;
            if (duration == 0) body.AddBuff((BuffIndex)this.buffId);
            else body.AddTimedBuff((BuffIndex)this.buffId, this.duration);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.player);
            writer.Write(this.buffId);
            writer.Write(this.duration);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.player = reader.ReadGameObject();
            this.buffId = reader.ReadInt32();
            this.duration = reader.ReadSingle();
        }

    }

    class ServerSetBuffCount : INetMessage
    {

        public GameObject player;
        public int buffId;
        public int buffCount;

        public ServerSetBuffCount()
        {

        }

        public ServerSetBuffCount(GameObject player,int buffId, int buffCount)
        {
            this.player = player;
            this.buffId = buffId;
            this.buffCount = buffCount;
        }

        public void OnReceived()
        {
            if (this.player == null) return;
            CharacterBody body = player.GetComponent<CharacterBody>();
            if (body == null) return;
            body.SetBuffCount((BuffIndex)this.buffId, this.buffCount);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.player);
            writer.Write(this.buffId);
            writer.Write(this.buffCount);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.player = reader.ReadGameObject();
            this.buffId = reader.ReadInt32();
            this.buffCount = reader.ReadInt32();
        }

    }

    class ServerRemoveBuff : INetMessage
    {

        public GameObject player;
        public int buffId;

        public ServerRemoveBuff()
        {

        }

        public ServerRemoveBuff(GameObject player, int buffId)
        {
            this.player = player;
            this.buffId = buffId;
        }

        public void OnReceived()
        {
            if (this.player == null) return;
            CharacterBody body = player.GetComponent<CharacterBody>();
            if (body == null) return;
            body.RemoveBuff((BuffIndex)this.buffId);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.player);
            writer.Write(this.buffId);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.player = reader.ReadGameObject();
            this.buffId = reader.ReadInt32();
        }

    }

    class ServerClearBuffs : INetMessage
    {

        public GameObject player;
        public int buffId;

        public ServerClearBuffs()
        {
            
        }

        public ServerClearBuffs(GameObject player, int buffId)
        {
            this.player = player;
            this.buffId = buffId;
        }

        public void OnReceived()
        {
            if (this.player == null) return;
            CharacterBody body = player.GetComponent<CharacterBody>();
            if (body == null) return;
            body.ClearTimedBuffs((BuffIndex)this.buffId);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.player);
            writer.Write(this.buffId);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.player = reader.ReadGameObject();
            this.buffId = reader.ReadInt32();
        }

    }

    class ServerHeal : INetMessage
    {

        public GameObject player;
        public float amount;

        public ServerHeal()
        {

        }

        public ServerHeal(GameObject player, float amount)
        {
            this.player = player;
            this.amount = amount;
        }

        public void OnReceived()
        {
            if (player == null) return;
            HealthComponent hc = player.GetComponent<HealthComponent>();
            if (hc == null) return;
            hc.Heal(amount, default(ProcChainMask));
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.player);
            writer.Write(this.amount);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.player = reader.ReadGameObject();
            this.amount = reader.ReadSingle();
        }

    }

    class ServerInflictDamage : INetMessage
    {

        GameObject attacker;
        GameObject target;
        Vector3 position;
        float damage;
        bool isCrit;
        uint damageType;
        byte damageColor;

        public ServerInflictDamage()
        {

        }

        public ServerInflictDamage(GameObject attacker, GameObject target, Vector3 position, float damage, bool isCrit = false, DamageType damageType = DamageType.Generic, DamageColorIndex damageColor = DamageColorIndex.Default)
        {
            this.attacker = attacker;
            this.target = target;
            this.position = position;
            this.damage = damage;
            this.isCrit = isCrit;
            this.damageType = (uint)damageType;
            this.damageColor = (byte)damageColor;
        }

        public void OnReceived()
        {
            if (this.attacker == null || this.target == null) return;
            HealthComponent hc = this.target.GetComponent<HealthComponent>();
            if (hc == null) return;
            DamageInfo damageInfo = new DamageInfo();
            damageInfo.damage = this.damage;
            damageInfo.crit = this.isCrit;
            damageInfo.inflictor = this.attacker;
            damageInfo.attacker = this.attacker;
            damageInfo.position = this.position;
            damageInfo.damageType = (DamageType)this.damageType;
            damageInfo.damageColorIndex = (DamageColorIndex)this.damageColor;
            damageInfo.force = Vector3.zero;
            damageInfo.procChainMask = default(ProcChainMask);
            damageInfo.procCoefficient = 1;
            hc.TakeDamage(damageInfo);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.attacker);
            writer.Write(this.target);
            writer.Write(this.position);
            writer.Write(this.damage);
            writer.Write(this.isCrit);
            writer.Write(this.damageType);
            writer.Write(this.damageColor);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.attacker = reader.ReadGameObject();
            this.target = reader.ReadGameObject();
            this.position = reader.ReadVector3();
            this.damage = reader.ReadSingle();
            this.isCrit = reader.ReadBoolean();
            this.damageType = reader.ReadUInt32();
            this.damageColor = reader.ReadByte();
        }

    }

    class ServerInflictDot : INetMessage
    {

        public GameObject attacker;
        public GameObject target;
        public int dotIndex;
        public float duration;
        public float damageMultiplier;

        public ServerInflictDot()
        {

        }

        public ServerInflictDot(GameObject attacker, GameObject target, DotIndex dotIndex, float duration = 1, float damageMultiplier = 1)
        {
            this.attacker = attacker;
            this.target = target;
            this.dotIndex = (int)dotIndex;
            this.duration = duration;
            this.damageMultiplier = damageMultiplier;
        }

        public void OnReceived()
        {
            if (attacker == null || target == null) return;
            InflictDotInfo dotInfo = new InflictDotInfo();
            dotInfo.attackerObject = this.attacker;
            dotInfo.victimObject = this.target;
            dotInfo.dotIndex = (DotIndex)dotIndex;
            dotInfo.duration = duration;
            dotInfo.damageMultiplier = damageMultiplier;
            DotController.InflictDot(ref dotInfo);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.attacker);
            writer.Write(this.target);
            writer.Write(this.dotIndex);
            writer.Write(this.duration);
            writer.Write(this.damageMultiplier);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.attacker = reader.ReadGameObject();
            this.target = reader.ReadGameObject();
            this.dotIndex = reader.ReadInt32();
            this.duration = reader.ReadSingle();
            this.damageMultiplier = reader.ReadSingle();
        }
    }

    class ServerStunTarget : INetMessage
    {

        public GameObject target;
        public float duration;

        public ServerStunTarget()
        {

        }

        public ServerStunTarget(GameObject target, float duration)
        {
            this.target = target;
            this.duration = duration;
        }

        public void OnReceived()
        {
            if (this.target == null) return;
            SetStateOnHurt setState = this.target.GetComponent<SetStateOnHurt>();
            if (setState == null) return;
            setState.SetStun(this.duration);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.target);
            writer.Write(this.duration);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.target = reader.ReadGameObject();
            this.duration = reader.ReadSingle();
        }

    }

    class ServerApplyWeak : INetMessage
    {

        public GameObject target;
        public float duration;

        public ServerApplyWeak()
        {

        }

        public ServerApplyWeak(GameObject target, float duration)
        {
            this.target = target;
            this.duration = duration;
        }

        public void OnReceived()
        {
            if (this.target == null) return;
            CharacterBody body = this.target.GetComponent<CharacterBody>();
            if (body == null) return;
            body.AddTimedBuff(PantheraConfig.WeakDebuffDef.buffIndex, PantheraConfig.Rip_weakDuration);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.target);
            writer.Write(this.duration);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.target = reader.ReadGameObject();
            this.duration = reader.ReadSingle();
        }
    }

    class ServerSetElite : INetMessage
    {

        GameObject target;
        bool setValue;

        public ServerSetElite()
        {

        }

        public ServerSetElite(GameObject target, bool setValue)
        {
            this.target = target;
            this.setValue = setValue;
        }

        

        public void OnReceived()
        {
            if (this.target == null) return;
            CharacterBody body = this.target.GetComponent<CharacterBody>();
            if (body == null) return;
            body.isElite = setValue;
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.target);
            writer.Write(this.setValue);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.target = reader.ReadGameObject();
            this.setValue = reader.ReadBoolean();
        }

    }

    class ServerRespawn : INetMessage
    {

        GameObject target;
        Vector3 position;
        Quaternion rotation;

        public ServerRespawn()
        {

        }

        public ServerRespawn(GameObject target, Vector3 position, Quaternion rotation)
        {
            this.target = target;
            this.position = position;
            this.rotation = rotation;
        }

        public void OnReceived()
        {
            if (target == null) return;
            CharacterMaster master = this.target.GetComponent<CharacterMaster>();
            if (master == null) return;
            master.Respawn(this.position, this.rotation);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.target);
            writer.Write(this.position);
            writer.Write(this.rotation);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.target = reader.ReadGameObject();
            this.position = reader.ReadVector3();
            this.rotation = reader.ReadQuaternion();
        }

    }

}
