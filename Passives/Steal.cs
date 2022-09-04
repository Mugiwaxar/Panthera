using Panthera.Components;
using Panthera.NetworkMessages;
using Panthera.Skills;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Passives
{
    class Steal
    {

        public static void DoSteal(PantheraObj ptra)
        {
            if (ptra.GetPassiveScript().stealed == true) return;
            ptra.GetPassiveScript().stealed = true;
            new ServerSetBuffCount(ptra.gameObject, (int)Buff.stealBuff.buffIndex, 1).Send(NetworkDestination.Server);
            Utils.Sound.playSound(Utils.Sound.Prowl, ptra.characterBody.gameObject);
            ptra.pantheraFX.SetStealFX(true);
            ptra.characterBody.outOfCombat = true;
            ptra.characterBody.outOfDanger = true;
            ptra.characterBody.moveSpeed -= PantheraConfig.Prowl_moveSpeedReduction;
            PantheraSkill.SkillDefsList[PantheraConfig.Prowl_SkillID].icon = Assets.ProwlActive;
        }

        public static void UnSteal(PantheraObj ptra)
        {
            if (ptra.GetPassiveScript().stealed == false) return;
            ptra.GetPassiveScript().stealed = false;
            new ServerSetBuffCount(ptra.gameObject, (int)Buff.stealBuff.buffIndex, 0).Send(NetworkDestination.Server);
            ptra.pantheraFX.SetStealFX(false);
            ptra.characterBody.outOfCombat = false;
            ptra.characterBody.outOfDanger = false;
            ptra.characterBody.moveSpeed += PantheraConfig.Prowl_moveSpeedReduction;
            PantheraSkill.SetCooldownTime(PantheraConfig.Prowl_SkillID, Time.time);
            PantheraSkill.SkillDefsList[PantheraConfig.Prowl_SkillID].icon = Assets.Prowl;
        }

    }
}
