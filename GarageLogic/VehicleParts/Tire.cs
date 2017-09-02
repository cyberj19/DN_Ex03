namespace GarageLogic.VehicleParts
{
    public struct Tire
    {
        readonly string r_ManufacturerName;
        LimitedRangeValue m_AirPressure;

        public float MaxPSI
        {
            get
            {
                return m_AirPressure.MaxAmount;
            }
        }

        public float CurrentPSI
        {
            get
            {
                return m_AirPressure.CurrentAmount;
            }
        }

        public void InflateAir(float i_AdditionPSI)
        {
            m_AirPressure.CurrentAmount = i_AdditionPSI;
        }

        public Tire(float i_MaxPSI, string i_ManufacturerName)
        {
            r_ManufacturerName = i_ManufacturerName;
            m_AirPressure = new LimitedRangeValue(i_MaxPSI);
        }

    }
}
