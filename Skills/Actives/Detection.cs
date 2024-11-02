using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.MachineScripts;
using Panthera.Utils;
using RoR2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Panthera.Skills.Actives
{
    public class Detection : MachineScript
    {
        public static readonly List<string> ChestList = [ 
            "Chest1(Clone)", "Chest2(Clone)", "CategoryChestDamage(Clone)", "CategoryChestUtility(Clone)", "CategoryChestHealing(Clone)", "NewtStatue (1)", "CasinoChest(Clone)",
            "EquipmentBarrel(Clone)", "LunarChest(Clone)", "VoidChest(Clone)", "Duplicator(Clone)",
            "ShrineChance(Clone)", "ShrineChanceSnowy Variant(Clone)", "ShrineBlood(Clone)", "ShrineBloodSnowy Variant(Clone)", "ShrineHealing(Clone)", "ShrineBoss(Clone)", "ShrineBossSnowy Variant(Clone)", "ShrineGoldshoresAccess(Clone)", "ShrineCombat(Clone)",
            "Drone1Broken(Clone)", "Drone2Broken(Clone)", "Turret1Broken(Clone)", "MegaDroneBroken(Clone)", "MissileDroneBroken(Clone)", "FlameDroneBroken(Clone)", "EquipmentDroneBroken(Clone)"];

        public float totalScanTime, lastDetectionScanTime;
        public bool scanFinished;
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
            if (ptraObj.detectionMode == false && ptraObj.skillLocator.GetCooldown(PantheraConfig.Detection_SkillID) > maxTime - PantheraConfig.Detection_cooldown) return false;
            return true;
        }

        public override void Start()
        {
            // Save the time //
            totalScanTime = 0f;
            lastDetectionScanTime = 0f;

            // Set the Max Time //
            float maxTime = Skills.Passives.Detection.GetDetectionMaxTime(base.pantheraObj);
            base.skillLocator.SetMaxCooldown(PantheraConfig.Detection_SkillID, maxTime);

            // Check if must Start or Stop Detection //
            if (pantheraObj.detectionMode == false)
            {
                // Start the Detection //
                Passives.Detection.EnableDetection(pantheraObj);

                // Check Sixth Sense Ability //
                if (this.pantheraObj.GetAbilityLevel(PantheraConfig.SixthSense_AbilityID) > 0)
                    sixthSense = true;

                this.pantheraObj.StartCoroutine(ScanAll());
            }
            else
            {
                // Stop the Detection //
                Passives.Detection.DisableDetection(pantheraObj);
            }
        }

        public IEnumerator ScanAll()
        {
            // Scan all Objects //
            // Check if Body //
            ScanBody(pantheraObj);

            // Check Prescience Ability //
            if (sixthSense == true)
            {
                // Check if Teleporter //
                yield return new WaitForFixedUpdate();
                ScanTeleporter(pantheraObj);

                // Check if Scrapper //
                yield return new WaitForFixedUpdate();
                ScanScrapper(pantheraObj);

                // Check if Purchase //
                yield return new WaitForFixedUpdate();
                ScanPurchaseObjects(pantheraObj);

                // Check if Barrel //
                yield return new WaitForFixedUpdate();
                ScanBarrels(pantheraObj);

                // Check if Triple Shop Base //
                yield return new WaitForFixedUpdate();
                ScanTripleShopBases(pantheraObj);

                // Check if Triple Shop Terminal //
                yield return new WaitForFixedUpdate();
                ScanTripleShopTerminals(pantheraObj);
            }
            scanFinished = true;
        }
        
        public override void FixedUpdate()
        {
            // Stop if the Skill last too long //
            totalScanTime += Time.fixedDeltaTime;
            if ((scanFinished && totalScanTime >= PantheraConfig.Detection_skillMinDuration) || totalScanTime >= PantheraConfig.Detection_skillMaxDuration || !base.pantheraObj.detectionMode)
            {
                EndScript();
                return;
            }

            // Check if Rescan //
            lastDetectionScanTime += Time.fixedDeltaTime;
            if (lastDetectionScanTime > PantheraConfig.Detection_scanRate && base.pantheraObj.detectionMode == true)
            {
                this.lastDetectionScanTime = 0;
                ScanBody(base.pantheraObj);
            }
        }

        public static void GetOrAddXrayComponent(PantheraObj ptra, GameObject obj, XRayComponent.XRayObjectType type)
        {
            if (!obj.TryGetComponent<XRayComponent>(out var xray))
                xray = obj.AddComponent<XRayComponent>();

            xray.ptraObj = ptra;
            xray.type = type;
        }

        public static void ScanBody(PantheraObj ptra)
        {
            foreach (var body in CharacterBody.instancesList)
            {
                if (body)
                    GetOrAddXrayComponent(ptra, body.gameObject, XRayComponent.XRayObjectType.Body);
            }
        }

        public static void ScanPurchaseObjects(PantheraObj ptra)
        {
            foreach (var interactable in InstanceTracker.GetInstancesList<PurchaseInteraction>())
            {
                if (interactable && ChestList.Contains(interactable.gameObject.name))
                    GetOrAddXrayComponent(ptra, interactable.gameObject, XRayComponent.XRayObjectType.Purchase);
            }
        }

        public static void ScanBarrels(PantheraObj ptra)
        {
            foreach (var interactable in InstanceTracker.GetInstancesList<BarrelInteraction>())
            {
                if (interactable && interactable.name is "Barrel1(Clone)" or "VoidCoinBarrel(Clone)")
                    GetOrAddXrayComponent(ptra, interactable.gameObject, XRayComponent.XRayObjectType.Barrel);
            }
        }

        public static void ScanTripleShopBases(PantheraObj ptra)
        {
            foreach (var interactable in InstanceTracker.GetInstancesList<MultiShopController>())
            {
                if (interactable && interactable.name is "TripleShop(Clone)" or "TripleShopLarge(Clone)" or "TripleShopEquipment(Clone)")
                    GetOrAddXrayComponent(ptra, interactable.gameObject, XRayComponent.XRayObjectType.TripleShopBase);
            }
        }
        public static void ScanTripleShopTerminals(PantheraObj ptra)
        {
            foreach (var interactable in InstanceTracker.GetInstancesList<ShopTerminalBehavior>())
            {
                if (interactable && interactable.name is "MultiShopTerminal(Clone)" or "MultiShopLargeTerminal(Clone)" or "MultiShopEquipmentTerminal(Clone)")
                    GetOrAddXrayComponent(ptra, interactable.gameObject, XRayComponent.XRayObjectType.TripleShopTerminal);
            }
        }
        public static void ScanScrapper(PantheraObj ptra)
        {
            foreach (var interactable in InstanceTracker.GetInstancesList<ShopTerminalBehavior>())
            {
                if (interactable && interactable.name is "Scrapper(Clone)")
                    GetOrAddXrayComponent(ptra, interactable.gameObject, XRayComponent.XRayObjectType.Scrapper);
            }
        }

        public static void ScanTeleporter(PantheraObj ptra)
        {
            if (TeleporterInteraction.instance)
            {
                var obj = TeleporterInteraction.instance.gameObject;
                if (obj.name is "Teleporter1(Clone)")
                {
                    GetOrAddXrayComponent(ptra, obj, XRayComponent.XRayObjectType.teleporterPart1);

                    var modelTransform = obj.TryGetComponent<ModelLocator>(out var loc) ? loc.modelTransform : null;
                    if (modelTransform != null)
                    {
                        var child1 = modelTransform.Find("TeleporterProngMesh");
                        if (child1 != null)
                            GetOrAddXrayComponent(ptra, child1.gameObject, XRayComponent.XRayObjectType.teleporterPart2);

                        var child2 = modelTransform.Find("SurfaceHeight");
                        if (child2 != null)
                            GetOrAddXrayComponent(ptra, child2.gameObject, XRayComponent.XRayObjectType.teleporterPart3);
                    }
                }
                else if (obj.name is "LunarTeleporter Variant(Clone)")
                {
                    GetOrAddXrayComponent(ptra, obj, XRayComponent.XRayObjectType.lunarTeleporterPart1);

                    var modelTransform = obj.TryGetComponent<ModelLocator>(out var loc) ? loc.modelTransform : null;
                    if (modelTransform != null)
                    {
                        var child1 = modelTransform.Find("ProngContainer/LunarTeleporterProngs(Clone)");
                        if (child1 != null)
                            GetOrAddXrayComponent(ptra, child1.gameObject, XRayComponent.XRayObjectType.lunarTeleporterPart2);

                        var child2 = modelTransform.Find("SurfaceHeight");
                        if (child2 != null)
                            GetOrAddXrayComponent(ptra, child2.gameObject, XRayComponent.XRayObjectType.lunarTeleporterPart3);
                    }
                }
            }
        }

    }
}
