using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic.Vehicle_Types
{
    class ElectricCar : Car
    {
        const float k_BattaryFullCapacityInHours = 2.8f;

        public ElectricCar() : base(new ElectricEngine())
        {

        }

    }
}
