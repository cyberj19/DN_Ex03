using System.Collections.Generic;
using GarageLogic.VehicleParts;
using System;

namespace GarageLogic.VehicleTypes
{
    public class Motorcycle : Vehicle
    {
        public enum eLicenseType
        {
            A1,
            B1,
            AA,
            BB
        }

        //todo: DID NOT PRINT THIS INFO! go back to VehiclePrintUTils. check for other things aswell
        //        private readonly Motorcycle.eLicenseType r_LicenseType;
        //        private readonly int r_EngineVolumeCC;
        private const string k_LicenseTypePropertyName = "License Type";
        private const string k_EngineVolumeCCPropertyName = "Engine Volume CC";
        private static readonly Dictionary<string, Type> sr_RequiredProperties = new Dictionary<string, Type>()
        {
            { k_LicenseTypePropertyName, typeof(eLicenseType) },
            { k_EngineVolumeCCPropertyName, typeof(int) }
        };

        public int EngineVolumeCC
        {
            get
            {
                return GetProp<int>(k_EngineVolumeCCPropertyName);
            }
        }

        public eLicenseType LicenseType
        {
            get
            {
                return GetProp<eLicenseType>(k_LicenseTypePropertyName);
            }
        }

        public Motorcycle() : base(sr_RequiredProperties)
        {
        }

        protected override object processPopluateRequest(string i_PropertyName, object i_Obj)
        {
            // no special processing is needed by this class, and no range check.
            return i_Obj;
        }
    }
}
