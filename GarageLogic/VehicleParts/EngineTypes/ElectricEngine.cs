using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    class ElectricEngine : Engine
    {
        float m_CurrentBattaryLeftHours;
        readonly float r_MaxBattaryTimeInHours;

        public void Recharge(float m_ChargeHoursToAdd)
        {
            if (((m_CurrentBattaryLeftHours + m_ChargeHoursToAdd) > r_MaxBattaryTimeInHours) || m_ChargeHoursToAdd < 0)
            {
                throw ValueOutOfRangeException;
            }
            else
            {
                m_CurrentBattaryLeftHours += m_ChargeHoursToAdd;
            }
        }

    }
}
