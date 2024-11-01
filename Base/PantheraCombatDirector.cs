using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;
using UnityEngine;
using Panthera.Components;
using RoR2.Navigation;
using UnityEngine.Bindings;

namespace Panthera.Base
{
    public class PantheraCombatDirector
    {

        public static void FixedUpdate(Action<RoR2.CombatDirector> orig, RoR2.CombatDirector self)
        {

            // Return if not Surged //
            if (IsSurged() == false)
            {
                orig(self);
                return;
            }

            // Return if spawn is disabled //
            if (CombatDirector.cvDirectorCombatDisable.value)
            {
                return;
            }

            // Check if running on server //
            if (NetworkServer.active && Run.instance)
            {
                // Add the Run difficulty //
                float compensatedDifficultyCoefficient = Run.instance.compensatedDifficultyCoefficient;
                for (int i = 0; i < self.moneyWaves.Length; i++)
                {
                    float num = self.moneyWaves[i].Update(Time.fixedDeltaTime, compensatedDifficultyCoefficient);
                    self.monsterCredit += num;
                }

                // Stop if too much Monsters //
                if (self.ignoreTeamSizeLimit == false && TeamComponent.GetTeamMembers(TeamIndex.Monster).Count >= TeamCatalog.GetTeamDef(TeamIndex.Monster).softCharacterLimit)
                    return;

                // Simulate //
                Simulate(Time.fixedDeltaTime, self);

            }
        }

        public static void Simulate(float deltaTime, RoR2.CombatDirector self)
        {

            // Select a Player to target //
            if (self.targetPlayers)
            {
                self.playerRetargetTimer -= deltaTime;
                if (self.playerRetargetTimer <= 0f)
                {
                    self.playerRetargetTimer = self.rng.RangeFloat(1f, 10f);
                    self.PickPlayerAsSpawnTarget();
                }
            }

            Utils.DebugInfo.addText("self.monsterSpawnTimer", "self.monsterSpawnTimer = " + self.monsterSpawnTimer);

            // Decrease the Spawn Timer //
            self.monsterSpawnTimer -= deltaTime;

            // Ask a new Card //
            self.currentMonsterCard = null;

            // Spawn Monster //
            if (self.monsterSpawnTimer <= 0f)
            {

                // Try to spawn a Monster //
                if (AttemptSpawnOnTarget(self, self.currentSpawnTarget ? self.currentSpawnTarget.transform : null, DirectorPlacementRule.PlacementMode.Approximate))
                {

                    // Check if only one wave //
                    if (self.shouldSpawnOneWave)
                    {
                        Debug.Log("CombatDirector hasStartedwave = true");
                        self.hasStartedWave = self;
                    }

                    // Increase the Spawn Timer //
                   // self.monsterSpawnTimer += self.rng.RangeFloat(self.minSeriesSpawnInterval, self.maxSeriesSpawnInterval);

                    return;
                }

                // Increase the Spawn Timer //
                //self.monsterSpawnTimer += self.rng.RangeFloat(self.minRerollSpawnInterval, self.maxRerollSpawnInterval);

                // Spawn Failed, remove Card //
                if (self.resetMonsterCardIfFailed)
                {
                    self.currentMonsterCard = null;
                }


                // Disable the Spawn if one wave mode //
                if (self.shouldSpawnOneWave && self.hasStartedWave)
                {
                    Debug.Log("CombatDirector wave complete");
                    self.enabled = false;
                    return;
                }


            }
        }

        public static bool AttemptSpawnOnTarget(RoR2.CombatDirector self, Transform spawnTarget, DirectorPlacementRule.PlacementMode placementMode = DirectorPlacementRule.PlacementMode.Approximate)
        {

            // Check the Monster Card or pick a new one //
            if (self.currentMonsterCard == null)
            {
                if (CombatDirector.cvDirectorCombatEnableInternalLogs.value)
                {
                    Debug.Log("Current monster card is null, pick new one.");
                }
                if (self.finalMonsterCardsSelection == null)
                {
                    return false;
                }
                self.PrepareNewMonsterWave(self.finalMonsterCardsSelection.Evaluate(self.rng.nextNormalizedFloat));
            }


            // Check if the max Spawn Count is reached //
            if (self.spawnCountInCurrentWave >= self.maximumNumberToSpawnBeforeSkipping)
            {
                self.spawnCountInCurrentWave = 0;
                if (CombatDirector.cvDirectorCombatEnableInternalLogs.value)
                {
                    Debug.LogFormat("Spawn count has hit the max ({0}/{1}). Aborting spawn.", new object[]
                    {
                        self.spawnCountInCurrentWave,
                        self.maximumNumberToSpawnBeforeSkipping
                    });
                }
                return false;
            }

            // Get costs //
            int cardCost1 = self.currentMonsterCard.cost;
            int cardCost2 = self.currentMonsterCard.cost;
            float eliteCostMultiplier = 1f;

            // Get actual Elite Def //
            EliteDef eliteDef = self.currentActiveEliteDef;

            // Calcule the cost if Elite //
            cardCost2 = (int)((float)cardCost1 * self.currentActiveEliteTier.costMultiplier);

            // Check if enought credit to spawn Elite or reset the Elite tier //
            if ((float)cardCost2 <= self.monsterCredit)
            {
                cardCost1 = cardCost2;
                eliteCostMultiplier = self.currentActiveEliteTier.costMultiplier;
            }
            else
            {
                self.ResetEliteType();
                eliteDef = self.currentActiveEliteDef;
            }

            // Return if the Card is not available //
            if (!self.currentMonsterCard.IsAvailable())
            {
                if (CombatDirector.cvDirectorCombatEnableInternalLogs.value)
                {
                    Debug.LogFormat("Spawn card {0} is invalid, aborting spawn.", new object[]
                    {
                        self.currentMonsterCard.spawnCard
                    });
                }
                return false;
            }

            // Return if the Card is too expensive //
            if (self.monsterCredit < (float)cardCost1)
            {
                if (CombatDirector.cvDirectorCombatEnableInternalLogs.value)
                {
                    Debug.LogFormat("Spawn card {0} is too expensive, aborting spawn.", new object[]
                    {
                        self.currentMonsterCard.spawnCard
                    });
                }
                return false;
            }

            // Check if the Card is too cheap //
            if (self.skipSpawnIfTooCheap && self.consecutiveCheapSkips < self.maxConsecutiveCheapSkips && (float)(cardCost2 * self.maximumNumberToSpawnBeforeSkipping) < self.monsterCredit)
            {

                if (CombatDirector.cvDirectorCombatEnableInternalLogs.value)
                {
                    Debug.LogFormat("Card {0} seems too cheap ({1}/{2}). Comparing against most expensive possible ({3})", new object[]
                    {
                        self.currentMonsterCard.spawnCard,
                        cardCost1 * self.maximumNumberToSpawnBeforeSkipping,
                        self.monsterCredit,
                        self.mostExpensiveMonsterCostInDeck
                    });
                }

                // Check if a more expensive Card is available //
                if (self.mostExpensiveMonsterCostInDeck > cardCost1)
                {
                    self.consecutiveCheapSkips++;
                    if (CombatDirector.cvDirectorCombatEnableInternalLogs.value)
                    {
                        Debug.LogFormat("Spawn card {0} is too cheap, aborting spawn.", new object[]
                        {
                            self.currentMonsterCard.spawnCard
                        });
                    }
                    return false;
                }

            }


            // Get spawn Card //
            SpawnCard spawnCard = self.currentMonsterCard.spawnCard;
            SpawnCard spawnCard2 = spawnCard;

            // Get Elite Def //
            EliteDef eliteDef2 = eliteDef;

            // Get the Elite Cost Multiplier //
            float valueMultiplier = eliteCostMultiplier;

            // Get prevent ocerhead //
            bool preventOverhead = self.currentMonsterCard.preventOverhead;

            // Try to Spawn the Card //
            if (self.Spawn(spawnCard2, eliteDef2, spawnTarget, self.currentMonsterCard.spawnDistance, preventOverhead, valueMultiplier, placementMode))
            {
                self.monsterCredit -= (float)cardCost1;
                self.totalCreditsSpent += (float)cardCost1;
                self.spawnCountInCurrentWave++;
                self.consecutiveCheapSkips = 0;
                self.monsterSpawnTimer += self.rng.RangeFloat(self.minRerollSpawnInterval, self.maxRerollSpawnInterval);
                return true;
            }

            // Return //
            return false;

        }

        public static void PrepareNewMonsterWave(Action<RoR2.CombatDirector, DirectorCard> orig, RoR2.CombatDirector self, DirectorCard monsterCard)
        {

            // Return if not Surged //
            if (IsSurged() == false)
            {
                orig(self, monsterCard);
                return;
            }

            // Log //
            if (CombatDirector.cvDirectorCombatEnableInternalLogs.value)
            {
                Debug.LogFormat("Preparing monster wave {0}", new object[]
                {
                    monsterCard.spawnCard
                });
            }

            // Set the current Card //
            self.currentMonsterCard = monsterCard;

            // Reset Elite Type //
            self.ResetEliteType();

            // Check if the Card can be an Elite //
            if (!(self.currentMonsterCard.spawnCard as CharacterSpawnCard).noElites)
            {

                // Itinerate all Elites tier //
                for (int i = 1; i < CombatDirector.eliteTiers.Length; i++)
                {

                    // Get the Elite Def //
                    CombatDirector.EliteTierDef eliteTierDef = CombatDirector.eliteTiers[i];

                    // Check if this Elite tier can be selected //
                    if (!eliteTierDef.CanSelect(self.currentMonsterCard.spawnCard.eliteRules))
                    {
                        if (CombatDirector.cvDirectorCombatEnableInternalLogs.value)
                        {
                            Debug.LogFormat("Elite tier index {0} is unavailable", new object[]
                            {
                                i
                            });
                        }
                    }
                    else
                    {

                        // Calculate the Card cost //
                        float finalCost = (float)self.currentMonsterCard.cost * eliteTierDef.costMultiplier * self.eliteBias;

                        // Check if enought Credit //
                        if (finalCost <= self.monsterCredit)
                        {
                            self.currentActiveEliteTier = eliteTierDef;
                            if (CombatDirector.cvDirectorCombatEnableInternalLogs.value)
                            {
                                Debug.LogFormat("Found valid elite tier index {0}", new object[]
                                {
                                    i
                                });
                            }
                        }
                        else if (CombatDirector.cvDirectorCombatEnableInternalLogs.value)
                        {
                            Debug.LogFormat("Elite tier index {0} is too expensive ({1}/{2})", new object[]
                            {
                                i,
                                finalCost,
                                self.monsterCredit
                            });
                        }
                    }
                }
            }
            else if (CombatDirector.cvDirectorCombatEnableInternalLogs.value)
            {
                Debug.LogFormat("Card {0} cannot be elite. Skipping elite procedure.", new object[]
                {
                    self.currentMonsterCard.spawnCard
                });
            }

            // Get a random Elite tier //
            self.currentActiveEliteDef = self.currentActiveEliteTier.GetRandomAvailableEliteDef(self.rng);

            // Log //
            if (CombatDirector.cvDirectorCombatEnableInternalLogs.value)
            {
                Debug.LogFormat("Assigned elite index {0}", new object[]
                {
                    self.currentActiveEliteDef
                });
            }

            // Save the last attempted Card //
            self.lastAttemptedMonsterCard = self.currentMonsterCard;

            // Set the Spawn count to 0 //
            self.spawnCountInCurrentWave = 0;

        }

        public static void SetNextSpawnAsBoss(Action<RoR2.CombatDirector> orig, RoR2.CombatDirector self)
        {

            // Return if not Surged //
            if (IsSurged() == false)
            {
                orig(self);
                return;
            }
            else
            {
                self.enabled = false;
                return;
            }

            // Create the Selection //
            WeightedSelection<DirectorCard> weightedSelection = new WeightedSelection<DirectorCard>(8);

            // Itinerate all Boss Choice //
            int i = 0;
            int count = self.finalMonsterCardsSelection.Count;
            while (i < count)
            {

                // Get the info //
                WeightedSelection<DirectorCard>.ChoiceInfo choice = self.finalMonsterCardsSelection.GetChoice(i);

                // Get the Spawn Card //
                SpawnCard spawnCard = choice.value.spawnCard;

                // Get if Champion //
                bool isChampion = spawnCard.prefab.GetComponent<CharacterMaster>().bodyPrefab.GetComponent<CharacterBody>().isChampion;

                // Get the Character Card //
                CharacterSpawnCard characterSpawnCard = spawnCard as CharacterSpawnCard;

                // Check if can be used as Boss //
                bool flag = characterSpawnCard != null && characterSpawnCard.forbiddenAsBoss;

                // Add the choice to the List if available //
                if (isChampion && !flag && choice.value.IsAvailable())
                {
                    weightedSelection.AddChoice(choice);
                }

                // Increment i //
                i++;

            }

            // Check if there are choice available //
            if (weightedSelection.Count > 0)
            {

                // chose a Card //
                DirectorCard directorCard = weightedSelection.Evaluate(self.rng.nextNormalizedFloat);

                // Log //
                Debug.Log(string.Format("Next boss spawn:  {0}", directorCard.spawnCard));

                // Send the Boss as next wave //
                self.PrepareNewMonsterWave(directorCard);

            }
            else
            {
                Debug.Log("Could not spawn boss");
            }

            // Set Spawn Timer to -600 //
            self.monsterSpawnTimer = -600f;

        }

        public static void SetSurged(GameObject teleporter, bool set)
        {
            Panthera.surgeComponent = teleporter.GetComponent<SurgeComponent>();
            if (Panthera.surgeComponent == null)
                Panthera.surgeComponent = teleporter.AddComponent<SurgeComponent>();
        }

        public static bool IsSurged()
        {
            if (NetworkServer.active == true && Panthera.surgeComponent != null && Panthera.surgeComponent.GetComponent<HoldoutZoneController>() != null &&  Panthera.surgeComponent.GetComponent<HoldoutZoneController>().enabled == true)
                return true;
            else
                return false;
        }

        public static void OnPortalCharging(Action<RoR2.TeleporterInteraction.ChargingState> orig, RoR2.TeleporterInteraction.ChargingState self)
        {

            // Call the Orig Function //
            orig(self);

            // Check if the Teleporter is Surged //
            if (IsSurged() == false)
                return;

            // Set the Teleporter charge time //
            self.teleporterInteraction.holdoutZoneController.baseChargeDuration = PantheraConfig.PortalSurge_teleporterChargeTime;

            // Set the Teleporter radius //
            self.teleporterInteraction.holdoutZoneController.baseRadius = PantheraConfig.PortalSurge_teleporterRadius;

        }

        public static void OnPortalCharged(Action<RoR2.TeleporterInteraction.ChargedState> orig, RoR2.TeleporterInteraction.ChargedState self)
        {

            // Call the Orig Function //
            orig(self);

            // Check if the Teleporter is Surged //
            if (Panthera.surgeComponent == null)
                return;

            // Check if Panthera //
            if (Panthera.PantheraCharacter.pantheraObj != null && Panthera.PantheraCharacter.pantheraObj.HasAuthority() == true)
            {
                Panthera.ProfileComponent.totalMasteryPoints++;
                Panthera.ProfileComponent.saveSkillsTree();
            }

            // Destroy the Effect //
            GameObject.Destroy(self.outer.transform.Find("PortalOverChargeFX(Clone)").gameObject);

        }

    }
}
