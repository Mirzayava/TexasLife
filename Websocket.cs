using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using GTANetworkInternals;

namespace TexasLife
{
    public class Websocket: Script
    {
        public Websocket()
        {
            NAPI.Exported.StuykSocket.onSocketMessageRecieved += new ExportedEvent(OnMessageSocket);
        }
        
        private void OnMessageSocket(dynamic[] parameters)
        {
            string result = parameters[0] as string;
            Console.WriteLine(result);
        }
    }
}
