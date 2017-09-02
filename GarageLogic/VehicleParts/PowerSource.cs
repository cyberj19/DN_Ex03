﻿namespace GarageLogic
{
    abstract class PowerSource
    {
        public float EnergyPercent
        {
            get
            {
                return CurrentPowerLevel / PowerCapacity;
            }
        }

        public virtual float CurrentPowerLevel { get; set; }
        public virtual float PowerCapacity { get; }
    }
}
