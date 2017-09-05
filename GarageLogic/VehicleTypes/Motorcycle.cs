using GarageLogic.Exceptions;

namespace GarageLogic.VehicleTypes
{
    class Motorcycle : Vehicle
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
                verifyValidEngineVolume(value);
                m_EngineVolumeCC = value;
            }
        }

        private void verifyValidEngineVolume(int i_Val)
        {
            if (i_Val < 0)
            {
                throw new ValueOutOfRangeException(0, int.MaxValue);
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
