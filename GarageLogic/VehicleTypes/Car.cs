using System.Collections.Generic;

namespace GarageLogic
{
    class Car : Vehicle
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

        //todo: Truck.cs review same rules here
        protected readonly eColor r_Color;
        protected readonly eDoorsAmount r_DoorsAmount;
       
        //todo: Why number of doors is in an enum? Tommorow i would want a car with 7 doors. Why isnt this possible?
        
        public Car(PowerSource i_PowerSource, VehicleRegistrationInfo i_VehicleInfo, List<Tire> i_Tires,
            eDoorsAmount i_DoorsAmount, eColor i_Color) 
            : base(i_PowerSource, i_VehicleInfo, i_Tires)
        {
            r_Color = i_Color;
            r_DoorsAmount = i_DoorsAmount;
        }

    }
}
