using Panthera;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.OldSkills;
using Panthera.Skills;
using Panthera.Skills.Actives;
using Panthera.Skills.Passives;
using Panthera.Utils;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Random = System.Random;

namespace Panthera.Skills.Actives
{
    public class ShieldBash : MachineScript
    {

        public float startTime;
        public int previousPlayerLayer;
        public bool firstUse = true;
        public bool wasGodMode = false;
        public int effectID;
        public Vector3 originalVelocity;
        public List<GameObject> enemiesHit = new List<GameObject>();
        public Vector3 startingDirection;

        public ShieldBash()
        {
            base.icon = Assets.ShieldBashSkill  ;
            base.name = PantheraTokens.Get("ability_ShieldBashName");
            base.baseCooldown = PantheraConfig.ShieldBash_cooldown;
            base.desc1 = String.Format(Utils.PantheraTokens.Get("ability_ShieldBashDesc"), PantheraConfig.ShieldBash_damageMultiplier * 100, PantheraConfig.ShieldBash_stunDuration);
            base.desc2 = null;
            base.skillID = PantheraConfig.ShieldBash_SkillID;
            base.requiredAbilityID = PantheraConfig.ShieldBash_AbilityID;
            //base.machineToUse = 2;
            base.showCooldown = true;
            base.priority = PantheraConfig.ShieldBash_priority;
            base.interruptPower = PantheraConfig.ShieldBash_interruptPower;
        }

        public override bool CanBeUsed(PantheraObj ptraObj)
        {
            if (ptraObj.characterBody.frontShield <= 0) return false;
            if (ptraObj.frontShieldObj.active == false) return false;
            if (ptraObj.skillLocator.getStock(PantheraConfig.ShieldBash_SkillID) <= 0) return false;
            return true;
        }

        public override void Start()
        {

            // Set the Cooldown //
            base.skillLocator.startCooldown(PantheraConfig.ShieldBash_SkillID);

            // Save the time //
            this.startTime = Time.time;

            // Set in combat //
            base.characterBody.outOfCombatStopwatch = 0f;

            // Set the character to forward //
            base.characterDirection.forward = base.GetAimRay().direction;

            // Get the Original Velocity //
            this.originalVelocity = base.characterMotor.velocity;

            // Get the Starting Direction //
            this.startingDirection = base.characterDirection.forward;

            // Get the God Mode //
            this.wasGodMode = base.healthComponent.godMode;

            // Set the Character to God Mode //
            new ServerSetGodMode(base.gameObject, true).Send(NetworkDestination.Server);

            // Start the effect //
            this.effectID = FXManager.SpawnEffect(base.gameObject, Assets.ShieldBashFX, base.modelTransform.position, 1, base.characterBody.gameObject, new Quaternion(), true);

            // Play the Sound //
            Sound.playSound(Sound.ShieldBash, base.gameObject);

            // Clear the List //
            this.enemiesHit.Clear();
        }

        public override void Update()
        {

        }

        public override void FixedUpdate()
        {

            // Stop if the duration is reached //
            float skillDuration = Time.time - this.startTime;
            if (skillDuration >= PantheraConfig.ShieldBash_skillDuration)
            {
                base.EndScript();
                return;
            }

            // Set the Direction //
            base.characterDirection.forward = this.startingDirection;

            // Get the Stun duration //
            float stunDuration = PantheraConfig.ShieldBash_stunDuration;

            // Calcule the damages //
            float damage = base.damageStat * PantheraConfig.ShieldBash_damageMultiplier;

            // Find the Enemies //
            Collider[] colliders = Physics.OverlapSphere(base.characterBody.corePosition + base.characterDirection.forward * PantheraConfig.ShieldBash_grabDistanceMultiplier, PantheraConfig.ShieldBash_grabScanRadius, LayerIndex.entityPrecise.mask.value);
            foreach (Collider collider in colliders)
            {
                HurtBox hurtbox = collider.GetComponent<HurtBox>();
                TeamComponent tc = hurtbox.healthComponent?.body?.teamComponent;
                if (tc != null && tc.teamIndex == TeamIndex.Monster)
                {
                    GameObject obj = tc.body.gameObject;
                    if (this.enemiesHit.Contains(obj) == false)
                    {
                        this.enemiesHit.Add(obj);
                        new ServerStunTarget(obj, stunDuration).Send(NetworkDestination.Server);
                        new ServerInflictDamage(base.gameObject, obj, obj.transform.position, damage, RollCrit()).Send(NetworkDestination.Server);
                        if (tc.body.characterMotor != null)
                        {
                            float forceRand = UnityEngine.Random.Range(PantheraConfig.ShieldBash_pushMinMultiplier, PantheraConfig.ShieldBash_pushMaxMultiplier);
                            float upRand = UnityEngine.Random.Range(PantheraConfig.ShieldBash_upMinMultiplier, PantheraConfig.ShieldBash_upMaxMultiplier);
                            Vector3 direction = tc.body.corePosition - base.characterBody.corePosition;
                            Vector3 velocity = direction.normalized * base.characterBody.moveSpeed * forceRand;
                            velocity.y = base.characterBody.moveSpeed * upRand;
                            new ClientSetBodyVelocity(tc.body.gameObject, velocity).Send(NetworkDestination.Server);
                            rechargeShield();
                        }
                    }
                }
            }
            // Do the Dash //
            this.dash();

        }

        public override void Stop()
        {

            // Decrease the Velocity //
            base.pantheraObj.StartCoroutine(DecreaseVelocity(base.characterMotor, this.originalVelocity, this.effectID));

            // Set the Character to Normal Mode //
            new ServerSetGodMode(base.gameObject, this.wasGodMode).Send(NetworkDestination.Server);

            // Set the Jump count to zero //
            if (base.characterMotor.jumpCount > 0)
                base.characterMotor.jumpCount--;

        }

        public static IEnumerator DecreaseVelocity(CharacterMotor motor, Vector3 originalVelocity, int effectID)
        {
            float startTime = Time.time;
            bool effectDestroyed = false;
            while(motor.velocity.sqrMagnitude > originalVelocity.sqrMagnitude)
            {
                yield return new WaitForSeconds(0.05f);
                float time = Time.time - startTime;
                if (time > PantheraConfig.ShieldBash_effectDuration && effectDestroyed == false)
                {
                    FXManager.DestroyEffect(effectID, 1);
                    effectDestroyed = true;
                }
                motor.velocity *= PantheraConfig.ShieldBash_magnetudeDecreasePercent;
            }
            if (effectDestroyed == false)
                FXManager.DestroyEffect(effectID, 0.5f);
            yield return null;
        }


        public void dash()
        {
            Vector3 dashDirection = base.GetAimRay().direction;
            if (base.characterMotor.isGrounded == false)
            {
                dashDirection.y = base.GetAimRay().direction.y;
                base.characterDirection.forward = base.GetAimRay().direction;
            }
            base.pantheraObj.pantheraMotor.isSprinting = true;
            base.characterBody.characterMotor.velocity = dashDirection * PantheraConfig.ShieldBash_moveSpeedMultiplier;
        }

        public void rechargeShield()
        {
            //int abilityLevel = CharacterAbilities.getAbilityLevel(PantheraConfig.KineticAbsorbtionAbilityID);
            //float absorbtionAdded = 0;
            //if (abilityLevel == 1)
            //    absorbtionAdded = PantheraConfig.Default_MaxShield * PantheraConfig.KineticAbsorbtion_percent1;
            //if (abilityLevel == 2)
            //    absorbtionAdded = PantheraConfig.Default_MaxShield * PantheraConfig.KineticAbsorbtion_percent2;
            //if (abilityLevel == 3)
            //    absorbtionAdded = PantheraConfig.Default_MaxShield * PantheraConfig.KineticAbsorbtion_percent3;
            //characterBody.shield += (float)Math.Ceiling(absorbtionAdded);
        }

    }
}
