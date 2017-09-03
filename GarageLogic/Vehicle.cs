using System.Collections.Generic;
using GarageLogic.VehicleParts;

namespace GarageLogic
{
    public abstract class Vehicle
    {
        readonly VehicleRegistrationInfo r_Info;
        protected List<Tire> m_Tires;
        protected PowerSource m_PowerSource;

        public List<Tire> Tires
        {
            get
            {
                return m_Tires;
            }
        }

        public PowerSource PowerSource
        {
            get
            {
                return m_PowerSource;
            }
        }

        public float EnergyLeftPercentage
        {
            get
            {
                return m_PowerSource.EnergyPercent;
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
            m_PowerSource = i_PowerSource;
            m_Tires = i_Tires;
        }

        public override int GetHashCode()
        {
            return PlateNumber.GetHashCode();
        }
    }
}
