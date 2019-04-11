using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace TexasLife.Player
{
    public class TLPlayerStats
    {
        public int _id { get; set; }
        public double[] last_location { get; set; } = new double[] { 316.44, -233.94, 53.96 };

        public double money { get; set; } = 0;

        public Vector3 GetLastPosition()
        {
            return new Vector3(last_location[0], last_location[1], last_location[2]);
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
