using Panthera;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.Skills;
using Panthera.SkillsHybrid;
using Panthera.Utils;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Random = System.Random;

namespace Panthera.SkillsHybrid
{
    internal class ShieldBash : MachineScript
    {

        public float startTime;
        public int previousPlayerLayer;
        public bool firstUse = true;
        public Vector3 originVelocity;
        public List<GameObject> enemiesHit = new List<GameObject>();
        public Vector3 startingDirection;

        public ShieldBash()
        {

        }

        public static void Create()
        {
            // Create the Skill //
            PantheraSkill skill = new PantheraSkill();
            skill.skillID = PantheraConfig.ShieldBash_SkillID;
            skill.name = "SHIELD_BASH_SKILL_NAME";
            skill.desc = "SHIELD_BASH_SKILL_DESC";
            skill.icon = Assets.ShieldBash;
            skill.iconPrefab = Assets.HybridSkillPrefab;
            skill.type = PantheraSkill.SkillType.hybrid;
            skill.associatedSkill = typeof(ShieldBash);
            skill.priority = PantheraConfig.ShieldBash_priority;
            skill.interruptPower = PantheraConfig.ShieldBash_interruptPower;
            skill.cooldown = PantheraConfig.ShieldBash_cooldown;
            skill.requiredEnergy = PantheraConfig.ShieldBash_requiredEnergy;

            // Save this Skill //
            PantheraSkill.SkillDefsList.Add(skill.skillID, skill);
        }

        public override PantheraSkill getSkillDef()
        {
            return base.pantheraObj.activePreset.getSkillByID(PantheraConfig.ShieldBash_SkillID);
        }

        public override bool CanBeUsed(PantheraObj ptraObj)
        {
            base.pantheraObj = ptraObj;
            if (ptraObj.characterBody.energy < this.getSkillDef().requiredEnergy) return false;
            if (ptraObj.characterBody.shield <= 0) return false;
            if (ptraObj.skillLocator.getCooldownInSecond(this.getSkillDef().skillID) > 0) return false;
            return true;
        }

        public override void Start()
        {

            // Set the Cooldown //
            skillLocator.startCooldown(this.getSkillDef().skillID);

            // Remove the Energy //
            characterBody.energy -= this.getSkillDef().requiredEnergy;

            // Save the time //
            startTime = Time.time;

            // Set in combat //
            characterBody.outOfCombatStopwatch = 0f;

            // Set the character to forward //
            characterDirection.forward = GetAimRay().direction;

            // Get the Original Velocity //
            originVelocity = characterMotor.velocity;

            // Get the Starting Direction //
            startingDirection = characterDirection.forward;

            // Play the Sound //
            Sound.playSound(Sound.ShieldBash, gameObject);

        }

        public override void Update()
        {

        }

        public override void FixedUpdate()
        {

            // Stop if the duration is reached //
            float skillDuration = Time.time - startTime;
            if (skillDuration >= PantheraConfig.ShieldBash_skillDuration)
            {
                EndScript();
                return;
            }

            // Set the Direction //
            characterDirection.forward = startingDirection;

            // Get the Stun duration //
            float stunDuration = pantheraObj.activePreset.shieldBash_stunDuration;

            // Calcule the damages //
            float damage = damageStat * pantheraObj.activePreset.shieldBash_damage;

            // Find the Enemies //
            Collider[] colliders = Physics.OverlapSphere(characterBody.corePosition + characterDirection.forward * PantheraConfig.ShieldBash_grabDistanceMultiplier, PantheraConfig.ShieldBash_grabScanRadius, LayerIndex.entityPrecise.mask.value);
            foreach (Collider collider in colliders)
            {
                HurtBox hurtbox = collider.GetComponent<HurtBox>();
                TeamComponent tc = hurtbox.healthComponent?.body?.teamComponent;
                if (tc != null && tc.teamIndex == TeamIndex.Monster)
                {
                    GameObject obj = tc.body.gameObject;
                    if (enemiesHit.Contains(obj) == false)
                    {
                        enemiesHit.Add(obj);
                        Passives.Stealth.DidDamageUnstealth(base.pantheraObj);
                        new ServerStunTarget(obj, stunDuration).Send(NetworkDestination.Server);
                        new ServerInflictDamage(gameObject, obj, obj.transform.position, damage, RollCrit()).Send(NetworkDestination.Server);
                        if (tc.body.characterMotor != null)
                        {
                            float forceRand = UnityEngine.Random.Range(PantheraConfig.ShieldBash_pushMinMultiplier, PantheraConfig.ShieldBash_pushMaxMultiplier);
                            float upRand = UnityEngine.Random.Range(PantheraConfig.ShieldBash_upMinMultiplier, PantheraConfig.ShieldBash_upMaxMultiplier);
                            Vector3 direction = tc.body.corePosition - characterBody.corePosition;
                            Vector3 velocity = direction.normalized * characterBody.moveSpeed * forceRand;
                            velocity.y = characterBody.moveSpeed * upRand;
                            tc.body.characterMotor.velocity = velocity;
                            rechargeShield();

                        }
                    }
                }
            }

            // Do the Dash //
            dash();

        }

        public override void Stop()
        {
            characterMotor.velocity = originVelocity;
        }

        public void dash()
        {
            Vector3 dashDirection = GetAimRay().direction;
            if (characterMotor.isGrounded == false)
            {
                dashDirection.y = GetAimRay().direction.y;
                characterDirection.forward = GetAimRay().direction;
            }
            pantheraObj.pantheraMotor.startSprint = true;
            characterBody.characterMotor.velocity = dashDirection * PantheraConfig.ShieldBash_moveSpeedMultiplier;
        }

        public void rechargeShield()
        {
            int abilityLevel = pantheraObj.activePreset.getAbilityLevel(PantheraConfig.KineticAbsorbtionAbilityID);
            float absorbtionAdded = 0;
            if (abilityLevel == 1)
                absorbtionAdded = pantheraObj.activePreset.maxShield * PantheraConfig.KineticAbsorbtion_percent1;
            if (abilityLevel == 2)
                absorbtionAdded = pantheraObj.activePreset.maxShield * PantheraConfig.KineticAbsorbtion_percent2;
            if (abilityLevel == 3)
                absorbtionAdded = pantheraObj.activePreset.maxShield * PantheraConfig.KineticAbsorbtion_percent3;
            characterBody.shield += (float)Math.Ceiling(absorbtionAdded);
        }

    }
}
