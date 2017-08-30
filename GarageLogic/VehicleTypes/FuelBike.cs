using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic.Vehicle_Types
{
    class FuelBike : Bike
    {
        const FuelEngine.eFuelType k_FuelType = FuelEngine.eFuelType.Octan95;
        const float k_FuelFullTankCapacityInLiters = 5.5f;

        public FuelBike() : base(new FuelEngine())
        {

        }

    }
}
