using System.Collections.Generic;

namespace GarageLogic
{
    class Truck : Vehicle
    {
         readonly bool r_IsCarryingDangerousMaterials;
         readonly float r_MaxCarryingWeightAllowed;
         const float k_MaxAllowedWheelPSI = 34;  //todo: Not good
         const byte k_NumOfWheels = 12; //todo: Not good! Same as above: Truck can have 30 wheels. it doesnt matter.
                                                //todo: This limitation is of the garage, not of the truck!
        
        public Truck(PowerSource i_PowerSource, VehicleRegistrationInfo i_VehicleInfo, List<Tire> i_Tires,
            bool i_IsCarryingDangerousMaterials, float i_MaxCarryingWeightAllowed) 
            :base(i_PowerSource, i_VehicleInfo, i_Tires)
        { 
            r_IsCarryingDangerousMaterials= i_IsCarryingDangerousMaterials;
            r_MaxCarryingWeightAllowed = i_MaxCarryingWeightAllowed;
        }
    }                 
}
