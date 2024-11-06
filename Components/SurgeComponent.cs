using KinematicCharacterController;
using RoR2;
using RoR2.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using static RoR2.SpawnCard;

namespace Panthera.Components
{
    public class SurgeComponent : NetworkBehaviour
    {

        public float addCreditsTimer = 0;
        public bool firstWave = true;
        public bool megaBossSpawned = false;

        public void Start()
        {
            SpawnCard.onSpawnedServerGlobal += OnSpawnedCreature;
        }

        public void OnDestroy()
        {
            SpawnCard.onSpawnedServerGlobal -= OnSpawnedCreature;
        }

        public void OnSpawnedCreature(SpawnCard.SpawnResult spawnResult)
        {

            // Return if Client //
            if (NetworkServer.active == false)
                return;

            // Get the Holdout //
            HoldoutZoneController hzc = base.GetComponent<HoldoutZoneController>();

            // Return if Teleported is not active //
            if (hzc.enabled == false)
                return;

            // Get the Character Master //
            CharacterMaster characterMaster = spawnResult.spawnedInstance ? spawnResult.spawnedInstance.GetComponent<CharacterMaster>() : null;

            // Check the Caracter Master //
            if (!characterMaster)
                return;

            // Check if Monster //
            if (characterMaster.teamIndex != TeamIndex.Monster)
                return;

            // Give Equipement //
            if (hzc.charge > 0.5)
                characterMaster.inventory.GiveRandomEquipment();

            // Give 1 Item Charge < 10% //
            if (hzc.charge < 0.1)
                characterMaster.inventory.GiveRandomItems(1, false, false);

            // Give 2 Items Charge < 20% //
            else if (hzc.charge < 0.2)
                characterMaster.inventory.GiveRandomItems(2, false, false);

            // Give 3 Items Charge < 30% //
            else if (hzc.charge < 0.3)
                characterMaster.inventory.GiveRandomItems(3, false, false);

            // Give 4 Items Charge < 40% //
            else if (hzc.charge < 0.4)
                characterMaster.inventory.GiveRandomItems(4, false, false);

            // Give 5 Items Charge < 50% //
            else if (hzc.charge < 0.5)
                characterMaster.inventory.GiveRandomItems(5, true, false);

            // Give 6 Items Charge < 60% //
            else if (hzc.charge < 0.6)
                characterMaster.inventory.GiveRandomItems(6, true, false);

            // Give 7 Items Charge < 70% //
            else if (hzc.charge < 0.7)
                characterMaster.inventory.GiveRandomItems(7, true, false);

            // Give 8 Items Charge < 80% //
            else if (hzc.charge < 0.8)
                characterMaster.inventory.GiveRandomItems(8, true, false);

            // Give 9 Items Charge < 90% //
            else if (hzc.charge < 0.9)
                characterMaster.inventory.GiveRandomItems(9, true, true);

            // Give 10 Items Charge < 100% //
            else if (hzc.charge < 1)
                characterMaster.inventory.GiveRandomItems(10, true, true);

            // Recalculate Stats //
            characterMaster.GetBody().RecalculateStats();

        }

        public void Update()
        {

            // Get the Combat Director //
            CombatDirector teleportDirector = base.GetComponent<TeleporterInteraction>().bonusDirector;

            // Get the Holdout //
            HoldoutZoneController hzc = base.GetComponent<HoldoutZoneController>();

            // Return if Teleported is not active //
            if (hzc.enabled == false)
                return;

            // Get the Combat Director //
            if (teleportDirector == null)
            {
                foreach (CombatDirector director in CombatDirector.instancesList)
                {
                    if (director.name.Contains("Teleport"))
                        teleportDirector = director;
                }
            }

            // Check if Combat Director is Null //
            if (teleportDirector == null)
                return;

            // Check if first Wave //
            if (this.firstWave == true)
            {

                // Set First Wave to false //
                this.firstWave = false;

                // Disable the Boss Health display //
                base.GetComponent<TeleporterInteraction>().bossGroup.shouldDisplayHealthBarOnHud = false;

                // Set the Teleporter charge time //
                hzc.baseChargeDuration = PantheraConfig.PortalSurge_teleporterChargeTime;

                // Set the Teleporter radius //
                hzc.baseRadius = PantheraConfig.PortalSurge_teleporterRadius;

                if (NetworkServer.active == false)
                    return;

                // Add Credits //
                teleportDirector.monsterCredit += PantheraConfig.PortalSurge_initialCredit;

                // Decrease Monsters Group to 1 //
                teleportDirector.maximumNumberToSpawnBeforeSkipping = 1;

                // Set the max cheap skip //
                teleportDirector.maxConsecutiveCheapSkips = 5;

                // Set the Spawn Time //
                teleportDirector.minSeriesSpawnInterval = 0.1f;
                teleportDirector.maxSeriesSpawnInterval = 0.11f;
                teleportDirector.minRerollSpawnInterval = 0.1f;
                teleportDirector.minRerollSpawnInterval = 0.11f;

                // Add all Monsters Type to the Card //
                teleportDirector.finalMonsterCardsSelection.Clear();
                foreach (DirectorCardCategorySelection.Category cat in RoR2.RoR2Content.mixEnemyMonsterCards.categories)
                {
                    foreach (DirectorCard card in cat.cards)
                    {
                        teleportDirector.finalMonsterCardsSelection.AddChoice(card, card.selectionWeight);
                    }
                }

            }

            if (NetworkServer.active == false)
                return;

            // Check if must Update //
            if (Time.time - this.addCreditsTimer > PantheraConfig.PortalSurge_addCreditsTime)
            {

                // Update the Timer //
                this.addCreditsTimer = Time.time;

                // Check if Charge less than 1% //
                if (hzc.charge < 0.01)
                {
                    if (teleportDirector.monsterCredit < 1500)
                        teleportDirector.monsterCredit += 20;
                }

                // Check if Charge less than 10% //
                else if (hzc.charge < 0.1)
                {
                    if (teleportDirector.monsterCredit < 1000)
                        teleportDirector.monsterCredit += 30;
                    teleportDirector.minSeriesSpawnInterval = 1f;
                    teleportDirector.maxSeriesSpawnInterval = 3f;
                    teleportDirector.minRerollSpawnInterval = 1f;
                    teleportDirector.minRerollSpawnInterval = 3f;
                    teleportDirector.maxConsecutiveCheapSkips = 10;
                }

                // Check if Charge less than 30% //
                else if (hzc.charge < 0.3)
                {
                    if (teleportDirector.monsterCredit < 2000)
                        teleportDirector.monsterCredit += 50;
                    teleportDirector.maxConsecutiveCheapSkips = 30;
                }

                // Check if Charge less than 50% //
                else if (hzc.charge < 0.5)
                {
                    if (teleportDirector.monsterCredit < 3000)
                        teleportDirector.monsterCredit += 70;
                    teleportDirector.maxConsecutiveCheapSkips = 50;
                }

                // Check if Charge less than 80% //
                else if (hzc.charge < 0.8)
                {
                    if (teleportDirector.monsterCredit < 5000)
                        teleportDirector.monsterCredit += 100;
                    teleportDirector.maxConsecutiveCheapSkips = 80;
                }

                // Check if Charge less than 100% //
                else if (hzc.charge < 1)
                {
                    if (teleportDirector.monsterCredit < 10000)
                        teleportDirector.monsterCredit += 150;
                    teleportDirector.maxConsecutiveCheapSkips = 100;
                }

                // Spawn the Mega Boss //
                if (this.megaBossSpawned == false && hzc.charge >= 0.8)
                {
                    this.SpawnMegaBoss();
                    this.megaBossSpawned = true;
                }

            }

        }

        public void SpawnMegaBoss()
        {

            // Enable the Boss Health display //
            base.GetComponent<TeleporterInteraction>().bossGroup.shouldDisplayHealthBarOnHud = true;

            // Get a random Master //
            int rand = (UnityEngine.Random.RandomRangeInt(1, Panthera.PantheraCharacter.bossList.Count)) - 1;

            // Get the Master //
            GameObject master = Panthera.PantheraCharacter.bossList.ElementAt(rand).Value;

            // Create the Spawn Card //
            CharacterSpawnCard spawnCard = ScriptableObject.CreateInstance<CharacterSpawnCard>();
            spawnCard.prefab = master;
            spawnCard.sendOverNetwork = true;

            // Set the Graph Type //
            GameObject body = spawnCard.prefab.GetComponent<CharacterMaster>().bodyPrefab;
            if (body != null)
            {
                spawnCard.nodeGraphType = (body.GetComponent<CharacterMotor>() == null
                    && (body.GetComponent<RigidbodyMotor>() != null || body.GetComponent<KinematicCharacterMotor>()))
                    ? MapNodeGroup.GraphType.Air
                    : MapNodeGroup.GraphType.Ground;
            }

            // Create the Spawn Request //
            DirectorSpawnRequest spawnRequest = new DirectorSpawnRequest(
                spawnCard,
                new DirectorPlacementRule
                {
                    placementMode = DirectorPlacementRule.PlacementMode.Approximate,
                    position = base.transform.position
                },
                RoR2Application.rng
            );
            spawnRequest.teamIndexOverride = TeamIndex.Monster;
            spawnRequest.ignoreTeamMemberLimit = true;

            // Spawn the Mega Boss //
            GameObject masterGameObject = spawnCard.DoSpawn(base.transform.position, Quaternion.identity, spawnRequest).spawnedInstance;

            // Add Items of all Players //
            CharacterMaster masterObj = masterGameObject.GetComponent<CharacterMaster>();
            foreach (PlayerCharacterMasterController mc in PlayerCharacterMasterController.instances)
            {
                masterObj.inventory.CopyItemsFrom(mc.master.inventory);
            }

            // Get the Body //
            CharacterBody characterBody = masterObj.GetBody();

            // Increase the Size //
            characterBody.modelLocator.modelTransform.localScale *= PantheraConfig.PortalSurge_bossSizeMultiplier;

            // Set the Stats //
            masterObj.isBoss = true;
            characterBody.isElite = true;
            characterBody.isChampion = true;
            characterBody.gameObject.name += PantheraConfig.PortalSurge_megaBossAddedName;

            // Recalculate Stats //
            characterBody.RecalculateStats();

            // Add the Boss to the Teleporter //
            base.GetComponent<TeleporterInteraction>().bossGroup.enabled = true;
            base.GetComponent<TeleporterInteraction>().bossGroup.AddBossMemory(masterObj);
            base.GetComponent<TeleporterInteraction>().bossGroup.combatSquad.AddMember(masterObj);

        }

        public static void SetMegaBossStats(CharacterBody bossBody)
        {
            bossBody.maxHealth *= PantheraConfig.PortalSurge_bossStatsMultiplier;
            bossBody.baseRegen = Math.Max(bossBody.baseRegen, bossBody.level / 10 * PantheraConfig.PortalSurge_bossStatsMultiplier);
            bossBody.baseMaxShield = Math.Max(bossBody.baseMaxShield, bossBody.maxHealth);
            bossBody.levelMoveSpeed *= PantheraConfig.PortalSurge_bossStatsMultiplier / 2;
            bossBody.damage *= PantheraConfig.PortalSurge_bossStatsMultiplier;
            bossBody.attackSpeed *= PantheraConfig.PortalSurge_bossStatsMultiplier / 2;
            bossBody.crit *= PantheraConfig.PortalSurge_bossStatsMultiplier;
            bossBody.armor *= PantheraConfig.PortalSurge_bossStatsMultiplier;
        }

    }
}
