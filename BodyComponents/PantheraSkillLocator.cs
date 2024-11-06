using EntityStates.LunarGolem;
using EntityStates.TitanMonster;
using Panthera;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.MachineScripts;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.BodyComponents
{
    public class PantheraSkillLocator : SkillLocator
    {

        // (int SkillID 1 - ??, LastUsedTime) Represent the Current Cooldown of all Skills //
        public Dictionary<int, RechargeSkill> rechargeSkillList = new Dictionary<int, RechargeSkill>();

        public PantheraObj ptraObj;

        public void createRechargeSkillsList()
        {
            this.rechargeSkillList = new Dictionary<int, RechargeSkill>();
            foreach (KeyValuePair<int, MachineScript> pairs in this.ptraObj.characterSkills.SkillsList)
            {
                MachineScript skill = pairs.Value;
                RechargeSkill rechargeSkill = new RechargeSkill(skill, skill.maxStock, skill.baseCooldown);
                this.rechargeSkillList.Add(skill.skillID, rechargeSkill);
            }
        }

        public void FixedUpdate()
        {
            // Itinerate all RechargeSkill
            foreach (KeyValuePair<int, RechargeSkill> pairs in this.rechargeSkillList)
            {
                // Check the RechargeSkill //
                RechargeSkill rechargeSkill = pairs.Value;
                if (rechargeSkill.stock < rechargeSkill.maxStock)
                {
                    // Remove the interval from the Cooldown //
                    rechargeSkill.cooldown -= Time.deltaTime;
                    // Check if a Stock must be added //
                    if (rechargeSkill.cooldown <= 0)
                    {
                        rechargeSkill.stock++;
                        rechargeSkill.cooldown = this.getMaxCooldown(pairs.Key);
                    }
                }
                else
                {
                    rechargeSkill.cooldown = 0;
                }
            }

        }

        public void setMaxStock(int skillID, int maxStock)
        {
            if (this.rechargeSkillList.ContainsKey(skillID) == false) return;
            this.rechargeSkillList[skillID].maxStock = maxStock;
        }

        public int getStock(int skillID)
        {
            if (this.rechargeSkillList.ContainsKey(skillID) == false) return 0;
            return this.rechargeSkillList[skillID].stock;
        }

        public void addOneStock(int skillID)
        {
            if (this.rechargeSkillList.ContainsKey(skillID) == false) return;
            RechargeSkill rechargeSkill = this.rechargeSkillList[skillID];
            if (rechargeSkill.stock < rechargeSkill.maxStock)
                rechargeSkill.stock++;
        }

        public void removeOneStock(int skillID)
        {
            if (this.rechargeSkillList.ContainsKey(skillID) == false) return;
            RechargeSkill rechargeSkill = this.rechargeSkillList[skillID];
            if (rechargeSkill.stock > 0)
                rechargeSkill.stock--;
        }

        public float getMaxCooldown(int skillID)
        {
            if (this.rechargeSkillList.ContainsKey(skillID) == false) return 0;
            return this.rechargeSkillList[skillID].baseCooldown;
        }

        public void setMaxCooldown(int skillID, float baseCooldown)
        {
            if (this.rechargeSkillList.ContainsKey(skillID) == false) return;
            this.rechargeSkillList[skillID].baseCooldown = baseCooldown;
        }

        public float getCooldown(int skillID)
        {
            if (this.rechargeSkillList.ContainsKey(skillID) == false) return 0;
            return this.rechargeSkillList[skillID].cooldown;
        }

        public void startCooldown(int skillID, float rechargeTime = 0)
        {

            // Check the Skill ID //
            if (this.rechargeSkillList.ContainsKey(skillID) == false) return;

            // Get the RechargeSkill //
            RechargeSkill rechargeSkill = this.rechargeSkillList[skillID];

            // Check the Recharge Time //
            if (rechargeTime <= 0)
                rechargeTime = getMaxCooldown(skillID);

            // Set the RechargeSkill //
            if (rechargeSkill.stock > 0)
                rechargeSkill.stock--;
            if (rechargeSkill.cooldown == 0)
                rechargeSkill.cooldown = rechargeTime;

        }

        public void setCooldown(int skillID, float cooldown)
        {
            if (this.rechargeSkillList.ContainsKey(skillID) == false) return;
            this.rechargeSkillList[skillID].cooldown = cooldown;
        }

        public static void applyAmmoPackHook(Action<SkillLocator> orig, SkillLocator self)
        {

            // Call the Original Function //
            orig(self);

            // Check if Panthera //
            if (self is not PantheraSkillLocator) return;
            PantheraSkillLocator skillLocator = (PantheraSkillLocator)self;

            // Add a Stock to all Skills //
            skillLocator.addOneStock(PantheraConfig.Rip_SkillID);
            skillLocator.addOneStock(PantheraConfig.Slash_SkillID);
            skillLocator.addOneStock(PantheraConfig.Leap_SkillID);
            skillLocator.addOneStock(PantheraConfig.MightyRoar_SkillID);

        }

    }

    public class RechargeSkill
    {

        public MachineScript skill;
        public int stock;
        public int maxStock;
        public float baseCooldown;
        public float cooldown = 0;

        public RechargeSkill(MachineScript skill, int maxStock, float baseCooldown)
        {
            this.skill = skill;
            this.stock = maxStock;
            this.maxStock = maxStock;
            this.baseCooldown = baseCooldown;
        }

    }

}
