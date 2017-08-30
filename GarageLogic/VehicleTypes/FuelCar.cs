using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic.Vehicle_Types
{
    class FuelCar : Car
    {
        const FuelEngine.eFuelType k_FuelType = FuelEngine.eFuelType.Octan98;
        const float k_FuelFullTankCapacityInLiters = 50;


        public FuelCar() : base(new FuelEngine())
        {

        }
    }
}
