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
    public class Fury : MachineScript
    {

        public float startTime;

        public Fury()
        {
            base.icon = Assets.FurySkill;
            base.name = PantheraTokens.Get("ability_FuryName");
            base.baseCooldown = PantheraConfig.Fury_cooldown;
            base.removeStealth = false;
            base.desc1 = string.Format(PantheraTokens.Get("ability_FuryDesc"), PantheraConfig.Fury_increasedAttackSpeed * 100, PantheraConfig.Fury_increasedMoveSpeed * 100);
            base.desc2 = null;
            base.skillID = PantheraConfig.Fury_SkillID;
            base.requiredAbilityID = PantheraConfig.Fury_AbilityID;
            base.priority = PantheraConfig.Fury_priority;
            base.interruptPower = PantheraConfig.Fury_interruptPower;
        }

        public override bool CanBeUsed(PantheraObj ptraObj)
        {
            if (ptraObj.skillLocator.getStock(PantheraConfig.Fury_SkillID) <= 0) return false;
            if (ptraObj.characterBody.fury <= 0) return false;
            return true;
        }

        public override void Start()
        {

            // Save the time //
            this.startTime = Time.time;

            // Set the Mode Fury on/off //
            if (base.pantheraObj.furyMode == true)
            {
                Skills.Passives.FuryMode.FuryOff(base.pantheraObj);
            }
            else
            {
                Skills.Passives.FuryMode.FuryOn(base.pantheraObj);
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
