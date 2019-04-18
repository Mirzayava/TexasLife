using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using TexasLife.Database;
using TexasLife.Player;
using TexasLife.Events;

namespace TexasLife.Commands
{
    public class TLMoney : Script
    {
        TLMongoDatabase db = new TLMongoDatabase();

        [Command("getbalance")]
        public void CMD_GetBalance(Client client)
        {
            TLPlayerStats playerStats = TLPlayerHelper.GetPlayerStats(client);

            if (playerStats == null)
            {
                client.SendChatMessage($"~r~Player has no wallet");
                return;
            }

            client.SendChatMessage($"Your Balance is: ${playerStats.money}");
            UpdateMoneyEvent.Update_Money(client);
        }

        [Command("givemoney")]
        public void CMD_GiveMoney(Client client, string username, double amount)
        {

            if(client.Name.ToLower() == username)
            {
                client.SendChatMessage($"You can't give yourself money.");
                return;
            }

            TLPlayerStats sendingPlayerStats = TLPlayerHelper.GetPlayerStats(client);
            TLPlayer recevingPlayerInfo = db.GetSingle<TLPlayer>("username", username).Result;

            if(recevingPlayerInfo == null)
            {
                client.SendChatMessage("That username does not exist");
                return;
            }

            TLPlayerStats receivingPlayerStats = db.GetSingle<TLPlayerStats>("username", recevingPlayerInfo.Username).Result;

            if(receivingPlayerStats == null)
            {
                Console.WriteLine($"{recevingPlayerInfo.Username} does not have TLPlayerStats table.");
                return;
            }

            bool sentMoney = sendingPlayerStats.SubMoney(amount);

            if(!sentMoney)
            {
                client.SendChatMessage("~r~You dont not have the sufficient funds for that.");
                return;
            }
            receivingPlayerStats.AddMoney(amount);

            db.Update<TLPlayerStats>(sendingPlayerStats.Id, "money", sendingPlayerStats.money.ToString());
            db.Update<TLPlayerStats>(receivingPlayerStats.Id, "money", receivingPlayerStats.money.ToString());

            client.SendChatMessage($"You sent ${amount} to {recevingPlayerInfo.Username}");

            UpdateMoneyEvent.Update_Money(client);

            Client receivingPlayerClient = NAPI.Player.GetPlayerFromName(username);

            if (receivingPlayerClient == null)
                return;

            UpdateMoneyEvent.Update_Money(receivingPlayerClient);
        }

        [Command("burnmoney")]
        public void CMD_BurnMoney(Client client, double amount)
        {
            TLPlayerStats playerStats = TLPlayerHelper.GetPlayerStats(client);

            if (playerStats == null)
                return;

            bool result = playerStats.SubMoney(amount);
            db.Update<TLPlayerStats>(playerStats.Id, "money", playerStats.money.ToString());

            if (result) {
                client.SendChatMessage($"~y~You burnt ${amount}");
            } else {
                client.SendChatMessage($"~r~Something went wrong.");
            }

            UpdateMoneyEvent.Update_Money(client);
        }

        [Command("updatebalance")]
        public void CMD_AddBalance(Client client, double amount)
        {
            TLPlayerStats playerStats = TLPlayerHelper.GetPlayerStats(client);

            if (playerStats == null)
                return;

            bool result = playerStats.AddMoney(amount);
            db.Update<TLPlayerStats>(playerStats.Id, "money", amount.ToString());

            if (result) {
                client.SendChatMessage($"~g~You recieved ${amount}");
            } else {
                client.SendChatMessage($"~r~Something went wrong.");
            }

            UpdateMoneyEvent.Update_Money(client);
        }
    }
}
