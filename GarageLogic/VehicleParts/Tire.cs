namespace GarageLogic.VehicleParts
{
    // Represents a tire
    public class Tire
    {
        private readonly string r_ManufacturerName;
        LimitedFloatValue m_AirPressure;

        public string ManufacturerName
        {
            get
            {
                return r_ManufacturerName;
            }
        }

        public float MaxPSI
        {
            get
            {
                return m_AirPressure.Max;
            }
        }

        public float CurrentPSI
        {
            get
            {
                return m_AirPressure.Value;
            }
        }

        // Fill up the tire
        public void InflateAir(float i_AdditionPSI)
        {
            m_AirPressure.Value += i_AdditionPSI;
        }

        public Tire(float i_MaxPSI, string i_ManufacturerName)
        {
            r_ManufacturerName = i_ManufacturerName;
            m_AirPressure = new LimitedFloatValue(i_MaxPSI);
        }
    }
}
