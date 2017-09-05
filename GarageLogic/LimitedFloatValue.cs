using GarageLogic.Exceptions;

namespace GarageLogic
{
    class LimitedFloatValue
    {
        private const float k_Min = 0f;
        private readonly float r_Max;
        private float m_Data;

        public float Value
        {
            get
            {
                return m_Data;
            }

            set
            {
                if ((value > r_Max) || (value < k_Min))
                {
                    throw new ValueOutOfRangeException(k_Min, r_Max);
                }

                m_Data = value;
            }
        }

        public float Max
        {
            get
            {
                return r_Max;
            }
        }

        public LimitedFloatValue(float i_Max)
        {
            r_Max = i_Max;
            m_Data = k_Min;
        }

        public LimitedFloatValue()
        {
            r_Max = float.MaxValue;
            m_Data = k_Min;
        }
    }
}
