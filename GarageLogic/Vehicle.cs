using System.Collections.Generic;

namespace GarageLogic
{
    abstract class Vehicle
    {
        //todo: These cannot be changed! ReadOnly
        protected string m_ModelName;
        protected string m_RegistrationNumber;
        protected float m_EnergyLeftPercent = 0;
        protected List<Tire> m_Wheels;
        protected PowerSource m_PowerSourceCapacitor;

        public Vehicle(PowerSource i_PowerSource, string i_ModelName,
            string i_RegistrationNumber, int i_NumberOfWheels, Tire i_TireType)
        {
            m_Wheels = new List<Tire>(i_NumberOfWheels);
            //todo: Please use formal foreach.. this is unreadable + unlearened
            m_Wheels.ForEach(wheel => wheel = i_TireType);
        }
    }
}
