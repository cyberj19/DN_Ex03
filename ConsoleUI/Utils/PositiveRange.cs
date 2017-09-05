namespace ConsoleUI.Utils
{
    class PositiveRange
    {
        private readonly uint r_Min;
        private readonly uint r_Max;

        public uint Min
        {
            get
            {
                return r_Min;
            }
        }

        public uint Max
        {
            get
            {
                return r_Max;
            }
        }

        public PositiveRange(uint i_Min, uint i_Max)
        {
            r_Min = i_Min;
            r_Max = i_Max;
        }

        public bool IsInRange(uint i_Num)
        {
            return (r_Min <= i_Num) && (i_Num <= r_Max);
        }
    }
}
