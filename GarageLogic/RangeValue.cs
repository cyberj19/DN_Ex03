using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    class RangeValue<T> where T : IComparable
    {
        private readonly Range<T> r_Range;
        private T m_Value;

        public T Value
        {
            set
            {
                setValue(value);
            }

            get
            {
                return m_Value;
            }
        }

        public RangeValue(T i_Value)
        {
            setValue(i_Value);
        }

        private void setValue(T i_Value)
        {
            if (!r_Range.IsInRange(i_Value))
            {
                throw new ArgumentException();
            }

            m_Value = i_Value;
        }
    }
}
