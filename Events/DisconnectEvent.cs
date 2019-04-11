using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using TexasLife.Database;
using TexasLife.Player;

namespace TexasLife.Events
{
    public class TLDisconnectEvent : Script
    {
        [ServerEvent(Event.PlayerDisconnected)]
        public void Event_PlayerDisconnected(Client client, DisconnectionType type, string reason)
        {
            TLPlayerStats playerStats = TLPlayerHelper.GetPlayerStats(client);

            playerStats.last_location = new double[] { client.Position.X, client.Position.Y, client.Position.Z };
            TLDatabase.Update(playerStats);
        }
    }
}
