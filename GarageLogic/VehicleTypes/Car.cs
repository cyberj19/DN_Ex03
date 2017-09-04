using System.Collections.Generic;
using GarageLogic.VehicleParts;
using System;

namespace GarageLogic.VehicleTypes
{
    public class Car : Vehicle
    {
        public enum eColor
        {
            Green,
            Silver,
            White,
            Black
        }

        public enum eDoorsAmount
        {
            Two,
            Three,
            Four,
            Five
        }

        private const string k_ColorPropertyName = "Color";
        private const string k_DoorsAmountPropertyName = "Doors Amount";
        private static readonly Dictionary<string, Type> sr_RequiredProperties = new Dictionary<string, Type>()
        {
            { k_ColorPropertyName, typeof(eColor) },
            { k_DoorsAmountPropertyName, typeof(eDoorsAmount) }
        };

        public eColor Color
        {
            get
            {
                return GetProp<eColor>(k_ColorPropertyName);
            }
        }

        public eDoorsAmount DoorsAmount
        {
            get
            {
                return GetProp<eDoorsAmount>(k_DoorsAmountPropertyName);
            }
        }
        
        public Car() : base(sr_RequiredProperties)
        {
        }

        protected override object processPopluateRequest(string i_PropertyName, object i_Obj)
        {
            // no special processing is needed by this class, and no range check.
            return i_Obj;
        }
    }
}
