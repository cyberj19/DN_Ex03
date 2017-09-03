using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic.VehicleTypes
{
    public class MotorcycleInfo
    {
        //todo: these enums declerations should be here or in motorcycle?
        private readonly Motorcycle.eLicenseType r_LicenseType;
        private readonly int r_EngineVolumeCC;

        public static MotorcycleInfo Default
        {
            get
            {
                return new MotorcycleInfo(Motorcycle.eLicenseType.A1, 0);
            }
        }

        public Motorcycle.eLicenseType LicenseType
        {
            get
            {
                return r_LicenseType;
            }
        }

        public int EngineVolumeCC
        {
            get
            {
                return r_EngineVolumeCC;
            }
        }

        public MotorcycleInfo(Motorcycle.eLicenseType i_LicenseType, int i_EngineVolumeCC)
        {
            r_LicenseType = i_LicenseType;
            r_EngineVolumeCC = i_EngineVolumeCC;
        }
    }
}
