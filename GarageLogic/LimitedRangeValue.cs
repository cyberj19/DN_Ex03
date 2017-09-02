using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GarageLogic.Exceptions;

namespace GarageLogic
{
    struct LimitedRangeValue //todo:  very bad name. Yehonatan, maybe you can think of a better one?
    {
        readonly float r_MaxAmount;
        const float k_MinAmount = 0f;
        float m_CurrentAmount;
        readonly T r_Type;

        public float CurrentAmount
        {
            get
            {
                return m_CurrentAmount;
            }

            set
            {
                if (((m_CurrentAmount + value) > r_MaxAmount) || value < k_MinAmount)
                {
                    throw new ValueOutOfRangeException();
                }
                m_CurrentAmount += value;
            }
        }
        public float MaxAmount
        {
            get
            {
                return r_MaxAmount;
            }
        }

        public LimitedRangeValue(float i_MaxAmount, T i_Type)
        {
            r_MaxAmount = i_MaxAmount;
            m_CurrentAmount = k_MinAmount;
            r_Type = i_Type;
        }

    }
}
