﻿using Panthera.NetworkMessages;
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
        public static string Dead1 = "Dead1"; // Death
        public static string Dead2 = "Dead2"; // Death
        public static string LevelUP = "LevelUP"; // Level UP
        public static string Click1 = "Click1"; // GUI
        public static string OpenGUI = "OpenGUI"; // GUI
        public static string CloseGUI = "CloseGUI"; // GUI
        public static string ResetChar = "ResetChar"; // GUI

        public static void PopulateSounds()
        {
            using (Stream manifestResourceStream2 = Assembly.GetExecutingAssembly().GetManifestResourceStream("Panthera.Properties.PantheraBank.bnk"))
            {
                byte[] array = new byte[manifestResourceStream2.Length];
                manifestResourceStream2.Read(array, 0, array.Length);
                SoundAPI.SoundBanks.Add(array);
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
