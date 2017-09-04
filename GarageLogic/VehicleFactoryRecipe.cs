using GarageLogic.VehicleParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    class VehicleFactoryRecipe
    {
        private readonly PowerSource r_PowerSource;
        private readonly List<Tire> r_SampleTires;
        private readonly Type r_VehicleType;

        public PowerSource PowerSource
        {
            get
            {
                return r_PowerSource;
            }
        }

        public List<Tire> Tires
        {
            get
            {
                return r_SampleTires;
            }
        }

        public Type Type
        {
            get
            {
                return r_VehicleType;
            }
        }

        public VehicleFactoryRecipe(PowerSource i_PowerSource, List<Tire> i_SampleTires, Type i_VehicleType)
        {
            r_PowerSource = i_PowerSource;
            r_SampleTires = i_SampleTires;
            r_VehicleType = i_VehicleType;
        }

    }
}
