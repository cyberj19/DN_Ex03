namespace GarageLogic.VehicleParts
{
    // Represents vehicle power source (Engine + Power Capacitor)
    public abstract class PowerSource
    {
        private const float k_Threshold = 0.999f;
        private const float k_fullPercent = 1.0f;

        public float EnergyPercent
        {
            get
            {
                float percent = CurrentPowerLevel / PowerCapacity;

                if (percent > k_Threshold)
                {
                    percent = k_fullPercent;
                }

                return percent;
            }
        }

        public PowerSource duplicate()
        {
            return duplicate(CurrentPowerLevel);
        }

        public abstract PowerSource duplicate(float i_InitialCapacity);

        public abstract float CurrentPowerLevel
        {
            get;
        }

        public abstract float PowerCapacity
        {
            get;
        }

        public abstract string Units
        {
            get;
        }
    }
}
