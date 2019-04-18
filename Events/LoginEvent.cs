using GTANetworkAPI;


// Texas Life
using TexasLife.Database;
using TexasLife.Handler;
using TexasLife.Player;

namespace TexasLife.Events
{
    public class LoginEvent : Script
    {

        TLMongoDatabase db = new TLMongoDatabase();

        [RemoteEvent("OnPlayerLoginAttempt")]
        public void Event_OnPlayerLoginAttemptAsync(Client client, object[] arguments)
        {
            string username = (string)arguments[0];
            string password = (string)arguments[1];

            TLPlayer playerLookup;

            var query = db.GetList<TLPlayer>("username", username).Result;

            if (query.Count == 0 || query == null)
            {
                client.SendChatMessage("~r~Data was not found");
                client.TriggerEvent("LoginResult", 0);
                return;
            }

            playerLookup = query[0];

            if (!playerLookup.CheckPassword(password))
            {
                client.SendChatMessage("~r~Data was not found for inputs provided");
                client.TriggerEvent("LoginResult", 0);
                return;
            }

            client.SetData("ID", playerLookup.Id);
            TLLoginHandler.FinishLogin(client);
        }

        [RemoteEvent("OnPlayerRegisterAttempt")]
        public void Event_OnPlayerRegisterAttemptAsync(Client client, object[] arguments)
        {
            string username = (string)arguments[0];
            string password = (string)arguments[1];

            TLPlayer player = new TLPlayer(username, password, client.Name);
            var isNewUser = db.GetList<TLPlayer>("username", username).Result;

            if (isNewUser.Count > 0)
            {
                client.SendChatMessage("~r~That username already exists");
                return;
            }

            db.Insert<TLPlayer>(player);
            client.SendChatMessage("You have been registered");
        }
    }
}
