using Panthera;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.MachineScripts;
using Panthera.NetworkMessages;
using Panthera.OldSkills;
using Panthera.Skills.Actives;
using Panthera.Skills.Passives;
using Panthera.Utils;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using static Rewired.Utils.Classes.Utility.ObjectInstanceTracker;

namespace Panthera.Skills.Actives
{
    public class Detection : MachineScript
    {

        public static List<string> ChestList = new List<string>() { "Chest1(Clone)", "Chest2(Clone)", "CategoryChestDamage(Clone)", "CategoryChestUtility(Clone)", "CategoryChestHealing(Clone)", "NewtStatue (1)", "CasinoChest(Clone)",
            "EquipmentBarrel(Clone)", "LunarChest(Clone)", "VoidChest(Clone)", "Duplicator(Clone)",
            "ShrineChance(Clone)", "ShrineChanceSnowy Variant(Clone)", "ShrineBlood(Clone)", "ShrineBloodSnowy Variant(Clone)", "ShrineHealing(Clone)", "ShrineBoss(Clone)", "ShrineBossSnowy Variant(Clone)", "ShrineGoldshoresAccess(Clone)", "ShrineCombat(Clone)",
            "Drone1Broken(Clone)", "Drone2Broken(Clone)", "Turret1Broken(Clone)", "MegaDroneBroken(Clone)", "MissileDroneBroken(Clone)", "FlameDroneBroken(Clone)", "EquipmentDroneBroken(Clone)"};

        public float startTime;
        public List<GameObject> objectsList = new List<GameObject>();
        public int scanIndex = 0;
        public bool sixthSense = false;

        public Detection()
        {
            base.icon = PantheraAssets.DetectionSkill;
            base.name = PantheraTokens.Get("ability_DetectionName");
            base.baseCooldown = PantheraConfig.Detection_maxTime;
            base.removeStealth = false;
            base.desc1 = String.Format(Utils.PantheraTokens.Get("ability_DetectionDesc"), PantheraConfig.Detection_maxTime);
            base.desc2 = null;
            base.machineToUse = 2;
            base.skillID = PantheraConfig.Detection_SkillID;
            base.requiredAbilityID = PantheraConfig.Detection_AbilityID;
            base.priority = PantheraConfig.Detection_priority;
            base.interruptPower = PantheraConfig.Detection_interruptPower;
        }

        public override bool CanBeUsed(PantheraObj ptraObj)
        {
            float maxTime = Skills.Passives.Detection.GetDetectionMaxTime(ptraObj);
            if (ptraObj.detectionMode == false && ptraObj.skillLocator.getCooldown(PantheraConfig.Detection_SkillID) > maxTime - PantheraConfig.Detection_cooldown) return false;
            return true;
        }

        public override void Start()
        {

            // Save the time //
            startTime = Time.time;

            // Set the Max Time //
            float maxTime = Skills.Passives.Detection.GetDetectionMaxTime(base.pantheraObj);
            base.skillLocator.setMaxCooldown(PantheraConfig.Detection_SkillID, maxTime);

            // Check if must Start or Stop Detection //
            if (pantheraObj.detectionMode == false)
            {
                // Start the Detection //
                Passives.Detection.EnableDetection(pantheraObj);
            }
            else
            {
                // Stop the Detection //
                Passives.Detection.DisableDetection(pantheraObj);
                return;
            }

            // Get all Game Objects //
            this.objectsList.Clear();
            this.objectsList = UnityEngine.Object.FindObjectsOfType<GameObject>().ToList();

            // Check Sixth Sense Ability //
            if (this.pantheraObj.profileComponent.getAbilityLevel(PantheraConfig.SixthSense_AbilityID) > 0)
                sixthSense = true;

        }

        public override void FixedUpdate()
        {

            // Stop if the Skill last too long //
            float skillDuration = Time.time - startTime;
            if (skillDuration >= PantheraConfig.Detection_skillMaxDuration)
            {
                EndScript();
                return;
            }

            // Stop if they are not Object to scan left //
            if (scanIndex >= objectsList.Count)
            {
                if (skillDuration >= PantheraConfig.Detection_skillMinDuration)
                    EndScript();
                return;
            }

            // Scan all Objects //
            int scannedItem = 0;
            while (scanIndex < objectsList.Count && scannedItem <= PantheraConfig.Detection_maxScanPerFrame)
            {

                // Get the next Object //
                GameObject currentObj = objectsList[scanIndex];
                scannedItem++;
                scanIndex++;

                // Check the Object //
                if (currentObj == null)
                    continue;

                // Check if Body //
                if (ScanBody(pantheraObj, currentObj) == true)
                    break;

                // Check Prescience Ability //
                if (sixthSense == true)
                {

                    // Check if Purchase //
                    if (ScanPurchaseObjects(pantheraObj, currentObj) == true)
                        break;

                    // Check if Barrel //
                    if (ScanBarrels(pantheraObj, currentObj) == true)
                        break;

                    // Check if Triple Shop Base //
                    if (ScanTripleShopBases(pantheraObj, currentObj) == true)
                        break;

                    // Check if Triple Shop Terminal //
                    if (ScanTripleShopTerminals(pantheraObj, currentObj) == true)
                        break;

                    // Check if Scrapper //
                    if (ScanScrapper(pantheraObj, currentObj) == true)
                        break;

                    // Check if Teleporter //
                    if (ScanTeleporter(pantheraObj, currentObj) == true)
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
                        Transform child1 = modelTransform.transform.Find("TeleporterProngMesh");
                        if (child1 != null)
                        {
                            XRayComponent component2 = child1.GetComponent<XRayComponent>();
                            if (component2 == null) component2 = child1.gameObject.AddComponent<XRayComponent>();
                            component2.ptraObj = ptra;
                            component2.type = XRayComponent.XRayObjectType.teleporterPart2;
                        }

                        Transform child2 = modelTransform.transform.Find("SurfaceHeight");
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
                        Transform child1 = modelTransform.transform.Find("ProngContainer")?.Find("LunarTeleporterProngs(Clone)");
                        if (child1 != null)
                        {
                            XRayComponent component2 = child1.GetComponent<XRayComponent>();
                            if (component2 == null) component2 = child1.gameObject.AddComponent<XRayComponent>();
                            component2.ptraObj = ptra;
                            component2.type = XRayComponent.XRayObjectType.lunarTeleporterPart2;
                        }

                        Transform child2 = modelTransform.transform.Find("SurfaceHeight");
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
