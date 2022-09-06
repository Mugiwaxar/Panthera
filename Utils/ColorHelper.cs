using Panthera.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Utils
{
    internal class ColorHelper
    {

        public static string EnergyColor = "F9FF00";
        public static string PowerColor = "6F00EC";
        public static string FuryColor = "FF4A00";
        public static string ShieldColor = "B0E0E6";
        public static string CPColor = "0000FF";
        public static string Green = "50C878";
        public static string Red = "FF0000";
        public static string Cyan = "30F9FF";

        public static string SetEnergy(String text)
        {
            return String.Format("<color=#{0}>{1}</color>", EnergyColor, text);
        }

        public static string SetPower(String text)
        {
            return String.Format("<color=#{0}>{1}</color>", PowerColor, text);
        }

        public static string SetFury(String text)
        {
            return String.Format("<color=#{0}>{1}</color>", FuryColor, text);
        }

        public static string SetShield(String text)
        {
            return String.Format("<color=#{0}>{1}</color>", ShieldColor, text);
        }

        public static string SetCP(String text)
        {
            return String.Format("<color=#{0}>{1}</color>", CPColor, text);
        }

        public static string SetGreen(String text)
        {
            return String.Format("<color=#{0}>{1}</color>", Green, text);
        }

        public static string SetRed(String text)
        {
            return String.Format("<color=#{0}>{1}</color>", Red, text);
        }

        public static string SetCyan(String text)
        {
            return String.Format("<color=#{0}>{1}</color>", Cyan, text);
        }

        public static string SetDamage(String text)
        {
            return String.Format("<style=cIsDamage>{0}</style>", text);
        }

        public static string SetUtility(String text)
        {
            return String.Format("<style=cIsUtility>{0}</style>", text);
        }
        
        public static string SetHeal(String text)
        {
            return String.Format("<style=cIsHealing>{0}</style>", text);
        }

        public static string GetColorForAbility(String text, int abilityID, int level)
        {
            if (Preset.SelectedPreset == null) return text;
            if (Preset.SelectedPreset.getAbilityLevel(abilityID) == level) return SetGreen(text);
            return text;
        }

    }
}
