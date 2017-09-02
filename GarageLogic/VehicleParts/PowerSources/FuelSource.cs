using GarageLogic.Exceptions;
using System;

namespace GarageLogic
{
    class FuelSource : PowerSource
    {
        public enum eFuelType
        {
            Octan95,
            Octan96,
            Octan98,
            Soler
        }

        readonly eFuelType r_FuelType;
        LimitedRangeValue FuelTank;
        readonly float r_MaxFuelLiters;
        float m_CurrentFuelLiters = 0;

        public override float CurrentPowerLevel
        {
            get
            {
                return FuelTank.CurrentAmount;
            }
        }

        public override float PowerCapacity
        {
            get
            {
                return FuelTank.MaxAmount;
            }
        }

        public FuelSource(eFuelType i_FuelType, float i_MaxCapacityInLiters)
        {
            r_FuelType = i_FuelType;
            r_MaxFuelLiters = i_MaxCapacityInLiters;
        }

        public void Refuel(eFuelType i_FuelType, float i_FuelLitersToAdd)
        {
            validateFuelType(i_FuelType);
            Utils.ValidateAddition(m_CurrentFuelLiters, i_FuelLitersToAdd, r_MaxFuelLiters); //todo: To a class...
            m_CurrentFuelLiters += i_FuelLitersToAdd;
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
