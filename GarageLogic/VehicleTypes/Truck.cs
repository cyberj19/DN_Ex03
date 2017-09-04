using System.Collections.Generic;
using GarageLogic.VehicleParts;
using System;

namespace GarageLogic.VehicleTypes
{
    public class Truck : Vehicle
    {

        private bool m_IsCarryingDangerousMaterials;
        private float m_MaxCarryingWeightAllowedInKg;

        public bool IsCarryingDangerousMaterials
        {
            get
            {
                return m_IsCarryingDangerousMaterials;
            }

            set
            {
                m_IsCarryingDangerousMaterials = value;
            }
        }

        public float MaxCarryingWeightAllowedKg
        {
            get
            {
                return m_MaxCarryingWeightAllowedInKg;
            }

            set
            {
                m_MaxCarryingWeightAllowedInKg = value;
            }
        }
    }
}
