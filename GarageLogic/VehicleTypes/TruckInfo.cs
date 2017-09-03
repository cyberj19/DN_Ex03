using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic.VehicleTypes
{
    public class TruckInfo
    {
        private readonly bool r_IsCarryingDangerousMaterials;
        private readonly float r_MaxCarryingWeightAllowed;

        public static TruckInfo Default
        {
            get
            {
                return new TruckInfo(false, 0);
            }
        }
        public bool IsCarryingDangerousMaterials
        {
            get
            {
                return r_IsCarryingDangerousMaterials;
            }
        }

        public float MaxCarryingWeightAllowed
        {
            get
            {
                return r_MaxCarryingWeightAllowed;
            }
        }

        public TruckInfo(bool i_IsCarryingDangerousMaterials, float i_MaxCarryingWeightAllowed)
        {
            r_IsCarryingDangerousMaterials = i_IsCarryingDangerousMaterials;
            r_MaxCarryingWeightAllowed = i_MaxCarryingWeightAllowed;
        }


    }
}
