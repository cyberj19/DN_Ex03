namespace GarageLogic.VehicleParts
{
    // Represents vehicle power source (Engine + Power Capacitor)
    public abstract class PowerSource
    {
        public float EnergyPercent
        {
            get
            {
                float percent = CurrentPowerLevel / PowerCapacity;

                if (percent > 0.999f)
                {
                    percent = 1.0f;
                }

                return percent;
            }
        }

        public PowerSource duplicate()
        {
            return duplicate(CurrentPowerLevel);
        }

        public abstract PowerSource duplicate(float i_InitialCapacity);

        public virtual float CurrentPowerLevel {
            get;
            set;
        }

        public virtual float PowerCapacity
        {
            get;
        }
    }
}
