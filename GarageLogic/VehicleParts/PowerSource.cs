namespace GarageLogic.VehicleParts
{
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

        public virtual float CurrentPowerLevel { get; set; }
        public virtual float PowerCapacity { get; }
    }
}
