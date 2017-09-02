using System;

namespace GarageLogic.VehicleParts.PowerSources
{
    public class FuelSource : PowerSource
    {
        public enum eFuelType
        {
            Octan95,
            Octan96,
            Octan98,
            Soler
        }

        readonly eFuelType r_FuelType;
        LimitedRangeValue m_FuelLevel;

        public override float CurrentPowerLevel
        {
            get
            {
                return m_FuelLevel.CurrentAmount;
            }
        }

        public override float PowerCapacity
        {
            get
            {
                return m_FuelLevel.MaxAmount;
            }
        }

        public eFuelType FuelType
        {
            get
            {
                return r_FuelType;
            }
        }

        public FuelSource(eFuelType i_FuelType, float i_MaxCapacityInLiters)
        {
            r_FuelType = i_FuelType;
            m_FuelLevel = new LimitedRangeValue(i_MaxCapacityInLiters);
        }

        public void Refuel(eFuelType i_FuelType, float i_FuelLitersToAdd)
        {
            validateFuelType(i_FuelType);
            m_FuelLevel.CurrentAmount += i_FuelLitersToAdd;
        }

        private void validateFuelType(eFuelType i_FuelType)
        {
            if (i_FuelType != r_FuelType)
            {
                throw new ArgumentException();
            }
        }
    }
}
