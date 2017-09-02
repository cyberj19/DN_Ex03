using GarageLogic.Exceptions;

namespace GarageLogic
{
    class ElectricalSource : PowerSource
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
        public void Recharge(float m_ChargeHoursToAdd)
        {
            m_BattaryLevel.CurrentAmount += m_ChargeHoursToAdd;
        }

    }
}
