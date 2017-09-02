namespace GarageLogic
{
    struct Tire
    {
        readonly string m_ManufacturerName; //todo: m --> r
        readonly float r_MaxPSI;
        float m_CurrentPSI;

        public void InflateAir(float i_AdditionPSI)
        {
            //todo: We talked about this, move this to a class
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
