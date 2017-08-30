namespace GarageLogic
{
    class Car : Vehicle
    {
        public enum eColor
        {
            Green,
            Silver,
            White,
            Black
        }

        public enum eDoorsAmount
        {
            Two = 2,
            Three,
            Four,
            Five
        }

        protected eColor m_Color;
        protected eDoorsAmount m_DoorsAmount;
        protected const float k_MaxAllowedWheelPSI = 32;
        protected const byte k_NumOfWheels = 4;

        public Car(PowerSource i_EngineType, string i_ModelName, string i_RegistrationNumber,
            eDoorsAmount i_DoorsAmount, eColor i_Color) 
            : base(i_EngineType, i_ModelName, i_RegistrationNumber, k_NumOfWheels, new Tire(k_MaxAllowedWheelPSI, "default"))
        {

        }

    }
}
