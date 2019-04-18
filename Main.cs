using GTANetworkAPI;

namespace TexasLife
{
    class Main : Script
    {
        public Main(){
            NAPI.Server.SetAutoSpawnOnConnect(false);
        }

        [ServerEvent(Event.PlayerConnected)]
        public void OnPlayerConnected(Client player)
        {
            NAPI.Entity.SetEntityTransparency(player, 0);
            NAPI.Entity.SetEntityInvincible(player, true);
        }
    }
}
