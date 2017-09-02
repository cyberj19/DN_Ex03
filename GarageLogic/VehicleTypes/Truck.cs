namespace GarageLogic
{
    class Truck : Vehicle
    {
        //todo: Readonly...
        protected bool m_IsCarryingDangerousMaterials;
        protected float m_MaxCarryingWeightAllowed;
        protected const float k_MaxAllowedWheelPSI = 34;  //todo: Not good!
        protected const byte k_NumOfWheels = 12; //todo: Not good! Same as above: Truck can have 30 wheels. it doesnt matter.
                                                //todo: This limitation is of the garage, not of the truck!
        
        public Truck(PowerSource i_EngineType, string i_ModelName, string i_RegistrationNumber,
            bool i_IsCarryingDangerousMaterials, float i_MaxCarryingWeightAllowed) //todo: "Default" ????????? what is default, why do u decide that is the default one? it's not default!
            :base(i_EngineType, i_ModelName, i_RegistrationNumber, k_NumOfWheels, new Tire(k_MaxAllowedWheelPSI,"default"))  //, bool i_IsCarryingDangerousMaterials, float i_MaxCarryingWeightAllowed //todo: Delete this comment
        {
            //todo: This initialization is bad. The truck should recv the Tire in c'tor, and not create one itself!
            //todo: Not generic
            //todo: I_EngineType bad name: i_Engine, it's not a type its not an enum etc'
        }
    }                 
}
