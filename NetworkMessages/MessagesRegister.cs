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
            NetworkingAPI.RegisterMessageType<ServerRemoveBuff>();
            NetworkingAPI.RegisterMessageType<ServerClearBuffs>();
            NetworkingAPI.RegisterMessageType<ServerHeal>();
            NetworkingAPI.RegisterMessageType<ServerInflictDamage>();
            NetworkingAPI.RegisterMessageType<ServerInflictDot>();
            NetworkingAPI.RegisterMessageType<ServerStunTarget>();
            NetworkingAPI.RegisterMessageType<ServerApplyWeak>();
            NetworkingAPI.RegisterMessageType<ServerSetElite>();
            NetworkingAPI.RegisterMessageType<ClientCharacterDieEvent>();
            NetworkingAPI.RegisterMessageType<ClientSpawnEffect>();
            NetworkingAPI.RegisterMessageType<ClientDestroyEffect>();
            NetworkingAPI.RegisterMessageType<ClientPlayAnimation>();
            NetworkingAPI.RegisterMessageType<ClientSetAnimatorBoolean>();
            NetworkingAPI.RegisterMessageType<ClientSetAnimatorFloat>();
            NetworkingAPI.RegisterMessageType<ClientPlaySound>();
            //NetworkingAPI.RegisterMessageType<ClientCreateLeapCercleFX>();
            //NetworkingAPI.RegisterMessageType<ClientDestroyLeapCercleFX>();
            //NetworkingAPI.RegisterMessageType<ServerSetRaySlashCharge>();
            //NetworkingAPI.RegisterMessageType<ClientSetShieldFX>();
            NetworkingAPI.RegisterMessageType<ClientSetLeapTrailFX>();
            NetworkingAPI.RegisterMessageType<ClientSetDashFX>();
            NetworkingAPI.RegisterMessageType<ClientSetStealthFX>();
            NetworkingAPI.RegisterMessageType<ClientShieldDamage>();
            NetworkingAPI.RegisterMessageType<ServerSendFrontShield>();
            NetworkingAPI.RegisterMessageType<ServerSetFrontShield>();
            NetworkingAPI.RegisterMessageType<ClientSetFrontShield>();
            NetworkingAPI.RegisterMessageType<ServerSyncPreset>();
            NetworkingAPI.RegisterMessageType<ServerAttachHoldTargetComp>();
            NetworkingAPI.RegisterMessageType<ServerDetachHoldTargetComp>();
            NetworkingAPI.RegisterMessageType<ClientAttachHoldTargetComp>();
            NetworkingAPI.RegisterMessageType<ClientDetachHoldTargetComp>();
            NetworkingAPI.RegisterMessageType<ServerHoldTargetLastPosition>();
            NetworkingAPI.RegisterMessageType<ClientAddComboPoint>();
            NetworkingAPI.RegisterMessageType<ServerZoneHealTargetComp>();
            NetworkingAPI.RegisterMessageType<ClientAttachZoneHealComp>();
            NetworkingAPI.RegisterMessageType<ServerRespawn>();
            NetworkingAPI.RegisterMessageType<ServerChangePantheraScale>();
            NetworkingAPI.RegisterMessageType<ClientChangePantheraScale>();
        }

    }
}
