using GarageLogic.VehicleParts.PowerSources;
using System;
using System.Collections.Generic;
namespace GarageLogic
{
    public struct PowerSourceInputParam
    {
        //todo: make sure all readonly with r_
        private readonly FuelSource.eFuelType? r_FuelType;
        private readonly float r_PowerLevel;

        public FuelSource.eFuelType FuelType
        {
            get
            {
                return r_FuelType.Value;
            }
        }

        public float PowerLevel
        {
            get
            {
                return r_PowerLevel;
            }
        }

        // Is power source fuel param? //todo: not sure this is the best way to do this.. perhaps create 2 classes
        public bool IsValidFuelParam()
        {
            return r_FuelType.HasValue;
        }

        // Is electric source param
        public bool IsValidElectricParam ()
        { 
            return !r_FuelType.HasValue;
        }

        // init as electric source param
        public PowerSourceInputParam(float i_PowerLevel)
        {
            r_PowerLevel = i_PowerLevel;
            r_FuelType = null;
        }

        // init as fuel source param
        public PowerSourceInputParam(float i_PowerLevel, FuelSource.eFuelType i_FuelType)
        {
            r_PowerLevel = i_PowerLevel;
            r_FuelType = i_FuelType;
        }
    }
}

