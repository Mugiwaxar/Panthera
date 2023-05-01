using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using static Rewired.Utils.Classes.Utility.ObjectInstanceTracker;

namespace Panthera.Skills
{
    public class Detection : MachineScript
    {

        public static List<String> ChestList = new List<string>() { "Chest1(Clone)", "Chest2(Clone)", "CategoryChestDamage(Clone)", "CategoryChestUtility(Clone)", "CategoryChestHealing(Clone)", "NewtStatue (1)", "CasinoChest(Clone)",
            "EquipmentBarrel(Clone)", "LunarChest(Clone)", "VoidChest(Clone)", "Duplicator(Clone)",
            "ShrineChance(Clone)", "ShrineChanceSnowy Variant(Clone)", "ShrineBlood(Clone)", "ShrineBloodSnowy Variant(Clone)", "ShrineHealing(Clone)", "ShrineBoss(Clone)", "ShrineBossSnowy Variant(Clone)", "ShrineGoldshoresAccess(Clone)", "ShrineCombat(Clone)",
            "Drone1Broken(Clone)", "Drone2Broken(Clone)", "Turret1Broken(Clone)", "MegaDroneBroken(Clone)", "MissileDroneBroken(Clone)", "FlameDroneBroken(Clone)", "EquipmentDroneBroken(Clone)"};

        public float startTime;
        public List<GameObject> objectsList = new List<GameObject>();
        public int scanIndex = 0;
        public bool prescience = false;

        public Detection()
        {

        }

        public static void Create()
        {
            // Create the Skill //
            PantheraSkill skill = new PantheraSkill();
            skill.skillID = PantheraConfig.Detection_SkillID;
            skill.name = "DETECTION_SKILL_NAME";
            skill.desc = "DETECTION_SKILL_DESC";
            skill.icon = Assets.Detection;
            skill.iconPrefab = Assets.ActiveSkillPrefab;
            skill.type = PantheraSkill.SkillType.active;
            skill.associatedSkill = typeof(Detection);
            skill.priority = PantheraConfig.Detection_priority;
            skill.interruptPower = PantheraConfig.Detection_interruptPower;
            skill.cooldown = PantheraConfig.Detection_coolDown;
            skill.skillMachine = 2;

            // Save this Skill //
            PantheraSkill.SkillDefsList.Add(skill.skillID, skill);
        }

        public override PantheraSkill getSkillDef()
        {
            return base.pantheraObj.activePreset.getSkillByID(PantheraConfig.Detection_SkillID);
        }

        public override bool CanBeUsed(PantheraObj ptraObj)
        {
            base.pantheraObj = ptraObj;
            if (ptraObj.skillLocator.getCooldownInSecond(this.getSkillDef().skillID) > 0) return false;
            return true;
        }

        public override void Start()
        {

            // Save the time //
            this.startTime = Time.time;

            // Check if must Start or Stop Detection //
            if (base.pantheraObj.detectionActivated == false)
            {
                // Start the Detection //
                Passives.Detection.EnableDetection(base.pantheraObj);
            }
            else
            {
                // Stop the Detection //
                Passives.Detection.DisableDetection(base.pantheraObj);
                return;
            }

            // Get all Game Objects //
            this.objectsList = GameObject.FindObjectsOfType<GameObject>().ToList();

            // Check Prescience Ability //
            if (base.pantheraObj.activePreset.getAbilityLevel(PantheraConfig.PrescienceAbilityID) > 0)
                this.prescience = true;

        }

        public override void FixedUpdate()
        {

            // Stop if the Skill last too long //
            float skillDuration = Time.time - this.startTime;
            if (skillDuration >= PantheraConfig.Detection_skillMaxDuration)
            {
                base.EndScript();
                return;
            }

            // Stop if they are not Object to scan left //
            if (this.scanIndex >= this.objectsList.Count)
            {
                if (skillDuration >= PantheraConfig.Detection_skillMinDuration)
                    base.EndScript();
                return;
            }

            // Scan all Objects //
            int scannedItem = 0;
            while (this.scanIndex < this.objectsList.Count && scannedItem <= PantheraConfig.Detection_maxScanPerFrame)
            {

                // Get the next Object //
                GameObject currentObj = this.objectsList[this.scanIndex];
                scannedItem++;
                this.scanIndex++;

                // Check if Body //
                if (ScanBody(base.pantheraObj, currentObj) == true)
                    break;

                // Check Prescience Ability //
                if (this.prescience == true)
                {

                    // Check if Purchase //
                    if (ScanPurchaseObjects(base.pantheraObj, currentObj) == true)
                        break;

                    // Check if Barrel //
                    if (ScanBarrels(base.pantheraObj, currentObj) == true)
                        break;

                    // Check if Triple Shop Base //
                    if (ScanTripleShopBases(base.pantheraObj, currentObj) == true)
                        break;

                    // Check if Triple Shop Terminal //
                    if (ScanTripleShopTerminals(base.pantheraObj, currentObj) == true)
                        break;

                    // Check if Scrapper //
                    if (ScanScrapper(base.pantheraObj, currentObj) == true)
                        break;

                    // Check if Teleporter //
                    if (ScanTeleporter(base.pantheraObj, currentObj) == true)
                        break;

                }

            }

        }

        public static bool ScanBody(PantheraObj ptra, GameObject obj)
        {
            if (obj.GetComponent<CharacterBody>() != null)
            {
                if (obj == ptra.gameObject) return false;
                XRayComponent component = obj.GetComponent<XRayComponent>();
                if (component == null) component = obj.gameObject.AddComponent<XRayComponent>();
                component.ptraObj = ptra;
                component.type = XRayComponent.XRayObjectType.Body;
                return true;
            }
            return false;
        }

        public static bool ScanPurchaseObjects(PantheraObj ptra, GameObject obj)
        {
            if (obj.GetComponent<PurchaseInteraction>() != null)
            {
                if (ChestList.Contains(obj.name) == false) return false;
                XRayComponent component = obj.GetComponent<XRayComponent>();
                if (component == null) component = obj.gameObject.AddComponent<XRayComponent>();
                component.ptraObj = ptra;
                component.type = XRayComponent.XRayObjectType.Purchase;
                return true;
            }
            return false;
        }

        public static bool ScanBarrels(PantheraObj ptra, GameObject obj)
        {
            if (obj.GetComponent<BarrelInteraction>() != null)
            {
                if (obj.name != "Barrel1(Clone)" && obj.name != "VoidCoinBarrel(Clone)") return false;
                XRayComponent component = obj.GetComponent<XRayComponent>();
                if (component == null) component = obj.gameObject.AddComponent<XRayComponent>();
                component.ptraObj = ptra;
                component.type = XRayComponent.XRayObjectType.Barrel;
                return true;
            }
            return false;
        }

        public static bool ScanTripleShopBases(PantheraObj ptra, GameObject obj)
        {
            if (obj.GetComponent<MultiShopController>() != null)
            {
                if (obj.name != "TripleShop(Clone)" && obj.name != "TripleShopLarge(Clone)" && obj.name != "TripleShopEquipment(Clone)") return false;
                XRayComponent component = obj.GetComponent<XRayComponent>();
                if (component == null) component = obj.gameObject.AddComponent<XRayComponent>();
                component.ptraObj = ptra;
                component.type = XRayComponent.XRayObjectType.TripleShopBase;
                return true;
            }
            return false;
        }

        public static bool ScanTripleShopTerminals(PantheraObj ptra, GameObject obj)
        {
            if (obj.GetComponent<ShopTerminalBehavior>() != null)
            {
                if (obj.name != "MultiShopTerminal(Clone)" && obj.name != "MultiShopLargeTerminal(Clone)" && obj.name != "MultiShopEquipmentTerminal(Clone)") return false;
                XRayComponent component = obj.GetComponent<XRayComponent>();
                if (component == null) component = obj.gameObject.AddComponent<XRayComponent>();
                component.ptraObj = ptra;
                component.type = XRayComponent.XRayObjectType.TripleShopTerminal;
                return true;
            }
            return false;
        }

        public static bool ScanScrapper(PantheraObj ptra, GameObject obj)
        {
            if (obj.GetComponent<PurchaseInteraction>() != null)
                if (obj is ScrapperController)
            {
                if (obj.name != "Scrapper(Clone)") return false;
                XRayComponent component = obj.GetComponent<XRayComponent>();
                if (component == null) component = obj.gameObject.AddComponent<XRayComponent>();
                component.ptraObj = ptra;
                component.type = XRayComponent.XRayObjectType.Scrapper;
                return true;
            }
            return false;
        }

        public static bool ScanTeleporter(PantheraObj ptra, GameObject obj)
        {
            if (obj.GetComponent<TeleporterInteraction>() != null)
            {
                if (obj.name == "Teleporter1(Clone)")
                {
                    XRayComponent component1 = obj.GetComponent<XRayComponent>();
                    if (component1 == null) component1 = obj.gameObject.AddComponent<XRayComponent>();
                    component1.ptraObj = ptra;
                    component1.type = XRayComponent.XRayObjectType.teleporterPart1;

                    Transform modelTransform = obj.GetComponent<ModelLocator>()?.modelTransform;
                    if (modelTransform != null)
                    {
                        Transform child1 = modelTransform.transform.FindChild("TeleporterProngMesh");
                        if (child1 != null)
                        {
                            XRayComponent component2 = child1.GetComponent<XRayComponent>();
                            if (component2 == null) component2 = child1.gameObject.AddComponent<XRayComponent>();
                            component2.ptraObj = ptra;
                            component2.type = XRayComponent.XRayObjectType.teleporterPart2;
                        }

                        Transform child2 = modelTransform.transform.FindChild("SurfaceHeight");
                        if (child2 != null)
                        {
                            XRayComponent component3 = child2.GetComponent<XRayComponent>();
                            if (component3 == null) component3 = child2.gameObject.AddComponent<XRayComponent>();
                            component3.ptraObj = ptra;
                            component3.type = XRayComponent.XRayObjectType.teleporterPart3;
                        }
                    }
                    return true;
                }
                if (obj.name == "LunarTeleporter Variant(Clone)")
                {
                    XRayComponent component1 = obj.GetComponent<XRayComponent>();
                    if (component1 == null) component1 = obj.gameObject.AddComponent<XRayComponent>();
                    component1.ptraObj = ptra;
                    component1.type = XRayComponent.XRayObjectType.lunarTeleporterPart1;

                    Transform modelTransform = obj.GetComponent<ModelLocator>()?.modelTransform;
                    if (modelTransform != null)
                    {
                        Transform child1 = modelTransform.transform.FindChild("ProngContainer")?.Find("LunarTeleporterProngs(Clone)");
                        if (child1 != null)
                        {
                            XRayComponent component2 = child1.GetComponent<XRayComponent>();
                            if (component2 == null) component2 = child1.gameObject.AddComponent<XRayComponent>();
                            component2.ptraObj = ptra;
                            component2.type = XRayComponent.XRayObjectType.lunarTeleporterPart2;
                        }

                        Transform child2 = modelTransform.transform.FindChild("SurfaceHeight");
                        if (child2 != null)
                        {
                            XRayComponent component3 = child2.GetComponent<XRayComponent>();
                            if (component3 == null) component3 = child2.gameObject.AddComponent<XRayComponent>();
                            component3.ptraObj = ptra;
                            component3.type = XRayComponent.XRayObjectType.lunarTeleporterPart3;
                        }
                    }
                    return true;
                }
                return false;
            }
            return false;
        }

    }
}
