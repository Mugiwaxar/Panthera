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

        public static string Rip1 = "Rip1"; // Rip
        public static string Rip2 = "Rip2"; // Rip
        public static string RipHit1 = "RipHit1"; // Rip
        public static string RipHit2 = "RipHit2"; // Rip
        public static string IntroRoar = "IntroRoar"; // Intro
        public static string MightyRoar = "MightyRoar"; // Myghty Roar
        public static string AirCleave1 = "AirCleave1"; // Air Cleave
        public static string AirCleave2 = "AirCleave2"; // Air Cleave
        public static string AirCleaveHit = "AirCleaveHit"; // Air Cleave
        public static string HealingCleave = "HealingCleave"; // Healing Cleave
        public static string ClawsStorm = "ClawsStorm"; // ClawsStorm
        public static string Leap = "Leap"; // Leap
        public static string FuriousBite = "FuriousBite"; // Furious Bite
        public static string Prowl = "Prowl"; // Prowl
        public static string Dash = "Dash"; // Dash
        public static string ShieldAbsorb = "ShieldAbsorb"; // Dash
        public static string FrontShieldBreak = "FrontShieldBreak"; // Front Shield
        public static string ShieldBash = "ShieldBash"; // Shield Bash
        public static string ZoneHeal = "ZoneHeal"; // Zone Heal
        public static string GhostRip = "GhostRip"; // Ghost Rip
        public static string FireRip1 = "FireRip1"; // Fire Rip
        public static string FireRip2 = "FireRip2"; // Fire Rip
        public static string Slash = "Slash"; // Slash
        public static string FireBird = "FireBird"; // Fire Bird
        public static string FireBirdLoopStart = "FireBirdLoopStart"; // Fire Bird
        public static string FireBirdLoopStop = "FireBirdLoopStop"; // Fire Bird
        public static string Revive = "Revive"; // Revive
        public static string ReviveFailed = "ReviveFailed"; // Revive
        public static string ReviveLoopPlay = "ReviveLoopPlay"; // Revive
        public static string ReviveLoopStop = "ReviveLoopStop"; // Revive
        public static string DetectionEnable = "DetectionEnable"; // Detection
        public static string DetectionDisable = "DetectionDisable"; // Detection
        public static string Dead1 = "Dead1"; // Death
        public static string Dead2 = "Dead2"; // Death
        public static string Click1 = "Click1"; // GUI
        public static string Page1 = "Page1"; // GUI
        public static string Page2 = "Page2"; // GUI
        public static string SwitchPreset = "SwitchPreset"; // GUI
        public static string ResetPreset = "ResetPreset"; // GUI
        //public static string Mangle = "Mangle"; // Mangle
        //public static string RaySlashRoar = "RaySlashRoar"; // Ray Slash
        //public static string RaySlashStart = "RaySlashStart"; // Ray Slash
        //public static string RaySlashChargeStart = "RaySlashChargeStart"; // Ray Slash
        //public static string RaySlashChargeStop = "RaySlashChargeStop"; // Ray Slash
        //public static string RaySlashLoopStart = "RaySlashLoopStart"; // Ray Slash  
        //public static string RaySlashLoopStop = "RaySlashLoopStop"; // Ray Slash
        //public static string NineLives = "NineLives"; // NineLives
        //public static string LeapCercle = "LeapCercle"; // Leap Cercle

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
                if (Utils.Functions.IsHost() == true) new ClientPlaySound(obj, soundName).Send(NetworkDestination.Clients);
                else if (Utils.Functions.IsClient() == true) new ClientPlaySound(obj, soundName).Send(NetworkDestination.Clients);
                else if (Utils.Functions.IsServer() == true) new ClientPlaySound(obj, soundName, true).Send(NetworkDestination.Clients);
            }
                
        }

        public static void playSound(uint soundID, GameObject obj, bool emit = true)
        {
            if (NetworkClient.active == true) AkSoundEngine.PostEvent(soundID, obj);
            if (emit == true)
            {
                if (Utils.Functions.IsHost() == true) new ClientPlaySound(obj, soundID).Send(NetworkDestination.Clients);
                else if (Utils.Functions.IsClient() == true) new ClientPlaySound(obj, soundID).Send(NetworkDestination.Clients);
                else if (Utils.Functions.IsServer() == true) new ClientPlaySound(obj, soundID, true).Send(NetworkDestination.Clients);
            }
        }

    }

}
