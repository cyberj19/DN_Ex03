using System;
using System.Collections.Generic;

namespace GarageLogic
{
    // Manages a garage
    public class GarageManager
    {
        private const int k_StatusNotFound = -1;
        List<VehicleRecord> m_VehicleRecords;

        public bool IsEmpty
        {
            get
            {
                return m_VehicleRecords.Count == 0;
            }
        }
        public GarageManager()
        {
            m_VehicleRecords = new List<VehicleRecord>();
        }

        // insert a new record to the garage
        public void InsertRecord(string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_Vehicle)
        {
            if (GetVehicleRecord(i_Vehicle.PlateNumber) != null)
            {
                throw new ArgumentException();
            }

            m_VehicleRecords.Add(new VehicleRecord(i_OwnerName, i_OwnerPhoneNumber, i_Vehicle));
        }

        // Get vehicle records. There's an option to filter out by status.
        public List<VehicleRecord> GetVehicleRecords(List<VehicleRecord.eStatus> i_FilterOutList)
        {
            List<VehicleRecord> retVehicleList = new List<VehicleRecord>();

            if (i_FilterOutList.Count == 0)
            {
                // no need to filter out, return whole list
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

        // Get a single vehicle record according to plate number
        public VehicleRecord GetVehicleRecord(string plateNumber)
        {
            VehicleRecord foundVehicle = null;
            int plateNumHashCode = plateNumber.GetHashCode();
            
            foreach (VehicleRecord vehicleRecord in m_VehicleRecords)
            {
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
