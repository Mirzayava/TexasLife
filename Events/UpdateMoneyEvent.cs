using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using TexasLife.Player;

namespace TexasLife.Events
{
    public static class UpdateMoneyEvent
    {
        public static void Update_Money(Client client)
        {
            TLPlayerStats player_stats = TLPlayerHelper.GetPlayerStats(client);

            if (player_stats == null)
                return;

            NAPI.ClientEvent.TriggerClientEvent(client, "updateMoney", Convert.ToSingle(player_stats.money));
        }
    }
}
