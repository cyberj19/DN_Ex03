using System.Collections.Generic;

namespace GarageLogic
{
    public class GarageManager
    {
        private const int k_StatusNotFound = -1;
        List<VehicleRecord> m_VehicleRecords;

        public static void ShowVehiclesArrangedByRegistrationNumber()
        {

        }

        public List<VehicleRecord> GetVehicleRecords(List<VehicleRecord.eStatus> i_FilterOutList)
        {
            List<VehicleRecord> retVehicleList = new List<VehicleRecord>();

            if (i_FilterOutList.Count == 0)
            {
                retVehicleList = m_VehicleRecords;
            }
            else
            {
                foreach (VehicleRecord vehicleRecord in m_VehicleRecords)
                {
                    if (k_StatusNotFound == i_FilterOutList.IndexOf(vehicleRecord.Status))
                    {
                        retVehicleList.Add(vehicleRecord);
                    }
                }
            }
                return retVehicleList;
        }

        public VehicleRecord GetVehicleRecord(string plateNumber) //todo: this is only the basic = MUST!!! refactor this func!!
        {
            VehicleRecord foundVehicle = null;
            int plateNumHashCode = plateNumber.GetHashCode();

            foreach (VehicleRecord vehicleRecord in m_VehicleRecords)
            {
                //todo: isnt .find implemented for this?
                if(vehicleRecord.Vehicle.GetHashCode() == plateNumHashCode)
                {
                    foundVehicle = vehicleRecord;
                    break;
                }
            }

            return foundVehicle;
        }

    }
}
