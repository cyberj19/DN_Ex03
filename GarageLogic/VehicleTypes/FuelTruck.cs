using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic.Vehicle_Types
{

    class FuelTruck : Truck
    {
        const FuelEngine.eFuelType k_FuelType = FuelEngine.eFuelType.Soler;
        const float k_FuelFullTankCapacityInLiters = 130;

        public FuelTruck() : base(new FuelEngine())
        {

        }

    }
}
