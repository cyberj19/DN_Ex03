using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic.Vehicle_Types
{
    class ElectricBike : Bike
    {
        const float k_BattaryFullCapacityInHours = 1.6f;

        public ElectricBike() : base(new ElectricEngine())
        {

        }

    }
}
