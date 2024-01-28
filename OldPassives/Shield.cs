using Panthera;
using Panthera.Components;
using Panthera.NetworkMessages;
using Panthera.Passives;
using Panthera.OldSkills;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Passives
{
    //class Shield
    //{

    //    public static void SetShieldState(PantheraFX fx, bool setValue, bool server = false)
    //    {
    //        if (server == false) fx.SetShieldFX(setValue);
    //        else fx.ServerSetShieldFX(setValue);
    //    }

    //    public static void CheckShieldState(CharacterBody body, PantheraFX fx)
    //    {
    //        if (body.HasBuff(Buff.shieldBuff)) SetShieldState(fx, true);
    //        else SetShieldState(fx, false);
    //    }

    //    public static void AddShieldStack(CharacterBody body, PantheraFX fx)
    //    {
    //        int shieldBuffCount = body.GetBuffCount(Buff.shieldBuff);
    //        if (shieldBuffCount < PantheraConfig.Shield_maxStack)
    //        {
    //            new ServerAddBuff(body.gameObject, (int)Buff.shieldBuff.buffIndex, PantheraConfig.Shield_duration).Send(NetworkDestination.Server);
    //            SetShieldState(fx, true);
    //        }
    //    }

    //    public static float MustUseShield(CharacterBody body, float damage)
    //    {
    //        if (body.HasBuff(Buff.shieldBuff) && damage > 0)
    //        {
    //            int buffsToAdd = (int)Math.Max(1, damage * PantheraConfig.Shield_damageAbsoptionShieldMultiplier);
    //            buffsToAdd = Math.Min(buffsToAdd, PantheraConfig.RaySlash_maxRaySlashBuff - body.GetBuffCount(Buff.raySlashBuff));
    //            body.SetBuffCount(Buff.raySlashBuff.buffIndex, body.GetBuffCount(Buff.raySlashBuff) + buffsToAdd);
    //            float shieldBuffCount = body.GetBuffCount(Buff.shieldBuff);
    //            float damageReductionCoefficient = PantheraConfig.Shield_damageCoefficientPerStack * shieldBuffCount;
    //            damageReductionCoefficient = 1 - damageReductionCoefficient;
    //            damage = damage * damageReductionCoefficient;
    //            return damage;
    //        }
    //        return damage;
    //    }

    //}
}
