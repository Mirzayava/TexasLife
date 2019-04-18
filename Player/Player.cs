using System;
using System.Collections.Generic;
using System.Text;
using BCrypt;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TexasLife.Player
{
    public class TLPlayer
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("client")]
        public string Client { get; set; }





        public TLPlayer() {
            Id = ObjectId.GenerateNewId();
        }

        public TLPlayer(string username, string password, string client)
        {
            this.Id = ObjectId.GenerateNewId();
            this.Username = username;
            this.Password = BCryptHelper.HashPassword(password, BCryptHelper.GenerateSalt());
            this.Client = client;
        }

        public bool CheckPassword(string input)
        {
            if (input == null) return false;

            return BCryptHelper.CheckPassword(input, this.Password);
        }
    }
}
