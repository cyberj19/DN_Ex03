using System;

namespace GarageLogic.VehicleParts.PowerSources
{
    public class ElectricalSource : PowerSource
    {
        LimitedRangeValue m_BattaryLevel;

        public override float CurrentPowerLevel
        {
            get
            {
                return m_BattaryLevel.CurrentAmount;
            }
        }

        public override float PowerCapacity
        {
            get
            {
                return m_BattaryLevel.MaxAmount;
            }
        }

        public ElectricalSource(float i_MaxBattaryTimeInHours)
        {
            m_BattaryLevel = new LimitedRangeValue(i_MaxBattaryTimeInHours);
        }

        // Charge battery
        public void Recharge(float m_ChargeHoursToAdd)
        {
            m_BattaryLevel.CurrentAmount += m_ChargeHoursToAdd;
        }

        public override PowerSource duplicate(float i_InitialCapacity)
        {
            ElectricalSource newSource = new ElectricalSource(m_BattaryLevel.MaxAmount);

            newSource.Recharge(i_InitialCapacity);

            return newSource;
        }
    }
}
