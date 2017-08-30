using GarageLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    class FuelEngine : Engine
    {
        public enum eFuelType
        {
            Octan95,
            Octan96,
            Octan98,
            Soler
        }

        readonly eFuelType m_FuelType;
        float m_CurrentFuelLiters;
        readonly float r_MaxFuelLiters;
        
        public void Refuel(eFuelType i_FuelType, float i_FuelLitersToAdd)
        {
            if(i_FuelType != m_FuelType)
            {
                throw ArgumentException;
            }
            else if(((m_CurrentFuelLiters + i_FuelLitersToAdd) > r_MaxFuelLiters) || i_FuelLitersToAdd < 0)
            {
                throw ValueOutOfRangeException;
            }
            else
            {
                m_CurrentFuelLiters += i_FuelLitersToAdd;
            }
        }
    }
}
