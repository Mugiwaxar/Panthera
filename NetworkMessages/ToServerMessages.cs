using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using R2API;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using RoR2.Orbs;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.TextCore;
using static Panthera.Base.PantheraSkill;
using static Rewired.UI.ControlMapper.ControlMapper;
using static RoR2.DotController;

namespace Panthera.NetworkMessages
{

    class ServerSyncProfile : INetMessage
    {

        public GameObject player;
        public Dictionary<String, int> dico = new Dictionary<String, int>();

        public ServerSyncProfile()
        {

        }

        public ServerSyncProfile(GameObject player, Dictionary<string, int> dico)
        {
            this.player = player;
            this.dico = dico;
        }

        public void OnReceived()
        {
            if (this.player == null) return;
            PantheraObj ptraObj = player.GetComponent<PantheraObj>();
            if (ptraObj == null)
                return;
            if (ptraObj.hasAuthority() == true)
            {
                ptraObj.characterBody.RecalculateStats();
                return;
            }
            ProfileComponent profileComp = ptraObj.profileComponent;
            if (profileComp == null)
            {
                profileComp = this.player.AddComponent<ProfileComponent>();
                ptraObj.profileComponent = profileComp;
            }
            profileComp.deserialize(this.dico);
            ptraObj.characterBody.RecalculateStats();
            new ClientSyncProfile(this.player, this.dico).Send(NetworkDestination.Clients);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.player);
            writer.Write(this.dico.Count);
            foreach (KeyValuePair<String, int> entry in this.dico)
            {
                writer.Write(entry.Key);
                writer.Write(entry.Value);
            }

        }

        public void Deserialize(NetworkReader reader)
        {
            this.dico.Clear();
            this.player = reader.ReadGameObject();
            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                string key = reader.ReadString();
                int value = reader.ReadInt32();
                this.dico.Add(key, value);
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
            PantheraObj ptraObj = this.player.GetComponent<PantheraObj>();
            if (ptraObj == null) return;
            Transform modelTransform = ptraObj.modelTransform;
            ptraObj.modelScale = scale;
            ptraObj.transform.localScale = new Vector3(scale, scale, scale);
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

        public GameObject creator;
        public GameObject target;
        public int buffId;
        public int count;
        public float duration;
        public int maxStacks = 0;
        public bool isDebuff = false;

        public ServerAddBuff()
        {

        }

        public ServerAddBuff(GameObject creator, GameObject target, PantheraBuff ptraBuff, int count = 1, float duration = -1)
        {
            this.creator = creator;
            this.target = target;
            this.buffId = ptraBuff.index;
            this.count = count;
            this.duration = duration > 0 ? duration : ptraBuff.duration;
            this.maxStacks = ptraBuff.maxStacks;
            this.isDebuff = ptraBuff.isDebuff;
        }

        public void OnReceived()
        {
            if (this.target == null) return;
            CharacterBody body = target.GetComponent<CharacterBody>();
            if (body == null) return;
            int buffToAdd = this.count;
            if (isDebuff == false)
            {
                int actualBuffCount = body.GetBuffCount((BuffIndex)buffId);
                buffToAdd = Math.Min(this.maxStacks - actualBuffCount, this.count);
            }

            if (buffToAdd <= 0) return;
            for (int i = 0; i < buffToAdd; i++)
            {
                if (this.duration == 0) body.AddBuff((BuffIndex)this.buffId);
                else body.AddTimedBuff((BuffIndex)this.buffId, this.duration);
            }
            if (this.creator == null) return;
            PantheraObj ptraObj = creator.GetComponent<PantheraObj>();
            if (this.isDebuff == true && ptraObj != null)
            {
                this.target.GetComponent<PredatorComponent>().lastHit = ptraObj;
            }
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.creator);
            writer.Write(this.target);
            writer.Write(this.buffId);
            writer.Write(this.count);
            writer.Write(this.duration);
            writer.Write(this.maxStacks);
            writer.Write(this.isDebuff);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.creator = reader.ReadGameObject();
            this.target = reader.ReadGameObject();
            this.buffId = reader.ReadInt32();
            this.count = reader.ReadInt32();
            this.duration = reader.ReadSingle();
            this.maxStacks = reader.ReadInt32();
            this.isDebuff = reader.ReadBoolean();
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

        public ServerSetBuffCount(GameObject player, int buffId, int buffCount)
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

    class ServerClearTimedBuffs : INetMessage
    {

        public GameObject player;
        public int buffId;

        public ServerClearTimedBuffs()
        {

        }

        public ServerClearTimedBuffs(GameObject player, int buffId)
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

    class ServerRemoveBuff : INetMessage
    {

        public GameObject player;
        public int buffId;
        public int count;
        public bool timed;

        public ServerRemoveBuff()
        {

        }

        public ServerRemoveBuff(GameObject player, int buffId, int count, bool timed = false)
        {
            this.player = player;
            this.buffId = buffId;
            this.count = count;
            this.timed = timed;
        }

        public void OnReceived()
        {
            CharacterBody body = this.player.GetComponent<CharacterBody>();
            if (body == null) return;
            for (int i = 0; i < this.count; i++)
            {
                if (this.timed == false)
                    body.RemoveBuff((BuffIndex)this.buffId);
                else
                    body.RemoveOldestTimedBuff((BuffIndex)this.buffId);
            }
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.player);
            writer.Write(this.buffId);
            writer.Write(this.count);
            writer.Write(this.timed);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.player = reader.ReadGameObject();
            this.buffId = reader.ReadInt32();
            this.count = reader.ReadInt32();
            this.timed = reader.ReadBoolean();
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

    class ServerSetGodMode : INetMessage
    {
        public GameObject player;
        public bool setValue;

        public ServerSetGodMode()
        {

        }

        public ServerSetGodMode(GameObject player, bool setValue)
        {
            this.player = player;
            this.setValue = setValue;
        }

        public void OnReceived()
        {
            if (this.player == null) return;
            HealthComponent hc = this.player.GetComponent<HealthComponent>();
            if (hc == null) return;
            hc.godMode = setValue;
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.player);
            writer.Write(this.setValue);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.player = reader.ReadGameObject();
            this.setValue = reader.ReadBoolean();
        }
    }

    class ServerInflictDamage : INetMessage
    {

        GameObject attacker;
        GameObject target;
        Vector3 position;
        float damage;
        float procCoefficient;
        bool isCrit;
        uint damageType;
        byte damageColor;

        public ServerInflictDamage()
        {

        }

        public ServerInflictDamage(GameObject attacker, GameObject target, Vector3 position, float damage, bool isCrit = false, float procCoefficient = 1, DamageType damageType = DamageType.Generic, DamageColorIndex damageColor = DamageColorIndex.Default)
        {
            this.attacker = attacker;
            this.target = target;
            this.position = position;
            this.damage = damage;
            this.procCoefficient = procCoefficient;
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
            damageInfo.procCoefficient = this.procCoefficient;
            hc.TakeDamage(damageInfo);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.attacker);
            writer.Write(this.target);
            writer.Write(this.position);
            writer.Write(this.damage);
            writer.Write(this.procCoefficient);
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
            this.procCoefficient = reader.ReadSingle();
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
            body.AddTimedBuff(PantheraConfig.WeakDebuffDef.buffIndex, this.duration);
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

    class ServerAddGold : INetMessage
    {
        GameObject player;
        int amount;

        public ServerAddGold()
        {

        }

        public ServerAddGold(GameObject player, int amount)
        {
            this.player = player;
            this.amount = amount;
        }

        public void OnReceived()
        {
            if (this.player == null) return;
            CharacterBody body = this.player.GetComponent<CharacterBody>();
            if (body == null) return;
            CharacterMaster master = body.master;
            if (master == null) return;
            master.GiveMoney((uint)amount);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.player);
            writer.Write(this.amount);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.player = reader.ReadGameObject();
            this.amount = reader.ReadInt32();
        }
    }

    class ServerApplyForceToBody : INetMessage
    {

        public GameObject target;
        public Vector3 force;

        public ServerApplyForceToBody()
        {

        }

        public ServerApplyForceToBody(GameObject target, Vector3 force)
        {
            this.target = target;
            this.force = force;
        }

        public void OnReceived()
        {
            if (this.target == null) return;
            CharacterMotor characterMotor = this.target.GetComponent<CharacterMotor>();
            if (characterMotor)
            {
                PhysForceInfo physForceInfo = PhysForceInfo.Create();
                physForceInfo.force = force;
                physForceInfo.ignoreGroundStick = true;
                physForceInfo.disableAirControlUntilCollision = true;
                physForceInfo.massIsOne = true;
                characterMotor.ApplyForceImpulse(physForceInfo);
            }
            Rigidbody rigidBody = this.target.GetComponent<Rigidbody>();
            if (rigidBody)
            {
                rigidBody.AddForce(force, ForceMode.Impulse);
            }
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.target);
            writer.Write(this.force);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.target = reader.ReadGameObject();
            this.force = reader.ReadVector3();
        }

    }

    public class ServerSetPortalSurge : INetMessage
    {

        public GameObject player;
        public GameObject portal;
        public bool set;

        public ServerSetPortalSurge()
        {

        }

        public ServerSetPortalSurge(GameObject target, GameObject portal, bool set)
        {
            this.player = target;
            this.portal = portal;
            this.set = set;
        }

        public void OnReceived()
        {
            if (this.set == true)
                this.portal.GetComponent<TeleporterInteraction>().OnInteractionBegin(player.GetComponent<Interactor>());
            Base.PantheraCombatDirector.SetSurged(this.portal, this.set);
            new ClientSetPortalSurge(this.player, this.portal, this.set).Send(NetworkDestination.Clients);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.player);
            writer.Write(this.portal);
            writer.Write(this.set);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.player = reader.ReadGameObject();
            this.portal = reader.ReadGameObject();
            this.set = reader.ReadBoolean();
        }

    }

}
