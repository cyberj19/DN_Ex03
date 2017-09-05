using GarageLogic.Exceptions;

namespace GarageLogic.VehicleTypes
{
    class Truck : Vehicle
    {
        private bool m_IsCarryingDangerousMaterials;
        private LimitedFloatValue m_MaxCarryingWeightAllowedInKg;

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
                return m_MaxCarryingWeightAllowedInKg.Value;
            }

            set
            {
                m_MaxCarryingWeightAllowedInKg.Value = value;
            }
        }

        public Truck()
        {
            m_MaxCarryingWeightAllowedInKg = new LimitedFloatValue();
        }
    }
}
