using EntityStates;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.GUI;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.Utils;
using R2API;
using R2API.Networking;
using R2API.Networking.Interfaces;
using Rewired;
using RoR2;
using RoR2.Audio;
using RoR2.Projectile;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Panthera.Skills
{
    class Rip : MachineScript
    {

        //public static float aimVelocity = 0.3f;
        //public static float maxRushDistance = 2;
        //public static float rushDistanceStop = 1;

        public OverlapAttack attack;
        public float damageMultiplier;
        public int comboNumber = 1;
        public string swingSoundString = "";
        public string hitSoundString = "";
        public CharacterBody target;
        public float startTime;
        public float baseDuration;
        public bool hasFired = false;
        public int ghostRipLevel;
        public bool isFireRip = false;

        public Rip()
        {

        }

        public static void Create()
        {
            // Create the Skill //
            PantheraSkill skill = new PantheraSkill();
            skill.skillID = PantheraConfig.Rip_SkillID;
            skill.name = "RIP_SKILL_NAME";
            skill.desc = "RIP_SKILL_DESC";
            skill.icon = Assets.Rip;
            skill.iconPrefab = Assets.ActiveSkillPrefab;
            skill.type = PantheraSkill.SkillType.active;
            skill.associatedSkill = typeof(Rip);
            skill.priority = PantheraConfig.Rip_priority;
            skill.interruptPower = PantheraConfig.Rip_interruptPower;
            skill.cooldown = PantheraConfig.Rip_cooldown;
            skill.requiredEnergy = PantheraConfig.Rip_requiredEnergy;

            // Save this Skill //
            PantheraSkill.SkillDefsList.Add(skill.skillID, skill);
        }

        public override PantheraSkill getSkillDef()
        {
            return base.pantheraObj.activePreset.getSkillByID(PantheraConfig.Rip_SkillID);
        }

        public override bool CanBeUsed(PantheraObj ptraObj)
        {
            base.pantheraObj = ptraObj;
            if (ptraObj.characterBody.energy < this.getSkillDef().requiredEnergy) return false;
            if (ptraObj.skillLocator.getCooldownInSecond(this.getSkillDef().skillID) > 0) return false;
            return true;
        }


        public override void Start()
        {

            // Set the Cooldown //
            base.skillLocator.startCooldown(this.getSkillDef().skillID);

            // Remove the Energy //
            base.characterBody.energy -= this.getSkillDef().requiredEnergy;

            // Get the The Rip-per buff count //
            int ripperBuffCount = base.characterBody.GetBuffCount(Base.Buff.TheRipperBuff);

            // Check if Fire Rip //
            if (base.pantheraObj.activePreset.getAbilityLevel(PantheraConfig.BurningSpiritAbilityID) > 0 && ripperBuffCount >= PantheraConfig.BurningSpirit_ripperStackNeeded)
            {
                this.isFireRip = true;
            }

            // Save the time //
            this.startTime = Time.time;

            // Unstealth //
            Passives.Stealth.DidDamageUnstealth(base.pantheraObj);

            // Set in combat //
            characterBody.outOfCombatStopwatch = 0f;

            // Stop the Sprint //
            base.pantheraObj.pantheraMotor.startSprint = false;

            // Start the Aim Mode //
            base.StartAimMode(PantheraConfig.Rip_minimumAimTime + this.baseDuration, false);

            // Apply Rip-per Buffs //
            float ripperDamage = 1 + (ripperBuffCount * PantheraConfig.TheRipper_damageIncreasePercent);
            float ripperSpeed = 1 + (ripperBuffCount * PantheraConfig.TheRipper_speedIncreasePercent);

            // Set the Damage //
            if (comboNumber == 1) this.damageMultiplier = PantheraConfig.Rip_atk1DamageMultiplier;
            else if (comboNumber == 2) this.damageMultiplier = PantheraConfig.Rip_atk2DamageMultiplier;
            else if (comboNumber == 3) this.damageMultiplier = PantheraConfig.Rip_atk3DamageMultiplier;

            // Get Critical Chance //
            float critChance = base.critStat;

            // Check the Ghost Rip Ability //
            this.ghostRipLevel = base.pantheraObj.activePreset.getAbilityLevel(PantheraConfig.GhostRipAbilityID);
            if (base.pantheraObj.stealthed == true)
            {
                if (this.ghostRipLevel == 1)
                    critChance += PantheraConfig.GhostRip_critPercent1;
                else if (this.ghostRipLevel == 2)
                    critChance += PantheraConfig.GhostRip_critPercent2;
                else if (this.ghostRipLevel == 3)
                    critChance += PantheraConfig.GhostRip_critPercent3;
                else if (this.ghostRipLevel == 4)
                    critChance += PantheraConfig.GhostRip_critPercent4;
                else if (this.ghostRipLevel == 5)
                    critChance += PantheraConfig.GhostRip_critPercent5;
            }

            // Create the Attack //
            bool isCrit = Util.CheckRoll(critChance, base.characterBody.master);
            float damage = base.characterBody.damage * (this.damageMultiplier * ripperDamage);
            this.attack = Utils.Functions.CreateOverlapAttack(base.gameObject, damage, isCrit, PantheraConfig.Rip_hitboxName, default(Vector3), Assets.RipHitFX);

            // Set the character to forward //
            base.characterDirection.forward = base.GetAimRay().direction;

            // Get the duration //
            if (comboNumber == 1) this.baseDuration = PantheraConfig.Rip_atk1BaseDuration;
            else if (comboNumber == 2) this.baseDuration = PantheraConfig.Rip_atk2BaseDuration;
            else if (comboNumber == 3) this.baseDuration = PantheraConfig.Rip_atk3BaseDuration;

            // Set the attack duration //
            this.baseDuration = (this.baseDuration / this.attackSpeedStat) / ripperSpeed;

        }

        public override void Update()
        {
            
        }

        public override void FixedUpdate()
        {

            // Stop the dash if the target is dead //
            //if (this.target != null && this.target.healthComponent.alive == false)
            //{
            //    this.target = null;
            //}

            // Stop if the duration is reached //
            float skillDuration = Time.time - this.startTime;
            if (skillDuration >= this.baseDuration)
            {
                base.EndScript();
                return;
            }


            // Check if the attack has already fired //
            if (this.hasFired == false)
            {

                // Fire the attack //
                List<HurtBox> enemiesHit = new List<HurtBox>();
                this.hasFired = true;
                this.attack.Fire(enemiesHit);

                // Create the Sound //
                string ripSound = "";

                // Combo 1 //
                if (this.comboNumber == 1 == true)
                {
                    Sound.playSound(Utils.Sound.Rip1, base.gameObject);
                    base.PlayAnimation("LeftRip", 0.2f);
                    ripSound = Utils.Sound.RipHit1;
                    Utils.FXManager.SpawnEffect(base.gameObject, Assets.LeftRipFX, base.characterBody.corePosition, 1, base.characterBody.gameObject, base.modelTransform.rotation, true);
                    if (this.isFireRip)
                    {
                        Sound.playSound(Utils.Sound.FireRip1, base.gameObject);
                        Utils.FXManager.SpawnEffect(base.gameObject, Assets.LeftFireRipFX, base.characterBody.corePosition, base.pantheraObj.modelScale, null, base.modelTransform.rotation);
                    }
                }
                // Combo 2 //
                else if (this.comboNumber == 2)
                {
                    Sound.playSound(Utils.Sound.Rip1, base.gameObject);
                    base.PlayAnimation("RightRip", 0.2f);
                    ripSound = Utils.Sound.RipHit1;
                    Utils.FXManager.SpawnEffect(base.gameObject, Assets.RightRipFX, base.characterBody.corePosition, 1, base.characterBody.gameObject, base.modelTransform.rotation, true);
                    if (this.isFireRip == true)
                    {
                        Sound.playSound(Utils.Sound.FireRip1, base.gameObject);
                        Utils.FXManager.SpawnEffect(base.gameObject, Assets.RightFireRipFX, base.characterBody.corePosition, base.pantheraObj.modelScale, null, base.modelTransform.rotation);
                    }
                }
                // Combo 3 //
                else if (this.comboNumber == 3)
                {
                    Sound.playSound(Utils.Sound.Rip2, base.gameObject);
                    base.PlayAnimation("FrontRip", 0.2f);
                    ripSound = Utils.Sound.RipHit2;
                    Utils.FXManager.SpawnEffect(base.gameObject, Assets.FrontRipFX, base.characterBody.corePosition, 1, base.characterBody.gameObject, base.modelTransform.rotation, true);
                    if (this.isFireRip == true)
                    {
                        Sound.playSound(Utils.Sound.FireRip2, base.gameObject);
                        Utils.FXManager.SpawnEffect(base.gameObject, Assets.FrontFireRipFX, base.characterBody.corePosition, 1, null, base.modelTransform.rotation);
                    }
                }

                // Check if Enemies was hit //
                if (enemiesHit != null && enemiesHit.Count > 0)
                {

                    // Add The Rip-per buff //
                    if (base.pantheraObj.activePreset.getAbilityLevel(PantheraConfig.TheRipperAbilityID) > 0)
                        Passives.Ripper.AddBuff(base.pantheraObj);

                    // Apply the Bloody Rage Ability //
                    int abilityLevel = base.pantheraObj.activePreset.getAbilityLevel(PantheraConfig.BloodyRageAbilityID);
                    int ripperBuffCount = base.characterBody.GetBuffCount(Base.Buff.TheRipperBuff);
                    float rand = UnityEngine.Random.Range(0f, 1f);
                    bool ok = false;
                    if (ripperBuffCount > 0 && abilityLevel > 0)
                    {
                        float chance = 0;
                        if (abilityLevel == 1)
                            chance = PantheraConfig.BloodyRage_Percent1 * ripperBuffCount;
                        else if (abilityLevel == 2)
                            chance = PantheraConfig.BloodyRage_Percent2 * ripperBuffCount;
                        else if (abilityLevel == 3)
                            chance = PantheraConfig.BloodyRage_Percent3 * ripperBuffCount;
                        else if (abilityLevel == 4)
                            chance = PantheraConfig.BloodyRage_Percent4 * ripperBuffCount;
                        else if (abilityLevel == 5)
                            chance = PantheraConfig.BloodyRage_Percent5 * ripperBuffCount;
                        if (rand <= chance)
                            base.pantheraObj.characterBody.fury += 1;
                    }

                    // Apply the Ghost Rip Ability //
                    float stunDuration = 0;
                    if (this.ghostRipLevel > 0 && base.pantheraObj.stealthed == true)
                        Utils.Sound.playSound(Utils.Sound.GhostRip, base.gameObject);
                    if (this.ghostRipLevel == 1)
                        stunDuration = PantheraConfig.GhostRip_stunDuration1;
                    else if (this.ghostRipLevel == 2)
                        stunDuration = PantheraConfig.GhostRip_stunDuration2;
                    else if (this.ghostRipLevel == 3)
                        stunDuration = PantheraConfig.GhostRip_stunDuration3;
                    else if (this.ghostRipLevel == 4)
                        stunDuration = PantheraConfig.GhostRip_stunDuration4;
                    else if (this.ghostRipLevel == 5)
                        stunDuration = PantheraConfig.GhostRip_stunDuration5;
                        
                    // Check all Enemies //
                    List<GameObject> enemiesHurt = new List<GameObject>();
                    foreach (HurtBox enemy in enemiesHit)
                    {
                        HealthComponent hc = enemy.healthComponent;
                        if (hc == null) continue;
                        if (enemiesHurt.Contains(hc.gameObject)) continue;
                        enemiesHurt.Add(hc.gameObject);
                        Utils.Sound.playSound(ripSound, hc.gameObject);
                        // Apply the Ghost Rip Ability //
                        if (stunDuration > 0 && base.pantheraObj.stealthed == true)
                            new ServerStunTarget(enemy.healthComponent.gameObject, stunDuration).Send(NetworkDestination.Server);
                        // Apply the Burning Spirit Ability //
                        if (this.isFireRip == true)
                            new ServerInflictDot(base.gameObject, enemy.healthComponent.gameObject, PantheraConfig.BurnDotIndex, PantheraConfig.BurningSpirit_burnDuration, PantheraConfig.BurningSpirit_burnDamage).Send(NetworkDestination.Server);
                        // Apply the Hell Cat Ability //
                        if (base.pantheraObj.activePreset.getAbilityLevel(PantheraConfig.HellCatAbilityID) > 0 && this.isFireRip == true)
                        {
                            base.characterBody.fury += 1;
                            base.characterBody.power += 1;
                        }
                    }  

                    // Add a Combo Point if this is the third combo //
                    if (this.comboNumber == 3)
                    {
                        this.characterBody.comboPoint += 1;
                    }

                    // Apply Weak //
                    //foreach (HurtBox enemy in enemiesHit)
                    //{
                    //    new ServerApplyWeak(enemy.healthComponent.gameObject, PantheraConfig.Rip_weakDuration).Send(NetworkDestination.Server);
                    //}
                }


            }

        }

        public override void Stop()
        {

            // Increate the combo number //
            this.comboNumber += 1;
            if (this.comboNumber > 3) this.comboNumber = 1;

            // Start the combo //
            if (this.characterBody.energy >= this.getSkillDef().requiredEnergy && base.wasInterrupted == false && base.inputBank.isSkillPressed(this.getSkillDef().skillID)) // && !(base.inputBank.IsDirectionKeyPressed() && base.inputBank.IsKeyPressed(PantheraConfig.SprintKey)))
            {
                this.SetScript(new Rip {comboNumber = this.comboNumber});
            }

        }

    }
}
