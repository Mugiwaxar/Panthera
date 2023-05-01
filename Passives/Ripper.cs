using Panthera.BodyComponents;
using Panthera.NetworkMessages;
using R2API.Networking;
using R2API.Networking.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.Passives
{
    public class Ripper
    {

        public static void AddBuff(PantheraObj ptraObj)
        {
            int ripperMaxBuffs = ptraObj.activePreset.theRipper_maxStack;
            int buffCount = ptraObj.characterBody.GetBuffCount(Base.Buff.TheRipperBuff);
            buffCount++;
            if (buffCount > ripperMaxBuffs) buffCount = ripperMaxBuffs;
            new ServerClearBuffs(ptraObj.gameObject, (int)Base.Buff.TheRipperBuff.buffIndex).Send(NetworkDestination.Server);
            for (int i = 1; i <= buffCount; i++)
            {
                new ServerAddBuff(ptraObj.gameObject, (int)Base.Buff.TheRipperBuff.buffIndex, PantheraConfig.TheRipper_buffDuration).Send(NetworkDestination.Server);
            }
        }

    }
}
