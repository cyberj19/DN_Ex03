using GarageLogic.VehicleParts.PowerSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        public bool IsValidFuelParam()
        {
            return r_FuelType.HasValue;
        }

        public bool IsValidElectricParam ()
        { 
            return !r_FuelType.HasValue;
        }

        public PowerSourceInputParam(float i_PowerLevel)
        {
            r_PowerLevel = i_PowerLevel;
            r_FuelType = null;
        }

        public PowerSourceInputParam(float i_PowerLevel, FuelSource.eFuelType i_FuelType)
        {
            r_PowerLevel = i_PowerLevel;
            r_FuelType = i_FuelType;
        }
    }
}

