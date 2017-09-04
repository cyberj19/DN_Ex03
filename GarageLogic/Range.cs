using System;

namespace GarageLogic
{
    class Range<T> where T : IComparable
    {
        private readonly T r_Min;
        private readonly T r_Max;

        public T Max
        {
            get
            {
                return r_Max;
            }
        }

        public T Min
        {
            get
            {
                return r_Min;
            }
        }

        //todo: Never assigned. is this class needed


        public bool IsInRange(T i_Val)
        {
            return (i_Val.CompareTo(r_Min) >= 0) && (r_Max.CompareTo(i_Val) >= 0); 
        }

        public override string ToString()
        {
            return string.Format("Range: {0} - {1}", r_Min, r_Max);
        }
    }
}
