using System;
using System.Collections.Generic;
using System.Text;
using BCrypt;

namespace TexasLife.Player
{
    public class TLPlayer
    {
        public int _id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
       


        public TLPlayer() { }
        public TLPlayer(string username, string password)
        {
            this.username = username;
            this.password = BCryptHelper.HashPassword(password, BCryptHelper.GenerateSalt());
        }

        public bool CheckPassword(string input)
        {
            if (input == null) return false;

            return BCryptHelper.CheckPassword(input, this.password);
        }
    }
}
