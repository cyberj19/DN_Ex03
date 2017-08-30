using GarageLogic.Exceptions;

namespace GarageLogic
{
    class ElectricalSource : PowerSource
    {
        float m_CurrentBattaryLeftHours = 0;
        readonly float r_MaxBattaryTimeInHours;

        public ElectricalSource(float i_MaxBattaryTimeInHours)
        {
            r_MaxBattaryTimeInHours = i_MaxBattaryTimeInHours;
        }
        public void Recharge(float m_ChargeHoursToAdd)
        {
            if (((m_CurrentBattaryLeftHours + m_ChargeHoursToAdd) > r_MaxBattaryTimeInHours) || m_ChargeHoursToAdd < 0)
            {
                throw new ValueOutOfRangeException();
            }
            else
            {
                m_CurrentBattaryLeftHours += m_ChargeHoursToAdd;
            }
        }

    }
}
