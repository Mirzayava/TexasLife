using GTANetworkAPI;

using TexasLife.World;
using TexasLife.Database;

namespace TexasLife.Server
{
    public class TLServer : Script
    {
        TLMongoDatabase db = new TLMongoDatabase();

        // Server EVENTS
        [ServerEvent(Event.ResourceStart)]
        public void EVENT_ResourceStart()
        {
           // Load World Defaults
           var world = db.GetList<TLWorldInfo>().Result;
           TLWorldInfo worldSettings = new TLWorldInfo();

           if(world.Count == 0) {
               db.Insert<TLWorldInfo>(worldSettings);
           } else {
               worldSettings = world[0];
           }

           

           worldSettings.LoadDefaultWeather();
           worldSettings.LoadDefaultTime();
        }
    }
}