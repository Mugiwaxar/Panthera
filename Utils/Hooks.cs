﻿using MonoMod.RuntimeDetour.HookGen;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using Panthera.Machines;
using Rewired;
using RoR2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using Debug = UnityEngine.Debug;

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
            AddHooks(typeof(RoR2.UserProfile), nameof(RoR2.UserProfile.OnLogin), Panthera.ProfileLoaded);
            AddHooks(typeof(RoR2.UI.CharacterSelectController), nameof(RoR2.UI.CharacterSelectController.Awake), GUI.PantheraPanel.CharacterSelectionStart);
            AddHooks(typeof(Rewired.Data.UserData), nameof(Rewired.Data.UserData.wVZZKoPFwEvodLvLcYNvVAPKpUj), GUI.KeysBinder.RegistersExtraInput);
            AddHooks(typeof(RoR2.CharacterBody), nameof(RoR2.CharacterBody.RecalculateStats), PantheraBody.RecalculateStatsHook);
            AddHooks(typeof(RoR2.UI.HUD), nameof(RoR2.UI.HUD.Awake), GUI.PantheraHUD.HUDAwakeHook);
            AddHooks(typeof(RoR2.SurvivorMannequins.SurvivorMannequinSlotController), nameof(RoR2.SurvivorMannequins.SurvivorMannequinSlotController.ApplyLoadoutToMannequinInstance), Base.Skin.SkinChangeHook);
            AddHooks(typeof(RoR2.CharacterDeathBehavior), nameof(RoR2.CharacterDeathBehavior.OnDeath), BodyComponents.PantheraDeathBehavior.CharacterDeathBehaviorHook);
            AddHooks(typeof(RoR2.HealthComponent), nameof(RoR2.HealthComponent.TakeDamage), PantheraHealthComponent.TakeDamageHook);
            AddHooks(typeof(RoR2.SkillLocator), nameof(RoR2.SkillLocator.ApplyAmmoPack), PantheraSkillLocator.applyAmmoPackHook);
            AddHooks(typeof(RoR2.CharacterBody), nameof(RoR2.CharacterBody.GetVisibilityLevel), new Type[] { typeof(RoR2.TeamIndex)}, PantheraBody.GetVisibilityLevelHook);
            AddHooks(typeof(RoR2.CharacterMaster), nameof(RoR2.CharacterMaster.GiveMoney), Skills.Passives.AmbitionMode.CalculateAddedMoney);
            AddHooks(typeof(RoR2.DamageInfo), nameof(RoR2.DamageInfo.ModifyDamageInfo), PantheraHealthComponent.ModifyDamageInfoHook);
            AddHooks(typeof(RoR2.Interactor), nameof(RoR2.Interactor.FindBestInteractableObject), new Type[] { typeof(Ray), typeof(float), typeof(Vector3), typeof(float) }, PantheraInteractor.FindBestInteractableObjectHook);

            //AddHooks(typeof(RoR2.UI.GameEndReportPanelController), nameof(RoR2.UI.GameEndReportPanelController.SetPlayerInfo), CharacterOld.GameEndRepportHook);
            //AddHooks(typeof(KinematicCharacterController.KinematicCharacterSystem), nameof(KinematicCharacterController.KinematicCharacterSystem.Simulate), new Type[] { typeof(float) }, Base.PantheraKinematicSystem.SimulateHook1);
            //AddHooks(typeof(KinematicCharacterController.KinematicCharacterSystem), nameof(KinematicCharacterController.KinematicCharacterSystem.Simulate), new Type[] { typeof(float), typeof(KinematicCharacterController.KinematicCharacterMotor[]), typeof(int), typeof(KinematicCharacterController.PhysicsMover[]), typeof(int) }, Base.PantheraKinematicSystem.SimulateHook2);
            //AddHooks(typeof(RoR2.UI.CharacterSelectController), nameof(RoR2.UI.CharacterSelectController.Awake), GUI.ConfigPanel.AddConfigPanelHook);
            //AddHooks(typeof(RoR2.HuntressTracker), nameof(RoR2.HuntressTracker.SearchForTarget), BodyComponents.PantheraTracker.SearchForTarget);

            //AddHooks(typeof(RoR2.Projectile.ProjectileController), nameof(RoR2.Projectile.ProjectileController.OnCollisionEnter), HookProvisoirePourDebugger);

        }

        public static void HookProvisoirePourDebugger(Action<RoR2.Projectile.ProjectileController, Collision> orig, RoR2.Projectile.ProjectileController self, Collision collision)
        {

            Debug.LogWarning("Projectile Collision");

            orig(self, collision);
        }

    }
}
