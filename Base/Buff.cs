using Panthera;
using Panthera.Base;
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
        public static List<BuffDef> buffDefs = new List<BuffDef>();
        public static List<PantheraBuff> pantheraBuffList = new List<PantheraBuff>();

        public static PantheraBuff CupidityBuff;
        public static PantheraBuff TenacityBuff;
        public static PantheraBuff RazorsBuff;
        public static PantheraBuff BleedOutDebuff;

        public static void RegisterBuffs()
        {

            // Cupidity buff //
            CupidityBuff = ScriptableObject.CreateInstance<PantheraBuff>();
            CupidityBuff.name = "B-Cupidity";
            CupidityBuff.displayName = Utils.PantheraTokens.Get("buff_CupidityName");
            CupidityBuff.desc = String.Format(Utils.PantheraTokens.Get("buff_CupidityDesc"), PantheraConfig.Cupidity_goldMultiplier * 100);
            CupidityBuff.maxStacks = PantheraConfig.Cupidity_maxStacks;
            CupidityBuff.iconSprite = Assets.CupidityBuff;
            CupidityBuff.buffColor = Color.white;
            CupidityBuff.isDebuff = false;
            CupidityBuff.canStack = true;
            buffDefs.Add(CupidityBuff);
            pantheraBuffList.Add(CupidityBuff);

            // Tenacity buff //
            TenacityBuff = ScriptableObject.CreateInstance<PantheraBuff>();
            TenacityBuff.name = "C-Tenacity";
            TenacityBuff.displayName = Utils.PantheraTokens.Get("buff_TenacityName");
            TenacityBuff.desc = String.Format(Utils.PantheraTokens.Get("buff_TenacityDesc"), PantheraConfig.Tenacity_blockAdded * 100);
            TenacityBuff.iconSprite = Assets.TenacityBuff;
            TenacityBuff.duration = PantheraConfig.Tenacity_duration;
            TenacityBuff.maxStacks = PantheraConfig.Tenacity_maxStacks;
            TenacityBuff.buffColor = Color.white;
            TenacityBuff.isDebuff = false;
            TenacityBuff.canStack = true;
            buffDefs.Add(TenacityBuff);
            pantheraBuffList.Add(TenacityBuff);

            // Razors buff //
            RazorsBuff = ScriptableObject.CreateInstance<PantheraBuff>();
            RazorsBuff.name = "D-Razors";
            RazorsBuff.displayName = Utils.PantheraTokens.Get("buff_RazorsName");
            RazorsBuff.desc = Utils.PantheraTokens.Get("buff_RazorsDesc");
            RazorsBuff.maxStacks = PantheraConfig.Razors_maxStacks;
            RazorsBuff.iconSprite = Assets.RazorsBuff;
            RazorsBuff.buffColor = Color.white;
            RazorsBuff.isDebuff = false;
            RazorsBuff.canStack = true;
            buffDefs.Add(RazorsBuff);
            pantheraBuffList.Add(RazorsBuff);

            // Bleed Out debuff //
            BleedOutDebuff = ScriptableObject.CreateInstance<PantheraBuff>();
            BleedOutDebuff.name = "A-BleedOut";
            BleedOutDebuff.displayName = Utils.PantheraTokens.Get("buff_BleedOutName");
            BleedOutDebuff.desc = String.Format(Utils.PantheraTokens.Get("buff_BleedOutDesc"), PantheraConfig.BleedOut_damage, PantheraConfig.BleedOut_damageTime);
            BleedOutDebuff.damage = PantheraConfig.BleedOut_damage;
            BleedOutDebuff.iconSprite = Assets.BleedOutBuff;
            BleedOutDebuff.duration = PantheraConfig.BleedOut_duration;
            BleedOutDebuff.buffColor = Color.white;
            BleedOutDebuff.isDebuff = true;
            BleedOutDebuff.canStack = true;
            buffDefs.Add(BleedOutDebuff);
            pantheraBuffList.Add(BleedOutDebuff);

        } 

    }

    public class PantheraBuff : BuffDef
    {
        public string displayName;
        public string desc;
        public float duration = 0;
        public int maxStacks = 0;
        public float damage = 0;
        public int index
        {
            get 
            { 
                return (int)this.buffIndex; 
            }
        }
    }

}
