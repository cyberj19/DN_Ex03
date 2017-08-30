using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    abstract class Vehicle
    {
        protected string m_ModelName;
        protected string m_RegistrationNumber;
        protected float m_EnergyLeftPercent;
        protected List<Wheel> m_Wheels;
        protected Engine m_Engine;

        public Vehicle(Engine i_Engine)
        {

        }
    }
}
