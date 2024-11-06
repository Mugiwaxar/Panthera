using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.MachineScripts;
using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Skills.Actives
{
    public class Ambition : MachineScript
    {

        public float startTime;

        public Ambition()
        {
            base.icon = PantheraAssets.AmbitionSkill;
            base.name = PantheraTokens.Get("ability_AmbitionName");
            base.baseCooldown = PantheraConfig.Ambition_cooldown;
            base.removeStealth = false;
            base.desc1 = String.Format(Utils.PantheraTokens.Get("ability_AmbitionDesc1"), PantheraConfig.Ambition_buffDuration);
            base.skillID = PantheraConfig.Ambition_SkillID;
            base.requiredAbilityID = PantheraConfig.Ambition_AbilityID;
            base.priority = PantheraConfig.Ambition_priority;
            base.interruptPower = PantheraConfig.Ambition_interruptPower;
        }

        public override bool CanBeUsed(PantheraObj ptraObj)
        {
            if (ptraObj.skillLocator.getStock(PantheraConfig.Ambition_SkillID) <= 0) return false;
            return true;
        }

        public override void Start()
        {

            // Save the time //
            this.startTime = Time.time;

            // Set the Mode Ambition on //
            Skills.Passives.AmbitionMode.AmbitionOn(base.pantheraObj);

        }

        public override void Update()
        {

        }

        public override void FixedUpdate()
        {

            // Stop if the duration is reached //
            float skillDuration = Time.time - this.startTime;
            if (skillDuration >= PantheraConfig.Ambition_duration)
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
