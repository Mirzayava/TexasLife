using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TexasLife.Player
{
    public class TLPlayerStats
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("last_location")]
        public double[] last_location { get; set; } = new double[] { -831.945, -102.6592, 28.18537 };

        private static double[] revive_location { get; set; } = new double[] { -831.945, -102.6592, 28.18537 };

        // Airport Terminal 1 -1014.881,-2470.308, 13.87139 
        // In front of appartments 316.44, -233.94, 53.96
        // Subway -831.945, -102.6592, 28.18537

        [BsonElement("money")]
        public double money { get; set; } = 0;

        public Vector3 GetLastPosition()
        {
            return new Vector3(last_location[0], last_location[1], last_location[2]);
        }

        public static Vector3 GetRevivePosition()
        {
            return new Vector3(revive_location[0], revive_location[1], revive_location[2]);
        }

        public bool AddMoney(double money_to_add)
        {
            if (money_to_add <= 0)
                return false;

            money += money_to_add;
            return true;
        }

        public bool SubMoney(double money_to_sub)
        {
            if (money_to_sub <= 0)
                return false;

            if (money_to_sub > money)
                return false;

            money -= money_to_sub;
            if(money < 0)
            {
                money = 0;
            }
            return true;
        }

        public bool HasEnoughMoney(double amount)
        {
            if (money >= amount)
                return true;

            return false;
        }
    }
}
