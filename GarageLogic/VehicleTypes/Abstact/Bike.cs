using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    abstract class Bike : Vehicle
    {
        public enum eLicenseType
        {
            A1,
            B1,
            AA,
            BB
        }

        protected eLicenseType m_LicenseType;
        protected int m_EngineVolumeCC;
        protected const float k_MaxAllowedWheelPSI = 28;
        protected const byte k_NumOfWheels = 2;

        public Bike(Engine i_EngineType) : base(i_EngineType)
        {

        }
    }
}
