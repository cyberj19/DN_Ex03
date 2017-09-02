using System.Collections.Generic;
using GarageLogic.VehicleParts;

namespace GarageLogic
{
    class TiresFactory
    {
        public List<Tire> ProduceTires(string i_ManufacturerName, int i_TiresAmount, float i_MaxAllowedAirPressure)
        {
            List<Tire> tires = new List<Tire>(i_TiresAmount);
            for(int i=0; i< tires.Count; ++i)
            {
                tires[i] = new Tire(i_MaxAllowedAirPressure, i_ManufacturerName);
            }
            return tires;
        }
    }
}
