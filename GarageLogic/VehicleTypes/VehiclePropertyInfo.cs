using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic.VehicleTypes
{
    public class VehiclePropertyInfo
    {
        private readonly string r_Name;
        private readonly Type r_Type;

        public string Name
        {
            get
            {
                return r_Name;
            }
        }

        public Type Type
        {
            get
            {
                return r_Type;
            }
        }
        
        //todo: 2 ct'ors..
        public VehiclePropertyInfo(string i_Name, Type i_Type)
        {
            r_Name = i_Name;
            r_Type = i_Type;
        }
    }
}
