using Panthera.Base;
using Panthera.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Utils
{
    public class ColorHelper
    {

        public static string EnergyColor = "F9FF00";
        public static string PowerColor = "6F00EC";
        public static string FuryColor = "FF4A00";
        public static string ShieldColor = "B0E0E6";
        public static string BarrierColor = "FFE800";
        public static string BlockColor = "805500";
        public static string ComboPointColor = "AFAF00";
        public static string StaminaColor = "87CEEB";
        public static string BuffColor = "00FFF0";
        public static string DeBuffColor = "38005F";
        public static string MasteryColor = "9F00DB";
        public static string Green = "50C878";
        public static string Red = "FF0000";
        public static string Cyan = "30F9FF";
        public static string Blue = "1D59FE";        
        public static string Yellow = "FFEA00";        

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

        public static string SetBarrier(String text)
        {
            return String.Format("<color=#{0}>{1}</color>", BarrierColor, text);
        }

        public static string SetBlock(String text)
        {
            return String.Format("<color=#{0}>{1}</color>", BlockColor, text);
        }

        public static string SetComboPoint(String text)
        {
            return String.Format("<color=#{0}>{1}</color>", ComboPointColor, text);
        }

        public static string SetStamina(string text)
        {
            return String.Format("<color=#{0}>{1}</color>", StaminaColor, text);
        }

        public static string SetBuff(string text)
        {
            return String.Format("<color=#{0}>{1}</color>", BuffColor, text);
        }

        public static string SetDeBuff(string text)
        {
            return String.Format("<color=#{0}>{1}</color>", DeBuffColor, text);
        }

        public static string SetMastery(string text)
        {
            return String.Format("<color=#{0}>{1}</color>", MasteryColor, text);
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

        public static string SetBlue(String text)
        {
            return String.Format("<color=#{0}>{1}</color>", Blue, text);
        }

        public static string SetYellow(String text)
        {
            return String.Format("<color=#{0}>{1}</color>", Yellow, text);
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

    }
}
