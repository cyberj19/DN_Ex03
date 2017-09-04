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

        private readonly eFuelType r_FuelType;
        private LimitedFloatValue m_FuelLevel;

        public override float CurrentPowerLevel
        {
            get
            {
                return m_FuelLevel.Value;
            }
        }

        public override float PowerCapacity
        {
            get
            {
                return m_FuelLevel.Max;
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
            m_FuelLevel = new LimitedFloatValue(i_MaxCapacityInLiters);
        }

        // Fuel power source
        public void Refuel(eFuelType i_FuelType, float i_FuelLitersToAdd)
        {
            validateFuelType(i_FuelType);
            m_FuelLevel.Value += i_FuelLitersToAdd;
        }

        // validate using correct fuel
        private void validateFuelType(eFuelType i_FuelType)
        {
            if (i_FuelType != r_FuelType)
            {
                throw new ArgumentException();
            }
        }

        public override PowerSource duplicate(float i_InitialCapacity)
        {
            FuelSource newSource = new FuelSource(r_FuelType, m_FuelLevel.Max);

            newSource.Refuel(r_FuelType, i_InitialCapacity);

            return newSource;
        }
    }
}
