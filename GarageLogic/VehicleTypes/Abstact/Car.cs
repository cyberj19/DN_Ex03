using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    abstract class Car : Vehicle
    {
        public enum eColor
        {
            Green,
            Silver,
            White,
            Black
        }

        public enum eDoorsAmount
        {
            Two = 2,
            Three,
            Four,
            Five
        }

        protected eColor m_Color;
        protected eDoorsAmount m_DoorsAmount;
        protected const float k_MaxAllowedWheelPSI = 32;
        protected const byte k_NumOfWheels = 4;

        public Car(Engine i_EngineType) : base(i_EngineType)
        {

        }

    }
}
