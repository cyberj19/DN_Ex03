using GarageLogic.VehicleParts;
using GarageLogic.VehicleParts.PowerSources;
using GarageLogic.VehicleTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    class VehicleFactory
    {
        public enum eSupportedVehicle 
        {
            ElectricCar,
            ElectricMotorcycle,
            RegularCar,
            RegularMotorcycle,
            Truck
        }

        Dictionary<eSupportedVehicle, Vehicle> m_VehicleModels;

        public VehicleFactory()
        {
            m_VehicleModels = new Dictionary<eSupportedVehicle, Vehicle>();
            initDictionary();
        }

        private MethodInfo getMethod(string i_MethodName)
        {
            return typeof(VehicleFactory).GetMethod(i_MethodName);
        }

        private void initDictionary()
        {
            m_VehicleModels.Add(eSupportedVehicle.ElectricCar, createElectricCarModel());
            m_VehicleModels.Add(eSupportedVehicle.ElectricMotorcycle, createElectricMotorcycleModel());
            m_VehicleModels.Add(eSupportedVehicle.RegularCar, createFuelCarModel());
            m_VehicleModels.Add(eSupportedVehicle.RegularMotorcycle, createFuelMotorcycleModel());
            m_VehicleModels.Add(eSupportedVehicle.Truck, createTruckModel());

        }

        //todo: better name than model
        public Vehicle Create(eSupportedVehicle i_VehicleModel, VehicleRegistrationInfo i_VehicleInfo,
                                object i_SpecificInfo,
                                float i_InitialPowerSourceValue,
                                string[] i_TiresManufacturerName, float[] i_TiresInitialAirValue)
        {
            Vehicle model = m_VehicleModels[i_VehicleModel];
            PowerSource newPowerSource = createPowerSourceFromModel(model, i_InitialPowerSourceValue);
            List<Tire> newTires = createTiresFromModel(model, i_TiresManufacturerName);
            Type modelType = model.GetType();
            Vehicle newVehicle = null;

            if (modelType == typeof(Car))
            {
                CarInfo carInfo = i_SpecificInfo as CarInfo;

                if (carInfo == null)
                {
                    throw new ArgumentException();
                }

                newVehicle = createCarFromModel(i_VehicleInfo, carInfo, newPowerSource, newTires);
            }
            else if (modelType == typeof(Motorcycle))
            {
                MotorcycleInfo motorcycleInfo = i_SpecificInfo as MotorcycleInfo;

                if (motorcycleInfo == null)
                {
                    throw new ArgumentException();
                }

                newVehicle = createMotorcycleFromModel(i_VehicleInfo, motorcycleInfo, newPowerSource, newTires);
            }
            else if (modelType == typeof(Truck))
            {
                TruckInfo truckInfo = i_SpecificInfo as TruckInfo;

                if (truckInfo == null)
                {
                    throw new ArgumentException();
                }

                newVehicle = createTruckFromModel(i_VehicleInfo, truckInfo, newPowerSource, newTires);
            }

            return newVehicle;
        }

        private Vehicle createTruckFromModel(VehicleRegistrationInfo i_VehicleInfo, TruckInfo i_TruckInfo, PowerSource i_NewPowerSource, List<Tire> i_NewTires)
        {
            return new Truck(i_NewPowerSource, i_VehicleInfo, i_NewTires, i_TruckInfo);
        }

        private Vehicle createMotorcycleFromModel(VehicleRegistrationInfo i_VehicleInfo, MotorcycleInfo i_MotorcycleInfo, PowerSource i_NewPowerSource, List<Tire> i_NewTires)
        {
            return new Motorcycle(i_NewPowerSource, i_VehicleInfo, i_NewTires, i_MotorcycleInfo);
        }

        private Vehicle createCarFromModel(VehicleRegistrationInfo i_VehicleInfo, CarInfo i_CarInfo, PowerSource i_NewPowerSource, List<Tire> i_NewTires)
        {
            return new Car(i_NewPowerSource, i_VehicleInfo, i_NewTires, i_CarInfo);
        }
        
        private List<Tire> createTiresFromModel(Vehicle i_Model, string[] i_TiresManufacturerName)
        {
            List<Tire> modelTires = i_Model.Tires;

            return TiresFactory.ProductTiresAccordingToExisting(i_TiresManufacturerName, modelTires);
        }

        private PowerSource createPowerSourceFromModel(Vehicle i_Model, float i_InitialPowerSourceValue)
        {
            ElectricalSource electricalSource = i_Model.PowerSource as ElectricalSource;
            FuelSource fuelSource;
            FuelSource newFuelSource = null;
            ElectricalSource newElectricalSource = null;
            PowerSource newPowerSource = null;

            if (electricalSource != null)
            {
                newElectricalSource = new ElectricalSource(electricalSource.PowerCapacity);
                newElectricalSource.Recharge(i_InitialPowerSourceValue);
                newPowerSource = newElectricalSource;
            }
            else
            {
                fuelSource = i_Model.PowerSource as FuelSource;
                newFuelSource = new FuelSource(fuelSource.FuelType, fuelSource.PowerCapacity);
                newFuelSource.Refuel(newFuelSource.FuelType ,i_InitialPowerSourceValue);
                newPowerSource = newFuelSource;
            }

            return newPowerSource;
        }

        private const int k_CarAmountOfTires = 4;
        private const float k_CarTireMaxAmountOfPressure = 32.0f;
        private const float k_CarMaxBattaryCapacityInHours = 2.8f;
        private const string k_DefaultTiresManufacturerName = ""; //its constant, cannot assign string.empty

        private const FuelSource.eFuelType k_CarFuelType = FuelSource.eFuelType.Octan98;
        private const float k_CarTankCapacity = 50.0f;

        private const FuelSource.eFuelType k_TruckFuelType = FuelSource.eFuelType.Soler;
        private const float k_TruckTankCapacity = 130.0f;
        private const int k_TruckAmountOfTires = 12;
        private const float k_TruckTireMaxAmountOfPressure = 34.0f;


        private const FuelSource.eFuelType k_MotorcycleFuelType = FuelSource.eFuelType.Octan95;
        private const float k_MotorcycleTankCapacity = 5.5f;
        private const int k_MotorcycleAmountOfTires = 2;
        private const float k_MotorcycleTireMaxAmountOfPressure = 28.0f;
        private const float k_MotorcycleMaxBattaryCapacityInHours = 1.6f; //todo: Go over all create functions make sure didnt typo care instead of motorcycle etc'


        private static Vehicle createElectricCarModel()
        {
            return new Car(
                            new ElectricalSource(k_CarMaxBattaryCapacityInHours),
                            TiresFactory.ProduceTires(k_DefaultTiresManufacturerName, k_CarAmountOfTires, k_CarTireMaxAmountOfPressure)
                        );
        }

        private static Vehicle createFuelCarModel()
        {
            return new Car(
                            new FuelSource(k_CarFuelType, k_CarTankCapacity),
                            TiresFactory.ProduceTires(k_DefaultTiresManufacturerName, k_CarAmountOfTires, k_CarTireMaxAmountOfPressure)
                        );
        }

        
        private static Vehicle createTruckModel()
        {
            return new Truck(
                            new FuelSource(k_TruckFuelType, k_TruckTankCapacity),
                            TiresFactory.ProduceTires(k_DefaultTiresManufacturerName, k_TruckAmountOfTires, k_TruckTireMaxAmountOfPressure)
                        );
        }

        private static Vehicle createElectricMotorcycleModel()
        {
            return new Motorcycle(
                            new ElectricalSource(k_MotorcycleMaxBattaryCapacityInHours),
                            TiresFactory.ProduceTires(k_DefaultTiresManufacturerName, k_MotorcycleAmountOfTires, k_MotorcycleTireMaxAmountOfPressure)
                        );
        }

        private static Vehicle createFuelMotorcycleModel()
        {
            return new Motorcycle(
                            new FuelSource(k_MotorcycleFuelType, k_MotorcycleTankCapacity),
                            TiresFactory.ProduceTires(k_DefaultTiresManufacturerName, k_MotorcycleAmountOfTires, k_MotorcycleTireMaxAmountOfPressure)
                        );
        }
    }
}
