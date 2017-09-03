using System.Collections.Generic;
using System;
using GarageLogic.VehicleParts;
using GarageLogic.VehicleParts.PowerSources;
using GarageLogic.VehicleTypes;

namespace GarageLogic
{
    class VehicleFactory
    {
        public enum eSupportedVehicle //todo: reconsider
        {
            ElectricCar,
            ElectricMotorcycle,
            RegularCar,
            RegularMotorcycle,
            Truck
        }

        public enum eVehicleTypes
        {
            Motorcycle,
            Car,
            Truck
        }

        public enum ePowerSource
        {
            Electrical,
            Fuel
        }

        readonly TiresFactory r_TiresFactory;
        Dictionary<Vehicle, PowerSource> m_CarRecipes; //todo:reconsider

        public void CreateMotorcycle(string i_ModelName, string i_PlateNumber, ePowerSource i_PowerSourceType, string i_TireManufacturer, //todo: reconsider
           Motorcycle.eLicenseType i_LisenceType, int i_EngineVolumeCC)
        {
            const float k_MaxAllowedWheelPSI = 28;
            const byte k_NumOfTires = 2;

            List<Tire> tires = r_TiresFactory.ProduceTires(i_TireManufacturer, k_NumOfTires, k_MaxAllowedWheelPSI);
            VehicleRegistrationInfo vehicleInfo = new VehicleRegistrationInfo(i_ModelName, i_PlateNumber);

            if (i_PowerSourceType == ePowerSource.Electrical)
            {
                MakeElectricMotorcycle(vehicleInfo, tires, i_LisenceType, i_EngineVolumeCC);
            }
            else if (i_PowerSourceType == ePowerSource.Fuel)
            {
                MakeFuelMotorcycle(vehicleInfo, tires, i_LisenceType, i_EngineVolumeCC);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public void CreateCar(string i_ModelName, string i_PlateNumber, ePowerSource i_PowerSourceType, string i_TireManufacturer,
            Car.eDoorsAmount i_DoorsAmount, Car.eColor i_Color)
        {
            const float k_MaxAllowedWheelPSI = 32;
            const byte k_NumOfTires = 4;

            List<Tire> tires = r_TiresFactory.ProduceTires(i_TireManufacturer, k_NumOfTires, k_MaxAllowedWheelPSI);
            VehicleRegistrationInfo vehicleInfo = new VehicleRegistrationInfo(i_ModelName, i_PlateNumber);

            if (i_PowerSourceType == ePowerSource.Electrical)
            {
                MakeElectricCar(vehicleInfo, tires, i_DoorsAmount, i_Color);
            }
            else if(i_PowerSourceType == ePowerSource.Fuel)
            {
                MakeFuelCar(vehicleInfo, tires, i_DoorsAmount, i_Color);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public void CreateTruck(string i_ModelName, string i_PlateNumber, ePowerSource i_PowerSourceType, string i_TireManufacturer,
            bool i_IsCarryingDangerousMaterials, float i_MaxCarryingWeightAllowed)
        {
            const float k_MaxAllowedWheelPSI = 34;
            const byte k_NumOfTires = 12;

            List<Tire> tires = r_TiresFactory.ProduceTires(i_TireManufacturer, k_NumOfTires, k_MaxAllowedWheelPSI);
            VehicleRegistrationInfo vehicleInfo = new VehicleRegistrationInfo(i_ModelName, i_PlateNumber);

            if (i_PowerSourceType == ePowerSource.Fuel)
            {
                MakeFuelTrack(vehicleInfo, tires, i_IsCarryingDangerousMaterials, i_MaxCarryingWeightAllowed);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public Car MakeFuelCar(VehicleRegistrationInfo i_VehicleInfo, List<Tire> i_Tires, Car.eDoorsAmount i_DoorsAmount, Car.eColor i_Color)
        {
            const FuelSource.eFuelType k_CarFuelType = FuelSource.eFuelType.Octan98;
            const float k_CarTankCapacity = 50f;

            return new Car(new FuelSource(k_CarFuelType, k_CarTankCapacity), i_VehicleInfo, i_Tires, i_DoorsAmount, i_Color);
        }
        public Truck MakeFuelTrack(VehicleRegistrationInfo i_VehicleInfo, List<Tire> i_Tires, bool i_IsCarryingDangerousMaterials, float i_MaxCarryingWeightAllowed)
        {
            const FuelSource.eFuelType k_TrackFuelType = FuelSource.eFuelType.Soler;
            const float k_TruckTankCapacity = 130f;

            return new Truck(new FuelSource(k_TrackFuelType, k_TruckTankCapacity), i_VehicleInfo, i_Tires, i_IsCarryingDangerousMaterials, i_MaxCarryingWeightAllowed);
        }
        public Motorcycle MakeFuelMotorcycle(VehicleRegistrationInfo i_VehicleInfo, List<Tire> i_Tires, Motorcycle.eLicenseType i_LisenceType, int i_EngineVolumeCC)
        {
            const FuelSource.eFuelType k_MotorcycleFuelType = FuelSource.eFuelType.Octan95;
            const float k_MotorcycleTankCapacity = 5.5f;

            return new Motorcycle(new FuelSource(k_MotorcycleFuelType, k_MotorcycleTankCapacity), i_VehicleInfo, i_Tires, i_LisenceType, i_EngineVolumeCC);
        }
        public Car MakeElectricCar(VehicleRegistrationInfo i_VehicleInfo, List<Tire> i_Tires, Car.eDoorsAmount i_DoorsAmount, Car.eColor i_Color)
        {
            const float k_MaxBattaryCapacityInHours = 2.8f;

            return new Car(new ElectricalSource(k_MaxBattaryCapacityInHours), i_VehicleInfo, i_Tires, i_DoorsAmount, i_Color);
        }
        public Motorcycle MakeElectricMotorcycle(VehicleRegistrationInfo i_VehicleInfo, List<Tire> i_Tires, Motorcycle.eLicenseType i_LisenceType, int i_EngineVolumeCC)
        {
            const float k_MaxBattaryCapacityInHours = 1.6f;

            return new Motorcycle(new ElectricalSource(k_MaxBattaryCapacityInHours), i_VehicleInfo, i_Tires, i_LisenceType, i_EngineVolumeCC);
        }
    }
}
