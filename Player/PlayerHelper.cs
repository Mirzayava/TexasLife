using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using MongoDB.Bson;
using TexasLife.Database;

namespace TexasLife.Player
{
    public static class TLPlayerHelper
    {
        

        public static TLPlayerStats GetPlayerStats(Client client)
        {
            TLMongoDatabase db = new TLMongoDatabase();

            if (!client.HasData("ID"))
                return null;

            TLPlayerStats playerStats;
            ObjectId client_id = client.GetData("ID");

            var query = db.GetListById<TLPlayerStats>(client_id).Result;

            if (query.Count == 0)
            {
                playerStats = new TLPlayerStats();
                playerStats.Id = client_id;
                db.Insert<TLPlayerStats>(playerStats);
            }
            else
            {
                playerStats = query[0];
            }

            return playerStats;
        }

        public static Client GetPlayerFromName(string name)
        {
            return NAPI.Player.GetPlayerFromName(name);
        }
    }
}
