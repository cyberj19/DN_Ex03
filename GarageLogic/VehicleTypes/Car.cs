using System.Collections.Generic;
using GarageLogic.VehicleParts;
using System;

namespace GarageLogic.VehicleTypes
{
    public class Car : Vehicle
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
            Two,
            Three,
            Four,
            Five
        }

        private eColor m_Color;
        private eDoorsAmount m_DoorsAmount;


        public eColor Color
        {
            get
            {
                return m_Color;
            }

            set
            {
                m_Color = value;
            }
        }

        public eDoorsAmount DoorsAmount
        {
            get
            {
                return m_DoorsAmount;
            }

            set
            {
                m_DoorsAmount = value;
            }
        }
    }
}
