using System;
using System.IO;
using GTANetworkAPI;

namespace TexasLife.Commands
{
    class TLCommands {

        [Command("cv")]
        public void CMD_CreateVehicle(Client client, string vehicle_name)
        {
            if (!client.HasData("VehicleSpawn")) return;
            if (!client.GetData("VehicleSpawn")) return;

            if (client.HasData("OwnedVehicle"))
            {
                TLVehicle previous_vehicle = client.GetData("OwnedVehicle");
                previous_vehicle.Delete();
            }
            TLVehicle customVehicle = new TLVehicle(client, vehicle_name);
        }

        [Command("cm")]
        public void CMD_CreateMoneyBag(Client sender)
        {
            Vector3 pos = NAPI.Entity.GetEntityPosition(sender);
            uint playerDimension = NAPI.Entity.GetEntityDimension(sender);

            NAPI.Object.CreateObject(NAPI.Util.GetHashKey("prop_money_bag_01"), pos - new Vector3(0, 0, 1f), new Vector3(), 255, playerDimension);
            NAPI.ColShape.CreateSphereColShape(pos, 10);
        }

        [Command("rv")]
        public void CMD_RepairVehicle(Client client)
        {
            Vehicle veh = client.Vehicle;
            veh.Repair();
        }

        [Command("lv")]
        public void CMD_LockVehicle(Client client)
        {
            /*
            bool was_vehicle_found = false;

            foreach(Vehicle vehicle in NAPI.Pools.GetAllVehicles())
            {
                if(vehicle.Position.DistanceTo2D(client.Position) <= 3)
                {
                    vehicle.Locked = !vehicle.Locked;
                    was_vehicle_found = true;
                    client.SendChatMessage($"Your Vehicle lock is now {vehicle.Locked}");
                    break;
                }
            }

            if (was_vehicle_found)
                return;

            client.SendChatMessage("No Vehicle was found near you.");*/


            //if(!client.HasData("PersonalVehicle"))
            //{
            //    client.SendChatMessage("You must create a personal vehicle first. /createvehicle [name]");
            //    return;
            //}

            //Vehicle personal_vehicle = client.GetData("PersonalVehicle");

            //if(client.Position.DistanceTo2D(personal_vehicle.Position) > 10)
            //{
            //    client.SendChatMessage("You are not close enough to unlock your personal vehicle");
            //    return;
            //}

            //personal_vehicle.Locked = !personal_vehicle.Locked;
            //client.SendChatMessage($"Your Vehicle lock is currently set to {personal_vehicle.Locked}");
        }

        [Command("spawn")]
        public void CMD_SpawnPoint(Client client)
        {
            TLColShape tlColShape = new TLColShape(client, "Vehicle Creation Zone");
            tlColShape.ColShape.SetData("VehicleSpawn", tlColShape);
        }

        //[Command("cv")]
        //public void CMD_CreateVehicle(Client client, string vehicle_name)
        //{
        //    if (client.HasData("PersonalVehicle"))
        //    {
        //        Vehicle previous_veh = client.GetData("PersonalVehicle");
        //        previous_veh.Delete();
        //    }

        //    // spawn a new vehicle
        //    uint hash = NAPI.Util.GetHashKey(vehicle_name);
        //    Vehicle veh = NAPI.Vehicle.CreateVehicle(hash, client.Position.Around(5), 0, 0, 0);
        //    client.SendChatMessage("~g~ Created a " + vehicle_name + " for you!");


        //    client.SetData("PersonalVehicle", veh);
        //}
    }
}
