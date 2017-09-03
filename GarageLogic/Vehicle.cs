using System.Collections.Generic;
using GarageLogic.VehicleParts;

namespace GarageLogic
{
    public abstract class Vehicle
    {
        private readonly VehicleRegistrationInfo r_Info;
        private readonly List<Tire> r_Tires; //todo: changed to read only make sure can still change inner itemss
        private readonly PowerSource r_PowerSource;

        public List<Tire> Tires
        {
            get
            {
                return r_Tires;
            }
        }

        public PowerSource PowerSource
        {
            get
            {
                return r_PowerSource;
            }
        }

        public float EnergyLeftPercentage
        {
            get
            {
                return r_PowerSource.EnergyPercent;
            }
        }

        public string ModelName
        {
            get
            {
                return r_Info.ModelName;
            }
        }

        public string PlateNumber
        {
            get
            {
                return r_Info.PlateNumber;
            }
        }

        public Vehicle(PowerSource i_PowerSource, VehicleRegistrationInfo i_VehicleInfo, List<Tire> i_Tires)
        {
            r_Info = i_VehicleInfo;
            r_PowerSource = i_PowerSource;
            r_Tires = i_Tires;
        }

        public override int GetHashCode()
        {
            return PlateNumber.GetHashCode();
        }
    }
}
