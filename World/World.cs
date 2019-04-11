using System;
using GTANetworkAPI;

namespace TexasLife
{
    public class TLWorldInfo
    {
        public Weather DefaultWeather { get; set; } = (Weather)2;
        public TimeSpan DefaultTime { get; set; } = new TimeSpan(12, 0, 0);
    }
}
