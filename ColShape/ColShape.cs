using System;
using GTANetworkAPI;

namespace TexasLife
{

    public class TLColShape
    {
        public ColShape ColShape { get; set; }
        public Marker Marker { get; set; }
        public TextLabel TextLabel { get; set; }

        public TLColShape(Client client, string text_label)
        {
            ColShape = NAPI.ColShape.CreateCylinderColShape(client.Position.Subtract(new Vector3(0, 0, 1)), 5, 5);
            Marker = NAPI.Marker.CreateMarker(1, client.Position.Subtract(new Vector3(0, 0, 1)), new Vector3(), new Vector3(), 2f, new Color(255, 255, 255, 100));
            TextLabel = NAPI.TextLabel.CreateTextLabel(text_label, client.Position, 5, 1f, 4, new Color(255,255,255,255));
        }

    }
}
