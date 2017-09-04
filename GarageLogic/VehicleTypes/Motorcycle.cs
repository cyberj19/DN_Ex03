using System.Collections.Generic;
using GarageLogic.VehicleParts;
using System;

namespace GarageLogic.VehicleTypes
{
    public class Motorcycle : Vehicle
    {
        public enum eLicenseType
        {
            A1,
            B1,
            AA,
            BB
        }

        private int m_EngineVolumeCC;
        private eLicenseType m_LicenseType;

        public int EngineVolumeCC
        {
            get
            {
                return m_EngineVolumeCC;
            }

            set
            {
                m_EngineVolumeCC = value;
            }
        }

        public eLicenseType LicenseType
        {
            get
            {
                return m_LicenseType;
            }
            set
            {
                m_LicenseType = value;
            }
        }
    }
}
