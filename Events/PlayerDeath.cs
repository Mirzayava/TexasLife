using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using TexasLife.Player;

namespace TexasLife.Events
{
    class PlayerDeath : Script
    {
        [ServerEvent(Event.PlayerDeath)]
        public void EVENT_PlayerDeath(Client client, Client killer, uint reason)
        {
            NAPI.Notification.SendNotificationToAll(killer.IsNull ? $"{client.Name} died" : $"{killer.Name} killed {client.Name}");
            NAPI.Task.Run(() =>
            {
                NAPI.Player.SpawnPlayer(client, TLPlayerStats.GetRevivePosition());
            }, delayTime: 4000);
        }
    }
}
