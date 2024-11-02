using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.GUI;
using Panthera.MachineScripts;
using Panthera.Utils;
using System;

namespace Panthera.Skills.Actives
{
    public class FrontShield : MachineScript
    {

        //public float startTime;

        public FrontShield()
        {
            base.icon = PantheraAssets.FrontShieldSkill;
            base.name = PantheraTokens.Get("ability_FrontShieldName");
            base.baseCooldown = PantheraConfig.FrontShield_cooldown;
            base.removeStealth = false;
            base.desc1 = String.Format(Utils.PantheraTokens.Get("ability_FrontShieldDesc"), PantheraConfig.FrontShield_maxShieldHealthPercent * 100, PantheraConfig.FrontShield_rechargeDelayAfterDamage, PantheraConfig.FrontShield_rechargeDelayAfterDestroyed);
            base.desc2 = null;
            base.skillID = PantheraConfig.FrontShield_SkillID;
            base.requiredAbilityID = PantheraConfig.FrontShield_AbilityID;
            base.priority = PantheraConfig.FrontShield_priority;
            base.interruptPower = PantheraConfig.FrontShield_interruptPower;
        }

        public override bool CanBeUsed(PantheraObj ptraObj)
        {
            if (ptraObj.skillLocator.GetStock(PantheraConfig.FrontShield_SkillID) <= 0) return false;
            if (ptraObj.characterBody.frontShield <= 0) return false;
            return true;
        }

        public override void Start()
        {

            // Set the Cooldown //
            base.skillLocator.StartCooldown(PantheraConfig.FrontShield_SkillID);

            // Save the time //
            //this.startTime = Time.time;

            // Set the character to forward //
            base.characterDirection.forward = GetAimRay().direction;

            // Start the Aim mode //
            base.StartAimMode(1, false);

            // Retrive the Front Shield if deployed //
            if (base.pantheraObj.frontShieldDeployed == true)
                Passives.FrontShield.DeployShield(base.pantheraObj, false);

        }

        public override void Update()
        {

        }

        public override void FixedUpdate()
        {

            //float skillDuration = Time.time - this.startTime;
            //if (skillDuration >= PantheraConfig.FrontShield_skillDuration)
            //{
            //    base.EndScript();
            //    return;
            //}

            // Check if the Shield must stop //
            if (base.characterBody.frontShield <= 0 || base.wasInterrupted == true || base.inputBank.keysPressedList.HasFlag(KeysBinder.KeysEnum.Skill2) == false)
            {
                base.EndScript();
                return;
            }

            // Check if Rip is pressed //
            //if (base.inputBank.keysPressedList.Contains(KeysBinder.KeysEnum.Skill1) && base.pantheraObj.getAbilityLevel(PantheraConfig.ShieldBash_AbilityID) > 0)
            //{
            //    MachineScript script = Panthera.PantheraCharacter.characterSkills.getSkillByID(PantheraConfig.ShieldBash_SkillID);
            //    if (script.CanBeUsed(base.pantheraObj))
            //    {
            //        base.pantheraObj.skillsMachine2.TryScript((MachineScript)script.Clone());
            //        Skills.Passives.Stealth.UnStealth(base.pantheraObj);
            //    }
            //}

            // Restart the Aim mode //
            base.StartAimMode(1, false);

        }

        public override void Stop()
        {
            // Disable the Shield //
            //if (base.pantheraObj.frontShieldDeployed == false)
            //    Skills.Passives.FrontShield.DisableFrontShield(base.pantheraObj);
        }

    }
}
