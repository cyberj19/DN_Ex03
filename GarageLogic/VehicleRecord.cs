namespace GarageLogic
{
    public class VehicleRecord
    {
        enum eStatus
        {
            InRepair,
            Fixed,
            PaymentReceived
        }

        //todo: Readonly?
        string m_OwnerName;
        string m_OwnerPhoneNumber;
        eStatus m_VehicleStatus;
        Vehicle m_Vehicle;

        public string OwnerName
        {
            get
            {
                return m_OwnerName;
            }
        }

        public string OwnerPhoneNumber
        {
            get
            {
                return m_OwnerPhoneNumber;
            }
        }

        eStatus Status
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
                return m_Vehicle;
            }
        }

        public VehicleRecord(string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_Vehicle)
        {
            m_Vehicle = i_Vehicle;
            m_VehicleStatus = eStatus.InRepair;
            m_OwnerName = i_OwnerName;
            m_OwnerPhoneNumber = i_OwnerPhoneNumber;
        }
    }
}
