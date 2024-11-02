using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.MachineScripts;
using Panthera.Utils;
using UnityEngine;

namespace Panthera.Skills.Actives
{
    public class Guardian : MachineScript
    {

        public float startTime;

        public Guardian()
        {
            base.icon = PantheraAssets.GuardianSkill;
            base.name = PantheraTokens.Get("ability_GuardianName");
            base.baseCooldown = PantheraConfig.Guardian_cooldown;
            base.removeStealth = false;
            base.desc1 = string.Format(PantheraTokens.Get("ability_GuardianDesc"), PantheraConfig.Guardian_increasedArmor * 100, PantheraConfig.Guardian_increasedHealthRegen * 100, (1 - PantheraConfig.Guardian_barrierDecayRatePercent) * 100);
            base.desc2 = null;
            base.skillID = PantheraConfig.Guardian_SkillID;
            base.requiredAbilityID = PantheraConfig.Guardian_AbilityID;
            base.priority = PantheraConfig.Guardian_priority;
            base.interruptPower = PantheraConfig.Guardian_interruptPower;
        }

        public override bool CanBeUsed(PantheraObj ptraObj)
        {
            if (ptraObj.skillLocator.GetStock(PantheraConfig.Guardian_SkillID) <= 0) return false;
            return true;
        }

        public override void Start()
        {

            // Save the time //
            this.startTime = Time.time;

            // Set the Mode Guardian on/off //
            if (base.pantheraObj.guardianMode == true)
            {
                Skills.Passives.GuardianMode.GuardianOff(base.pantheraObj);
            }
            else
            {
                Skills.Passives.GuardianMode.GuardianOn(base.pantheraObj);
            }
        }

        public override void Update()
        {

        }

        public override void FixedUpdate()
        {

            // Stop if the duration is reached //
            float skillDuration = Time.time - this.startTime;
            if (skillDuration >= PantheraConfig.Fury_duration)
            {
                base.EndScript();
                return;
            }

        }

        public override void Stop()
        {

        }

    }
}
