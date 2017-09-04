using System;

namespace GarageLogic.VehicleParts.PowerSources
{
    public class ElectricalSource : PowerSource
    {
        LimitedFloatValue m_BattaryLevel;

        public override float CurrentPowerLevel
        {
            get
            {
                return m_BattaryLevel.Value;
            }
        }

        public override float PowerCapacity
        {
            get
            {
                return m_BattaryLevel.Max;
            }
        }

        public ElectricalSource(float i_MaxBattaryTimeInHours)
        {
            m_BattaryLevel = new LimitedFloatValue(i_MaxBattaryTimeInHours);
        }

        // Charge battery
        public void Recharge(float m_ChargeHoursToAdd)
        {
            m_BattaryLevel.Value += m_ChargeHoursToAdd;
        }

        public override PowerSource duplicate(float i_InitialCapacity)
        {
            ElectricalSource newSource = new ElectricalSource(m_BattaryLevel.Max);

            newSource.Recharge(i_InitialCapacity);

            return newSource;
        }
    }
}
