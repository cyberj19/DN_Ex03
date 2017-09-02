namespace GarageLogic
{
    class Motorcycle : Vehicle
    {
        public enum eLicenseType
        {
            A1,
            B1,
            AA,
            BB
        }
        //todo: Same review notes as truck.cs
        protected eLicenseType m_LicenseType;
        protected int m_EngineVolumeCC;
        protected const float k_MaxAllowedWheelPSI = 28;
        protected const byte k_NumOfWheels = 2;

        public Motorcycle(PowerSource i_EngineType, string i_ModelName, string i_RegistrationNumber,
            eLicenseType i_LisenceType, int i_EngineVolumeCC) 
            : base(i_EngineType, i_ModelName, i_RegistrationNumber, k_NumOfWheels, new Tire(k_MaxAllowedWheelPSI,"default"))
        {

        }
    }
}
