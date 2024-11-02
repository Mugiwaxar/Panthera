using RoR2;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Panthera.Base
{
    class Buff
    {
        public static List<BuffDef> buffDefs = new List<BuffDef>();
        public static List<PantheraBuff> pantheraBuffList = new List<PantheraBuff>();

        public static PantheraBuff EclipseBuff;
        public static PantheraBuff FrozenPawsBuff;
        public static PantheraBuff RegenerationBuff;
        public static PantheraBuff EnrageBuff;
        public static PantheraBuff ResilienceBuff;
        public static PantheraBuff TenacityBuff;
        public static PantheraBuff CupidityBuff;
        public static PantheraBuff RazorsBuff;
        public static PantheraBuff MortalMirageDebuff;
        public static PantheraBuff BleedOutDebuff;
        public static PantheraBuff IgnitionDebuff;

        public static void RegisterBuffs()
        {

            // Eclipse //
            EclipseBuff = ScriptableObject.CreateInstance<PantheraBuff>();
            EclipseBuff.name = "A-Eclipse";
            EclipseBuff.displayName = Utils.PantheraTokens.Get("buff_EclipseName");
            EclipseBuff.desc = String.Format(Utils.PantheraTokens.Get("buff_EclipseDesc"));
            EclipseBuff.maxStacks = 1;
            EclipseBuff.iconSprite = PantheraAssets.EclipseBuff;
            EclipseBuff.duration = PantheraConfig.Eclipse_duration;
            EclipseBuff.buffColor = Color.white;
            EclipseBuff.isDebuff = false;
            EclipseBuff.canStack = false;
            buffDefs.Add(EclipseBuff);
            pantheraBuffList.Add(EclipseBuff);

            // Frozen Paws //
            FrozenPawsBuff = ScriptableObject.CreateInstance<PantheraBuff>();
            FrozenPawsBuff.name = "B-FrozenPaws";
            FrozenPawsBuff.displayName = Utils.PantheraTokens.Get("buff_FrozenPawsName");
            FrozenPawsBuff.desc = String.Format(Utils.PantheraTokens.Get("buff_FrozenPawsDesc"));
            FrozenPawsBuff.maxStacks = 1;
            FrozenPawsBuff.iconSprite = PantheraAssets.FrozenPawsBuff;
            FrozenPawsBuff.duration = PantheraConfig.FrozenPaws_duration;
            FrozenPawsBuff.buffColor = Color.white;
            FrozenPawsBuff.isDebuff = false;
            FrozenPawsBuff.canStack = false;
            buffDefs.Add(FrozenPawsBuff);
            pantheraBuffList.Add(FrozenPawsBuff);

            // Regeneration //
            RegenerationBuff = ScriptableObject.CreateInstance<PantheraBuff>();
            RegenerationBuff.name = "C-Regeneration";
            RegenerationBuff.displayName = Utils.PantheraTokens.Get("buff_RegenerationName");
            RegenerationBuff.desc = String.Format(Utils.PantheraTokens.Get("buff_RegenerationDesc"), PantheraConfig.Regeneration_percentHeal * 100);
            RegenerationBuff.maxStacks = PantheraConfig.Regeneration_maxStack;
            RegenerationBuff.iconSprite = PantheraAssets.RegenerationBuff;
            RegenerationBuff.duration = PantheraConfig.Regeneration_duration;
            RegenerationBuff.buffColor = Color.white;
            RegenerationBuff.isDebuff = false;
            RegenerationBuff.canStack = true;
            buffDefs.Add(RegenerationBuff);
            pantheraBuffList.Add(RegenerationBuff);

            // Enrage //
            EnrageBuff = ScriptableObject.CreateInstance<PantheraBuff>();
            EnrageBuff.name = "D-Enrage";
            EnrageBuff.displayName = Utils.PantheraTokens.Get("buff_EnrageName");
            EnrageBuff.desc = String.Format(Utils.PantheraTokens.Get("buff_EnrageDesc"), PantheraConfig.Enrage_furyPercent * 100);
            EnrageBuff.maxStacks = PantheraConfig.Enrage_maxStack;
            EnrageBuff.iconSprite = PantheraAssets.EnrageBuff;
            EnrageBuff.duration = PantheraConfig.Enrage_duration;
            EnrageBuff.buffColor = Color.white;
            EnrageBuff.isDebuff = false;
            EnrageBuff.canStack = true;
            buffDefs.Add(EnrageBuff);
            pantheraBuffList.Add(EnrageBuff);

            // Resilience //
            ResilienceBuff = ScriptableObject.CreateInstance<PantheraBuff>();
            ResilienceBuff.name = "E-Resilience";
            ResilienceBuff.displayName = Utils.PantheraTokens.Get("buff_ResilienceName");
            ResilienceBuff.desc = String.Format(Utils.PantheraTokens.Get("buff_ResilienceDesc"), PantheraConfig.Resilience_percentArmor * 100);
            ResilienceBuff.iconSprite = PantheraAssets.ResilienceBuff;
            ResilienceBuff.duration = PantheraConfig.Resilience_duration;
            ResilienceBuff.maxStacks = PantheraConfig.Resilience_maxStack;
            ResilienceBuff.buffColor = Color.white;
            ResilienceBuff.isDebuff = false;
            ResilienceBuff.canStack = true;
            buffDefs.Add(ResilienceBuff);
            pantheraBuffList.Add(ResilienceBuff);

            // Tenacity //
            TenacityBuff = ScriptableObject.CreateInstance<PantheraBuff>();
            TenacityBuff.name = "F-Tenacity";
            TenacityBuff.displayName = Utils.PantheraTokens.Get("buff_TenacityName");
            TenacityBuff.desc = String.Format(Utils.PantheraTokens.Get("buff_TenacityDesc"), PantheraConfig.Tenacity_blockAdded * 100);
            TenacityBuff.iconSprite = PantheraAssets.TenacityBuff;
            TenacityBuff.duration = PantheraConfig.Tenacity_duration;
            TenacityBuff.maxStacks = PantheraConfig.Tenacity_maxStacks;
            TenacityBuff.buffColor = Color.white;
            TenacityBuff.isDebuff = false;
            TenacityBuff.canStack = true;
            buffDefs.Add(TenacityBuff);
            pantheraBuffList.Add(TenacityBuff);

            // Cupidity //
            CupidityBuff = ScriptableObject.CreateInstance<PantheraBuff>();
            CupidityBuff.name = "G-Cupidity";
            CupidityBuff.displayName = Utils.PantheraTokens.Get("buff_CupidityName");
            CupidityBuff.desc = String.Format(Utils.PantheraTokens.Get("buff_CupidityDesc"), PantheraConfig.Cupidity_goldMultiplier * 100);
            CupidityBuff.maxStacks = PantheraConfig.Cupidity_maxStacks;
            CupidityBuff.iconSprite = PantheraAssets.CupidityBuff;
            CupidityBuff.buffColor = Color.white;
            CupidityBuff.isDebuff = false;
            CupidityBuff.canStack = true;
            buffDefs.Add(CupidityBuff);
            pantheraBuffList.Add(CupidityBuff);

            // Razors //
            RazorsBuff = ScriptableObject.CreateInstance<PantheraBuff>();
            RazorsBuff.name = "H-Razors";
            RazorsBuff.displayName = Utils.PantheraTokens.Get("buff_RazorsName");
            RazorsBuff.desc = Utils.PantheraTokens.Get("buff_RazorsDesc");
            RazorsBuff.maxStacks = PantheraConfig.Razors_maxStacks;
            RazorsBuff.iconSprite = PantheraAssets.RazorsBuff;
            RazorsBuff.buffColor = Color.white;
            RazorsBuff.isDebuff = false;
            RazorsBuff.canStack = true;
            buffDefs.Add(RazorsBuff);
            pantheraBuffList.Add(RazorsBuff);

            // Mortal Mirage //
            MortalMirageDebuff = ScriptableObject.CreateInstance<PantheraBuff>();
            MortalMirageDebuff.name = "A-MortalMirage";
            MortalMirageDebuff.displayName = Utils.PantheraTokens.Get("buff_MortalMirageName");
            MortalMirageDebuff.desc = String.Format(Utils.PantheraTokens.Get("buff_MortalMirageDesc"));
            MortalMirageDebuff.maxStacks = 1;
            MortalMirageDebuff.iconSprite = PantheraAssets.MortalMirageBuff;
            MortalMirageDebuff.duration = PantheraConfig.MortalMirage_duration;
            MortalMirageDebuff.buffColor = Color.white;
            MortalMirageDebuff.isDebuff = true;
            MortalMirageDebuff.canStack = false;
            buffDefs.Add(MortalMirageDebuff);
            pantheraBuffList.Add(MortalMirageDebuff);

            // Bleed Out //
            BleedOutDebuff = ScriptableObject.CreateInstance<PantheraBuff>();
            BleedOutDebuff.name = "B-BleedOut";
            BleedOutDebuff.displayName = Utils.PantheraTokens.Get("buff_BleedOutName");
            BleedOutDebuff.desc = String.Format(Utils.PantheraTokens.Get("buff_BleedOutDesc"), PantheraConfig.BleedOut_damage, PantheraConfig.BleedOut_damageTime);
            BleedOutDebuff.damage = PantheraConfig.BleedOut_damage;
            BleedOutDebuff.iconSprite = PantheraAssets.BleedOutBuff;
            BleedOutDebuff.duration = PantheraConfig.BleedOut_duration;
            BleedOutDebuff.buffColor = Color.white;
            BleedOutDebuff.isDebuff = true;
            BleedOutDebuff.canStack = true;
            buffDefs.Add(BleedOutDebuff);
            pantheraBuffList.Add(BleedOutDebuff);

            // Ignition //
            IgnitionDebuff = ScriptableObject.CreateInstance<PantheraBuff>();
            IgnitionDebuff.name = "C-Ignition";
            IgnitionDebuff.displayName = Utils.PantheraTokens.Get("buff_IgnitionName");
            IgnitionDebuff.desc = String.Format(Utils.PantheraTokens.Get("buff_IgnitionDesc"), PantheraConfig.Ignition_damage, PantheraConfig.Ignition_damageTime);
            IgnitionDebuff.damage = PantheraConfig.Ignition_damage;
            IgnitionDebuff.iconSprite = PantheraAssets.IgnitionBuff;
            IgnitionDebuff.duration = PantheraConfig.Ignition_duration;
            IgnitionDebuff.buffColor = Color.white;
            IgnitionDebuff.isDebuff = true;
            IgnitionDebuff.canStack = true;
            buffDefs.Add(IgnitionDebuff);
            pantheraBuffList.Add(IgnitionDebuff);

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
