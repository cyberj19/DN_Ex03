using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic.VehicleParts
{
    public class TiresInfo
    {
        private readonly string[] r_TiresManufacturerName;
        private readonly float[] r_TiresInitialAirValue;

        public string[] TiresManufacturerNameArray
        {
            get
            {
                return r_TiresManufacturerName;
            }
        }

        public float[] TiresInitialAirValue
        {
            get
            {
                return r_TiresInitialAirValue;
            }
        }

        public TiresInfo(string[] i_TiresManufacturerName, float[] i_TiresInitialAirValue)
        {
            r_TiresInitialAirValue = i_TiresInitialAirValue;
            r_TiresManufacturerName = i_TiresManufacturerName;
        }
    }
}
