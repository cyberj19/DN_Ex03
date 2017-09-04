namespace GarageLogic
{
    // Represents a garage record
    public class VehicleRecord
    {
        public enum eStatus
        {
            InRepair,
            Fixed,
            PaymentReceived
        }

        private readonly string r_OwnerName;
        private readonly string r_OwnerPhoneNumber;
        private readonly Vehicle r_Vehicle;
        private eStatus m_VehicleStatus;

        public string OwnerName
        {
            get
            {
                return r_OwnerName;
            }
        }

        public string OwnerPhoneNumber
        {
            get
            {
                return r_OwnerPhoneNumber;
            }
        }

        public eStatus Status
        {
            get
            {
                return m_VehicleStatus;
            }

            set
            {
                m_VehicleStatus = value;
            }
        }

        public Vehicle Vehicle
        {
            get
            {
                return r_Vehicle;
            }
        }

        public VehicleRecord(string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_Vehicle)
        {
            r_Vehicle = i_Vehicle;
            m_VehicleStatus = eStatus.InRepair;
            r_OwnerName = i_OwnerName;
            r_OwnerPhoneNumber = i_OwnerPhoneNumber;
        }
    }
}
