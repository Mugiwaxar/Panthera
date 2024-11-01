using Panthera;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.GUI;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.OldSkills;
using Panthera.Skills;
using Panthera.Skills.Actives;
using Panthera.Utils;
using R2API.Networking;
using R2API.Networking.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Skills.Actives
{
    public class Prowl : MachineScript
    {

        public float startTime;

        public Prowl()
        {
            base.icon = PantheraAssets.ProwlSkill;
            base.name = PantheraTokens.Get("ability_ProwlName");
            base.baseCooldown = PantheraConfig.Prowl_coolDown;
            base.desc1 = Utils.PantheraTokens.Get("ability_ProwlDesc");
            base.desc2 = null;
            base.machineToUse = 1;
            base.removeStealth = false;
            base.skillID = PantheraConfig.Prowl_SkillID;
            base.requiredAbilityID = PantheraConfig.Prowl_AbilityID;
            base.priority = PantheraConfig.Prowl_priority;
            base.interruptPower = PantheraConfig.Prowl_interruptPower;
        }

        public override bool CanBeUsed(PantheraObj ptraObj)
        {
            if (ptraObj.characterBody.GetBuffCount(Buff.EclipseBuff) <= 0 && ptraObj.GetPassiveScript().isOutOfCombat == false) return false;
            if (ptraObj.skillLocator.getStock(PantheraConfig.Prowl_SkillID) <= 0) return false;
            return true;
        }

        public override void Start()
        {

            // Save the time //
            this.startTime = Time.time;

            // Steal the Character //
            if (this.pantheraObj.stealthed == true)
            {
                Passives.Stealth.UnStealth(this.pantheraObj);
            }
            else
            {
                Passives.Stealth.DoStealth(this.pantheraObj);
            }

        }

        public override void Update()
        {

        }

        public override void FixedUpdate()
        {
            float skillDuration = Time.time - this.startTime;
            if (skillDuration >= PantheraConfig.Prowl_skillDuration)
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
