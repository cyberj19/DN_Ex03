using System;
using System.Collections.Generic;
using GarageLogic.VehicleParts;
using GarageLogic.VehicleParts.PowerSources;
using GarageLogic.VehicleTypes;

namespace GarageLogic
{
    public class VehicleFactory
    {
        public enum eSupportedVehicle 
        {
            ElectricCar,
            ElectricMotorcycle,
            RegularCar,
            RegularMotorcycle,
            Truck
        }

        // Consts for Allowed Vehicle Patterns
        private const string k_DefaultTiresManufacturerName = ""; // its constant, cannot assign string.empty

        private const int k_CarAmountOfTires = 4;
        private const float k_CarTireMaxAmountOfPressure = 32.0f;
        private const float k_CarMaxBattaryCapacityInHours = 2.8f;
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
        private const float k_MotorcycleMaxBattaryCapacityInHours = 1.6f;
        private readonly Dictionary<eSupportedVehicle, VehicleFactoryRecipe> r_VehicleModels;

        public VehicleFactory()
        {
            r_VehicleModels = new Dictionary<eSupportedVehicle, VehicleFactoryRecipe>();
            initDictionary();
        }

        public float GetPowerCapacityOfPowerSourceInRecipe(eSupportedVehicle i_VehicleModel)
        {
            return r_VehicleModels[i_VehicleModel].PowerSource.PowerCapacity;
        }

        public float GetTireMaxPsiInRecipe(eSupportedVehicle i_VehicleModel)
        {
            return r_VehicleModels[i_VehicleModel].Tires[0].MaxPSI;
        }

        public int GetNumTiresInRecipe(eSupportedVehicle i_VehicleModel)
        {
            return r_VehicleModels[i_VehicleModel].Tires.Count;
        }

        public Vehicle CreateUnpopulatedVehicle(
                                                eSupportedVehicle i_VehicleModel, 
                                                VehicleRegistrationInfo i_VehicleInfo,
                                                float i_InitialPowerSourceValue,
                                                TiresInfo i_TiresInfo)
        {
            VehicleFactoryRecipe recipe = r_VehicleModels[i_VehicleModel];
            PowerSource newPowerSource = createPowerSourceFromRecipe(recipe, i_InitialPowerSourceValue);
            List<Tire> newTires = createTiresFromRecipe(recipe, i_TiresInfo.TiresManufacturerNameArray);
            Vehicle newVehicle = createAccordingToRecipe(recipe);

            newVehicle.InitVehicle(newPowerSource, i_VehicleInfo, newTires);
            inflateTires(newTires, i_TiresInfo);

            return newVehicle;
        }

        private static void inflateTires(List<Tire> i_Tires, TiresInfo i_TiresInfo)
        {
            int numTires = i_Tires.Count;

            for (int i = 0; i < numTires; i++)
            {
                i_Tires[i].InflateAir(i_TiresInfo.TiresInitialAirValue[i]);
            }
        }
        
        // Create electric car Recipe
        private static VehicleFactoryRecipe createElectricCarRecipe()
        {
            return new VehicleFactoryRecipe(
                            new ElectricalSource(k_CarMaxBattaryCapacityInHours),
                            TiresFactory.ProduceTires(k_DefaultTiresManufacturerName, k_CarAmountOfTires, k_CarTireMaxAmountOfPressure),
                            typeof(Car));
        }

        // Create fuel car Recipe
        private static VehicleFactoryRecipe createFuelCarRecipe()
        {
            return new VehicleFactoryRecipe(
                            new FuelSource(k_CarFuelType, k_CarTankCapacity),
                            TiresFactory.ProduceTires(k_DefaultTiresManufacturerName, k_CarAmountOfTires, k_CarTireMaxAmountOfPressure),
                            typeof(Car));
        }

        // Create truck Recipe
        private static VehicleFactoryRecipe createTruckRecipe()
        {
            return new VehicleFactoryRecipe(
                            new FuelSource(k_TruckFuelType, k_TruckTankCapacity),
                            TiresFactory.ProduceTires(k_DefaultTiresManufacturerName, k_TruckAmountOfTires, k_TruckTireMaxAmountOfPressure),
                            typeof(Truck));
        }

        // Create electric motorcycle Recipe
        private static VehicleFactoryRecipe createElectricMotorcycleRecipe()
        {
            return new VehicleFactoryRecipe(
                            new ElectricalSource(k_MotorcycleMaxBattaryCapacityInHours),
                            TiresFactory.ProduceTires(k_DefaultTiresManufacturerName, k_MotorcycleAmountOfTires, k_MotorcycleTireMaxAmountOfPressure),
                            typeof(Motorcycle));
        }

        // Create fuel motorcycle Recipe
        private static VehicleFactoryRecipe createFuelMotorcycleRecipe()
        {
            return new VehicleFactoryRecipe(
                            new FuelSource(k_MotorcycleFuelType, k_MotorcycleTankCapacity),
                            TiresFactory.ProduceTires(k_DefaultTiresManufacturerName, k_MotorcycleAmountOfTires, k_MotorcycleTireMaxAmountOfPressure),
                            typeof(Motorcycle));
        }

        private List<Tire> createTiresFromRecipe(VehicleFactoryRecipe i_Recipe, string[] i_TiresManufacturerName)
        {
            return TiresFactory.ProduceTiresAccordingToExisting(i_TiresManufacturerName, i_Recipe.Tires);
        }

        private PowerSource createPowerSourceFromRecipe(VehicleFactoryRecipe i_Recipe, float i_InitialPowerSourceValue)
        {
            return i_Recipe.PowerSource.duplicate(i_InitialPowerSourceValue);
        }

        // init allowed vehicles to be created.
        private void initDictionary()
        {
            r_VehicleModels.Add(eSupportedVehicle.ElectricCar, createElectricCarRecipe());
            r_VehicleModels.Add(eSupportedVehicle.ElectricMotorcycle, createElectricMotorcycleRecipe());
            r_VehicleModels.Add(eSupportedVehicle.RegularCar, createFuelCarRecipe());
            r_VehicleModels.Add(eSupportedVehicle.RegularMotorcycle, createFuelMotorcycleRecipe());
            r_VehicleModels.Add(eSupportedVehicle.Truck, createTruckRecipe());
        }

        private Vehicle createAccordingToRecipe(VehicleFactoryRecipe i_VehicleRecipe)
        {
            return (Vehicle)Activator.CreateInstance(i_VehicleRecipe.Type);
        }
    }
}
