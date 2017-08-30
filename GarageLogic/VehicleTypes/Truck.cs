namespace GarageLogic
{
    class Truck : Vehicle
    {
        protected bool m_IsCarryingDangerousMaterials;
        protected float m_MaxCarryingWeightAllowed;
        protected const float k_MaxAllowedWheelPSI = 34;
        protected const byte k_NumOfWheels = 12;
        
        public Truck(PowerSource i_EngineType, string i_ModelName, string i_RegistrationNumber,
            bool i_IsCarryingDangerousMaterials, float i_MaxCarryingWeightAllowed)
            :base(i_EngineType, i_ModelName, i_RegistrationNumber, k_NumOfWheels, new Tire(k_MaxAllowedWheelPSI,"default"))  //, bool i_IsCarryingDangerousMaterials, float i_MaxCarryingWeightAllowed
        {
            
        }
    }                 
}
