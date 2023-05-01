using Panthera;
using Panthera.Base;
using Panthera.Skills;
using R2API;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Base
{
    class Buff
    {
        internal static List<BuffDef> buffDefs = new List<BuffDef>();

        //public static BuffDef mangleBuff;
        //public static BuffDef raySlashBuff;
        //public static BuffDef shieldBuff;
        //public static BuffDef nineLives;
        //public static BuffDef leapCercle;
        public static BuffDef StealthBuff;
        public static BuffDef TheRipperBuff;
        public static BuffDef DetectionBuff;

        public static void RegisterBuffs()
        {

            //// Nine Lives //
            //nineLives = ScriptableObject.CreateInstance<BuffDef>();
            //nineLives.name = "A-NineLives";
            //nineLives.iconSprite = Assets.NineLivesBuff;
            //nineLives.buffColor = Color.white;
            //nineLives.isDebuff = false;
            //nineLives.canStack = false;
            //Buff.buffDefs.Add(nineLives);

            //// Mangle buff //
            //mangleBuff = ScriptableObject.CreateInstance<BuffDef>();
            //mangleBuff.name = "B-MangleBuff";
            //mangleBuff.iconSprite = Assets.MangleBuff;
            //mangleBuff.buffColor = Color.white;
            //mangleBuff.isDebuff = false;
            //mangleBuff.canStack = true;
            //Buff.buffDefs.Add(mangleBuff);

            //// Ray Slash buff //
            //raySlashBuff = ScriptableObject.CreateInstance<BuffDef>();
            //raySlashBuff.name = "C-RaySlashBuff";
            //raySlashBuff.iconSprite = Assets.RaySlashBuff;
            //raySlashBuff.buffColor = Color.white;
            //raySlashBuff.isDebuff = false;
            //raySlashBuff.canStack = true;
            //Buff.buffDefs.Add(raySlashBuff);

            //// Leap Cercle //
            //leapCercle = ScriptableObject.CreateInstance<BuffDef>();
            //leapCercle.name = "D-LeapCercle";
            //leapCercle.iconSprite = Assets.LeapCercleBuff;
            //leapCercle.buffColor = Color.white;
            //leapCercle.isDebuff = false;
            //leapCercle.canStack = true;
            //Buff.buffDefs.Add(leapCercle);

            //// Shield buff //
            //shieldBuff = ScriptableObject.CreateInstance<BuffDef>();
            //shieldBuff.name = "E-Shield";
            //shieldBuff.iconSprite = Assets.ShieldBuff;
            //shieldBuff.buffColor = Color.white;
            //shieldBuff.isDebuff = false;
            //shieldBuff.canStack = true;
            //Buff.buffDefs.Add(shieldBuff);

            // Stealth buff //
            StealthBuff = ScriptableObject.CreateInstance<BuffDef>();
            StealthBuff.name = "A-Steal";
            StealthBuff.iconSprite = Assets.StealthBuff;
            StealthBuff.buffColor = Color.white;
            StealthBuff.isDebuff = false;
            StealthBuff.canStack = false;
            buffDefs.Add(StealthBuff);

            // The Rip-per buff //
            TheRipperBuff = ScriptableObject.CreateInstance<BuffDef>();
            TheRipperBuff.name = "C-TheRipper";
            TheRipperBuff.iconSprite = Assets.TheRipperAbility;
            TheRipperBuff.buffColor = Color.white;
            TheRipperBuff.isDebuff = false;
            TheRipperBuff.canStack = true;
            buffDefs.Add(TheRipperBuff);

            // Detection buff //
            DetectionBuff = ScriptableObject.CreateInstance<BuffDef>();
            DetectionBuff.name = "B-Detection";
            DetectionBuff.iconSprite = Assets.Detection;
            DetectionBuff.buffColor = Color.white;
            DetectionBuff.isDebuff = false;
            DetectionBuff.canStack = false;
            buffDefs.Add(DetectionBuff);

        }

    }
}
