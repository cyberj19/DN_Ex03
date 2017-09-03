using System.Collections.Generic;
using GarageLogic.VehicleParts;

namespace GarageLogic.VehicleTypes
{
    public class Truck : Vehicle
    {
        //todo: Access this, and give releveant inormation to VehiclePrintUtils to all these 3 classes
        private readonly TruckInfo r_TruckInfo;
        
        public Truck(PowerSource i_PowerSource, VehicleRegistrationInfo i_VehicleInfo, List<Tire> i_Tires,
            TruckInfo i_TruckInfo) 
            :base(i_PowerSource, i_VehicleInfo, i_Tires)
        {
            r_TruckInfo = i_TruckInfo;
        }

        public Truck(PowerSource i_PowerSource, List<Tire> i_Tires) : base(i_PowerSource, VehicleRegistrationInfo.Default, i_Tires)
        {
            r_TruckInfo = TruckInfo.Default;       
        }
    }
}
