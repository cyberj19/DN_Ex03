using GarageLogic.Exceptions;

namespace GarageLogic
{
    static class Utils
    {
        public static void ValidateAddition(float i_CurrentAmount, float i_AdditionAmount, float i_MaxAmount)
        {
            if (((i_CurrentAmount + i_AdditionAmount) > i_MaxAmount) || i_AdditionAmount < 0)
            {
                throw new ValueOutOfRangeException();
            }
        }
    }
}
