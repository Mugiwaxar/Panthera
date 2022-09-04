using Panthera.NetworkMessages;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Passives
{
    class LifeSteal
    {
        public static void Heal(CharacterBody body, Vector3 damagePosition, DamageType damageType, float damage)
        {
            if (Vector3.Distance(body.corePosition, damagePosition) < PantheraConfig.Passive_maxLifeStealDistance && damageType == DamageType.Generic)
            {
                new ServerHealSelf(body.gameObject, damage * PantheraConfig.Passive_lifeStealMultiplier).Send(NetworkDestination.Server);
            }
        }

    }
}
