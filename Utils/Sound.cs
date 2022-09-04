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
        public static string IntroRoar = "IntroRoar"; // Intro
        public static string MightyRoar = "MightyRoar"; // Myghty Roar
        public static string AirCleave1 = "AirCleave1"; // Air Cleave
        public static string AirCleave2 = "AirCleave2"; // Air Cleave
        public static string ClawsStorm = "ClawsStorm"; // ClawsStorm
        public static string Leap = "Leap"; // Leap
        public static string FuriousBite = "FuriousBite"; // Furious Bite
        public static string Prowl = "Prowl"; // Prowl
        public static string Dash = "Dash"; // Dash
        public static string ShieldAbsorb = "ShieldAbsorb"; // Dash
        public static string FrontShieldBreak = "FrontShieldBreak"; // Dash
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
            AkSoundEngine.PostEvent(soundName, obj);
            if(NetworkClient.active == true && emit == true) new ClientPlaySound(obj, soundName).Send(NetworkDestination.Clients);
        }

        public static void playSound(uint soundID, GameObject obj, bool emit = true)
        {
            AkSoundEngine.PostEvent(soundID, obj);
            if (NetworkClient.active == true && emit == true) new ClientPlaySound(obj, soundID).Send(NetworkDestination.Clients);
        }

    }

}
