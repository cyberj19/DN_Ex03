using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    class VehicleCareOrder
    {
        enum eStatus
        {
            InRepair,
            Fixed,
            PaymentReceived
        }

        string m_OwnerName;
        string m_OwnerPhone;
        eStatus m_VehicleStatus;
        Vehicle m_Vehicle;

        public VehicleCareOrder(string i_OwnerName, string i_OwnerPhone, Vehicle i_Vehicle)
        {
            m_Vehicle = i_Vehicle;
            m_VehicleStatus = eStatus.InRepair;
            m_OwnerName = i_OwnerName;
            m_OwnerPhone = i_OwnerPhone;
        }
    }
}
