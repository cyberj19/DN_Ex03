namespace GarageLogic
{
    struct Tire
    {
        readonly string m_ManufacturerName;
        readonly float r_MaxPSI;
        float m_CurrentPSI;

        public void InflateAir(float i_AdditionPSI)
        {
            Utils.ValidateAddition(m_CurrentPSI, i_AdditionPSI, r_MaxPSI);
            m_CurrentPSI += i_AdditionPSI;
        }

        public Tire(float i_MaxPSI, string i_ManufacturerName)
        {
            r_MaxPSI = i_MaxPSI;
            m_ManufacturerName = i_ManufacturerName;
            m_CurrentPSI = 0;
        }

    }
}
