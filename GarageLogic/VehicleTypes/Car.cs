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

        //todo: Truck.cs review same rules here
        protected eColor m_Color;
        protected eDoorsAmount m_DoorsAmount;
        protected const float k_MaxAllowedWheelPSI = 32;
        protected const byte k_NumOfWheels = 4;

        //todo: Why number of doors is in an enum? Tommorow i would want a car with 7 doors. Why isnt this possible?
        
        public Car(PowerSource i_EngineType, string i_ModelName, string i_RegistrationNumber,
            eDoorsAmount i_DoorsAmount, eColor i_Color) 
            : base(i_EngineType, i_ModelName, i_RegistrationNumber, k_NumOfWheels, new Tire(k_MaxAllowedWheelPSI, "default"))
        {

        }

    }
}
