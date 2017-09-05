using GarageLogic;

namespace ConsoleUI.VehicleInputOutput
{
    class VehicleRecordData
    {
        private readonly string r_OwnerName;
        private readonly string r_OwnerPhoneNumber;
        private readonly Vehicle r_Vehicle;

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

        public Vehicle Vehicle
        {
            get
            {
                return r_Vehicle;
            }
        }

        public VehicleRecordData(string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_Vehicle)
        {
            r_OwnerName = i_OwnerName;
            r_OwnerPhoneNumber = i_OwnerPhoneNumber;
            r_Vehicle = i_Vehicle;
        }
    }
}
