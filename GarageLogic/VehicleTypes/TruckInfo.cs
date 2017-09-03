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
        private readonly float r_MaxCarryingWeightAllowedKg;

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

        public float MaxCarryingWeightAllowedKg
        {
            get
            {
                return r_MaxCarryingWeightAllowedKg;
            }
        }

        public TruckInfo(bool i_IsCarryingDangerousMaterials, float i_MaxCarryingWeightAllowed)
        {
            r_IsCarryingDangerousMaterials = i_IsCarryingDangerousMaterials;
            r_MaxCarryingWeightAllowedKg = i_MaxCarryingWeightAllowed;
        }


    }
}
