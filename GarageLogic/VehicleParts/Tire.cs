namespace GarageLogic.VehicleParts
{

    //todo: rethink if this should be struct because on foreach loop it doesnt change (not by ref.........)
    //todo: meanwhile change to class. need to make sure it doesnt ruin any other things. consider also turning to classes other
    //todo: objects. And make sure foreach loop are working ok with all our objects
    public class Tire
    {
        private readonly string r_ManufacturerName;
        LimitedRangeValue m_AirPressure;

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

        // Fill up the tire
        public void InflateAir(float i_AdditionPSI)
        {
            m_AirPressure.CurrentAmount += i_AdditionPSI;
        }

        public Tire(float i_MaxPSI, string i_ManufacturerName)
        {
            r_ManufacturerName = i_ManufacturerName;
            m_AirPressure = new LimitedRangeValue(i_MaxPSI);
        }
    }
}
