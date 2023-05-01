using Panthera;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
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
        public Dictionary<int, float> cooldownList = new Dictionary<int, float>();

        public PantheraObj ptraObj;

        public float getMaxCooldown(int skillID)
        {
            PantheraSkill skill = ptraObj.activePreset.getSkillByID(skillID);
            if (skill == null) return 0;
            float maxCD = skill.cooldown;

            Inventory inventory = ptraObj.characterBody?.master?.inventory;
            if (inventory != null)
            {
                int count = inventory.GetItemCount(PantheraConfig.ItemChange_magazineIndex);
                if (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        maxCD *= PantheraConfig.ItemChange_magazinePercentCooldownReduction;
                    }
                }
                int count2 = inventory.GetItemCount(PantheraConfig.ItemChange_alienHeadIndex);
                if (count2 > 0)
                {
                    for (int i = 0; i < count2; i++)
                    {
                        maxCD *= PantheraConfig.ItemChange_alienHeadPercentCooldownReduction;
                    }
                }
                int count3 = inventory.GetItemCount(PantheraConfig.ItemChange_hardlightAfterburnerIndex);
                if (count3 > 0)
                {
                    for (int i = 0; i < count3; i++)
                    {
                        maxCD *= PantheraConfig.ItemChange_hardlightAfterburnerPercentCooldownReduction;
                    }
                }
                int count4 = inventory.GetItemCount(PantheraConfig.ItemChange_lightFluxPauldronIndex);
                if (count4 > 0)
                {
                    for (int i = 0; i < count4; i++)
                    {
                        maxCD *= PantheraConfig.ItemChange_lightFluxPauldronPercentCooldownReduction;
                    }
                }
                int count5 = inventory.GetItemCount(PantheraConfig.ItemChange_purityIndex);
                if (count5 > 0)
                {
                    for (int i = 0; i < count5; i++)
                    {
                        maxCD *= PantheraConfig.ItemChange_purityPercentCooldownReduction;
                    }
                }
                int count6 = inventory.GetItemCount(PantheraConfig.ItemChange_lysateCellIndex);
                if (count6 > 0)
                {
                    for (int i = 0; i < count6; i++)
                    {
                        maxCD *= PantheraConfig.ItemChange_lysateCellCooldownReduction;
                    }
                }
            }

            return maxCD;
        }

        public float getCooldownTime(int skillID)
        {
            if (cooldownList.ContainsKey(skillID) == true) return cooldownList[skillID];
            return 0;
        }

        public float getCooldownInSecond(int skillID)
        {
            if (cooldownList.ContainsKey(skillID) == false) return 0;
            float maxCD = getMaxCooldown(skillID);
            float timePassed = Time.time - cooldownList[skillID];
            float cooldown = Math.Max(0, maxCD - timePassed);
            return cooldown;
        }

        public void setCooldownTime(int skillID, float time)
        {
            if (cooldownList.ContainsKey(skillID) == true) cooldownList[skillID] = time;
            else cooldownList.Add(skillID, time);
        }

        public void setCooldownInSecond(int skillID, float seconds)
        {
            float maxCD = getMaxCooldown(skillID);
            if (cooldownList.ContainsKey(skillID) == true) cooldownList[skillID] = Time.time - maxCD + seconds;
            else cooldownList.Add(skillID, Time.time - maxCD + seconds);
        }

        public void startCooldown(int skillID)
        {
            if (cooldownList.ContainsKey(skillID) == true) cooldownList[skillID] = Time.time;
            else cooldownList.Add(skillID, Time.time);
        }

    }
}
