using R2API.Networking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.NetworkMessages
{
    internal class MessagesRegister
    {

        public static void Register()
        {
            NetworkingAPI.RegisterMessageType<ServerAddBuff>();
            NetworkingAPI.RegisterMessageType<ServerSetBuffCount>();
            NetworkingAPI.RegisterMessageType<ServerHealSelf>();
            NetworkingAPI.RegisterMessageType<ServerInflictDamage>();
            NetworkingAPI.RegisterMessageType<ServerInflictDot>();
            NetworkingAPI.RegisterMessageType<ServerStunTarget>();
            NetworkingAPI.RegisterMessageType<ServerApplyWeak>();
            NetworkingAPI.RegisterMessageType<ServerSetElite>();
            NetworkingAPI.RegisterMessageType<ClientCharacterDieEvent>();
            NetworkingAPI.RegisterMessageType<ClientSpawnEffect>();
            NetworkingAPI.RegisterMessageType<ClientPlayAnimation>();
            NetworkingAPI.RegisterMessageType<ClientSetAnimatorBoolean>();
            NetworkingAPI.RegisterMessageType<ClientSetAnimatorFloat>();
            NetworkingAPI.RegisterMessageType<ClientPlaySound>();
            NetworkingAPI.RegisterMessageType<ClientCreateLeapCercleFX>();
            NetworkingAPI.RegisterMessageType<ClientDestroyLeapCercleFX>();
            NetworkingAPI.RegisterMessageType<ServerKeepThePrey>();
            NetworkingAPI.RegisterMessageType<ServerReleasePrey>();
            NetworkingAPI.RegisterMessageType<ServerDoMightyRoar>();
            NetworkingAPI.RegisterMessageType<ServerSetRaySlashCharge>();
            NetworkingAPI.RegisterMessageType<ClientSetShieldFX>();
            NetworkingAPI.RegisterMessageType<ClientSetLeapTrailFX>();
            NetworkingAPI.RegisterMessageType<ClientSetSuperSprintFX>();
            NetworkingAPI.RegisterMessageType<ClientSetStealFX>();
            NetworkingAPI.RegisterMessageType<ClientShieldDamage>();
            NetworkingAPI.RegisterMessageType<ServerSendFrontShield>();
            NetworkingAPI.RegisterMessageType<ServerSetFrontShield>();
            NetworkingAPI.RegisterMessageType<ServerSyncPreset>();
            NetworkingAPI.RegisterMessageType<ServerAttachHoldTargetComp>();
            NetworkingAPI.RegisterMessageType<ServerDetachHoldTargetComp>();
            NetworkingAPI.RegisterMessageType<ServerHoldTargetLastPosition>();
        }

    }
}
