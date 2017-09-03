using GarageLogic.Exceptions;

namespace GarageLogic
{
    //todo: This is the positive range class. Perhaps change it to template and use it instead?...
    struct LimitedRangeValue
    {
        private const float k_MinAmount = 0f;
        private readonly float r_MaxAmount;
        private float m_CurrentAmount;

        public float CurrentAmount
        {
            get
            {
                return m_CurrentAmount;
            }

            set
            {
                if ((value > r_MaxAmount) || (value < k_MinAmount))
                {
                    throw new ValueOutOfRangeException(k_MinAmount, r_MaxAmount);
                }
                m_CurrentAmount = value;
            }
        }
        public float MaxAmount
        {
            get
            {
                return r_MaxAmount;
            }
        }

        public LimitedRangeValue(float i_MaxAmount)
        {
            r_MaxAmount = i_MaxAmount;
            m_CurrentAmount = k_MinAmount;
        }

    }
}
