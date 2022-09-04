using EntityStates;
using Panthera.NetworkMessages;
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
    class Functions
    {
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

        public static GameObject SpawnEffect(GameObject creator, GameObject prefab, Vector3 origin, float scale = 1, GameObject parent = null, Quaternion rotation = new Quaternion(), bool transmit = true)
        {

            // The effect can only be created by the client //
            if (NetworkClient.active == false) return null;

            // Create the effect data //
            EffectData effectData = new EffectData();

            // Find the effect index //
            EffectIndex effectIndex = EffectCatalog.FindEffectIndexFromPrefab(prefab);

            // Check if the effect must be send to the others clients //
            if (transmit  == true)
            {
                // Send the effect //
                new ClientSpawnEffect(creator, prefab, origin, scale, parent, rotation).Send(NetworkDestination.Clients);
            }

            // Start the sound //
            if (effectData.networkSoundEventIndex != NetworkSoundEventIndex.Invalid)
            {
                PointSoundManager.EmitSoundLocal(NetworkSoundEventCatalog.GetAkIdFromNetworkSoundEventIndex(effectData.networkSoundEventIndex), effectData.origin);
            }

            // Get the effect def //
            EffectDef effectDef = EffectCatalog.GetEffectDef(effectIndex);
            if (effectDef == null)
            {
                return null;
            }

            // Play the sound attached to the effect def //
            string spawnSoundEventName = effectDef.spawnSoundEventName;
            if (!string.IsNullOrEmpty(spawnSoundEventName))
            {
                PointSoundManager.EmitSoundLocal((AkEventIdArg)spawnSoundEventName, origin);
            }

            // Play the sound related to the surface where the effect is spawned //
            SurfaceDef surfaceDef = SurfaceDefCatalog.GetSurfaceDef(effectData.surfaceDefIndex);
            if (surfaceDef != null)
            {
                string impactSoundString = surfaceDef.impactSoundString;
                if (!string.IsNullOrEmpty(impactSoundString))
                {
                    PointSoundManager.EmitSoundLocal((AkEventIdArg)impactSoundString, effectData.origin);
                }
            }

            // Check if the effect can be spawned //
            if (!VFXBudget.CanAffordSpawn(effectDef.prefabVfxAttributes))
            {
                return null;
            }

            // Check the culling method ? //
            if (effectDef.cullMethod != null && !effectDef.cullMethod(effectData))
            {
                return null;
            }

            // Clone the effect data //
            EffectData effectData2 = effectData.Clone();

            // Instantiate the effect //
            GameObject effectObject = UnityEngine.Object.Instantiate<GameObject>(effectDef.prefab, effectData2.origin, effectData2.rotation);
            EffectComponent component = effectObject.GetComponent<EffectComponent>();

            // Set the effect data of the component with the clone //
            if (component)
            {
                component.effectData = effectData2.Clone();
            }

            // Set the parrent //
            if (parent != null) effectObject.transform.parent = parent.transform;

            // Set the Position/Rotation/Scale //
            effectObject.transform.position = origin;
            effectObject.transform.rotation = rotation;
            effectObject.transform.localScale = new Vector3(scale, scale, scale);

            // Return the effect object //
            return effectObject;

        }

        public static void PlayAnimation(GameObject character, string layerName, string animationName, bool transmit = true)
        {
            ModelLocator locator = character.GetComponent<ModelLocator>();
            if (locator == null || locator.modelTransform == null) return;
            Animator animator = locator.modelTransform.GetComponent<Animator>();
            if (animator == null) return;
            EntityState.PlayAnimationOnAnimator(animator, layerName, animationName);
            if (NetworkServer.active == false && transmit == true) new ClientPlayAnimation(character, layerName, animationName).Send(NetworkDestination.Clients);
        }

        public static void SetAnimatorBoolean(GameObject character, string paramName, bool setValue, bool transmit = true)
        {
            ModelLocator locator = character.GetComponent<ModelLocator>();
            if (locator == null || locator.modelTransform == null) return;
            Animator animator = locator.modelTransform.GetComponent<Animator>();
            if (animator == null) return;
            animator.SetBool(paramName, setValue);
            if (NetworkServer.active == false && transmit == true)
                new ClientSetAnimatorBoolean(character, paramName, setValue).Send(NetworkDestination.Clients);
        }

        public static void SetAnimatorFloat(GameObject character, string paramName, float value1, float value2 = 0, float value3 = 0, bool transmit = true)
        {
            ModelLocator locator = character.GetComponent<ModelLocator>();
            if (locator == null || locator.modelTransform == null) return;
            Animator animator = locator.modelTransform.GetComponent<Animator>();
            if (animator == null) return;
            animator.SetFloat(paramName, value1, value2, value3);
            if (NetworkServer.active == false && transmit == true)
                new ClientSetAnimatorFloat(character, paramName, value1, value2, value3).Send(NetworkDestination.Clients);
        }

        public static OverlapAttack CreateOverlapAttack(GameObject attacker, float damageCoefficient, string hitBoxName)
        {
            // Get the HitBox //
            HitBoxGroup hitBoxGroup = null;
            Transform modelTransform = attacker.GetComponent<ModelLocator>().modelTransform;
            if (modelTransform)
            {
                hitBoxGroup = Array.Find<HitBoxGroup>(modelTransform.GetComponents<HitBoxGroup>(), (HitBoxGroup element) => element.groupName == hitBoxName);
            }

            // Create the Attack //
            OverlapAttack attack;
            CharacterBody body = attacker.GetComponent<CharacterBody>();
            attack = new OverlapAttack();
            attack.damageType = PantheraConfig.Rip_damageType;
            attack.attacker = attacker;
            attack.inflictor = attacker;
            attack.teamIndex = TeamComponent.GetObjectTeam(attacker);
            attack.damage = damageCoefficient * body.damage;
            attack.procCoefficient = PantheraConfig.Rip_procCoefficient;
            //this.attack.hitEffectPrefab = this.hitEffectPrefab;
            attack.forceVector = PantheraConfig.Rip_bonusForce;
            attack.pushAwayForce = PantheraConfig.Rip_pushForce;
            attack.hitBoxGroup = hitBoxGroup;
            attack.isCrit = Util.CheckRoll(body.crit, body.master);
            //this.attack.impactSound = this.impactSound;

            return attack;
        }

        public static float getCollideDistance(Rigidbody r1, Rigidbody r2)
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

    }
}
