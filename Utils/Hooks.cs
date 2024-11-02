using MonoMod.RuntimeDetour.HookGen;
using Panthera.BodyComponents;
using RoR2;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;

namespace Panthera.Utils
{
    public class Hooks
    {

        public static void AddHooks(Type classType, string methodName, Delegate calledMethod)
        {
            AddHooks(classType, methodName, null, calledMethod);
        }

        public static void AddHooks(Type classType, string methodName, Type[] methodType, Delegate calledMethod)
        {

            // Check all values //
            if (classType == null || methodName == null || calledMethod == null)
            {
                UnityEngine.Debug.LogWarning("PantheraHooks -> Failed to create hook, argument null: " + classType.Name + "." + methodName + " -> " + calledMethod.GetMethodName());
                return;
            }

            // Get the Method //
            MethodInfo method = null;
            if (methodType != null)
                method = classType.GetMethod(methodName, (BindingFlags)(-1), null, methodType, null);
            else
                method = classType.GetMethod(methodName, (BindingFlags)(-1));


            // Check the Method //
            if (method == null)
            {
                UnityEngine.Debug.LogWarning("PantheraHooks -> Failed to create hook, method null : " + classType.Name + "." + methodName + " -> " + calledMethod.GetMethodName());
                return;
            }

            HookEndpointManager.Add(method, calledMethod);
            UnityEngine.Debug.Log("PantheraHooks -> Hook Added for " + classType.Name + "." + methodName + " -> " + calledMethod.GetMethodName());

        }

        public static void RegisterHooks()
        {

            // Get the User Profile and create the Panthera Panel
            AddHooks(typeof(RoR2.UserProfile), nameof(RoR2.UserProfile.OnLogin), Panthera.ProfileLoaded);

            // Get the Character Select Controller //
            AddHooks(typeof(RoR2.UI.CharacterSelectController), nameof(RoR2.UI.CharacterSelectController.Awake), GUI.PantheraPanel.CharacterSelectionStart);

            // Register all Keys and buttons //
            AddHooks(typeof(Rewired.Data.UserData), nameof(Rewired.Data.UserData.gLOOAxUFAvrvUufkVjaYyZoeLbLE), GUI.KeysBinder.RegistersExtraInput);

            // Recalculate Panthera Stats //
            AddHooks(typeof(RoR2.CharacterBody), nameof(RoR2.CharacterBody.RecalculateStats), PantheraBody.RecalculateStatsHook);

            // Create the Panthera HUD //
            AddHooks(typeof(RoR2.UI.HUD), nameof(RoR2.UI.HUD.Awake), GUI.PantheraHUD.HUDAwakeHook);

            // Get the Skin Index //
            AddHooks(typeof(RoR2.SurvivorMannequins.SurvivorMannequinSlotController), nameof(RoR2.SurvivorMannequins.SurvivorMannequinSlotController.ApplyLoadoutToMannequinInstance), Base.Skin.SkinChangeHook);

            // Panthera Death //
            AddHooks(typeof(RoR2.CharacterDeathBehavior), nameof(RoR2.CharacterDeathBehavior.OnDeath), BodyComponents.PantheraDeathBehavior.CharacterDeathBehaviorHook);

            // Manage Damages //
            AddHooks(typeof(RoR2.HealthComponent), nameof(RoR2.HealthComponent.TakeDamage), PantheraHealthComponent.TakeDamageHook);

            // Used to decrease Cooldowns //
            AddHooks(typeof(RoR2.SkillLocator), nameof(RoR2.SkillLocator.ApplyAmmoPack), PantheraSkillLocator.ApplyAmmoPackHook);

            // Set the Character visibility when stealthed //
            AddHooks(typeof(RoR2.CharacterBody), nameof(RoR2.CharacterBody.GetVisibilityLevel), new Type[] { typeof(RoR2.TeamIndex)}, PantheraBody.GetVisibilityLevelHook);

            // Apply the Cupidity Buff //
            AddHooks(typeof(RoR2.CharacterMaster), nameof(RoR2.CharacterMaster.GiveMoney), Skills.Passives.AmbitionMode.CalculateAddedMoney);

            // Fix Interactor bug when Detection is on //
            AddHooks(typeof(RoR2.Interactor), nameof(RoR2.Interactor.FindBestInteractableObject), new Type[] { typeof(Ray), typeof(float), typeof(Vector3), typeof(float) }, PantheraInteractor.FindBestInteractableObjectHook);

            // Used for the Portal Surge //
            AddHooks(typeof(RoR2.CombatDirector), nameof(RoR2.CombatDirector.FixedUpdate), Base.PantheraCombatDirector.FixedUpdate);
            AddHooks(typeof(RoR2.CombatDirector), nameof(RoR2.CombatDirector.PrepareNewMonsterWave), Base.PantheraCombatDirector.PrepareNewMonsterWave);
            AddHooks(typeof(RoR2.CombatDirector), nameof(RoR2.CombatDirector.SetNextSpawnAsBoss), Base.PantheraCombatDirector.SetNextSpawnAsBoss);
            AddHooks(typeof(RoR2.TeleporterInteraction.ChargingState), nameof(RoR2.TeleporterInteraction.ChargingState.OnEnter), Base.PantheraCombatDirector.OnPortalCharging);
            AddHooks(typeof(RoR2.TeleporterInteraction.ChargedState), nameof(RoR2.TeleporterInteraction.ChargedState.OnEnter), Base.PantheraCombatDirector.OnPortalCharged);

            // Allows the Front Shield to block Blast Attack //
            AddHooks(typeof(RoR2.BlastAttack), nameof(RoR2.BlastAttack.CollectHits), Components.FrontShieldComponent.OnBlastAttack);

            //AddHooks(typeof(RoR2.UI.GameEndReportPanelController), nameof(RoR2.UI.GameEndReportPanelController.SetPlayerInfo), CharacterOld.GameEndRepportHook);
            //AddHooks(typeof(KinematicCharacterController.KinematicCharacterSystem), nameof(KinematicCharacterController.KinematicCharacterSystem.Simulate), new Type[] { typeof(float) }, Base.PantheraKinematicSystem.SimulateHook1);
            //AddHooks(typeof(KinematicCharacterController.KinematicCharacterSystem), nameof(KinematicCharacterController.KinematicCharacterSystem.Simulate), new Type[] { typeof(float), typeof(KinematicCharacterController.KinematicCharacterMotor[]), typeof(int), typeof(KinematicCharacterController.PhysicsMover[]), typeof(int) }, Base.PantheraKinematicSystem.SimulateHook2);
            //AddHooks(typeof(RoR2.UI.CharacterSelectController), nameof(RoR2.UI.CharacterSelectController.Awake), GUI.ConfigPanel.AddConfigPanelHook);
            //AddHooks(typeof(RoR2.HuntressTracker), nameof(RoR2.HuntressTracker.SearchForTarget), BodyComponents.PantheraTracker.SearchForTarget);
            //AddHooks(typeof(RoR2.DamageInfo), nameof(RoR2.DamageInfo.ModifyDamageInfo), PantheraHealthComponent.ModifyDamageInfoHook);

            //AddHooks(typeof(RoR2.UI.HUDBossHealthBarController), nameof(RoR2.UI.HUDBossHealthBarController.OnClientDamageNotified), HookProvisoirePourDebugger);

        }


        public static void HookProvisoirePourDebugger(Action<RoR2.UI.HUDBossHealthBarController, DamageDealtMessage> orig, RoR2.UI.HUDBossHealthBarController self, DamageDealtMessage damageDealtMessage)
        {

            if (!self.nextAllowedSourceUpdateTime.hasPassed)
            {
                return;
            }
            if (!damageDealtMessage.victim)
            {
                return;
            }
            CharacterBody component = damageDealtMessage.victim.GetComponent<CharacterBody>();
            if (!component)
            {
                return;
            }
            if (component.isBoss && damageDealtMessage.attacker == self.hud.targetBodyObject)
            {
                BossGroup bossGroup = BossGroup.FindBossGroup(component);
                if (bossGroup && bossGroup.shouldDisplayHealthBarOnHud)
                {
                    self.currentBossGroup = bossGroup;
                    self.nextAllowedSourceUpdateTime = Run.TimeStamp.now + 1f;
                }
            }
        }

    }
}
