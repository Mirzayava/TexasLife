
using GTANetworkAPI;
using MongoDB.Bson;


using TexasLife.Database;
using TexasLife.Events;
using TexasLife.Player;

namespace TexasLife.Handler
{
    public static class TLLoginHandler
    {

  

        public static void FinishLogin(Client client)
        {
            TLMongoDatabase db = new TLMongoDatabase();
            ObjectId client_id = client.GetData("ID");
            TLPlayerStats playerStats;

            var query = db.GetListById<TLPlayerStats>(client_id).Result;
            
            if (query.Count == 0)
            {
                playerStats = new TLPlayerStats();
                playerStats.Id = client_id;
                db.Insert<TLPlayerStats>(playerStats);
            } else
            {
                playerStats = query[0];
            }

            

            client.Position = playerStats.GetLastPosition();

            NAPI.Entity.SetEntityTransparency(client, 255);
            NAPI.Entity.SetEntityInvincible(client, false);
            NAPI.ClientEvent.TriggerClientEvent(client, "playerLoggedIn");
            NAPI.ClientEvent.TriggerClientEvent(client, "LoginResult", 1);

            UpdateMoneyEvent.Update_Money(client);
        }
    }
}
