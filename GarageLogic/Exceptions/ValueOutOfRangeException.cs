using System;

namespace GarageLogic.Exceptions
{
    class ValueOutOfRangeException : Exception
    {
        float m_MaxValue;
        float m_MinValue;
    }
}
