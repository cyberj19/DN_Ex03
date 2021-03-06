﻿using System.Collections.Generic;
using GarageLogic.VehicleParts;

namespace GarageLogic
{
    // Represents a vehicle
    public abstract class Vehicle
    {
        private VehicleRegistrationInfo m_Info;
        private List<Tire> m_Tires;
        private PowerSource m_PowerSource;

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
    }
}
