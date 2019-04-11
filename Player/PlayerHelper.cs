using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using TexasLife.Database;

namespace TexasLife.Player
{
    public static class TLPlayerHelper
    {
        public static TLPlayerStats GetPlayerStats(Client client)
        {
            if (!client.HasData("ID"))
                return null;

            int client_id = client.GetData("ID");
            TLPlayerStats playerStats = TLDatabase.GetByID<TLPlayerStats>(client_id);

            return playerStats;
        }

        public static Client GetPlayerFromName(string name)
        {
            return NAPI.Player.GetPlayerFromName(name);
        }
    }
}
