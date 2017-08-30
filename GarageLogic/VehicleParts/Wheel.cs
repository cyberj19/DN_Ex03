using GarageLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    class Wheel
    {
        readonly string m_ManufacturerName;
        readonly float r_MaxPSI;
        float m_CurrentPSI;

        public void InflateAir(float i_PressureToAdd)
        {
            if((m_CurrentPSI + i_PressureToAdd) > r_MaxPSI || i_PressureToAdd < 0)
            {
                throw ValueOutOfRangeException;
            }
            else
            {
                m_CurrentPSI += i_PressureToAdd;
            }
        }

        public Wheel(float i_MaxPSI, string i_ManufacturerName)
        {
            r_MaxPSI = i_MaxPSI;
            m_ManufacturerName = i_ManufacturerName;
        }

    }
}
