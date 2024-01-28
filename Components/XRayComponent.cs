using Panthera.Base;
using Panthera.BodyComponents;
using R2API;
using RoR2;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace Panthera.Components
{
    public class XRayComponent : MonoBehaviour
    {

        public enum XRayObjectType
        {
            Body,
            Purchase,
            Barrel,
            TripleShopBase,
            TripleShopTerminal,
            Scrapper,
            teleporterPart1,
            teleporterPart2,
            teleporterPart3,
            lunarTeleporterPart1,
            lunarTeleporterPart2,
            lunarTeleporterPart3
        }

        public PantheraObj ptraObj;
        public XRayObjectType type;
        public CharacterModel characterModel;
        public Renderer renderer;
        public CharacterModel.RendererInfo[] origRenInfo;
        public CharacterModel.RendererInfo[] changedRenInfo;
        public Material[] origMats;
        public Material[] changedMats;
        public Color32 defaultColor = PantheraConfig.DetectionDefaultColor;
        public Gradient gradient = new Gradient();
        public int origLayer = 0;
        public bool dynOcclusion;
        public bool wasActive = false;
        public float lastHealth = UnityEngine.Mathf.NegativeInfinity;
        public int chestPurchaseState = -2;
        public int barrelWasOpened = -2;
        public int tripleShopBasePurchaseState = -2;
        public int tripleShopTerminalPurchaseState = -2;

        public void Start()
        {
            
            // Check the Panthera Object //
            if (this.ptraObj == null) return;

            // Find All Components //
            ModelLocator modelLocator = base.GetComponent<ModelLocator>();
            Transform modelTransform = modelLocator?.modelTransform;
            if (this.type == XRayObjectType.Body)
            {
                this.characterModel = modelTransform?.GetComponent<CharacterModel>();
                this.renderer = this.characterModel?.mainSkinnedMeshRenderer;
                this.defaultColor = PantheraConfig.DetectionCharacterDefaultColor;
            }
            if (this.type == XRayObjectType.Purchase)
            {
                // Simple Chest //
                this.renderer = modelTransform?.Find("Cube.001")?.GetComponentInChildren<Renderer>();
                // Big Chest //
                if (this.renderer == null)
                    this.renderer = modelTransform?.Find("chest2Mesh")?.GetComponent<Renderer>();
                // Utility/Damage/Healing Chest //
                if (this.renderer == null)
                    this.renderer = modelTransform?.Find("CategoryChestMesh")?.GetComponent<Renderer>();
                // Equipment Chest //
                if (this.renderer == null)
                    this.renderer = modelTransform?.Find("Cube")?.GetComponent<Renderer>();
                // Casino Chest //
                if (this.renderer == null)
                    this.renderer = modelTransform?.Find("CasionChestMesh")?.GetComponent<Renderer>();
                // Lunar Chest //
                if (this.renderer == null)
                    this.renderer = modelTransform?.Find("LunarChestMesh")?.GetComponent<Renderer>();
                // Void Chest //
                if (this.renderer == null)
                    this.renderer = modelTransform?.Find("Voidchest")?.GetComponent<Renderer>();
                // Duplicator //
                if (this.renderer == null)
                    this.renderer = modelTransform?.Find("DuplicatorMesh")?.GetComponent<Renderer>();
                // Chance/Blood/Healing/Boss/Combat Shrine //
                if (this.renderer == null)
                    this.renderer = modelTransform?.GetComponent<Renderer>();
                // Drone 1 //
                if (this.renderer == null)
                    this.renderer = modelTransform?.Find("Drone1Mesh")?.GetComponent<Renderer>();
                // Drone 2 / Missile Drone //
                if (this.renderer == null)
                    this.renderer = modelTransform?.Find("Drone2Mesh")?.GetComponent<Renderer>();
                // Flame/Equipment Drone //
                if (this.renderer == null)
                    this.renderer = modelTransform?.Find("DroneMesh")?.GetComponent<Renderer>();
                // Mega Drone //
                if (this.renderer == null)
                    this.renderer = modelTransform?.Find("MegaDroneMesh")?.GetComponent<Renderer>();
                // Turret 1 //
                if (this.renderer == null)
                    this.renderer = modelTransform?.Find("Turret1Mesh")?.GetComponent<Renderer>();
                // Blue Orb //
                if (this.renderer == null)
                    this.renderer = base.transform.Find("mdlNewtStatue")?.GetComponent<Renderer>();

            }
            if (this.type == XRayObjectType.Barrel)
            {
                this.renderer = modelTransform?.Find("BarrelMesh")?.GetComponent<Renderer>();
                if (this.renderer == null)
                    this.renderer = modelTransform?.Find("VoidBarrelMesh")?.GetComponent<Renderer>();
            }
            if (this.type == XRayObjectType.TripleShopBase)
            {
                this.renderer = base.transform.Find("mdlMultiShopTerminalCenter")?.GetComponent<Renderer>();
            }
            if (this.type == XRayObjectType.TripleShopTerminal)
            {
                this.renderer = base.transform.Find("Display")?.Find("mdlMultiShopTerminal")?.Find("MultiShopTerminalMesh")?.GetComponent<Renderer>();
            }
            if (this.type == XRayObjectType.Scrapper)
            {
                this.renderer = modelTransform?.Find("ScrapperMesh")?.GetComponent<Renderer>();
            }
            if (this.type == XRayObjectType.teleporterPart1)
            {
                this.renderer = modelTransform?.GetComponent<Renderer>();
            }
            if (this.type == XRayObjectType.teleporterPart2)
            {
                this.renderer = base.transform.GetComponent<Renderer>();
            }
            if (this.type == XRayObjectType.teleporterPart3)
            {
                this.renderer = base.transform.Find("TeleporterBeacon")?.GetComponent<Renderer>();
            }
            if (this.type == XRayObjectType.lunarTeleporterPart1)
            {
                this.renderer = modelTransform?.GetComponent<Renderer>();
            }
            if (this.type == XRayObjectType.lunarTeleporterPart2)
            {
                this.renderer = modelTransform?.Find("LunarTeleporterProngSkinned")?.GetComponent<Renderer>();
            }
            if (this.type == XRayObjectType.lunarTeleporterPart3)
            {
                this.renderer = base.transform.Find("TeleporterBeacon")?.GetComponent<Renderer>();
            }
            this.origRenInfo = this.characterModel?.baseRendererInfos;
            this.origMats = this.renderer?.materials;

            // Create the Changed Renderer Info List //
            if (this.origRenInfo != null && this.origRenInfo.Length > 0)
            {
                this.changedRenInfo = new CharacterModel.RendererInfo[this.origRenInfo.Length];
                Array.Copy(this.origRenInfo, this.changedRenInfo, this.origRenInfo.Length);
                for (int i = 0; i < this.changedRenInfo.Length; i++)
                {
                    Texture cachedTexture = this.changedRenInfo[i].defaultMaterial.mainTexture;
                    this.changedRenInfo[i].defaultMaterial = new Material(Base.Assets.XRayMat);
                    if (cachedTexture != null)
                        this.changedRenInfo[i].defaultMaterial.mainTexture = cachedTexture;
                    this.changedRenInfo[i].defaultMaterial.SetColor("_FresnelColor", this.defaultColor);
                }
            }

            // Create the Changed Materials List //
            if (this.origMats != null && this.origMats.Length > 0)
            {
                this.changedMats = new Material[this.origMats.Length];
                Array.Copy(this.origMats, this.changedMats, this.origMats.Length);
                for (int i = 0; i < this.origMats.Length; i++)
                {
                    Texture cachedTexture = this.changedMats[i].mainTexture;
                    this.changedMats[i] = new Material(Base.Assets.XRayMat);
                    if (cachedTexture != null)
                        this.changedMats[i].mainTexture = cachedTexture;
                    this.changedMats[i].SetColor("_FresnelColor", this.defaultColor);
                }
            }

            // Save the Original Layer //
            this.origLayer = this.renderer?.gameObject?.layer != null ? this.renderer.gameObject.layer : 0;

            // Create the Gradient //
            GradientColorKey[] colorKey = new GradientColorKey[3];
            colorKey[0].color = UnityEngine.Color.red;
            colorKey[0].time = 0.0f;
            colorKey[1].color = new Color32(255, 255, 0, 255);
            colorKey[1].time = 0.5f;
            colorKey[2].color = UnityEngine.Color.green;
            colorKey[2].time = 1.0f;
            GradientAlphaKey[] alphaKey = new GradientAlphaKey[2];
            alphaKey[0].alpha = 1.0f;
            alphaKey[0].time = 0.0f;
            alphaKey[1].alpha = 1.0f;
            alphaKey[1].time = 1.0f;
            this.gradient.SetKeys(colorKey, alphaKey);

        }

        public void FixedUpdate()
        {

            // Check if must enable Detection //
            if (this.ptraObj.detectionMode == true && this.wasActive == false)
            {
                // Set as enabled //
                this.wasActive = true;
                this.lastHealth = UnityEngine.Mathf.NegativeInfinity;
                this.chestPurchaseState = -2;
                this.barrelWasOpened = -2;
                this.tripleShopBasePurchaseState = -2;
                this.tripleShopTerminalPurchaseState = -2;
                this.enableXRayMat();
            }

            // Check if must disable Detection //
            if (this.ptraObj.detectionMode == false && this.wasActive == true)
            {
                // Set as disabled //
                this.wasActive = false;
                this.disableXRayMat();
            }

            // Stop of Detection is not enabled //
            if (this.ptraObj.detectionMode == false)
                return;

            // Uptade the Color of the Body //
            if (this.type == XRayObjectType.Body)
                this.updateBodyColor();

            // Update Colors //
            if (this.ptraObj.detectionMode == true && this.ptraObj.getAbilityLevel(PantheraConfig.SixthSense_AbilityID) > 0)
            {
                // Update the Color of the Chest //
                if (this.type == XRayObjectType.Purchase && this.name != "Duplicator(Clone)")
                    this.updateChestColor();
                // Update the Color of the Barrel //
                if (this.type == XRayObjectType.Barrel)
                    this.updateBarrelColor();
                // Update the Color of the Tripe Shop Base //
                if (this.type == XRayObjectType.TripleShopBase)
                    this.updateTripleShopBaseColor();
                // Update the Color of the Tripe Shop Terminal //
                if (this.type == XRayObjectType.TripleShopTerminal)
                    this.updateTripleShopTerminalColor();
            }

        }

        public void OnDisable()
        {
            this.disableXRayMat();
        }

        public void OnDestroy()
        {
            this.disableXRayMat();
        }

        public void enableXRayMat()
        {
            // Check the Renderer Info //
            if (this.characterModel != null && this.changedRenInfo != null)
            {
                // Change the Renderer Info //
                this.characterModel.baseRendererInfos = this.changedRenInfo;
                // Update the Model //
                characterModel.UpdateMaterials();
            }
            // Chech the Materials //
            if (this.renderer != null && this.changedMats != null)
            {
                // Change the Materials //
                this.renderer.materials = this.changedMats;
                // Update the Model //
                if (this.characterModel != null)
                    characterModel.UpdateMaterials();
            }
            // Disable the Dynamic Occlusion //
            if (this.renderer != null)
            {
                this.dynOcclusion = this.renderer.allowOcclusionWhenDynamic;
                this.renderer.allowOcclusionWhenDynamic = false;
            }
            // Change the Game Object Layer //
            if (this.renderer != null)
                this.renderer.gameObject.layer = PantheraConfig.Detection_layerIndex;
        }

        public void disableXRayMat()
        {
            if (this.characterModel != null && this.origRenInfo != null)
            {
                // Change the Renderer Info //
                this.characterModel.baseRendererInfos = this.origRenInfo;
                // Update the Model //
                characterModel.UpdateMaterials();
            }
            // Chech the Materials //
            if (this.renderer != null && this.origMats != null)
            {
                // Change the Materials //
                this.renderer.materials = this.origMats;
                // Update the Model //
                if (this.characterModel != null)
                    characterModel.UpdateMaterials();
            }
            // Set back the Dynamic Occlusion //
            if (this.renderer != null)
            {
                this.renderer.allowOcclusionWhenDynamic = this.dynOcclusion;
            }
            // Set back the Game Object Layer //
            if (this.renderer != null)
                this.renderer.gameObject.layer = this.origLayer;
        }

        public void updateBodyColor()
        {

            // Get the Components //
            HealthComponent hc = base.GetComponent<HealthComponent>();
            CharacterBody body = base.GetComponent<CharacterBody>();

            // Check if the Color must change //
            if (hc != null && body != null && hc.health != this.lastHealth)
            {
                // Calcule the Color //
                this.lastHealth = hc.health;
                float percentHeal = hc.health / body.maxHealth;
                Color32 gradientColor = this.gradient.Evaluate(percentHeal);
                if (hc.alive == false)
                {
                    gradientColor = UnityEngine.Color.red;
                }
                // Apply the Color //
                if (this.origRenInfo != null && this.origRenInfo.Length > 0)
                {
                    for (int i = 0; i < this.changedRenInfo.Length; i++)
                    {
                        this.changedRenInfo[i].defaultMaterial.SetColor("_FresnelColor", gradientColor);
                    }
                }
                if (this.origMats != null && this.origMats.Length > 0)
                {
                    for (int i = 0; i < this.origMats.Length; i++)
                    {
                        this.changedMats[i].SetColor("_FresnelColor", gradientColor);
                    }
                }
                // Update the Model //
                this.enableXRayMat();
            }

        }

        public void updateChestColor()
        {

            // Get the Purchase Component //
            PurchaseInteraction purchase = base.GetComponent<PurchaseInteraction>();
            if (purchase == null || this.origMats == null || this.origMats.Length <= 0) return;

            // Look for the purchase capabilities //
            float money = 0;
            if (purchase.costType == CostTypeIndex.Money)
                money = this.ptraObj.characterBody.master.money;
            else if (purchase.costType == CostTypeIndex.LunarCoin)
                money = this.ptraObj.characterBody.master.playerCharacterMasterController.networkUser.lunarCoins;
            else if (purchase.costType == CostTypeIndex.PercentHealth)
                money = this.ptraObj.healthComponent.health / this.ptraObj.characterBody.maxHealth * 100;
            else if (purchase.costType == CostTypeIndex.None)
                money = Mathf.Infinity;

            int state = -1;
            if (purchase.available == true && purchase.cost <= money)
                state = 0;
            else if (purchase.available == true)
                state = 1;
            else
                state = 2;

            // Check if the Color must change //
            if (this.chestPurchaseState != state)
            {
                this.chestPurchaseState = state;
                for (int i = 0; i < this.origMats.Length; i++)
                {
                    if (state == 0)
                        this.changedMats[i].SetColor("_FresnelColor", PantheraConfig.DetectionCanAffortChestColor);
                    else if (state == 1)
                        this.changedMats[i].SetColor("_FresnelColor", PantheraConfig.DetectionDefaultColor);
                    else if (state == 2)
                    {
                        this.disableXRayMat();
                        return;
                    }
                }
                // Update the Model //
                this.enableXRayMat();
            }

        }

        public void updateBarrelColor()
        {

            // Get the Barrel Component //
            BarrelInteraction interaction = base.GetComponent<BarrelInteraction>();
            if (interaction == null || this.origMats == null || this.origMats.Length <= 0) return;

            // Check opened state //
            int opened = -1;
            if (interaction.opened == false)
                opened = 0;
            else
                opened = 1;

            // Check if the Color must change //
            if (opened != this.barrelWasOpened)
            {
                this.barrelWasOpened = opened;
                for (int i = 0; i < this.origMats.Length; i++)
                {
                    if (opened == 0)
                        this.changedMats[i].SetColor("_FresnelColor", PantheraConfig.DetectionCanAffortChestColor);
                    else if (opened == 1)
                    {
                        this.disableXRayMat();
                        return;
                    }
                }
                // Update the Model //
                this.enableXRayMat();
            }

        }

        public void updateTripleShopBaseColor()
        {

            // Get the Purchase Component //
            MultiShopController purchase = base.GetComponent<MultiShopController>();
            if (purchase == null || this.origMats == null || this.origMats.Length <= 0) return;

            // Look for the purchase capabilities //
            int state = -1;
            if (purchase.available == true && purchase.cost <= this.ptraObj.characterBody.master.money)
                state = 0;
            else if (purchase.available == true)
                state = 1;
            else
                state = 2;

            // Check if the Color must change //
            if (this.tripleShopBasePurchaseState != state)
            {
                this.tripleShopBasePurchaseState = state;
                for (int i = 0; i < this.origMats.Length; i++)
                {
                    if (state == 0)
                        this.changedMats[i].SetColor("_FresnelColor", PantheraConfig.DetectionCanAffortChestColor);
                    else if (state == 1)
                        this.changedMats[i].SetColor("_FresnelColor", PantheraConfig.DetectionDefaultColor);
                    else if (state == 2)
                    {
                        this.disableXRayMat();
                        return;
                    }
                }
                // Update the Model //
                this.enableXRayMat();
            }

        }

        public void updateTripleShopTerminalColor()
        {

            // Get the Purchase Component //
            PurchaseInteraction purchase = base.GetComponent<PurchaseInteraction>();
            if (purchase == null || this.origMats == null || this.origMats.Length <= 0) return;

            // Look for the purchase capabilities //
            int state = -1;
            if (purchase.available == true && purchase.cost <= this.ptraObj.characterBody.master.money)
                state = 0;
            else if (purchase.available == true)
                state = 1;
            else
                state = 2;

            // Check if the Color must change //
            if (this.tripleShopTerminalPurchaseState != state)
            {
                this.tripleShopTerminalPurchaseState = state;
                for (int i = 0; i < this.origMats.Length; i++)
                {
                    if (state == 0)
                        this.changedMats[i].SetColor("_FresnelColor", PantheraConfig.DetectionCanAffortChestColor);
                    else if (state == 1)
                        this.changedMats[i].SetColor("_FresnelColor", PantheraConfig.DetectionDefaultColor);
                    else if (state == 2)
                    {
                        this.disableXRayMat();
                        return;
                    }
                }
                // Update the Model //
                this.enableXRayMat();
            }

        }

    }
}
