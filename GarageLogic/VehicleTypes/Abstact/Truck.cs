using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    abstract class Truck : Vehicle
    {
        protected bool m_IsCarryingDangerousMaterials;
        protected float m_MaxCarryingWeightAllowed;
        protected const float k_MaxAllowedWheelPSI = 34;
        protected const byte k_NumOfWheels = 12;
        
        public Truck(Engine i_EngineType) :base(i_EngineType)  //, bool i_IsCarryingDangerousMaterials, float i_MaxCarryingWeightAllowed
        {
            m_Wheels = new List<Wheel>(k_NumOfWheels);
            m_Wheels.ForEach(wheel => wheel = new Wheel(k_MaxAllowedWheelPSI, "default")); //todo: rethink - default is bad, using of this type of foreach with lambda is bad also.

        }
    }                 
}
