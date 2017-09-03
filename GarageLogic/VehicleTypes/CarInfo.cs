using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic.VehicleTypes
{
    public class CarInfo
    {
        //todo: these enums should be here or in Car?
        private readonly Car.eColor r_Color;
        private readonly Car.eDoorsAmount r_DoorsAmount;

        public static CarInfo Default
        {
            get
            {
                return new CarInfo(Car.eColor.White, Car.eDoorsAmount.Four);
            }
        }
        public Car.eColor Color
        {
            get
            {
                return r_Color;
            }
        }

        public Car.eDoorsAmount DoorsAmount
        {
            get
            {
                return r_DoorsAmount;
            }
        }

        public CarInfo(Car.eColor i_Color, Car.eDoorsAmount i_DoorsAmount)
        {
            r_Color = i_Color;
            r_DoorsAmount = i_DoorsAmount;
        }
    }
}
