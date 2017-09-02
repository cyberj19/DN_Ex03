using System.Collections.Generic;
using GarageLogic.VehicleParts;

namespace GarageLogic.VehicleTypes
{
    class Motorcycle : Vehicle
    {
        public enum eLicenseType
        {
            A1,
            B1,
            AA,
            BB
        }
        //todo: Same review notes as truck.cs
        protected eLicenseType m_LicenseType;
        protected int m_EngineVolumeCC;
        protected const float k_MaxAllowedWheelPSI = 28;
        protected const byte k_NumOfWheels = 2;

        public Motorcycle(PowerSource i_PowerSource, VehicleRegistrationInfo i_VehicleInfo, List<Tire> i_Tires,
            eLicenseType i_LisenceType, int i_EngineVolumeCC) 
            : base(i_PowerSource, i_VehicleInfo, i_Tires)
        {

        }
    }
}
