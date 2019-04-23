using GTANetworkAPI;

namespace TexasLife
{
    class Main : Script
    {
        public Main(){
            // Disable spawn on connect
            NAPI.Server.SetAutoSpawnOnConnect(false);
            NAPI.Server.SetAutoRespawnAfterDeath(false);
            
        }

        [ServerEvent(Event.PlayerConnected)]
        public void OnPlayerConnected(Client player)
        {
            NAPI.ClientEvent.TriggerClientEvent(player, "moveSkyCamera", player, "up", 1, true);
            NAPI.Entity.SetEntityTransparency(player, 0);
            NAPI.Entity.SetEntityInvincible(player, true);
        }
    }
}
