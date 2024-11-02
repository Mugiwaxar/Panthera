using Panthera.MachineScripts;
using RoR2;
using System;
using System.Linq;
using UnityEngine;

namespace Panthera.BodyComponents
{
    public class PantheraSkillLocator : SkillLocator
    {

        // (int SkillID 1 - ??, LastUsedTime) Represent the Current Cooldown of all Skills //
        public RechargeSkill[] rechargeSkillList;

        public PantheraObj ptraObj;

        public void CreateRechargeSkillsList()
        {
            this.rechargeSkillList = [.. this.ptraObj.CharacterSkills.SkillsList.Select(skill => new RechargeSkill(skill, skill.maxStock, skill.baseCooldown))];
        }

        public void FixedUpdate()
        {
            // Itinerate all RechargeSkill
            if (!this.rechargeSkillList?.Any() != true)
                return;

            for (int i = 0; i < this.rechargeSkillList.Length; i++)
            {
                // Check the RechargeSkill //
                RechargeSkill rechargeSkill = this.rechargeSkillList[i];
                if (rechargeSkill.stock < rechargeSkill.maxStock)
                {
                    // Remove the interval from the Cooldown //
                    rechargeSkill.cooldown -= Time.fixedDeltaTime;
                    // Check if a Stock must be added //
                    if (rechargeSkill.cooldown <= 0)
                    {
                        rechargeSkill.stock++;
                        rechargeSkill.cooldown = this.GetMaxCooldown(i);
                    }
                }
                else
                {
                    rechargeSkill.cooldown = 0;
                }
            }
        }

        public RechargeSkill GetSkill(int skillID) => this.rechargeSkillList?.ElementAtOrDefault(skillID);

        public int GetStock(int skillID) => this.rechargeSkillList?.ElementAtOrDefault(skillID)?.stock ?? 0;

        public void SetMaxStock(int skillID, int maxStock)
        {
            var skill = this.rechargeSkillList?.ElementAtOrDefault(skillID);
            if (skill is not null)
                skill.maxStock = maxStock;
        }

        public void AddOneStock(int skillID)
        {
            var skill = this.rechargeSkillList?.ElementAtOrDefault(skillID);
            if (skill is not null && skill.stock < skill.maxStock)
                skill.stock++;
        }

        public void RemoveOneStock(int skillID)
        {
            var skill = this.rechargeSkillList?.ElementAtOrDefault(skillID);
            if (skill is not null && skill.stock > 0)
                skill.stock--;
        }

        public float GetMaxCooldown(int skillID) => this.rechargeSkillList?.ElementAtOrDefault(skillID)?.baseCooldown ?? 0;

        public void SetMaxCooldown(int skillID, float baseCooldown)
        {
            var skill = this.rechargeSkillList?.ElementAtOrDefault(skillID);
            if (skill is not null)
                skill.baseCooldown = baseCooldown;
        }

        public float GetCooldown(int skillID) => this.rechargeSkillList?.ElementAtOrDefault(skillID)?.cooldown ?? 0;

        public void SetCooldown(int skillID, float cooldown)
        {
            var skill = this.rechargeSkillList?.ElementAtOrDefault(skillID);
            if (skill is not null)
                skill.cooldown = cooldown;
        }

        public void StartCooldown(int skillID, float newTime = 0)
        {
            // Get the RechargeSkill //
            RechargeSkill rechargeSkill = GetSkill(skillID);
            if (rechargeSkill is null)
                return;


            // Set the RechargeSkill //
            if (rechargeSkill.stock > 0)
                rechargeSkill.stock--;

            if (rechargeSkill.cooldown == 0)
            {
                // Check the Recharge Time //
                if (newTime <= 0)
                    newTime = GetMaxCooldown(skillID);

                rechargeSkill.cooldown = newTime;
            }
        }

        public static void ApplyAmmoPackHook(Action<SkillLocator> orig, SkillLocator self)
        {
            
            // Call the Original Function //
            orig(self);

            // Check if Panthera //
            if (self is PantheraSkillLocator skillLocator && skillLocator)
            {
                // Add a Stock to all Skills //
                skillLocator.AddOneStock(PantheraConfig.Rip_SkillID);
                skillLocator.AddOneStock(PantheraConfig.Slash_SkillID);
                skillLocator.AddOneStock(PantheraConfig.Leap_SkillID);
                skillLocator.AddOneStock(PantheraConfig.MightyRoar_SkillID);
            }

        }

    }

    public class RechargeSkill
    {

        public MachineScript skill;
        public int stock;
        public int maxStock;
        public float baseCooldown;
        public float cooldown;

        public RechargeSkill(MachineScript skill, int maxStock, float baseCooldown)
        {
            this.skill = skill;
            this.stock = maxStock;
            this.maxStock = maxStock;
            this.baseCooldown = baseCooldown;
        }
    }

}
