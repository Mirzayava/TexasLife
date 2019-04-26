using GTANetworkAPI;
using TexasLife.World;

namespace TexasLife
{
    class Main : Script
    {
        public Main(){
            // Disable spawn on connect
            NAPI.Server.SetAutoSpawnOnConnect(false);
            // Disable Respawn once dead
            NAPI.Server.SetAutoRespawnAfterDeath(false);

            TLWorldInfo world = new TLWorldInfo();
        }

        [ServerEvent(Event.PlayerConnected)]
        public void OnPlayerConnected(Client player)
        {
            // Move camera to sky once player connected
            NAPI.ClientEvent.TriggerClientEvent(player, "moveSkyCamera", player, "up", 1, true);
            NAPI.Entity.SetEntityTransparency(player, 0);
            NAPI.Entity.SetEntityInvincible(player, true);
        }
    }
}
