using System;
using System.IO;
using GTANetworkAPI;
using LiteDB;

// Texas Life NS
using TexasLife.Database;
using TexasLife.Handler;
using TexasLife.Player;
using TexasLife.Settings;

namespace TexasLife.Commands
{
    public class TLRegistration : Script
    {
        TLSettings LoadedSettings { get; set; }

        public TLRegistration() {

            using (var db = new LiteDatabase(@"./Database.db"))
            {
                var settings = db.GetCollection<TLSettings>();

                if (settings.Count() == 0)
                {
                    TLSettings serverSettings = new TLSettings();
                    settings.Upsert(serverSettings);
                }

                // read settings
                LoadedSettings = settings.FindById(1);
            }

            if (LoadedSettings == null)
                return;

            LoadDefaultWeather();
        }

        public void LoadDefaultWeather()
        {
            NAPI.World.SetWeather((Weather)LoadedSettings.default_weather);
        }

        [Command("register")]
        public void CMD_Register(Client client, string username, string password)
        {
            TLPlayer player = new TLPlayer(username, password);

            if (TLDatabase.GetDataByField<TLPlayer>("username", username) != null)
            {
                client.SendChatMessage("~r~That username already exists");
                return;
            }

            TLDatabase.Upsert(player);
            client.SendChatMessage("You have been registered");
        }

        [Command("login")]
        public void CMD_Login(Client client, string username, string password)
        {
            TLPlayer playerLookup = TLDatabase.GetDataByField<TLPlayer>("username", username);

            if (playerLookup == null)
            {
                client.SendChatMessage("~r~Data was not found");
                return;
            }

            if (!playerLookup.CheckPassword(password))
            {
                client.SendChatMessage("~r~Data was not found for inputs provided");
                return;
            }

            client.SetData("ID", playerLookup._id);
            TLLoginHandler.FinishLogin(client);
        }


        // Server EVENTS
        [ServerEvent(Event.ResourceStart)]
        public void EVENT_ResourceStart()
        {
            Console.WriteLine("Started Texas Life");
        }

        [ServerEvent(Event.PlayerConnected)]
        public void EVENT_PlayerConnected(Client player)
        {
            player.SendChatMessage("Welcome to Texas Life Yall");
        }

        //[ServerEvent(Event.PlayerDamage)]
        //public void EVENT_PlayerDamage(Client player, float healthLoss, float armorLoss)
        //{
        //    if (healthLoss > 20)
        //    {
        //        player.Health += 20;
        //        player.Armor += 5;
        //    }
        //}

        /*
        [ServerEvent(Event.PlayerEnterVehicle)]
        public void EVENT_PlayerEventVehicle(Client player, Vehicle veh, sbyte seatID)
        {
            NAPI.Player.WarpPlayerOutOfVehicle(player);
        }
        */


        [ServerEvent(Event.PlayerEnterColshape)]
        public void EVENT_PlayerEnterColShape(ColShape shape, Client client)
        {
            client.SendChatMessage("Entered a colshape");

            if (!shape.HasData("VehicleSpawn")) return;

            client.SetData("VehicleSpawn", true);
        }

        [ServerEvent(Event.PlayerExitColshape)]
        public void EVENT_PlayerExitColShape(ColShape shape, Client client)
        {
            client.SendChatMessage("Exited a colshape");

            if (!shape.HasData("VehicleSpawn")) return;

            client.SetData("VehicleSpawn", false);
        }
    }
}
