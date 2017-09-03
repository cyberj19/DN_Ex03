using System.Collections.Generic;
using GarageLogic.VehicleParts;

namespace GarageLogic.VehicleTypes
{
    public class Motorcycle : Vehicle
    {
        public enum eLicenseType
        {
            A1,
            B1,
            AA,
            BB
        }

        //todo: DID NOT PRINT THIS INFO! go back to VehiclePrintUTils. check for other things aswell
        private readonly MotorcycleInfo r_Info;

        
        public int EngineVolumeCC
        {
            get
            {
                return r_Info.EngineVolumeCC;
            }
        }

        public eLicenseType LicenseType
        {
            get
            {
                return r_Info.LicenseType;
            }
        }


        public Motorcycle(PowerSource i_PowerSource, VehicleRegistrationInfo i_VehicleInfo, List<Tire> i_Tires,
            MotorcycleInfo i_Info) 
            : base(i_PowerSource, i_VehicleInfo, i_Tires)
        {
            r_Info = i_Info;
        }

        public Motorcycle(PowerSource i_PowerSource, List<Tire> i_Tires) : base(i_PowerSource, VehicleRegistrationInfo.Default, i_Tires)
        {
            r_Info = MotorcycleInfo.Default;
        }
    }
}
