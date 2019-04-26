using System;
using GTANetworkAPI;
using MongoDB.Bson.Serialization.Attributes;
namespace TexasLife.World
{
    public class TLWorldInfo
    {
        [BsonId]
        public int _id { get; set; } = 1;
        [BsonElement("default_weather")]
        public Weather DefaultWeather { get; set; } = (Weather)2;
        [BsonElement("default_time")]
        public TimeSpan DefaultTime { get; set; } = new TimeSpan(12, 0, 0);

        public TLWorldInfo() {

        }

        public void LoadDefaultWeather()
        {
           NAPI.World.SetWeather((Weather)DefaultWeather);
        }

        public void LoadDefaultTime()
        {
            NAPI.World.SetTime(DefaultTime.Hours, DefaultTime.Minutes, DefaultTime.Seconds);
        }
    }
}
