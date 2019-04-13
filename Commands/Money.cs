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
            TLPlayer recevingPlayerInfo = TLDatabase.GetDataByField<TLPlayer>("username", username);

            if(recevingPlayerInfo == null)
            {
                client.SendChatMessage("That username does not exist");
                return;
            }

            TLPlayerStats receivingPlayerStats = TLDatabase.GetByID<TLPlayerStats>(recevingPlayerInfo._id);

            if(receivingPlayerStats == null)
            {
                Console.WriteLine($"{recevingPlayerInfo.username} does not have TLPlayerStats table.");
                return;
            }

            bool sentMoney = sendingPlayerStats.SubMoney(amount);

            if(!sentMoney)
            {
                client.SendChatMessage("~r~You dont not have the sufficient funds for that.");
                return;
            }
            receivingPlayerStats.AddMoney(amount);

            TLDatabase.Update(sendingPlayerStats);
            TLDatabase.Update(receivingPlayerStats);

            client.SendChatMessage($"You sent ${amount} to {recevingPlayerInfo.username}");

            UpdateMoneyEvent.Update_Money(client);

            Client receivingPlayerClient = TLPlayerHelper.GetPlayerFromName(username);

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
            TLDatabase.Update(playerStats);

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
            TLDatabase.Update(playerStats);

            if(result) {
                client.SendChatMessage($"~g~You recieved ${amount}");
            } else {
                client.SendChatMessage($"~r~Something went wrong.");
            }

            UpdateMoneyEvent.Update_Money(client);
        }
    }
}
