using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public struct VehicleRegistrationInfo
    {
        readonly string r_ModelName;
        readonly string r_PlateNumber;

        public string ModelName
        {
            get
            {
                return r_ModelName;
            }
        }

        public string PlateNumber
        {
            get
            {
                return r_PlateNumber;
            }
        }

        public VehicleRegistrationInfo(string i_ModelName, string i_PlateNumber)
        {
            r_ModelName = i_ModelName;
            r_PlateNumber = i_PlateNumber;
        }
    }
}
