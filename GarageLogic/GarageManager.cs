using System.Collections.Generic;

namespace GarageLogic
{
    public class GarageManager
    {
        List<VehicleRecord> m_VehicleRecord;

        public static void ShowVehiclesArrangedByRegistrationNumber()
        {

        }

        public Vehicle GetVehicle(string plateNumber) //todo: this is only the basic = MUST!!! refactor this func!!
        {
            Vehicle foundVehicle = null;
            int plateNumHashCode = plateNumber.GetHashCode();

            foreach (VehicleRecord vr in m_VehicleRecord)
            {
                if(vr.Vehicle.GetHashCode() == plateNumHashCode)
                {
                    foundVehicle = vr.Vehicle;
                }
            }

            return foundVehicle;
        }

    }
}
