using System.Collections.Generic;
using GarageLogic.VehicleParts;
using System;

namespace GarageLogic.VehicleTypes
{
    public class Truck : Vehicle
    {
        //todo: Access this, and give releveant inormation to VehiclePrintUtils to all these 3 classes
        private const string k_IsCarryingDangerousMaterialsPropertyName = "Is Carrying Dangerous Materials";
        private const string k_MaxCarryWeightAllowedKgPropertyName = "Max Carry Weight Allowed In Kg";
        private static readonly Dictionary<string, Type> sr_RequiredProperties = new Dictionary<string, Type>()
        {
            { k_IsCarryingDangerousMaterialsPropertyName, typeof(bool) },
            { k_MaxCarryWeightAllowedKgPropertyName, typeof(float) }
        };


        public bool IsCarryingDangerousMaterials
        {
            get
            {
                return GetProp<bool>(k_IsCarryingDangerousMaterialsPropertyName);
            }
        }

        public float MaxCarryingWeightAllowedKg
        {
            get
            {
                return GetProp<float>(k_MaxCarryWeightAllowedKgPropertyName);
            }
        }

        public Truck() : base(sr_RequiredProperties)
        {
        }

        protected override object processPopluateRequest(string i_PropertyName, object i_Obj)
        {
            // no special processing is needed by this class, and no range check.
            return i_Obj;
        }
    }
}
