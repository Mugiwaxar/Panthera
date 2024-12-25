using Panthera.NetworkMessages;
using R2API;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Panthera.Utils
{
    class Sound
    {

        public const string soundBankFolder = "SoundBanks";
        public const string soundBankFileName = "PantheraBank.bnk";
        public const string soundBankName = "PantheraBank";
        public static uint soundBankId;

        public static string Dodge = "Dodge"; // Dodge
        public static string Block = "Block"; // Block
        public static string Reduced = "Reduced"; // Reduced
        public static string Rip1 = "Rip1"; // Rip
        public static string RipHit1 = "RipHit1"; // Rip
        public static string IntroRoar = "IntroRoar"; // Intro
        public static string MightyRoar = "MightyRoar"; // Myghty Roar
        public static string AirCleave1 = "AirCleave1"; // Air Cleave
        public static string AirCleave2 = "AirCleave2"; // Air Cleave
        public static string AirCleaveHit = "AirCleaveHit"; // Air Cleave
        public static string ClawsStorm = "ClawsStorm"; // ClawsStorm
        public static string Leap = "Leap"; // Leap
        public static string Prowl = "Prowl"; // Prowl
        public static string ShieldAbsorb = "ShieldAbsorb"; // Dash
        public static string FrontShieldBreak = "FrontShieldBreak"; // Front Shield
        public static string ShieldBash = "ShieldBash"; // Shield Bash
        public static string GhostRip = "GhostRip"; // Ghost Rip
        public static string Slash = "Slash"; // Slash
        public static string DetectionEnable = "DetectionEnable"; // Detection
        public static string DetectionDisable = "DetectionDisable"; // Detection
        public static string FuryOn = "FuryOn"; // Fury Activated
        public static string GardianOn = "GardianOn"; // Gardian Activated
        public static string GardianOff = "GardianOff"; // Gardian Desactivated
        public static string AmbitionOn = "AmbitionOn"; // Ambition Activated
        public static string AmbitionBuff = "AmbitionBuff"; // Ambition Buff added
        public static string AirSlash = "AirSlash"; // Air Slash
        public static string GoldenRip = "GoldenRip"; // Golden Rip
        public static string ArcaneAnchor = "ArcaneAnchor"; // Arcane Anchor
        public static string ConvergenceHook = "ConvergenceHook"; // Convergence Hook
        public static string PortalCharging = "PortalCharging"; // Portal Surge
        public static string PortalChargingStop = "PortalChargingStop"; // Portal Surge
        public static string PortalChargeFailed = "PortalChargeFailed"; // Portal Surge
        public static string Regeneration = "Regeneration"; // Regeneration
        public static string Resilience = "Resilience"; // Resilience
        public static string Enrage = "Enrage"; // Enrage
        public static string FireRip1 = "FireRip1"; // Fire Rip
        public static string FeralBite = "FeralBite"; // Feral Bite
        public static string Dead1 = "Dead1"; // Death
        public static string Dead2 = "Dead2"; // Death
        public static string LevelUP = "LevelUP"; // Level UP
        public static string Click1 = "Click1"; // GUI
        public static string OpenGUI = "OpenGUI"; // GUI
        public static string CloseGUI = "CloseGUI"; // GUI
        public static string ResetChar = "ResetChar"; // GUI

        public static void PopulateSounds()
        {

            string soundBankDirectory = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Panthera.PInfo.Location), soundBankFolder);

            var akResult = AkSoundEngine.AddBasePath(soundBankDirectory);
            if (akResult == AKRESULT.AK_Success)
            {
                Debug.Log($"Added bank base path : {soundBankDirectory}");
            }
            else
            {
                Debug.LogError(
                    $"Error adding base path : {soundBankDirectory} " +
                    $"Error code : {akResult}");
            }

            akResult = AkSoundEngine.LoadBank(soundBankFileName, out soundBankId);
            if (akResult == AKRESULT.AK_Success)
            {
                Debug.Log($"Added bank : {soundBankFileName}");
            }
            else
            {
                Debug.LogError(
                    $"Error loading bank : {soundBankFileName} " +
                    $"Error code : {akResult}");
            }

        }

        public static void playSound(string soundName, GameObject obj, bool emit = true)
        {
            if (NetworkClient.active == true) AkSoundEngine.PostEvent(soundName, obj);
            if (emit == true)
            {
                if (Utils.Functions.IsHost() == true) new ServerPlaySound(obj, soundName).Send(NetworkDestination.Server);
                else if (Utils.Functions.IsClient() == true) new ServerPlaySound(obj, soundName).Send(NetworkDestination.Server);
                else if (Utils.Functions.IsServer() == true) new ServerPlaySound(obj, soundName, true).Send(NetworkDestination.Server);
            }

        }

    }

}
