using HarmonyLib;
using Panthera.Machines;
using Panthera.MachineScripts;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Components
{
    public class PantheraSkill
    {

        public enum SkillType
        {
            passive,
            active
        }

        // (int SkillID 1 - ??, PantheraSkill Object) Represent the list of all Panthera Skill Definitions //
        public static Dictionary<int, PantheraSkill> SkillDefsList = new Dictionary<int, PantheraSkill>();

        // (int SkillID 1 - ??, LastUsedTime) Represent the Current Cooldown of all Skills //
        public static Dictionary<int, float> CooldownList = new Dictionary<int, float>();

        public int skillID;
        public string name;
        public string desc;
        public Sprite icon;
        public GameObject iconPrefab;
        public SkillType type;
        public Type associatedSkill;
        public ScriptPriority priority = ScriptPriority.NoPriority;
        public ScriptPriority interruptPower = ScriptPriority.NoPriority;
        public int unlockLevel = 1;
        public float _cooldown = 0;
        public float cooldown
        {
            set
            {
                _cooldown = value;
            }
            get
            {
                Inventory inventory = PantheraObj.Instance?.characterBody?.master?.inventory;
                if(inventory != null)
                {
                    float resultCooldown = _cooldown;
                    int count = inventory.GetItemCount(PantheraConfig.ItemChange_magazineIndex);
                    if (count > 0)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            resultCooldown *= PantheraConfig.ItemChange_magazinePercentCooldownReduction;
                        }
                    }
                    int count2 = inventory.GetItemCount(PantheraConfig.ItemChange_alienHeadIndex);
                    if (count2 > 0)
                    {
                        for (int i = 0; i < count2; i++)
                        {
                            resultCooldown *= PantheraConfig.ItemChange_alienHeadPercentCooldownReduction;
                        }
                    }
                    int count3 = inventory.GetItemCount(PantheraConfig.ItemChange_hardlightAfterburnerIndex);
                    if (count3 > 0)
                    {
                        for (int i = 0; i < count3; i++)
                        {
                            resultCooldown *= PantheraConfig.ItemChange_hardlightAfterburnerPercentCooldownReduction;
                        }
                    }
                    int count4 = inventory.GetItemCount(PantheraConfig.ItemChange_lightFluxPauldronIndex);
                    if (count4 > 0)
                    {
                        for (int i = 0; i < count4; i++)
                        {
                            resultCooldown *= PantheraConfig.ItemChange_lightFluxPauldronPercentCooldownReduction;
                        }
                    }
                    int count5 = inventory.GetItemCount(PantheraConfig.ItemChange_purityIndex);
                    if (count5 > 0)
                    {
                        for (int i = 0; i < count5; i++)
                        {
                            resultCooldown *= PantheraConfig.ItemChange_purityPercentCooldownReduction;
                        }
                    }
                    int count6 = inventory.GetItemCount(PantheraConfig.ItemChange_lysateCellIndex);
                    if (count6 > 0)
                    {
                        for (int i = 0; i < count6; i++)
                        {
                            resultCooldown *= PantheraConfig.ItemChange_lysateCellCooldownReduction;
                        }
                    }
                    return resultCooldown;
                }

                return _cooldown;
            }
        }
        public int requiredEnergy = 0;
        public int requiredPower = 0;
        public int requiredFury = 0;
        public int requiredCombo = 0;

        public static void RegisterSkills()
        {
            Skills.Rip.Create();
            Skills.AirCleave.Create();
            Skills.Leap.Create();
            Skills.MightyRoar.Create();
            Skills.ClawsStorm.Create();
            Skills.FrontShield.Create();
            Skills.Prowl.Create();
            Skills.FuriousBite.Create();
        }

        public static float GetCooldownTime(int skillID)
        {
            if (CooldownList.ContainsKey(skillID) == true) return CooldownList[skillID];
            return 0;
        }

        public static void SetCooldownTime(int skillID, float time)
        {
            if (CooldownList.ContainsKey(skillID) == true) CooldownList[skillID] = time;
            else CooldownList.Add(skillID, time);
        }

        //public static void OnRunRecharge(On.RoR2.GenericSkill.orig_RunRecharge orig, GenericSkill self, float dt)
        //{
        //    // Get the Panthera object //
        //    PantheraObj ptraObj = self.characterBody.GetComponent<PantheraObj>();

        //    // Check if this is the right object //
        //    if (ptraObj == null)
        //    {
        //        orig(self, dt);
        //        return;
        //    }

        //    // Get the skills machine //
        //    PantheraMachine machine = ptraObj.skillsMachine;

        //    // Recharge the skill //
        //    if (self.stock < self.maxStock)
        //    {
        //        if (self.beginSkillCooldownOnSkillEnd == false)
        //        {
        //            self.rechargeStopwatch += dt;
        //        }
        //        else if (machine.GetCurrentScript() == null)
        //        {
        //            self.rechargeStopwatch += dt;
        //        }
        //        else if (machine.GetCurrentScript() != null && machine.GetCurrentScript().GetType() != ((self.skillDef) as PantheraSkillDef).skillType)
        //        {
        //            self.rechargeStopwatch += dt;
        //        }
        //        if (self.rechargeStopwatch >= self.finalRechargeInterval)
        //        {
        //            self.RestockSteplike();
        //        }
        //    }

        //}

    }
}
