using EntityStates;
using Panthera.Base;
using Panthera.GUI;
using Panthera.NetworkMessages;
using R2API;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using RoR2.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Panthera.Utils
{
    public class Functions
    {

        public static T CopyComponent<T>(T original, GameObject destination) where T : Component
        {
            System.Type type = original.GetType();
            Component copy = destination.AddComponent(type);
            System.Reflection.FieldInfo[] fields = type.GetFields();
            foreach (System.Reflection.FieldInfo field in fields)
            {
                field.SetValue(copy, field.GetValue(original));
            }
            return copy as T;
        }

        public static void CreateHitbox(GameObject prefab, Transform hitboxTransform, string hitboxName)
        {
            HitBoxGroup hitBoxGroup = prefab.AddComponent<HitBoxGroup>();

            HitBox hitBox = hitboxTransform.gameObject.AddComponent<HitBox>();
            hitboxTransform.gameObject.layer = LayerIndex.projectile.intVal;

            hitBoxGroup.hitBoxes = new HitBox[]
            {
                hitBox
            };

            hitBoxGroup.groupName = hitboxName;
        }

        public static OverlapAttack CreateOverlapAttack(GameObject attacker, float damage, bool isCrit, string hitBoxName, Vector3 forceVector = new Vector3(), GameObject hitEffect = null)
        {
            // Get the HitBox //
            HitBoxGroup hitBoxGroup = null;
            Transform modelTransform = attacker.GetComponent<ModelLocator>().modelTransform;
            if (modelTransform)
            {
                hitBoxGroup = Array.Find<HitBoxGroup>(modelTransform.GetComponents<HitBoxGroup>(), (HitBoxGroup element) => element.groupName == hitBoxName);
            }

            // Get the Body //
            CharacterBody body = attacker.GetComponent<CharacterBody>();

            // Create the Attack //
            OverlapAttack attack = new OverlapAttack();
            attack.damageType = PantheraConfig.Rip_damageType;
            attack.attacker = attacker;
            attack.inflictor = attacker;
            attack.teamIndex = TeamComponent.GetObjectTeam(attacker);
            attack.damage = damage;
            attack.procCoefficient = PantheraConfig.Rip_procCoefficient;
            attack.hitEffectPrefab = hitEffect;
            attack.forceVector = forceVector;
            attack.pushAwayForce = PantheraConfig.Rip_pushForce;
            attack.hitBoxGroup = hitBoxGroup;
            attack.isCrit = isCrit;
            //attack.impactSound = hitSound;

            return attack;

        }

        public static BlastAttack CreateBlastAttack(GameObject attacker, float damage, BlastAttack.FalloffModel falloffModel, bool isCrit, Vector3 position, float radius, Vector3 forceVector = new Vector3(), GameObject hitEffect = null)
        {

            // Get the Body //
            CharacterBody body = attacker.GetComponent<CharacterBody>();

            // Create the Attack //
            BlastAttack attack = new BlastAttack();
            attack.damageType = PantheraConfig.Rip_damageType;
            attack.attacker = attacker;
            attack.inflictor = attacker;
            attack.position = position;
            attack.teamIndex = TeamComponent.GetObjectTeam(attacker);
            attack.baseDamage = damage;
            attack.falloffModel = falloffModel;
            attack.procCoefficient = PantheraConfig.Rip_procCoefficient;
            //attack.hitEffectPrefab = hitEffect;
            attack.bonusForce = forceVector;
            attack.baseForce = PantheraConfig.Rip_pushForce;
            attack.radius = radius;
            attack.crit = isCrit;
            //attack.impactSound = hitSound;

            return attack;

        }

        public static DamageInfo CreateDotDamageInfo(PantheraBuff buff, GameObject inflictor, GameObject victime, float damage = 0)
        {
            DamageInfo damageInfo = new DamageInfo();
            damageInfo.damage = damage == 0 ? buff.damage : damage;
            damageInfo.crit = false;
            damageInfo.inflictor = inflictor;
            damageInfo.attacker = inflictor;
            damageInfo.position = victime.transform.position;
            damageInfo.damageType = DamageType.DoT;
            damageInfo.damageColorIndex = DamageColorIndex.Bleed;
            damageInfo.force = Vector3.zero;
            damageInfo.procChainMask = default(ProcChainMask);
            damageInfo.procCoefficient = 1;
            return damageInfo;
        }

        public static float GetCollideDistance(Rigidbody r1, Rigidbody r2)
        {

            if (r1 == null || r2 == null) 
                return 999999;

            Collider c1 = r1.GetComponent<Collider>();
            Collider c2 = r2.GetComponent<Collider>();

            if (c1 == null || c2 == null) return 999999;

            Vector3 vcp1 = c1.ClosestPoint(r2.position);
            Vector3 vcp2 = c2.ClosestPoint(r1.position);

            return Vector3.Distance(vcp1, vcp2);

        }

        public static float GetCollideDistance(CharacterBody b1, CharacterBody b2)
        {

            if (b1 == null || b2 == null)
                return 999999;

            Collider c1 = b1.GetComponent<Collider>();
            Collider c2 = b2.GetComponent<Collider>();

            if (c1 == null || c2 == null) return 999999;

            Vector3 vcp1 = c1.ClosestPoint(b2.corePosition);
            Vector3 vcp2 = c2.ClosestPoint(b1.corePosition);

            return Vector3.Distance(vcp1, vcp2);

        }

        public static Sprite KeyEnumToSprite(KeysBinder.KeysEnum keyEnum)
        {
            if (keyEnum == KeysBinder.KeysEnum.Interact) return Assets.XButtonIcon;
            else if (keyEnum == KeysBinder.KeysEnum.Equipment) return Assets.YButtonIcon;
            else if (keyEnum == KeysBinder.KeysEnum.Sprint) return Assets.LButtonIcon;
            else if (keyEnum == KeysBinder.KeysEnum.Info) return Assets.InfoButtonIcon;
            else if (keyEnum == KeysBinder.KeysEnum.Ping) return Assets.RButtonIcon;
            else if (keyEnum == KeysBinder.KeysEnum.Forward) return Assets.LUpButtonIcon;
            else if (keyEnum == KeysBinder.KeysEnum.Backward) return Assets.LDownButtonIcon;
            else if (keyEnum == KeysBinder.KeysEnum.Left) return Assets.LLeftButtonIcon;
            else if (keyEnum == KeysBinder.KeysEnum.Right) return Assets.LRightButtonIcon;
            else if (keyEnum == KeysBinder.KeysEnum.Jump) return Assets.AButtonIcon;
            else if (keyEnum == KeysBinder.KeysEnum.Skill1) return Assets.RTButtonIcon;
            else if (keyEnum == KeysBinder.KeysEnum.Skill2) return Assets.LTButtonIcon;
            else if (keyEnum == KeysBinder.KeysEnum.Skill3) return Assets.LBButtonIcon;
            else if (keyEnum == KeysBinder.KeysEnum.Skill4) return Assets.RBButtonIcon;
            else if (keyEnum == KeysBinder.KeysEnum.Ability1) return Assets.UpArrowButtonIcon;
            else if (keyEnum == KeysBinder.KeysEnum.Ability2) return Assets.RightArrowButtonIcon;
            else if (keyEnum == KeysBinder.KeysEnum.Ability3) return Assets.DownArrowButtonIcon;
            else if (keyEnum == KeysBinder.KeysEnum.Ability4) return Assets.LeftArrowButtonIcon;
            else if (keyEnum == KeysBinder.KeysEnum.SpellsMode) return Assets.BButtonIcon;
            return null;
        }

        public static bool IsSinglePlayer()
        {
            return RoR2Application.isInSinglePlayer;
        }

        public static bool IsMultiplayer()
        {
            return RoR2Application.isInMultiPlayer;
        }

        public static bool IsHost()
        {
            if (RoR2Application.isInMultiPlayer == true && NetworkClient.active == true && NetworkServer.active == true)
                return true;
            return false;
        }

        public static bool IsClient()
        {
            if (RoR2Application.isInMultiPlayer == true && NetworkClient.active == true && NetworkServer.active == false)
                return true;
            return false;
        }

        public static bool IsServer()
        {
            if (NetworkClient.active == false && NetworkServer.active == true) return true;
            return false;
        }

        public static bool IsSingleOrHost()
        {
            if (NetworkClient.active == true && NetworkServer.active == true) return true;
            return false;
        }

        public static bool IsHostOrClient()
        {
            if (NetworkClient.active == true && RoR2Application.isInMultiPlayer == true) return true;
            return false;
        }

        public static void ToOpaqueMode(Material material)
        {
            material.SetOverrideTag("RenderType", "");
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            material.SetFloat("_Mode", 0);
            material.SetInt("_ZWrite", 1);
            material.EnableKeyword("_EMISSION");
            material.DisableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHABLEND_ON_EMISSION");
            material.renderQueue = -1;
        }

        public static void ToFadeMode(Material material)
        {
            material.SetOverrideTag("RenderType", "Transparent");
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetFloat("_Mode", 2);
            //material.SetInt("_ZWrite", 0);
            material.EnableKeyword("_EMISSION");
            material.EnableKeyword("_ALPHABLEND_ON");
            material.EnableKeyword("_ALPHABLEND_ON_EMISSION");
            material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
        }

        public static float StringToFloat(string text)
        {
            if (text == null) return 0;
            float value = 0f;
            try
            {
                value = float.Parse(text);
            }
            catch (Exception e)
            {
                Debug.LogError("[Panthera -> Function.StringToFloat] Unable to parse the String.");
                Debug.LogError(e.ToString());
                value = 0f;
            }
            return value;
        }

    }
}
