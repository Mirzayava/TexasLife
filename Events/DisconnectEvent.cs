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
        TLMongoDatabase db = new TLMongoDatabase();

        [ServerEvent(Event.PlayerDisconnected)]
        public void Event_PlayerDisconnectedAsync(Client client, DisconnectionType type, string reason)
        {
            TLPlayerStats playerStats = TLPlayerHelper.GetPlayerStats(client);

            playerStats.last_location = new double[] { client.Position.X, client.Position.Y, client.Position.Z };

            db.Update<TLPlayerStats>(playerStats.Id, "last_location", playerStats.last_location.ToString());
        }
    }
}
