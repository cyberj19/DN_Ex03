using System.Collections.Generic;
using GarageLogic.VehicleParts;

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
            Two = 2,
            Three,
            Four,
            Five
        }
        //todo: need to access these..

        //todo: Truck.cs review same rules here
        //        protected readonly eColor r_Color = eColor.White; //default value
        //        protected readonly eDoorsAmount r_DoorsAmount = eDoorsAmount.Four;

        private readonly CarInfo r_CarInfo;
       
        public eColor Color
        {
            get
            {
                return r_CarInfo.Color;
            }
        }

        public eDoorsAmount DoorsAmount
        {
            get
            {
                return r_CarInfo.DoorsAmount;
            }
        }

        //todo: Why number of doors is in an enum? Tommorow i would want a car with 7 doors. Why isnt this possible?
        
        public Car(PowerSource i_PowerSource, VehicleRegistrationInfo i_VehicleInfo, List<Tire> i_Tires, CarInfo i_CarInfo) 
            : base(i_PowerSource, i_VehicleInfo, i_Tires)
        {
            r_CarInfo = i_CarInfo;
        }

        public Car(PowerSource i_PowerSource, List<Tire> i_Tires)
            : base(i_PowerSource, VehicleRegistrationInfo.Default, i_Tires)
        {
            r_CarInfo = CarInfo.Default;
        }

    }
}
