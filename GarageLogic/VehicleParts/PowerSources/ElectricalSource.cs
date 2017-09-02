using GarageLogic.Exceptions;

namespace GarageLogic
{
    class ElectricalSource : PowerSource
    {
        //todo: Readonly comes first, before other variables that are non-const and non-static
        float m_CurrentBattaryLeftHours = 0;
        readonly float r_MaxBattaryTimeInHours;

        public ElectricalSource(float i_MaxBattaryTimeInHours)
        {
            r_MaxBattaryTimeInHours = i_MaxBattaryTimeInHours;
        }
        public void Recharge(float m_ChargeHoursToAdd)
        {
            //todo: Unreadable if
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
