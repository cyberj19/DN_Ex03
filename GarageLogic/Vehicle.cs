using System.Collections.Generic;
using GarageLogic.VehicleParts;
using System;
using GarageLogic.VehicleTypes;

namespace GarageLogic
{
    public abstract class Vehicle
    {
        private readonly Dictionary<string, Type> r_RequiredObjectProperties;
        private VehicleRegistrationInfo m_Info;
        private List<Tire> m_Tires; //todo: changed to read only make sure can still change inner itemss
        private PowerSource m_PowerSource;
        private Dictionary<string, object> m_Properties = new Dictionary<string, object>();


        public List<Tire> Tires
        {
            get
            {
                return m_Tires;
            }
        }

        public PowerSource PowerSource
        {
            get
            {
                return m_PowerSource;
            }
        }

        public float EnergyLeftPercentage
        {
            get
            {
                return m_PowerSource.EnergyPercent;
            }
        }

        public string ModelName
        {
            get
            {
                return m_Info.ModelName;
            }
        }

        public string PlateNumber
        {
            get
            {
                return m_Info.PlateNumber;
            }
        }

        protected Type getObjPropertiesInfo(string i_PropName)
        {
            Type type;

            if (!r_RequiredObjectProperties.TryGetValue(i_PropName, out type))
            {
                throw new MemberAccessException(); //todo: exception type
            }

            return type;
        }

        protected T GetProp<T>(string i_PropName)
        {
            object retObj = null;

            m_Properties.TryGetValue(i_PropName, out retObj);

            return (T)retObj; //todo: will throw an exception..
        }


        public Vehicle(Dictionary<string, Type> i_RequiredObjProperties)
        {
            r_RequiredObjectProperties = i_RequiredObjProperties;
        }

        public void InitVehicle(PowerSource i_PowerSource, VehicleRegistrationInfo i_VehicleInfo, List<Tire> i_Tires)
        {
            m_Info = i_VehicleInfo;
            m_PowerSource = i_PowerSource;
            m_Tires = i_Tires;
        }

        public override int GetHashCode()
        {
            return PlateNumber.GetHashCode();
        }

//        protected abstract Dictionary<string, Type> getCurrentPropertiesInfo();
        protected abstract object processPopluateRequest(string i_PropertyName, object i_Obj);

        public void populate(string i_PropertyName, object i_Obj)
        {
            if (m_Properties.ContainsKey(i_PropertyName))
            {
                throw new Exception(); //todo: other type
            }

            //todo: exception type..
            if (i_Obj == null)
            {
                throw new Exception();
            }

            m_Properties.Add(i_PropertyName, processPopluateRequest(i_PropertyName, i_Obj));
        }

        public VehiclePropertyInfo GetAnUnpopulatedPropertyInfo()
        {
            VehiclePropertyInfo retInfo = null;
            Dictionary<string, Type> objectRequiredProperties = r_RequiredObjectProperties;
            Dictionary<string, object> currentPopulatedProperties = m_Properties;

            foreach (KeyValuePair<string, Type> currRequiredObj in objectRequiredProperties)
            {
                if (!currentPopulatedProperties.ContainsKey(currRequiredObj.Key))
                {
                    retInfo = new VehiclePropertyInfo(currRequiredObj.Key, currRequiredObj.Value);
                    break;
                }
            }

            return retInfo;
        }
    }
}
