using System;

namespace GarageLogic.Exceptions
{
    public class ValueOutOfRangeException : Exception
    {
        private const string k_ErrFormartStr = "Out of range. Range: {0} - {1}";
        private readonly float r_MinValue;
        private readonly float r_MaxValue;

        public float Min
        {
            get
            {
                return r_MinValue;
            }
        }

        public float Max
        {
            get
            {
                return r_MaxValue;
            }
        }

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue) : base(string.Format(k_ErrFormartStr, i_MinValue, i_MaxValue))
        {
            r_MinValue = i_MinValue;
            r_MaxValue = i_MaxValue;
        }
    }
}
