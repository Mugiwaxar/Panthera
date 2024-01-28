using R2API.Networking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panthera.NetworkMessages
{
    public class MessagesRegister
    {

        public static void Register()
        {
            // To serveur Messages //
            NetworkingAPI.RegisterMessageType<ServerAddBuff>();
            NetworkingAPI.RegisterMessageType<ServerSetBuffCount>();
            NetworkingAPI.RegisterMessageType<ServerRemoveBuff>();
            NetworkingAPI.RegisterMessageType<ServerClearBuffs>();
            NetworkingAPI.RegisterMessageType<ServerHeal>();
            NetworkingAPI.RegisterMessageType<ServerSetGodMode>();
            NetworkingAPI.RegisterMessageType<ServerInflictDamage>();
            NetworkingAPI.RegisterMessageType<ServerInflictDot>();
            NetworkingAPI.RegisterMessageType<ServerStunTarget>();
            NetworkingAPI.RegisterMessageType<ServerApplyWeak>();
            NetworkingAPI.RegisterMessageType<ServerSetElite>();
            NetworkingAPI.RegisterMessageType<ServerRespawn>();
            NetworkingAPI.RegisterMessageType<ServerAddGold>();
            NetworkingAPI.RegisterMessageType<ServerSpawnGoldOrb>();

            // To client Messages //
            NetworkingAPI.RegisterMessageType<ClientCharacterDieEvent>();

            // Character Sync //
            NetworkingAPI.RegisterMessageType<ServerChangePantheraScale>();
            NetworkingAPI.RegisterMessageType<ClientChangePantheraScale>();
            NetworkingAPI.RegisterMessageType<ServerSetBodyVelocity>();
            NetworkingAPI.RegisterMessageType<ClientSetBodyVelocity>();
            NetworkingAPI.RegisterMessageType<ServerSyncCharacter>();
            NetworkingAPI.RegisterMessageType<ClientSyncCharacter>();
            NetworkingAPI.RegisterMessageType<ClientAddFury>();

            // FX Messages //
            NetworkingAPI.RegisterMessageType<ServerSpawnEffect>();
            NetworkingAPI.RegisterMessageType<ClientSpawnEffect>();
            NetworkingAPI.RegisterMessageType<ServerDestroyEffect>();
            NetworkingAPI.RegisterMessageType<ClientDestroyEffect>();
            NetworkingAPI.RegisterMessageType<ServerSetLeapTrailFX>();
            NetworkingAPI.RegisterMessageType<ClientSetLeapTrailFX>();
            NetworkingAPI.RegisterMessageType<ClientSetDashFX>();
            NetworkingAPI.RegisterMessageType<ServerSetStealthFX>();
            NetworkingAPI.RegisterMessageType<ClientSetStealthFX>();
            NetworkingAPI.RegisterMessageType<ServerSetFuryModeFX>();
            NetworkingAPI.RegisterMessageType<ClientSetFuryModeFX>();
            NetworkingAPI.RegisterMessageType<ServerSetGuardianModeFX>();
            NetworkingAPI.RegisterMessageType<ClientSetGuardianModeFX>();
            NetworkingAPI.RegisterMessageType<ServerSetAmbitionModeFX>();
            NetworkingAPI.RegisterMessageType<ClientSetAmbitionModeFX>();

            // Animation Messages //
            NetworkingAPI.RegisterMessageType<ServerPlayAnimation>();
            NetworkingAPI.RegisterMessageType<ClientPlayAnimation>();
            NetworkingAPI.RegisterMessageType<ServerSetAnimatorBoolean>();
            NetworkingAPI.RegisterMessageType<ClientSetAnimatorBoolean>();
            NetworkingAPI.RegisterMessageType<ServerSetAnimatorFloat>();
            NetworkingAPI.RegisterMessageType<ClientSetAnimatorFloat>();

            // Sound Messages //
            NetworkingAPI.RegisterMessageType<ServerPlaySound>();
            NetworkingAPI.RegisterMessageType<ClientPlaySound>();

            // Front Shield //
            NetworkingAPI.RegisterMessageType<ClientDamageShield>();
            NetworkingAPI.RegisterMessageType<ServerSetFrontShieldAmount>();
            NetworkingAPI.RegisterMessageType<ClientSetFrontShieldAmount>();
            NetworkingAPI.RegisterMessageType<ServerSetFrontShieldActive>();
            NetworkingAPI.RegisterMessageType<ClientSetFrontShieldActive>();
            NetworkingAPI.RegisterMessageType<ServerSetFrontShieldDeployed>();
            NetworkingAPI.RegisterMessageType<ClientSetFrontShieldDeployed>();

            // Skills Messages /
            NetworkingAPI.RegisterMessageType<ServerFuryMessage>();
            NetworkingAPI.RegisterMessageType<ClientFuryMessage>();
            NetworkingAPI.RegisterMessageType<ServerGuardianMessage>();
            NetworkingAPI.RegisterMessageType<ClientGuardianMessage>();
            NetworkingAPI.RegisterMessageType<ServerAmbitionMessage>();
            NetworkingAPI.RegisterMessageType<ClientAmbitionMessage>();
            NetworkingAPI.RegisterMessageType<ServerStealthMessage>();
            NetworkingAPI.RegisterMessageType<ClientStealthMessage>();
            NetworkingAPI.RegisterMessageType<ServerSetClawsStormMessage>();
            NetworkingAPI.RegisterMessageType<ClientSetClawsStormMessage>();
            NetworkingAPI.RegisterMessageType<ClientAddConvergenceHookComp>();
            NetworkingAPI.RegisterMessageType<ServerActivateConvergenceHookComp>();
            NetworkingAPI.RegisterMessageType<ClientActivateConvergenceHookComp>();

        }

    }
}
