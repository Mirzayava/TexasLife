using GTANetworkAPI;

namespace TexasLife
{
    class TLVehicle
    {
        public Vehicle CreatedVehicle { get; set; }
        public Client VehicleOwner { get; set; }

        public TLVehicle(Client client, string type_of_vehicle) {
            Vehicle vehicle = NAPI.Vehicle.CreateVehicle(NAPI.Util.GetHashKey(type_of_vehicle), client.Position.Around(10), 0, 0, 0);
            
            CreatedVehicle = vehicle;
            VehicleOwner = client;

            vehicle.SetData("VehicleOwner", this); // The Owner of the vehicle
            vehicle.SetData("OwnedVehicle", this); // The Owned Vehicle
        }

        public void Lock()
        {
            if (CreatedVehicle != null) return;
            if (VehicleOwner.Vehicle != CreatedVehicle) return;

            CreatedVehicle.Locked = !CreatedVehicle.Locked;
            VehicleOwner.SendChatMessage("Vehicle Lock Status Changed");
        }

        public void Delete()
        {
            if (CreatedVehicle == null) return;

            
            VehicleOwner.ResetData("OwnedVehicle");
            CreatedVehicle.ResetData("VehicleOwner");
            CreatedVehicle.Delete();
        }
    }

}
